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

namespace MassConvert
{
    public partial class frmConvertPayorByRegisterDate : Telerik.WinControls.UI.RadForm
    {
        #region GlobalVariable
        int countSuccess = 0;
        int countExist = 0;
        int countFail = 0;
        string payorChoose = "";
        string MassConvertLogHN = "";
        string MassConvertLogUID = "";
        string MassConvertLogEnable = System.Configuration.ConfigurationManager.AppSettings["MassConvertLogEnable"].Trim().ToLower();
        #endregion
        public frmConvertPayorByRegisterDate()
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

        private void btFind_Click(object sender, EventArgs e)
        {
            bwWaiting.RunWorkerAsync();
            //Find();
        }
        private void setPayor()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            #region Variable
            var strSQL = "SELECT DISTINCT Payor "+
                "FROM Patient "+
                "INNER JOIN tblCheckList ON Patient.rowguid=tblCheckList.PatientUID AND tblCheckList.WFID=1 "+
                "WHERE tblCheckList.RegDate BETWEEN '" + dtpDateFrom.Value.ToString("yyyy-MM-dd")+" "+dtpTimeFrom.Value.ToString("HH:mm") + "' AND '" + dtpDateTo.Value.ToString("yyyy-MM-dd")+" "+dtpTimeTo.Value.ToString("HH:mm") + "' ORDER BY Payor ASC;";
            var clsSQL = new clsSQLNative();
            var dt = new DataTable();
            #endregion
            #region Procedure
            if (ddlPayor.Items.Count > 0)
            {
                ddlPayor.Items.Clear();
            }
            dt = clsSQL.Bind(strSQL, clsSQLNative.DBType.SQLServer, "MobieConnect");
            if(dt!=null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddlPayor.Items.Add(dt.Rows[i]["Payor"].ToString().Trim());
                }
                ddlPayor.SelectedIndex = 0;
            }
            ddlPayor.Items.Insert(0, new RadListDataItem("- ทั้งหมด -", "NULL"));
            ddlPayor.SelectedIndex = 0;
            #endregion
        }
        //Custom Method
        private void CheckAll()
        {
            if (gvPatient.InvokeRequired)
            {
                gvPatient.Invoke(new MethodInvoker(delegate
                {
                    foreach (GridViewRowInfo row in gvPatient.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells["Check"].Value) == false)
                        {
                            if (row.IsVisible == true)
                            {
                                row.Cells["Check"].Value = true;
                            }
                        }
                    }
                }));
            }
            else
            {
                foreach (GridViewRowInfo row in gvPatient.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["Check"].Value) == false)
                    {
                        if (row.IsVisible == true)
                        {
                            row.Cells["Check"].Value = true;
                        }
                    }
                }
            }
        }
        private void UnCheckAll()
        {
            if (gvPatient.InvokeRequired)
            {
                gvPatient.Invoke(new MethodInvoker(delegate
                {
                    foreach (GridViewRowInfo row in gvPatient.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells["Check"].Value) == true)
                        {
                            row.Cells["Check"].Value = false;
                        }
                    }
                }));
            }
            else
            {
                foreach (GridViewRowInfo row in gvPatient.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["Check"].Value) == true)
                    {
                        row.Cells["Check"].Value = false;
                    }
                }
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
        private void CheckCareproviderUID(string login)
        {
            DataTable ldt = db.Select_Login_by_LoginName(login);
            if (ldt != null && ldt.Rows.Count > 0)
            {
                Cuser = ldt.Rows[0]["CareproviderUID"].ToString();
            }
        }
        private void BindPayor()
        {
            try
            {
                DataTable pdt = db.Select_InsuranceCompany();
                if (pdt != null && pdt.Rows.Count > 0)
                {
                    cboPayor.DataSource = pdt;
                    cboPayor.DisplayMember = "CompanyName";
                    cboPayor.ValueMember = "UID";
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        string GenerateSequenEachType(string ordercattype)
        {
            string seq = "nodata";
            if (ordercattype == "lab")
            {
                string sreq = db.SEQID("SEQRequest");
                sreq = OwnerOrganization + "0" + sreq.PadLeft(6, '0');
                return sreq;
            }
            if (ordercattype == "xray")
            {
                string sreq = db.SEQID("SEQRISRequestID");
                return sreq;

            }
            return seq;
        }
        private string CheckStatus(DataTable dt)
        {
            DataTable dtStatus = new DataTable();

            string status = string.Empty;
            int raised = 0;
            int reviewed = 0;
            int rejected = 0;
            int executed = 0;
            int cancelled = 0;
            int discontinue = 0;
            int collected = 0;
            int accepted = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (dt.Rows[i]["Status"].ToString())
                {
                    case "Reviewed":
                        reviewed += 1;
                        break;
                    case "Specimen Accepted":
                        accepted += 1;
                        break;
                    case "Specimen Collected":
                        collected += 1;
                        break;
                    case "Raised":
                        raised += 1;
                        break;
                    case "Specimen Rejected":
                        rejected += 1;
                        break;
                    case "Cancelled":
                        cancelled += 1;
                        break;
                    case "Discontinue":
                        discontinue += 1;
                        break;
                    case "Executed":
                        executed += 1;
                        break;
                }

            }

            if (reviewed == dt.Rows.Count)
            {
                dtStatus = db.GetStatusLab("Reviewed");
                status = dtStatus.Rows[0]["UID"].ToString() + "|" + dtStatus.Rows[0]["Description"].ToString();
            }
            else if (accepted == dt.Rows.Count)
            {
                dtStatus = db.GetStatusLab("Specimen Accepted");
                status = dtStatus.Rows[0]["UID"].ToString() + "|" + dtStatus.Rows[0]["Description"].ToString();
            }
            else if (collected == dt.Rows.Count)
            {
                dtStatus = db.GetStatusLab("Specimen Collected");
                status = dtStatus.Rows[0]["UID"].ToString() + "|" + dtStatus.Rows[0]["Description"].ToString();
            }
            else if (raised == dt.Rows.Count)
            {
                dtStatus = db.GetStatusLab("Raised");
                status = dtStatus.Rows[0]["UID"].ToString() + "|" + dtStatus.Rows[0]["Description"].ToString();
            }
            else if (cancelled == dt.Rows.Count)
            {
                dtStatus = db.GetStatusLab("Cancelled");
                status = dtStatus.Rows[0]["UID"].ToString() + "|" + dtStatus.Rows[0]["Description"].ToString();
            }
            else if (discontinue == dt.Rows.Count)
            {
                dtStatus = db.GetStatusLab("Discontinue");
                status = dtStatus.Rows[0]["UID"].ToString() + "|" + dtStatus.Rows[0]["Description"].ToString();
            }
            else if (rejected == dt.Rows.Count)
            {
                dtStatus = db.GetStatusLab("Specimen Rejected");
                status = dtStatus.Rows[0]["UID"].ToString() + "|" + dtStatus.Rows[0]["Description"].ToString();
            }
            else if (reviewed >= 1)
            {
                dtStatus = db.GetStatusLab("Partially Reviewed");
                status = dtStatus.Rows[0]["UID"].ToString() + "|" + dtStatus.Rows[0]["Description"].ToString();
            }
            else if (accepted >= 1)
            {
                dtStatus = db.GetStatusLab("Partially Completed");
                status = dtStatus.Rows[0]["UID"].ToString() + "|" + dtStatus.Rows[0]["Description"].ToString();
            }
            else if (collected >= 1)
            {
                dtStatus = db.GetStatusLab("Partially Collected");
                status = dtStatus.Rows[0]["UID"].ToString() + "|" + dtStatus.Rows[0]["Description"].ToString();

            }
            else if (cancelled >= 1)
            {
                dtStatus = db.GetStatusLab("Partially Cancelled");
                status = dtStatus.Rows[0]["UID"].ToString() + "|" + dtStatus.Rows[0]["Description"].ToString();

            }
            return status;
        }
        private void Update_Status_In_TblPatientList(string ScheduleOrderUID)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = db.Select_Patient_For_Update_TblPatientList(ScheduleOrderUID);
                db.Update_TblPatientList(dt.Rows[0]["Forename"].ToString().Trim(), dt.Rows[0]["Surname"].ToString().Trim(), dt.Rows[0]["ScheduledDttm"].ToString().Trim(), dt.Rows[0]["BirthDttm"].ToString().Trim());
                #region UpdateCheckListStatus by Oof
                UpdateCheckListStatus(dt.Rows[0]["PASID"].ToString().Trim(), dt.Rows[0]["ScheduledDttm"].ToString().Trim());
                #endregion
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        private void UpdateCheckListStatus(string HN, string DOE)
        {
            #region Variable
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            var strSQL = new StringBuilder();
            var dt = new DataTable();
            var clsSQL = new clsSQLNative();
            #endregion
            #region Procedure
            try
            {
                #region หาข้อมูลอ้างอิงกับเทเบิ้ล tblCheckList
                DOE = DateTime.Parse(DOE).ToString("yyyy-MM-dd HH:mm");
                #region SQLQuery
                strSQL.Append("SELECT TOP 1 ");
                strSQL.Append("rowguid PatientUID,Episode EN,ProChkList,Name,LastName ");
                strSQL.Append("FROM ");
                strSQL.Append("Patient ");
                strSQL.Append("WHERE ");
                strSQL.Append("HN='" + HN + "' ");
                strSQL.Append("AND DOE='" + DOE + "';");
                #endregion
                dt = clsSQL.Bind(strSQL.ToString(), clsSQLNative.DBType.SQLServer, "MobieConnect");
                if (dt == null || dt.Rows.Count == 0)
                {
                    return;
                }
                strSQL.Length = 0; strSQL.Capacity = 0;
                #endregion
                #region อัพเดทสถานะในเทเบิ้ล tblCheckList
                strSQL.Append("UPDATE tblCheckList SET ");
                strSQL.Append("ProStatus=2,ModifyDate=GETDATE() ");
                strSQL.Append("WHERE ");
                strSQL.Append("(PatientUID='" + dt.Rows[0]["PatientUID"].ToString().Trim() + "' OR EN='" + dt.Rows[0]["EN"].ToString().Trim() + "') ");
                strSQL.Append("AND ProStatus=1;");
                #endregion
                clsSQL.Execute(strSQL.ToString(), clsSQLNative.DBType.SQLServer, "MobieConnect");
            }
            catch (Exception) { }
            #endregion
        }
        private void ConvertPreOrder(string OrderNo)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            string careUID = "2283";
            var outMessage = "";
            var clsTempData = new clsTempData();
            var clsSQL = new clsSQLNative();

            //หาข้อมูลใน tb PatientScheduleOrder ของ OrderNumber ที่ถูกส่งมา
            DataTable Schdt = new DataTable();
            Schdt = db.Select_PatientScheduleOrder(OrderNo);
            if (Schdt != null && Schdt.Rows.Count > 0)
            {
                if (!clsTempData.setConvertResult(out outMessage,
                        "",
                        "",
                        "Select_PatientScheduleOrder",
                        "Success",
                        "OrderNo:"+OrderNo))
                {
                    MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                for (int x = 0; x < Schdt.Rows.Count; x++)
                {
                    MassConvertLogUID = "";
                    var outMessage2 = "";
                    var PatientVisitUID = "";
                    PatientVisitUID = Schdt.Rows[x]["PatientVisitUID"].ToString();
                    //เช็คว่ามีการ Convert แล้วหรือยัง
                    if (string.IsNullOrEmpty(PatientVisitUID) || PatientVisitUID == "0")
                    {
                        #region MassConvertLog
                        if (MassConvertLogEnable == "true")
                        {
                            MassConvertLogUID = clsSQL.Return("INSERT INTO MassConvertLog(HN,StartWhen,Username) OUTPUT INSERTED.UID VALUES('" + MassConvertLogHN + "',GETDATE(),'"+clsTempData.Username+"');",clsSQLNative.DBType.SQLServer, "MobieConnect");
                        }
                        #endregion
                        #region setConvertResult
                        if (!clsTempData.setConvertResult(out outMessage2,
                            "",
                            "",
                            "เช็คว่ามีการ Convert แล้วหรือยัง",
                            "No",
                            "PatientVisitUID:" + Schdt.Rows[x]["PatientVisitUID"].ToString()))
                        {
                            MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        #endregion
                        //Update Booking ของคนไข้คนนี้ให้ Status Flag = 'U' (แสดงว่าคนไข้ได้มาตามนัด หรือ ได้ตรวจแล้ว)
                        if (db.Update_Booking_Status(Schdt.Rows[x]["PatientUID"].ToString(), Schdt.Rows[x]["ScheduledDttm"].ToString(),out outMessage) == false)
                        {
                            #region setConvertResult
                            if (!clsTempData.setConvertResult(out outMessage2,
                                "",
                                "",
                                "Update Booking ของคนไข้คนนี้ให้ Status Flag = 'U' (แสดงว่าคนไข้ได้มาตามนัด หรือ ได้ตรวจแล้ว)",
                                "Fail",
                                ""))
                            {
                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            #endregion
                            MessageBox.Show("Cannot update booking status\n\n"+outMessage);
                        }
                        else
                        {
                            #region setConvertResult
                            if (!clsTempData.setConvertResult(out outMessage2,
                                "",
                                "",
                                "Update Booking ของคนไข้คนนี้ให้ Status Flag = 'U' (แสดงว่าคนไข้ได้มาตามนัด หรือ ได้ตรวจแล้ว)",
                                "Success",
                                ""))
                            {
                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            #endregion
                        }

                        //หา BillPackageItem ทั้งหมดของคนไข้คนนี้
                        DataTable SchDetdt = db.Select_OrderCategory_By_ScheduleOrderNumber_Filter_Package_AllItem(Schdt.Rows[x]["UID"].ToString());

                        //dgv.DataSource = SchDetdt;

                        if (SchDetdt != null && SchDetdt.Rows.Count > 0)
                        {
                            #region setConvertResult
                            if (!clsTempData.setConvertResult(out outMessage2,
                                "",
                                "",
                                "หา BillPackageItem ทั้งหมดของคนไข้คนนี้",
                                "Found",
                                "UID:"+ Schdt.Rows[x]["UID"].ToString()))
                            {
                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            #endregion
                            //หา BillableItem ของ Order ของคนไข้คนนี้ตาม BSMDD
                            DataTable bsmdt = db.Select_BillableItem_By_ScheduleOrderNumber_GroupBy_BSMDDUID(Schdt.Rows[x]["UID"].ToString()); //new check by BSMDDUID
                            //หา Order Category ของ Order ของคนไข้คนนี้
                            DataTable catdt = db.Select_OrderCategory_By_ScheduleOrderNumber_GroupBy_OrderCategoryUID(Schdt.Rows[x]["UID"].ToString()); //old


                            if (bsmdt != null && bsmdt.Rows.Count > 0)
                            {
                                #region setConvertResult
                                if (!clsTempData.setConvertResult(out outMessage2,
                                    "",
                                    "",
                                    "หา BillableItem ของ Order ของคนไข้คนนี้ตาม BSMDD",
                                    "Found",
                                    "UID:" + Schdt.Rows[x]["UID"].ToString()))
                                {
                                    MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                #endregion
                                //หาว่า Billable Type นี้มี Medicin , Supply อยู่ด้วยหรือไม่
                                //if (CheckBillableMedicineItemTypeInsidePackage(bsmdt) == true) //New concept check by BSMDDUID
                                //{
                                //MessageBox.Show("This version not support Medicine Item inside Package");
                                //return;
                                //}
                                //==================================================
                                //GENERATE PATIENT VISIT AND CREATE VISIT NO SECTION.
                                //==================================================
                                string visitno = db.SEQID("SEQVisitID");//StoreProcedure

                                if (Encounter_Desc.ToUpper().StartsWith("H"))
                                    visitno = "H" + BU + "-" + DateTime.Today.ToString("yy") + "-" + visitno.PadLeft(6, '0').ToString();
                                else
                                    visitno = "O" + BU + "-" + DateTime.Today.ToString("yy") + "-" + visitno.PadLeft(6, '0').ToString();

                                //Insert ข้อมูลเข้า table PatientVisit แล้ว return ค่า PatientVisitUID ออกมา
                                string patvisituid = db.Insert_PatientVisit(Schdt.Rows[x]["PatientUID"].ToString(), Encounter_UID
                                   , careUID, "0", Location_UID, "0", OwnerOrganization, "mass convert", Cuser
                                   , OwnerOrganization, "0");
                                #region setConvertResult
                                if (!clsTempData.setConvertResult(out outMessage2,
                                    "",
                                    "",
                                    "Insert ข้อมูลเข้า table PatientVisit แล้ว return ค่า PatientVisitUID ออกมา",
                                    "Success",
                                    "PatientVisitUID:" + patvisituid))
                                {
                                    MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                #endregion
                                //Update PatientVisitUID ใน table PatientScheduleOrder ให้เป็น PatientVisitUID จริง ๆ
                                if(db.Update_PatientScheduleOrder_By_UID(Schdt.Rows[x]["UID"].ToString(), patvisituid, Cuser))
                                {
                                    #region setConvertResult
                                    if (!clsTempData.setConvertResult(out outMessage2,
                                        "",
                                        "",
                                        "Update PatientVisitUID ใน table PatientScheduleOrder ให้เป็น PatientVisitUID จริง ๆ",
                                        "Success",
                                        "PatientVisitUID:" + patvisituid))
                                    {
                                        MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region setConvertResult
                                    if (!clsTempData.setConvertResult(out outMessage2,
                                        "",
                                        "",
                                        "Update PatientVisitUID ใน table PatientScheduleOrder ให้เป็น PatientVisitUID จริง ๆ",
                                        "Fail",
                                        "PatientVisitUID:" + patvisituid))
                                    {
                                        MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    #endregion
                                }
                                countSuccess += 1;

                                //Insert ข้อมูลเข้า table PatientVisitPayor แล้ว return ค่า PatientVisitPayorUID ออกมา
                                //string patientVisitPayorUID = db.Insert_PatientVisitPayor(patvisituid, Schdt.Rows[x]["PatientUID"].ToString()
                                //    , ((DataRowView)cboPayorOffice.SelectedItem).Row.ItemArray[0].ToString(), ((DataRowView)cboPolicy.SelectedItem).Row.ItemArray[0].ToString(),
                                //((DataRowView)cboPolicy.SelectedItem).Row.ItemArray[1].ToString(), "", "MassCovert", Cuser, "A", OwnerOrganization, ((DataRowView)cboPayor.SelectedItem).Row.ItemArray[0].ToString(),
                                //((DataRowView)cboAgreement.SelectedItem).Row.ItemArray[0].ToString(), "", "", ((DataRowView)cboAgreement.SelectedItem).Row.ItemArray[2].ToString(), "0");
                                string patientVisitPayorUID = db.Insert_PatientVisitPayor(patvisituid, Schdt.Rows[x]["PatientUID"].ToString()
                                    , cboPayorOffice.SelectedItem.Value.ToString(), cboPolicy.SelectedItem.Value.ToString(),
                                cboPolicy.SelectedItem.Text.ToString(), "", "MassCovert", Cuser, "A", OwnerOrganization, cboPayor.SelectedItem.Value.ToString(),
                                cboAgreement.SelectedItem.Value.ToString(), "", "", FindAgreementPercentTag(cboAgreement.SelectedItem.Value.ToString()), "0");
                                #region setConvertResult
                                if (!clsTempData.setConvertResult(out outMessage2,
                                    "",
                                    "",
                                    "Insert ข้อมูลเข้า table PatientVisitPayor แล้ว return ค่า PatientVisitPayorUID ออกมา",
                                    "Success",
                                    "PatientVisitPayorUID:" + patientVisitPayorUID))
                                {
                                    MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                #endregion
                                //Insert ข้อมูลเข้า table PatientVisiID
                                if(db.Insert_PatientVisitID(visitno, "Y", patvisituid, Cuser, OwnerOrganization))
                                {
                                    #region setConvertResult
                                    if (!clsTempData.setConvertResult(out outMessage2,
                                        "",
                                        "",
                                        "Insert ข้อมูลเข้า table PatientVisiID",
                                        "Success",
                                        "PatientVisitUID:" + patvisituid))
                                    {
                                        MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region setConvertResult
                                    if (!clsTempData.setConvertResult(out outMessage2,
                                        "",
                                        "",
                                        "Insert ข้อมูลเข้า table PatientVisiID",
                                        "Fail",
                                        "PatientVisitUID:" + patvisituid))
                                    {
                                        MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    #endregion
                                }
                                countSuccess += 1;

                                //==============================================
                                //INSERT PATIENT PACKAGE & PACKAGE ITEM SECTION. 
                                //==============================================
                                //หา Package ย่อย ของ ScheduleOrderNumber นี้
                                string packuid = string.Empty;
                                DataTable dtBDMSScheduleOrder = db.Select_BDMSPatientScheduleOrderDetail_By_ScheduleOrderUID(Schdt.Rows[x]["UID"].ToString());
                                if (dtBDMSScheduleOrder != null && dtBDMSScheduleOrder.Rows.Count > 0)
                                {
                                    #region setConvertResult
                                    if (!clsTempData.setConvertResult(out outMessage2,
                                        "",
                                        "",
                                        "หา Package ย่อย ของ ScheduleOrderNumber นี้",
                                        "Found",
                                        ""))
                                    {
                                        MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    #endregion
                                    for (int m = 0; m < dtBDMSScheduleOrder.Rows.Count; m++)
                                    {
                                        //Insert ข้อมูลเข้า table PatientPackage ตาม ScheduleOrderNumber ที่ส่งเข้ามา  แล้ว return ค่า PackageUID ออกมา
                                        packuid = db.Insert_PatientPackage_by_ScheduleOrderNumber(Schdt.Rows[x]["UID"].ToString(), OwnerOrganization, dtBDMSScheduleOrder.Rows[m]["BillPackageUID"].ToString(), patvisituid);
                                        #region setConvertResult
                                        if (!clsTempData.setConvertResult(out outMessage2,
                                            "",
                                            "",
                                            "Insert ข้อมูลเข้า table PatientPackage ตาม ScheduleOrderNumber ที่ส่งเข้ามา  แล้ว return ค่า PackageUID ออกมา",
                                            "Success",
                                            "PackageUID:"+ packuid))
                                        {
                                            MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                        #endregion
                                        //Insert ข้อมูลเข้า table PatientPackageItem
                                        if(db.Insert_PatientPackageItem_by_ScheduleOrderNumber_BillPackage("", packuid, OwnerOrganization))
                                        {
                                            #region setConvertResult
                                            if (!clsTempData.setConvertResult(out outMessage2,
                                                "",
                                                "",
                                                "Insert ข้อมูลเข้า table PatientPackageItem",
                                                "Success",
                                                "PackageUID:" + packuid))
                                            {
                                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            #region setConvertResult
                                            if (!clsTempData.setConvertResult(out outMessage2,
                                                "",
                                                "",
                                                "Insert ข้อมูลเข้า table PatientPackageItem",
                                                "Fail",
                                                "PackageUID:" + packuid))
                                            {
                                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                            #endregion
                                        }
                                        countSuccess += 1;
                                    }
                                }
                                else
                                {
                                    #region setConvertResult
                                    if (!clsTempData.setConvertResult(out outMessage2,
                                        "",
                                        "",
                                        "หา Package ย่อย ของ ScheduleOrderNumber นี้",
                                        "Not Found",
                                        ""))
                                    {
                                        MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    #endregion
                                }
                                //วนลูป add หัว package ทั้ง program หลัก และ Option เข้า table PatientBillableItem
                                DataTable dtPktBdmsSch = db.Select_PatientPackage_By_VisitUID(patvisituid);
                                if (dtPktBdmsSch != null && dtPktBdmsSch.Rows.Count > 0)
                                {
                                    #region setConvertResult
                                    if (!clsTempData.setConvertResult(out outMessage2,
                                        "",
                                        "",
                                        "วนลูป add หัว package ทั้ง program หลัก และ Option เข้า table PatientBillableItem",
                                        "Found",
                                        "PatientVisitUID:" + patvisituid))
                                    {
                                        MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    #endregion
                                    for (int n = 0; n < dtPktBdmsSch.Rows.Count; n++)
                                    {
                                        //INSERT PATIENT BILLABLE FOR BILL PACKAGE IDENTIFIER TYPE

                                        //Insert ข้อมูลเข้า table PatientBillableItem 
                                        if(db.Insert_PatientBillableItem_For_PackageHeader(OrderNo, dtPktBdmsSch.Rows[n]["UID"].ToString(), OwnerOrganization, Cuser, patvisituid, Schdt.Rows[x]["PatientUID"].ToString(), dtPktBdmsSch.Rows[n]["BillPackageUID"].ToString()))
                                        {
                                            #region setConvertResult
                                            if (!clsTempData.setConvertResult(out outMessage2,
                                                "",
                                                "",
                                                "Insert ข้อมูลเข้า table PatientBillableItem",
                                                "Success",
                                                ""))
                                            {
                                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            #region setConvertResult
                                            if (!clsTempData.setConvertResult(out outMessage2,
                                                "",
                                                "",
                                                "Insert ข้อมูลเข้า table PatientBillableItem",
                                                "Fail",
                                                ""))
                                            {
                                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                            #endregion
                                        }
                                        countSuccess += 1;
                                    }
                                }
                                else
                                {
                                    #region setConvertResult
                                    if (!clsTempData.setConvertResult(out outMessage2,
                                        "",
                                        "",
                                        "วนลูป add หัว package ทั้ง program หลัก และ Option เข้า table PatientBillableItem",
                                        "Not Found",
                                        "PatientVisitUID:" + patvisituid))
                                    {
                                        MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    #endregion
                                }
                                //วนลูปตาม Order Category
                                for (int i = 0; i < catdt.Rows.Count; i++)
                                {
                                    string SEQ = db.SEQID("SEQpatientorder");
                                    string SpecialSEQ = GenerateSequenEachType(catdt.Rows[i]["Description"].ToString().ToLower());
                                    string OrderCateType = catdt.Rows[i]["Description"].ToString().ToLower();
                                    string Identype = string.Empty;
                                    //ถ้าเป็น Order ทั่วไป
                                    if (SpecialSEQ == "nodata")
                                        Identype = "PATIENTORDER";
                                    else //ถ้าเป็น Order Lab ,Xray
                                        Identype = "REQUEST";

                                    //วนลูปทีละ BillPackageItem
                                    for (int j = 0; j < SchDetdt.Rows.Count; j++)
                                    {
                                        //เช็คว่าเป็น OrderCategory เดียวกันหรือไม่
                                        if (catdt.Rows[i]["Value"].ToString().ToLower() == SchDetdt.Rows[j]["Value"].ToString()) //Check OrderCategory must match for assign each item to specific table
                                        {
                                            string ordcat = "NULL";
                                            string ordtsubcat = "NULL";
                                            string ordtoval = "0"; //Order To Location Value
                                            //เซ๊ตค่า OrderCat
                                            if (SchDetdt.Rows[j]["ORDCTUID"].ToString() != "")
                                                ordcat = SchDetdt.Rows[j]["ORDCTUID"].ToString();

                                            //เซ็ตค่า OrderSubCat
                                            if (SchDetdt.Rows[j]["OrderSubCategoryUID"].ToString() != "")
                                                ordtsubcat = SchDetdt.Rows[j]["OrderSubCategoryUID"].ToString();

                                            //เซ็ตค่า Order To Location
                                            DataTable ordto = db.Select_OrderToLocationUID(Location_UID,
                                                 SchDetdt.Rows[j]["BillableItemUID"].ToString(), "", true);
                                            if (ordto != null && ordto.Rows.Count > 0)
                                                ordtoval = (ordto.Rows[0][0].ToString() == "" ? "0" : ordto.Rows[0][0].ToString());


                                            string patorderuid = string.Empty;
                                            //Insert ข้อมูลเข้า table PatientOrder แล้ว return PatientOrderUID ออกมา
                                            bool flaginsertPatientOrder = db.insert_PatientOrder(SEQ, Schdt.Rows[x]["PatientUID"].ToString()
                                               , patvisituid, Cuser, "mass convert"
                                               , SchDetdt.Rows[j]["ORDCTUID"].ToString()
                                               , Identype, Location_UID, ordtoval, OwnerOrganization, out patorderuid);
                                            if (flaginsertPatientOrder)
                                            {
                                                #region setConvertResult
                                                if (!clsTempData.setConvertResult(out outMessage2,
                                                    "",
                                                    "",
                                                    "Insert ข้อมูลเข้า table PatientOrder แล้ว return PatientOrderUID ออกมา",
                                                    "Success",
                                                    "PatientOrderUID:"+ patorderuid))
                                                {
                                                    MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                                #endregion
                                            }
                                            else
                                            {
                                                #region setConvertResult
                                                if (!clsTempData.setConvertResult(out outMessage2,
                                                    "",
                                                    "",
                                                    "Insert ข้อมูลเข้า table PatientOrder แล้ว return PatientOrderUID ออกมา",
                                                    "Fail",
                                                    "PatientOrderUID:" + patorderuid))
                                                {
                                                    MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                                #endregion
                                            }
                                            countSuccess += 1;

                                            //หา PatientPackageUID ของ Order และ Visit นี้
                                            DataTable dtPTPck = db.Select_PatientPackageUID_By_BillPackageItemUID(patvisituid, SchDetdt.Rows[j]["UID"].ToString());

                                            //ถ้า Insert ข้อมูลเข้า table PatientOrder ได้
                                            if (flaginsertPatientOrder)
                                            {
                                                //ถ้าเป็น Order ทั่วไปให้ set status = Raised
                                                string orddetstatus = "Raised";
                                                //ถ้าเป็น Order Lab set status = Specimen Collected
                                                if (OrderCateType.ToString().ToLower() == "lab")
                                                    orddetstatus = "Specimen Collected";
                                                //ถ้าเป็น Order Xray set status = Executed
                                                else if (OrderCateType.ToString().ToLower() == "xray")
                                                    orddetstatus = "Executed";

                                                // กลับมาเช็คต่อ หาในส่วนของ PatientOrderDetail ต่อใน Package
                                                //INSERT DATA IN PATIENT ORDER DETAIL

                                                //IdentifyingType
                                                //-------------------------------------------------------------------------------------------------
                                                //MEDICATION , REQUESTDETAIL ,SERVICECHARGE, BILLABLEEVENT, ORDERITEM, DRUGCATALOGITEM, REQUESTITEM
                                                //-------------------------------------------------------------------------------------------------

                                                string IdentTypeforOrderDetail = "";
                                                if (Identype == "PATIENTORDER")
                                                    IdentTypeforOrderDetail = "ORDERITEM";
                                                if (Identype == "REQUEST")
                                                    IdentTypeforOrderDetail = "REQUESTDETAIL";

                                                //Calculate NetAmout
                                                string NetAmount = "0";

                                                try
                                                {
                                                    int quantity = int.Parse(SchDetdt.Rows[j]["Quantity"].ToString());
                                                    int amount = int.Parse(SchDetdt.Rows[j]["Amount"].ToString());
                                                    NetAmount = (quantity * amount).ToString();
                                                }
                                                catch (Exception)
                                                {

                                                    NetAmount = "0";
                                                }

                                                string patorddetails = "0";


                                                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                                string requid = string.Empty;
                                                if (Identype == "REQUEST")
                                                {
                                                    //1827 SPECIMEN COLLECTED
                                                    //ถ้า Order Category เป็น Lab
                                                    if (OrderCateType.ToString().ToLower() == "lab")
                                                    {
                                                        //เช็คว่ามีการ Generate LabNo โดยโปรแกรม Pre GenLab แล้วหรือยัง
                                                        DataTable mssdt = db.Select_BDMSASTMassConvert_Between(OrderNo, OrderNo);
                                                        if (mssdt != null && mssdt.Rows.Count > 0)
                                                        {
                                                            #region setConvertResult
                                                            if (!clsTempData.setConvertResult(out outMessage2,
                                                                "",
                                                                "",
                                                                "เช็คว่ามีการ Generate LabNo โดยโปรแกรม Pre GenLab แล้วหรือยัง",
                                                                "Yes",
                                                                "LabNo:"+ SpecialSEQ))
                                                            {
                                                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }
                                                            #endregion
                                                            //ถ้ามีแล้วให้ดึง LabNo ที่ถูก Generate แล้วมา  เพื่อที่จะได้ไม่ต้อง Genrate LabNo ใหม่
                                                            SpecialSEQ = mssdt.Rows[0]["LabNo"].ToString();
                                                        }
                                                        else
                                                        {
                                                            #region setConvertResult
                                                            if (!clsTempData.setConvertResult(out outMessage2,
                                                                "",
                                                                "",
                                                                "เช็คว่ามีการ Generate LabNo โดยโปรแกรม Pre GenLab แล้วหรือยัง",
                                                                "No",
                                                                "LabNo:" + SpecialSEQ))
                                                            {
                                                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }
                                                            #endregion
                                                        }
                                                        //นำ Lab No ที่ Generate มาแล้วไปเช็คใน table request ว่ามีแล้วหรือยัง
                                                        DataTable labnodt = db.Select_Request_By_LabNo(SpecialSEQ);
                                                        if (labnodt != null && labnodt.Rows.Count > 0) //found lab no in database
                                                        {
                                                            //ถ้ามีแล้วให้คืนค่า RequestUID กลับมา
                                                            requid = labnodt.Rows[0]["UID"].ToString();
                                                            #region setConvertResult
                                                            if (!clsTempData.setConvertResult(out outMessage2,
                                                                "",
                                                                "",
                                                                "นำ Lab No ที่ Generate มาแล้วไปเช็คใน table request ว่ามีแล้วหรือยัง",
                                                                "Yes",
                                                                "RequestUID:" + requid))
                                                            {
                                                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }
                                                            #endregion
                                                        }
                                                        else //not found lab no in database
                                                        {
                                                            //ถ้ายังไม่มี ให้ทำการ Insert ข้อมูลใหม่เข้าไปใน table request แล้ว return RequestUID ออกมา
                                                            requid = db.Insert_Request(patvisituid,Schdt.Rows[x]["PatientUID"].ToString(),Cuser,SpecialSEQ,"Specimen Collected"
                                                                        ,Location_UID, ordtoval, OwnerOrganization);
                                                            #region setConvertResult
                                                            if (!clsTempData.setConvertResult(out outMessage2,
                                                                "",
                                                                "",
                                                                "นำ Lab No ที่ Generate มาแล้วไปเช็คใน table request ว่ามีแล้วหรือยัง",
                                                                "No",
                                                                "RequestUID:" + requid+ " (ทำการ Insert ข้อมูลใหม่เข้าไปใน table request แล้ว return RequestUID ออกมา)"))
                                                            {
                                                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }
                                                            #endregion
                                                        }
                                                        // Update table PatientOrder 
                                                        db.Update_PatientOrder_By_UID(patorderuid, requid);
                                                        #region setConvertResult
                                                        if (!clsTempData.setConvertResult(out outMessage2,
                                                            "",
                                                            "",
                                                            "Update table PatientOrder",
                                                            "Success",
                                                            "PatientOrderUID:" + patorderuid))
                                                        {
                                                            MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        }
                                                        #endregion
                                                        //Insert ข้อมูลเข้า table RequestDetail แล้ว return UID กลับมา
                                                        string retreqdet = db.Insert_RequestDetail(SchDetdt.Rows[j]["ItemUID"].ToString(),
                                                                    requid, Cuser, SchDetdt.Rows[j]["Comments"].ToString(), "Specimen Collected", OwnerOrganization);
                                                        //ถ้า Insert สำเร็จ
                                                        if (retreqdet != "")
                                                        {
                                                            #region setConvertResult
                                                            if (!clsTempData.setConvertResult(out outMessage2,
                                                                "",
                                                                "",
                                                                "Insert ข้อมูลเข้า table RequestDetail แล้ว return UID กลับมา",
                                                                "Success",
                                                                "RequestDetailUID:" + retreqdet))
                                                            {
                                                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }
                                                            #endregion
                                                            //หาข้อมูลว่ามีการ PreOrder แล้วหรือยัง
                                                            DataTable pschdt = db.Select_PatientScheduleOrder(OrderNo);
                                                            if (pschdt != null && pschdt.Rows.Count > 0)
                                                            {
                                                                #region setConvertResult
                                                                if (!clsTempData.setConvertResult(out outMessage2,
                                                                    "",
                                                                    "",
                                                                    "หาข้อมูลว่ามีการ PreOrder แล้วหรือยัง",
                                                                    "Yes",
                                                                    "OrderNo:" + OrderNo))
                                                                {
                                                                    MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                }
                                                                #endregion
                                                                //เช็ค Specimen ที่เราได้ทำตอน Generate LabNo
                                                                DataTable bspctsdt = db.Select_BDMSASTMAssSpecimenTestset_BY_PatientScheduleOrderUID_And_TestSet_HN(
                                                                              pschdt.Rows[0]["UID"].ToString(), SchDetdt.Rows[j]["RequestCode"].ToString()
                                                                              , Schdt.Rows[x]["HN"].ToString());
                                                                //ถ้ามี Specimen ตอน Generate LabNo แล้ว
                                                                if (bspctsdt != null && bspctsdt.Rows.Count > 0)
                                                                {
                                                                    #region setConvertResult
                                                                    if (!clsTempData.setConvertResult(out outMessage2,
                                                                        "",
                                                                        "",
                                                                        "เช็ค Specimen ที่เราได้ทำตอน Generate LabNo",
                                                                        "Found",
                                                                        ""))
                                                                    {
                                                                        MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                    }
                                                                    #endregion
                                                                    //เข้าไปดึง specimen ใน table master specimen โดยดึงตาม Code ที่กำหนด
                                                                    DataTable spdt = db.Select_Specimen_By_Name(bspctsdt.Rows[0]["Specimen"].ToString());
                                                                    if (spdt != null && spdt.Rows.Count > 0)
                                                                    {
                                                                        //Insert ข้อมูล Specimen เข้า table RequestDetailSpecimen
                                                                        if(db.Insert_RequestDetailSpecimen(retreqdet, "NULL", "'Manual Entry'"
                                                                                , spdt.Rows[0]["UID"].ToString(), spdt.Rows[0]["Name"].ToString(), "0", "0"
                                                                                , "3", "0", "NULL", "0", "0", Cuser, "'Mass Convert'", Cuser
                                                                                , OwnerOrganization, "NULL", "NULL", "Specimen Collected", "''", "NULL", "NULL"))
                                                                        {
                                                                            #region setConvertResult
                                                                            if (!clsTempData.setConvertResult(out outMessage2,
                                                                                "",
                                                                                "",
                                                                                "Insert ข้อมูล Specimen เข้า table RequestDetailSpecimen",
                                                                                "Success",
                                                                                ""))
                                                                            {
                                                                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                            }
                                                                            #endregion
                                                                        }
                                                                        else
                                                                        {
                                                                            #region setConvertResult
                                                                            if (!clsTempData.setConvertResult(out outMessage2,
                                                                                "",
                                                                                "",
                                                                                "Insert ข้อมูล Specimen เข้า table RequestDetailSpecimen",
                                                                                "Fail",
                                                                                ""))
                                                                            {
                                                                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                            }
                                                                            #endregion
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    #region setConvertResult
                                                                    if (!clsTempData.setConvertResult(out outMessage2,
                                                                        "",
                                                                        "",
                                                                        "เช็ค Specimen ที่เราได้ทำตอน Generate LabNo",
                                                                        "Not Found",
                                                                        ""))
                                                                    {
                                                                        MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                    }
                                                                    #endregion
                                                                    //RequestItemUID = SchDetdt.Rows[j]["ItemUID"].ToString()
                                                                    //LabCode = SchDetdt.Rows[j]["RequestCode"].ToString()

                                                                    //ใช้ RequestItemUID ไปหาว่าใช้ Specimen code อะไร
                                                                    string speceminitem = string.Empty;
                                                                    DataTable spedt = db.Select_RequestItemSpecimen_By_RequestItemUID(SchDetdt.Rows[j]["ItemUID"].ToString(), "Y");
                                                                    if (spedt != null && spedt.Rows.Count > 0)
                                                                    {
                                                                        speceminitem = spedt.Rows[0]["SpecimenCode"].ToString();
                                                                    }
                                                                    else
                                                                    {
                                                                        spedt = db.Select_RequestItemSpecimen_By_RequestItemUID(SchDetdt.Rows[j]["ItemUID"].ToString(), "N");
                                                                        if (spedt != null && spedt.Rows.Count > 0)
                                                                            speceminitem = spedt.Rows[0]["SpecimenCode"].ToString();

                                                                    }
                                                                    //เข้าไปดึง specimen ใน table master specimen โดยดึงตาม Code ที่กำหนด
                                                                    DataTable spMasterdt = db.Select_Specimen_By_Name(spedt.Rows[0]["SpecimenCode"].ToString());
                                                                    if (spMasterdt != null && spMasterdt.Rows.Count > 0)
                                                                    {
                                                                        #region setConvertResult
                                                                        if (!clsTempData.setConvertResult(out outMessage2,
                                                                            "",
                                                                            "",
                                                                            "เข้าไปดึง specimen ใน table master specimen โดยดึงตาม Code ที่กำหนด",
                                                                            "Found",
                                                                            ""))
                                                                        {
                                                                            MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                        }
                                                                        #endregion
                                                                        //Insert ข้อมูล Specimen เข้า table RequestDetailSpecimen
                                                                        if(db.Insert_RequestDetailSpecimen(retreqdet, "NULL", "'Manual Entry'"
                                                                                , spMasterdt.Rows[0]["UID"].ToString(), spMasterdt.Rows[0]["Name"].ToString(), "0", "0"
                                                                                , "3", "0", "NULL", "0", "0", Cuser, "'Mass Convert'", Cuser
                                                                                , OwnerOrganization, "NULL", "NULL", "Specimen Collected", "''", "NULL", "NULL"))
                                                                        {
                                                                            #region setConvertResult
                                                                            if (!clsTempData.setConvertResult(out outMessage2,
                                                                                "",
                                                                                "",
                                                                                "Insert ข้อมูล Specimen เข้า table RequestDetailSpecimen",
                                                                                "Success",
                                                                                ""))
                                                                            {
                                                                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                            }
                                                                            #endregion
                                                                        }
                                                                        else
                                                                        {
                                                                            #region setConvertResult
                                                                            if (!clsTempData.setConvertResult(out outMessage2,
                                                                                "",
                                                                                "",
                                                                                "Insert ข้อมูล Specimen เข้า table RequestDetailSpecimen",
                                                                                "Fail",
                                                                                ""))
                                                                            {
                                                                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                            }
                                                                            #endregion
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        #region setConvertResult
                                                                        if (!clsTempData.setConvertResult(out outMessage2,
                                                                            "",
                                                                            "",
                                                                            "เข้าไปดึง specimen ใน table master specimen โดยดึงตาม Code ที่กำหนด",
                                                                            "Not Found",
                                                                            ""))
                                                                        {
                                                                            MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                        }
                                                                        #endregion
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                #region setConvertResult
                                                                if (!clsTempData.setConvertResult(out outMessage2,
                                                                    "",
                                                                    "",
                                                                    "หาข้อมูลว่ามีการ PreOrder แล้วหรือยัง",
                                                                    "No",
                                                                    "OrderNo:" + OrderNo))
                                                                {
                                                                    MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                }
                                                                #endregion
                                                            }
                                                            patorddetails = db.insert_PatientOrderDetail(SEQ, retreqdet
                                                            , IdentTypeforOrderDetail, Cuser, SchDetdt.Rows[j]["BillableItemUID"].ToString(), "mass convert"
                                                            , orddetstatus, SchDetdt.Rows[j]["Quantity"].ToString()
                                                            , SchDetdt.Rows[j]["OrderSubCategoryUID"].ToString()
                                                            , SchDetdt.Rows[j]["ORDCTUID"].ToString()
                                                            , SchDetdt.Rows[j]["Amount"].ToString()
                                                            , (NetAmount == "0" ? SchDetdt.Rows[j]["Amount"].ToString() : NetAmount)
                                                            , SchDetdt.Rows[j]["Amount"].ToString()
                                                            , dtPTPck.Rows[0]["PatientPackageUID"].ToString(), OwnerOrganization, patientVisitPayorUID);
                                                            #region setConvertResult
                                                            if (!clsTempData.setConvertResult(out outMessage2,
                                                                "",
                                                                "",
                                                                "insert_PatientOrderDetail",
                                                                "Success",
                                                                "PatientOrderDetailUID:"+ patorddetails))
                                                            {
                                                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }
                                                            #endregion
                                                            //Packuid
                                                        } //if (retreqdt !="")
                                                        else
                                                        {
                                                            #region setConvertResult
                                                            if (!clsTempData.setConvertResult(out outMessage2,
                                                                "",
                                                                "",
                                                                "Insert ข้อมูลเข้า table RequestDetail แล้ว return UID กลับมา",
                                                                "Fail",
                                                                ""))
                                                            {
                                                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }
                                                            #endregion
                                                        }

                                                        DataTable lsdt = db.GetRequestDetail_Status(SpecialSEQ);
                                                        if (lsdt != null && lsdt.Rows.Count > 0)
                                                        {
                                                            #region setConvertResult
                                                            if (!clsTempData.setConvertResult(out outMessage2,
                                                                "",
                                                                "",
                                                                "GetRequestDetail_Status",
                                                                "Found",
                                                                "SpecialSEQ:" + SpecialSEQ))
                                                            {
                                                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }
                                                            #endregion
                                                            var cstat = CheckStatus(lsdt).Split('|');
                                                            if (cstat.Count() == 2)
                                                            {
                                                                db.Update_Request_By_LabNumber(SpecialSEQ, cstat[0].ToString());
                                                                #region setConvertResult
                                                                if (!clsTempData.setConvertResult(out outMessage2,
                                                                    "",
                                                                    "",
                                                                    "Update_Request_By_LabNumber",
                                                                    "Success",
                                                                    ""))
                                                                {
                                                                    MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                }
                                                                #endregion
                                                            }
                                                            else
                                                            {
                                                                #region setConvertResult
                                                                if (!clsTempData.setConvertResult(out outMessage2,
                                                                    "",
                                                                    "",
                                                                    "Update_Request_By_LabNumber",
                                                                    "Fail",
                                                                    ""))
                                                                {
                                                                    MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                }
                                                                #endregion
                                                            }
                                                        }// if (lsdt != null && lsdt.Rows.Count > 0)
                                                        else
                                                        {
                                                            #region setConvertResult
                                                            if (!clsTempData.setConvertResult(out outMessage2,
                                                                "",
                                                                "",
                                                                "GetRequestDetail_Status",
                                                                "Not Found",
                                                                "SpecialSEQ:" + SpecialSEQ))
                                                            {
                                                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }
                                                            #endregion
                                                        }



                                                    }//if (OrderCateType.ToString().ToLower()=="lab")
                                                    else if (OrderCateType.ToString().ToLower() == "xray")
                                                    {
                                                        //requid = db.Insert_Request(patvisituid
                                                        //  , Schdt.Rows[x]["PatientUID"].ToString(), Cuser, SpecialSEQ, "Executed"
                                                        //  , cbLocationOrder.SelectedValue.ToString(), ordtoval, OwnerOrganization);

                                                        requid = db.Insert_Request(patvisituid
                                                          , Schdt.Rows[x]["PatientUID"].ToString(), Cuser, SpecialSEQ, "Executed"
                                                          , Location_UID, ordtoval, OwnerOrganization);
                                                        #region setConvertResult
                                                        if (!clsTempData.setConvertResult(out outMessage2,
                                                            "",
                                                            "",
                                                            "Insert_Request",
                                                            "Success",
                                                            "requid:"+ requid))
                                                        {
                                                            MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        }
                                                        #endregion

                                                        db.Update_PatientOrder_By_UID(patorderuid, requid);
                                                        #region setConvertResult
                                                        if (!clsTempData.setConvertResult(out outMessage2,
                                                            "",
                                                            "",
                                                            "Update_PatientOrder_By_UID",
                                                            "Success",
                                                            "PatientOrderUID:" + patorderuid))
                                                        {
                                                            MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        }
                                                        #endregion

                                                        string retreqdet = db.Insert_RequestDetail(SchDetdt.Rows[j]["itemUID"].ToString(),
                                                               requid, Cuser, SchDetdt.Rows[j]["Comments"].ToString(), "Executed", OwnerOrganization);
                                                        if (retreqdet != "")
                                                        {
                                                            #region setConvertResult
                                                            if (!clsTempData.setConvertResult(out outMessage2,
                                                                "",
                                                                "",
                                                                "Insert_RequestDetail",
                                                                "Success",
                                                                "retreqdet:" + retreqdet))
                                                            {
                                                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }
                                                            #endregion
                                                            patorddetails = db.insert_PatientOrderDetail(SEQ, retreqdet
                                                               , IdentTypeforOrderDetail, Cuser, SchDetdt.Rows[j]["BillableItemUID"].ToString(), "mass convert"
                                                               , orddetstatus, SchDetdt.Rows[j]["Quantity"].ToString()
                                                               , SchDetdt.Rows[j]["OrderSubCategoryUID"].ToString()
                                                               , SchDetdt.Rows[j]["ORDCTUID"].ToString()
                                                               , SchDetdt.Rows[j]["Amount"].ToString()
                                                               , (NetAmount == "0" ? SchDetdt.Rows[j]["Amount"].ToString() : NetAmount)
                                                               , SchDetdt.Rows[j]["Amount"].ToString()
                                                               , dtPTPck.Rows[0]["PatientPackageUID"].ToString(), OwnerOrganization, patientVisitPayorUID);
                                                            #region setConvertResult
                                                            if (!clsTempData.setConvertResult(out outMessage2,
                                                                "",
                                                                "",
                                                                "insert_PatientOrderDetail",
                                                                "Success",
                                                                "patorddetails:" + patorddetails))
                                                            {
                                                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }
                                                            #endregion
                                                            //packuid

                                                        }//if (retreqdet != "")
                                                        else
                                                        {
                                                            #region setConvertResult
                                                            if (!clsTempData.setConvertResult(out outMessage2,
                                                                "",
                                                                "",
                                                                "Insert_RequestDetail",
                                                                "Fail",
                                                                ""))
                                                            {
                                                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }
                                                            #endregion
                                                        }
                                                    }//if (OrderCateType.ToString().ToLower()=="xray")
                                                }//if (Identype=="REQUEST")
                                                else// for Identype = "ORDER"
                                                {
                                                    patorddetails = db.insert_PatientOrderDetail(SEQ, SchDetdt.Rows[j]["ItemUID"].ToString()
                                                        , IdentTypeforOrderDetail, Cuser, SchDetdt.Rows[j]["BillableItemUID"].ToString(), "mass convert"
                                                        , orddetstatus, SchDetdt.Rows[j]["Quantity"].ToString()
                                                        , SchDetdt.Rows[j]["OrderSubCategoryUID"].ToString()
                                                        , SchDetdt.Rows[j]["ORDCTUID"].ToString()
                                                        , SchDetdt.Rows[j]["Amount"].ToString()
                                                        , (NetAmount == "0" ? SchDetdt.Rows[j]["Amount"].ToString() : NetAmount)
                                                        , SchDetdt.Rows[j]["Amount"].ToString()
                                                        , dtPTPck.Rows[0]["PatientPackageUID"].ToString(), OwnerOrganization, patientVisitPayorUID);
                                                    #region setConvertResult
                                                    if (!clsTempData.setConvertResult(out outMessage2,
                                                        "",
                                                        "",
                                                        "insert_PatientOrderDetail",
                                                        "Success",
                                                        "patorddetails:" + patorddetails))
                                                    {
                                                        MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    }
                                                    #endregion
                                                    //packuid
                                                }//=================================================================================

                                                //**********************************************************
                                                //INSERT PATIENTBILLABLEITEM SECTION 
                                                //IdentifyingUID = SchDetdt.Rows[j]["ItemUID"].ToString()


                                                //**********************************************************

                                                //IdentifyingType = REQUESTITEM , OrderType = REQUEST
                                                //IdentifyingType = ORDITEM , OrderType = PATIENTORDER

                                                if (Identype == "REQUEST")//ถ้าเป็น Order Lab , Xray
                                                {
                                                    if (OrderCateType.ToString().ToLower() == "lab")
                                                    {
                                                        //error
                                                        if(db.Insert_BillableItem(Schdt.Rows[x]["PatientUID"].ToString()
                                                        , patvisituid, SchDetdt.Rows[j]["ItemUID"].ToString(), "'REQUESTITEM'", Cuser, OwnerOrganization
                                                        , SchDetdt.Rows[j]["BSMDDUID"].ToString(), SchDetdt.Rows[j]["Amount"].ToString()
                                                        , (NetAmount == "0" ? SchDetdt.Rows[j]["Amount"].ToString() : NetAmount), SchDetdt.Rows[j]["BillableItemUID"].ToString()
                                                        , SchDetdt.Rows[j]["Quantity"].ToString(), "null", "null", SchDetdt.Rows[j]["Comments"].ToString()
                                                        , "null", "null", dtPTPck.Rows[0]["PatientPackageUID"].ToString(), "'REQUEST'", requid, patorddetails, "Specimen Collected"))
                                                        {
                                                            #region setConvertResult
                                                            if (!clsTempData.setConvertResult(out outMessage2,
                                                                "",
                                                                "",
                                                                "Insert_BillableItem",
                                                                "Success",
                                                                ""))
                                                            {
                                                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }
                                                            #endregion
                                                        }
                                                        else
                                                        {
                                                            #region setConvertResult
                                                            if (!clsTempData.setConvertResult(out outMessage2,
                                                                "",
                                                                "",
                                                                "Insert_BillableItem",
                                                                "Fail",
                                                                ""))
                                                            {
                                                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }
                                                            #endregion
                                                        }
                                                        //packuid
                                                        countSuccess += 1;
                                                    }
                                                    else//(OrderCateType.ToString().ToLower() == "xray")
                                                    {
                                                        if(db.Insert_BillableItem(Schdt.Rows[x]["PatientUID"].ToString()
                                                        , patvisituid, SchDetdt.Rows[j]["ItemUID"].ToString(), "'REQUESTITEM'", Cuser, OwnerOrganization
                                                        , SchDetdt.Rows[j]["BSMDDUID"].ToString(), SchDetdt.Rows[j]["Amount"].ToString()
                                                        , (NetAmount == "0" ? SchDetdt.Rows[j]["Amount"].ToString() : NetAmount), SchDetdt.Rows[j]["BillableItemUID"].ToString()
                                                        , SchDetdt.Rows[j]["Quantity"].ToString(), "null", "null", SchDetdt.Rows[j]["Comments"].ToString()
                                                        , "null", "null", dtPTPck.Rows[0]["PatientPackageUID"].ToString(), "'REQUEST'", requid, patorddetails, "Executed"))
                                                        {
                                                            #region setConvertResult
                                                            if (!clsTempData.setConvertResult(out outMessage2,
                                                                "",
                                                                "",
                                                                "Insert_BillableItem",
                                                                "Success",
                                                                ""))
                                                            {
                                                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }
                                                            #endregion
                                                        }
                                                        else
                                                        {
                                                            #region setConvertResult
                                                            if (!clsTempData.setConvertResult(out outMessage2,
                                                                "",
                                                                "",
                                                                "Insert_BillableItem",
                                                                "Fail",
                                                                ""))
                                                            {
                                                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }
                                                            #endregion
                                                        }
                                                        //packuid
                                                        countSuccess += 1;
                                                    }
                                                }
                                                else //ถ้าเป็น Order อื่น ๆ
                                                {

                                                    if(db.Insert_BillableItem(Schdt.Rows[x]["PatientUID"].ToString()
                                                        , patvisituid, SchDetdt.Rows[j]["ItemUID"].ToString(), "'ORDITEM'", Cuser, OwnerOrganization
                                                        , SchDetdt.Rows[j]["BSMDDUID"].ToString(), SchDetdt.Rows[j]["Amount"].ToString()
                                                        , (NetAmount == "0" ? SchDetdt.Rows[j]["Amount"].ToString() : NetAmount), SchDetdt.Rows[j]["BillableItemUID"].ToString()
                                                        , SchDetdt.Rows[j]["Quantity"].ToString(), "null", "null", SchDetdt.Rows[j]["Comments"].ToString()
                                                        , "null", "null", dtPTPck.Rows[0]["PatientPackageUID"].ToString(), "'PATIENTORDER'", patorderuid, patorddetails, "Raised"))
                                                    {
                                                        #region setConvertResult
                                                        if (!clsTempData.setConvertResult(out outMessage2,
                                                            "",
                                                            "",
                                                            "Insert_BillableItem",
                                                            "Success",
                                                            ""))
                                                        {
                                                            MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        }
                                                        #endregion
                                                    }
                                                    else
                                                    {
                                                        #region setConvertResult
                                                        if (!clsTempData.setConvertResult(out outMessage2,
                                                            "",
                                                            "",
                                                            "Insert_BillableItem",
                                                            "Fail",
                                                            ""))
                                                        {
                                                            MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        }
                                                        #endregion
                                                    }
                                                    //packuid
                                                    countSuccess += 1;


                                                    //db.Insert_PatientBillableItem(SchDetdt.Rows[j]["UID"].ToString()
                                                    //                                        , packuid, patorddetails, "ORDITEM", patorderuid, "Raised", OwnerOrganization);

                                                }
                                            }//if (flaginsertPatientOrder)
                                        }//if (catdt.Rows[i]["Value"].ToString().ToLower() == SchDetdt.Rows[j][""].ToString())

                                    }//for (int j = 0; j < SchDetdt.Rows.Count; j++)
                                }//for (int i = 0; i < catdt.Rows.Count; i++)  ลูปตาม OrderCat

                                //=================== ส่ง EN ไป update ใน table Patient ของโปรแกรม Contact Checkup
                                string strPayor = db.Select_Payor(patvisituid);
                                db.Update_EN_IN_ContactCheckup(Schdt.Rows[x]["ScheduleOrderNumber"].ToString(), visitno, strPayor);
                                #region setConvertResult
                                if (!clsTempData.setConvertResult(out outMessage2,
                                    "",
                                    "",
                                    "ส่ง EN ไป update ใน table Patient ของโปรแกรม Contact Checkup",
                                    "Success",
                                    "ScheduleOrderNumber:"+ Schdt.Rows[x]["ScheduleOrderNumber"].ToString() + " EN:"+ visitno))
                                {
                                    MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                #endregion

                                //==================== Update ใน table tblPatientList ว่าคนไข้คนนี้ได้ทำการ Convert แล้ว
                                Update_Status_In_TblPatientList(Schdt.Rows[x]["ScheduleOrderNumber"].ToString());
                                #region setConvertResult
                                if (!clsTempData.setConvertResult(out outMessage2,
                                    "",
                                    "",
                                    "Update ใน table tblPatientList ว่าคนไข้คนนี้ได้ทำการ Convert แล้ว",
                                    "Success",
                                    "ScheduleOrderNumber:" + Schdt.Rows[x]["ScheduleOrderNumber"].ToString()))
                                {
                                    MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                #endregion

                                //========= Insert table PatientServiceEvent เพื่อให้สามารถค้นหาหน้า Patient List ได้
                                db.Insert_PatientServiceEvent(patvisituid);
                                #region setConvertResult
                                if (!clsTempData.setConvertResult(out outMessage2,
                                    "",
                                    "",
                                    "Insert table PatientServiceEvent เพื่อให้สามารถค้นหาหน้า Patient List ได้",
                                    "Success",
                                    "PatientVisitUID:" + patvisituid))
                                {
                                    MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                #endregion
                            }
                            else
                            {
                                #region setConvertResult
                                if (!clsTempData.setConvertResult(out outMessage2,
                                    "",
                                    "",
                                    "หา BillableItem ของ Order ของคนไข้คนนี้ตาม BSMDD",
                                    "Not Found",
                                    "UID:" + Schdt.Rows[x]["UID"].ToString()))
                                {
                                    MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            #region setConvertResult
                            if (!clsTempData.setConvertResult(out outMessage2,
                                "",
                                "",
                                "หา BillPackageItem ทั้งหมดของคนไข้คนนี้",
                                "Not Found",
                                "UID:" + Schdt.Rows[x]["UID"].ToString()))
                            {
                                MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        countExist += 1;
                        #region setConvertResult
                        if (!clsTempData.setConvertResult(out outMessage2,
                            "",
                            "",
                            "เช็คว่ามีการ Convert แล้วหรือยัง",
                            "Yes",
                            "PatientVisitUID:" + Schdt.Rows[x]["PatientVisitUID"].ToString()))
                        {
                            MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage2, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        #endregion
                    }
                    #region MassConvertLog
                    if (MassConvertLogEnable == "true")
                    {
                        if (MassConvertLogUID != "")
                        {
                            clsSQL.Execute("UPDATE MassConvertLog SET EndWhen=GETDATE() WHERE UID="+MassConvertLogUID+";", clsSQLNative.DBType.SQLServer, "MobieConnect");
                        }
                    }
                    #endregion
                }//
            }//if (Schdt != null && Schdt.Rows.Count > 0)
            else
            {
                if (!clsTempData.setConvertResult(out outMessage,
                        "",
                        "",
                        "Select_PatientScheduleOrder",
                        "Not found",
                        "OrderNo:" + OrderNo))
                {
                    MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                countFail += 1;
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
            bs.Filter = string.Format("ChildCompany LIKE '%{0}%'", txtFilterPayor.Text.Trim());
            CheckAll();
        }
        private void frmConvertPayor_Load(object sender, EventArgs e)
        {
            setLabel(lblProgressDetail, "");
            db = new SQL();
            dtpDateFrom.Value = DateTime.Today;
            dtpDateTo.Value = DateTime.Today;
            dtpTimeFrom.Value = Convert.ToDateTime("00:00:00");
            dtpTimeTo.Value = Convert.ToDateTime("23:59:59");
            setPayor();
            CheckCareproviderUID(Login_UID);
            AddingCheckBoxColumn();
            ddlIsConverted.SelectedIndex = 0;
            //BindPayor();
        }
        private void cboPayor_Click(object sender, EventArgs e)
        {
            BindPayor();
        }
        private void btConvert_Click(object sender, EventArgs e)
        {
            #region DropDownListChecker
            if (cboPayor.Text.Trim().Length == 0 || cboPayor.SelectedItems.Count == 0)
            {
                cboPayor.Focus();
                MessageBox.Show(
                    "โปรดเลือกข้อมูล Payor ก่อน Convert",
                    "MassConvert : Warn",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (cboAgreement.Text.Trim().Length == 0 || cboAgreement.SelectedItems.Count == 0)
            {
                cboAgreement.Focus();
                MessageBox.Show(
                    "โปรดเลือกข้อมูล Agreement ก่อน Convert",
                    "MassConvert : Warn",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (cboPayorOffice.Text.Trim().Length == 0 || cboPayorOffice.SelectedItems.Count == 0)
            {
                cboPayorOffice.Focus();
                MessageBox.Show(
                    "โปรดเลือกข้อมูล PayorOffice ก่อน Convert",
                    "MassConvert : Warn",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (cboPolicy.Text.Trim().Length == 0 || cboPolicy.SelectedItems.Count == 0)
            {
                cboPolicy.Focus();
                MessageBox.Show(
                    "โปรดเลือกข้อมูล Policy ก่อน Convert",
                    "MassConvert : Warn",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            #endregion
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("โปรแกรมยังทำงานค้างไม่เสร็จ โปรดรอสักครู่", "Please wait.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private string FindAgreementPercentTag(string AgreementUID)
        {
            string PercentTag = "100";
            try
            {
                DataTable dtPer = db.Select_Agreement_ClaimPercentage(cboAgreement.SelectedItem.Value.ToString());
                PercentTag = dtPer.Rows[0]["ClaimPercentage"].ToString();
                return PercentTag;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return PercentTag;
            }
        }
        private void cboPayor_SelectedIndexChanged_1(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                if (cboPayor.SelectedIndex > 0)
                {
                    string PayorUID = cboPayor.SelectedItem.Value.ToString();
                    if (!string.IsNullOrEmpty(PayorUID) || PayorUID != "0")
                    {
                        DataTable adt = db.Select_Agreement(PayorUID);
                        //DataTable adt = db.Select_Agreement(((DataRowView)cboPayor.SelectedItems).Row.ItemArray[0].ToString());
                        cboAgreement.DataSource = adt;
                        cboAgreement.DisplayMember = "Name";
                        cboAgreement.ValueMember = "UID";
                        //cboAgreement.ValueMember = "ClaimPercentage";

                        DataTable pyfdt = db.Select_PayorDetail(PayorUID);
                        cboPayorOffice.DataSource = pyfdt;
                        cboPayorOffice.DisplayMember = "PayorName";
                        cboPayorOffice.ValueMember = "UID";

                        DataTable pofdt = db.Select_Policy(PayorUID);
                        cboPolicy.DataSource = pofdt;
                        cboPolicy.DisplayMember = "PolicyName";
                        cboPolicy.ValueMember = "UID";

                        btConvert.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        private void cboAgreement_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (cboPayor.SelectedIndex > 0 && cboAgreement.SelectedIndex > 0)
            {
                string PayorUID = cboPayor.SelectedItem.Value.ToString();
                if (!string.IsNullOrEmpty(PayorUID) || PayorUID != "0")
                {
                    string AgreementUID = cboAgreement.SelectedItem.Value.ToString();
                    if (!string.IsNullOrEmpty(AgreementUID) || AgreementUID != "0")
                    {
                        DataTable pyfdt = db.Select_PayorDetail(PayorUID, AgreementUID);
                        cboPayorOffice.DataSource = pyfdt;
                        cboPayorOffice.DisplayMember = "PayorName";
                        cboPayorOffice.ValueMember = "UID";

                        DataTable pofdt = db.Select_Policy(PayorUID, AgreementUID);
                        cboPolicy.DataSource = pofdt;
                        cboPolicy.DisplayMember = "PolicyName";
                        cboPolicy.ValueMember = "UID";
                    }
                }
            }
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            #region Variable
            var countLoop = 0;
            var SQLPT = new StringBuilder();
            var clsTempData = new clsTempData();
            var outMessage = "";
            countSuccess = 0;
            countExist = 0;
            countFail = 0;
            #endregion
            setRadButton(btConvert, false);
            setRadButton(btCancel, true);
            setPictureBox(anWaiting, true);
            #region FindMaxLoop
            int CountCheckBox = 0;
            for (int r = 0; r <= gvPatient.Rows.Count - 1; r++)
            {
                //Loop ทำงานเฉพาะคนที่ Check Box
                if (Convert.ToBoolean(gvPatient.Rows[r].Cells["Check"].Value) == true)
                {
                    CountCheckBox++;
                }
            }
            #endregion
            setProgressBar(progressBar1, CountCheckBox, 0);
            clsTempData.dtConvertResult = null;
            for (int i = 0; i <= gvPatient.Rows.Count - 1; i++)
            {
                if (backgroundWorker1.CancellationPending)
                {
                    setProgressBar(progressBar1, CountCheckBox, 0);
                    e.Cancel = true;
                    setRadButton(btCancel, false);
                    MessageBox.Show("ขั้นตอนถูกยกเลิกโดย User"+Environment.NewLine+
                        "ดำเนินการไปแล้วทั้งสิ้น "+countLoop.ToString()+" จากทั้งหมด "+CountCheckBox.ToString()+" รายการ", "Cancel by User.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                //Loop ทำงานเฉพาะคนที่ Check Box
                if (Convert.ToBoolean(gvPatient.Rows[i].Cells["Check"].Value) == true)
                {
                    MassConvertLogHN = gvPatient.Rows[i].Cells["HN"].Value.ToString().Trim();
                    var forename = gvPatient.Rows[i].Cells["Name"].Value.ToString().Trim();
                    var surname = gvPatient.Rows[i].Cells["LastName"].Value.ToString().Trim();
                    var doe = gvPatient.Rows[i].Cells["DOE"].Value;
                    countLoop += 1;
                    setLabel(lblStatus, string.Format("Converting order of {0} {1}", gvPatient.Rows[i].Cells["Name"].Value.ToString().Trim(), gvPatient.Rows[i].Cells["LastName"].Value.ToString().Trim()));
                    #region setConvertResult
                    if (!clsTempData.setConvertResult(out outMessage,
                        gvPatient.Rows[i].Cells["HN"].Value.ToString(),
                        gvPatient.Rows[i].Cells["Name"].Value.ToString().Trim() + " " + gvPatient.Rows[i].Cells["LastName"].Value.ToString().Trim(),
                        "Start",
                        "",
                        ""))
                    {
                        MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    #endregion
                    #region SQLQuery
                    SQLPT.Append("SELECT * FROM [PatientScheduleOrder] ps");
                    SQLPT.Append(" inner join Patient p on p.UID = ps.PatientUID ");
                    SQLPT.Append(" and p.Forename = N'" + forename + "'");
                    SQLPT.Append(" and Surname = N'" + surname + "'");
                    SQLPT.Append(" and ps.ScheduledDttm between '" + Convert.ToDateTime(doe).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(doe).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                    SQLPT.Append(" and ps.StatusFlag = 'A'");
                    #endregion
                    DataTable dt = new DataTable();
                    dt = db.Select_OrderNo(SQLPT.ToString());
                    SQLPT.Length = 0;SQLPT.Capacity = 0;
                    if (dt.Rows.Count > 0)
                    {
                        #region setConvertResult
                        if (!clsTempData.setConvertResult(out outMessage,
                            "",
                            "",
                            "Select_OrderNo",
                            "Success",
                            ""))
                        {
                            MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        #endregion
                        ConvertPreOrder(dt.Rows[0]["ScheduleOrderNumber"].ToString());
                    }
                    else
                    {
                        #region setConvertResult
                        if (!clsTempData.setConvertResult(out outMessage,
                            "",
                            "",
                            "Select_OrderNo",
                            "No Row",
                            ""))
                        {
                            MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างหน้าสรุปการ Convert" + Environment.NewLine + outMessage, "setConvertResult", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        #endregion
                    }
                    //System.Threading.Thread.Sleep(5000);
                }
                setProgressBar(progressBar1, CountCheckBox, countLoop);
                setLabel(lblProgressDetail, string.Format("{0}/{1}", countLoop, CountCheckBox));
                //setLabel(lblStatus, string.Format("Converting order ",""));
            }
            setLabel(lblProgressDetail, "");
            setPictureBox(anWaiting, false);
            setRadButton(btConvert, true);
            setRadButton(btCancel, false);
            setLabel(lblStatus, string.Format("Waiting", ""));
            setProgressBar(progressBar1, CountCheckBox, 0);

            DialogResult dr= MessageBox.Show(string.Format("Convert order successful {0} from {1}"+Environment.NewLine+"(Success : {2} Exist : {3} Fail : {4}"+Environment.NewLine+Environment.NewLine+"ต้องการดูรายละเอียดการ Convert คลิก Yes",
                countLoop.ToString(), CountCheckBox.ToString(),
                countSuccess.ToString(),
                countExist.ToString(),
                countFail.ToString()),
                "Success",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                ConvertResult cr = new ConvertResult();
                cr.ShowDialog();
            }
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //MessageBox.Show("Completed.");
        }
        private void setProgressBar(ProgressBar control,int maxValue,int value)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new MethodInvoker(delegate
                {
                    control.Maximum = maxValue;
                    control.Value = value;
                }));
            }
            else
            {
                control.Maximum = maxValue;
                control.Value = value;
            }
        }
        private void setLabel(Label control, string text)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new MethodInvoker(delegate
                {
                    control.Text = text;
                }));
            }
            else
            {
                control.Text = text;
            }
        }
        private void setComboBox(ComboBox control, int index)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new MethodInvoker(delegate
                {
                    control.SelectedIndex = index;
                }));
            }
            else
            {
                control.SelectedIndex = index;
            }
        }
        private void setDataGridView(RadGridView control, DataTable dt)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new MethodInvoker(delegate
                {
                    control.DataSource = dt;
                }));
            }
            else
            {
                control.DataSource = dt;
            }
        }
        private void setRadButton(RadButton control, bool enable)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new MethodInvoker(delegate
                {
                    control.Enabled = enable;
                }));
            }
            else
            {
                control.Enabled = enable;
            }
        }
        private void setPictureBox(PictureBox control, bool visible)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new MethodInvoker(delegate
                {
                    control.Visible = visible;
                }));
            }
            else
            {
                control.Visible = visible;
            }
        }
        private void gvPatient_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            if (e.RowElement.RowInfo.Cells["STS"].Value.ToString().Trim() == "R")
            {
                e.RowElement.DrawFill = true;
                e.RowElement.GradientStyle = GradientStyles.Solid;
                e.RowElement.BackColor = ColorTranslator.FromHtml("#FFD544");
            }
            else
            {
                e.RowElement.DrawFill = true;
                e.RowElement.GradientStyle = GradientStyles.Solid;
                e.RowElement.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            }
            if (e.RowElement.RowInfo.Cells["IsConvertPreOrder"].Value.ToString().Trim() == "1")
            {
                e.RowElement.ForeColor= ColorTranslator.FromHtml("#22B14C");
            }
            else
            {
                e.RowElement.ForeColor = ColorTranslator.FromHtml("#000000");
            }
        }
        private void btCancel_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }
        private void ddlIsConverted_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsConvertFilter();
        }
        private void IsConvertFilter()
        {
            UnCheckAll();
            var value = "";
            if (ddlIsConverted.InvokeRequired)
            {
                ddlIsConverted.Invoke(new MethodInvoker(delegate
                {
                    value = ddlIsConverted.Text;
                }));
            }
            else
            {
                value = ddlIsConverted.Text;
            }
            var count = 0;
            switch (value)
            {
                case "- ทั้งหมด -":
                    if (gvPatient.InvokeRequired)
                    {
                        gvPatient.Invoke(new MethodInvoker(delegate
                        {
                            for (int i = 0; i < gvPatient.Rows.Count; i++)
                            {
                                gvPatient.Rows[i].IsVisible = true;
                                count += 1;
                            }
                        }));
                    }
                    else
                    {
                        for (int i = 0; i < gvPatient.Rows.Count; i++)
                        {
                            gvPatient.Rows[i].IsVisible = true;
                            count += 1;
                        }
                    }
                    break;
                case "เฉพาะที่ยังไม่ Convert":
                    if (gvPatient.InvokeRequired)
                    {
                        gvPatient.Invoke(new MethodInvoker(delegate
                        {
                            for (int i = 0; i < gvPatient.Rows.Count; i++)
                            {
                                if (gvPatient.Rows[i].Cells["IsConvertPreOrder"].Value.ToString().Trim() == "1")
                                {
                                    gvPatient.Rows[i].IsVisible = false;
                                }
                                else
                                {
                                    gvPatient.Rows[i].IsVisible = true;
                                    count += 1;
                                }
                            }
                        }));
                    }
                    else
                    {
                        for (int i = 0; i < gvPatient.Rows.Count; i++)
                        {
                            if (gvPatient.Rows[i].Cells["IsConvertPreOrder"].Value.ToString().Trim() == "1")
                            {
                                gvPatient.Rows[i].IsVisible = false;
                            }
                            else
                            {
                                gvPatient.Rows[i].IsVisible = true;
                                count += 1;
                            }
                        }
                    }
                    break;
                case "เฉพาะที่ Convert แล้ว":
                    if (gvPatient.InvokeRequired)
                    {
                        gvPatient.Invoke(new MethodInvoker(delegate
                        {
                            for (int i = 0; i < gvPatient.Rows.Count; i++)
                            {
                                if (gvPatient.Rows[i].Cells["IsConvertPreOrder"].Value.ToString().Trim() == "1")
                                {
                                    gvPatient.Rows[i].IsVisible = true;
                                    count += 1;
                                }
                                else
                                {
                                    gvPatient.Rows[i].IsVisible = false;
                                }
                            }
                        }));
                    }
                    else
                    {
                        for (int i = 0; i < gvPatient.Rows.Count; i++)
                        {
                            if (gvPatient.Rows[i].Cells["IsConvertPreOrder"].Value.ToString().Trim() == "1")
                            {
                                gvPatient.Rows[i].IsVisible = true;
                                count += 1;
                            }
                            else
                            {
                                gvPatient.Rows[i].IsVisible = false;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
            setLabel(lblIsConvertCount, string.Format("พบข้อมูลตรงเงื่อนไข {0} รายการ", count.ToString()));
            if (chkBox.InvokeRequired)
            {
                chkBox.Invoke(new MethodInvoker(delegate
                {
                    if (chkBox.Checked)
                    {
                        CheckAll();
                    }
                }));
            }
            else
            {
                if (chkBox.Checked)
                {
                    CheckAll();
                }

            }
        }
        private void dtpDateFrom_ValueChanged(object sender, EventArgs e)
        {
            setPayor();
        }
        private void dtpTimeFrom_ValueChanged(object sender, EventArgs e)
        {
            setPayor();
        }
        private void dtpDateTo_ValueChanged(object sender, EventArgs e)
        {
            setPayor();
        }
        private void dtpTimeTo_ValueChanged(object sender, EventArgs e)
        {
            setPayor();
        }
        private void dtpDateFrom_ValueChanged_1(object sender, EventArgs e)
        {
            setPayor();
        }
        private void dtpDateTo_ValueChanged_1(object sender, EventArgs e)
        {
            setPayor();
        }
        /// <summary>
        /// Search Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwWaiting_DoWork(object sender, DoWorkEventArgs e)
        {
            Find();
        }
        private void Find()
        {
            //if (ddlIsConverted.InvokeRequired)
            //{
            //    ddlIsConverted.Invoke(new MethodInvoker(delegate
            //    {
            //        ddlIsConverted.SelectedIndex = 0;
            //    }));
            //}
            //else
            //{
            //    ddlIsConverted.SelectedIndex = 0;
            //}
            setRadButton(btFind, false);
            setPictureBox(pbCountPT, true);
            setLabel(lblCountPT, "");
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            ExcData exc = new ExcData();
            if (gvPatient.InvokeRequired)
            {
                gvPatient.Invoke(new MethodInvoker(delegate
                {
                    if (gvPatient.Rows.Count > 0)
                    {
                        gvPatient.DataSource = null;
                        gvPatient.Rows.Clear();
                    }
                }));
            }
            else
            {
                if (gvPatient.Rows.Count > 0)
                {
                    gvPatient.DataSource = null;
                    gvPatient.Rows.Clear();
                }
            }
            string DateFrom = dtpDateFrom.Value.ToString("yyyy-MM-dd") + " " + dtpTimeFrom.Value.ToString("HH:mm");
            string DateTo = dtpDateTo.Value.ToString("yyyy-MM-dd") + " " + dtpTimeTo.Value.ToString("HH:mm");
            string SQL = string.Empty;
            var strSQL = new StringBuilder();
            var clsSQL = new clsSQLNative();
            var clsTempData = new clsTempData();
            var payor = "";

            #region SQLQuery
            strSQL.Append("SELECT ");
            strSQL.Append("P.NO,");
            strSQL.Append("P.HN,");
            strSQL.Append("LTRIM(REPLACE(P.Name, P.PreName, ''))Name,");
            strSQL.Append("P.LastName,");
            strSQL.Append("P.DOE,");
            strSQL.Append("P.Payor,");
            strSQL.Append("PL.ChildCompany,");
            strSQL.Append("P.StatusOnMobile STS,");
            strSQL.Append("CL.RegDate RegisterDate,");
            strSQL.Append("P.SyncWhen,'0' IsConvertPreOrder ");
            strSQL.Append("FROM ");
            strSQL.Append("Patient P ");
            strSQL.Append("INNER JOIN tblCheckList CL ON P.rowguid = CL.PatientUID AND WFID = 1 ");
            strSQL.Append("LEFT JOIN tblPatientList PL ON P.rowguid = PL.PatientUID ");
            strSQL.Append("WHERE ");
            strSQL.Append("(CL.RegDate BETWEEN '" + DateFrom + "' AND '" + DateTo + "') ");
            if (rbAll.Checked)
            {
                //SQL += "AND StatusOnMobile in ('A','R') ";
            }
            else if (rbNotRegister.Checked)
            {
                strSQL.Append("AND StatusOnMobile IS NULL ");
            }
            else if (rbRegister.Checked)
            {
                strSQL.Append("AND StatusOnMobile='R' ");
            }
            if (ddlPayor.InvokeRequired)
            {
                ddlPayor.Invoke(new MethodInvoker(delegate
                {
                    if (ddlPayor.SelectedItem.ToString() != "- ทั้งหมด -")
                    {
                        payor = ddlPayor.SelectedItem.ToString();
                        payorChoose = payor;
                        strSQL.Append("AND Payor='" + payor + "' ");
                    }
                }));
            }
            else
            {
                if (ddlPayor.SelectedItem.ToString() != "- ทั้งหมด -")
                {
                    payor = ddlPayor.SelectedItem.ToString();
                    payorChoose = payor;
                    strSQL.Append("AND Payor='" + payor + "' ");
                }
            }
            strSQL.Append("ORDER BY ");
            //strSQL.Append("CL.RegDate ASC;");
            strSQL.Append("DOE DESC;");
            #endregion
            dtPatient = clsSQL.Bind(strSQL.ToString(), clsSQLNative.DBType.SQLServer, "MobieConnect");
            strSQL.Length = 0; strSQL.Capacity = 0;
            clsTempData.dtIsConverted = null;
            if (dtPatient != null && dtPatient.Rows.Count > 0)
            {
                #region Find Min&Max DOE
                var col = dtPatient.Columns["DOE"];
                var minDOE = dtPatient.AsEnumerable()
                    .Select(cols => cols.Field<DateTime>(col.ColumnName))
                    .OrderBy(p => p.Ticks)
                    .FirstOrDefault();
                var maxDOE = dtPatient.AsEnumerable()
                    .Select(cols => cols.Field<DateTime>(col.ColumnName))
                    .OrderByDescending(p => p.Ticks)
                    .FirstOrDefault();
                #endregion
                try
                {
                    //setComboBox(ddlIsConverted, 0);
                }
                catch (Exception) { }
                #region Check IsConvertPreOrder
                for (int i = 0; i < dtPatient.Rows.Count; i++)
                {
                    if (clsTempData.IsConverted(
                        dtPatient.Rows[i]["Name"].ToString(),
                        dtPatient.Rows[i]["LastName"].ToString(),
                        dtPatient.Rows[i]["DOE"].ToString(),
                        minDOE.ToString("yyyy-MM-dd HH:mm"), maxDOE.ToString("yyyy-MM-dd HH:mm")))
                    {
                        dtPatient.Rows[i]["IsConvertPreOrder"] = "1";
                    }
                }
                dtPatient.AcceptChanges();
                #endregion
                #region SearchIsConvertFilter
                /*
                if (ddlIsConverted.InvokeRequired)
                {
                    ddlIsConverted.Invoke(new MethodInvoker(delegate
                    {
                        if (ddlIsConverted.SelectedIndex > 0)
                        {
                            DataView dv = dtPatient.DefaultView;
                            if (ddlIsConverted.SelectedItem.ToString() == "เฉพาะที่ยังไม่ Convert")
                            {
                                dv.RowFilter = "IsConvertPreOrder=0";

                            }
                            else if(ddlIsConverted.SelectedItem.ToString() == "เฉพาะที่ Convert แล้ว")
                            {
                                dv.RowFilter = "IsConvertPreOrder=1";
                            }
                            dtPatient = dv.ToTable();
                        }
                    }));
                }
                */
                #endregion
                if (gvPatient.InvokeRequired)
                {
                    gvPatient.Invoke(new MethodInvoker(delegate
                    {
                        bs.DataSource = dtPatient;
                        gvPatient.DataSource = bs;
                        gvPatient.Columns["No"].Width = 20;
                        gvPatient.Columns["HN"].Width = 100;
                        gvPatient.Columns["Name"].Width = 100;
                        gvPatient.Columns["LastName"].Width = 100;
                        gvPatient.Columns["DOE"].Width = 130;
                        gvPatient.Columns["NO"].Width = 40;
                        gvPatient.Columns["Payor"].Width = 170;
                        gvPatient.Columns["ChildCompany"].Width = 170;
                        gvPatient.Columns["STS"].Width = 40;
                        gvPatient.Columns["RegisterDate"].Width = 130;
                        gvPatient.Columns["SyncWhen"].Width = 130;
                        gvPatient.Columns["IsConvertPreOrder"].IsVisible = false;
                        gvPatient.Refresh();
                    }));
                }
                else
                {
                    bs.DataSource = dtPatient;
                    gvPatient.DataSource = bs;
                    gvPatient.Columns["No"].Width = 20;
                    gvPatient.Columns["HN"].Width = 100;
                    gvPatient.Columns["Name"].Width = 100;
                    gvPatient.Columns["LastName"].Width = 100;
                    gvPatient.Columns["DOE"].Width = 130;
                    gvPatient.Columns["NO"].Width = 40;
                    gvPatient.Columns["Payor"].Width = 170;
                    gvPatient.Columns["ChildCompany"].Width = 170;
                    gvPatient.Columns["STS"].Width = 40;
                    gvPatient.Columns["RegisterDate"].Width = 130;
                    gvPatient.Columns["SyncWhen"].Width = 130;
                    gvPatient.Columns["IsConvertPreOrder"].IsVisible = false;
                    gvPatient.Refresh();
                }
                setLabel(lblCountPT, dtPatient.Rows.Count.ToString() + " Record.");
                setLabel(lblIsConvertCount, string.Format("พบข้อมูลตรงเงื่อนไข {0} รายการ", dtPatient.Rows.Count.ToString()));
                CheckAll();
                IsConvertFilter();
            }
            else
            {
                MessageBox.Show("ไม่พบข้อมูลตามเงื่อนไขที่ต้องการ", "No data.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                setLabel(lblCountPT, "0 Record.");
                bs.DataSource = null;
                if (gvPatient.InvokeRequired)
                {
                    gvPatient.Invoke(new MethodInvoker(delegate
                    {
                        gvPatient.DataSource = bs;
                        gvPatient.Refresh();
                    }));
                }
                else
                {
                    gvPatient.DataSource = bs;
                    gvPatient.Refresh();
                }
            }
            setPictureBox(pbCountPT, false);
            setRadButton(btFind, true);
        }
    }
}