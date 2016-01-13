using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
namespace MassConvert.Database
{
    class SQL
    {
        clsTempData clsTempData = new clsTempData();
        string outMessage = "";
        SqlConnection con;
        //string path = @"Data Source=10.103.13.40;Initial Catalog=BNH-BConnect;User ID=osa;Password=osa";
         //string path = @"Data Source=ASTROBOY;Initial Catalog=Bconnecttest;User ID=sa;Password=njp9126";
        private string path=System.Configuration.ConfigurationManager.AppSettings["csBConnect"];
        public string Path
        {
            get { return path; }
            set { path = value; }
        }
        // string path = @"Data Source=10.103.10.81;Initial Catalog=BNH-BConnect-RPT;User ID=osa;Password=osa";
        //Data Source=10.103.13.40;Initial Catalog=BNH-BConnect;User ID=osa;Password=***********
        public SQL()
        {
            con = new SqlConnection(path);
        }
        void Connect()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
        }
        void Disconnect()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
        ExcData exc;

        //===========Select======================
        public DataTable Select_CareProvider()
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            string selectInsuranceCompany = @"select isnull(ForeName,'') + ' ' + isnull(MiddleName,'') + ' ' + isnull(Surname,'') FullName, * from Careprovider where Statusflag = 'A' and IsCareProvider='Y' order by ForeName asc";
            try
            {
                this.Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = selectInsuranceCompany;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                this.Disconnect();
            }

            return dt;
        }
        public DataTable Select_InsuranceCompany()
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            string selectInsuranceCompany = @"select  IC.UID,CompanyName from InsurancePlan IP 
                                            INNER JOIN InsuranceCompany IC
                                            ON IP.IdentifyingUID = IC.UID 
                                            WHERE IP.StatusFlag ='A'
                                            and IC.StatusFlag ='A'
                                            and (IP.Activeto is null Or IP.Activeto > GETDATE())
                                            and (IC.Activeto is null Or IC.Activeto > GETDATE())
                                            GROUP BY IP.IdentifyingUID,IC.UID,IC.CompanyName 
                                            ORDER BY CompanyName ASC;";
            try
            {
                this.Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = selectInsuranceCompany;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                this.Disconnect();
            }
      
            return dt;
        }
        public DataTable Select_PayorDetail(string uidInsuranceCompany)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            string selectPayorDetail = @"select UID,PayorName from PayorDetail where UID in (select PayorDetailUID from InsurancePlan where IdentifyingUID = " + uidInsuranceCompany + @")
                                       and StatusFlag ='A' ";
     
            try
            {
                this.Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = selectPayorDetail;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                this.Disconnect();
            }
    
            return dt;
        }
        public DataTable Select_PayorDetail(string uidInsuranceCompany,string uidPayorAgreement)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("SELECT ");
            strSQL.Append("A.PayorDetailUID UID,");
            strSQL.Append("B.PayorName ");
            strSQL.Append("FROM ");
            strSQL.Append("InsurancePlan A ");
            strSQL.Append("INNER JOIN PayorDetail B ON A.PayorDetailUID=B.UID AND B.StatusFlag='A' ");
            strSQL.Append("WHERE ");
            strSQL.Append("A.IdentifyingUID=" + uidInsuranceCompany + " ");
            strSQL.Append("AND A.PayorAgreementUID=" + uidPayorAgreement + " ");
            strSQL.Append("AND A.StatusFlag='A' ");
            strSQL.Append("AND (A.ActiveFrom IS NULL OR A.ActiveFrom <= GETDATE()) ");
            strSQL.Append("AND (A.ActiveTo IS NULL OR A.ActiveTo >= GETDATE());");
            try
            {
                this.Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                this.Disconnect();
            }

            return dt;
        }
        public DataTable Select_Agreement(string uidInsuranceCompany)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            string selectAgreement = @"select UID,Name/*,ClaimPercentage*/ from PayorAgreement where UID in (select PayorAgreementUID from InsurancePlan where IdentifyingUID = " + uidInsuranceCompany + @")
                                       and StatusFlag ='A' "+
                                       "and (ActiveFrom is null or ActiveFrom <= GETDATE()) "+
                                       "and (ActiveTo is null or ActiveTo >= GETDATE())";
            try
            {
                this.Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = selectAgreement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                this.Disconnect();
            }

            return dt;
        }
        public DataTable Select_Agreement_ClaimPercentage(string uidInsuranceCompany)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            string selectAgreement = @"select UID,ClaimPercentage from PayorAgreement where UID = " + uidInsuranceCompany + @"
                                       and StatusFlag ='A' and (ActiveTo is null or ActiveTo > GETDATE())";
            try
            {
                this.Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = selectAgreement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                this.Disconnect();
            }

            return dt;
        }
        public string Select_Agreement_By_UID_For_ClaimPercentage(string agreementUID)
        {
            DataTable dt = new DataTable();
            string ClaimPercentage = string.Empty;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select ClaimPercentage from PayorAgreement where UID = "+agreementUID+"";
                dt.Load(cmd.ExecuteReader());
                ClaimPercentage = dt.Rows[0]["ClaimPercentage"].ToString();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            return ClaimPercentage;
        }
        public DataTable Select_Policy(string uidInsuranceCompany)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            string selectPayorDetail = @"select UID,PolicyName from PolicyMaster where UID in (select PolicyMasterUID from InsurancePlan where IdentifyingUID = " + uidInsuranceCompany + @")
                                       and StatusFlag ='A' ";
            try
            {
                this.Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = selectPayorDetail;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                this.Disconnect();
            }

            return dt;
        }
        public DataTable Select_Policy(string uidInsuranceCompany,string uidPayorAgreement)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            StringBuilder strSQL = new StringBuilder();

            #region Old
            /*strSQL.Append("SELECT ");
            strSQL.Append("A.PolicyMasterUID UID,");
            strSQL.Append("B.PolicyName ");
            strSQL.Append("FROM ");
            strSQL.Append("InsurancePlan A ");
            strSQL.Append("INNER JOIN PolicyMaster B ON A.PolicyMasterUID=B.UID AND B.StatusFlag='A' ");
            strSQL.Append("WHERE ");
            strSQL.Append("A.IdentifyingUID=" + uidInsuranceCompany + " ");
            strSQL.Append("AND A.PayorAgreementUID=" + uidPayorAgreement + " ");
            strSQL.Append("AND A.StatusFlag='A' ");
            strSQL.Append("AND (A.ActiveFrom IS NULL OR A.ActiveFrom <= GETDATE()) ");
            strSQL.Append("AND (A.ActiveTo IS NULL OR A.ActiveTo >= GETDATE());");*/
            #endregion
            #region New
            strSQL.Append("SELECT ");
            strSQL.Append("B.UID,B.PolicyName ");
            strSQL.Append("FROM ");
            strSQL.Append("PayorAgreement A ");
            strSQL.Append("INNER JOIN PolicyMaster B ON A.PolicyMasterUID=B.UID AND B.StatusFlag='A' ");
            strSQL.Append("WHERE ");
            strSQL.Append("A.StatusFlag='A' ");
            strSQL.Append("AND (A.ActiveFrom IS NULL OR A.ActiveFrom <= GETDATE()) AND (A.ActiveTo IS NULL OR A.ActiveTo >= GETDATE()) ");
            strSQL.Append("AND A.InsuranceCompanyUID=" + uidInsuranceCompany + " ");
            strSQL.Append("AND A.UID=" + uidPayorAgreement + ";");
            #endregion

            try
            {
                this.Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                this.Disconnect();
            }

            return dt;
        }
        public DataTable Select_Location()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @"	
		select * from Location where  LOTYPUID in (select UID from ReferenceValue where Description = 'Department') order by Name asc
            ";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                er.ToString();
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_EncouterType()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @"select * from ReferenceValue where  (Description = 'Out Patient' or Description ='HealthPromotion') and DomainCode = 'ENTYP' and  StatusFlag = 'A' order by UID desc";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_BDMSASTMAssSpecimenTestset_BY_HN_SPECIMEN(string hn , string specimen)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @"select * from BDMSASTMAssSpecimenTestset where HN = '" + hn + "' and Specimen = '"  + specimen + "' ";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_BDMSASTMAssSpecimenTestset_BY_PatientScheduleOrderUID_And_TestSet_HN(string patschUID, string TS,string hn)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @"select * from BDMSASTMAssSpecimenTestset where HN = '" + hn + "' and PatientScheduleOrderUID = '" + patschUID + @"' 
            and TestSet like '%" + TS + "%' ";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_Specimen_By_Name(string spenme)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @"	
		select * from Specimen where CODE =@spcnme and Statusflag= 'A'";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@spcnme", spenme);
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_All_Location()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @"	
		select * from Location where Statusflag = 'A' order by Name asc ";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                er.ToString();
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
       public DataTable Select_Location_Condition(string condition)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @"	
		select * from Location where Description not in (" + condition + ") and Description = 'Execute' and Statusflag = 'A' order by Name asc ";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_Login_by_LoginName(string name)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @"	
		select top 1 * from Login where LoginName = '" + name + "' and statusflag = 'A' order by UID desc";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_Location_By_UID(string uid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @"	
		select * from Location where UID = '" + uid + "'";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_Lab_ByNo(string lab)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @"	
		select (select Description from ReferenceValue rv where rv.UID= LRQSTUID),LRQSTUID,* from Request where RequestNumber like '" + lab + "%'";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_Patient_By_UID(string uid)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"Select (select Description from ReferenceValue where ReferenceValue.UID = Patient.TITLEUID and ReferenceValue.Statusflag = 'A') TitleDesc,* from Patient where UID ='" + uid + "' ";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_Patient_By_HN(string pasid)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"Select TOP 1 * from Patient where pasid ='" + pasid + "'";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                //  //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_PatientPackage_By_VisitUID(string vuid)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"Select * from PatientPackage where PatientVisitUID ='" + vuid + "' and Statusflag = 'A' ";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_PatientVisit_By_UID(string uid)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"Select * from PatientVisit where PatientUID ='" + uid + "' ";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                ////System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_RequestItemSpecimen_By_RequestItemUID(string uid,string isdefault)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = string.Empty;
                if (isdefault=="Y")
                 //SQLstatement = @"select (select code from Specimen where Specimen.UID=  SpecimenUID) SpecimenCode,* from RequestItemSpecimen where RequestItemUID = '" + uid + "' and IsDFT ='" + isdefault + "' and StatusFlag = 'A' order by UID desc ";
                    SQLstatement = @"select (select code from Specimen where Specimen.UID=  SpecimenUID) SpecimenCode,* from RequestItemSpecimen where RequestItemUID = '" + uid + "' and IsDefault ='" + isdefault + "' and StatusFlag = 'A' order by UID desc ";
                else
                    SQLstatement = @"select (select code from Specimen where Specimen.UID=  SpecimenUID) SpecimenCode,* from RequestItemSpecimen where RequestItemUID = '" + uid + "'  and StatusFlag = 'A' order by UID desc ";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                ////System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_HistoryOrder(string patuid)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"
                select  	(select rv.Description from ReferenceValue rv where  rv.UID = ORDSTUID) OrderStatus,ORDSTUID
							,(select rv.Description from ReferenceValue rv where  rv.UID = ORDCTUID) OrderType,ORDCTUID
                            ,(select rv.NumericValue from ReferenceValue rv where  rv.UID = ORDCTUID) OrderTypeNumber,* from PatientOrder  
            where PatientUID = '" + patuid + "' and   StartDttm < '" + DateTime.Today.ToString("yyyy-MM-dd 00:00:00.000") + "' order by StartDttm desc ";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                ////System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_PatientID(string Episode)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"Select * from PatientVisitID where Identifier ='" + Episode + "' ";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                ////System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_PatientVistID_By_VisitUID(string Episode)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"Select * from PatientVisitID where PatientVisitUID ='" + Episode + "' ";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                ////System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_ReferenceValue(string uid)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"Select * from referenceValue  where UID ='" + uid + "' and statusflag = 'A' ";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                ////System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_ReferenceValue_By_DomainCode(string uid)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"Select * from referenceValue  where domaincode ='" + uid + "' and statusflag = 'A' ";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                ////System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_ReferenceValue_Domain_BSMDD_By_Description(string uid)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"Select * from referenceValue  where domaincode = 'BSMDD' and  description ='" + uid + "' and statusflag = 'A' ";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                ////System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_ReferenceValue_Domain_BSMDD_By_UID(string uid)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"Select * from referenceValue  where domaincode = 'BSMDD' and  UID ='" + uid + "' and statusflag = 'A' ";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                ////System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_OrderCategoryGroup(string ScheduleOrderNumber )
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"select  (select top 1 Description from referencevalue where domaincode = 'OrDCT' 
			                and statusflag = 'A' and ReferenceValue.UID =  OrderCategoryUID) 
                            from PatientScheduleOrder,PatientScheduleOrderDetail 
                            where PatientScheduleOrder.UID=PatientScheduleOrderDetail.PatientScheduleOrderUID 
							and ScheduleOrderNumber = '" + ScheduleOrderNumber + "' group by PatientScheduleOrderDetail.OrderCategoryUID";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_Billable_BSMDD()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"select * from ReferenceValue where UID in ( select distinct BSMDDUID from BillableItem where Statusflag = 'a') and Statusflag = 'A' order by Description asc";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_OrderToLocationUID(string Locfrom, string OrderCategoryUID ,string OrderSubCategoryUID)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                
                Connect();
                cmd.Connection = con;
                cmd.CommandText = "select dbo.BDMSASTLARIMEfGetLocationToUID(" + Locfrom + "," + OrderCategoryUID + "," + OrderSubCategoryUID + ")";
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_OrderToLocationUID(string Locfrom, string billableitemUID, string OrderSubCategoryUID, bool isnewconcept)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();

                Connect();
                cmd.Connection = con;
                cmd.CommandText = "select * from   [fBDMSGetOrderLocation]( " + billableitemUID + "," + Locfrom + ")";
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_PatientScheduleOrder(string ScheduleOrderNumber)
        {
            DataTable dt = new DataTable();
            clsSQLNative clsSQL = new clsSQLNative();
            dt = clsSQL.Bind("Select *,(select pasid from Patient where Patient.UID = PatientScheduleOrder.PatientUID and Patient.StatusFlag = 'A') HN from PatientScheduleOrder where ScheduleOrderNumber = '" + ScheduleOrderNumber + "' and statusflag = 'A'", 
                clsSQLNative.DBType.SQLServer, 
                "csBConnect");
            return dt;
        }
        public DataTable Select_PatientScheduleOrder_Old(string ScheduleOrderNumber)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"Select *,(select pasid from Patient where Patient.UID = PatientScheduleOrder.PatientUID and Patient.StatusFlag = 'A') HN from PatientScheduleOrder where ScheduleOrderNumber = '" + ScheduleOrderNumber + "' and statusflag = 'A'";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_PatientScheduleOrder_Between(string ScheduleOrderNumberFrom, string ScheduleOrderNumberTo)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"select UID,ScheduleOrderNumber,PatientUID,PatientVisitUID,* from PatientScheduleOrder where ScheduleOrderNumber between " + ScheduleOrderNumberFrom + " and  " + ScheduleOrderNumberTo + " and  statusflag = 'A'";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_BDMSASTMassConvert_Between(string ScheduleOrderNumberFrom, string ScheduleOrderNumberTo)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"select * from BDMSASTMassConvert where PatientScheduleOrderNo between " + ScheduleOrderNumberFrom + " and  " + ScheduleOrderNumberTo + " and  statusflag = 'A'";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_Request_By_LabNo(string labno)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"select * from request where RequestNumber='" + labno + "' and Statusflag = 'A' ";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_PatientScheduleOrderDetail_By_PatientScheduleOrderUID(string PatientScheduleOrderUID)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"select * from PatientScheduleOrderDetail where PatientScheduleOrderUID='" + PatientScheduleOrderUID + "' and Statusflag = 'A'";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_PatientScheduleOrderDetail_By_PatientScheduleOrderUID_For_CheckRequestItemOnly(string PatientScheduleOrderUID)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"select * from PatientScheduleOrderDetail where PatientScheduleOrderUID='" + PatientScheduleOrderUID + "' and IdentifyingType ='REQUESTITEM' and Statusflag = 'A'";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_PatientScheduleOrderDetail_By_PatientScheduleOrderUID_For_CheckBillPackageOnly(string PatientScheduleOrderUID)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"select * from PatientScheduleOrderDetail where PatientScheduleOrderUID='" + PatientScheduleOrderUID + "' and IdentifyingType ='BILLPACKAGE' and Statusflag = 'A'";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_RequestItem_By_UID_For_CheckItemCategory(string uid)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"select (select top 1 Description from ReferenceValue where UID= RequestItem.RICATUID and ReferenceValue.StatusFlag='A' ) ItemCat, * from RequestItem where uid = '" + uid + "' and Statusflag = 'A' ";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_OrderCategory_By_ScheduleOrderNumber_GroupBy_OrderCategoryUID(string PatientScheduleOrderUID)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"
            select (select top 1 Description from referencevalue where domaincode = 'OrDCT' 
 and statusflag = 'A' and ReferenceValue.UID =  b.ORDCTUID) Description,
 (select top 1 UID from referencevalue where domaincode = 'OrDCT' 
 and statusflag = 'A' and ReferenceValue.UID =  b.ORDCTUID) Value 
 from BillPackageItem a inner join BillableItem b on a.BillableItemUID = b.UID where BillPackageUID in (
 select BillPackageUID from BDMSPatientScheduleOrderDetail where PatientScheduleOrderUID = '" + PatientScheduleOrderUID + @"' and StatusFlag = 'A'
 ) and a.Statusflag = 'A' and (a.Activeto is null or a.Activeto >= GETDATE())  group by b.ORDCTUID";
//                string SQLstatement = @"
//            select (select top 1 Description from referencevalue where domaincode = 'OrDCT' 
// and statusflag = 'A' and ReferenceValue.UID =  b.ORDCTUID) Description,
// (select top 1 UID from referencevalue where domaincode = 'OrDCT' 
// and statusflag = 'A' and ReferenceValue.UID =  b.ORDCTUID) Value 
// from BillPackageItem a inner join BillableItem b on a.BillableItemUID = b.UID where BillPackageUID = (
// select IdentifyingUID from PatientScheduleOrderDetail where PatientScheduleOrderUID = '" + PatientScheduleOrderUID + @"' and 
// PatientScheduleOrderDetail.IdentifyingType = 'BILLPACKAGE'
// ) and a.Statusflag = 'A' and (a.Activeto is null or a.Activeto >= GETDATE())  group by b.ORDCTUID";
//                string SQLstatement = @"
//            select (select top 1 Description from referencevalue where domaincode = 'OrDCT' 
//			and statusflag = 'A' and ReferenceValue.UID =  ORDCTUID) Description,
//                (select top 1 UID from referencevalue where domaincode = 'OrDCT' 
//			and statusflag = 'A' and ReferenceValue.UID =  ORDCTUID) Value from BillPackageItem where BillPackageUID = (
//			   select IdentifyingUID from PatientScheduleOrderDetail where PatientScheduleOrderUID = '" + PatientScheduleOrderUID + @"' and 
//			    PatientScheduleOrderDetail.IdentifyingType = 'BILLPACKAGE'
//			   ) and BillPackageItem.Statusflag = 'A' and (Activeto is null or Activeto >= GETDATE())  group by ORDCTUID";

                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_BillableItem_By_ScheduleOrderNumber_GroupBy_BSMDDUID(string PatientScheduleOrderUID)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"
             select distinct (select top 1 Description from referencevalue where domaincode = 'BSMDD' 
 and statusflag = 'A' and ReferenceValue.UID =  b.BSMDDUID and ReferenceValue.Statusflag='A') BillableType,
 (select top 1 UID from referencevalue where domaincode = 'OrDCT' 
 and statusflag = 'A' and ReferenceValue.UID =  b.ORDCTUID) Value from BillPackageItem a inner join BillableItem b on a.BillableItemUID = b.UID
  where BillPackageUID in (
 select BillPackageUID from BDMSPatientScheduleOrderDetail where PatientScheduleOrderUID = '" + PatientScheduleOrderUID + @"' and StatusFlag = 'A'
 ) and a.Statusflag = 'A' and (a.Activeto is null or a.Activeto >= GETDATE()) ";
//                string SQLstatement = @"
//            select distinct (select top 1 Description from referencevalue where domaincode = 'BSMDD' 
// and statusflag = 'A' and ReferenceValue.UID =  b.BSMDDUID and ReferenceValue.Statusflag='A') BillableType,
// (select top 1 UID from referencevalue where domaincode = 'OrDCT' 
// and statusflag = 'A' and ReferenceValue.UID =  b.ORDCTUID) Value from BillPackageItem a inner join BillableItem b on a.BillableItemUID = b.UID
//  where BillPackageUID = (
// select IdentifyingUID from PatientScheduleOrderDetail where PatientScheduleOrderUID = '" + PatientScheduleOrderUID + @"' and 
// PatientScheduleOrderDetail.IdentifyingType = 'BILLPACKAGE'
// ) and a.Statusflag = 'A' and (a.Activeto is null or a.Activeto >= GETDATE()) ";
//                string SQLstatement = @"
//            select distinct (select top 1 Description from referencevalue where domaincode = 'BSMDD' 
//			and statusflag = 'A' and ReferenceValue.UID =  BSMDDUID and ReferenceValue.Statusflag='A') BillableType,
//                (select top 1 UID from referencevalue where domaincode = 'OrDCT' 
//			and statusflag = 'A' and ReferenceValue.UID =  ORDCTUID) Value from BillPackageItem where BillPackageUID = (
//			   select IdentifyingUID from PatientScheduleOrderDetail where PatientScheduleOrderUID = '" + PatientScheduleOrderUID + @"' and 
//			    PatientScheduleOrderDetail.IdentifyingType = 'BILLPACKAGE'
//			   ) and BillPackageItem.Statusflag = 'A' and (Activeto is null or Activeto >= GETDATE())  ";

                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_OrderCategory_By_ScheduleOrderNumber(string PatientScheduleOrderUID)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"
            select  (select top 1 Description from referencevalue where domaincode = 'OrDCT' 
			and statusflag = 'A' and ReferenceValue.UID =  OrderCategoryUID) Description
			,(select top 1 UID from referencevalue where domaincode = 'OrDCT' 
			and statusflag = 'A' and ReferenceValue.UID =  OrderCategoryUID) Value
            from PatientScheduleOrder,PatientScheduleOrderDetail where 
            PatientScheduleOrder.UID=PatientScheduleOrderDetail.PatientScheduleOrderUID 
		    and PatientScheduleOrder.UID = '" + PatientScheduleOrderUID + "' group by PatientScheduleOrderDetail.OrderCategoryUID";

                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_OrderCategory_By_ScheduleOrderNumber_Filter_Package_Only(string PatientScheduleOrderUID)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"
               select (select top 1 Description from referencevalue where domaincode = 'OrDCT' 
			and statusflag = 'A' and ReferenceValue.UID =  ORDCTUID) Description,
                (select top 1 UID from referencevalue where domaincode = 'OrDCT' 
			and statusflag = 'A' and ReferenceValue.UID =  ORDCTUID) Value,* from BillPackageItem where BillPackageUID = (
			   select IdentifyingUID from PatientScheduleOrderDetail where PatientScheduleOrderUID = '" + PatientScheduleOrderUID + @"' and 
			    PatientScheduleOrderDetail.IdentifyingType = 'BILLPACKAGE'
			   ) and BillPackageItem.Statusflag = 'A' and (Activeto is null or Activeto >= GETDATE()) ";

                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_OrderCategory_By_ScheduleOrderNumber_Filter_Package_Only(string PatientScheduleOrderUID,string cat)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"
               select (select top 1 Description from referencevalue where domaincode = 'OrDCT' 
			and statusflag = 'A' and ReferenceValue.UID =  ORDCTUID) Description,
                (select top 1 ItemCode from REquestitem where REquestitem.UID = ItemUID) ItemCode,
			(select top 1 ItemDescription from REquestitem where REquestitem.UID = ItemUID) ItemDescription,
                (select top 1 UID from referencevalue where domaincode = 'OrDCT' 
			and statusflag = 'A' and ReferenceValue.UID =  ORDCTUID) Value,* from BillPackageItem where BillPackageUID = (
			   select IdentifyingUID from PatientScheduleOrderDetail where PatientScheduleOrderUID = '" + PatientScheduleOrderUID + @"' and 
			    PatientScheduleOrderDetail.IdentifyingType = 'BILLPACKAGE'
			   ) and BillPackageItem.Statusflag = 'A' and (Activeto is null or Activeto >= GETDATE()) and (select top 1 Description from referencevalue where domaincode = 'OrDCT' 
			and statusflag = 'A' and ReferenceValue.UID =  ORDCTUID)='" + cat + "' ";

                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_OrderCategory_By_ScheduleOrderNumber_Filter_Package_Only_Lab_And_Xray(string PatientScheduleOrderUID)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"
               select (select top 1 Description from referencevalue where domaincode = 'OrDCT' 
			and statusflag = 'A' and ReferenceValue.UID =  ORDCTUID) Description,
                (select top 1 UID from referencevalue where domaincode = 'OrDCT' 
			and statusflag = 'A' and ReferenceValue.UID =  ORDCTUID) Value,* from BillPackageItem where BillPackageUID = (
			   select IdentifyingUID from PatientScheduleOrderDetail where PatientScheduleOrderUID = '" + PatientScheduleOrderUID + @"' and 
			    PatientScheduleOrderDetail.IdentifyingType = 'BILLPACKAGE'
			   ) and BillPackageItem.Statusflag = 'A' and (Activeto is null or Activeto >= GETDATE()) and (select top 1 Description from referencevalue where domaincode = 'OrDCT' 
			and statusflag = 'A' and ReferenceValue.UID =  ORDCTUID) in('Lab','Xray') ";

                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_OrderCategory_By_ScheduleOrderNumber_Filter_Package_AllItem(string PatientScheduleOrderUID)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @" select (select top 1 Description from referencevalue where domaincode = 'OrDCT'  and statusflag = 'A' and ReferenceValue.UID =  b.ORDCTUID) Description,
 (select top 1 UID from referencevalue where domaincode = 'OrDCT'  and statusflag = 'A' and ReferenceValue.UID =  b.ORDCTUID) Value
 ,(select top 1 ItemCode from RequestItem where RequestItem.UID = b.ItemUID and RequestItem.Statusflag = 'A') RequestCode
 ,b.ORDCTUID , b.OrderSubCategoryUID , a.BillableItemUID ,a.UID , a.Quantity ,a.Amount ,b.ItemUID ,b.ItemName Comments  ,b.BSMDDUID 
  from BillPackageItem a inner join BillableItem b on a.BillableItemUID = b.UID  
  where BillPackageUID in (select BillPackageUID from BDMSPatientScheduleOrderDetail where PatientScheduleOrderUID = '" + PatientScheduleOrderUID + @"' and Statusflag = 'A')
  and a.Statusflag = 'A' and (a.Activeto is null or a.Activeto >= GETDATE())  ";

//                string SQLstatement = @"
//               select (select top 1 Description from referencevalue where domaincode = 'OrDCT' 
//			and statusflag = 'A' and ReferenceValue.UID =  ORDCTUID) Description,
//                (select top 1 UID from referencevalue where domaincode = 'OrDCT' 
//			and statusflag = 'A' and ReferenceValue.UID =  ORDCTUID) Value
//                ,(select top 1 ItemCode from RequestItem where RequestItem.UID = BillPackageItem.ItemUID and RequestItem.Statusflag = 'A') RequestCode
//                ,* from BillPackageItem where BillPackageUID = (
//			   select IdentifyingUID from PatientScheduleOrderDetail where PatientScheduleOrderUID = '" + PatientScheduleOrderUID + @"' and 
//			    PatientScheduleOrderDetail.IdentifyingType = 'BILLPACKAGE'
//			   ) and BillPackageItem.Statusflag = 'A' and (Activeto is null or Activeto >= GETDATE())  ";

                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_BillPackage_By_UID(string uid)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"select * from BillPackage where UID = '" + uid + "' and Statusflag = 'A'";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_OtherEvent_by_PatientVisitUID_And_IdentifyType(string patvisituid, string Idenuid, string identifytype = "RESULT")
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"select * from OtherEvent where PatientVisitUID = '" + patvisituid + "' and IdentifyingType = '" + identifytype + "' and IdentifyingUID = '" + Idenuid + "' and statusflag = 'A'";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_Frequency()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"Select * from FrequencyDefinition where statusflag = 'A' order by uid asc";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_Instruction_By_RefValue()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"Select * from ReferenceValue where statusflag = 'A' and DomainCode='PDSTS' order by uid asc";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_Frequency_by_UID(string uid)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"Select * from FrequencyDefinition where statusflag = 'A' and UID= '" + uid + "' order by uid asc";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_Priority()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"Select * from ReferenceValue where statusflag = 'A' and DomainCode = 'RQPRT'";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_Order_Status()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"Select * from ReferenceValue where statusflag = 'A' and DomainCode = 'ORDST'";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_Route()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"Select * from ReferenceValue where statusflag = 'A' and DomainCode = 'ROUTE'";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_DrugCatalog(string duid)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"Select * from DrugCatalogItem where UID = '" + duid + "'";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_DrugTradeName_By_DrugCatalogItemUID(string druguid)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"Select * from DrugTradename where DrugCatalogItemUID = '" + druguid + "'";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_ItemMaster_By_UID(string itemuid)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"Select * from ItemMaster where UID = '" + itemuid + "'";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_Store_By_UID(string uid)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string SQLstatement = @"Select * from Store where UID ='" + uid + "' ";
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLstatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {

                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;

        }
        public DataTable Select_Order_ByEpisode(string epi)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @"	
		                    select IdentifyingType,OrderNumber,UID
							,(select rv.Description from ReferenceValue rv where  rv.UID = ORDSTUID) OrderStatus,ORDSTUID
							,(select rv.Description from ReferenceValue rv where  rv.UID = ORDCTUID) OrderType,ORDCTUID
                            ,(select rv.NumericValue from ReferenceValue rv where  rv.UID = ORDCTUID) OrderTypeNumber
							, * from PatientOrder where PatientVisitUID 
							in ( select PatientVisitUID from PatientVisitID where Identifier  = '" + epi + "')";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_BillableItem_by_Description(string desc, string bsmdd = "")
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = string.Empty;

            if (bsmdd != "")
                SQLStatement = @"select * from BillableItem where BSMDDUID in (" + bsmdd + ") and (itemname like N'" + desc + "%' or Description like N'" + desc + "%') and StatusFlag='A' order by itemname asc ";
            else
                SQLStatement = @"select * from BillableItem where  itemname like N'" + desc + "%' or Description like N'" + desc + "%' order by itemname asc ";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_BillPackage_by_Description(string desc)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @"select * from BillPackage where PackageName like N'" + desc + "%' or Description like N'" + desc + "%'";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_BillPackageItem_by_BillPackageUID(string uid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @"select * from BillPackageItem where BillPackageUID = '" + uid + "'";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_RequestItem(string uid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();



            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = "Select * from RequestItem where UID = '" + uid + "'";
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_ResultItem_By_Code(string code)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();



            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = "select top 1 * from ResultItem where Code = '" + code + "' and Statusflag = 'A'";
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_LocationStore(string category, string subcate)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();



            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = "Select distinct ServiceName from LocationStore where ORDCTUID = '" + category + "' and OrderSubCategoryUID = '" + subcate + "'";
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_BillableItem_by_Description_Contain(string desc, string bsmdd = "")
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();


            string SQLStatement = string.Empty;
            if (bsmdd != "")
                SQLStatement = @"select * from BillableItem where BSMDDUID in (" + bsmdd + ") and (itemname like N'%" + desc + "%' or Description like N'%" + desc + "%') and StatusFlag='A' order by itemname asc ";
            else
                SQLStatement = @"select * from BillableItem where itemname like N'%" + desc + "%' or Description like N'%" + desc + "%' order by itemname asc ";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_BillPackage_by_Description_Contain(string desc)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();

            string SQLStatement = string.Empty;

            SQLStatement = @"select * from BillPackage where PackageName like N'%" + desc + "%' or Description like N'%" + desc + "%'";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_PatientOrder_By_OrderNumber(string orderno)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @" select * from PatientOrder where OrderNumber = '" + orderno + "'";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_PatientBillableitem(string patvisituid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @" select * from PatientBillableItem where PatientVisitUID = '" + patvisituid + "' and Statusflag = 'A'";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_PatientBillableitem_By_VisitUID_And_PackageUID(string patvisituid, string packuid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @" select BillableItemUID, sum(ItemMultiplier) qtyitem from PatientBillableItem where PatientVisitUID = '" + patvisituid + "' and BillPackageUID = '" + packuid + "' and IdentifyingType <> 'BillPackage' and Statusflag = 'A' group by BillableItemUID,ItemMultiplier";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_PatientBillableitem_By_OrderDetail_UID(string orderuid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @" select * from PatientBillableItem where PatientOrderDetailUID = '" + orderuid + "' and Statusflag = 'A'";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_PatientOrderDetail_By_PatientOrderUID(string patientorderuid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @" select * from PatientOrderDetail where PatientOrderUID in (" + patientorderuid + ")";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_PatientOrderDetail_By_UID(string patientorderuid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @" select * from PatientOrderDetail where UID in (" + patientorderuid + ")";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_ItemPaidStatus_By_PatientOrderDetailUID(string patdetuid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @"
              select * from PatientBilledItem where PatientBillableItemUID in 
              (select UID from PatientBillableItem where PatientOrderDetailUID
              in (select  UID from PatientOrderDetail where PatientOrderUID ='" + patdetuid + "'))  ";

            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_PatientOrderDetail_By_PatientVisitUID(string patientvisituid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @" select * from PatientOrderDetail where PatientOrderUID in (select UID from PatientOrder where  PatientVisitUID = '" + patientvisituid + "') order by MWhen desc ";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_Rquest_By_Date(DateTime dtereq)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            //2012-12-08 00:00:00.000
            string SQLStatement = @"	
		select (select Description from ReferenceValue rv where rv.UID= LRQSTUID) LabStatus,LRQSTUID,(select top 1 Pasid + '^' + ' ' + forename + '  ' + surname  from Patient p where p.UID= PatientUID) PatientNo,* from Request where RequestedDttm >= '" + dtereq.ToString("yyyy-MM-dd 00:00:00.000") + "' and RequestedDttm <= '" + dtereq.AddDays(1).ToString("yyyy-MM-dd 00:00:00.000") + "' and RequestNumber like '09%' and LRQSTUID <> 4412 order by UID asc";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_Result_Not_Gen(DateTime dtereq)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            //2012-12-08 00:00:00.000
            string SQLStatement = @"	
		select (select UID from HIMessageQueue hq where hq.UID= HIMessageUID  ) HUID,(select IdentifyingType from HIMessageQueue hq where hq.UID= HIMessageUID  ) FileName,(select MessageContent from HIMessageQueue hq where hq.UID= HIMessageUID  ) Content,* from HIMessageError where transactionDttm >= '" + dtereq.ToString("yyyy-MM-dd 00:00:00.000") + "' and transactionDttm <= '" + dtereq.AddDays(1).ToString("yyyy-MM-dd 00:00:00.000") + "' and (ErrorDescription ='Unable to parse HL7-string to HL7-Object' or ErrorDescription ='OBR.PlacerOrderNo.EntityID is empty')  order by UID asc";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_Rquest_By_Date(DateTime dtereq, string status)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            //2012-12-08 00:00:00.000
            string SQLStatement = @"	
		select (select Description from ReferenceValue rv where rv.UID= LRQSTUID) LabStatus,LRQSTUID,(select top 1 Pasid + '^' + ' ' + forename + '  ' + surname  from Patient p where p.UID= PatientUID) PatientNo,* from Request where RequestedDttm >= '" + dtereq.ToString("yyyy-MM-dd 00:00:00.000") + "' and RequestedDttm <= '" + dtereq.AddDays(1).ToString("yyyy-MM-dd 00:00:00.000") + "' and RequestNumber like '09%' and LRQSTUID ='" + status + "' order by UID asc";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_Visit_By_VisitUID(string visitUID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            //2012-12-08 00:00:00.000
            string SQLStatement = @"	
		    select * from PatientVisitID where PatientVisitUID = '" + visitUID + "'";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_Rquest_By_HN(string hn)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            //2012-12-08 00:00:00.000
            string SQLStatement = @"	
		    select (select Description from ReferenceValue rv where rv.UID= LRQSTUID) LabStatus,* from Request where PatientUID in (select UID from Patient where replace(PASID,'-','')= '" + hn + "')  and RequestNumber like '09%'  order by UID asc";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_Request_By_PatientVisitUID(string uid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            //2012-12-08 00:00:00.000
            string SQLStatement = @"	
		    select * from Request where PatientVisitUID ='" + uid + "'";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_Request_By_RequestNumber(string LabNo)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            //2012-12-08 00:00:00.000
            string SQLStatement = @"	
		    select * from Request where RequestNumber ='" + LabNo + "'";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_Result_By_RequestDetailUID(string uid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            //2012-12-08 00:00:00.000
            string SQLStatement = @"	
		    select * from Result where RequestDetailUID ='" + uid + "'";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_ResultComponent_By_ResultUID(string uid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            //2012-12-08 00:00:00.000
            string SQLStatement = @"	
		    select * from ResultComponent where ResultUID ='" + uid + "'";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_ResultTextual_By_ResultComponentUID(string uid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            //2012-12-08 00:00:00.000
            string SQLStatement = @"	
		    select * from ResultTextual where ResultComponentUID ='" + uid + "'";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_Rquest_No(string reqno)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @"	
		select (select Description from ReferenceValue rv where rv.UID= LRQSTUID) LabStatus,LRQSTUID,(select top 1 Pasid + '^' + ' ' + forename + '  ' + surname  from Patient p where p.UID= PatientUID) PatientNo,*  from Request where RequestNumber like'" + reqno + "%'";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_RequestDetail_By_RequestUID(string requid, string tscode)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @"	
        select * from RequestDetail where RequestUID = '" + requid + "' and ( select RequestItem.ItemCode from RequestItem WHERE RequestItem.uid = RequestItemUID and statusflag = 'A' ) ='" + tscode + "'   ";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_RequestDetail_No(string reqnouid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @"	
        select 
	    RequestItemName,RequestItemCode, (select Description from ReferenceValue rv where rv.UID= RSSTSUID) RequestStatus,* from RequestDetail  where RSSTSUID <> 67170 and StatusFlag='A' 
			and RequestUID in (select uid from Request where UID = '" + reqnouid + "' ) order by UID asc";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_RequestDetail_No_All_Status(string reqnouid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @"	
        select 
	    RequestItemName,RequestItemCode, (select Description from ReferenceValue rv where rv.UID= RSSTSUID) RequestStatus,* from RequestDetail  where  StatusFlag='A' 
			and RequestUID in (select uid from Request where UID = '" + reqnouid + "' ) order by UID asc";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_LabStatus()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @"	select * from Referencevalue where DomainCode= 'ORDST' and StatusFlag = 'A'    ";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_OrderNo(string strSQL)
        {
            DataTable dt = new DataTable();
            clsSQLNative clsSQL = new clsSQLNative();
            dt = clsSQL.Bind(strSQL, clsSQLNative.DBType.SQLServer, "csBConnect");
            return dt;
        }
        public DataTable Select_OrderNo_OLD(string strSQL)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            //string SQLStatement = @"	select * from Referencevalue where DomainCode= 'ORDST' and StatusFlag = 'A'    ";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = strSQL;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();
            }
            return dt;
        }
        public DataTable Select_BDMSPatientScheduleOrderDetail_By_ScheduleOrderUID(string ScheduleOrderUID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @"SELECT [UID] ,[PatientScheduleOrderUID] ,[ItemName] ,[BillPackageUID] FROM [BDMSPatientScheduleOrderDetail] ";
                   SQLStatement += "WHERE [StatusFlag] = 'A' AND [PatientScheduleOrderUID] = '" + ScheduleOrderUID + "'";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_PatientPackageUID_By_BillPackageItemUID(string PatientVisitUID, string BillPackageItemUID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @"select PatientPackage.UID PatientPackageUID,* 
                                    from PatientPackage inner join BillPackage on PatientPackage.BillPackageUID = BillPackage.UID
                                    inner join BillPackageItem on BillPackage.UID = BillPackageItem.BillPackageUID
                                    where  PatientPackage.PatientVisitUID = '" + PatientVisitUID + "' and BillPackageItem.UID = '" + BillPackageItemUID + "'";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public DataTable Select_Patient_For_Update_TblPatientList(string ScheduleOrderUID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @"select p.PASID,p.Forename , p.Surname , ps.ScheduledDttm , p.BirthDttm  
from Patient p inner join PatientScheduleOrder ps on p.UID = ps.PatientUID 
where ps.UID = '" + ScheduleOrderUID + "'";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;
        }


        //==============Update===============
        public bool UpdateOPDFileStatus(string location, string locuid, string hn)
        {
            SqlCommand cmd = new SqlCommand();
            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"update PatientFileLocation set CurrentStorageLocationUID='" + locuid + "',CurrentLocationName='" + location + "',FILSTUID = 4531 where PatientUID =(select top 1 UID from Patient where PASID ='" + hn + "')";

                cmd.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return flag;
        }
        public bool UpdateRequest_Detail_Status(string uid, string statusuid)
        {
            SqlCommand cmd = new SqlCommand();
            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"update RequestDetail set RSSTSUID='" + statusuid + "' where UID = '" + uid + "'";

                cmd.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return flag;
        }
        public DataTable Select_Rquest_No1(string reqno)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string SQLStatement = @"	
		select * from Request where RequestNumber ='" + reqno + "'";
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandText = SQLStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //	//System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //	throw;
            }
            finally
            {
                dt.Dispose();
                Disconnect();

            }
            return dt;

        }
        public bool Update_Result_By_RequestDetailUID(string ReqDetailuid)
        {
            SqlCommand cmd = new SqlCommand();
            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"update Result set Statusflag='U' ,CUSer = 99899 where RequestDetailUID= '" + ReqDetailuid + "' ";
                //cmd.Parameters.AddWithValue("@content", content);

                cmd.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return flag;
        }
        public string SEQID(string SEQ)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            // SqlConnection conStoreProcedure = new SqlConnection(sql.connectionString);
            string readerSEQ = string.Empty;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[pGetSEQID]";
                cmd.Parameters.Add("@P_SEQTableName", SEQ);
                IDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    readerSEQ = dr[0].ToString();
                }
            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                //throw;
            }
            finally
            {

                Disconnect();
            }
            return readerSEQ;
        }
        public bool Update_ResultComponent_By_RequestDetailUID(string ReqDetailuid)
        {
            SqlCommand cmd = new SqlCommand();
            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"update ResultComponent set Statusflag='U' ,CUSer = 99899 
                                    where ResultUID in (select  UID from Result where RequestDetailUID = '" + ReqDetailuid + "')";
                //cmd.Parameters.AddWithValue("@content", content);

                cmd.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return flag;
        }
        public bool Update_ResultTextual_By_RequestDetailUID(string ReqDetailuid)
        {
            SqlCommand cmd = new SqlCommand();
            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"update ResultTextual set Statusflag='U' ,CUSer = 99899 
                                    where ResultComponentUID in (select UID from ResultComponent 
                                    where  ResultValue='FT' and 
                                    ResultUID in (select  UID from Result where RequestDetailUID = '" + ReqDetailuid + "')   )";
                //cmd.Parameters.AddWithValue("@content", content);

                cmd.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return flag;
        }
        public void Update_TblPatientList(string Forename, string Surname, string DOE, string DOB)
        {
            ExcData exc = new ExcData();
            string SQL = @"UPDATE [tblPatientList] SET [STS] = 'C' WHERE [Forename] = '" + Forename.Trim()  +"' and [Surname] = '" + Surname.Trim() +"' and [DOE] = '" + DOE +"' and [DOB] = '" + DOB +"'";
            try
            {
                exc.ExecData(SQL);
            }
            catch (Exception ex)
            {
                ex.ToString();
                //MessageBox.Show("Cannot update EN :" + EN + " in Contact Database");
            }
        }

        //==============Insert===============
        public bool Insert_Result(string RequestDetailUID,
                                    string PatientUID,
                                    string RSSTSUID,
                                    string StatusFlag,
                                    string OwnerOrganisationUID,
                                    string PatientVisitUID,
                                    string ResultNumber,
                                    string RequestItemName,
                                    string RequestItemCode,
                                    string EnterUserUID,
                                    string AcknowledgeUID,
                                    DateTime EnterDate,
                                    DateTime AckDate,
                                    out string scope)
        {
            scope = string.Empty;
            SqlCommand cmd = new SqlCommand();
            string path =
            @"
            
            INSERT [Result]
           ([RequestDetailUID]
           ,[PatientUID]
           ,[ResultEnteredDttm]
           ,[ResultEnteredUserUID]
           ,[AcknowledgedDttm]
           ,[AcknowledgedUserUID]
           ,[RSSTSUID]
           ,[CUser]
           ,[CWhen]
           ,[MUser]
           ,[MWhen]
           ,[StatusFlag]
           ,[OwnerOrganisationUID]
           ,[RequestedOrganisationUID]
           ,[PatientVisitUID]
           ,[ResultNumber]
           ,[Comments]
           ,[RequestItemName]
           ,[DispatchedOn]
           ,[DispatchedBy]
           ,[DLMEDUID]
           ,[IsExcludeRequestComplete]
           ,[IsAllParameterFilled]
           ,[PreparedByUID]
           ,[PreparedDttm]
           ,[Modality]
           ,[EquipmentUID]
           ,[NoFilmUsed]
           ,[NoFilmWasted]
           ,[RABSTSUID]
           ,[ProcessingNote]
           ,[IsPACS]
           ,[NoofExposure]
           ,[RadiologistUID]
           ,[RequestItemCode])
            select  
	            '" + RequestDetailUID + @"' RequestDetailUID , 
	            '" + PatientUID + @"' PatientUID , 
	            '" + EnterDate.ToString("yyyy-MM-dd hh:mm:ss") + @"' ResultEnteredDttm , 
	            '" + EnterUserUID + @"' ResultEnteredUserUID , 
	            '" + AckDate.ToString("yyyy-MM-dd hh:mm:ss") + @"' AcknowledgedDttm,
	            '" + AcknowledgeUID + @"' AcknowledgedUserUID , 
	            '" + RSSTSUID + @"' RSSTSUID , 
 	            989899 CUser , 
	            getdate() CWhen , 
                989899 MUser , 
                getdate()	MWhen , 
	            'A' StatusFlag , 
	            9 OwnerOrganisationUID , 
	            null RequestedOrganisationUID , 
	            '" + PatientVisitUID + @"' PatientVisitUID , 
	            '" + ResultNumber + @"'ResultNumber , 
	            null Comments , 
	            @RequestItemName RequestItemName ,
	            null DispatchedOn , 
	            null DispatchedBy , 
	            null DLMEDUID , 
	            null IsExcludeRequestComplete , 
	            null IsAllParameterFilled , 
	            99899 PreparedByUID , 
	            getdate() PreparedDttm , 
	            null Modality ,
	            null EquipmentUID,
	            null NoFilmUsed ,
	            null NoFilmWasted ,
	            null RABSTSUID  , 
	            null ProcessingNote ,
	            null IsPACS,
	            null NoofExposure,
	            null RadiologistUID ,
	            '" + RequestItemCode + @"'RequestItemCode
                declare @id bigint;
                SET @id = SCOPE_IDENTITY();
                select @id";
            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = path;
                cmd.Parameters.AddWithValue("@RequestItemName", RequestItemName);

                scope = cmd.ExecuteScalar().ToString();
            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return flag;
        }
        public bool Insert_ResultComponent_Get_ScopeAlso(string ResultUID,
                                           string ResultItemUID,
                                           string RVTYPUID,
                                           string ResultValue,
                                           string ReferenceRange,
                                           string RSUOMUID,
                                           string Comments,
                                           string IsAbnormal,
                                           string StatusFlag,
                                           string OwnerOrganisationUID,
                                           string ResultItemName,
                                           string Expression,
                                           string ResultItemCode, out string scope)
        {
            scope = string.Empty;
            SqlCommand cmd = new SqlCommand();
            string path =
            @"
            INSERT  [ResultComponent]
           ([ResultUID]
           ,[ResultItemUID]
           ,[RVTYPUID]
           ,[ResultValue]
           ,[ReferenceRange]
           ,[RSUOMUID]
           ,[Comments]
           ,[IsAbnormal]
           ,[CUser]
           ,[CWhen]
           ,[MUser]
           ,[MWhen]
           ,[StatusFlag]
           ,[OwnerOrganisationUID]
           ,[ResultItemName]
           ,[Expression]
           ,[ResultItemCode])
             SELECT  
               '" + ResultUID + @"' ResultUID
              , '" + ResultItemUID + @"'ResultItemUID
              ,'" + RVTYPUID + @"' RVTYPUID
              ,@ResultValue ResultValue
              ,@ReferenceRange ReferenceRange
              ,'" + RSUOMUID + @"'RSUOMUID
              ,'" + Comments + @"'Comments
              ,'" + IsAbnormal + @"'IsAbnormal
              ,989899 CUser
              ,getdate() CWhen
              ,989899 MUser
              ,getdate() MWhen
              ,'A' StatusFlag
              ,9 OwnerOrganisationUID
              ,@ResultItemName ResultItemName
              ,'" + Expression + @"'Expression
              ,'" + ResultItemCode + @"'ResultItemCode
               declare @id bigint;
               SET @id = SCOPE_IDENTITY();
               select @id ";
            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = path;
                cmd.Parameters.AddWithValue("@ResultValue", ResultValue);
                cmd.Parameters.AddWithValue("@ResultItemName", ResultItemName);
                cmd.Parameters.AddWithValue("@ReferenceRange", ReferenceRange);

                scope = cmd.ExecuteScalar().ToString();
            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return flag;
        }


        public bool Insert_ResultComponent(string ResultUID,
                                           string ResultItemUID,
                                           string RVTYPUID,
                                           string ResultValue,
                                           string ReferenceRange,
                                           string RSUOMUID,
                                           string Comments,
                                           string IsAbnormal,
                                           string StatusFlag,
                                           string OwnerOrganisationUID,
                                           string ResultItemName,
                                           string Expression,
                                           string ResultItemCode)
        {
            SqlCommand cmd = new SqlCommand();
            string path =
            @"
            INSERT  [ResultComponent]
           ([ResultUID]
           ,[ResultItemUID]
           ,[RVTYPUID]
           ,[ResultValue]
           ,[ReferenceRange]
           ,[RSUOMUID]
           ,[Comments]
           ,[IsAbnormal]
           ,[CUser]
           ,[CWhen]
           ,[MUser]
           ,[MWhen]
           ,[StatusFlag]
           ,[OwnerOrganisationUID]
           ,[ResultItemName]
           ,[Expression]
           ,[ResultItemCode])
             SELECT  
               '" + ResultUID + @"' ResultUID
              , '" + ResultItemUID + @"'ResultItemUID
              ,'" + RVTYPUID + @"' RVTYPUID
              ,@ResultValue ResultValue
              ,@ReferenceRange ReferenceRange
              ,'" + RSUOMUID + @"'RSUOMUID
              ,'" + Comments + @"'Comments
              ,'" + IsAbnormal + @"'IsAbnormal
              ,989899 CUser
              ,getdate() CWhen
              ,989899 MUser
              ,getdate() MWhen
              ,'A' StatusFlag
              ,9 OwnerOrganisationUID
              ,@ResultItemName ResultItemName
              ,'" + Expression + @"'Expression
              ,'" + ResultItemCode + @"'ResultItemCode
                ";
            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = path;
                cmd.Parameters.AddWithValue("@ResultValue", ResultValue);
                cmd.Parameters.AddWithValue("@ResultItemName", ResultItemName);
                cmd.Parameters.AddWithValue("@ReferenceRange", ReferenceRange);
                cmd.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return flag;
        }
        public bool Insert_OtherEvent(string patvisituid, string organisation, string resultuid)
        {
            SqlCommand cmd = new SqlCommand();
            string path =
            @"          
            insert into  OtherEvent (PatientVisitUID,EVNTYUID,Comments,OccuredDttm,CareproviderUID,CUser,CWhen,MUser
            ,MWhen,StatusFlag,OwnerOrganisationUID,IdentifyingUID,IdentifyingType,CarePathWayUID,CarePath)
            Select Result.PatientVisitUID PatientVisitUID,413 EVNTYUID,'Insert BackEnd by Astroboy'Comments,getdate() OccuredDttm";
            path += ",isnull((select top 1 PatientVisit.CareProviderUID from PatientVisit where PatientVisit.UID= " + patvisituid + " and PatientVisit.StatusFlag = 'A' ),0) CareproviderUID,99999 CUser,getdate() CWhen,99999 MUser,getdate() MWhen,'A' StatusFlag," + organisation + " OwnerOrganisationUID,Result.UID IdentifyingUID,'RESULT' IdentifyingType,null CarePathWayUID,'10.103.103.63' CarePath from Result where Patientvisituid = '" + patvisituid + "' and Result.UID = '" + resultuid + "'";
            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = path;


                cmd.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return flag;
        }
        public string Insert_PatientPackage_by_ScheduleOrderNumber(string ScheduleOrderNumber,string own)
        {
            string packageuid = string.Empty;
            SqlCommand cmd = new SqlCommand();
            string path =
            @"insert PatientPackage (PatientUID,PatientVisitUID,PackageName,PackageID,BillPackageUID,PatientBillEstimateUID
                            ,PKGSTUID,TotalAmount,ActiveFrom,ActiveTo,PackageCreatedByUID,PackageCreatedDttm,Comments
						,CUser,CWhen,MUser,MWhen,StatusFlag,OwnerOrganisationUID,BillingGroupUID,SubGroupUID,IsConsiderItem)
		                select top 1 PatientScheduleOrder.PatientUID PatientUID,PatientScheduleOrder.PatientVisitUID PatientVisitUID
						,(select top 1 PackageName from BillPackage where BillPackage.UID = PatientScheduleOrderDetail.IdentifyingUID and BillPackage.StatusFlag = 'A') PackageName
						,null PackageID
						,(select top 1 UID from BillPackage where BillPackage.UID = PatientScheduleOrderDetail.IdentifyingUID and BillPackage.StatusFlag = 'A') BillPackageUID
						,null PatientBillEstimateUID
						,0 PKGSTUID
						,(select top 1 TotalAmount from BillPackage where BillPackage.UID = PatientScheduleOrderDetail.IdentifyingUID and BillPackage.StatusFlag = 'A') TotalAmount
						,(select top 1 ActiveFrom from BillPackage where BillPackage.UID = PatientScheduleOrderDetail.IdentifyingUID and BillPackage.StatusFlag = 'A') ActiveFrom
						,(select top 1 ActiveTo from BillPackage where BillPackage.UID = PatientScheduleOrderDetail.IdentifyingUID and BillPackage.StatusFlag = 'A') ActiveTo
						,1 PackageCreatedByUID,getdate() PackageCreatedDttm
						,'Generate by MassConvert' Comments
						,1 CUser,getdate() CWhen,1 MUser,getdate() MWhen,'A' StatusFlag,'" + own + @"' OwnerOrganisationUID
						,(select top 1 BillPackage.AccountUID from BillPackage where BillPackage.UID = PatientScheduleOrderDetail.IdentifyingUID and BillPackage.StatusFlag = 'A') BillingGroupUID
						,(select top 1 BillPackage.SubAccountUID from BillPackage where BillPackage.UID = PatientScheduleOrderDetail.IdentifyingUID and BillPackage.StatusFlag = 'A') SubGroupUID
						,(select top 1 BillPackage.AllowOverridePrice from BillPackage where BillPackage.UID = PatientScheduleOrderDetail.IdentifyingUID and BillPackage.StatusFlag = 'A') IsConsiderItem 
						from  PatientScheduleOrder,PatientScheduleOrderDetail where PatientScheduleOrder.UID=PatientScheduleOrderDetail.PatientScheduleOrderUID 
							and ScheduleOrderNumber = '" + ScheduleOrderNumber + "' and PatientScheduleOrder.StatusFlag ='A' and  PatientScheduleOrderDetail.IdentifyingType = 'BILLPACKAGE' and PatientScheduleOrderDetail.StatusFlag = 'A'";

            path+=" Select SCOPE_IDENTITY() AS [SCOPE]";
            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = path;
                packageuid = cmd.ExecuteScalar().ToString();
            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return packageuid;
        }
        public string Insert_PatientPackage_by_ScheduleOrderNumber(string ScheduleOrderNumber, string own, string PackageUID,string PatientVisitUID)
        {
            string packageuid = string.Empty;
            SqlCommand cmd = new SqlCommand();
            //string path =
//           @"insert PatientPackage (PatientUID,PatientVisitUID,PackageName,PackageID,BillPackageUID,PatientBillEstimateUID
//                            ,PKGSTUID,TotalAmount,ActiveFrom,ActiveTo,PackageCreatedByUID,PackageCreatedDttm,Comments
//						,CUser,CWhen,MUser,MWhen,StatusFlag,OwnerOrganisationUID,BillingGroupUID,SubGroupUID,IsConsiderItem)
//		                select top 1 PatientScheduleOrder.PatientUID PatientUID,PatientScheduleOrder.PatientVisitUID PatientVisitUID
//						,(select top 1 PackageName from BillPackage where BillPackage.UID = PatientScheduleOrderDetail.IdentifyingUID and BillPackage.StatusFlag = 'A') PackageName
//						,null PackageID
//						,(select top 1 UID from BillPackage where BillPackage.UID = PatientScheduleOrderDetail.IdentifyingUID and BillPackage.StatusFlag = 'A') BillPackageUID
//						,null PatientBillEstimateUID
//						,0 PKGSTUID
//						,(select top 1 TotalAmount from BillPackage where BillPackage.UID = PatientScheduleOrderDetail.IdentifyingUID and BillPackage.StatusFlag = 'A') TotalAmount
//						,(select top 1 ActiveFrom from BillPackage where BillPackage.UID = PatientScheduleOrderDetail.IdentifyingUID and BillPackage.StatusFlag = 'A') ActiveFrom
//						,(select top 1 ActiveTo from BillPackage where BillPackage.UID = PatientScheduleOrderDetail.IdentifyingUID and BillPackage.StatusFlag = 'A') ActiveTo
//						,1 PackageCreatedByUID,getdate() PackageCreatedDttm
//						,'Generate by MassConvert' Comments
//						,1 CUser,getdate() CWhen,1 MUser,getdate() MWhen,'A' StatusFlag,'" + own + @"' OwnerOrganisationUID
//						,(select top 1 BillPackage.AccountUID from BillPackage where BillPackage.UID = PatientScheduleOrderDetail.IdentifyingUID and BillPackage.StatusFlag = 'A') BillingGroupUID
//						,(select top 1 BillPackage.SubAccountUID from BillPackage where BillPackage.UID = PatientScheduleOrderDetail.IdentifyingUID and BillPackage.StatusFlag = 'A') SubGroupUID
//						,(select top 1 BillPackage.AllowOverridePrice from BillPackage where BillPackage.UID = PatientScheduleOrderDetail.IdentifyingUID and BillPackage.StatusFlag = 'A') IsConsiderItem 
//						from  PatientScheduleOrder,PatientScheduleOrderDetail where PatientScheduleOrder.UID=PatientScheduleOrderDetail.PatientScheduleOrderUID 
//							and ScheduleOrderNumber = '" + ScheduleOrderNumber + "' and PatientScheduleOrder.StatusFlag ='A' and  PatientScheduleOrderDetail.IdentifyingType = 'BILLPACKAGE' and PatientScheduleOrderDetail.StatusFlag = 'A'";

//            path += " Select SCOPE_IDENTITY() AS [SCOPE]";
            string path = @"insert PatientPackage (PatientUID,PatientVisitUID,PackageName,PackageID,BillPackageUID,PatientBillEstimateUID";
                   path += " ,PKGSTUID,TotalAmount,ActiveFrom,ActiveTo,PackageCreatedByUID,PackageCreatedDttm,Comments";
                   path += " ,CUser,CWhen,MUser,MWhen,StatusFlag,OwnerOrganisationUID,BillingGroupUID,SubGroupUID,IsConsiderItem,Qty)";
                   path += " select PatientUID PatientUID,'" + PatientVisitUID  + @"' PatientVisitUID";
                   path += " ,(select top 1 PackageName from BillPackage where BillPackage.UID = BDMSPatientScheduleOrderDetail.BillPackageUID and BillPackage.StatusFlag = 'A') PackageName";
                   path += " ,null PackageID";
                   path += " ,(select top 1 UID from BillPackage where BillPackage.UID = BDMSPatientScheduleOrderDetail.BillPackageUID and BillPackage.StatusFlag = 'A') BillPackageUID";
                   path += " ,null PatientBillEstimateUID";
                   path += " ,0 PKGSTUID";
                   path += " ,(select top 1 TotalAmount from BillPackage where BillPackage.UID = BDMSPatientScheduleOrderDetail.BillPackageUID and BillPackage.StatusFlag = 'A') TotalAmount";
                   path += " ,(select top 1 ActiveFrom from BillPackage where BillPackage.UID = BDMSPatientScheduleOrderDetail.BillPackageUID and BillPackage.StatusFlag = 'A') ActiveFrom";
                   path += " ,(select top 1 ActiveTo from BillPackage where BillPackage.UID = BDMSPatientScheduleOrderDetail.BillPackageUID and BillPackage.StatusFlag = 'A') ActiveTo";
                   path += " ,1 PackageCreatedByUID,getdate() PackageCreatedDttm";
                   path += " ,'Generate by MassConvert' Comments";
                   path += " ,1 CUser,getdate() CWhen,1 MUser,getdate() MWhen,'A' StatusFlag,'" + own + @"' OwnerOrganisationUID";
                   path += " ,(select top 1 BillPackage.AccountUID from BillPackage where BillPackage.UID = BDMSPatientScheduleOrderDetail.BillPackageUID and BillPackage.StatusFlag = 'A') BillingGroupUID";
                   path += " ,(select top 1 BillPackage.SubAccountUID from BillPackage where BillPackage.UID = BDMSPatientScheduleOrderDetail.BillPackageUID and BillPackage.StatusFlag = 'A') SubGroupUID";
                   path += " ,isnull((select top 1 BillPackage.AllowOverridePrice from BillPackage where BillPackage.UID = BDMSPatientScheduleOrderDetail.BillPackageUID and BillPackage.StatusFlag = 'A'),'N') IsConsiderItem ,1 as Qty ";
                   path += " from  BDMSPatientScheduleOrderDetail where ";
                   path += " PatientScheduleOrderUID = '" + ScheduleOrderNumber + "' and StatusFlag ='A' and BillPackageUID = '" + PackageUID + "'";

                   path += " Select SCOPE_IDENTITY() AS [SCOPE]";
            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = path;
                packageuid = cmd.ExecuteScalar().ToString();
            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return packageuid;
        }
        public bool Insert_PatientPackageItem_by_ScheduleOrderNumber(string ScheduleOrderNumber,string PatientPackageUID)
        {
            SqlCommand cmd = new SqlCommand();
            string path =
            @"insert PatientPackageItem (PatientPackageUID,BSMDDUID,BillableItemUID,ItemName,Amount,PatientBillUID,CUser,CWhen,MUser,MWhen,StatusFlag,OwnerOrganisationUID,Discount,ItemMultiplier,ActualAmount)";
            path += "select '" + PatientPackageUID + "' PatientPackageUID";
            path += ",(select top 1 BSMDDUID from BillableItem where BillableItem.UID = PatientScheduleOrderDetail.BillableItemUID)BSMDDUID ";
            path +=",PatientScheduleOrderDetail.BillableItemUID BillableItemUID,PatientScheduleOrderDetail.ItemName ItemName,isnull((select top 1 BillPackageItem.Amount from BillPackage,BillPackageItem where BillPackage.UID = BillPackageItem.BillPackageUID ";
            path += " and BillPackage.UID = PatientScheduleOrderDetail.BillPackageUID and BillPackageItem.BillableItemUID = PatientScheduleOrderDetail.BillableItemUID and BillPackageItem.ActiveFrom <= getdate() and (BillPackageItem.ActiveTo is null or BillPackageItem.ActiveTo >= getdate() )";
            path += " and BillPackage.StatusFlag = 'A' ),0) Amount,null PatientBillUID,1 CUser,getdate() CWhen,1 MUser,getdate() MWhen,'A' StatusFlag,9 OwnerOrganisationUID,null Discount,isnull((select top 1 BillPackageItem.Quantity from BillPackage,BillPackageItem where BillPackage.UID = BillPackageItem.BillPackageUID ";
            path += " and BillPackage.UID = PatientScheduleOrderDetail.BillPackageUID and BillPackageItem.BillableItemUID = PatientScheduleOrderDetail.BillableItemUID and BillPackageItem.ActiveFrom <= getdate() and (BillPackageItem.ActiveTo is null or BillPackageItem.ActiveTo >= getdate() )";
            path += " and BillPackage.StatusFlag = 'A' ),0) ItemMultiplier,0 ActualAmount from PatientScheduleOrderDetail where PatientScheduleOrderUID in (select UID from PatientScheduleOrder where ScheduleOrderNumber = '" + ScheduleOrderNumber + "')";
            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = path;
                cmd.ExecuteNonQuery();

            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return flag;
        }
        public bool Insert_PatientPackageItem_by_ScheduleOrderNumber_BillPackage(string ScheduleOrderNumber, string PatientPackageUID,string Owner)
        {
            SqlCommand cmd = new SqlCommand();
            string path =
            //@"insert PatientPackageItem (PatientPackageUID,BSMDDUID,BillableItemUID,ItemName,Amount,PatientBillUID,CUser,CWhen,MUser,MWhen,StatusFlag,OwnerOrganisationUID,Discount,ItemMultiplier,ActualAmount)";
            //path += "select '" + PatientPackageUID + "' PatientPackageUID";
            //path += ",(select top 1 BSMDDUID from BillableItem where BillableItem.UID = BillableItemUID)BSMDDUID ";
            //path += ",BillableItemUID BillableItemUID,Comments ItemName,isnull((select top 1 BillPackageItem.Amount from BillPackage,BillPackageItem where BillPackage.UID = BillPackageItem.BillPackageUID ";
            //path += " and BillPackage.UID = BillPackageUID and BillPackageItem.BillableItemUID = BillableItemUID and BillPackageItem.ActiveFrom <= getdate() and (BillPackageItem.ActiveTo is null or BillPackageItem.ActiveTo >= getdate() )";
            //path += " and BillPackage.StatusFlag = 'A' ),0) Amount,null PatientBillUID,1 CUser,getdate() CWhen,1 MUser,getdate() MWhen,'A' StatusFlag,'" + Owner + "' OwnerOrganisationUID,null Discount,isnull((select top 1 BillPackageItem.Quantity from BillPackage,BillPackageItem where BillPackage.UID = BillPackageItem.BillPackageUID ";
            //path += " and BillPackage.UID = BillPackageUID and BillPackageItem.BillableItemUID = BillableItemUID and BillPackageItem.ActiveFrom <= getdate() and (BillPackageItem.ActiveTo is null or BillPackageItem.ActiveTo >= getdate() )";
            //path += " and BillPackage.StatusFlag = 'A' ),0) ItemMultiplier,0 ActualAmount from BillPackageItem where BillPackageItem.BillPackageUID = '" + PatientPackageUID + "'";
            @"insert PatientPackageItem (PatientPackageUID,BSMDDUID,BillableItemUID,ItemName,Amount,PatientBillUID,CUser,CWhen,MUser,MWhen,StatusFlag,OwnerOrganisationUID,Discount,ItemMultiplier,ActualAmount)";
            //path += "select '" + PatientPackageUID + "' PatientPackageUID";
            //path += ",(select top 1 BSMDDUID from BillableItem where BillableItem.UID = BillableItemUID)BSMDDUID ";
            //path += ",BillableItemUID BillableItemUID,Comments ItemName,isnull((select top 1 BillPackageItem.Amount from BillPackage,BillPackageItem where BillPackage.UID = BillPackageItem.BillPackageUID ";
            //path += " and BillPackage.UID = BillPackageUID and BillPackageItem.BillableItemUID = BillableItemUID and BillPackageItem.ActiveFrom <= getdate() and (BillPackageItem.ActiveTo is null or BillPackageItem.ActiveTo >= getdate() )";
            //path += " and BillPackage.StatusFlag = 'A' ),0) Amount,null PatientBillUID,1 CUser,getdate() CWhen,1 MUser,getdate() MWhen,'A' StatusFlag,'" + Owner + "' OwnerOrganisationUID,null Discount,isnull((select top 1 BillPackageItem.Quantity from BillPackage,BillPackageItem where BillPackage.UID = BillPackageItem.BillPackageUID ";
            //path += " and BillPackage.UID = BillPackageUID and BillPackageItem.BillableItemUID = BillableItemUID and BillPackageItem.ActiveFrom <= getdate() and (BillPackageItem.ActiveTo is null or BillPackageItem.ActiveTo >= getdate() )";
            //path += " and BillPackage.StatusFlag = 'A' ),0) ItemMultiplier,0 ActualAmount from BillPackageItem where BillPackageItem.BillPackageUID = isnull((select top 1 PatientPackage.BillPackageUID from PatientPackage where PatientPackage.UID = '" + PatientPackageUID + "'),0)";
            path += " select '" + PatientPackageUID + "' PatientPackageUID ,(select top 1 BSMDDUID from BillableItem where BillableItem.UID = BillableItemUID)BSMDDUID";
            path += " ,BillableItemUID BillableItemUID,b.ItemName ItemName,isnull((select top 1 BillPackageItem.Amount from BillPackage,BillPackageItem where BillPackage.UID = BillPackageItem.BillPackageUID ";
            path += " and BillPackage.UID = BillPackageUID and BillPackageItem.BillableItemUID = BillableItemUID and BillPackageItem.ActiveFrom <= getdate() and (BillPackageItem.ActiveTo is null or BillPackageItem.ActiveTo >= getdate() )";
            path += " and BillPackage.StatusFlag = 'A' and BillPackageUID = (select top 1 PatientPackage.BillPackageUID from PatientPackage where PatientPackage.UID = '" + PatientPackageUID + "')),0) Amount,null PatientBillUID,1 CUser,getdate() CWhen,1 MUser,getdate() MWhen,'A' StatusFlag,'" + Owner + "' OwnerOrganisationUID,null Discount,isnull((select top 1 BillPackageItem.Quantity from BillPackage,BillPackageItem where BillPackage.UID = BillPackageItem.BillPackageUID ";
            path += " and BillPackage.UID = BillPackageUID and BillPackageItem.BillableItemUID = BillableItemUID and BillPackageItem.ActiveFrom <= getdate() and (BillPackageItem.ActiveTo is null or BillPackageItem.ActiveTo >= getdate() )";
            path += " and BillPackage.StatusFlag = 'A' ),0) ItemMultiplier,0 ActualAmount ";
            path += " from BillPackageItem a inner join BillableItem b on a.BillableItemUID = b.uid ";
            path += " where a.BillPackageUID = isnull((select top 1 PatientPackage.BillPackageUID from PatientPackage where PatientPackage.UID = '" + PatientPackageUID + "'),0)";

            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = path;
                cmd.ExecuteNonQuery();

            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return flag;
        }
        public bool Insert_PatientBillableItem_For_PackageHeader(string ScheduleOrderDetailUID, string PatientPackageUID,string own,string cuser, string patientVisitUID,string patuid,string IdentifyingUID)
        {
            SqlCommand cmd = new SqlCommand();
            string path =
            @"insert PatientBillableItem (PatientUID,PatientVisitUID,IdentifyingUID,IdentifyingType,CUser,CWhen,MUser,MWhen,StatusFlag,OwnerOrganisationUID,ProcedureUID,BSMDDUID,Amount,Discount,NetAmount,Description,BillableItemUID,Comments,ItemMultiplier,StartDttm,EndDttm,DiscountAuthorizedBy,ItemUID,VISITYUID,RSLVLUID,ItemName,CareProviderUID,ConsultantShare,EventOccuredDttm,QNUOMUID,ConsultantDiscount,EducationCess,HigherEducationCess,ServiceTax,PayorUID,PatientVisitPayorUID,BatchID,VATPercentage,InternalCost,CalculateTaxOnMRP,StoreUID,BillPackageUID,SplitDiscount,BLTYPUID,OrderType,OrderTypeUID,ReceiptNumber,RefundBillNumber,PatientOrderDetailUID,IPFillProcessUID,ORDSTUID,IsModified,PreviousVisitUID,ENTYPUID,AllocatedPatBillableItemUID,ExpiryDttm,GroupUID,SubGroupUID,GroupMaxCoverage,GroupCovered,SubGroupMaxCoverage,SubGroupCovered,ItemMaxCoverage,ItemCovered,PBLCTUID,ALLDIUID)
            select '" + patuid + @"' PatientUID,'" + patientVisitUID + @"' PatientVisitUID
			,BDMSPatientScheduleOrderDetail.BillPackageUID IdentifyingUID
			,'BillPackage' IdentifyingType
			,'" + cuser + @"' CUser,getdate() CWhen,'" + cuser + @"' MUser,getdate() MWhen,'A' StatusFlag,'" + own + @"' OwnerOrganisationUID
			,null ProcedureUID
			,0 BSMDDUID
			,isnull((select top 1 TotalAmount from BillPackage where 
			 BillPackage.UID = BDMSPatientScheduleOrderDetail.BillPackageUID 
			and BillPackage.ActiveFrom <= getdate() and (BillPackage.ActiveTo is null or BillPackage.ActiveTo >= getdate() )
			and BillPackage.StatusFlag = 'A' ),0) Amount
			,0 Discount
			,isnull((select top 1 TotalAmount*1 from BillPackage where 
			 BillPackage.UID = BDMSPatientScheduleOrderDetail.BillPackageUID 
			and BillPackage.ActiveFrom <= getdate() and (BillPackage.ActiveTo is null or BillPackage.ActiveTo >= getdate() )
			and BillPackage.StatusFlag = 'A' ),0) NetAmount
			,null Description
			,null BillableItemUID
			,null Comments
			,isnull((select top 1 BillPackageItem.Quantity  from BillPackage,BillPackageItem where BillPackage.UID = BillPackageItem.BillPackageUID 
			and BillPackageItem.BillPackageUID = BDMSPatientScheduleOrderDetail.BillPackageUID
			and BillPackageItem.ActiveFrom <= getdate() and (BillPackageItem.ActiveTo is null or BillPackageItem.ActiveTo >= getdate() )
			and BillPackage.StatusFlag = 'A' ),1) ItemMultiplier
			,null StartDttm,null EndDttm
			,null DiscountAuthorizedBy
			,null ItemUID
			,null VISITYUID,null RSLVLUID
			,BDMSPatientScheduleOrderDetail.ItemName ItemName
			,null CareProviderUID
			,null ConsultantShare,getdate() EventOccuredDttm
			,(select top 1 QNUOMUID from BillableItem where BillableItem.UID = BDMSPatientScheduleOrderDetail.BillableItemUID) QNUOMUID
			,null ConsultantDiscount,null EducationCess,null HigherEducationCess
			,null ServiceTax
			,isnull((select top 1 PayorUID from PatientVisitPayor where PatientVisitPayor.PatientUID=BDMSPatientScheduleOrderDetail.PatientUID 
			and PatientVisitPayor.StatusFlag = 'A' order by PatientVisitPayor.UID asc  ),0) PayorUID
			,isnull((select top 1 UID from PatientVisitPayor where PatientVisitPayor.PatientUID=BDMSPatientScheduleOrderDetail.PatientUID 
			and PatientVisitPayor.StatusFlag = 'A' order by PatientVisitPayor.UID asc  ),0) PatientVisitPayorUID
			,null BatchID,null VATPercentage,null InternalCost,null CalculateTaxOnMRP,null StoreUID
			,'" + PatientPackageUID + @"' BillPackageUID  
			,isnull((select top 1 BillPackageItem.Quantity  from BillPackage,BillPackageItem where BillPackage.UID = BillPackageItem.BillPackageUID 
			and BillPackageItem.BillPackageUID = BDMSPatientScheduleOrderDetail.BillableItemUID
			and BillPackageItem.ActiveFrom <= getdate() and (BillPackageItem.ActiveTo is null or BillPackageItem.ActiveTo >= getdate() )
			and BillPackage.StatusFlag = 'A' ),1) SplitDiscount
            ,null BLTYPUID
			,null OrderType,null OrderTypeUID
			,null ReceiptNumber,null RefundBillNumber
			,null PatientOrderDetailUID,null IPFillProcessUID
			,null ORDSTUID,null IsModified,null PreviousVisitUID
			,null ENTYPUID
			,null AllocatedPatBillableItemUID,null ExpiryDttm
			,null GroupUID,null SubGroupUID,null GroupMaxCoverage
			,null GroupCovered,null SubGroupMaxCoverage
            ,isnull((select top 1 TotalAmount from BillPackage where 
			 BillPackage.UID = BDMSPatientScheduleOrderDetail.BillPackageUID 
			and BillPackage.ActiveFrom <= getdate() and (BillPackage.ActiveTo is null or BillPackage.ActiveTo >= getdate() )
			and BillPackage.StatusFlag = 'A' ),0) SubGroupCovered,null ItemMaxCoverage,null ItemCovered
			,null PBLCTUID,null ALLDIUID
from  BDMSPatientScheduleOrderDetail where 
			BDMSPatientScheduleOrderDetail.PatientScheduleOrderUID = (select top 1 UID From PatientScheduleOrder where Statusflag ='A' and ScheduleOrderNumber = '" + ScheduleOrderDetailUID + "') and BDMSPatientScheduleOrderDetail.BillPackageUID = '" + IdentifyingUID + "'";
//            //and BDMSPatientScheduleOrderDetail.BillPackageUID = '" + PatientPackageUID + "'

//            @"insert PatientBillableItem (PatientUID,PatientVisitUID,IdentifyingUID,IdentifyingType,CUser,CWhen,MUser,MWhen,StatusFlag,OwnerOrganisationUID,ProcedureUID,BSMDDUID,Amount,Discount,NetAmount,Description,BillableItemUID,Comments,ItemMultiplier,StartDttm,EndDttm,DiscountAuthorizedBy,ItemUID,VISITYUID,RSLVLUID,ItemName,CareProviderUID,ConsultantShare,EventOccuredDttm,QNUOMUID,ConsultantDiscount,EducationCess,HigherEducationCess,ServiceTax,PayorUID,PatientVisitPayorUID,BatchID,VATPercentage,InternalCost,CalculateTaxOnMRP,StoreUID,BillPackageUID,SplitDiscount,BLTYPUID,OrderType,OrderTypeUID,ReceiptNumber,RefundBillNumber,PatientOrderDetailUID,IPFillProcessUID,ORDSTUID,IsModified,PreviousVisitUID,ENTYPUID,AllocatedPatBillableItemUID,ExpiryDttm,GroupUID,SubGroupUID,GroupMaxCoverage,GroupCovered,SubGroupMaxCoverage,SubGroupCovered,ItemMaxCoverage,ItemCovered,PBLCTUID,ALLDIUID)
//            select '" + patuid + @"' PatientUID,'" + patientVisitUID + @"' PatientVisitUID
//			,PatientScheduleOrderDetail.IdentifyingUID IdentifyingUID
//			,'BillPackage' IdentifyingType
//			,'" + cuser + @"' CUser,getdate() CWhen,'" + cuser + @"' MUser,getdate() MWhen,'A' StatusFlag,'" + own + @"' OwnerOrganisationUID
//			,null ProcedureUID
//			,0 BSMDDUID
//			,isnull((select top 1 TotalAmount from BillPackage where 
//			 BillPackage.UID = PatientScheduleOrderDetail.IdentifyingUID 
//			and BillPackage.ActiveFrom <= getdate() and (BillPackage.ActiveTo is null or BillPackage.ActiveTo >= getdate() )
//			and BillPackage.StatusFlag = 'A' ),0) Amount
//			,0 Discount
//			,isnull((select top 1 TotalAmount*1 from BillPackage where 
//			 BillPackage.UID = PatientScheduleOrderDetail.IdentifyingUID 
//			and BillPackage.ActiveFrom <= getdate() and (BillPackage.ActiveTo is null or BillPackage.ActiveTo >= getdate() )
//			and BillPackage.StatusFlag = 'A' ),0) NetAmount
//			,null Description
//			,null BillableItemUID
//			,null Comments
//			,isnull((select top 1 BillPackageItem.Quantity  from BillPackage,BillPackageItem where BillPackage.UID = BillPackageItem.BillPackageUID 
//			and BillPackage.UID = PatientScheduleOrderDetail.BillPackageUID 
//			and BillPackageItem.BillableItemUID = PatientScheduleOrderDetail.BillableItemUID
//			and BillPackageItem.ActiveFrom <= getdate() and (BillPackageItem.ActiveTo is null or BillPackageItem.ActiveTo >= getdate() )
//			and BillPackage.StatusFlag = 'A' ),1) ItemMultiplier
//			,null StartDttm,null EndDttm
//			,null DiscountAuthorizedBy
//			,null ItemUID
//			,null VISITYUID,null RSLVLUID
//			,PatientScheduleOrderDetail.ItemName ItemName
//			,null CareProviderUID
//			,null ConsultantShare,getdate() EventOccuredDttm
//			,(select top 1 QNUOMUID from BillableItem where BillableItem.UID = PatientScheduleOrderDetail.BillableItemUID) QNUOMUID
//			,null ConsultantDiscount,null EducationCess,null HigherEducationCess
//			,null ServiceTax
//			,isnull((select top 1 PayorUID from PatientVisitPayor where PatientVisitPayor.PatientUID=PatientScheduleOrder.PatientUID 
//			and PatientVisitPayor.PatientVisitUID = PatientScheduleOrder.PatientVisitUID 
//			and PatientVisitPayor.StatusFlag = 'A' order by PatientVisitPayor.PAYRTPUID asc  ),0) PayorUID
//			,isnull((select top 1 UID from PatientVisitPayor where PatientVisitPayor.PatientUID=PatientScheduleOrder.PatientUID 
//			and PatientVisitPayor.PatientVisitUID = PatientScheduleOrder.PatientVisitUID 
//			and PatientVisitPayor.StatusFlag = 'A' order by PatientVisitPayor.PAYRTPUID asc  ),0) PatientVisitPayorUID
//			,null BatchID,null VATPercentage,null InternalCost,null CalculateTaxOnMRP,null StoreUID
//			,@xpackageuid BillPackageUID  
//			,null SplitDiscount,null BLTYPUID
//			,null OrderType,null OrderTypeUID
//			,null ReceiptNumber,null RefundBillNumber
//			,null PatientOrderDetailUID,null IPFillProcessUID
//			,null ORDSTUID,null IsModified,null PreviousVisitUID
//			,null ENTYPUID
//			,null AllocatedPatBillableItemUID,null ExpiryDttm
//			,null GroupUID,null SubGroupUID,null GroupMaxCoverage
//			,null GroupCovered,null SubGroupMaxCoverage
//            ,isnull((select top 1 TotalAmount from BillPackage where 
//			 BillPackage.UID = PatientScheduleOrderDetail.IdentifyingUID 
//			and BillPackage.ActiveFrom <= getdate() and (BillPackage.ActiveTo is null or BillPackage.ActiveTo >= getdate() )
//			and BillPackage.StatusFlag = 'A' ),0) SubGroupCovered,null ItemMaxCoverage,null ItemCovered
//			,null PBLCTUID,null ALLDIUID
//from  PatientScheduleOrder,PatientScheduleOrderDetail where PatientScheduleOrder.UID=PatientScheduleOrderDetail.PatientScheduleOrderUID 
//							  and PatientScheduleOrder.StatusFlag ='A' and PatientScheduleOrderDetail.StatusFlag = 'A' and PatientScheduleOrderDetail.PatientScheduleOrderUID = (select top 1 UID From PatientScheduleOrder where Statusflag ='A' and ScheduleOrderNumber = '" + ScheduleOrderDetailUID + "')";

            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@xpackageuid", PatientPackageUID);
                
                
                
                cmd.CommandText = path;
                cmd.ExecuteNonQuery();

            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return flag;
        }
        public string Insert_PatientVisitPayor(string patientvisitUID, string patientUID, string payorUID
            , string planUID, string planName, string planNumber, string comment, string user
           , string statusFlag, string ownerOrganisationUID, string corporateUID, string payorAgreementUId, string activeFrom, string activeTo
           , string claimPercentage, string fixedCopayAmount)
        {
            string UID = string.Empty ;
            try
            {

                this.Connect();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.Text;
                com.CommandText = @"INSERT INTO PatientVisitPayor
                                   ([PatientVisitUID]
                                   ,[PatientUID]
                                   ,[PayorUID]
                                   ,[PlanUID]
                                   ,[PlanName]
                                   ,[PlanNumber]
                                   ,[Comments]
                                   ,[CUser]
                                   ,[CWhen]
                                   ,[MUser]
                                   ,[MWhen]
                                   ,[StatusFlag]
                                   ,[OwnerOrganisationUID]
                                   ,[CorporateUID]
                                   ,[PBLCTUID]
                                   ,[PAYRTPUID]
                                   ,[PayorAgreementUID]
                                   ,[ActiveFrom]
                                   ,[ActiveTo]
                                   ,[ClaimPercentage]
                                   ,[FixedCopayAmount])
                                    Values
                                    (@PatientVisitUID
                                   ,@PatientUID
                                   ,@PayorUID
                                   ,@PlanUID
                                   ,@PlanName
                                   ,@PlanNumber
                                   ,@Comments
                                   ,'" + user + @"'
                                   ,getdate()
                                   ,'" + user + @"'
                                   ,getdate()
                                   ,@StatusFlag
                                   ,'" + ownerOrganisationUID +@"'
                                   ,@CorporateUID
                                   ,'0'
                                   ,(select top 1 UID from ReferenceValue where ValueCode = 'PRIMARY' and DomainCode = 'PAYRTP' and StatusFlag = 'A')
                                   ,@PayorAgreementUID
                                   ,@ActiveFrom
                                   ,@ActiveTo
                                   ,@ClaimPercentage
                                   ,@FixedCopayAmount)

                                    Select SCOPE_IDENTITY() AS [SCOPE]";

                com.Parameters.AddWithValue("@PatientVisitUID", patientvisitUID);
                com.Parameters.AddWithValue("@PatientUID", patientUID);
                com.Parameters.AddWithValue("@PayorUID", payorUID);
                com.Parameters.AddWithValue("@PlanName", planName);
                com.Parameters.AddWithValue("@PlanNumber", planNumber);
                com.Parameters.AddWithValue("@Comments", comment);
                //com.Parameters.AddWithValue("@CUser", user);
                //com.Parameters.AddWithValue("@MUser", user);
                //com.Parameters.AddWithValue("@OwnerOrganisationUID", ownerOrganisationUID);
                com.Parameters.AddWithValue("@CorporateUID", corporateUID);
                com.Parameters.AddWithValue("@PayorAgreementUID", payorAgreementUId);


                if (string.IsNullOrEmpty(activeFrom))
                {
                    com.Parameters.Add("@ActiveFrom", SqlDbType.DateTime).Value = DateTime.Now;
                }
                else
                {
                    com.Parameters.Add("@ActiveFrom", SqlDbType.DateTime).Value = activeFrom;
                }
                if (string.IsNullOrEmpty(activeTo))
                {
                    com.Parameters.Add("@ActiveTo", SqlDbType.DateTime).Value = DBNull.Value;
                }
                else
                {
                    com.Parameters.Add("@ActiveTo", SqlDbType.DateTime).Value = activeTo;
                }


                com.Parameters.AddWithValue("@ClaimPercentage", claimPercentage);
                com.Parameters.AddWithValue("@FixedCopayAmount", fixedCopayAmount);
                com.Parameters.AddWithValue("@PlanUID", planUID);
                com.Parameters.AddWithValue("@StatusFlag", statusFlag);


                UID = com.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {


                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Disconnect();
            }
            return UID;
        }
        public bool Insert_BillableItem(string PatientUID, string PatientVisitUID, string IdentifyingUID, string IdentifyingType, string User, string OwnerOrganisationUID
          , string BSMDDUID, string Amount, string NetAmount, string BillableItemUID, string ItemMultiplier, string VISITYUID
          , string RSLVLUID, string ItemName, string ConsultantShare
          , string StoreUID, string BillPackageUID, string OrderType
            , string OrderTypeUID, string PatientOrderDetailUID
            , string ORDSTUID)
        {
            bool flag = false;
            try
            {
                this.Connect();
                SqlCommand cmd = new SqlCommand();
                string statement = string.Empty;
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                statement = @"INSERT INTO [PatientBillableItem]
                               ([PatientUID]
                               ,[PatientVisitUID]
                               ,[IdentifyingUID]
                               ,[IdentifyingType]
                               ,[CUser]
                               ,[CWhen]
                               ,[MUser]
                               ,[MWhen]
                               ,[StatusFlag]
                               ,[OwnerOrganisationUID]
                               ,[BSMDDUID]
                               ,[Amount]
                               ,[NetAmount]
                               ,[BillableItemUID]
                               ,[ItemMultiplier]
                               ,[StartDttm]
                               ,[EndDttm]
                               ,[VISITYUID]
                               ,[RSLVLUID]
                               ,[ItemName]
                               ,[CareProviderUID]
                               ,[ConsultantShare]
                               ,[EventOccuredDttm]
                               ,[QNUOMUID]
                               ,[PayorUID]
                               ,[PatientVisitPayorUID]
                               ,[StoreUID]
                               ,[BillPackageUID]
                               ,[SplitDiscount]
                               ,[OrderType]
                               ,[OrderTypeUID]
                               ,[PatientOrderDetailUID]
                               ,[ORDSTUID]
                               ,[ENTYPUID]
                               ,[OrderSetBillableItemUID]
                               ,[OrderSetUID])
                         VALUES
                               (" + PatientUID + @",
                                " + PatientVisitUID + @",
                                " + IdentifyingUID + @",
                                " + IdentifyingType + @",
                                " + User + @",
                                getdate(),
                                " + User + @",
                                getdate(),
                                'A',
                                " + OwnerOrganisationUID + @",
                                " + BSMDDUID + @",
                                " + Amount + @",
                                " + NetAmount + @",
                                " + BillableItemUID + @",
                                " + ItemMultiplier + @",
                                " + "getdate()" + @",
                                " + "dateadd(ss,86399,getdate())" + @",
                                " + VISITYUID + @",
                                " + RSLVLUID + @",
                                " + "@itemname" + @",
                                " + "isnull((select top 1 CareproviderUID from PatientVisit where PatientVisit.UID = '" + PatientVisitUID + "' ),0)" + @",
                                " + ConsultantShare + @",
                                getdate(),
                                " + "(select top 1 QNUOMUID from BillableItem where BillableItem.UID = '" + BillableItemUID + "' and BillableItem.Statusflag = 'A')" + @",
                                " + "isnull((select top 1 PayorUID from PatientVisitPayor where PatientVisitPayor.PatientUID='" + PatientUID + "' and PatientVisitPayor.PatientVisitUID = '" + PatientVisitUID + "' and PatientVisitPayor.StatusFlag = 'A' order by PatientVisitPayor.PAYRTPUID asc  ),0)" + @",
                                " + "isnull((select top 1 UID from PatientVisitPayor where PatientVisitPayor.PatientUID='" + PatientUID + "' and PatientVisitPayor.PatientVisitUID = '" + PatientVisitUID + "' and PatientVisitPayor.StatusFlag = 'A' order by PatientVisitPayor.PAYRTPUID asc  ),0)" + @",
                                " + "null" + @",
                                " + BillPackageUID + @",
                                " + ItemMultiplier + @",
                                " + OrderType + @",
                                " + OrderTypeUID + @",
                                " + PatientOrderDetailUID + @",
                                " + "(select UID from ReferenceValue where Description = '" + ORDSTUID +"' and DomainCode = 'ORDST' and StatusFlag = 'A')" + @",
                                " + "isnull((select top 1 ENTYPUID from PatientVisit where PatientVisit.UID = '" + PatientVisitUID + "' ),0)" + @",
                                " + "null" + @",
                                " + "null" + ")";
                cmd.Parameters.AddWithValue("@itemname", ItemName);
                cmd.CommandText = statement;
                cmd.ExecuteNonQuery();
                flag = true;
            }
            catch (Exception ex)
            {


                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Disconnect();
            }
            return flag;
        }
        public bool Insert_PatientBillableItem(string ScheduleOrderDetailUID, string PatientPackageUID
                    , string PatientOrderDetailUID,string TableTranscationType,string TableUIDTransactionType,string OrderStatusUID,string own)
        {
            SqlCommand cmd = new SqlCommand();
            string path =
            @"insert PatientBillableItem (PatientUID,PatientVisitUID,IdentifyingUID,IdentifyingType,CUser,CWhen,MUser,MWhen,StatusFlag,OwnerOrganisationUID,ProcedureUID,BSMDDUID,Amount,Discount,NetAmount,Description,BillableItemUID,Comments,ItemMultiplier,StartDttm,EndDttm,DiscountAuthorizedBy,ItemUID,VISITYUID,RSLVLUID,ItemName,CareProviderUID,ConsultantShare,EventOccuredDttm,QNUOMUID,ConsultantDiscount,EducationCess,HigherEducationCess,ServiceTax,PayorUID,PatientVisitPayorUID,BatchID,VATPercentage,InternalCost,CalculateTaxOnMRP,StoreUID,BillPackageUID,SplitDiscount,BLTYPUID,OrderType,OrderTypeUID,ReceiptNumber,RefundBillNumber,PatientOrderDetailUID,IPFillProcessUID,ORDSTUID,IsModified,PreviousVisitUID,ENTYPUID,AllocatedPatBillableItemUID,ExpiryDttm,GroupUID,SubGroupUID,GroupMaxCoverage,GroupCovered,SubGroupMaxCoverage,SubGroupCovered,ItemMaxCoverage,ItemCovered,PBLCTUID,ALLDIUID)
            select PatientScheduleOrder.PatientUID PatientUID,PatientScheduleOrder.PatientVisitUID PatientVisitUID
			,PatientScheduleOrderDetail.IdentifyingUID IdentifyingUID
			,case when PatientScheduleOrderDetail.IdentifyingType ='ORDERITEM' then 'ORDITEM' ELSE PatientScheduleOrderDetail.IdentifyingType END IdentifyingType
			,1 CUser,getdate() CWhen,1 MUser,getdate() MWhen,'A' StatusFlag,'" + own + @"' OwnerOrganisationUID
			,null ProcedureUID
			,(select top 1 BSMDDUID from BillableItem where BillableItem.UID = PatientScheduleOrderDetail.BillableItemUID) BSMDDUID
			,isnull((select top 1 BillPackageItem.Amount from BillPackage,BillPackageItem where BillPackage.UID = BillPackageItem.BillPackageUID 
			and BillPackage.UID = PatientScheduleOrderDetail.BillPackageUID 
			and BillPackageItem.BillableItemUID = PatientScheduleOrderDetail.BillableItemUID
			and BillPackageItem.ActiveFrom <= getdate() and (BillPackageItem.ActiveTo is null or BillPackageItem.ActiveTo >= getdate() )
			and BillPackage.StatusFlag = 'A' ),0) Amount
			,NULL Discount
			,isnull((select top 1 (BillPackageItem.Quantity * BillPackageItem.Amount) from BillPackage,BillPackageItem where BillPackage.UID = BillPackageItem.BillPackageUID 
			and BillPackage.UID = PatientScheduleOrderDetail.BillPackageUID 
			and BillPackageItem.BillableItemUID = PatientScheduleOrderDetail.BillableItemUID
			and BillPackageItem.ActiveFrom <= getdate() and (BillPackageItem.ActiveTo is null or BillPackageItem.ActiveTo >= getdate() )
			and BillPackage.StatusFlag = 'A' ),0) NetAmount
			,null Description
			,PatientScheduleOrderDetail.BillableItemUID BillableItemUID
			,null Comments
			,isnull((select top 1 BillPackageItem.Quantity  from BillPackage,BillPackageItem where BillPackage.UID = BillPackageItem.BillPackageUID 
			and BillPackage.UID = PatientScheduleOrderDetail.BillPackageUID 
			and BillPackageItem.BillableItemUID = PatientScheduleOrderDetail.BillableItemUID
			and BillPackageItem.ActiveFrom <= getdate() and (BillPackageItem.ActiveTo is null or BillPackageItem.ActiveTo >= getdate() )
			and BillPackage.StatusFlag = 'A' ),0) ItemMultiplier
			,getdate() StartDttm,dateadd(ss,86399,getdate()) EndDttm
			,null DiscountAuthorizedBy
			,(select top 1 ItemUID from BillableItem where BillableItem.UID = PatientScheduleOrderDetail.BillableItemUID) ItemUID
			,null VISITYUID,null RSLVLUID
			,(select top 1 ItemName from BillableItem where BillableItem.UID = PatientScheduleOrderDetail.BillableItemUID) ItemName
			,isnull((select top 1 CareproviderUID from PatientVisit where PatientVisit.UID = PatientScheduleOrder.PatientVisitUID ),0) CareProviderUID
			,null ConsultantShare,getdate() EventOccuredDttm
			,(select top 1 QNUOMUID from BillableItem where BillableItem.UID = PatientScheduleOrderDetail.BillableItemUID) QNUOMUID
			,null ConsultantDiscount,null EducationCess,null HigherEducationCess
			,null ServiceTax
			,isnull((select top 1 PayorUID from PatientVisitPayor where PatientVisitPayor.PatientUID=PatientScheduleOrder.PatientUID 
			and PatientVisitPayor.PatientVisitUID = PatientScheduleOrder.PatientVisitUID 
			and PatientVisitPayor.StatusFlag = 'A' order by PatientVisitPayor.PAYRTPUID asc  ),0) PayorUID
			,isnull((select top 1 UID from PatientVisitPayor where PatientVisitPayor.PatientUID=PatientScheduleOrder.PatientUID 
			and PatientVisitPayor.PatientVisitUID = PatientScheduleOrder.PatientVisitUID 
			and PatientVisitPayor.StatusFlag = 'A' order by PatientVisitPayor.PAYRTPUID asc  ),0) PatientVisitPayorUID
			,null BatchID,null VATPercentage,null InternalCost,null CalculateTaxOnMRP,0 StoreUID
			,@xpackageuid BillPackageUID  
			,null SplitDiscount,null BLTYPUID
			,@TableTranscationType OrderType,@TableUIDTransactionType OrderTypeUID
			,null ReceiptNumber,null RefundBillNumber
			,@xPatientOrderDetailUID PatientOrderDetailUID,null IPFillProcessUID
			,(select UID from ReferenceValue where Description = '@OrderStatusUID' and DomainCode = 'ORDST' and StatusFlag = 'A') ORDSTUID,null IsModified,null PreviousVisitUID
			,isnull((select top 1 ENTYPUID from PatientVisit where PatientVisit.UID = PatientScheduleOrder.PatientVisitUID ),0) ENTYPUID
			,null AllocatedPatBillableItemUID,null ExpiryDttm
			,null GroupUID,null SubGroupUID,null GroupMaxCoverage
			,null GroupCovered,null SubGroupMaxCoverage,null SubGroupCovered,null ItemMaxCoverage,null ItemCovered
			,null PBLCTUID,null ALLDIUID
from  PatientScheduleOrder,PatientScheduleOrderDetail where PatientScheduleOrder.UID=PatientScheduleOrderDetail.PatientScheduleOrderUID 
							  and PatientScheduleOrder.StatusFlag ='A' and PatientScheduleOrderDetail.StatusFlag = 'A' and PatientScheduleOrderDetail.UID = '" + ScheduleOrderDetailUID + "'";
            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                path=path.Replace("@OrderStatusUID",OrderStatusUID);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@xpackageuid", PatientPackageUID);
                cmd.Parameters.AddWithValue("@xPatientOrderDetailUID", PatientOrderDetailUID);
                cmd.Parameters.AddWithValue("@TableTranscationType", TableTranscationType);
                cmd.Parameters.AddWithValue("@TableUIDTransactionType", TableUIDTransactionType);
                
                
                cmd.CommandText = path;
                cmd.ExecuteNonQuery();

            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return flag;
        }
        public string Insert_Request(string PatientVisitUID, string PatientUID, string cuser
                                        , string RequestNumber, string LRQSTUID, string OrderLocationUID
                                        , string OrderToLocationUID,string own)
        {
            SqlCommand cmd = new SqlCommand();
            string requestUID = string.Empty;
            string path =
            @"insert Request (PatientVisitUID,PatientUID,RequestedUserUID,RequestedDttm,AuthorisedDttm,IsAuthorised,CUser,CWhen,MUser,MWhen,StatusFlag,OwnerOrganisationUID,RequestNumber,Comments,LRQSTUID,ReviewedUserUID,OrderLocationUID,OrderToLocationUID,IsHistory,ReviewedDoctorUID,SampleReviewedDttm)";
            path += " select " + PatientVisitUID + " PatientVisitUID," + PatientUID + " PatientUID,0 RequestedUserUID,getdate() RequestedDttm,null AuthorisedDttm,'N' IsAuthorised," + cuser + " CUser,getdate() CWhen," + cuser + " MUser,getdate() MWhen,'A' StatusFlag,'" + own +"' OwnerOrganisationUID,'" + RequestNumber + "' RequestNumber,'mass convert' Comments,(select UID from ReferenceValue where Description = '" + LRQSTUID + "' and DomainCode = 'LRQST' and StatusFlag = 'A') LRQSTUID,0 ReviewedUserUID," + OrderLocationUID + " OrderLocationUID," + OrderToLocationUID + " OrderToLocationUID,null IsHistory,null ReviewedDoctorUID,null SampleReviewedDttm  ";
            path+=" Select SCOPE_IDENTITY() AS [SCOPE]";
            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = path;
                requestUID = cmd.ExecuteScalar().ToString();


            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return requestUID;
        }
        public string Insert_RequestDetail(string RequestitemUID,string RequestUID
            ,string cuser,string RequestItemName,string RSSTSUID,string own)
        {
            string ret = string.Empty;
            SqlCommand cmd = new SqlCommand();
            string path =
            @"insert RequestDetail (RequestitemUID,RequestUID,RECATUID,ResultRequiredDttm,RequestedDttm,RPRITUID,RequestedUserUID,Comments
,CUser,CWhen,MUser,MWhen,StatusFlag,OwnerOrganisationUID,RequestItemName
,InterimRequiredDttm,PreparedDttm,ProcessingNote,RSSTSUID,CareProviderGroupUID,RequestItemCode)

select @xRequestitemUID RequestitemUID,@xRequestUID RequestUID,null RECATUID,getdate() ResultRequiredDttm,getdate() RequestedDttm
,(select UID from ReferenceValue where Valuecode = 'ROUTN' and statusflag = 'A' and DomainCode = 'RQPRT') RPRITUID
,0 RequestedUserUID,'' Comments,@xCUser CUser,getdate() CWhen
,@xCUser MUser,getdate() MWhen,'A' StatusFlag,'" + own + @"' OwnerOrganisationUID
,@RequestItemName RequestItemName,getdate() InterimRequiredDttm,getdate() PreparedDttm
,null ProcessingNote,(select top 1 UID from ReferenceValue where Description = '@xRSSTSUID' and DomainCode = 'RSSTS' and StatusFlag = 'A') RSSTSUID,null CareProviderGroupUID,(select top 1 ItemCode from Requestitem where Requestitem.UID = @xRequestitemUID) RequestItemCode

Select SCOPE_IDENTITY() AS [SCOPE]";
            bool flag = true;
            try
            {
                path = path.Replace("@xRequestitemUID", RequestitemUID);
                path = path.Replace("@xRequestUID", RequestUID);
                path = path.Replace("@xCUser", cuser);
                path = path.Replace("@xRSSTSUID", RSSTSUID);

                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@RequestItemName", RequestItemName);
                cmd.CommandText = path;
                //cmd.ExecuteNonQuery();
                ret = cmd.ExecuteScalar().ToString();

            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return ret;
        }
        public string insert_PatientOrder(string HN, string EN, string date, string time, string phoneNumber, string cUser, string comments)
        {
            SqlCommand cmd = new SqlCommand();
            string SEQ = SEQID("SEQpatientorder");
            try
            {
                int years = int.Parse(date.Substring(1, 4));
                int month = int.Parse(date.Substring(5, 2));
                int day = int.Parse(date.Substring(7, 2));
                int hour = int.Parse(time.Substring(1, 2));
                int minute = int.Parse(time.Substring(3, 2));
                int second = int.Parse(time.Substring(5, 2));
                string startDttm = date + time;
                DateTime datetime = new DateTime(years, month, day, hour, minute, second);
                //DateTime datetime = DateTime.ParseExact(startDttm, "yyyy:MM:dd HH:mm:ss", null);
                string sqlStateMent = @"insert into patientorder 
									(
										OrderNumber,
										RequestedDttm,
										ORDCTUID,
										ORDSTUID,
										ORDPRUID,
										PatientUID,
										PatientVisitUID,
										StartDttm,
										EndDttm,
										IdentifyingType,
										OrderRaisedBy,
										ServiceUID,
										Cuser,
										Cwhen,
										Muser,
										Mwhen,
										Statusflag,
										OwnerOrganisationUID,
										OrderLocationUID,
                                        OrderToLocationUID,
                                        comments
									)
									Values
									(
										" + SEQ + @",
										@requestdate,
										(select UID from referencevalue where domaincode = 'OrDCT' and description like N'%โทรศัพท์%' and statusflag = 'A'),
										(select UID from referencevalue where domaincode = 'ORDST' and valuecode = 'RAISED' and statusflag='A'),
										(select UID from referencevalue where domaincode = 'RQPRT' and valuecode = 'ROUTN' and statusflag = 'A'),
										(select UID from patient where pasid = N'" + HN + @"'),
										(select UID from patientvisit where uid = (select patientvisitUID from patientvisitid where identifier = '" + EN + @"' )) ,
										@datetime1,
										@datetime2,
										'PATIENTORDER',
										0,
										0,
										" + cUser + @" ,
										getdate(),
										9898,
										getdate(),
										'A',
										9,
										(select top 1 locationUID from locationprofile where phoneNumber = '" + phoneNumber + @"')	,
                                           (select UID from Location where Name ='Cashier IPD' and StatusFlag = 'A')
                                            ,@comments
									)
									";

                Connect();
                cmd.Connection = con;
                cmd.CommandText = sqlStateMent;

                cmd.Parameters.AddWithValue("@datetime1", datetime);
                cmd.Parameters.AddWithValue("@datetime2", datetime);
                cmd.Parameters.AddWithValue("@requestdate", datetime);
                cmd.Parameters.AddWithValue("@comments", comments);
                cmd.ExecuteNonQuery();

            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());

            }
            finally
            {
                Disconnect();
            }

            return SEQ;
        }
        public bool insert_PatientOrder(string SEQ,string patientUID , string patientVisitUID
            , string cUser, string comments
            , string ordercategoryUID, string IdentifyingType, string OrderLocationUID
            , string OrderToLocationUID,string own,out string patorderUID)
        {
            SqlCommand cmd = new SqlCommand();
            bool flag = false;
            patorderUID ="";
            //string SEQ = SEQID("SEQpatientorder");
            try
            {
                
                
                //DateTime datetime = DateTime.ParseExact(startDttm, "yyyy:MM:dd HH:mm:ss", null);
                string sqlStateMent = @"insert into patientorder 
									(
										OrderNumber,
										RequestedDttm,
										ORDCTUID,
										ORDSTUID,
										ORDPRUID,
										PatientUID,
										PatientVisitUID,
										StartDttm,
										EndDttm,
										IdentifyingType,
										OrderRaisedBy,
										ServiceUID,
										Cuser,
										Cwhen,
										Muser,
										Mwhen,
										Statusflag,
										OwnerOrganisationUID,
										OrderLocationUID,
                                        OrderToLocationUID,
                                        comments
									)
									Values
									(
										" + SEQ + @",
										getdate(),
										@ordercategoryUID,
										(select UID from referencevalue where domaincode = 'ORDST' and valuecode = 'RAISED' and statusflag='A'),
										(select UID from referencevalue where domaincode = 'RQPRT' and valuecode = 'ROUTN' and statusflag = 'A'),
										@patuid,
										@patvisituid ,
										getdate(),
										getdate(),
										@IdentifyingType,
										0,
										0,
										" + cUser + @" ,
										getdate(),
										9898,
										getdate(),
										'A',
										'" + own + @"',
										@OrderLocationUID	,
                                           @OrderToLocationUID
                                            ,@comments
									)
									
                                    Select SCOPE_IDENTITY() AS [SCOPE]";

                Connect();
                cmd.Connection = con;
                cmd.CommandText = sqlStateMent;
                cmd.Parameters.AddWithValue("@patuid",patientUID);
                cmd.Parameters.AddWithValue("@patvisituid", patientVisitUID);
                cmd.Parameters.AddWithValue("@comments", comments);
                cmd.Parameters.AddWithValue("@ordercategoryUID", ordercategoryUID);
                cmd.Parameters.AddWithValue("@IdentifyingType",IdentifyingType);
                cmd.Parameters.AddWithValue("@OrderLocationUID", OrderLocationUID);
                cmd.Parameters.AddWithValue("@OrderToLocationUID", OrderToLocationUID);

                //cmd.ExecuteNonQuery();
                patorderUID = cmd.ExecuteScalar().ToString();

                flag = true;
            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message.ToString());

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }

            return flag ;
        }
        public string insert_PatientOrderDetail(string SEQ, string identyfierUID, string identyfierType, string cuser
                        , string billableItemUID, string comments
                        , string OrderStatus, string Quantity, string OrderSubCategoryUID, string OrderCategoryUID
                        , string Unitprice, string NetAmount, string OriginalUnitPrice,string PatientPackageUID,string own
                        ,string visitpayorUID )
        {
            SqlCommand cmd = new SqlCommand();
            string patientOrderDetail = string.Empty;
            string sqlStateMent = string.Empty;
            try
            {
                sqlStateMent = @"
												INSERT INTO  [PatientOrderDetail]
											   ([PatientOrderUID]
											   ,[ItemName]
											   ,[IdentifyingUID]
											   ,[IdentifyingType]
											   ,[StartDttm]
											   ,[EndDttm]
		  
											   ,[ORDSTUID]
											   ,[ORDPRUID]
                        					   ,[Quantity]
											   ,[QNUOMUID]
		                					   ,[UnitPrice]
											   ,[NetAmount]
                        					   ,[OrderSubCategoryUID]
											   ,[OrderCategoryUID]
											   ,[CUser]
											   ,[CWhen]
											   ,[MUser]
											   ,[MWhen]
											   ,[StatusFlag]
											   ,[OwnerOrganisationUID]
                                               ,[Comments]
											   ,[OriginalUnitPrice]
											   ,[BillableItemUID]
                                               ,[PatientPackageUID]
                                                ,[PatientVisitPayorUID]
                                                ,BillpackageUID
											   )
										 VALUES
											   (
											   (select top 1 UID from patientorder where orderNUMBER = " + SEQ + @")
											   ,(select top 1 ItemName from BillableItem where BillableItem.UID = '" + billableItemUID + @"')
                                                ," + identyfierUID + @"
											   ,'" + identyfierType + @"'
											   ,GETDATE()
											   ,GETDATE()
											   ,(select top 1 UID from referencevalue where domaincode = 'ORDST' and Description =  '" + OrderStatus  + @"' and statusflag='A')
											   ,(select top 1 UID from referencevalue where domaincode = 'RQPRT' and valuecode = 'ROUTN' and statusflag = 'A')
											   ," + Quantity + @"
											   ,(select top 1 QNUOMUID from BillableItem where BillableItem.UID = '" + billableItemUID + @"')
											   ," + Unitprice + @"
											   ," + NetAmount + @"
											   ," + OrderSubCategoryUID + @"
											   ," + OrderCategoryUID + @"
											   ," + cuser + @"
											   ,getdate()
											   ,1
											   ,getdate()
											   ,'A'
											   ,'" + own + @"'
                                               ,@cm
											   ," + OriginalUnitPrice + @"
											   ," + billableItemUID + @"
                                               ," + PatientPackageUID + @"          
                                               ," + visitpayorUID + @"
                                               ,(select top 1 BillpackageUID from patientpackage where patientpackage.UId = '"+PatientPackageUID+@"')
											   )
												
												select  SCOPE_IDENTITY() iden";


                Connect();
                cmd.Connection = con;
                cmd.CommandText = sqlStateMent;
                cmd.Parameters.AddWithValue("@cm", comments);
                DataTable dtid = new DataTable();
                dtid.Load(cmd.ExecuteReader());
                if (dtid != null && dtid.Rows.Count > 0)
                    patientOrderDetail = dtid.Rows[0]["iden"].ToString();
                else
                    patientOrderDetail = "-1";
                //

            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                MessageBox.Show(sqlStateMent.ToString());

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return patientOrderDetail;
        }
        public bool Insert_ResultTextual(string ResultComponentUID
                                           , string TextualValue)
        {
            SqlCommand cmd = new SqlCommand();
            string path =
            @"          
            INSERT [ResultTextual]
                       ([ResultComponentUID]
                       ,[TextualValue]
                       ,[CUser]
                       ,[MUser]
                       ,[CWhen]
                       ,[MWhen]
                       ,[StatusFlag]
                       ,[OwnerOrganisationUID]
                       ,[TemplateXML]
                       ,[IsDraft]
                       ,[HasHistory]
                       ,[ResultVersion])
                select 
	                   '" + ResultComponentUID + @"' [ResultComponentUID]
                      ,@TextualValue [TextualValue]
                      , 989899 [CUser]
                      , 989899 [MUser]
                      , getdate() [CWhen]
                      , getdate() [MWhen]
                      , 'A' [StatusFlag]
                      , 9 [OwnerOrganisationUID]
                      ,null  [TemplateXML]
                      ,null [IsDraft]
                      ,null [HasHistory]
                      ,null [ResultVersion]";
            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = path;
                cmd.Parameters.AddWithValue("@TextualValue", TextualValue);

                cmd.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return flag;
        }
         public string Insert_PatientVisit(string patientUID, string entypUID, string careProviderUID, string ServiceUID , string locationUID , string vistyUID , string organisationUID
            , string comments,  string user, string ownerOrganisationUID, string priorityUID )
        {
            string visitUID = string.Empty;
            try
            {

                this.Connect();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.Text;
                com.CommandText = @"INSERT INTO [dbo].[PatientVisit]
                                   ([PatientUID]
                                  ,[ENTYPUID]
                                  ,[CareProviderUID]
                                  ,[ServiceUID]
                                  ,[LocationUID]
                                  ,[StartDTTM]
                                  ,[OrganisationUID]
                                  ,[ArrivedDttm]       
                                  ,[Comments]
                                  ,[VISTYUID]
                                  ,[OUTCMUID]
                                  ,[CUser]
                                  ,[CWhen]
                                  ,[MUser]
                                  ,[MWhen]
                                  ,[StatusFlag]
                                  ,[OwnerOrganisationUID] 
                                  ,[CurrentLocationUID]
                                  ,[CurrentServiceUID]
                                  ,[PriorityUID]
                                  ,[PACLSUID] )
                               VALUES
                                    (@PatientUID
                                  ,@ENTYPUID
                                  ,@CareProviderUID
                                  ,@ServiceUID
                                  ,@LocationUID
                                  ,getdate()
                                  ,@OrganisationUID
                                  ,getdate()       
                                  ,@Comments
                                  ,@VISTYUID
                                  ,'0'
                                  ,@CUser
                                  ,getdate()
                                  ,@MUser
                                  ,getdate()
                                  ,'A'
                                  ,@OwnerOrganisationUID
                                  ,@CurrentLocationUID
                                  ,@CurrentServiceUID
                                  ,@PriorityUID
                                   ,(Select top 1 UID From ReferenceValue where Statusflag ='A' and domaincode = 'BKSTS' and Description = 'Medical Discharge'))
                                    
                                   Select SCOPE_IDENTITY() AS [SCOPE]";


                com.Parameters.AddWithValue("@PatientUID", patientUID);
                com.Parameters.AddWithValue("@ENTYPUID", entypUID);
                com.Parameters.AddWithValue("@CareProviderUID", careProviderUID);
                com.Parameters.AddWithValue("@ServiceUID", ServiceUID);
                com.Parameters.AddWithValue("@LocationUID", locationUID);
                com.Parameters.AddWithValue("@OrganisationUID", organisationUID);
                com.Parameters.AddWithValue("@Comments", comments);
              
                com.Parameters.AddWithValue("CUser", user);
                com.Parameters.AddWithValue("@VISTYUID", vistyUID);
                com.Parameters.AddWithValue("@MUser", user);
                com.Parameters.AddWithValue("@OwnerOrganisationUID", ownerOrganisationUID);
                com.Parameters.AddWithValue("@CurrentLocationUID", locationUID);
                com.Parameters.AddWithValue("@CurrentServiceUID", ServiceUID);
                com.Parameters.AddWithValue("@PriorityUID", priorityUID);

                visitUID = com.ExecuteScalar().ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                
                this.Disconnect();
            }
            return visitUID;
        }
        public bool Insert_PatientVisitID(string Identifier, string mainIdentifier, string patientVisitUID, string user, string ownerOraganisationUID)
        {
            var result = false;
            try
            {
                this.Connect();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandType = CommandType.Text;
                com.CommandText = @"INSERT INTO [dbo].[PatientVisitID]
                                   ([Identifier]
                                   ,[VISIDUID]
                                   ,[MainIdentifier]
                                   ,[ActiveFrom]
                                   ,[PatientVisitUID]
                                   ,[CUser]
                                   ,[CWhen]
                                   ,[MUser]
                                   ,[MWhen]
                                   ,[StatusFlag]
                                   ,[OwnerOrganisationUID])
                             VALUES
                                   (@Identifier
                                    ,(select Top 1 UID from ReferenceValue where DomainCode = 'VISIT' and StatusFlag = 'A')
                                    ,@MainIdentifier
                                    ,getdate()
                                    ,@PatientVisitUID
                                    ,@CUser
                                    ,getdate()
                                    ,@mUser
                                    ,getdate()
                                    ,'A'
                                    ,@OwnerOrganisationUID)";

                com.Parameters.AddWithValue("@Identifier", Identifier);
                com.Parameters.AddWithValue("@MainIdentifier", mainIdentifier);
                com.Parameters.AddWithValue("@PatientVisitUID", patientVisitUID);
                com.Parameters.AddWithValue("@CUser", user);
                com.Parameters.AddWithValue("@MUSER", user);
                com.Parameters.AddWithValue("@OwnerOrganisationUID", ownerOraganisationUID);

                com.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Disconnect();
            }
            return result;
        }
 public bool Insert_BDMSASTMassConvert(string PatientScheduleOrderUID, string PatientScheduleOrderNo, string LabNo
     , string IsConvertSuccess, string statusflag, string SpecimenCount, string hn, string patientuid, string patfull, string SpecimenItemList,string TestSetList)
 {
     SqlCommand cmd = new SqlCommand();
     string path =
     @"insert BDMSASTMassConvert (PatientScheduleOrderUID,PatientScheduleOrderNo,LabNo,IsConvertSuccess,Statusflag,ConvertDate,SpecimenCount,HN,PatientUID,PatientFullName,SpecimenItemList,TestSetList)
        select '" + PatientScheduleOrderUID + "' PatientScheduleOrderUID,'" + PatientScheduleOrderNo + "' PatientScheduleOrderNo,'" + LabNo + "' LabNo,'" + IsConvertSuccess + "' IsConvertSuccess,'A' Statusflag,getdate() ConvertDate,'" + SpecimenCount + "' SpecimenCount,'" + hn + "' HN," + patientuid + " PatientUID,@patfull PatientFullName,@SpecimenItemList SpecimenItemList,@TestSetList TestSetList ";

     bool flag = true;
     try
     {
         Connect();
         cmd.Connection = con;
         cmd.CommandType = CommandType.Text;
         cmd.Parameters.AddWithValue("@patfull", patfull);
         cmd.Parameters.AddWithValue("@SpecimenItemList", SpecimenItemList);
         cmd.Parameters.AddWithValue("@TestSetList", TestSetList);
         cmd.CommandText = path;
         cmd.ExecuteNonQuery();

     }
     catch (Exception er)
     {
         System.Windows.Forms.MessageBox.Show(er.Message.ToString());
         flag = false;

     }
     finally
     {
         cmd.Dispose();
         Disconnect();
     }
     return flag;
 }
 public bool Insert_BDMSASTMassSpecimenTestSet(string hn, string Specimen, string TestSet, string PatientScheduleOrderUID)
 {
     SqlCommand cmd = new SqlCommand();
     string path = @"insert BDMSASTMassSpecimenTestSet (HN,Specimen,TestSet,PatientScheduleOrderUID) select @vhn HN,@vSpecimen  Specimen,@vTestSet TestSet,@vPatientScheduleOrderUID PatientScheduleOrderUID";
     
     bool flag = true;
     try
     {
         Connect();
         cmd.Connection = con;
         cmd.CommandType = CommandType.Text;
         cmd.Parameters.AddWithValue("@vhn", hn);
         cmd.Parameters.AddWithValue("@vSpecimen", Specimen);
         cmd.Parameters.AddWithValue("@vTestSet", TestSet);
         cmd.Parameters.AddWithValue("@vPatientScheduleOrderUID", PatientScheduleOrderUID);
         cmd.CommandText = path;
         cmd.ExecuteNonQuery();

     }
     catch (Exception er)
     {
         System.Windows.Forms.MessageBox.Show(er.Message.ToString());
         flag = false;

     }
     finally
     {
         cmd.Dispose();
         Disconnect();
     }
     return flag;
 }
public bool Insert_BDMSPatientScheduleOrderDetail(int PatientScheduleOrderUID, string PackageName ,int PackageUID, DateTime DOE, string PatientUID)
 {
     SqlCommand cmd = new SqlCommand();
     DateTime ChkDate = new DateTime();
     ChkDate = Convert.ToDateTime(DOE);
     string path = "INSERT INTO [BDMSPatientScheduleOrderDetail] "
                 + "([PatientScheduleOrderUID] ,[ItemName] ,[BillPackageUID] ,[StartDttm] ,[EndDttm] , [Comments] ,[BookingUID] ,[CUser] "
                 + " ,[CWhen] ,[MUser] ,[MWhen] ,[StatusFlag] ,[OwnerOrganisationUID] ,[PatientUID] ,[PatientVisitUID] ,[BillableItemUID]) "
                 + " VALUES (" + PatientScheduleOrderUID + " ,N'" + PackageName + "' ,'" + PackageUID + "' ,'" + ChkDate + "' ,'" + ChkDate + "' , "
                 + " 'Mass Convert' ,0 ,521 ,GETDATE() ,521 ,GETDATE() ,'A' ,15 ,'" + PatientUID + "' ,0 ,'" + PackageUID + "')";
     bool flag = true;
     try
     {
         Connect();
         cmd.Connection = con;
         cmd.CommandType = CommandType.Text;

         cmd.CommandText = path;
         //cmd.Parameters.AddWithValue("@ResultValue", ResultValue);
         //cmd.Parameters.AddWithValue("@ResultItemName", ResultItemName);
         //cmd.Parameters.AddWithValue("@ReferenceRange", ReferenceRange);
         cmd.ExecuteNonQuery();
     }
     catch (Exception er)
     {
         flag = false;
         er.ToString();
     }
     finally
     {
         cmd.Dispose();
         Disconnect();
     }
     return flag;
}
public bool Update_BDMSPatientScheduleOrderDetail(int PatientScheduleOrderUID, string PackageName, int PackageUID, DateTime DOE, string PatientUID, int UID, string ItemName)
{
    SqlCommand cmd = new SqlCommand();
    DateTime ChkDate = new DateTime();
    ChkDate = Convert.ToDateTime(DOE);
    string path = "UPDATE BDMSPatientScheduleOrderDetail set PatientScheduleOrderUID = " + PatientScheduleOrderUID + " "
                + " , ItemName = N'" + ItemName.Trim() + "' , BillPackageUID = " + PackageUID + " , StartDttm = '" + ChkDate + "'"
                + " , PatientUID = '" + PatientUID + "' , BillableItemUID = " + PackageUID + " , MWhen = GETDATE()"
                + " where UID = " + UID + "";
    bool flag = true;
    try
    {
        Connect();
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;

        cmd.CommandText = path;
        //cmd.Parameters.AddWithValue("@ResultValue", ResultValue);
        //cmd.Parameters.AddWithValue("@ResultItemName", ResultItemName);
        //cmd.Parameters.AddWithValue("@ReferenceRange", ReferenceRange);
        cmd.ExecuteNonQuery();
    }
    catch (Exception er)
    {
        flag = false;
        er.ToString();
    }
    finally
    {
        cmd.Dispose();
        Disconnect();
    }
    return flag;
}
 public bool Delete_BDMSASTMassSpecimenTestSet(string hn, string Specimen)
 {
     SqlCommand cmd = new SqlCommand();
     string path = @"delete from  BDMSASTMassSpecimenTestSet where HN = '" + hn + "' and Specimen = '" + Specimen + "'";

     bool flag = true;
     try
     {
         Connect();
         cmd.Connection = con;
         cmd.CommandType = CommandType.Text;
         
         cmd.CommandText = path;
         cmd.ExecuteNonQuery();

     }
     catch (Exception er)
     {
         System.Windows.Forms.MessageBox.Show(er.Message.ToString());
         flag = false;

     }
     finally
     {
         cmd.Dispose();
         Disconnect();
     }
     return flag;
 }
 public bool Delete_BDMSASTMassSpecimenTestSet(string hn, string Specimen, string PatientScheduleOrderUID)
 {
     SqlCommand cmd = new SqlCommand();
     string path = @"delete from  BDMSASTMassSpecimenTestSet where HN = '" + hn + "' and Specimen = '" + Specimen + "' and PatientScheduleOrderUID = '" + PatientScheduleOrderUID + "'";

     bool flag = true;
     try
     {
         Connect();
         cmd.Connection = con;
         cmd.CommandType = CommandType.Text;

         cmd.CommandText = path;
         cmd.ExecuteNonQuery();

     }
     catch (Exception er)
     {
         System.Windows.Forms.MessageBox.Show(er.Message.ToString());
         flag = false;

     }
     finally
     {
         cmd.Dispose();
         Disconnect();
     }
     return flag;
 }
 public bool Update_BDMSASTMassConvert_By_PatientScheduleOrderUID(string PatientScheduleOrderUID,string status)
 {
     SqlCommand cmd = new SqlCommand();
     string path =
     @"update BDMSASTMassConvert set   Statusflag = '" + status + "' where PatientScheduleOrderUID='" + PatientScheduleOrderUID + "'";
     bool flag = true;
     try
     {
         Connect();
         cmd.Connection = con;
         cmd.CommandType = CommandType.Text;
         cmd.CommandText = path;
         cmd.ExecuteNonQuery();

     }
     catch (Exception er)
     {
         System.Windows.Forms.MessageBox.Show(er.Message.ToString());
         flag = false;

     }
     finally
     {
         cmd.Dispose();
         Disconnect();
     }
     return flag;
 }

        public bool Insert_BDMSASTLabSensitivity(string ResultUID
                                           , string DrugTradeName
                                           , string SenseTest, string ResultItemCode, string RequestDetailUID)
        {
            SqlCommand cmd = new SqlCommand();
            string path =
            @"          
            INSERT INTO [dbo].[BDMSASTLabSensitivity]
           ([ResultUID]
           ,[DrugTradeName]
           ,[SenseTest],ResultItemCode,StatusFlag,RequestDetailUID)
            select
            '" + ResultUID + "' ResultUID,  @DrugTradeName DrugTradeName, @SenseTest SenseTest, @ResultItemCode ResultItemCode, 'A' StatusFlag, @RequestDetailUID RequestDetailUID";
            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = path;
                cmd.Parameters.AddWithValue("@DrugTradeName", DrugTradeName);
                cmd.Parameters.AddWithValue("@SenseTest", SenseTest);
                cmd.Parameters.AddWithValue("@ResultItemCode", ResultItemCode);
                cmd.Parameters.AddWithValue("@RequestDetailUID", RequestDetailUID);
                cmd.ExecuteNonQuery();

            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return flag;
        }
        public bool Insert_BDMSASTNotefromInstrument(string ResultUID
                                   , string Note, string RequestDetailUID
                                   )
        {
            SqlCommand cmd = new SqlCommand();
            string path =
            @"          
            INSERT INTO [dbo].[BDMSASTNotefromInstrument]
           ([ResultUID]
           ,[Note],StatusFlag ,RequestDetailUID
           )
            select
            '" + ResultUID + "' ResultUID,  @Note Note, 'A' StatusFlag, @RequestDetailUID RequestDetailUID ";
            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = path;
                cmd.Parameters.AddWithValue("@Note", Note);
                cmd.Parameters.AddWithValue("@RequestDetailUID", RequestDetailUID);
                cmd.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return flag;
        }
        //BDMSASTLabSensitivity

        public bool Update_BDMSASTLabSensitivity(string RequestDetailuid, string Statusflag, string resultItem)
        {
            SqlCommand cmd = new SqlCommand();
            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"update BDMSASTLabSensitivity set Statusflag  = '" + Statusflag + "' where RequestDetailuid = '" + RequestDetailuid + "' and ResultItemCode =@resultItem";
                cmd.Parameters.AddWithValue("@resultItem", resultItem);
                cmd.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return flag;
        }
        public void Update_Request_By_LabNumber(string labNumber, string status)
        {
            SqlCommand cmd = new SqlCommand();
            string sqlStatement = @"update Request set LRQSTUID = '" + status + "' where RequestNumber = '" + labNumber + "'";
            try
            {
                this.Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlStatement;
                cmd.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                Disconnect();

            }
        }
        public void Update_PatientOrder_By_UID(string uid, string iduid)
        {
            SqlCommand cmd = new SqlCommand();
            string sqlStatement = @"update PatientOrder set IdentifyingUID = '" + iduid + "' where uid = '" + uid + "'";
            try
            {
                this.Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlStatement;
                cmd.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                Disconnect();

            }
        }
        public void Update_RequestDetail_Status_By_UID(string reqDetailUID, string status)
        {
            SqlCommand cmd = new SqlCommand();
            string sqlStatement = @"update RequestDetail set RSSTSUID = '" + status + "' where UID = '" + reqDetailUID + "'";
            try
            {
                this.Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlStatement;
                cmd.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                Disconnect();

            }
        }
        public bool Insert_RequestDetailSpecimen(string RequestDetailUID, string SequenceNumber, string SpecimenID,string SpecimenUID
            , string SpecimenName, string SPMTPUID, string ContainerUID
            , string VolumeCollected, string VOUNTUID, string BODSTUID
            , string CLROUUID, string COLMDUID, string CollectedBy, string Comments
            , string User, string OwnerOrganisationUID, string StorageInstuction, string HandlingInstruction
            , string SPSTSUID, string ReviewComments, string SpecimenNumber, string ReviewedBy)
        {
            bool flag = false;
            try
            {
                this.Connect();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO [dbo].[RequestDetailSpecimen]
                                   ([RequestDetailUID]
                                   ,[SequenceNumber]
                                   ,[SpecimenID]
                                   ,[SpecimenUID]
                                   ,[SpecimenName]
                                   ,[SPMTPUID]
                                   ,[ContainerUID]
                                   ,[VolumeCollected]
                                   ,[VOUNTUID]
                                   ,[CollectionDttm]
                                   ,[BODSTUID]
                                   ,[CLROUUID]
                                   ,[COLMDUID]
                                   ,[CollectedBy]
                                   ,[Comments]
                                   ,[CUser]
                                   ,[CWhen]
                                   ,[MUser]
                                   ,[MWhen]
                                   ,[StatusFlag]
                                   ,[OwnerOrganisationUID]
                                   ,[StorageInstuction]
                                   ,[HandlingInstruction]
                                   ,[SPSTSUID]
                                   ,[ReviewComments]
                                   ,[SpecimenNumber]
                                   ,[ReviewedBy])
                             VALUES
                                    (" + RequestDetailUID + @"
                                   ," + SequenceNumber + @"
                                   ," + SpecimenID + @"
                                   ," + SpecimenUID + @"
                                   ," + "@spename" + @"
                                   ," + SPMTPUID + @"
                                   ," + ContainerUID + @"
                                   ," + VolumeCollected + @"
                                   ," + VOUNTUID + @"
                                   ," + "GETDATE()" + @"
                                   ," + BODSTUID + @"
                                   ," + CLROUUID + @"
                                   ," + COLMDUID + @"
                                   ," + CollectedBy + @"
                                   ," + Comments + @"
                                   ," + User + @"
                                   ,getdate()
                                   ," + User + @"
                                   ,getdate()
                                   ,'A'
                                   ," + OwnerOrganisationUID + @"
                                   ," + StorageInstuction + @"
                                   ," + HandlingInstruction + @"
                                   ," + "(select top 1 UID from ReferenceValue where Description = '" + SPSTSUID + "' and DomainCode = 'RSSTS' and StatusFlag = 'A')" + @"
                                   ," + ReviewComments + @"
                                   ," + SpecimenNumber + @"
                                   ," + ReviewedBy + ")";
                cmd.Parameters.AddWithValue("@spename", SpecimenName);
                cmd.ExecuteNonQuery();
                flag = true;
            }
            catch (Exception ex)
            {


                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Disconnect();
            }
            return flag;
        }
        public DataTable GetRequestDetail_Status(string requestNumber)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            string sqlStatement = @"select 
                                    (select top 1  description From ReferenceValue where ReferenceValue.UID = RSSTSUID and Statusflag = 'A' ) Status
                                     from RequestDetail where RequestUID in ( select top 1 UID from Request where RequestNumber = '" + requestNumber + "' and Statusflag = 'A')";

            try
            {
                this.Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlStatement;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                cmd.Dispose();
                Disconnect();

            }
            return dt;
        }
        public DataTable GetStatusLab(string status)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            string sqlStatement = @"select UID,ValueCode,Description 
                                    from ReferenceValue where DomainCode = 'ORDST' and StatusFlag ='A' 
                                    and Description = '" + status + "'";

            try
            {
                this.Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return dt;
        }
        public bool Update_BDMSASTLabSensitivity(string RequestDetailuid, string Statusflag)
        {
            SqlCommand cmd = new SqlCommand();
            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"update BDMSASTLabSensitivity set Statusflag  = '" + Statusflag + "' where RequestDetailuid = '" + RequestDetailuid + "' ";

                cmd.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return flag;
        }
        public bool Update_PatientScheduleOrder_By_UID(string uid, string patvisituid,string cuser)
        {
            SqlCommand cmd = new SqlCommand();
            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"update PatientScheduleOrder set PatientVisitUID = '" + patvisituid + "',Cuser = '" + cuser + "' where UID = '" + uid + "'";

                cmd.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return flag;
        }
        public bool Update_BDMSASTNotefromInstrument(string RequestDetailUID, string Statusflag)
        {
            SqlCommand cmd = new SqlCommand();
            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"update BDMSASTNotefromInstrument set Statusflag  = '" + Statusflag + "' where RequestDetailUID = '" + RequestDetailUID + "'";
                cmd.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return flag;
        }
        public bool Update_HIMessageError(string uid, string content)
        {
            SqlCommand cmd = new SqlCommand();
            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"update HIMessageQueue set MSGSTUID = -1,messageContent=@content where UID = '" + uid + "'";
                cmd.Parameters.AddWithValue("@content", content);

                cmd.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return flag;
        }
        public bool Delete_HIMessageErrorTable(string uid)
        {
            SqlCommand cmd = new SqlCommand();
            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"delete from  HIMessageError where   UID = '" + uid + "'";

                cmd.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return flag;
        }
        public bool UpdateRequest_Detail_Specimen_Status(string uid, string statusuid)
        {
            SqlCommand cmd = new SqlCommand();
            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"update RequestDetailSpecimen set SPSTSUID='" + statusuid + "' where RequestDetailUID = '" + uid + "'";

                cmd.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return flag;
        }
        public bool UpdateIPDFileStatus(string location, string locuid, string vol)
        {
            SqlCommand cmd = new SqlCommand();
            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"update PatientFileLocation set CurrentStorageLocationUID='" + locuid + "',CurrentLocationName='" + location + "',FILSTUID = 4531 where Volume = '" + vol + "'";

                cmd.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return flag;
        }
        public bool Update_Booking_Status(string Patientuid, string ScheduledDttm)
        {
            SqlCommand cmd = new SqlCommand();
            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"update Booking set StatusFlag = 'U' where PatientUID = '" + Patientuid + "' and SlotStartDttm = '" + ScheduledDttm + "'";

                cmd.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return flag;
        }
        public bool Update_Booking_Status(string Patientuid, string ScheduledDttm,out string outMessage)
        {
            outMessage = "";
            SqlCommand cmd = new SqlCommand();
            bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"update Booking set StatusFlag = 'U' where PatientUID = '" + Patientuid + "' and SlotStartDttm = '" + ScheduledDttm + "'";

                cmd.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                outMessage = er.Message;
                flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return flag;
        }
        public void Update_EN_IN_ContactCheckup(string OrderNumber ,string EN, string Payor)
        {
            ExcData exc = new ExcData();
            string SQL = @"UPDATE [Patient] SET [Episode] = '" + EN + "' WHERE [Episode] = '" + OrderNumber + "'";
            try
            {
                exc.ExecData(SQL);
            }
            catch (Exception ex)
            {
                ex.ToString();
                //MessageBox.Show("Cannot update EN :" + EN + " in Contact Database");
            }
        }
        public string Select_Payor(string VisitUID)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            string selectPayorDetail = @"select dbo.fGetPrimaryPayorByVisit(UID,15) as Payor from PatientVisit where UID = '" + VisitUID +"' and StatusFlag = 'A' ";
            string Payor = string.Empty;
            try
            {
                this.Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = selectPayorDetail;
                dt.Load(cmd.ExecuteReader());
                Payor = dt.Rows[0]["Payor"].ToString();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                this.Disconnect();
            }
           
            return Payor;
        }

        public string FindPatientUID(string ForeName, string SurName)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            string SQL = @"select UID from Patient where Forename = N'" + ForeName + "' and Surname = N'" + SurName + "'";
            string PatientUID = string.Empty;
            try
            {
                this.Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SQL;
                dt.Load(cmd.ExecuteReader());
                PatientUID = dt.Rows[0]["UID"].ToString();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                this.Disconnect();
            }

            return PatientUID;
        }
        //public string FindPatientScheduleOrderUID(string PatientUID, string DOE)
        //{
        //    string PatientScheduleOrderUID = "";
        //    string SQL = "select UID,ScheduleOrderNumber from PatientScheduleOrder where PatientUID = '" + PatientUID + "' and ScheduledDttm = '" + DOE + "'";
        //    exc = new ExcData();
        //    DataTable dt = new DataTable();
        //    dt = exc.data_Table(SQL);
        //    PatientScheduleOrderUID = dt.Rows[0]["UID"].ToString();
        //    return PatientScheduleOrderUID;

        //    SqlCommand cmd = new SqlCommand();
        //    DataTable dt = new DataTable();
        //    string SQL = @"select UID,ScheduleOrderNumber from PatientScheduleOrder where PatientUID = '" + PatientUID + "' and ScheduledDttm = '" + DOE + "'";
        //    string PatientUID = string.Empty;
        //    try
        //    {
        //        this.Connect();
        //        cmd.Connection = con;
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = SQL;
        //        dt.Load(cmd.ExecuteReader());
        //        PatientUID = dt.Rows[0]["UID"].ToString();
        //    }
        //    catch (Exception er)
        //    {
        //        MessageBox.Show(er.Message.ToString());
        //    }
        //    finally
        //    {
        //        this.Disconnect();
        //    }

        //    return PatientUID;
        //}
        public DataTable FindPackage(string PackageCode)
        {
            DataTable dt = new DataTable();
            string SQL = "select UID , PackageName from BillPackage where Code = '" + PackageCode + "'";
            exc = new ExcData();
            dt = exc.data_Table(SQL);
            return dt;
        }
        public DataTable Select_Package_by_PackageName(string PackageName)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            string sqlStatement = @"select UID,Code,PackageName from BillPackage where PackageName like N'%" + PackageName.Trim() + "%' and StatusFlag = 'A' ";

            try
            {
                this.Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlStatement;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return dt;
        }
        public DataTable FindDataTable(string SQL)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
//            string sqlStatement = @"select UID,ValueCode,Description 
//                                    from ReferenceValue where DomainCode = 'ORDST' and StatusFlag ='A' 
//                                    and Description = '" + status + "'";

            try
            {
                this.Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SQL;
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception er)
            {
                //System.Windows.Forms.MessageBox.Show(er.Message.ToString());
                er.ToString();
            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            return dt;
        }
        public void Insert_PatientServiceEvent(string patvisituid)
        {
            SqlCommand cmd = new SqlCommand();
            string path =
            @"INSERT INTO [PatientServiceEvent] ([PatientVisitUID] ,[RequestUID] ,[DepartmentUID] ,[CUser] ,[CWhen]
            ,[MUser] ,[MWhen] ,[StatusFlag] ,[OwnerOrganisationUID] ,[EventStartDttm] ,[EventEndDttm]
            ,[LocationUID] ,[IdentifyingType]) VALUES ('" + patvisituid + "' ,'0' ,34001 ,1 ,GETDATE() ,1 ,GETDATE() ,'A' ,'15' ,GETDATE() ,null ,69 ,'REGISTRATION')";
            //bool flag = true;
            try
            {
                Connect();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = path;


                cmd.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                er.ToString();
                //flag = false;

            }
            finally
            {
                cmd.Dispose();
                Disconnect();
            }
            //return flag;
        }
    }
}
