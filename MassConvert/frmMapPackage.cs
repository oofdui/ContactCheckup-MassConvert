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

namespace MassConvert
{
    public partial class frmMapPackage : Form
    {
        ExcData exc;
        SQL db;
        public frmMapPackage()
        {
            InitializeComponent();
        }
        private void frmMapPackage_Load(object sender, EventArgs e)
        {
            AddingCheckBoxColumn();
        }

        private void btLoad_Click(object sender, EventArgs e)
        {
            db = new SQL();

            string PatientUID = string.Empty;
            string Forename = string.Empty;
            string Surname = string.Empty;
            string strDOE = string.Empty;
            DateTime DOE = new DateTime();
            string PackageCode = string.Empty;
            int PatientScheduleOrderUID = 0;
            int PackageUID = 0;
            string PackageName = string.Empty;
            string ProChkList = string.Empty;
            string SQLIns = string.Empty;
            int BDMSPatientScheduleOrderDetailUID = 0;

            progressBar1.Maximum = gvPatient.Rows.Count;
            progressBar1.Value = 0;

            //วนลูปอ่านจาก Gride
            for (int i = 1; i <= gvPatient.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvPatient.Rows[i - 1].Cells["Check"].Value) == true)
                {
                    //Forename
                    Forename = gvPatient.Rows[i - 1].Cells["Name"].Value.ToString().Trim();
                    //Surname
                    Surname = gvPatient.Rows[i - 1].Cells["LastName"].Value.ToString().Trim();
                    //DOE
                    DOE = Convert.ToDateTime(gvPatient.Rows[i - 1].Cells["DOE"].Value);
                    //ProChkList
                    ProChkList = gvPatient.Rows[i - 1].Cells["ProChkList"].Value.ToString().Trim();

                    //หา PatientUID
                    PatientUID = FindPatientUID(Forename.Trim(), Surname.Trim(), DOE);
                    //หา PatientScheduleOrderUID
                    PatientScheduleOrderUID = Convert.ToInt32(FindPatientScheduleOrderUID(PatientUID.Trim(), DOE));

                    //หาว่า ProCheckList นี้ผูกกับ Package อะไรบ้าง
                    DataTable dtProMapPackage = new DataTable();
                    dtProMapPackage = FindProchklistMapPackage(ProChkList.Trim());
                    for (int j = 0; j <= dtProMapPackage.Rows.Count - 1; j++)
                    {
                        //หา Package
                        DataTable dtPackage = new DataTable();
                        dtPackage = FindPackage(dtProMapPackage.Rows[j]["PackageCode"].ToString());
                        //PackageUID
                        PackageUID = Convert.ToInt32(dtPackage.Rows[0]["UID"].ToString());
                        //PackageName
                        PackageName = dtPackage.Rows[0]["PackageName"].ToString();

                        //เช็คว่าเคย Load ไปแล้วหรือยัง
                        BDMSPatientScheduleOrderDetailUID = CheckRepeat(PatientUID, PatientScheduleOrderUID, PackageUID, DOE);
                        if (BDMSPatientScheduleOrderDetailUID == 0)
                        {
                            if (db.Insert_BDMSPatientScheduleOrderDetail(PatientScheduleOrderUID, PackageName.Trim(), PackageUID, DOE, PatientUID.Trim()) == false)
                            {
                                MessageBox.Show("Save failed");
                            }
                        }
                        else
                        {
                            if (db.Update_BDMSPatientScheduleOrderDetail(PatientScheduleOrderUID, PackageName.Trim(), PackageUID, DOE, PatientUID.Trim(), BDMSPatientScheduleOrderDetailUID, PackageName) == false)
                            {
                                MessageBox.Show("Update failed");
                            }
                        }
                    }
                }
                progressBar1.Value += 1;
            }

            gvPatient.DataSource = BindProchklistMapPackage(DOE);
            gvPatient.Columns["UID"].Width = 50;
            gvPatient.Columns["PatientScheduleOrderUID"].Width = 50;
            gvPatient.Columns["BillPackageUID"].Width = 50;
            gvPatient.Columns["PatientUID"].Width = 50;
            gvPatient.Refresh();
            
            MessageBox.Show("Seccessful");
        }
        private string FindPatientUID(string ForeName, string SurName, DateTime DOE)
        {
            string PatientUID = "";
            //string SQL = "select UID from Patient where Forename = N'" + ForeName + "' and Surname = N'" + SurName + "' and StatusFlag = 'A'";
            string SQL = "select p.UID from patient p inner join patientscheduleorder ps on p.uid = ps.patientuid where p.forename = N'" + ForeName + "' and p.surname = N'" + SurName + "' and p.statusflag = 'A' and ps.statusflag = 'A' and ScheduledDttm = '" + DOE + "'";
            //exc = new ExcData();
            db = new SQL();
            DataTable dt = new DataTable();
            dt = db.FindDataTable(SQL);
            if (dt.Rows.Count > 0)
            {
                PatientUID = dt.Rows[0]["UID"].ToString();
            }
            return PatientUID;
        }
        private string FindPatientScheduleOrderUID(string PatientUID, DateTime DOE)
        {
            DateTime ChkDate = new DateTime();
            ChkDate = Convert.ToDateTime(DOE);
            string PatientScheduleOrderUID = "0";
            string SQL = "select UID,ScheduleOrderNumber from PatientScheduleOrder where PatientUID = '" + PatientUID + "' and ScheduledDttm = '" + ChkDate + "' and StatusFlag = 'A'";
            //exc = new ExcData();
            db = new SQL();
            DataTable dt = new DataTable();
            dt = db.FindDataTable(SQL);
            if (dt.Rows.Count > 0)
            {
                PatientScheduleOrderUID = dt.Rows[0]["UID"].ToString();
            }
            return PatientScheduleOrderUID;
        }
        private DataTable FindPackage(string PackageCode)
        {
            DataTable dt = new DataTable();
            string SQL = "select UID , PackageName from BillPackage where Code = '" + PackageCode + "' and StatusFlag = 'A'";
            //exc = new ExcData();
            db = new SQL();
            dt = db.FindDataTable(SQL);
            return dt;
        }
        private DataTable FindProchklistMapPackage(string ProChkList)
        {
            exc = new ExcData();
            //db = new SQL();
            string SQL = "SELECT [ProChkList] ,[PackageCode] FROM [tblProchklistMapPackage] WHERE [ProChkList] = '" + ProChkList + "' AND StatusFlag = 'A'";
            return exc.data_Table(SQL);
        }
        private DataTable BindProchklistMapPackage(DateTime DOE)
        {
            db = new SQL();
            DateTime ChkDate = new DateTime();
            ChkDate = Convert.ToDateTime(DOE);
            string SQL = "SELECT  UID, PatientScheduleOrderUID,ItemName,BillPackageUID,PatientUID FROM [BDMSPatientScheduleOrderDetail] where StartDttm = '" + ChkDate + "' and StatusFlag = 'A'";
            return db.FindDataTable(SQL);
        }
        private void btFind_Click(object sender, EventArgs e)
        {
            ExcData exc = new ExcData();
            DataTable dtPatient = new DataTable();
            string DateFrom = dtpDateFrom.Value.ToString("yyyy-MM-dd") + " " + dtpTimeFrom.Value.ToString("HH:mm");
            string DateTo = dtpDateTo.Value.ToString("yyyy-MM-dd") + " " + dtpTimeTo.Value.ToString("HH:mm");
            string SQL = "SELECT Forename as Name , Surname as LastName , DOE , ProChkList, NO FROM [tblPatientList] WHERE DOE BETWEEN '" + DateFrom + "' AND '" + DateTo + "'";
            dtPatient = exc.data_Table(SQL);
            gvPatient.DataSource = dtPatient;
            gvPatient.Columns["Check"].Width = 50;
            gvPatient.Refresh();
            lblCountPT.Text = dtPatient.Rows.Count.ToString();
            CheckAll();
        }
        private void CheckAll()
        {
            foreach (DataGridViewRow row in gvPatient.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Check"].Value) == false)
                {
                    row.Cells["Check"].Value = true;
                }
            }
        }
        private void UnCheckAll()
        {
            foreach (DataGridViewRow row in gvPatient.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Check"].Value) == true)
                {
                    row.Cells["Check"].Value = false;
                }
            }
        }
        private void AddingCheckBoxColumn()
        {
            DataGridViewCheckBoxColumn colChk = new DataGridViewCheckBoxColumn();
            colChk.Name = "Check";
            colChk.HeaderText = "เลือก";
            gvPatient.Columns.Add(colChk);
            //gvPatient.AutoSize = true;
            gvPatient.AllowUserToAddRows = false;
            gvPatient.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gvPatient.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            for (int i = 0; i < gvPatient.RowCount; i++)
            {
                gvPatient.Rows[i].Cells["colChk"].Value = false;
            }
        }
        private void chkBox_CheckedChanged(object sender, EventArgs e)
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
        private int CheckRepeat(string PatientUID, int PatientSchedulOrderUID, int PackageUID, DateTime DOE)
        {
            int UID = 0;
            string SQL = string.Empty;
            SQL = "select UID from BDMSPatientScheduleOrderDetail "
                + "where PatientUID = " + PatientUID + " and PatientScheduleOrderUID = " + PatientSchedulOrderUID + " and BillPackageUID = " + PackageUID + " and StartDttm = '" + DOE + "' and StatusFlag = 'A'";
            DataTable dt = new DataTable();
            db = new SQL();
            dt = db.FindDataTable(SQL);
            if (dt.Rows.Count > 0 && dt != null)
            {
                UID = Convert.ToInt32(dt.Rows[0]["UID"]);
            }
            return UID;
        }
    }
}
