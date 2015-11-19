using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MassConvert.Database;
using MassConvert.Model;
using System.Diagnostics;
using System.Threading;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.VisualBasic;
using System.Drawing.Printing;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.Data;
using System.IO;

namespace MassConvert
{
    public partial class frmGenLabNo : Telerik.WinControls.UI.RadForm
    {
        public frmGenLabNo()
        {
            InitializeComponent();
        }
        #region Attribute
        SQL db;
        string OwnerOrganization = "15";
        string BU = "15";
        string Cuser = "1";
        //Encounter Type = HealthPromotion
        string Encounter_UID = "10063";
        string Encounter_Desc = "HealthPromotion";

        //Location = Mobile Checkup
        string Location_UID = "69";

        //Care Provider = พยาบาล Checkup
        string CarePro_UID = "2283";

        //Log in UID
        string Login_UID = "652";

        DataTable dtPatient = new DataTable();
        BindingSource bs = new BindingSource();
        #endregion Attribute

        private void frmGenLabNo_Load(object sender, EventArgs e)
        {
            db = new SQL();
            dtpDateFrom.Value = DateTime.Today;
            dtpDateTo.Value = DateTime.Today;
            dtpTimeFrom.Value = Convert.ToDateTime("06:00:00");
            dtpTimeTo.Value = Convert.ToDateTime("06:00:00");

            CheckCareproviderUID(Login_UID);
            AddingCheckBoxColumn();
        }
        private void btFind_Click(object sender, EventArgs e)
        {
            ExcData exc = new ExcData();

            string DateFrom = dtpDateFrom.Value.ToString("yyyy-MM-dd") + " " + dtpTimeFrom.Value.ToString("HH:mm");
            string DateTo = dtpDateTo.Value.ToString("yyyy-MM-dd") + " " + dtpTimeTo.Value.ToString("HH:mm");
            string SQL = string.Empty;

            SQL = "SELECT Forename as Name , Surname as LastName , DOE , [NO] , [ChildCompany],[STS] FROM [tblPatientList] WHERE DOE BETWEEN '" + DateFrom + "' AND '" + DateTo + "' ORDER BY NO";

            dtPatient = exc.data_Table(SQL);
            bs.DataSource = dtPatient;
            gvPatient.DataSource = bs;

            gvPatient.Columns["Name"].Width = 150;
            gvPatient.Columns["LastName"].Width = 150;
            gvPatient.Columns["DOE"].Width = 140;
            gvPatient.Columns["NO"].Width = 50;
            gvPatient.Columns["ChildCompany"].Width = 280;
            gvPatient.Columns["STS"].Width = 50;
            gvPatient.Refresh();

            lblCountPT.Text = dtPatient.Rows.Count.ToString() + " Record.";

            CheckAll();

        }
        private void chkBox_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (chkBox.Checked == true)
            {
                CheckAll();
            }
            else
            {
                UnCheckAll();
            }
        }
        private void txtFilterPayor_TextChanged(object sender, EventArgs e)
        {
            bs.Filter = string.Format("ChildCompany LIKE '%{0}%'", txtFilterPayor.Text.Trim());
            CheckAll();
        }
        private void btGenLabNo_Click(object sender, EventArgs e)
        {
            int CountCheckBox = 0;
            for (int r = 0; r <= gvPatient.Rows.Count - 1; r++)
            {
                //Loop ทำงานเฉพาะคนที่ Check Box
                if (Convert.ToBoolean(gvPatient.Rows[r].Cells["Check"].Value) == true)
                {
                    CountCheckBox++;
                }
            }
            progressBar1.Maximum = CountCheckBox;
            progressBar1.Step = 1;
            progressBar1.Value1 = 0;

            for (int i = 0; i <= gvPatient.Rows.Count - 1; i++)
            {
                //Loop ทำงานเฉพาะคนที่ Check Box
                if (Convert.ToBoolean(gvPatient.Rows[i].Cells["Check"].Value) == true)
                {
                    StringBuilder SQLPT = new StringBuilder();
                    SQLPT.Append("SELECT * FROM [PatientScheduleOrder] ps");
                    SQLPT.Append(" inner join Patient p on p.UID = ps.PatientUID ");
                    SQLPT.Append(" and p.Forename = N'" + gvPatient.Rows[i].Cells["Name"].Value.ToString().Trim() + "'");
                    SQLPT.Append(" and Surname = N'" + gvPatient.Rows[i].Cells["LastName"].Value.ToString().Trim() + "'");
                    SQLPT.Append(" and ps.ScheduledDttm between '" + Convert.ToDateTime(gvPatient.Rows[i].Cells["DOE"].Value).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(gvPatient.Rows[i].Cells["DOE"].Value).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                    SQLPT.Append(" and ps.StatusFlag = 'A'");
                    DataTable dt = new DataTable();
                    dt = db.Select_OrderNo(SQLPT.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        GenLabEpisode(dt.Rows[0]["ScheduleOrderNumber"].ToString(), dt.Rows[0]["ScheduleOrderNumber"].ToString());
                        //GenLabEpisode(180,180);
                    }
                }
                if (progressBar1.Value1 > progressBar1.Maximum)
                {
                    lblStatus.Text = "Generating lab number of " + gvPatient.Rows[i].Cells["Name"].Value.ToString().Trim() + "  " + gvPatient.Rows[i].Cells["LastName"].Value.ToString().Trim();
                    progressBar1.Value1++;
                }
            }
            lblStatus.Text = "Generate lab number sucessful.";
            MessageBox.Show("Generate lab number sucessful.");
        }
        private void btPrintSticker_Click(object sender, EventArgs e)
        {
            int CountCheckBox = 0;
            for (int r = 0; r <= gvPatient.Rows.Count - 1; r++)
            {
                //Loop ทำงานเฉพาะคนที่ Check Box
                if (Convert.ToBoolean(gvPatient.Rows[r].Cells["Check"].Value) == true)
                {
                    CountCheckBox++;
                }
            }
            progressBar1.Maximum = CountCheckBox;
            progressBar1.Value1 = 0;
            progressBar1.Step = 1;

            int PrnNo = 1;
            string PrnValue = "1";
            if (InputBox("Print Sticker Lab", "Number of copies : ", ref PrnValue) == DialogResult.OK)
            {
                PrnNo = Convert.ToInt32(PrnValue);
            }
            for (int i = 0; i <= gvPatient.Rows.Count - 1; i++)
            {
                //Loop ทำงานเฉพาะคนที่ Check Box
                if (Convert.ToBoolean(gvPatient.Rows[i].Cells["Check"].Value) == true)
                {
                    StringBuilder SQLPT = new StringBuilder();
                    SQLPT.Append("SELECT * FROM [PatientScheduleOrder] ps");
                    SQLPT.Append(" inner join Patient p on p.UID = ps.PatientUID ");
                    SQLPT.Append(" and p.Forename = N'" + gvPatient.Rows[i].Cells["Name"].Value.ToString() + "'");
                    SQLPT.Append(" and Surname = N'" + gvPatient.Rows[i].Cells["LastName"].Value.ToString() + "'");
                    SQLPT.Append(" and ps.ScheduledDttm between '" + gvPatient.Rows[i].Cells["DOE"].Value.ToString() + "' and '" + gvPatient.Rows[i].Cells["DOE"].Value.ToString() + "'");
                    SQLPT.Append(" and ps.StatusFlag = 'A'");
                    DataTable dt = new DataTable();
                    dt = db.Select_OrderNo(SQLPT.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        progressBar1.Value1 += 1;
                        printSticker(dt.Rows[0]["ScheduleOrderNumber"].ToString(), PrnNo, gvPatient.Rows[i].Cells["NO"].Value.ToString());
                    }
                }
            }
        }

        //Custom Method
        private void CheckAll()
        {
            foreach (GridViewRowInfo row in gvPatient.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Check"].Value) == false)
                {
                    row.Cells["Check"].Value = true;
                }
            }
        }
        private void UnCheckAll()
        {
            foreach (GridViewRowInfo row in gvPatient.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Check"].Value) == true)
                {
                    row.Cells["Check"].Value = false;
                }
            }
        }
        private void Update_Status_In_TblPatientList(string ScheduleOrderUID)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = db.Select_Patient_For_Update_TblPatientList(ScheduleOrderUID);
                db.Update_TblPatientList(dt.Rows[0]["Forename"].ToString().Trim(), dt.Rows[0]["Surname"].ToString().Trim(), dt.Rows[0]["ScheduledDttm"].ToString().Trim(), dt.Rows[0]["BirthDttm"].ToString().Trim());
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        private void CheckCareproviderUID(string login)
        {
            DataTable ldt = db.Select_Login_by_LoginName(login);
            if (ldt != null && ldt.Rows.Count > 0)
            {
                Cuser = ldt.Rows[0]["CareproviderUID"].ToString();
            }
        }
        private void AddingCheckBoxColumn()
        {
            GridViewCheckBoxColumn colChk = new GridViewCheckBoxColumn();
            colChk.Name = "Check";
            colChk.HeaderText = "เลือก";
            gvPatient.Columns.Add(colChk);

            for (int i = 0; i < gvPatient.Rows.Count; i++)
            {
                gvPatient.Rows[i].Cells["colChk"].Value = false;
            }
        }
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
        private void printSticker(string OrderNo, int PrnNo,string NO)
        {
            string LabNo = string.Empty;
            string HN = string.Empty;
            string Name = string.Empty;
            string DOB = string.Empty;
            string RunningNo = string.Empty;
            string strInfor = string.Empty;

            //หาค่ามูล Lab Episode และ Patient Info ใน table BDMSASTMassConvert
            DataTable mdt = db.Select_BDMSASTMassConvert_Between(OrderNo, OrderNo);
            if (mdt != null && mdt.Rows.Count > 0)
            {
                for (int i = 0; i < PrnNo; i++)
                {   //ถ้ามี Lab Number
                    if (mdt.Rows[0]["LabNo"].ToString() != string.Empty)
                    {
                        LabNo = mdt.Rows[0]["LabNo"].ToString();
                        HN = "HN:" + mdt.Rows[0]["HN"].ToString();
                        Name = "PT: " + mdt.Rows[0]["PatientFullName"].ToString();
                        RunningNo = "No: " + NO;

                        if (mdt.Rows[0]["HN"].ToString() != "")
                        {
                            DataTable Patdt = db.Select_Patient_By_HN(mdt.Rows[0]["HN"].ToString());
                            if (Patdt != null && Patdt.Rows.Count > 0)
                            {
                                try
                                {
                                    string bd = Convert.ToDateTime(Patdt.Rows[0]["BirthDttm"].ToString()).ToString("dd MMM yyyy");
                                    string yy = Convert.ToDateTime(Patdt.Rows[0]["BirthDttm"].ToString()).ToString("yyyy");
                                    DOB = "DOB: " + bd + " (" + (int.Parse(yy) + 543).ToString() + ")";
                                }
                                catch (Exception er)
                                {
                                    er.ToString();
                                }

                            }
                        }
                        strInfor += HN + "\t" + LabNo + "\t" + Name + "\t" + RunningNo + "\t" + DOB + "\n\r";                       
                    }
                }
                //ถ้ามี textfile อยู่ให้ลบออกก่อน
                if (File.Exists("D:\\StikerPrinting\\ListPatient.txt"))
                {
                    File.Delete("D:\\StikerPrinting\\ListPatient.txt");
                }
                WriteTextFile("D:\\StikerPrinting\\ListPatient.txt", strInfor);

                if (!File.Exists("D:\\StikerPrinting\\BARTEND.EXE"))
                {
                    MessageBox.Show("Bartend.exe not found", "Warning");
                    System.Environment.Exit(0);
                }
                else if (!File.Exists("D:\\StikerPrinting\\BarcodeDesign.BTW"))
                {
                    MessageBox.Show("BarcodeDesign.BTW not found", "Warning");
                    System.Environment.Exit(0);
                }
                else
                {
                    Thread.Sleep(1500);
                    string outputMessage = string.Empty;
                    string errorMessage = string.Empty;
                    ExecuteShellCommand("D:\\StikerPrinting\\bartend.exe", "D:\\StikerPrinting\\BarcodeDesign.BTW /p /x", ref outputMessage, ref errorMessage);
                }
            }
        }
        
        private void GenLabEpisode(string OrderNoFrom, string OrderNoTo)
        {
            //หาว่า ScheduleNo ของคนไข้ จากข้อมูลที่ทำการ Bulk Regis
            DataTable Schdt = db.Select_PatientScheduleOrder_Between(OrderNoFrom, OrderNoTo);
            //dataGridView1.DataSource = Schdt;
            //if (dataGridView1.RowCount > 0)
            if (Schdt.Rows.Count > 0)
            {
                //for (int i = 0; i < dataGridView1.RowCount; i++)
                for (int i = 0; i < Schdt.Rows.Count; i++)
                {
                    //เช็คว่ามีการ Generate Lab No ไปแล้วหรือยัง
                    DataTable dtchkGenLab = db.Select_BDMSASTMassConvert_Between(OrderNoFrom, OrderNoTo);
                    if (dtchkGenLab == null || dtchkGenLab.Rows.Count == 0)
                    {
                        List<LabItemObject> litmobj = new List<LabItemObject>();
                        //string scheduleUID = dataGridView1.Rows[i].Cells[0].Value.ToString();//UID
                        //string scheduleNo = dataGridView1.Rows[i].Cells[1].Value.ToString();//ScheduleOrderNo
                        //string patientuid = dataGridView1.Rows[i].Cells[2].Value.ToString();//PatientUID
                        string scheduleUID = Schdt.Rows[i][0].ToString(); //UID
                        string scheduleNo = Schdt.Rows[i][1].ToString();//ScheduleOrderNo
                        string patientuid = Schdt.Rows[i][2].ToString();//PatientUID

                        string LabSEQ = db.SEQID("SEQRequest");
                        LabSEQ = OwnerOrganization + "1" + LabSEQ.PadLeft(6, '0');
                        //หาว่าคนไข้คนนี้มี Order อะไรบ้าง
                        DataTable catdt = db.Select_OrderCategory_By_ScheduleOrderNumber_Filter_Package_Only(scheduleUID);
                        if (catdt != null && catdt.Rows.Count > 0)
                        {
                            string val = string.Empty;
                            //เช็คว่ามี Order Lab อยู่ใน Package นี้หรือไม่
                            if (CheckLabItemTypeInsidePackage(catdt, out val) == true)
                            {
                                for (int x = 0; x < catdt.Rows.Count; x++)
                                {
                                    //หาเฉพาะ Order Lab ,Xray ของ Package นี้
                                    DataTable sdetdt = db.Select_OrderCategory_By_ScheduleOrderNumber_Filter_Package_Only_Lab_And_Xray(scheduleUID);
                                    if (sdetdt != null && sdetdt.Rows.Count > 0)
                                    {
                                        for (int j = 0; j < sdetdt.Rows.Count; j++)
                                        {
                                            //เช็ค Order Item ว่าอยู่ Item Category อะไร
                                            DataTable ldt = db.Select_RequestItem_By_UID_For_CheckItemCategory(sdetdt.Rows[j]["ItemUID"].ToString());
                                            if (ldt != null && ldt.Rows.Count > 0)
                                            {   //ถ้าอยู่ใน Category LAB
                                                if (ldt.Rows[0]["ItemCat"].ToString().ToUpper() == "LAB")
                                                {
                                                    string speceminitem = string.Empty;
                                                    //หา Specimen Code
                                                    DataTable spedt = db.Select_RequestItemSpecimen_By_RequestItemUID(sdetdt.Rows[j]["ItemUID"].ToString(), "Y");
                                                    if (spedt != null && spedt.Rows.Count > 0)
                                                        speceminitem = spedt.Rows[0]["SpecimenCode"].ToString();
                                                    else
                                                    {
                                                        spedt = db.Select_RequestItemSpecimen_By_RequestItemUID(sdetdt.Rows[j]["ItemUID"].ToString(), "N");
                                                        if (spedt != null && spedt.Rows.Count > 0)
                                                            speceminitem = spedt.Rows[0]["SpecimenCode"].ToString();

                                                    }//if (spedt != null && spedt.Rows.Count > 0)

                                                    //กำหนดค่าให้ List Object
                                                    litmobj.Add(new LabItemObject()
                                                    {
                                                        TestName = ldt.Rows[0]["ItemDescription"].ToString(),
                                                        TestSetCode = ldt.Rows[0]["ItemCode"].ToString(),
                                                        Specimen = speceminitem

                                                    });


                                                }//if (ldt.Rows[0]["ItemCat"].ToString() == "LAB")


                                            }//if (ldt != null && ldt.Rows.Count > 0)



                                        }//for (int j = 0; j < sdetdt.Rows.Count; j++)


                                    }//if (sdetdt != null && sdetdt.Rows.Count > 0)

                                }//for (int x = 0; x < catdt.Rows.Count; x++)

                                //MessageBox.Show("This version not support Medicine Item inside Package");
                                //return;

                            }//if (CheckLabItemTypeInsidePackage(catdt,out val) == true)
                        }

                        //หาข้อมูลคนไข้
                        DataTable patdt = db.Select_Patient_By_UID(patientuid);
                        string hn = string.Empty;
                        string patfull = string.Empty;
                        if (patdt != null && patdt.Rows.Count > 0)
                        {
                            hn = patdt.Rows[0]["pasid"].ToString();
                            patfull = patdt.Rows[0]["TitleDesc"].ToString() + " " + patdt.Rows[0]["Forename"].ToString() + " " + patdt.Rows[0]["Surname"].ToString();
                        }

                        string spelst = string.Empty;
                        string TestSetMapbySpecimen = string.Empty;
                        //
                        litmobj.Where(f => f.Specimen != string.Empty).Select(f => f.Specimen).Distinct().ToList().ForEach(f =>
                        {

                            spelst += f.ToString() + "|";
                            TestSetMapbySpecimen = "";
                            if (f.ToString() != "")
                            {

                                litmobj.Where(x => x.Specimen == f.ToString()).Select(x => x.TestSetCode).Distinct().ToList().ForEach(x =>
                                {
                                    TestSetMapbySpecimen += x.ToString() + ",";
                                });

                                db.Delete_BDMSASTMassSpecimenTestSet(hn, f.ToString(), scheduleUID);
                                db.Insert_BDMSASTMassSpecimenTestSet(hn, f.ToString(), TestSetMapbySpecimen, scheduleUID);

                            }

                        });


                        db.Update_BDMSASTMassConvert_By_PatientScheduleOrderUID(scheduleUID, "D");
                        db.Insert_BDMSASTMassConvert(scheduleUID, scheduleNo, LabSEQ
                            , "N", "A"
                            , litmobj.Where(f => f.Specimen != string.Empty).Select(f => f.Specimen).Distinct().Count().ToString()
                            , hn, patientuid, patfull, spelst, "");
                    }
                    //ปิดการเช็คว่ามีการ Generate Lab No ไปแล้วหรือยัง
                } //for (int i = 0; i < dataGridView1.RowCount; i++)
            }
        }
        bool CheckLabItemTypeInsidePackage(DataTable pkdt, out string value)
        {
            bool flag = false;
            value = "";
            if (pkdt != null && pkdt.Rows.Count > 0)
            {
                for (int i = 0; i < pkdt.Rows.Count; i++)
                {
                    if (pkdt.Rows[i]["Description"].ToString().ToLower() == "lab")
                    {
                        value = pkdt.Rows[i]["Value"].ToString().ToLower();
                        flag = true;
                        return true;
                    }
                }
            }
            return flag;
        }
        private void WriteTextFile(string path, string data)
        {
            using (StreamWriter outfile = new StreamWriter(path,false,Encoding.Default))
            {
                outfile.Write(data);
            }
        }
        private void ExecuteShellCommand(string _FileToExecute, string _CommandLine, ref string _outputMessage, ref string _errorMessage)
        {
            // Set process variable
            // Provides access to local and remote processes and enables you to start and stop local system processes.
            System.Diagnostics.Process _Process = null;
            try
            {
                _Process = new System.Diagnostics.Process();

                // invokes the cmd process specifying the command to be executed.
                string _CMDProcess = string.Format(System.Globalization.CultureInfo.InvariantCulture, @"{0}\cmd.exe", new object[] { Environment.SystemDirectory });
                // pass executing file to cmd (Windows command interpreter) as a arguments
                // /C tells cmd that we want it to execute the command that follows, and then exit.
                string _Arguments = string.Format(System.Globalization.CultureInfo.InvariantCulture, "/C {0}", new object[] { _FileToExecute });
                // pass any command line parameters for execution
                if (_CommandLine != null && _CommandLine.Length > 0)
                {
                    _Arguments += string.Format(System.Globalization.CultureInfo.InvariantCulture, " {0}", new object[] { _CommandLine, System.Globalization.CultureInfo.InvariantCulture });
                }

                // Specifies a set of values used when starting a process.
                System.Diagnostics.ProcessStartInfo _ProcessStartInfo = new System.Diagnostics.ProcessStartInfo(_CMDProcess, _Arguments);
                // sets a value indicating not to start the process in a new window. 
                _ProcessStartInfo.CreateNoWindow = true;
                // sets a value indicating not to use the operating system shell to start the process. 
                _ProcessStartInfo.UseShellExecute = false;
                // sets a value that indicates the output/input/error of an application is written to the Process.
                _ProcessStartInfo.RedirectStandardOutput = true;
                _ProcessStartInfo.RedirectStandardInput = true;
                _ProcessStartInfo.RedirectStandardError = true;
                _Process.StartInfo = _ProcessStartInfo;
                // Starts a process resource and associates it with a Process component.
                _Process.Start();
                // Instructs the Process component to wait indefinitely for the associated process to exit.
                _errorMessage = _Process.StandardError.ReadToEnd();
                _Process.WaitForExit();
                // Instructs the Process component to wait indefinitely for the associated process to exit.
                _outputMessage = _Process.StandardOutput.ReadToEnd();
                _Process.WaitForExit();
            }
            catch (Win32Exception _Win32Exception)
            {
                // Error
                Console.WriteLine("Win32 Exception caught in process: {0}", _Win32Exception.ToString());
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }
            finally
            {
                // close process and do cleanup
                _Process.Close();
                _Process.Dispose();
                _Process = null;
            }
        }
    }
}
