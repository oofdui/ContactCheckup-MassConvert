using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace MassConvert.Database
{
    class ExcData
    {
        private SqlConnection Conn;
        private SqlCommand cmd;
        private string strConn;
        //private string strConnLab;
        //private string strConnMEDDATA;
        //private string strConnXray;
        //private string strConnBconnec;
        public ExcData()
        {
            strConn = ConfigurationSettings.AppSettings["MobieConnect"];
        }
        public DataSet data_Set(string strSQL)
        {
            // *** Connect Database ***
            Conn = new SqlConnection(strConn);
            Conn.Open();

            DataSet ds = new DataSet();
            try
            {
                // *** Query Data ***
                SqlDataAdapter da = new SqlDataAdapter(strSQL, Conn);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                // *** Disconnect Database ***
                Conn.Close();
            }
            // *** Return dataset ***
            return ds;
        }
        /// <summary>
        /// สำหรับ Query เป็น Data Table
        /// </summary>
        /// <param name="strSQL">ส่ง SQL Statement</param>
        /// <returns></returns>
        public DataTable data_Table(string strSQL)
        {
            // *** Connect Database ***
            Conn = new SqlConnection(strConn);
            Conn.Open();

            DataTable dt = new DataTable();
            try
            {
                // *** Query Data ***
                SqlCommand cmd = new SqlCommand(strSQL, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                dt.Load(dr);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                // *** Disconnect Database ***
                Conn.Close();
            }
            // *** Return datatable ***
            return dt;
        }
        public Boolean ExecData(string strSQL)
        {
            Conn = new SqlConnection(strConn);
            Conn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand(strSQL, Conn);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
            finally
            {
                Conn.Close();
            }
        }
        public Boolean ChkData(string strSQL)
        {
            Conn = new SqlConnection(strConn);
            Conn.Open();

            //SqlCommand cmd;
            SqlDataReader dr = null;
            Boolean chk = false;

            try
            {
                // *** Query Data ***
                cmd = new SqlCommand(strSQL, Conn);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    chk = true;
                }
                else
                {
                    chk = false;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                // *** Disconnect Database ***
                if (dr != null)
                {
                    dr.Close();
                }
                Conn.Close();
            }
            return chk;
        }

    }
}
