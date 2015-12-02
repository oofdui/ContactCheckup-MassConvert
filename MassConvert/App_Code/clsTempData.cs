using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

class clsTempData
{
    private static DataTable dtPayor;
    private static DataTable dtPayorSearch;
    private static string _username="";
    public static string Username
    {
        get { return _username; }
        set { _username = value; }
    }
    private static int rowNumber = 0;
    private static DataTable _dtIsConverted;
    public static DataTable dtIsConverted
    {
        get { return _dtIsConverted; }
        set { _dtIsConverted = value; }
    }
    private static DataTable _dtConvertResult;
    public static DataTable dtConvertResult
    {
        get { return _dtConvertResult; }
        set { _dtConvertResult = value; }
    }
    public bool setConvertResult(out string outMessage,string hn="",string name="",string procedure="",string result="",string remark="")
    {
        #region Variable
        var resultBool = false;
        outMessage = "";
        #endregion
        #region Procedure
        try
        {
            if (_dtConvertResult == null)
            {
                rowNumber = 0;
                _dtConvertResult = new DataTable();
                _dtConvertResult.Columns.Add("Row", typeof(int));
                _dtConvertResult.Columns.Add("HN", typeof(string));
                _dtConvertResult.Columns.Add("Name", typeof(string));
                _dtConvertResult.Columns.Add("When", typeof(DateTime));
                _dtConvertResult.Columns.Add("Procedure", typeof(string));
                _dtConvertResult.Columns.Add("Result", typeof(string));
                _dtConvertResult.Columns.Add("Remark", typeof(string));
            }
            rowNumber += 1;
            _dtConvertResult.Rows.Add(rowNumber,hn,name,DateTime.Now,procedure,result,remark);
            resultBool = true;
        }
        catch(Exception ex)
        {
            outMessage = ex.Message;
            resultBool = false;
        }
        #endregion
        return resultBool;
    }
    public bool IsConverted(string forename,string surname,string doe,string doeFrom,string doeTo)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        #region Variable
        var result = false;
        var clsSQL = new clsSQLNative();
        var strSQL = new StringBuilder();
        var dt = new DataTable();
        var dtPatientScheduleOrder = new DataTable();
        #endregion
        #region Procedure
        if(_dtIsConverted== null || _dtIsConverted.Rows.Count == 0)
        {
            #region SQLQuery
            strSQL.Append("SELECT PatientVisitUID,ScheduleOrderNumber,p.Forename,p.Surname,ps.ScheduledDttm DOE FROM [PatientScheduleOrder] ps ");
            strSQL.Append("INNER JOIN Patient p on p.UID = ps.PatientUID ");
            strSQL.Append("AND ps.ScheduledDttm BETWEEN '" + Convert.ToDateTime(doeFrom).ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + Convert.ToDateTime(doeTo).ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            strSQL.Append("AND ps.StatusFlag = 'A';");
            #endregion
            _dtIsConverted = new DataTable();
            _dtIsConverted = clsSQL.Bind(strSQL.ToString(), clsSQLNative.DBType.SQLServer, "csBConnect");
        }
        if(_dtIsConverted!=null && _dtIsConverted.Rows.Count > 0)
        {
            DataRow[] drs = _dtIsConverted.Select("Forename='"+forename.Trim()+"' AND Surname='"+surname.Trim()+"' AND DOE='"+DateTime.Parse(doe).ToString("yyyy-MM-dd HH:mm")+ "'");
            if (drs.Length > 0)
            {
                for(int i = 0; i < drs.Length; i++)
                {
                    if (string.IsNullOrEmpty(drs[i]["PatientVisitUID"].ToString()) || drs[i]["PatientVisitUID"].ToString() == "0")
                    {
                        //result = false;
                    }
                    else
                    {
                        result = true;
                    }
                }
            }
        }
        /*
        #region PatientScheduleOrder
        #region SQLQuery
        strSQL.Append("SELECT TOP 1 ScheduleOrderNumber FROM [PatientScheduleOrder] ps ");
        strSQL.Append("INNER JOIN Patient p on p.UID = ps.PatientUID ");
        strSQL.Append("AND p.Forename = N'" + forename.Trim() + "' ");
        strSQL.Append("AND Surname = N'" + surname.Trim() + "' ");
        strSQL.Append("AND ps.ScheduledDttm BETWEEN '" + Convert.ToDateTime(doeFrom).ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + Convert.ToDateTime(doeTo).ToString("yyyy-MM-dd HH:mm:ss") + "' ");
        strSQL.Append("AND ps.StatusFlag = 'A';");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), clsSQLNative.DBType.SQLServer, "csBConnect");
        if(dt!=null && dt.Rows.Count > 0)
        {
            strSQL.Length = 0;strSQL.Capacity = 0;
            strSQL.Append("SELECT *,(SELECT PASID FROM Patient WHERE Patient.UID = PatientScheduleOrder.PatientUID AND Patient.StatusFlag='A') HN FROM PatientScheduleOrder WHERE ScheduleOrderNumber = '" + dt.Rows[0]["ScheduleOrderNumber"].ToString() + "' AND StatusFlag='A';");
            dtPatientScheduleOrder = clsSQL.Bind(strSQL.ToString(), clsSQLNative.DBType.SQLServer, "csBConnect");
            if(dtPatientScheduleOrder!=null && dtPatientScheduleOrder.Rows.Count > 0)
            {
                for(int i = 0; i < dtPatientScheduleOrder.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(dtPatientScheduleOrder.Rows[i]["PatientVisitUID"].ToString()) || dtPatientScheduleOrder.Rows[i]["PatientVisitUID"].ToString() == "0")
                    {
                        //result = false;
                    }
                    else
                    {
                        result = true;
                    }
                }
            }
        }
        #endregion
        */
        #endregion
        return result;
    }
    /// <summary>
    /// ดึงรายชื่อพนักงานตามเงื่อนไขที่กำหนด เพื่อนำไปแสดงบนหน้า ConvertByPayor
    /// </summary>
    /// <param name="doeFrom"></param>
    /// <param name="doeTo"></param>
    /// <param name="payor"></param>
    /// <returns></returns>
    public DataTable getPatient(DateTime doeFrom,DateTime doeTo,string payor,string mobileStatus)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        #region Variable
        var dt = new DataTable();
        var strSQL = new StringBuilder();
        var clsSQL = new clsSQLNative();
        #endregion
        #region Procedure
        #region SQLQuery
        strSQL.Append("SELECT ");
        strSQL.Append("No,HN,PreName,Name,LastName,DOE,Payor,SyncWhen,'0' IsConvertPreOrder ");
        strSQL.Append("FROM ");
        strSQL.Append("Patient P ");
        strSQL.Append("WHERE ");
        strSQL.Append("(DOE BETWEEN '" + doeFrom.ToString("yyyy-MM-dd HH:mm") + "' AND '" + doeTo.ToString("yyyy-MM-dd HH:mm") + "') ");
        if(payor!="" && payor.ToLower() != "null")
        {
            strSQL.Append("AND Payor='"+payor+"' ");
        }
        if (mobileStatus == "NotRegister")
        {
            strSQL.Append("AND SyncStatus!='1' ");
        }
        else if (mobileStatus == "Register")
        {
            strSQL.Append("AND SyncStatus='1' ");
        }
        strSQL.Append("ORDER BY ");
        strSQL.Append("No;");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), clsSQLNative.DBType.SQLServer, "MobieConnect");
        #endregion
        return dt;
    }
    /// <summary>
    /// ใช้ในหน้า MapPayor
    /// </summary>
    /// <param name="searchName"></param>
    /// <returns></returns>
    public DataTable getPayor(string searchName="")
    {
        #region Variable
        var clsSQL = new clsSQLNative();
        var strSQL = new StringBuilder();
        #endregion
        #region Procedure
        if(dtPayor==null || dtPayor.Rows.Count==0)
        {
            dtPayor = new DataTable();
            strSQL.Append("SELECT P.Payor,COUNT(MP.UID) CountMap ");
            strSQL.Append("FROM Patient P WITH(NOLOCK) ");
            strSQL.Append("LEFT JOIN MassConvertPayorMap MP WITH(NOLOCK) ON P.Payor = MP.Payor AND MP.StatusFlag = 'A' ");
            strSQL.Append("WHERE P.Payor <> '' AND NOT P.Payor IS NULL ");
            strSQL.Append("GROUP BY P.Payor ");
            strSQL.Append("ORDER BY P.Payor;");
            dtPayor = clsSQL.Bind(strSQL.ToString(), clsSQLNative.DBType.SQLServer, "MobieConnect");
        }
        dtPayorSearch = new DataTable();
        dtPayorSearch = dtPayor.Copy();

        if (searchName.Trim() != "")
        {
            if (dtPayorSearch != null && dtPayorSearch.Rows.Count > 0)
            {
                DataView dv = dtPayorSearch.DefaultView;
                dv.RowFilter = "Payor LIKE '%" + searchName + "%'";
                dtPayorSearch = dv.ToTable();
            }
        }
        #endregion
        return dtPayorSearch;
    }
    public DataTable getDOE(string payorName = "")
    {
        #region Variable
        var dt = new DataTable();
        var clsSQL = new clsSQLNative();
        var strSQL = new StringBuilder();
        #endregion
        #region Procedure
        #region SQLQuery
        strSQL.Append("SELECT ");
        strSQL.Append("UID,Payor,DOEFrom,DOETo ");
        strSQL.Append("FROM ");
        strSQL.Append("MassConvertPayorMap ");
        strSQL.Append("WHERE ");
        strSQL.Append("StatusFlag='A' ");
        strSQL.Append("AND Payor='"+payorName+"' ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("DOEFrom ASC;");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), clsSQLNative.DBType.SQLServer, "MobieConnect");
        #endregion
        return dt;
    }
    public DataTable getPayorMapSummary()
    {
        #region Variable
        var strSQL = new StringBuilder();
        var dt = new DataTable();
        var clsSQL = new clsSQLNative();
        #endregion
        #region Procedure
        #region SQLQuery
        strSQL.Append("SELECT ");
        strSQL.Append("MP.Payor Company, MP.DOEFrom,MP.DOETo,IC.CompanyName Payor, PA.Name Agreement, PD.PayorName PayorOffice, PM.PolicyName Policy ");
        strSQL.Append("FROM ");
        strSQL.Append("MassConvertPayorMap MP ");
        strSQL.Append("LEFT JOIN " + System.Configuration.ConfigurationManager.AppSettings["LinkServer"] + ".InsuranceCompany IC ON MP.InsuranceCompanyUID = IC.UID ");
        strSQL.Append("LEFT JOIN " + System.Configuration.ConfigurationManager.AppSettings["LinkServer"] + ".PayorAgreement PA ON MP.PayorAgreementUID = PA.UID ");
        strSQL.Append("LEFT JOIN " + System.Configuration.ConfigurationManager.AppSettings["LinkServer"] + ".PayorDetail PD ON MP.PayorDetailUID = PD.UID ");
        strSQL.Append("LEFT JOIN " + System.Configuration.ConfigurationManager.AppSettings["LinkServer"] + ".PolicyMaster PM ON MP.PolicyMasterUID = PM.UID ");
        strSQL.Append("WHERE ");
        strSQL.Append("MP.StatusFlag = 'A' ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("MP.Payor,MP.DOEFrom;");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), clsSQLNative.DBType.SQLServer, "MobieConnect");
        #endregion
        return dt;
    }
}