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
    public partial class frmPrnStkLab : Telerik.WinControls.UI.RadForm
    {
        DataTable dtPatient = new DataTable();
        BindingSource bs = new BindingSource();
        public frmPrnStkLab()
        {
            InitializeComponent();
        }

        private void rdbPayor_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            dtpDateFrom.Enabled = true;
            dtpDateTo.Enabled = true;
            dtpTimeFrom.Enabled = true;
            dtpTimeTo.Enabled = true;

            txtHN.Enabled = false;
        }

        private void rdbHN_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            dtpDateFrom.Enabled = false;
            dtpDateTo.Enabled = false;
            dtpTimeFrom.Enabled = false;
            dtpTimeTo.Enabled = false;

            txtHN.Enabled = true;
        }

        private void btFind_Click(object sender, EventArgs e)
        {
            ExcData exc = new ExcData();

            string DateFrom = dtpDateFrom.Value.ToString("yyyy-MM-dd") + " " + dtpTimeFrom.Value.ToString("HH:mm");
            string DateTo = dtpDateTo.Value.ToString("yyyy-MM-dd") + " " + dtpTimeTo.Value.ToString("HH:mm");
            string SQL = string.Empty;
            var strSQL = new StringBuilder();
            if (rdbPayor.IsChecked == true)
            {
                strSQL.Append("SELECT A.HN,A.Name,A.LastName,A.DOE,A.[NO],A.[Payor],B.[BookCreate],A.[Shift],A.[LabEpisode] LabNo,A.[DOB] ");
                strSQL.Append("FROM [Patient] A ");
                strSQL.Append("LEFT JOIN tblPatientList B ON A.HN=B.HN AND A.DOE=B.DOE ");
                strSQL.Append("WHERE A.DOE BETWEEN '" + DateFrom + "' AND '" + DateTo + "' ORDER BY NO;");
                //SQL = "SELECT HN, Name , LastName , DOE , [NO] , [Payor],[BookCreate],[Shift], [LabEpisode] LabNo ,[DOB] FROM [Patient] WHERE DOE BETWEEN '" + DateFrom + "' AND '" + DateTo + "' ORDER BY NO";
            }
            else
            {
                strSQL.Append("SELECT A.HN,A.Name,A.LastName,A.DOE,A.[NO],A.[Payor],B.[BookCreate],A.[Shift],A.[LabEpisode] LabNo,A.[DOB] ");
                strSQL.Append("FROM [Patient] A ");
                strSQL.Append("LEFT JOIN tblPatientList B ON A.HN=B.HN AND A.DOE=B.DOE ");
                strSQL.Append("WHERE A.HN='" + txtHN.Text.Trim() + "' ORDER BY NO;");
                //SQL = "SELECT HN, Name , LastName , DOE , [NO] , [Payor],[BookCreate],[Shift], [LabEpisode] LabNo ,[DOB] FROM [Patient] WHERE HN = '" + txtHN.Text.Trim() + "' ORDER BY DOE DESC";
            }
            dtPatient = exc.data_Table(strSQL.ToString());
            strSQL.Length = 0; strSQL.Capacity = 0;
            if (dtPatient != null && dtPatient.Rows.Count > 0)
            {
                bs.DataSource = dtPatient;
                gvPatient.DataSource = bs;

                gvPatient.Columns["HN"].Width = 80;
                gvPatient.Columns["Name"].Width = 130;
                gvPatient.Columns["LastName"].Width = 130;
                gvPatient.Columns["DOE"].Width = 100;
                gvPatient.Columns["NO"].Width = 50;
                gvPatient.Columns["Payor"].Width = 200;
                gvPatient.Columns["BookCreate"].Width = 100;
                gvPatient.Columns["Shift"].Width = 80;
                gvPatient.Columns["LabNo"].Width = 50;
                gvPatient.Columns["DOB"].Width = 50;
                gvPatient.Refresh();
            }
            else
            {
                MessageBox.Show("ไม่พบข้อมูล");
            }
            lblCountPT.Text = dtPatient.Rows.Count.ToString() + " Record.";

            CheckAll();
        }
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

        private void btPrintSticker_Click(object sender, EventArgs e)
        {
            int CountCheckBox = 0;
            string Fullname = string.Empty;
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
            //MessageBox.Show(CountCheckBox.ToString()); return;
            int PrnNo = 1;
            string PrnValue = "1";
            if (InputBox("Print Sticker Lab", "Number of copies : ", ref PrnValue) == DialogResult.OK)
            {
                PrnNo = Convert.ToInt32(PrnValue);
            }
            if (gvPatient.Rows.Count > 0)
            {
                for (int i = 0; i <= gvPatient.Rows.Count - 1; i++)
                {
                    //Loop ทำงานเฉพาะคนที่ Check Box
                    if (Convert.ToBoolean(gvPatient.Rows[i].Cells["Check"].Value) == true)
                    {
                        //StringBuilder SQLPT = new StringBuilder();
                        //SQLPT.Append("SELECT * FROM [PatientScheduleOrder] ps");
                        //SQLPT.Append(" inner join Patient p on p.UID = ps.PatientUID ");
                        //SQLPT.Append(" and p.Forename = N'" + gvPatient.Rows[i].Cells["Name"].Value.ToString() + "'");
                        //SQLPT.Append(" and Surname = N'" + gvPatient.Rows[i].Cells["LastName"].Value.ToString() + "'");
                        //SQLPT.Append(" and ps.ScheduledDttm between '" + gvPatient.Rows[i].Cells["DOE"].Value.ToString() + "' and '" + gvPatient.Rows[i].Cells["DOE"].Value.ToString() + "'");
                        //SQLPT.Append(" and ps.StatusFlag = 'A'");
                        //DataTable dt = new DataTable();
                        //dt = db.Select_OrderNo(SQLPT.ToString());
                        //if (dt.Rows.Count > 0)
                        //{
                        Fullname = gvPatient.Rows[i].Cells["Name"].Value.ToString() + "  " + gvPatient.Rows[i].Cells["LastName"].Value.ToString();
                        progressBar1.Value1 += 1;
                        printSticker(PrnNo, gvPatient.Rows[i].Cells["HN"].Value.ToString(), gvPatient.Rows[i].Cells["LabNo"].Value.ToString(), Fullname, gvPatient.Rows[i].Cells["DOB"].Value.ToString(), gvPatient.Rows[i].Cells["NO"].Value.ToString());
                        //printSticker(dt.Rows[0]["ScheduleOrderNumber"].ToString(), PrnNo, gvPatient.Rows[i].Cells["NO"].Value.ToString());
                        //}
                    }
                }
            }
        }
        //private void printSticker(string OrderNo, int PrnNo, string NO)
        private void printSticker(int PrnNo,string strHN ,string strLabNo,string strName,string strDOB,string strRunningNo )
        {
            string LabNo = string.Empty;
            string HN = string.Empty;
            string Name = string.Empty;
            string DOB = string.Empty;
            string RunningNo = string.Empty;
            string strInfor = string.Empty;

            //หาค่ามูล Lab Episode และ Patient Info ใน table BDMSASTMassConvert
            //DataTable mdt = db.Select_BDMSASTMassConvert_Between(OrderNo, OrderNo);
            //if (gvPatient.Rows.Count > 0)
            //{
                for (int i = 0; i < PrnNo; i++)
                {   //ถ้ามี Lab Number
                    //if (mdt.Rows[0]["LabNo"].ToString() != string.Empty)
                    //{
                        //LabNo = mdt.Rows[0]["LabNo"].ToString();
                        //HN = "HN:" + mdt.Rows[0]["HN"].ToString();
                        //Name = "PT: " + mdt.Rows[0]["PatientFullName"].ToString();
                        //RunningNo = "No: " + NO;
                    LabNo = strLabNo;
                    HN = "HN:" + strHN;
                    Name = "PT: " + strName;
                    RunningNo = "No: " + strRunningNo;

                        //if (mdt.Rows[0]["HN"].ToString() != "")
                        //{
                        //    DataTable Patdt = db.Select_Patient_By_HN(mdt.Rows[0]["HN"].ToString());
                        //    if (Patdt != null && Patdt.Rows.Count > 0)
                        //    {
                                try
                                {
                                    string bd = Convert.ToDateTime(strDOB).ToString("dd MMM yyyy");
                                    string yy = Convert.ToDateTime(strDOB).ToString("yyyy");
                                    DOB = "DOB: " + bd + " (" + (int.Parse(yy) + 543).ToString() + ")";
                                }
                                catch (Exception er)
                                {
                                    er.ToString();
                                }

                        //    }
                        //}
                        strInfor += HN + "\t" + LabNo + "\t" + Name + "\t" + RunningNo + "\t" + DOB + "\n\r";
                    //}
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
            //}
        }
        private void WriteTextFile(string path, string data)
        {
            using (StreamWriter outfile = new StreamWriter(path, false, Encoding.Default))
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

        private void frmPrnStkLab_Load(object sender, EventArgs e)
        {
            dtpDateFrom.Value = DateTime.Today;
            dtpDateTo.Value = DateTime.Today;
            dtpTimeFrom.Value = Convert.ToDateTime("06:00:00");
            dtpTimeTo.Value = Convert.ToDateTime("06:00:00");
            AddingCheckBoxColumn();
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
            Filter();
        }

        private void txtFilterBookCreate_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void txtFilterShift_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void Filter()
        {
            var filter = "";

            #region FilterBuilder
            if (txtFilterPayor.Text.Trim().Length > 0)
            {
                if (filter.Length > 0) { filter += " AND "; }
                filter += string.Format("Payor LIKE '%{0}%'", txtFilterPayor.Text.Trim());
            }
            if (txtFilterBookCreate.Text.Trim().Length > 0)
            {
                if (filter.Length > 0) { filter += " AND "; }
                filter += string.Format("BookCreate LIKE '%{0}%'", txtFilterBookCreate.Text.Trim());
            }
            if (txtFilterShift.Text.Trim().Length > 0)
            {
                if (filter.Length > 0) { filter += " AND "; }
                filter += string.Format("Shift LIKE '%{0}%'", txtFilterShift.Text.Trim());
            }
            #endregion
            if (filter.Length > 0)
            {
                bs.Filter = filter;
            }
            else
            {
                bs.RemoveFilter();
            }
            lblFIlterCount.Text = string.Format("พบข้อมูลที่ตรงเงื่อนไขทั้งหมด {0}", gvPatient.Rows.Count.ToString());
            CheckAll();
        }
    }
}
