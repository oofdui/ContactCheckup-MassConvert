using System;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// คลาสจัดการเกี่ยวกับฐานข้อมูลทั้งหมด เช่น Insert Update Query หรือ ตัวจัดการเกี่ยวกับคำสั่ง SQL
/// </summary>
public class clsSQLNative
{
    public clsSQLNative()
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
    }

    public enum DBType
    {
        /// <summary>
        /// ใช้กับฐานข้อมูล SQL Server
        /// </summary>
        SQLServer,
        /// <summary>
        /// ใช้กับฐานข้อมูล ODBC
        /// </summary>
        ODBC
    }

    /// <summary>
    /// Execute คำสั่ง SQL แล้วเก็บค่าที่ได้ใส่ DataTable
    /// </summary>
    /// <param name="strSql">SQL Query</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <returns>ข้อมูล</returns>
    /// <example>
    /// clsSQL.Bind("SELECT * FROM member",clsSQL.DBType.MySQL,"cs");
    /// </example>
    public DataTable Bind(string strSql, DBType dbType, string appsetting_name)
    {
        #region Variable
        var csSQL = System.Configuration.ConfigurationManager.AppSettings[appsetting_name];
        var dt = new DataTable();
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(csSQL))
        {
            if (dbType == DBType.SQLServer)
            {
                #region SQLServer
                var myConn_SQL = new SqlConnection(csSQL);
                var myDa_SQL = new SqlDataAdapter(strSql, myConn_SQL);

                myDa_SQL.Fill(dt);
                myConn_SQL.Dispose();
                myDa_SQL.Dispose();
                if (dt.Rows.Count > 0 && dt != null)
                {
                    return dt;
                }
                else
                {
                    dt.Dispose();
                    return null;
                }
                #endregion
            }
            else if (dbType == DBType.ODBC)
            {
                #region ODBC
                var myConn_ODBC = new OdbcConnection(csSQL);
                var myDa_ODBC = new OdbcDataAdapter(strSql, myConn_ODBC);

                myDa_ODBC.Fill(dt);
                myConn_ODBC.Dispose();
                myDa_ODBC.Dispose();
                if (dt.Rows.Count > 0 && dt != null)
                {
                    return dt;
                }
                else
                {
                    dt.Dispose();
                    return null;
                }
                #endregion
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
        #endregion
    }

    /// <summary>
    /// Execute คำสั่ง SQL แล้วเก็บค่าที่ได้ใส่ DataTable
    /// </summary>
    /// <param name="strSql">SQL Query</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <param name="outMessage">คืนค่าข้อความ กรณีเกิดข้อผิดพลาด</param>
    /// <returns>DataTable</returns>
    /// <example>
    /// string outMessage;
    /// clsSQL.Bind("SELECT * FROM member",clsSQL.DBType.MySQL,"cs",out outMessage);
    /// </example>
    public DataTable Bind(string strSql, DBType dbType, string appsetting_name, out string outMessage)
    {
        #region Variable
        var csSQL = System.Configuration.ConfigurationManager.AppSettings[appsetting_name];
        var dt = new DataTable();
        outMessage = "";
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(csSQL))
        {
            if (dbType == DBType.SQLServer)
            {
                #region SQLServer
                try
                {
                    var myConn_SQL = new SqlConnection(csSQL);
                    var myDa_SQL = new SqlDataAdapter(strSql, myConn_SQL);

                    myDa_SQL.Fill(dt);
                    myConn_SQL.Dispose();
                    myDa_SQL.Dispose();
                    if (dt.Rows.Count > 0 && dt != null)
                    {
                        return dt;
                    }
                    else
                    {
                        dt.Dispose();
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    outMessage = ex.Message;
                    return null;
                }
                #endregion
            }
            else if (dbType == DBType.ODBC)
            {
                #region ODBC
                try
                {
                    var myConn_ODBC = new OdbcConnection(csSQL);
                    var myDa_ODBC = new OdbcDataAdapter(strSql, myConn_ODBC);

                    myDa_ODBC.Fill(dt);
                    myConn_ODBC.Dispose();
                    myDa_ODBC.Dispose();
                    if (dt.Rows.Count > 0 && dt != null)
                    {
                        return dt;
                    }
                    else
                    {
                        dt.Dispose();
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    outMessage = ex.Message;
                    return null;
                }
                #endregion
            }
            else
            {
                outMessage = "Not found DBType.";
                return null;
            }
        }
        else
        {
            outMessage = "Not found AppSettingName.";
            return null;
        }
        #endregion
    }

    /// <summary>
    /// Execute คำสั่ง SQL แล้วเก็บค่าที่ได้ใส่ DataTable โดยสามารถระบุ SQL Parameter ได้
    /// </summary>
    /// <param name="strSql">SQL Query</param>
    /// <param name="arrParameter">SQL Parameter (new string[,] { { "?ID", txtTest.Text } })</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <returns>ข้อมูล</returns>
    /// <example>
    /// strSQL.Append("SELECT email FROM member WHERE id=?ID");
    /// dt = Bind(strSQL.ToString(), new string[,] { { "?ID", txtTest.Text } }, clsSQL.DBType.MySQL, "cs");
    /// </example>
    public DataTable Bind(string strSql, string[,] arrParameter, DBType dbType, string appsetting_name)
    {
        #region Variable
        var csSQL = System.Configuration.ConfigurationManager.AppSettings[appsetting_name];
        var dt = new DataTable();
        var i = 0;
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(csSQL))
        {
            if (dbType == DBType.SQLServer)
            {
                #region SQLServer
                var myConn_SQL = new SqlConnection(csSQL);
                var myDa_SQL = new SqlDataAdapter(strSql, myConn_SQL);

                for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                {
                    myDa_SQL.SelectCommand.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                }
                myDa_SQL.Fill(dt);
                myConn_SQL.Dispose();
                myDa_SQL.Dispose();
                if (dt.Rows.Count > 0 && dt != null)
                {
                    return dt;
                }
                else
                {
                    dt.Dispose();
                    return null;
                }
                #endregion
            }
            else if (dbType == DBType.ODBC)
            {
                #region ODBC
                var myConn_ODBC = new OdbcConnection(csSQL);
                var myDa_ODBC = new OdbcDataAdapter(strSql, myConn_ODBC);

                for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                {
                    myDa_ODBC.SelectCommand.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                }
                myDa_ODBC.Fill(dt);
                myConn_ODBC.Dispose();
                myDa_ODBC.Dispose();
                if (dt.Rows.Count > 0 && dt != null)
                {
                    return dt;
                }
                else
                {
                    dt.Dispose();
                    return null;
                }
                #endregion
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
        #endregion
    }

    /// <summary>
    /// Execute คำสั่ง SQL แล้วเก็บค่าที่ได้ใส่ DataTable โดยสามารถระบุ SQL Parameter ได้
    /// </summary>
    /// <param name="strSql">SQL Query</param>
    /// <param name="arrParameter">SQL Parameter (new string[,] { { "?ID", txtTest.Text } })</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <param name="outMessage">ข้อความ กรณีเกิดข้อผิดพลาด</param>
    /// <returns>DataTable</returns>
    /// <example>
    /// string outMessage;
    /// strSQL.Append("SELECT email FROM member WHERE id=?ID");
    /// dt = Bind(strSQL.ToString(), new string[,] { { "?ID", txtTest.Text } }, clsSQL.DBType.MySQL, "cs",out outMessage);
    /// </example>
    public DataTable Bind(string strSql, string[,] arrParameter, DBType dbType, string appsetting_name, out string outMessage)
    {
        #region Variable
        var csSQL = System.Configuration.ConfigurationManager.AppSettings[appsetting_name];
        var dt = new DataTable();
        var i = 0;
        outMessage = "";
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(csSQL))
        {
            if (dbType == DBType.SQLServer)
            {
                #region SQLServer
                try
                {
                    var myConn_SQL = new SqlConnection(csSQL);
                    var myDa_SQL = new SqlDataAdapter(strSql, myConn_SQL);

                    for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                    {
                        myDa_SQL.SelectCommand.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                    }
                    myDa_SQL.Fill(dt);
                    myConn_SQL.Dispose();
                    myDa_SQL.Dispose();
                    if (dt.Rows.Count > 0 && dt != null)
                    {
                        return dt;
                    }
                    else
                    {
                        dt.Dispose();
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    outMessage = ex.Message;
                    return null;
                }
                #endregion
            }
            else if (dbType == DBType.ODBC)
            {
                #region ODBC
                try
                {
                    var myConn_ODBC = new OdbcConnection(csSQL);
                    var myDa_ODBC = new OdbcDataAdapter(strSql, myConn_ODBC);

                    for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                    {
                        myDa_ODBC.SelectCommand.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                    }
                    myDa_ODBC.Fill(dt);
                    myConn_ODBC.Dispose();
                    myDa_ODBC.Dispose();
                    if (dt.Rows.Count > 0 && dt != null)
                    {
                        return dt;
                    }
                    else
                    {
                        dt.Dispose();
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    outMessage = ex.Message;
                    return null;
                }
                #endregion
            }
            else
            {
                outMessage = "Not found DBType.";
                return null;
            }
        }
        else
        {
            outMessage = "Not found AppSettingName.";
            return null;
        }
        #endregion
    }

    /// <summary>
    /// Execute คำสั่ง SQL ที่คืนค่าเป็นค่าเดียว
    /// </summary>
    /// <param name="strSql">SQL Query</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <returns>ข้อมูล</returns>
    /// <example>
    /// clsSQL.Return("SELECT MAX(id) FROM member",clsSQL.DBType.MySQL,"cs");
    /// </example>
    public string Return(string strSql, DBType dbType, string appsetting_name)
    {
        #region Variable
        var csSQL = System.Configuration.ConfigurationManager.AppSettings[appsetting_name];
        var strReturn = "";
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(csSQL))
        {
            if (dbType == DBType.SQLServer)
            {
                #region SQLServer
                var myConn_SQL = new SqlConnection(csSQL);
                var myCmd_SQL = new SqlCommand(strSql, myConn_SQL);
                try
                {
                    myConn_SQL.Open();
                    strReturn = myCmd_SQL.ExecuteScalar().ToString();
                    myConn_SQL.Close();
                    myCmd_SQL.Dispose();
                }
                catch (Exception)
                {

                }
                finally
                {
                    myCmd_SQL.Dispose();
                    myConn_SQL.Close();
                }
                #endregion
            }
            else if (dbType == DBType.ODBC)
            {
                #region ODBC
                var myConn_ODBC = new OdbcConnection(csSQL);
                var myCmd_ODBC = new OdbcCommand(strSql, myConn_ODBC);
                try
                {
                    myConn_ODBC.Open();
                    strReturn = myCmd_ODBC.ExecuteScalar().ToString();
                    myConn_ODBC.Close();
                    myCmd_ODBC.Dispose();
                }
                catch (Exception)
                {

                }
                finally
                {
                    myCmd_ODBC.Dispose();
                    myConn_ODBC.Close();
                }
                #endregion
            }
            else
            {
                strReturn = "";
            }
        }
        return strReturn;
        #endregion
    }

    /// <summary>
    /// Execute คำสั่ง SQL ที่คืนค่าเป็นค่าเดียว
    /// </summary>
    /// <param name="strSql">SQL Query</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <param name="outMessage">ข้อความ กรณีเกิดข้อผิดพลาด</param>
    /// <returns>ข้อมูล</returns>
    /// <example>
    /// string outMessage;
    /// clsSQL.Return("SELECT MAX(id) FROM member",clsSQL.DBType.MySQL,"cs",out outMessage);
    /// </example>
    public string Return(string strSql, DBType dbType, string appsetting_name, out string outMessage)
    {
        #region Variable
        var csSQL = System.Configuration.ConfigurationManager.AppSettings[appsetting_name];
        var strReturn = "";
        outMessage = "";
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(csSQL))
        {
            if (dbType == DBType.SQLServer)
            {
                #region SQLServer
                var myConn_SQL = new SqlConnection(csSQL);
                var myCmd_SQL = new SqlCommand(strSql, myConn_SQL);
                try
                {
                    myConn_SQL.Open();
                    strReturn = myCmd_SQL.ExecuteScalar().ToString();
                    myConn_SQL.Close();
                    myCmd_SQL.Dispose();
                }
                catch (Exception ex)
                {
                    outMessage = ex.Message;
                }
                finally
                {
                    myCmd_SQL.Dispose();
                    myConn_SQL.Close();
                }
                #endregion
            }
            else if (dbType == DBType.ODBC)
            {
                #region ODBC
                var myConn_ODBC = new OdbcConnection(csSQL);
                var myCmd_ODBC = new OdbcCommand(strSql, myConn_ODBC);
                try
                {
                    myConn_ODBC.Open();
                    strReturn = myCmd_ODBC.ExecuteScalar().ToString();
                    myConn_ODBC.Close();
                    myCmd_ODBC.Dispose();
                }
                catch (Exception ex)
                {
                    outMessage = ex.Message;
                }
                finally
                {
                    myCmd_ODBC.Dispose();
                    myConn_ODBC.Close();
                }
                #endregion
            }
            else
            {
                strReturn = "";
                outMessage = "Not found DBType.";
            }
        }
        return strReturn;
        #endregion
    }

    /// <summary>
    /// Execute คำสั่ง SQL ที่คืนค่าเป็นค่าเดียว โดยสามารถใช้ SQL Parameter ได้
    /// </summary>
    /// <param name="strSql">SQL Query</param>
    /// <param name="arrParameter">SQL Parameter (new string[,] { { "?ID", txtTest.Text } })</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <returns>ข้อมูล</returns>
    /// <example>
    /// strSQL.Append("SELECT email FROM member WHERE id=?ID");
    /// lblMessage.Text = clsSQL.Return(strSQL.ToString(), new string[,] { { "?ID", txtTest.Text } }, clsSQL.DBType.MySQL, "cs");
    /// </example>
    public string Return(string strSql, string[,] arrParameter, DBType dbType, string appsetting_name)
    {
        #region Variable
        var csSQL = System.Configuration.ConfigurationManager.AppSettings[appsetting_name];
        var strReturn = "";
        var i = 0;
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(csSQL))
        {
            if (dbType == DBType.SQLServer)
            {
                #region SQLServer
                var myConn_SQL = new SqlConnection(csSQL);
                var myCmd_SQL = new SqlCommand(strSql, myConn_SQL);

                for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                {
                    myCmd_SQL.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                }
                try
                {
                    myConn_SQL.Open();
                    strReturn = myCmd_SQL.ExecuteScalar().ToString();
                    myConn_SQL.Close();
                    myCmd_SQL.Dispose();
                }
                catch (Exception)
                {

                }
                finally
                {
                    myCmd_SQL.Dispose();
                    myConn_SQL.Close();
                }
                #endregion
            }
            else if (dbType == DBType.ODBC)
            {
                #region ODBC
                var myConn_ODBC = new OdbcConnection(csSQL);
                var myCmd_ODBC = new OdbcCommand(strSql, myConn_ODBC);

                for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                {
                    myCmd_ODBC.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                }
                try
                {
                    myConn_ODBC.Open();
                    strReturn = myCmd_ODBC.ExecuteScalar().ToString();
                    myConn_ODBC.Close();
                    myCmd_ODBC.Dispose();
                }
                catch (Exception)
                {

                }
                finally
                {
                    myCmd_ODBC.Dispose();
                    myConn_ODBC.Close();
                }
                #endregion
            }
            else
            {
                strReturn = "";
            }
        }
        return strReturn;
        #endregion
    }

    /// <summary>
    /// Execute คำสั่ง SQL ที่คืนค่าเป็นค่าเดียว โดยสามารถใช้ SQL Parameter ได้
    /// </summary>
    /// <param name="strSql">SQL Query</param>
    /// <param name="arrParameter">SQL Parameter (new string[,] { { "?ID", txtTest.Text } })</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <param name="outMessage">ข้อความ กรณีเกิดข้อผิดพลาด</param>
    /// <returns>ข้อมูล</returns>
    /// <example>
    /// string outMessage;
    /// strSQL.Append("SELECT email FROM member WHERE id=?ID");
    /// lblMessage.Text = clsSQL.Return(strSQL.ToString(), new string[,] { { "?ID", txtTest.Text } }, clsSQL.DBType.MySQL, "cs",out outMessage);
    /// </example>
    public string Return(string strSql, string[,] arrParameter, DBType dbType, string appsetting_name, out string outMessage)
    {
        #region Variable
        var csSQL = System.Configuration.ConfigurationManager.AppSettings[appsetting_name];
        var strReturn = "";
        var i = 0;
        outMessage = "";
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(csSQL))
        {
            if (dbType == DBType.SQLServer)
            {
                #region SQLServer
                var myConn_SQL = new SqlConnection(csSQL);
                var myCmd_SQL = new SqlCommand(strSql, myConn_SQL);

                for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                {
                    myCmd_SQL.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                }
                try
                {
                    myConn_SQL.Open();
                    strReturn = myCmd_SQL.ExecuteScalar().ToString();
                    myConn_SQL.Close();
                    myCmd_SQL.Dispose();
                }
                catch (Exception ex)
                {
                    outMessage = ex.Message;
                }
                finally
                {
                    myCmd_SQL.Dispose();
                    myConn_SQL.Close();
                }
                #endregion
            }
            else if (dbType == DBType.ODBC)
            {
                #region ODBC
                var myConn_ODBC = new OdbcConnection(csSQL);
                var myCmd_ODBC = new OdbcCommand(strSql, myConn_ODBC);

                for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                {
                    myCmd_ODBC.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                }
                try
                {
                    myConn_ODBC.Open();
                    strReturn = myCmd_ODBC.ExecuteScalar().ToString();
                    myConn_ODBC.Close();
                    myCmd_ODBC.Dispose();
                }
                catch (Exception ex)
                {
                    outMessage = ex.Message;
                }
                finally
                {
                    myCmd_ODBC.Dispose();
                    myConn_ODBC.Close();
                }
                #endregion
            }
            else
            {
                outMessage = "Not found DBType.";
                strReturn = "";
            }
        }
        return strReturn;
        #endregion
    }

    /// <summary>
    /// Execute คำสั่ง SQL ที่ใช้ในการบันทึกข้อมูล
    /// </summary>
    /// <param name="strSql">SQL Query</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <returns>True=รันสำเร็จ , False=รันไม่สำเร็จ</returns>
    /// <example>
    /// clsSQL.Execute("DELETE FROM member WHERE id=1",clsSQL.DBType.MySQL,"cs");
    /// </example>
    public bool Execute(string strSql, DBType dbType, string appsetting_name)
    {
        #region Variable
        var csSQL = System.Configuration.ConfigurationManager.AppSettings[appsetting_name];
        var result = false;
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(csSQL))
        {
            if (dbType == DBType.SQLServer)
            {
                #region SQLServer
                var myConn_SQL = new SqlConnection(csSQL);
                var myCmd_SQL = new SqlCommand(strSql, myConn_SQL);
                try
                {
                    myConn_SQL.Open();
                    myCmd_SQL.ExecuteNonQuery();
                    myConn_SQL.Close();
                    myCmd_SQL.Dispose();
                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                }
                finally
                {
                    myCmd_SQL.Dispose();
                    myConn_SQL.Close();
                }
                #endregion
            }
            else if (dbType == DBType.ODBC)
            {
                #region ODBC
                var myConn_ODBC = new OdbcConnection(csSQL);
                var myCmd_ODBC = new OdbcCommand(strSql, myConn_ODBC);
                try
                {
                    myConn_ODBC.Open();
                    myCmd_ODBC.ExecuteNonQuery();
                    myConn_ODBC.Close();
                    myCmd_ODBC.Dispose();
                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                }
                finally
                {
                    myCmd_ODBC.Dispose();
                    myConn_ODBC.Close();
                }
                #endregion
            }
            else
            {
                result = false;
            }
        }
        else
        {
            result = false;
        }
        return result;
        #endregion
    }

    /// <summary>
    /// Execute คำสั่ง SQL ที่ใช้ในการบันทึกข้อมูล
    /// </summary>
    /// <param name="strSql">SQL Query</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <param name="outMessage">ข้อความ กรณีเกิดข้อผิดพลาด</param>
    /// <returns>True=รันสำเร็จ , False=รันไม่สำเร็จ</returns>
    /// <example>
    /// string outMessage;
    /// clsSQL.Execute("DELETE FROM member WHERE id=1",clsSQL.DBType.MySQL,"cs",out outMessage);
    /// </example>
    public bool Execute(string strSql, DBType dbType, string appsetting_name, out string outMessage)
    {
        #region Variable
        var csSQL = System.Configuration.ConfigurationManager.AppSettings[appsetting_name];
        var result = false;
        outMessage = "";
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(csSQL))
        {
            if (dbType == DBType.SQLServer)
            {
                #region SQLServer
                var myConn_SQL = new SqlConnection(csSQL);
                var myCmd_SQL = new SqlCommand(strSql, myConn_SQL);
                try
                {
                    myConn_SQL.Open();
                    myCmd_SQL.ExecuteNonQuery();
                    myConn_SQL.Close();
                    myCmd_SQL.Dispose();
                    result = true;
                }
                catch (Exception ex)
                {
                    outMessage = ex.Message;
                    result = false;
                }
                finally
                {
                    myCmd_SQL.Dispose();
                    myConn_SQL.Close();
                }
                #endregion
            }
            else if (dbType == DBType.ODBC)
            {
                #region ODBC
                var myConn_ODBC = new OdbcConnection(csSQL);
                var myCmd_ODBC = new OdbcCommand(strSql, myConn_ODBC);
                try
                {
                    myConn_ODBC.Open();
                    myCmd_ODBC.ExecuteNonQuery();
                    myConn_ODBC.Close();
                    myCmd_ODBC.Dispose();
                    result = true;
                }
                catch (Exception ex)
                {
                    outMessage = ex.Message;
                    result = false;
                }
                finally
                {
                    myCmd_ODBC.Dispose();
                    myConn_ODBC.Close();
                }
                #endregion
            }
            else
            {
                outMessage = "Not found DBType.";
                result = false;
            }
        }
        else
        {
            outMessage = "Not found AppSettingName.";
            result = false;
        }
        return result;
        #endregion
    }

    /// <summary>
    /// Execute คำสั่ง SQL ที่ใช้ในการบันทึกข้อมูล โดยสามารถระบุ SQL Parameter ได้
    /// </summary>
    /// <param name="strSql">SQL Query</param>
    /// <param name="arrParameter">SQL Parameter (new string[,] { { "?ID", txtTest.Text } })</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <returns>True=รันสำเร็จ , False=รันไม่สำเร็จ</returns>
    /// <example>
    /// clsSQL.Execute("UPDATE webboard_type SET type_name=?NAME WHERE type_id=?ID", new string[,] { { "?ID", txtTest.Text }, { "?NAME", "ใช้ Array 2 มิติ" } }, clsSQL.DBType.MySQL, "cs");
    /// </example>
    public bool Execute(string strSql, string[,] arrParameter, DBType dbType, string appsetting_name)
    {
        #region Variable
        var csSQL = System.Configuration.ConfigurationManager.AppSettings[appsetting_name];
        var result = false;
        var i = 0;
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(csSQL) && arrParameter.Rank == 2)
        {
            if (dbType == DBType.SQLServer)
            {
                #region SQLServer
                var myConn_SQL = new SqlConnection(csSQL);
                var myCmd_SQL = new SqlCommand(strSql, myConn_SQL);

                for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                {
                    myCmd_SQL.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                }

                try
                {
                    myConn_SQL.Open();
                    myCmd_SQL.ExecuteNonQuery();
                    myConn_SQL.Close();
                    myCmd_SQL.Dispose();
                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                }
                finally
                {
                    myCmd_SQL.Dispose();
                    myConn_SQL.Close();
                }
                #endregion
            }
            else if (dbType == DBType.ODBC)
            {
                #region ODBC
                var myConn_ODBC = new OdbcConnection(csSQL);
                var myCmd_ODBC = new OdbcCommand(strSql, myConn_ODBC);

                for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                {
                    myCmd_ODBC.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                }

                try
                {
                    myConn_ODBC.Open();
                    myCmd_ODBC.ExecuteNonQuery();
                    myConn_ODBC.Close();
                    myCmd_ODBC.Dispose();
                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                }
                finally
                {
                    myCmd_ODBC.Dispose();
                    myConn_ODBC.Close();
                }
                #endregion
            }
            else
            {
                result = false;
            }
        }
        else
        {
            result = false;
        }
        return result;
        #endregion
    }

    /// <summary>
    /// Execute คำสั่ง SQL ที่ใช้ในการบันทึกข้อมูล โดยสามารถระบุ SQL Parameter ได้
    /// </summary>
    /// <param name="strSql">SQL Query</param>
    /// <param name="arrParameter">SQL Parameter (new string[,] { { "?ID", txtTest.Text } })</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <param name="outMessage">ข้อความ เมื่อเกิดข้อผิดพลาด</param>
    /// <returns>True=รันสำเร็จ , False=รันไม่สำเร็จ</returns>
    /// <example>
    /// string outMessage;
    /// clsSQL.Execute("UPDATE webboard_type SET type_name=?NAME WHERE type_id=?ID", new string[,] { { "?ID", txtTest.Text }, { "?NAME", "ใช้ Array 2 มิติ" } }, clsSQL.DBType.MySQL, "cs",out outMessage);
    /// </example>
    public bool Execute(string strSql, string[,] arrParameter, DBType dbType, string appsetting_name, out string outMessage)
    {
        #region Variable
        var csSQL = System.Configuration.ConfigurationManager.AppSettings[appsetting_name];
        var result = false;
        var i = 0;
        outMessage = "";
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(csSQL) && arrParameter.Rank == 2)
        {
            if (dbType == DBType.SQLServer)
            {
                #region SQLServer
                var myConn_SQL = new SqlConnection(csSQL);
                var myCmd_SQL = new SqlCommand(strSql, myConn_SQL);

                for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                {
                    myCmd_SQL.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                }

                try
                {
                    myConn_SQL.Open();
                    myCmd_SQL.ExecuteNonQuery();
                    myConn_SQL.Close();
                    myCmd_SQL.Dispose();
                    result = true;
                }
                catch (Exception ex)
                {
                    outMessage = ex.Message;
                    result = false;
                }
                finally
                {
                    myCmd_SQL.Dispose();
                    myConn_SQL.Close();
                }
                #endregion
            }
            else if (dbType == DBType.ODBC)
            {
                #region ODBC
                var myConn_ODBC = new OdbcConnection(csSQL);
                var myCmd_ODBC = new OdbcCommand(strSql, myConn_ODBC);

                for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                {
                    myCmd_ODBC.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                }

                try
                {
                    myConn_ODBC.Open();
                    myCmd_ODBC.ExecuteNonQuery();
                    myConn_ODBC.Close();
                    myCmd_ODBC.Dispose();
                    result = true;
                }
                catch (Exception ex)
                {
                    outMessage = ex.Message;
                    result = false;
                }
                finally
                {
                    myCmd_ODBC.Dispose();
                    myConn_ODBC.Close();
                }
                #endregion
            }
            else
            {
                outMessage = "Not found DBType.";
                result = false;
            }
        }
        else
        {
            outMessage = "Not found AppSettingName.";
            result = false;
        }
        return result;
        #endregion
    }

}

public static class clsSQLExtension
{
    /// <summary>
    /// กรองอักขระพิเศษที่ปนมากับ SQL Query Statement
    /// </summary>
    /// <param name="sqlQuery">SQL Query</param>
    /// <returns>SQL Query ที่ถูกกรองอักขระพิเศษออกแล้ว</returns>
    /// <example>
    /// "INSERT INTO Test(Detail)VALUES('" + txtDetail.Text.SQLQueryFilter() + "');";
    /// </example>
    public static string SQLQueryFilter(this string sqlQuery)
    {
        #region Variable
        string sqlQueryFiltered;
        string[,] wordReplaces =
            new string[,]{
                {"&nbsp;"," "},
                {"''","'"},
                {"'","''"}};
        #endregion
        #region Error Checker
        if (string.IsNullOrEmpty(sqlQuery.Trim())) { return ""; }
        #endregion
        #region SQL Query Filter
        sqlQueryFiltered = sqlQuery.Trim();

        for (int i = 0; i < wordReplaces.Length / wordReplaces.Rank; i++)
        {
            sqlQueryFiltered = sqlQueryFiltered.Replace(wordReplaces[i, 0], wordReplaces[i, 1]);
        }
        #endregion

        return sqlQueryFiltered;
    }

    /// <summary>
    /// ตรวจสอบสถานะการเชื่อมต่อฐานข้อมูล
    /// </summary>
    /// <param name="connection"></param>
    /// <returns></returns>
    /// <example>
    /// SqlConnection myConn = new SqlConnection("server=10.121.10.7;uid=kpi;pwd=kpi;database=BRHDB;");
    /// if (myConn.IsConnected())
    /// {
    ///     Response.Write("ฐานข้อมูลใช้ได้นะ");
    /// }
    /// else
    /// {
    ///     Response.Write("ต่อฐานข้อมูลไม่ได้เลย");
    /// }
    /// </example>
    public static bool IsConnected(this SqlConnection connection)
    {
        try
        {
            connection.Open();
            connection.Close();
        }
        catch (SqlException)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// ตรวจสอบสถานะการเชื่อมต่อฐานข้อมูล
    /// </summary>
    /// <param name="connection"></param>
    /// <returns></returns>
    /// <example>
    /// MySqlConnection myConn = new MySqlConnection("server=10.121.10.7;uid=kpi;pwd=kpi;database=BRHDB;");
    /// if (myConn.IsConnected())
    /// {
    ///     Response.Write("ฐานข้อมูลใช้ได้นะ");
    /// }
    /// else
    /// {
    ///     Response.Write("ต่อฐานข้อมูลไม่ได้เลย");
    /// }
    /// </example>
    public static bool IsConnected(this OdbcConnection connection)
    {
        try
        {
            connection.Open();
            connection.Close();
        }
        catch (SqlException)
        {
            return false;
        }

        return true;
    }
}