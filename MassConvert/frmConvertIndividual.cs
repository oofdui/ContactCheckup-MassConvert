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
    public partial class frmConvertIndividual : Telerik.WinControls.UI.RadForm
    {
        #region GlobalVariable
        string MassConvertLogPatientUID = "";
        string MassConvertLogHN = "";
        string MassConvertLogUID = "";
        string MassConvertLogEnable = System.Configuration.ConfigurationManager.AppSettings["MassConvertLogEnable"].Trim().ToLower();
        #endregion
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
        #endregion Attribute
        public frmConvertIndividual()
        {
            InitializeComponent();
        }

        private void frmConvertIndividual_Load(object sender, EventArgs e)
        {
            db = new SQL();
            dtpDateFrom.Value = DateTime.Today;
            dtpDateTo.Value = DateTime.Today;
            dtpTimeFrom.Value = Convert.ToDateTime("06:00:00");
            dtpTimeTo.Value = Convert.ToDateTime("06:00:00");
            CheckCareproviderUID(Login_UID);
        }
        private void CheckCareproviderUID(string login)
        {
            DataTable ldt = db.Select_Login_by_LoginName(login);
            if (ldt != null && ldt.Rows.Count > 0)
            {
                Cuser = ldt.Rows[0]["CareproviderUID"].ToString();
            }
        }
        private void cboPayor_Click(object sender, EventArgs e)
        {
            BindPayor();
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
            string DateFrom = dtpDateFrom.Value.ToString("yyyy-MM-dd") + " " + dtpTimeFrom.Value.ToString("HH:mm");
            string DateTo = dtpDateTo.Value.ToString("yyyy-MM-dd") + " " + dtpTimeTo.Value.ToString("HH:mm");

            progressBar1.Maximum = 100;
            progressBar1.Step = 1;
            progressBar1.Value1 = 0;

            StringBuilder SQLPT = new StringBuilder();
            SQLPT.Append("SELECT * FROM [PatientScheduleOrder] ps");
            SQLPT.Append(" inner join Patient p on p.UID = ps.PatientUID ");
            SQLPT.Append(" and p.PasID = '" + txtHN.Text.Trim() + "'");
            SQLPT.Append(" and ps.ScheduledDttm between '" + DateFrom + "' and '" + DateTo + "'");
            SQLPT.Append(" and ps.StatusFlag = 'A'");
            DataTable dt = new DataTable();
            dt = db.Select_OrderNo(SQLPT.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                ConvertPreOrder(dt.Rows[0]["ScheduleOrderNumber"].ToString());
            }
            else
            {
                MessageBox.Show("No data of this HN");
            }
            lblStatus.Text = "Convert order sucessful.";
            MessageBox.Show("Convert order sucessful.");
        }
        private void ConvertPreOrder(string OrderNo)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            string careUID = "2283";
            progressBar1.Value1 = 10;
            var clsSQL = new clsSQLNative();
            //หาข้อมูลใน tb PatientScheduleOrder ของ OrderNumber ที่ถูกส่งมา
            DataTable Schdt = db.Select_PatientScheduleOrder(OrderNo);
            if (Schdt != null && Schdt.Rows.Count > 0)
            {
                for (int x = 0; x < Schdt.Rows.Count; x++)
                {
                    MassConvertLogUID = "";
                    //เช็คว่ามีการ Convert แล้วหรือยัง
                    if (string.IsNullOrEmpty(Schdt.Rows[x]["PatientVisitUID"].ToString()) || Schdt.Rows[x]["PatientVisitUID"].ToString() == "0")
                    {
                        #region MassConvertLog
                        if (MassConvertLogEnable == "true")
                        {
                            MassConvertLogUID = clsSQL.Return("INSERT INTO MassConvertLog(HN,StartWhen,Username,Detail,PatientUID) OUTPUT INSERTED.UID VALUES('" + MassConvertLogHN + "',GETDATE(),'" + clsTempData.Username + "','Individual','" + MassConvertLogPatientUID + "');", clsSQLNative.DBType.SQLServer, "MobieConnect");
                        }
                        #endregion
                        //Update Booking ของคนไข้คนนี้ให้ Status Flag = 'U'
                        string outMessage = "";
                        if (db.Update_Booking_Status(Schdt.Rows[x]["PatientUID"].ToString(), Schdt.Rows[x]["ScheduledDttm"].ToString(),out outMessage) == false)
                        {
                            MessageBox.Show("Cannot update booking status\n\n"+outMessage);
                        }

                        //หา BillPackageItem ทั้งหมดของคนไข้คนนี้
                        DataTable SchDetdt = db.Select_OrderCategory_By_ScheduleOrderNumber_Filter_Package_AllItem(Schdt.Rows[x]["UID"].ToString());

                        //dgv.DataSource = SchDetdt;

                        if (SchDetdt != null && SchDetdt.Rows.Count > 0)
                        {

                            //หา BillableItem ของ Order ของคนไข้คนนี้ตาม BSMDD
                            DataTable bsmdt = db.Select_BillableItem_By_ScheduleOrderNumber_GroupBy_BSMDDUID(Schdt.Rows[x]["UID"].ToString()); //new check by BSMDDUID
                            //หา Order Category ของ Order ของคนไข้คนนี้
                            DataTable catdt = db.Select_OrderCategory_By_ScheduleOrderNumber_GroupBy_OrderCategoryUID(Schdt.Rows[x]["UID"].ToString()); //old


                            if (bsmdt != null && bsmdt.Rows.Count > 0)
                            {
                                //หาว่า Billable Type นี้มี Medicin , Supply อยู่ด้วยหรือไม่
                                //if (CheckBillableMedicineItemTypeInsidePackage(bsmdt) == true) //New concept check by BSMDDUID
                                //{
                                //MessageBox.Show("This version not support Medicine Item inside Package");
                                //return;
                                //}
                                //==================================================
                                //GENERATE PATIENT VISIT AND CREATE VISIT NO SECTION.
                                //==================================================
                                lblStatus.Text = "Generate Visit";
                                string visitno = db.SEQID("SEQVisitID");

                                if (Encounter_Desc.ToUpper().StartsWith("H"))
                                    visitno = "H" + BU + "-" + DateTime.Today.ToString("yy") + "-" + visitno.PadLeft(6, '0').ToString();
                                else
                                    visitno = "O" + BU + "-" + DateTime.Today.ToString("yy") + "-" + visitno.PadLeft(6, '0').ToString();

                                //Insert ข้อมูลเข้า table PatientVisit แล้ว return ค่า PatientVisitUID ออกมา
                                string patvisituid = db.Insert_PatientVisit(Schdt.Rows[x]["PatientUID"].ToString(), Encounter_UID
                                   , careUID, "0", Location_UID, "0", OwnerOrganization, "mass convert", Cuser
                                   , OwnerOrganization, "0");

                                //Update PatientVisitUID ใน table PatientScheduleOrder ให้เป็น PatientVisitUID จริง ๆ
                                db.Update_PatientScheduleOrder_By_UID(Schdt.Rows[x]["UID"].ToString(), patvisituid, Cuser);

                                //Insert ข้อมูลเข้า table PatientVisitPayor แล้ว return ค่า PatientVisitPayorUID ออกมา
                                //string patientVisitPayorUID = db.Insert_PatientVisitPayor(patvisituid, Schdt.Rows[x]["PatientUID"].ToString()
                                //    , ((DataRowView)cboPayorOffice.SelectedItem).Row.ItemArray[0].ToString(), ((DataRowView)cboPolicy.SelectedItem).Row.ItemArray[0].ToString(),
                                //((DataRowView)cboPolicy.SelectedItem).Row.ItemArray[1].ToString(), "", "MassCovert", Cuser, "A", OwnerOrganization, ((DataRowView)cboPayor.SelectedItem).Row.ItemArray[0].ToString(),
                                //((DataRowView)cboAgreement.SelectedItem).Row.ItemArray[0].ToString(), "", "", ((DataRowView)cboAgreement.SelectedItem).Row.ItemArray[2].ToString(), "0");
                                
                                //string patientVisitPayorUID = db.Insert_PatientVisitPayor(patvisituid, Schdt.Rows[x]["PatientUID"].ToString()
                                //    , cboPayor.SelectedItem.Value.ToString(), cboPolicy.SelectedItem.Value.ToString(),
                                //cboPolicy.SelectedItem.Text.ToString(), "", "MassCovert", Cuser, "A", OwnerOrganization, cboPayor.SelectedItem.Value.ToString(),
                                //cboAgreement.SelectedItem.Value.ToString(), "", "", FindAgreementPercentTag(cboAgreement.SelectedItem.Value.ToString()), "0");

                                string patientVisitPayorUID = db.Insert_PatientVisitPayor(patvisituid, Schdt.Rows[x]["PatientUID"].ToString()
                                    , cboPayorOffice.SelectedItem.Value.ToString(), cboPolicy.SelectedItem.Value.ToString(),
                                cboPolicy.SelectedItem.Text.ToString(), "", "MassCovert", Cuser, "A", OwnerOrganization, cboPayor.SelectedItem.Value.ToString(),
                                cboAgreement.SelectedItem.Value.ToString(), "", "", FindAgreementPercentTag(cboAgreement.SelectedItem.Value.ToString()), "0");

                                //Insert ข้อมูลเข้า table PatientVisiID
                                db.Insert_PatientVisitID(visitno, "Y", patvisituid, Cuser, OwnerOrganization);

                                //==============================================
                                //INSERT PATIENT PACKAGE & PACKAGE ITEM SECTION. 
                                //==============================================
                                //หา Package ย่อย ของ ScheduleOrderNumber นี้
                                string packuid = string.Empty;
                                DataTable dtBDMSScheduleOrder = db.Select_BDMSPatientScheduleOrderDetail_By_ScheduleOrderUID(Schdt.Rows[x]["UID"].ToString());
                                if (dtBDMSScheduleOrder != null && dtBDMSScheduleOrder.Rows.Count > 0)
                                {
                                    for (int m = 0; m < dtBDMSScheduleOrder.Rows.Count; m++)
                                    {
                                        //Insert ข้อมูลเข้า table PatientPackage ตาม ScheduleOrderNumber ที่ส่งเข้ามา  แล้ว return ค่า PackageUID ออกมา
                                        packuid = db.Insert_PatientPackage_by_ScheduleOrderNumber(Schdt.Rows[x]["UID"].ToString(), OwnerOrganization, dtBDMSScheduleOrder.Rows[m]["BillPackageUID"].ToString(), patvisituid);

                                        //Insert ข้อมูลเข้า table PatientPackageItem
                                        db.Insert_PatientPackageItem_by_ScheduleOrderNumber_BillPackage("", packuid, OwnerOrganization);
                                    }
                                }
                                //วนลูป add หัว package ทั้ง program หลัก และ Option เข้า table PatientBillableItem
                                DataTable dtPktBdmsSch = db.Select_PatientPackage_By_VisitUID(patvisituid);
                                if (dtPktBdmsSch != null && dtPktBdmsSch.Rows.Count > 0)
                                {
                                    for (int n = 0; n < dtPktBdmsSch.Rows.Count; n++)
                                    {
                                        //INSERT PATIENT BILLABLE FOR BILL PACKAGE IDENTIFIER TYPE

                                        //Insert ข้อมูลเข้า table PatientBillableItem 
                                        db.Insert_PatientBillableItem_For_PackageHeader(OrderNo, dtPktBdmsSch.Rows[n]["UID"].ToString(), OwnerOrganization, Cuser, patvisituid, Schdt.Rows[x]["PatientUID"].ToString(), dtPktBdmsSch.Rows[n]["BillPackageUID"].ToString());
                                    }
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

                                            lblStatus.Text = "Add Order";
                                            string patorderuid = string.Empty;
                                            //Insert ข้อมูลเข้า table PatientOrder แล้ว return PatientOrderUID ออกมา
                                            bool flaginsertPatientOrder = db.insert_PatientOrder(SEQ, Schdt.Rows[x]["PatientUID"].ToString()
                                               , patvisituid, Cuser, "mass convert"
                                               , SchDetdt.Rows[j]["ORDCTUID"].ToString()
                                               , Identype, Location_UID, ordtoval, OwnerOrganization, out patorderuid);

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
                                                            //ถ้ามีแล้วให้ดึง LabNo ที่ถูก Generate แล้วมา  เพื่อที่จะได้ไม่ต้อง Genrate LabNo ใหม่
                                                            SpecialSEQ = mssdt.Rows[0]["LabNo"].ToString();
                                                        }
                                                        //นำ Lab No ที่ Generate มาแล้วไปเช็คใน table request ว่ามีแล้วหรือยัง
                                                        DataTable labnodt = db.Select_Request_By_LabNo(SpecialSEQ);
                                                        if (labnodt != null && labnodt.Rows.Count > 0) //found lab no in database
                                                        {
                                                            //ถ้ามีแล้วให้คืนค่า RequestUID กลับมา
                                                            requid = labnodt.Rows[0]["UID"].ToString();
                                                        }
                                                        else //not found lab no in database
                                                        {
                                                            //ถ้ายังไม่มี ให้ทำการ Insert ข้อมูลใหม่เข้าไปใน table request แล้ว return RequestUID ออกมา
                                                            requid = db.Insert_Request(patvisituid
                                                           , Schdt.Rows[x]["PatientUID"].ToString(), Cuser, SpecialSEQ, "Specimen Collected"
                                                                        , Location_UID, ordtoval, OwnerOrganization);
                                                        }
                                                        // Update table PatientOrder 
                                                        db.Update_PatientOrder_By_UID(patorderuid, requid);

                                                        //Insert ข้อมูลเข้า table RequestDetail แล้ว return UID กลับมา
                                                        string retreqdet = db.Insert_RequestDetail(SchDetdt.Rows[j]["ItemUID"].ToString(),
                                                                    requid, Cuser, SchDetdt.Rows[j]["Comments"].ToString(), "Specimen Collected", OwnerOrganization);
                                                        //ถ้า Insert สำเร็จ
                                                        if (retreqdet != "")
                                                        {
                                                            //หาข้อมูลว่ามีการ PreOrder แล้วหรือยัง
                                                            DataTable pschdt = db.Select_PatientScheduleOrder(OrderNo);
                                                            if (pschdt != null && pschdt.Rows.Count > 0)
                                                            {
                                                                //เช็ค Specimen ที่เราได้ทำตอน Generate LabNo
                                                                DataTable bspctsdt = db.Select_BDMSASTMAssSpecimenTestset_BY_PatientScheduleOrderUID_And_TestSet_HN(
                                                                              pschdt.Rows[0]["UID"].ToString(), SchDetdt.Rows[j]["RequestCode"].ToString()
                                                                              , Schdt.Rows[x]["HN"].ToString());
                                                                //ถ้ามี Specimen ตอน Generate LabNo แล้ว
                                                                if (bspctsdt != null && bspctsdt.Rows.Count > 0)
                                                                {
                                                                    //เข้าไปดึง specimen ใน table master specimen โดยดึงตาม Code ที่กำหนด
                                                                    DataTable spdt = db.Select_Specimen_By_Name(bspctsdt.Rows[0]["Specimen"].ToString());
                                                                    if (spdt != null && spdt.Rows.Count > 0)
                                                                    {
                                                                        //Insert ข้อมูล Specimen เข้า table RequestDetailSpecimen
                                                                        db.Insert_RequestDetailSpecimen(retreqdet, "NULL", "'Manual Entry'"
                                                                                , spdt.Rows[0]["UID"].ToString(), spdt.Rows[0]["Name"].ToString(), "0", "0"
                                                                                , "3", "0", "NULL", "0", "0", Cuser, "'Mass Convert'", Cuser
                                                                                , OwnerOrganization, "NULL", "NULL", "Specimen Collected", "''", "NULL", "NULL");
                                                                    }

                                                                }
                                                                else
                                                                {
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
                                                                        //Insert ข้อมูล Specimen เข้า table RequestDetailSpecimen
                                                                        db.Insert_RequestDetailSpecimen(retreqdet, "NULL", "'Manual Entry'"
                                                                                , spMasterdt.Rows[0]["UID"].ToString(), spMasterdt.Rows[0]["Name"].ToString(), "0", "0"
                                                                                , "3", "0", "NULL", "0", "0", Cuser, "'Mass Convert'", Cuser
                                                                                , OwnerOrganization, "NULL", "NULL", "Specimen Collected", "''", "NULL", "NULL");
                                                                    }
                                                                }
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
                                                            //Packuid
                                                        } //if (retreqdt !="")

                                                        DataTable lsdt = db.GetRequestDetail_Status(SpecialSEQ);
                                                        if (lsdt != null && lsdt.Rows.Count > 0)
                                                        {
                                                            var cstat = CheckStatus(lsdt).Split('|');
                                                            if (cstat.Count() == 2)
                                                            {
                                                                db.Update_Request_By_LabNumber(SpecialSEQ, cstat[0].ToString());

                                                            }
                                                        }// if (lsdt != null && lsdt.Rows.Count > 0)



                                                    }//if (OrderCateType.ToString().ToLower()=="lab")
                                                    else if (OrderCateType.ToString().ToLower() == "xray")
                                                    {
                                                        //requid = db.Insert_Request(patvisituid
                                                        //  , Schdt.Rows[x]["PatientUID"].ToString(), Cuser, SpecialSEQ, "Executed"
                                                        //  , cbLocationOrder.SelectedValue.ToString(), ordtoval, OwnerOrganization);

                                                        requid = db.Insert_Request(patvisituid
                                                          , Schdt.Rows[x]["PatientUID"].ToString(), Cuser, SpecialSEQ, "Executed"
                                                          , Location_UID, ordtoval, OwnerOrganization);

                                                        db.Update_PatientOrder_By_UID(patorderuid, requid);

                                                        string retreqdet = db.Insert_RequestDetail(SchDetdt.Rows[j]["itemUID"].ToString(),
                                                               requid, Cuser, SchDetdt.Rows[j]["Comments"].ToString(), "Executed", OwnerOrganization);
                                                        if (retreqdet != "")
                                                        {
                                                            patorddetails = db.insert_PatientOrderDetail(SEQ, retreqdet
                                                       , IdentTypeforOrderDetail, Cuser, SchDetdt.Rows[j]["BillableItemUID"].ToString(), "mass convert"
                                                       , orddetstatus, SchDetdt.Rows[j]["Quantity"].ToString()
                                                       , SchDetdt.Rows[j]["OrderSubCategoryUID"].ToString()
                                                       , SchDetdt.Rows[j]["ORDCTUID"].ToString()
                                                       , SchDetdt.Rows[j]["Amount"].ToString()
                                                       , (NetAmount == "0" ? SchDetdt.Rows[j]["Amount"].ToString() : NetAmount)
                                                       , SchDetdt.Rows[j]["Amount"].ToString()
                                                       , dtPTPck.Rows[0]["PatientPackageUID"].ToString(), OwnerOrganization, patientVisitPayorUID);
                                                            //packuid

                                                        }//if (retreqdet != "")



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
                                                    //packuid
                                                }//=================================================================================


                                                //**********************************************************
                                                //INSERT PATIENTBILLABLEITEM SECTION 
                                                //IdentifyingUID = SchDetdt.Rows[j]["ItemUID"].ToString()


                                                //**********************************************************
                                                lblStatus.Text = "Add billable item.";
                                                //IdentifyingType = REQUESTITEM , OrderType = REQUEST
                                                //IdentifyingType = ORDITEM , OrderType = PATIENTORDER
                                                lblStatus.Text = "Convert order sucessful.";
                                                if (Identype == "REQUEST")//ถ้าเป็น Order Lab , Xray
                                                {
                                                    if (OrderCateType.ToString().ToLower() == "lab")
                                                    {
                                                        //error
                                                        db.Insert_BillableItem(Schdt.Rows[x]["PatientUID"].ToString()
                                                        , patvisituid, SchDetdt.Rows[j]["ItemUID"].ToString(), "'REQUESTITEM'", Cuser, OwnerOrganization
                                                        , SchDetdt.Rows[j]["BSMDDUID"].ToString(), SchDetdt.Rows[j]["Amount"].ToString()
                                                        , (NetAmount == "0" ? SchDetdt.Rows[j]["Amount"].ToString() : NetAmount), SchDetdt.Rows[j]["BillableItemUID"].ToString()
                                                        , SchDetdt.Rows[j]["Quantity"].ToString(), "null", "null", SchDetdt.Rows[j]["Comments"].ToString()
                                                        , "null", "null", dtPTPck.Rows[0]["PatientPackageUID"].ToString(), "'REQUEST'", requid, patorddetails, "Specimen Collected");
                                                        //packuid
                                                    }
                                                    else//(OrderCateType.ToString().ToLower() == "xray")
                                                    {
                                                        db.Insert_BillableItem(Schdt.Rows[x]["PatientUID"].ToString()
                                                        , patvisituid, SchDetdt.Rows[j]["ItemUID"].ToString(), "'REQUESTITEM'", Cuser, OwnerOrganization
                                                        , SchDetdt.Rows[j]["BSMDDUID"].ToString(), SchDetdt.Rows[j]["Amount"].ToString()
                                                        , (NetAmount == "0" ? SchDetdt.Rows[j]["Amount"].ToString() : NetAmount), SchDetdt.Rows[j]["BillableItemUID"].ToString()
                                                        , SchDetdt.Rows[j]["Quantity"].ToString(), "null", "null", SchDetdt.Rows[j]["Comments"].ToString()
                                                        , "null", "null", dtPTPck.Rows[0]["PatientPackageUID"].ToString(), "'REQUEST'", requid, patorddetails, "Executed");
                                                        //packuid
                                                    }
                                                }
                                                else //ถ้าเป็น Order อื่น ๆ
                                                {

                                                    db.Insert_BillableItem(Schdt.Rows[x]["PatientUID"].ToString()
                                                        , patvisituid, SchDetdt.Rows[j]["ItemUID"].ToString(), "'ORDITEM'", Cuser, OwnerOrganization
                                                        , SchDetdt.Rows[j]["BSMDDUID"].ToString(), SchDetdt.Rows[j]["Amount"].ToString()
                                                        , (NetAmount == "0" ? SchDetdt.Rows[j]["Amount"].ToString() : NetAmount), SchDetdt.Rows[j]["BillableItemUID"].ToString()
                                                        , SchDetdt.Rows[j]["Quantity"].ToString(), "null", "null", SchDetdt.Rows[j]["Comments"].ToString()
                                                        , "null", "null", dtPTPck.Rows[0]["PatientPackageUID"].ToString(), "'PATIENTORDER'", patorderuid, patorddetails, "Raised");
                                                    //packuid


                                                    //db.Insert_PatientBillableItem(SchDetdt.Rows[j]["UID"].ToString()
                                                    //                                        , packuid, patorddetails, "ORDITEM", patorderuid, "Raised", OwnerOrganization);

                                                }
                                            }//if (flaginsertPatientOrder)
                                            else
                                            {
                                                MessageBox.Show(
                                                    "ไม่สามารถบันทึกลงตาราง PatientOrder ได้", 
                                                    "PatientOrder", 
                                                    MessageBoxButtons.OK, 
                                                    MessageBoxIcon.Error);
                                            }
                                        }//if (catdt.Rows[i]["Value"].ToString().ToLower() == SchDetdt.Rows[j][""].ToString())

                                    }//for (int j = 0; j < SchDetdt.Rows.Count; j++)
                                }//for (int i = 0; i < catdt.Rows.Count; i++)  ลูปตาม OrderCat

                                //=================== ส่ง EN ไป update ใน table Patient ของโปรแกรม Contact Checkup
                                string strPayor = db.Select_Payor(patvisituid);
                                db.Update_EN_IN_ContactCheckup(Schdt.Rows[x]["ScheduleOrderNumber"].ToString(), visitno, strPayor);

                                //==================== Update ใน table tblPatientList ว่าคนไข้คนนี้ได้ทำการ Convert แล้ว
                                Update_Status_In_TblPatientList(Schdt.Rows[x]["ScheduleOrderNumber"].ToString());

                                //========= Insert table PatientServiceEvent เพื่อให้สามารถค้นหาหน้า Patient List ได้
                                db.Insert_PatientServiceEvent(patvisituid);
                            }
                        }//  
                    }
                    #region MassConvertLog
                    if (MassConvertLogEnable == "true")
                    {
                        if (MassConvertLogUID != "")
                        {
                            clsSQL.Execute("UPDATE MassConvertLog SET EndWhen=GETDATE() WHERE UID=" + MassConvertLogUID + ";", clsSQLNative.DBType.SQLServer, "MobieConnect");
                        }
                    }
                    #endregion
                }
                progressBar1.Value1 = 100;
                //
            }//if (Schdt != null && Schdt.Rows.Count > 0)
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
            var clsSQL=new clsSQLNative();
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
                strSQL.Append("(PatientUID='"+dt.Rows[0]["PatientUID"].ToString().Trim()+"' OR EN='"+dt.Rows[0]["EN"].ToString().Trim()+"') ");
                strSQL.Append("AND ProStatus=1;");
                #endregion
                clsSQL.Execute(strSQL.ToString(), clsSQLNative.DBType.SQLServer, "MobieConnect");
            }
            catch (Exception) { }
            #endregion
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

        private void txtHN_TextChanged(object sender, EventArgs e)
        {
            if (txtHN.Text.Length == 12)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                var clsSQL = new clsSQLNative();
                var strSQL = "";
                db = new SQL();
                DataTable dtPatient = new DataTable();
                dtPatient = db.Select_Patient_By_HN(txtHN.Text.Trim());
                if (dtPatient.Rows.Count > 0 && dtPatient != null)
                {
                    MassConvertLogHN = dtPatient.Rows[0]["PASID"].ToString();
                    strSQL = "SELECT TOP 1 rowguid FROM Patient WHERE HN='" + MassConvertLogHN + "' /*AND DOE BETWEEN '" + dtpDateFrom.Value.ToString("yyyy-MM-dd HH:mm") + "' AND '" + dtpDateTo.Value.ToString("yyyy-MM-dd HH:mm") + "'*/ ORDER BY DOE DESC;";
                    MassConvertLogPatientUID = clsSQL.Return(strSQL, clsSQLNative.DBType.SQLServer, "MobieConnect");
                    txtPTName.Text = dtPatient.Rows[0]["Forename"] + "  " + dtPatient.Rows[0]["Surname"] + "";
                }
            }
        }

        private void cboPayor_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
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
            if (cboPayor.SelectedIndex > 0 && cboAgreement.SelectedIndex>0)
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
    }
}