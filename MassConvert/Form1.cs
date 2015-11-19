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
using Telerik.Data;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Telerik.WinControls.UI.Export;

namespace MassConvert
{
    public partial class Form1 : Form
    {
        public Form1()
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

        //Load Form
        private void Form1_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(DateTime.Today.AddHours(45).ToString("dd MMM yyyy hh:mm:ss"));


            db = new SQL();

            //CheckCareproviderUID(textBox1.Text);
            CheckCareproviderUID(Login_UID);

            AddingCheckBoxColumn();

            //DataTable pdt = db.Select_InsuranceCompany();
            //if (pdt != null && pdt.Rows.Count > 0)
            //{
            //    cboPayor.DataSource = pdt;
            //    cboPayor.DisplayMember = "CompanyName";
            //    cboPayor.ValueMember = "UID";
            //}


            //DataTable cdt = db.Select_CareProvider();
            //if (cdt != null && cdt.Rows.Count > 0)
            //{
            //    comboBox2.DataSource = cdt;
            //    comboBox2.DisplayMember = "FullName";
            //    comboBox2.ValueMember = "UID";
            //}


            //DataTable ldt = db.Select_Location_Condition("'Dispensing','Medical Records','Pac_Ward','Pac_Bed'");
            //if (ldt != null && ldt.Rows.Count > 0)
            //{
            //    cbLocationOrder.DataSource = ldt;
            //    cbLocationOrder.DisplayMember = "Name";
            //    cbLocationOrder.ValueMember = "UID";
            //}

            //DataTable edt = db.Select_EncouterType();
            //if (edt != null && edt.Rows.Count > 0)
            //{
            //    this.comboBox1.DataSource = edt;
            //    this.comboBox1.DisplayMember = "Description";
            //    this.comboBox1.ValueMember = "UID";
            //}

            //DataTable ordto = db.Select_OrderToLocationUID("124", "55653", "null");
            //if (ordto != null && ordto.Rows.Count > 0)
            //    MessageBox.Show(ordto.Rows[0][0].ToString());
        }

        //ปุ่ม Convert
        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Maximum = Convert.ToInt32(gvPatient.Rows.Count);
            progressBar1.Value = 0;
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
                    SQLPT.Append(" and ps.ScheduledDttm between '" + gvPatient.Rows[i].Cells["DOE"].Value.ToString() + "' and '" + gvPatient.Rows[i].Cells["DOE"].Value.ToString() + "'");
                    SQLPT.Append(" and ps.StatusFlag = 'A'");
                    DataTable dt = new DataTable();
                    dt = db.Select_OrderNo(SQLPT.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        ConvertPreOrder(dt.Rows[0]["ScheduleOrderNumber"].ToString());
                        //ConvertPreOrder("180");
                    }
                }
                progressBar1.Value += 1;
            }
            MessageBox.Show("Convert order sucessful.");
        }
        /// <summary>
        /// Function สำหรับ Convert Order
        /// </summary>
        /// <param name="OrderNo">Schedule Order Number</param>
        private void ConvertPreOrder(string OrderNo)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            string careUID = "2283";

            //หาข้อมูลใน tb PatientScheduleOrder ของ OrderNumber ที่ถูกส่งมา
            DataTable Schdt = db.Select_PatientScheduleOrder(OrderNo);
            if (Schdt != null && Schdt.Rows.Count > 0)
            {
                for (int x = 0; x < Schdt.Rows.Count; x++)
                {
                    //เช็คว่ามีการ Convert แล้วหรือยัง
                    if (string.IsNullOrEmpty(Schdt.Rows[x]["PatientVisitUID"].ToString()) || Schdt.Rows[x]["PatientVisitUID"].ToString() == "0")
                    {
                        //Update Booking ของคนไข้คนนี้ให้ Status Flag = 'U'
                        string outMessage = "";
                        if (db.Update_Booking_Status(Schdt.Rows[x]["PatientUID"].ToString(), Schdt.Rows[x]["ScheduledDttm"].ToString(),out outMessage) == false)
                        {
                            MessageBox.Show("Cannot update booking status\n\n"+outMessage);
                        }

                        //หา BillPackageItem ทั้งหมดของคนไข้คนนี้
                        DataTable SchDetdt = db.Select_OrderCategory_By_ScheduleOrderNumber_Filter_Package_AllItem(Schdt.Rows[x]["UID"].ToString());

                        dgv.DataSource = SchDetdt;

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
                                string patientVisitPayorUID = db.Insert_PatientVisitPayor(patvisituid, Schdt.Rows[x]["PatientUID"].ToString()
                                    , ((DataRowView)cboPayorOffice.SelectedItem).Row.ItemArray[0].ToString(), ((DataRowView)cboPolicy.SelectedItem).Row.ItemArray[0].ToString(),
                                ((DataRowView)cboPolicy.SelectedItem).Row.ItemArray[1].ToString(), "", "MassCovert", Cuser, "A", OwnerOrganization, ((DataRowView)cboPayor.SelectedItem).Row.ItemArray[0].ToString(),
                                ((DataRowView)cboAgreement.SelectedItem).Row.ItemArray[0].ToString(), "", "", ((DataRowView)cboAgreement.SelectedItem).Row.ItemArray[2].ToString(), "0");

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

                                                //IdentifyingType = REQUESTITEM , OrderType = REQUEST
                                                //IdentifyingType = ORDITEM , OrderType = PATIENTORDER

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
                }//
            }//if (Schdt != null && Schdt.Rows.Count > 0)
        }
        

        //ปุ่ม Pre Generate Lab Number
        private void button2_Click(object sender, EventArgs e)
        {
            progressBar1.Maximum = Convert.ToInt32(gvPatient.Rows.Count);
            progressBar1.Value = 0;
            for (int i = 0; i <= gvPatient.Rows.Count - 1; i++)
            {
                //Loop ทำงานเฉพาะคนที่ Check Box
                if(Convert.ToBoolean(gvPatient.Rows[i].Cells["Check"].Value) == true)
                {
                    StringBuilder SQLPT = new StringBuilder();
                    SQLPT.Append("SELECT * FROM [PatientScheduleOrder] ps");
                    SQLPT.Append(" inner join Patient p on p.UID = ps.PatientUID ");
                    SQLPT.Append(" and p.Forename = N'" + gvPatient.Rows[i].Cells["Name"].Value.ToString().Trim() + "'");
                    SQLPT.Append(" and Surname = N'" + gvPatient.Rows[i].Cells["LastName"].Value.ToString().Trim() + "'");
                    SQLPT.Append(" and ps.ScheduledDttm between '" + gvPatient.Rows[i].Cells["DOE"].Value.ToString() + "' and '" + gvPatient.Rows[i].Cells["DOE"].Value.ToString() + "'");
                    SQLPT.Append(" and ps.StatusFlag = 'A'");
                    DataTable dt = new DataTable();
                    dt = db.Select_OrderNo(SQLPT.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        GenLabEpisode(dt.Rows[0]["ScheduleOrderNumber"].ToString(), dt.Rows[0]["ScheduleOrderNumber"].ToString());
                        //GenLabEpisode(180,180);
                    }
                }
                progressBar1.Value = +1;
            }
            MessageBox.Show("Generate lab episode sucessful.");
            //dataGridView1.Visible = true;
        }
        /// <summary>
        /// Function สำหรับ Generate Lab Number
        /// </summary>
        /// <param name="OrderNoFrom">Schedule Order Number</param>
        /// <param name="OrderNoTo">Schedule Order Number</param>
        private void GenLabEpisode(string OrderNoFrom, string OrderNoTo)
        {
            //หาว่า ScheduleNo ของคนไข้ จากข้อมูลที่ทำการ Bulk Regis
            DataTable Schdt = db.Select_PatientScheduleOrder_Between(OrderNoFrom, OrderNoTo);
            dataGridView1.DataSource = Schdt;
            if (dataGridView1.RowCount > 0)
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    //เช็คว่ามีการ Generate Lab No ไปแล้วหรือยัง
                    DataTable dtchkGenLab = db.Select_BDMSASTMassConvert_Between(OrderNoFrom, OrderNoTo);
                    if (dtchkGenLab == null || dtchkGenLab.Rows.Count == 0)
                    {
                        List<LabItemObject> litmobj = new List<LabItemObject>();
                        string scheduleUID = dataGridView1.Rows[i].Cells[0].Value.ToString();//UID
                        string scheduleNo = dataGridView1.Rows[i].Cells[1].Value.ToString();//ScheduleOrderNo
                        string patientuid = dataGridView1.Rows[i].Cells[2].Value.ToString();//PatientUID
                        string LabSEQ = db.SEQID("SEQRequest");
                        LabSEQ = OwnerOrganization + "0" + LabSEQ.PadLeft(6, '0');
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

                            }//if (f.ToString != "")

                        });


                        db.Update_BDMSASTMassConvert_By_PatientScheduleOrderUID(scheduleUID, "D");
                        db.Insert_BDMSASTMassConvert(scheduleUID, scheduleNo, LabSEQ
                            , "N", "A"
                            , litmobj.Where(f => f.Specimen != string.Empty).Select(f => f.Specimen).Distinct().Count().ToString()
                            , hn, patientuid, patfull, spelst, "");
                    }
                    //ปิดการเช็คว่ามีการ Generate Lab No ไปแล้วหรือยัง

                } //for (int i = 0; i < dataGridView1.RowCount; i++)
                //MessageBox.Show("Generate lab number successful.");
                //dataGridView1.Visible = true;
            }
        }

        void ConvertOrder(string ordernumber)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            //หา Patient Schedule Order
            DataTable Schdt = db.Select_PatientScheduleOrder(ordernumber);
            if (Schdt != null && Schdt.Rows.Count > 0)
            {
                for (int x = 0; x < Schdt.Rows.Count; x++)
                {
                    //หา Order Category ตาม Schedule Order Number
                    DataTable SchDetdt = db.Select_OrderCategory_By_ScheduleOrderNumber_Filter_Package_AllItem(Schdt.Rows[x]["UID"].ToString());

                    dgv.DataSource = SchDetdt;

                    if (SchDetdt != null && SchDetdt.Rows.Count > 0)
                    {


                        DataTable catdt = db.Select_OrderCategory_By_ScheduleOrderNumber_GroupBy_OrderCategoryUID(Schdt.Rows[x]["UID"].ToString()); //old
                        // DataTable catdt = db.Select_OrderCategory_By_ScheduleOrderNumber_Filter_Package_AllItem(Schdt.Rows[x]["UID"].ToString());
                        if (catdt != null && catdt.Rows.Count > 0)
                        {
                            if (CheckMedicineItemTypeInsidePackage(catdt) == true)
                            {
                                MessageBox.Show("This version not support Medicine Item inside Package");
                                return;
                            }

                            //GENERATE PATIENT VISIT AND CREATE VISIT NO SECTION.
                            string visitno = db.SEQID("SEQVisitID");
                            //if (comboBox1.Text.ToUpper().StartsWith("H"))
                            if (Encounter_Desc.ToUpper().StartsWith("H"))
                                visitno = "H" + BU + "-" + DateTime.Today.ToString("yy") + "-" + visitno.PadLeft(6, '0').ToString();
                            else
                                visitno = "O" + BU + "-" + DateTime.Today.ToString("yy") + "-" + visitno.PadLeft(6, '0').ToString();

                            //string patvisituid = db.Insert_PatientVisit(Schdt.Rows[x]["PatientUID"].ToString(), comboBox1.SelectedValue.ToString()
                            //   , "0", "0", this.cbLocationOrder.SelectedValue.ToString(), "0", OwnerOrganization, "mass convert", Cuser
                            //   , OwnerOrganization, "0");
                            string patvisituid = db.Insert_PatientVisit(Schdt.Rows[x]["PatientUID"].ToString(), Encounter_UID
                               , "0", "0", Location_UID, "0", OwnerOrganization, "mass convert", Cuser
                               , OwnerOrganization, "0");

                            db.Update_PatientScheduleOrder_By_UID(Schdt.Rows[x]["UID"].ToString(), patvisituid, Cuser);

                            //*************************************************************************

                            string patientVisitPayorUID = db.Insert_PatientVisitPayor(patvisituid, Schdt.Rows[x]["PatientUID"].ToString()
                                , ((DataRowView)cboPayorOffice.SelectedItem).Row.ItemArray[0].ToString(), ((DataRowView)cboPolicy.SelectedItem).Row.ItemArray[0].ToString(),
                            ((DataRowView)cboPolicy.SelectedItem).Row.ItemArray[1].ToString(), "", "MassCovert", Cuser, "A", OwnerOrganization, ((DataRowView)cboPayor.SelectedItem).Row.ItemArray[0].ToString(),
                            ((DataRowView)cboAgreement.SelectedItem).Row.ItemArray[0].ToString(), "", "", ((DataRowView)cboAgreement.SelectedItem).Row.ItemArray[2].ToString(), "0");
                            //*************************************************************************

                            db.Insert_PatientVisitID(visitno, "Y", patvisituid, Cuser, OwnerOrganization);


                            //=============================================================================================

                            //INSERT PATIENT PACKAGE & PACKAGE ITEM SECTION. 

                            string packuid = db.Insert_PatientPackage_by_ScheduleOrderNumber(Schdt.Rows[x]["ScheduleOrderNumber"].ToString(), OwnerOrganization);
                            //db.Insert_PatientPackageItem_by_ScheduleOrderNumber(Schdt.Rows[x]["ScheduleOrderNumber"].ToString(), packuid);

                            db.Insert_PatientPackageItem_by_ScheduleOrderNumber_BillPackage("", packuid, OwnerOrganization);
                            //=============================================================================================

                            //INSERT PATIENT BILLABLE FOR BILL PACKAGE IDENTIFIER TYPE

                            db.Insert_PatientBillableItem_For_PackageHeader(ordernumber, packuid, OwnerOrganization, Cuser, patvisituid, Schdt.Rows[x]["PatientUID"].ToString(),"");

                            //=============================================================================================

                            for (int i = 0; i < catdt.Rows.Count; i++)
                            {
                                string SEQ = db.SEQID("SEQpatientorder");
                                string SpecialSEQ = GenerateSequenEachType(catdt.Rows[i]["Description"].ToString().ToLower());
                                string OrderCateType = catdt.Rows[i]["Description"].ToString().ToLower();
                                string Identype = string.Empty;
                                if (SpecialSEQ == "nodata")
                                    Identype = "PATIENTORDER";
                                else
                                    Identype = "REQUEST";

                                // MessageBox.Show(i.ToString());
                                for (int j = 0; j < SchDetdt.Rows.Count; j++)
                                {

                                    //if (catdt.Rows[i]["Value"].ToString().ToLower() == SchDetdt.Rows[j]["OrderCategoryUID"].ToString()) //Check OrderCategory must match for assign each item to specific table
                                    if (catdt.Rows[i]["Value"].ToString().ToLower() == SchDetdt.Rows[j]["Value"].ToString()) //Check OrderCategory must match for assign each item to specific table
                                    {
                                        string ordcat = "NULL";
                                        string ordtsubcat = "NULL";
                                        string ordtoval = "0"; //Order To Location Value
                                        if (SchDetdt.Rows[j]["ORDCTUID"].ToString() != "")
                                            ordcat = SchDetdt.Rows[j]["ORDCTUID"].ToString();

                                        if (SchDetdt.Rows[j]["OrderSubCategoryUID"].ToString() != "")
                                            ordtsubcat = SchDetdt.Rows[j]["OrderSubCategoryUID"].ToString();


                                        //DataTable ordto = db.Select_OrderToLocationUID(cbLocationOrder.SelectedValue.ToString(),
                                        //     ordcat, ordtsubcat);
                                        DataTable ordto = db.Select_OrderToLocationUID(Location_UID,
                                             ordcat, ordtsubcat);

                                        if (ordto != null && ordto.Rows.Count > 0)
                                            ordtoval = (ordto.Rows[0][0].ToString() == "" ? "0" : ordto.Rows[0][0].ToString());


                                        string patorderuid = string.Empty;
                                        //bool flaginsertPatientOrder = db.insert_PatientOrder(SEQ, Schdt.Rows[x]["PatientUID"].ToString()
                                        //   , patvisituid, Cuser, "mass convert"
                                        //   , SchDetdt.Rows[j]["ORDCTUID"].ToString()
                                        //   , Identype, cbLocationOrder.SelectedValue.ToString(), ordtoval, OwnerOrganization, out patorderuid);
                                        bool flaginsertPatientOrder = db.insert_PatientOrder(SEQ, Schdt.Rows[x]["PatientUID"].ToString()
                                           , patvisituid, Cuser, "mass convert"
                                           , SchDetdt.Rows[j]["ORDCTUID"].ToString()
                                           , Identype, Location_UID, ordtoval, OwnerOrganization, out patorderuid);

                                        if (flaginsertPatientOrder)
                                        {

                                            string orddetstatus = "Raised";
                                            if (OrderCateType.ToString().ToLower() == "lab")
                                                orddetstatus = "Specimen Collected";
                                            else if (OrderCateType.ToString().ToLower() == "xray")
                                                orddetstatus = "Executed";

                                            // กลับมาเช็คต่อ หาในส่วนของ PatientOrderDetail ต่อใน Package
                                            //INSERT DATA IN PATIENT ORDER DETAIL

                                            //IdentifyingType
                                            //========================================
                                            //MEDICATION
                                            //REQUESTDETAIL
                                            //SERVICECHARGE
                                            //BILLABLEEVENT
                                            //ORDERITEM
                                            //DRUGCATALOGITEM
                                            //REQUESTITEM

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

                                            string patorddetails = db.insert_PatientOrderDetail(SEQ, SchDetdt.Rows[j]["ItemUID"].ToString()
                                                   , IdentTypeforOrderDetail, Cuser, SchDetdt.Rows[j]["BillableItemUID"].ToString(), "mass convert"
                                                   , orddetstatus, SchDetdt.Rows[j]["Quantity"].ToString()
                                                   , SchDetdt.Rows[j]["OrderSubCategoryUID"].ToString()
                                                   , SchDetdt.Rows[j]["ORDCTUID"].ToString()
                                                   , SchDetdt.Rows[j]["Amount"].ToString()
                                                   , (NetAmount == "0" ? SchDetdt.Rows[j]["Amount"].ToString() : NetAmount)
                                                   , SchDetdt.Rows[j]["Amount"].ToString()
                                                   , packuid, OwnerOrganization, patientVisitPayorUID);

                                            //========================================


                                            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                            string requid = string.Empty;
                                            if (Identype == "REQUEST")
                                            {
                                                //1827 SPECIMEN COLLECTED
                                                if (OrderCateType.ToString().ToLower() == "lab")
                                                {
                                                    DataTable mssdt = db.Select_BDMSASTMassConvert_Between(ordernumber, ordernumber);
                                                    if (mssdt != null && mssdt.Rows.Count > 0)
                                                    {
                                                        SpecialSEQ = mssdt.Rows[0]["LabNo"].ToString();
                                                    }

                                                    DataTable labnodt = db.Select_Request_By_LabNo(SpecialSEQ);
                                                    if (labnodt != null && labnodt.Rows.Count > 0)
                                                    {
                                                        //found lab no in database
                                                        requid = labnodt.Rows[0]["UID"].ToString();
                                                    }
                                                    else
                                                    {
                                                        // requid = db.Insert_Request(patvisituid
                                                        //, Schdt.Rows[x]["PatientUID"].ToString(), Cuser, SpecialSEQ, "Specimen Collected"
                                                        //             , cbLocationOrder.SelectedValue.ToString(), ordtoval, OwnerOrganization);
                                                        requid = db.Insert_Request(patvisituid
                                                       , Schdt.Rows[x]["PatientUID"].ToString(), Cuser, SpecialSEQ, "Specimen Collected"
                                                                    , Location_UID, ordtoval, OwnerOrganization);
                                                        db.Update_PatientOrder_By_UID(patorderuid, requid);
                                                    }

                                                    string retreqdet = db.Insert_RequestDetail(SchDetdt.Rows[j]["ItemUID"].ToString(),
                                                                requid, Cuser, SchDetdt.Rows[j]["Comments"].ToString(), "Specimen Collected", OwnerOrganization);

                                                    if (retreqdet != "")
                                                    {

                                                    }

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

                                                    db.Insert_RequestDetail(SchDetdt.Rows[j]["itemUID"].ToString(),
                                                           requid, Cuser, SchDetdt.Rows[j]["Comments"].ToString(), "Executed", OwnerOrganization);


                                                }//if (OrderCateType.ToString().ToLower()=="xray")


                                            }//if (Identype=="REQUEST")



                                            //**********************************************************
                                            //INSERT PATIENTBILLABLEITEM SECTION 
                                            //IdentifyingUID = SchDetdt.Rows[j]["ItemUID"].ToString()


                                            //**********************************************************

                                            //IdentifyingType = REQUESTITEM , OrderType = REQUEST
                                            //IdentifyingType = ORDITEM , OrderType = PATIENTORDER

                                            if (Identype == "REQUEST")


                                                if (OrderCateType.ToString().ToLower() == "lab")
                                                {
                                                    //error
                                                    db.Insert_BillableItem(Schdt.Rows[x]["PatientUID"].ToString()
                                                    , patvisituid, SchDetdt.Rows[j]["ItemUID"].ToString(), "'REQUESTITEM'", Cuser, OwnerOrganization
                                                    , SchDetdt.Rows[j]["BSMDDUID"].ToString(), SchDetdt.Rows[j]["Amount"].ToString()
                                                    , (NetAmount == "0" ? SchDetdt.Rows[j]["Amount"].ToString() : NetAmount), SchDetdt.Rows[j]["BillableItemUID"].ToString()
                                                    , SchDetdt.Rows[j]["Quantity"].ToString(), "null", "null", SchDetdt.Rows[j]["Comments"].ToString()
                                                    , "null", "null", packuid, "'REQUEST'", requid, patorddetails, "Specimen Collected");




                                                    //db.Insert_PatientBillableItem(SchDetdt.Rows[j]["UID"].ToString()
                                                    //                                                , packuid, patorddetails, "REQUESTITEM", requid, "Specimen Collected", OwnerOrganization);
                                                }
                                                else//(OrderCateType.ToString().ToLower() == "xray")
                                                {
                                                    db.Insert_BillableItem(Schdt.Rows[x]["PatientUID"].ToString()
                                                    , patvisituid, SchDetdt.Rows[j]["ItemUID"].ToString(), "'REQUESTITEM'", Cuser, OwnerOrganization
                                                    , SchDetdt.Rows[j]["BSMDDUID"].ToString(), SchDetdt.Rows[j]["Amount"].ToString()
                                                    , (NetAmount == "0" ? SchDetdt.Rows[j]["Amount"].ToString() : NetAmount), SchDetdt.Rows[j]["BillableItemUID"].ToString()
                                                    , SchDetdt.Rows[j]["Quantity"].ToString(), "null", "null", SchDetdt.Rows[j]["Comments"].ToString()
                                                    , "null", "null", packuid, "'REQUEST'", requid, patorddetails, "Executed");


                                                    //db.Insert_PatientBillableItem(SchDetdt.Rows[j]["UID"].ToString()
                                                    //                                        , packuid, patorddetails, "REQUESTITEM", requid, "Executed", OwnerOrganization);
                                                }
                                            else
                                            {

                                                db.Insert_BillableItem(Schdt.Rows[x]["PatientUID"].ToString()
                                                    , patvisituid, SchDetdt.Rows[j]["ItemUID"].ToString(), "'ORDITEM'", Cuser, OwnerOrganization
                                                    , SchDetdt.Rows[j]["BSMDDUID"].ToString(), SchDetdt.Rows[j]["Amount"].ToString()
                                                    , (NetAmount == "0" ? SchDetdt.Rows[j]["Amount"].ToString() : NetAmount), SchDetdt.Rows[j]["BillableItemUID"].ToString()
                                                    , SchDetdt.Rows[j]["Quantity"].ToString(), "null", "null", SchDetdt.Rows[j]["Comments"].ToString()
                                                    , "null", "null", packuid, "'PATIENTORDER'", patorderuid, patorddetails, "Raised");


                                                //db.Insert_PatientBillableItem(SchDetdt.Rows[j]["UID"].ToString()
                                                //                                        , packuid, patorddetails, "ORDITEM", patorderuid, "Raised", OwnerOrganization);

                                            }


                                        }//if (flaginsertPatientOrder)


                                    }//if (catdt.Rows[i]["Value"].ToString().ToLower() == SchDetdt.Rows[j][""].ToString())

                                }//for (int j = 0; j < SchDetdt.Rows.Count; j++)


                            }//for (int i = 0; i < catdt.Rows.Count; i++)

                        }

                    }//for (int x = 0; x < Schdt.Rows.Count; x++)  


                }//if (Schdt !=null && Schdt.Rows.Count >0 )


            }//if (Schdt != null && Schdt.Rows.Count > 0)





        }       
        string GenerateSequenEachType(string ordercattype)
        {
            string seq = "nodata";
            if (ordercattype == "lab")
            {
                string sreq = db.SEQID("SEQRequest");
                sreq =OwnerOrganization + "0" +  sreq.PadLeft(6, '0');
                return sreq;
            }
            if (ordercattype == "xray")
            {
                string sreq = db.SEQID("SEQRISRequestID");
                return sreq;

            }
            //if (ordercattype == "medicine")
            //{
            //    return seq;
            //}



            return seq;
        }
        bool CheckBillableMedicineItemTypeInsidePackage(DataTable pkdt)
        {
            bool flag = false;

            if (pkdt != null && pkdt.Rows.Count > 0)
            {
                for (int i = 0; i < pkdt.Rows.Count; i++)
                {
                    if (pkdt.Rows[i]["BillableType"].ToString().ToLower() == "medicine" || pkdt.Rows[i]["BillableType"].ToString().ToLower() == "supply")
                    {
                        flag = true;
                        return true;
                    }
                }
            }
            return flag;
        }      
        bool CheckMedicineItemTypeInsidePackage(DataTable pkdt)
        {
            bool flag = false;

            if (pkdt != null && pkdt.Rows.Count > 0)
            {
                for (int i = 0; i < pkdt.Rows.Count; i++)
                {
                    if (pkdt.Rows[i]["Description"].ToString().ToLower() == "medicine")
                    {
                        flag = true;
                        return true;
                    }
                }
            }
            return flag;
        }
        bool CheckLabItemTypeInsidePackage(DataTable pkdt,out string value)
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
        
        void CheckCareproviderUID(string login)
        {
            DataTable ldt= db.Select_Login_by_LoginName(login);
            if (ldt != null && ldt.Rows.Count > 0)
            {
                Cuser = ldt.Rows[0]["CareproviderUID"].ToString();
            }
        }            
        private void button3_Click(object sender, EventArgs e)
        {
            int PrnNo = 1;
            string PrnValue = "1";
            if (InputBox("Print Sticker Lab", "Number of copies : ", ref PrnValue) == DialogResult.OK)
            {
                PrnNo = Convert.ToInt32(PrnValue);
            }
            progressBar1.Maximum = Convert.ToInt32(gvPatient.Rows.Count);
            progressBar1.Value = 0;
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
                        progressBar1.Value += 1;
                        printSticker(dt.Rows[0]["ScheduleOrderNumber"].ToString(), PrnNo);
                    }
                }
            }
            #region OldCode
//            DataTable mdt= db.Select_BDMSASTMassConvert_Between(textBox2.Text, textBox3.Text);
//            if (mdt != null && mdt.Rows.Count > 0)
//            {
//                for (int i = 0; i < mdt.Rows.Count; i++)
//                {

//                    if (mdt.Rows[i]["SpecimenItemList"].ToString() != string.Empty)
//                    {
//                        var items= mdt.Rows[i]["SpecimenItemList"].ToString().Split('|').ToList();
//                        foreach (var SpecimenName in items)
//                        {
//                            if (SpecimenName != "")//Check Specimen not empty
//                            {
//                                string specimenNumber = db.SEQID("SEQSpecimen");
//                                //string s = "SIZE 101.6 mm, 25 mm\r\nGAP 3 mm, 0 mm\r\nSPEED 2\r\nDENSITY 15\r\nDIRECTION 0,0\r\nREFERENCE 0,0\r\nOFFSET 0 mm\r\nSHIFT 0\r\nSET PEEL OFF\r\nSET CUTTER OFF\r\nSET TEAR ON\r\nCLS\r\nCODEPAGE 850\r\nTEXT 616,177," + '"' + 3 + '"' + ",180,1,1," + '"' + resultArray.PatientName + '"' + "\r\nBARCODE 577,135," + '"' + "128M" + '"' + ",77,0,180,3,6," + '"' + specimenNumber + '"' + "\r\nTEXT 340,49," + '"' + 3 + '"' + ",180,1,1," + '"' + distinctElement.SpecimenName + '"' + "\r\nTEXT 579,49," + '"' + 3 + '"' + ",180,1,1," + '"' + resultArray.PatientID + '"' + "\r\nPRINT 1,1\r\n";
//                               // string s = "SIZE 101.6 mm, 25 mm\r\nGAP 3 mm, 0 mm\r\nSPEED 2\r\nDENSITY 15\r\nDIRECTION 0,0\r\nREFERENCE 0,0\r\nOFFSET 0 mm\r\nSHIFT 0\r\nSET PEEL OFF\r\nSET CUTTER OFF\r\nSET TEAR ON\r\nCLS\r\nCODEPAGE 850\r\nTEXT 616,177," + '"' + 3 + '"'   + ",180,1,1," + '"'+ mdt.Rows[i]["PatientFullName"].ToString() + '"'+ "\r\nBARCODE 577,135," + '"' + "128M" + '"' + ",77,0,180,3,6,"+ '"' + specimenNumber + '"' + "\r\nTEXT 340,49," + '"' + 3 + '"' + ",180,1,1," + '"'+ SpecimenName + '"' + "\r\nTEXT 579,49," + '"' + 3 + '"' + ",180,1,1," + '"' + mdt.Rows[i]["HN"].ToString() + '"'+ "\r\nPRINT 1,1\r\n";
//                               // string printerName = "Bar Code Printer T-9650 Plus";

//                                //^XA
//                                //^MMT
//                                //^PW406
//                                //^LL0203
//                                //^LS0
//                                //^BY3,3,68^FT42,74^BCN,,N,N
//                                //^FD>;@LabNo^FS
//                                //^A@N,30,30,E:ANGSANA.FNT
//                                //^FT42,148
//                                //^A0N,20,19^FH\^FD@TestSetList^FS
//                                //^FT38,105
//                                //^A@N,35,35^FD PT @PATFULL^FS
//                                //^FT298,147^A0N,25,24^FH\^FD@SPECIMEN^FS
//                                //^FT42,125^A0N,20,19^FH\^FDDOB. @DOB^FS
//                                //^FT397,148^A@B,32,31^FH\^FDHN.@HN^FS
//                                //^FT33,130^A0B,25,24^FH\^FD091200023^FS
//                                //^PQ1,0,1,Y^XZ";

//                                string command = @"
//                                ^XA
//                                ^MMT
//                                ^PW406
//                                ^LL0203
//                                ^LS0
//                                ^BY3,3,68^FT42,74^BCN,,N,N
//                                ^FD>;@LabNo^FS
//                                ^A@N,30,30,E:ANGSANA.FNT
//                                ^FT42,148
//                                ^A0N,20,19^FH\^FD@TestSetList^FS
//                                ^FT38,105
//                                ^A@N,35,35^FD PT @PATFULL^FS
//                                ^FT298,127^A0N,25,24^FH\^FD@SPECIMEN^FS
//                                ^FT42,125^A0N,20,19^FH\^FDDOB. @DOB^FS
//                                ^FT397,148^A@B,32,31^FH\^FDHN.@HN^FS
//                                ^FT33,130^A0B,25,24^FH\^FD@LabNo^FS
//                                ^PQ1,0,1,Y^XZ";

//                                command = command.Replace("@LabNo", mdt.Rows[i]["LabNo"].ToString());
//                                command = command.Replace("@HN", mdt.Rows[i]["HN"].ToString());

//                                if (mdt.Rows[i]["HN"].ToString() != "")
//                                { 
//                                 DataTable Patdt= db.Select_Patient_By_HN(mdt.Rows[i]["HN"].ToString());
//                                 if (Patdt != null && Patdt.Rows.Count > 0)
//                                 {
//                                     try
//                                     {
//                                         string bd = Convert.ToDateTime(Patdt.Rows[0]["BirthDttm"].ToString()).ToString("dd MMM yyyy");
//                                         string yy = Convert.ToDateTime(Patdt.Rows[0]["BirthDttm"].ToString()).ToString("yyyy");
                                         
//                                         command = command.Replace("@DOB", bd + " (" + (int.Parse(yy)+543).ToString() + ")");
//                                     }
//                                     catch (Exception er)
//                                     {
//                                     command = command.Replace("@DOB", "");    
//                                     }
                                     
//                                 }
//                                 else
//                                 {
//                                     command = command.Replace("@DOB", "");

//                                 }//if (Patdt != null && Patdt.Rows.Count > 0)
//                                }
//                                command = command.Replace("@PATFULL", mdt.Rows[i]["PatientFullName"].ToString());
//                                command = command.Replace("@SPECIMEN", SpecimenName);


//                                DataTable spdt =  db.Select_BDMSASTMAssSpecimenTestset_BY_HN_SPECIMEN(mdt.Rows[i]["HN"].ToString(), SpecimenName);
//                                string tslst = string.Empty;
//                                if (spdt != null && spdt.Rows.Count > 0)
//                                {
//                                    tslst = spdt.Rows[0]["TestSet"].ToString();
//                                }

//                                if (tslst != string.Empty && tslst.Length > 0)
//                                    tslst = tslst.Substring(0, tslst.Length - 1);
                                

//                                command = command.Replace("@TestSetList", tslst);

//                                string printerName = "Zebra  TLP2844";
//                                //ZDesigner LP 2844-Z (Copy 1)
//                                RawPrinterHelper.SendStringToPrinter(printerName, command);

//                            }//if (item != "")
//                        }
                        
                        
//                    }

//                }//for (int i = 0; i < mdt.Rows.Count; i++)

//            }//if (mdt != null && mdt.Rows.Count > 0)
    #endregion OldCode
        }
        private void cboPayor_SelectedIndexChanged(object sender, EventArgs e)
        {


            DataTable adt = db.Select_Agreement(((DataRowView)cboPayor.SelectedItem).Row.ItemArray[0].ToString());
            cboAgreement.DataSource = adt;
            cboAgreement.DisplayMember = "Name";
            cboAgreement.ValueMember = "UID";
            cboAgreement.ValueMember = "ClaimPercentage";


            DataTable pyfdt = db.Select_PayorDetail(((DataRowView)cboPayor.SelectedItem).Row.ItemArray[0].ToString());
            cboPayorOffice.DataSource = pyfdt;
            cboPayorOffice.DisplayMember = "PayorName";
            cboPayorOffice.ValueMember = "UID";


            DataTable pofdt = db.Select_Policy(((DataRowView)cboPayor.SelectedItem).Row.ItemArray[0].ToString());
            cboPolicy.DataSource = pofdt;
            cboPolicy.DisplayMember = "PolicyName";
            cboPolicy.ValueMember = "UID";

            
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string test = db.Insert_PatientVisitPayor( "1", "1", ((DataRowView)cboPayorOffice.SelectedItem).Row.ItemArray[0].ToString(), ((DataRowView)cboPolicy.SelectedItem).Row.ItemArray[0].ToString(),
                ((DataRowView)cboPolicy.SelectedItem).Row.ItemArray[1].ToString(), "", "MassCovert", Cuser, "A", OwnerOrganization, ((DataRowView)cboPayor.SelectedItem).Row.ItemArray[0].ToString(),
                ((DataRowView)cboAgreement.SelectedItem).Row.ItemArray[0].ToString(), "", "", ((DataRowView)cboAgreement.SelectedItem).Row.ItemArray[2].ToString(), "0");

            MessageBox.Show(test);
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox3.Text = textBox2.Text;
        }
        private void btBrows_Click(object sender, EventArgs e)
        {
            //Filter ชนิดของไฟล์ที่สามารถ Upload ได้
            openFileDialog1.Filter = "All Exel Files|*.xl*|Exel 2003 Files|*.xls|Exel 2010 Files|*.xlsx";
            openFileDialog1.ShowDialog();

            txtFileLoad.Text = openFileDialog1.FileName;
        }
        private void btLoad_Click(object sender, EventArgs e)
        {
            //เปิดไฟล์ Excel
            Excel.Application xlsApp = new Excel.Application();
            Excel.Workbook xlsWorkBook = null;
            Excel.Worksheet xlsWorkSheet = null;
            string FileName = txtFileLoad.Text;
            string str = string.Empty;
            string TmpValue = string.Empty;
            xlsWorkBook = xlsApp.Workbooks.Open(FileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlsWorkSheet = (Excel.Worksheet)xlsWorkBook.Worksheets.get_Item(1);
            object missing = System.Reflection.Missing.Value;
            Excel.Range xlsRange = xlsWorkSheet.UsedRange;
            int rowCount = xlsRange.Rows.Count;
            int colCount = xlsRange.Columns.Count;

            //Create datatable
            DataTable dtPatient = new DataTable();
            dtPatient.Columns.Add("Name",typeof(string));
            dtPatient.Columns.Add("LastName", typeof(string));
            dtPatient.Columns.Add("DOE", typeof(DateTime));

            //Create datarow
            DataRow drPatient;

            string DOE = string.Empty;
            //วนลูปอ่านค่าแต่ละแถวแล้วบันทึกลงใน Datatable
            for (int i = 1; i <= rowCount; i++)
            {
                drPatient = dtPatient.NewRow();

                //กำหนดให้เริ่มอ่านแถวที่ 2 เป็นต้นไป
                if (i >= 2)
                {        
                            //ถ้าอ่านแล้วฟิล์ดนั้นต้องไม่ใช่ค่าว่าง
                            try
                            {
                                drPatient[0]  = (string)(xlsRange.Cells[i, 2] as Excel.Range).Value2.ToString();
                                drPatient[1] = (string)(xlsRange.Cells[i, 4] as Excel.Range).Value2.ToString();
                                DOE = (string)(xlsRange.Cells[i, 7] as Excel.Range).Value2;
                                drPatient[2] = Convert.ToDateTime(DOE);
                               
                            }
                            catch (Exception ex)
                            {
                                //drPatient[0] = "";
                                //drPatient[1] = "";
                                //drPatient[2] = null;
                                ex.ToString();
                                MessageBox.Show("ไม่สามารถอ่านค่าจากไฟล์ Excel ได้ " + ex.ToString());
                            }
                            dtPatient.Rows.Add(drPatient);
                }
            }
            gvPatient.DataSource = dtPatient;
            gvPatient.Refresh();
            lblCountPT.Text = dtPatient.Rows.Count.ToString();
            //Close file excel
            xlsWorkBook.Close(false, missing, missing);
            xlsApp.Quit();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsApp);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsWorkBook);

            xlsWorkSheet = null;
            xlsWorkBook = null;
        }
        private void cboPayor_DropDown(object sender, EventArgs e)
        {
            DataTable pdt = db.Select_InsuranceCompany();
            if (pdt != null && pdt.Rows.Count > 0)
            {
                cboPayor.DataSource = pdt;
                cboPayor.DisplayMember = "CompanyName";
                cboPayor.ValueMember = "UID";
            }
        }

        private void printSticker(string OrderNo, int PrnNo)
        {
            //หาค่ามูล Lab Episode และ Patient Info ใน table BDMSASTMassConvert
            DataTable mdt = db.Select_BDMSASTMassConvert_Between(OrderNo, OrderNo);
            if (mdt != null && mdt.Rows.Count > 0)
            {
                for (int i = 0; i < PrnNo; i++)
                {   //ถ้ามี Lab Number
                    if (mdt.Rows[0]["LabNo"].ToString() != string.Empty)
                    {
                        string LNA = mdt.Rows[0]["LabNo"].ToString().Substring(0, 8);
                        string LNB = mdt.Rows[0]["LabNo"].ToString().Substring(8, 1);
                        string command = @"
                                ^XA
                                ^MMT
                                ^PW406
                                ^LL0203
                                ^LS0
                                ^FT52,74^BY2,2,68^BCN,68,N,N,N^FD>;>8@LNA>6@LNB^FS
                                ^FT42,148^A0N,20,19^FH\^FD@TestSetList^FS
                                ^FT38,105^A@N,30,30,E:ANGSANA.FNT ^FD PT @PATFULL^FS
                                ^FT298,127^A0N,25,24^FH\^FD@SPECIMEN^FS
                                ^FT42,125^A0N,20,19^FH\^FDDOB. @DOB^FS
                                ^FT397,148^A@B,32,31^FH\^FDHN.@HN^FS
                                ^FT33,130^A0B,25,24^FH\^FD@LabNo^FS
                                ^PQ1,0,1,Y^XZ";

                        command = command.Replace("@LabNo", mdt.Rows[0]["LabNo"].ToString());
                        command = command.Replace("@LNA", LNA);
                        command = command.Replace("@LNB", LNB);
                        command = command.Replace("@HN", mdt.Rows[0]["HN"].ToString());
                        //นำ HN ไปหา DOB ใน table Patient
                        if (mdt.Rows[0]["HN"].ToString() != "")
                        {
                            DataTable Patdt = db.Select_Patient_By_HN(mdt.Rows[0]["HN"].ToString());
                            if (Patdt != null && Patdt.Rows.Count > 0)
                            {
                                try
                                {
                                    string bd = Convert.ToDateTime(Patdt.Rows[0]["BirthDttm"].ToString()).ToString("dd MMM yyyy");
                                    string yy = Convert.ToDateTime(Patdt.Rows[0]["BirthDttm"].ToString()).ToString("yyyy");

                                    command = command.Replace("@DOB", bd + " (" + (int.Parse(yy) + 543).ToString() + ")");
                                }
                                catch (Exception er)
                                {
                                    command = command.Replace("@DOB", "");
                                    er.ToString();
                                }

                            }
                            else
                            {
                                command = command.Replace("@DOB", "");

                            }
                        }
                        command = command.Replace("@PATFULL", mdt.Rows[0]["PatientFullName"].ToString());
                        command = command.Replace("@SPECIMEN", "");
                        command = command.Replace("@TestSetList", "");

                        //select default printer
                        PrintDocument prtDoc = new PrintDocument();
                        string printerName = prtDoc.PrinterSettings.PrinterName;
                        //Print to default printer
                        RawPrinterHelper.SendStringToPrinter(printerName, command);
                    }

                }

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
        private void lblGenLabNo_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;

            groupBox2.Visible = false;
            button1.Visible = false;
        }
        private void lblGenLab_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            button2.Visible = false;
            button3.Visible = false;

            groupBox2.Visible = true;
            button1.Visible = true;
        }
        private void btFind_Click(object sender, EventArgs e)
        {
            ExcData exc = new ExcData();

            string DateFrom = dtpDateFrom.Value.ToString("yyyy-MM-dd") + " " + dtpTimeFrom.Value.ToString("HH:mm");
            string DateTo = dtpDateTo.Value.ToString("yyyy-MM-dd") + " " + dtpTimeTo.Value.ToString("HH:mm");
                string SQL = string.Empty;
            if(lblGenLabNo.Checked == true)
            {
                SQL = "SELECT Forename as Name , Surname as LastName , DOE , [NO] , [ChildCompany],[STS] FROM [tblPatientList] WHERE DOE BETWEEN '" + DateFrom + "' AND '" + DateTo + "' ORDER BY NO";
            }
            else
            {
                SQL = "SELECT Forename as Name , Surname as LastName , DOE , [NO] , [ChildCompany],[STS] FROM [tblPatientList] WHERE DOE BETWEEN '" + DateFrom + "' AND '" + DateTo + "' AND STS in ('A','R') ORDER BY STS DESC , NO";
            }
            
            dtPatient = exc.data_Table(SQL);
            bs.DataSource = dtPatient;
            gvPatient.DataSource = bs;
            gvPatient.Refresh();

            lblCountPT.Text = dtPatient.Rows.Count.ToString();

            if (lblGenLabNo.Checked == true)
            {
                CheckAll();
            }
            else
            {
                CheckOnlyStatusRA();
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
        private void CheckOnlyStatusRA()
        {
            foreach (DataGridViewRow row in gvPatient.Rows)
            {
                if (row.Cells["STS"].Value.ToString() == "R")
                {
                    row.Cells["Check"].Value = true;
                }
                else if(row.Cells["STS"].Value.ToString() == "A")
                {
                    row.Cells["Check"].Value = false;
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

        private void txtFilterPayor_TextChanged(object sender, EventArgs e)
        {
            bs.Filter = string.Format("ChildCompany LIKE '%{0}%'", txtFilterPayor.Text.Trim());
            CheckOnlyStatusRA();
        }

    }  
}
