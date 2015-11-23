using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MassConvert
{
    public partial class ConvertByPayor: Form
    {
        #region GlobalVariable

        #endregion
        public ConvertByPayor()
        {
            InitializeComponent();
            setDefault();
        }
        private void ConvertByPayor_Load(object sender, EventArgs e)
        {
            
        }
        private void setDefault()
        {
            dtDOEFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0);
            dtDOETo.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0);
            setPayor();
        }
        private void setPayor()
        {
            #region Variable
            var strSQL = "SELECT DISTINCT Payor FROM Patient WHERE DOE BETWEEN '" + dtDOEFrom.Value.ToString("yyyy-MM-dd HH:mm") + "' AND '" + dtDOETo.Value.ToString("yyyy-MM-dd HH:mm") + "' ORDER BY Payor ASC;";
            var clsSQL = new clsSQLNative();
            var dt = new DataTable();
            #endregion
            #region Procedure
            if (cbPayor.Items.Count > 0) cbPayor.Items.Clear();
            dt = clsSQL.Bind(strSQL, clsSQLNative.DBType.SQLServer, "MobieConnect");
            var item = new ComboboxItem();
            item.Text = "- ทั้งหมด -";
            item.Value = "ALL";
            cbPayor.Items.Add(item);
            if(dt!=null && dt.Rows.Count > 0)
            {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    item = new ComboboxItem();
                    item.Text = dt.Rows[i]["Payor"].ToString().Trim();
                    item.Value = dt.Rows[i]["Payor"].ToString().Trim();
                    cbPayor.Items.Add(item);
                }
            }
            cbPayor.SelectedIndex = 0;
            #endregion
        }
        private string getPayor()
        {
            #region Variable
            var result = "";
            #endregion
            #region Procedure
            try
            {
                result = cbPayor.SelectedItem.ToString();
                if (result == "- ทั้งหมด -")
                {
                    result = "null";
                }
            }
            catch (Exception) { }
            #endregion
            return result;
        }
        private void dtDOEFrom_ValueChanged(object sender, EventArgs e)
        {
            setPayor();
        }
        private void dtDOETo_ValueChanged(object sender, EventArgs e)
        {
            setPayor();
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            getPatient();
        }
        private void getPatient()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            #region Variable
            var dt = new DataTable();
            var clsTempData = new clsTempData();
            var mobileStatus = "";
            #endregion
            #region Procedure
            if (rbAll.Checked)
            {
                mobileStatus = "All";
            }
            else if (rbNotRegister.Checked)
            {
                mobileStatus = "NotRegister";

            }
            else if (rbRegister.Checked)
            {
                mobileStatus = "Register";
            }
            dt = clsTempData.getPatient(dtDOEFrom.Value,dtDOETo.Value,getPayor(),mobileStatus);
            clsTempData.dtIsConverted = null;
            if (dt!=null && dt.Rows.Count > 0)
            {
                #region Check IsConvertPreOrder
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (clsTempData.IsConverted(
                        dt.Rows[i]["Name"].ToString().Replace(dt.Rows[i]["PreName"].ToString(),""),
                        dt.Rows[i]["LastName"].ToString(),
                        dt.Rows[i]["DOE"].ToString(),
                        dtDOEFrom.Value.ToString("yyyy-MM-dd HH:mm"), dtDOETo.Value.ToString("yyyy-MM-dd HH:mm")))
                    {
                        dt.Rows[i]["IsConvertPreOrder"] = "1";
                    }
                }
                dt.AcceptChanges();
                #endregion
                lblSearchResult.Text = string.Format("พบข้อมูลทั้งหมด {0} รายการ",dt.Rows.Count.ToString());
                gvPatient.DataSource = dt;
                gvPatient.Columns["PreName"].Visible = false;
                gvPatient.Columns["IsConvertPreOrder"].Visible = false;
            }
            else
            {
                lblSearchResult.Text = string.Format("ไม่พบข้อมูลที่ต้องการ");
                gvPatient.DataSource = null;
            }
            #endregion
        }
        #region Filter
        private void cbCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCheckAll.Checked)
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
            for (int i = 0; i < gvPatient.Rows.Count; i++)
            {
                if (gvPatient.Rows[i].Visible == true)
                {
                    gvPatient.Rows[i].Cells["Choose"].Value = true;
                }
            }
        }
        private void UnCheckAll()
        {
            for (int i = 0; i < gvPatient.Rows.Count; i++)
            {
                gvPatient.Rows[i].Cells["Choose"].Value = false;
            }
        }
        #endregion

        private void ddlIsConverted_SelectedIndexChanged(object sender, EventArgs e)
        {
            UnCheckAll();
            var value = ddlIsConverted.Text;
            var count = 0;
            switch (value)
            {
                case "- ทั้งหมด -":
                    for (int i = 0; i < gvPatient.Rows.Count; i++)
                    {
                        gvPatient.Rows[i].Visible = true;
                        count += 1;
                    }
                    break;
                case "เฉพาะที่ยังไม่ Convert":
                    for (int i = 0; i < gvPatient.Rows.Count; i++)
                    {
                        if (gvPatient.Rows[i].Cells["IsConvertPreOrder"].Value.ToString().Trim() == "1")
                        {
                            gvPatient.Rows[i].Visible = false;
                        }
                        else
                        {
                            gvPatient.Rows[i].Visible = true;
                            count += 1;
                        }
                    }
                    break;
                case "เฉพาะที่ Convert แล้ว":
                    for (int i = 0; i < gvPatient.Rows.Count; i++)
                    {
                        if (gvPatient.Rows[i].Cells["IsConvertPreOrder"].Value.ToString().Trim() == "1")
                        {
                            gvPatient.Rows[i].Visible = true;
                            count += 1;
                        }
                        else
                        {
                            gvPatient.Rows[i].Visible = false;
                        }
                    }
                    break;
                default:
                    break;
            }
            lblIsConvertCount.Text = string.Format("พบข้อมูลตรงเงื่อนไขทั้งหมด {0} รายการ", count.ToString());
            if (cbCheckAll.Checked)
            {
                CheckAll();
            }
        }
    }
    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
