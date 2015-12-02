using MassConvert.Database;
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
    public partial class MapPayor : Form
    {
        #region GlobalVariable
        SQL db = new SQL();
        #endregion
        public MapPayor()
        {
            InitializeComponent();
        }
        #region Event
        private void MapPayor_Load(object sender, EventArgs e)
        {
            setDefault();
            setPayor();
            setPayorDetail();
        }
        private void btPayorSearch_Click(object sender, EventArgs e)
        {
            setPayor(txtPayorSearch.Text.Trim());
        }
        private void txtPayorSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                setPayor(txtPayorSearch.Text.Trim());
            }
        }
        private void lvPayor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvPayor.SelectedItems.Count > 0)
            {
                btDOESubmit.Enabled = true;
                setClearPayorDetail();
                foreach (ListViewItem item in lvPayor.SelectedItems)
                {
                    setDOE(item.Text.Trim());
                }
            }
            else
            {
                btDOESubmit.Enabled = false;
                lblPayor.Text = "";
            }
        }
        private void btDOESubmit_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            #region Variable
            var strSQL = new StringBuilder();
            var clsSQL = new clsSQLNative();
            var payor = "";
            #endregion
            #region Procedure
            #region Validate
            if (dtDOEFrom.Value >= dtDOETo.Value)
            {
                MessageBox.Show("โปรดเลือกช่วงวันที่เริ่มต้นให้น้อยกว่าวันที่สิ้นสุด", "Error on DatePicker", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (lvPayor.SelectedItems.Count == 0)
            {
                MessageBox.Show("โปรดเลือกชื่อ Payor ด้านซ้ายก่อน", "Please choose payor first", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                foreach (ListViewItem item in lvPayor.SelectedItems)
                {
                    payor = item.Text.Trim();
                }
            }
            if (payor.Trim() == "")
            {
                MessageBox.Show("ไม่พบชื่อ Payor ที่เลือก", "Please choose payor first", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion
            #region SQLQuery
            strSQL.Append("INSERT INTO ");
            strSQL.Append("MassConvertPayorMap");
            strSQL.Append("(Payor,DOEFrom,DOETo,CUser,MUser)");
            strSQL.Append("VALUES(");
            strSQL.Append("'"+payor.SQLQueryFilter()+"',");
            strSQL.Append("'" + dtDOEFrom.Value.ToString("yyyy-MM-dd HH:mm") + "',");
            strSQL.Append("'" + dtDOETo.Value.ToString("yyyy-MM-dd HH:mm") + "',");
            strSQL.Append("'" + clsTempData.Username + "',");
            strSQL.Append("'" + clsTempData.Username + "'");
            strSQL.Append(");");
            #endregion
            if (clsSQL.Execute(strSQL.ToString(), clsSQLNative.DBType.SQLServer, "MobieConnect"))
            {
                setDOE(payor);
                MessageBox.Show("เพิ่มช่วงวันที่ออกตรวจเสร็จสมบูรณ์", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("เพิ่มช่วงวันที่ออกตรวจล้มเหลว", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion
        }
        private void lvDOE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvDOE.SelectedItems.Count > 0 && lvPayor.SelectedItems.Count>0)
            {
                var payor = "";
                var MassConvertPayorMapUID = 0;
                ddlPayor.Enabled = true;
                ddlAgreement.Enabled = true;
                ddlPayorOffice.Enabled = true;
                ddlPolicy.Enabled = true;
                foreach (ListViewItem item in lvPayor.SelectedItems)
                {
                    payor = item.Text.Trim();
                }
                foreach (ListViewItem lvi in lvDOE.SelectedItems)
                {
                    lblPayorDetailHeader.Text = string.Format(
                        "{0}"+Environment.NewLine+"{1}-{2}",
                        payor,
                        lvi.SubItems[1].Text,
                        lvi.SubItems[2].Text);
                    MassConvertPayorMapUID = int.Parse(lvi.SubItems[0].Text.Trim());
                }
                setPayorDetail(MassConvertPayorMapUID);
            }
            else
            {
                setClearPayorDetail();
            }
        }
        private void ddlPayor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (getDropDownListValue(ddlPayor, "UID") != "0")
                {
                    string PayorUID = getDropDownListValue(ddlPayor, "UID");
                    if (!string.IsNullOrEmpty(PayorUID) || PayorUID != "0")
                    {
                        DataTable adt = db.Select_Agreement(PayorUID);
                        ddlAgreement.DataSource = adt;
                        ddlAgreement.DisplayMember = "Name";
                        ddlAgreement.ValueMember = "UID";

                        DataTable pyfdt = db.Select_PayorDetail(PayorUID);
                        ddlPayorOffice.DataSource = pyfdt;
                        ddlPayorOffice.DisplayMember = "PayorName";
                        ddlPayorOffice.ValueMember = "UID";

                        DataTable pofdt = db.Select_Policy(PayorUID);
                        ddlPolicy.DataSource = pofdt;
                        ddlPolicy.DisplayMember = "PolicyName";
                        ddlPolicy.ValueMember = "UID";

                        btSave.Enabled = true;
                    }
                }
                else
                {
                    ddlAgreement.DataSource = null;
                    ddlPayorOffice.DataSource = null;
                    ddlPolicy.DataSource = null;
                    btSave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "PayorAutoSelect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btSave_Click(object sender, EventArgs e)
        {
            #region Variable
            var clsSQL = new clsSQLNative();
            var clsTempData = new clsTempData();
            var strSQL = new StringBuilder();
            var MassConvertPayorMapUID = "";
            var PayorUID = "";
            var AgreementUID = "";
            var PayorOfficeUID = "";
            var PolicyUID = "";
            #endregion
            #region Procedure
            foreach (ListViewItem lvi in lvDOE.SelectedItems)
            {
                MassConvertPayorMapUID = lvi.SubItems[0].Text;
            }
            #region Validation
            if (MassConvertPayorMapUID.Trim()=="")
            {
                MessageBox.Show("โปรดเลือกช่วงวันที่ออกตรวจก่อน", "Please choose", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lvDOE.Focus();
                return;
            }
            if (lvPayor.SelectedItems.Count == 0)
            {
                MessageBox.Show("โปรดเลือกรายชื่อบริษัทก่อน", "Please choose", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lvPayor.Focus();
                return;
            }
            if (lvDOE.SelectedItems.Count == 0)
            {
                MessageBox.Show("โปรดเลือกช่วงวันที่ออกตรวจก่อน", "Please choose", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lvDOE.Focus();
                return;
            }
            PayorUID = getDropDownListValue(ddlPayor, "UID");
            if (PayorUID == "0")
            {
                MessageBox.Show("โปรดระบุชื่อ Payor ก่อน", "Please choose", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ddlPayor.Focus();
                return;
            }
            AgreementUID = getDropDownListValue(ddlAgreement, "UID");
            if (AgreementUID.Trim() == "")
            {
                MessageBox.Show("โปรดระบุชื่อ Agreement ก่อน", "Please choose", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ddlAgreement.Focus();
                return;
            }
            PayorOfficeUID = getDropDownListValue(ddlPayorOffice, "UID");
            if (PayorOfficeUID.Trim() == "")
            {
                MessageBox.Show("โปรดระบุชื่อ PayorOffice ก่อน", "Please choose", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ddlPayorOffice.Focus();
                return;
            }
            PolicyUID = getDropDownListValue(ddlPolicy, "UID");
            if (PolicyUID.Trim() == "")
            {
                MessageBox.Show("โปรดระบุชื่อ Policy ก่อน", "Please choose", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ddlPolicy.Focus();
                return;
            }
            #endregion
            #region SQLQuery
            strSQL.Append("UPDATE ");
            strSQL.Append("MassConvertPayorMap ");
            strSQL.Append("SET ");
            strSQL.Append("InsuranceCompanyUID=" + getDropDownListValue(ddlPayor, "UID") + ",");
            strSQL.Append("PayorAgreementUID=" + getDropDownListValue(ddlAgreement, "UID") + ",");
            strSQL.Append("PayorDetailUID=" + getDropDownListValue(ddlPayorOffice, "UID") + ",");
            strSQL.Append("PolicyMasterUID=" + getDropDownListValue(ddlPolicy, "UID") + ",");
            strSQL.Append("MWhen=GETDATE(),");
            strSQL.Append("MUser='"+clsTempData.Username+"' ");
            strSQL.Append("WHERE ");
            strSQL.Append("UID="+ MassConvertPayorMapUID + ";");
            #endregion
            if (clsSQL.Execute(strSQL.ToString(), clsSQLNative.DBType.SQLServer, "MobieConnect"))
            {
                MessageBox.Show("บันทึกข้อมูลเสร็จสิ้น", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("เกิดข้อผิดพลาดขณะบันทึกข้อมูล"+Environment.NewLine+Environment.NewLine+strSQL.ToString(), "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion
        }
        private void lvDOE_DoubleClick(object sender, EventArgs e)
        {
            if (lvDOE.SelectedItems.Count > 0)
            {
                var MassConvertPayorMapUID = "";
                foreach (ListViewItem lvi in lvDOE.SelectedItems)
                {
                    MassConvertPayorMapUID = lvi.SubItems[0].Text;
                }
                if (MassConvertPayorMapUID != "")
                {
                    MapPayorDOE child = new MapPayorDOE();
                    DialogResult dr;
                    child.MassConvertPayorMapUID = int.Parse(MassConvertPayorMapUID);
                    dr=child.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        setClearPayorDetail();
                        foreach (ListViewItem item in lvPayor.SelectedItems)
                        {
                            setDOE(item.Text.Trim());
                        }
                    }
                }
            }
        }
        private void btPayorMapSummary_Click(object sender, EventArgs e)
        {
            var child = new MapPayorSummary();
            child.ShowDialog(this);
        }
        #endregion
        private void setDefault()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            dtDOEFrom.Value = DateTime.Parse(DateTime.Now.AddDays(0).ToString("yyyy-MM-dd") + " 00:00");
            dtDOETo.Value = DateTime.Parse(DateTime.Now.AddDays(0).ToString("yyyy-MM-dd") + " 23:59");
            btDOESubmit.Enabled = false;
        }
        private void setPayor(string searchName="")
        {
            #region Variable
            var clsTempData = new clsTempData();
            var dt = new DataTable();
            #endregion
            #region Procedure
            if (lvPayor.Items.Count > 0)
            {
                lvPayor.Items.Clear();
            }
            dt = clsTempData.getPayor(searchName);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lvPayor.Items.Add(dt.Rows[i]["Payor"].ToString().Trim());
                    if (dt.Rows[i]["CountMap"].ToString() != "0")
                    {
                        lvPayor.Items[i].BackColor = ColorTranslator.FromHtml("#FFC90E");
                    }
                }
                lvPayor.Columns[0].Width = -2;
                lblPayorSearchResult.Text = string.Format("พบข้อมูล {0} รายการ", dt.Rows.Count.ToString());
            }
            else
            {
                lblPayorSearchResult.Text = string.Format("พบข้อมูล {0} รายการ", "0");
            }
            setClearPayorDetail();
            #endregion
        }
        private void setClearPayorDetail()
        {
            #region ClearPayorDetail
            ddlPayor.Enabled = false; if (ddlPayor.Items.Count > 0)ddlPayor.SelectedIndex = 0;
            ddlAgreement.Enabled = false; ddlAgreement.DataSource = null;
            ddlPayorOffice.Enabled = false; ddlPayorOffice.DataSource = null;
            ddlPolicy.Enabled = false; ddlPolicy.DataSource = null;
            lblPayorDetailHeader.Text = "";
            #endregion
        }
        private void setDOE(string searchPayor)
        {
            #region Variable
            var dt = new DataTable();
            var clsTempData = new clsTempData();
            #endregion
            #region Procedure
            lblPayor.Text = searchPayor;
            if (lvDOE.Items.Count > 0)
            {
                lvDOE.Items.Clear();
            }
            dt = clsTempData.getDOE(searchPayor);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lvDOE.Items.Add(new ListViewItem(new string[] 
                    {
                        dt.Rows[i]["UID"].ToString(),
                        DateTime.Parse(dt.Rows[i]["DOEFrom"].ToString()).ToString("yyyy-MM-dd HH:mm"),
                        DateTime.Parse(dt.Rows[i]["DOETo"].ToString()).ToString("yyyy-MM-dd HH:mm")
                    }
                    ));
                }
                lvDOE.Columns[0].Width = 0;
                lvDOE.Columns[1].Width = (lvDOE.Width / 2);
                //lvDOE.Columns[0].TextAlign = HorizontalAlignment.Center;
                lvDOE.Columns[2].Width = -2;
                //lvDOE.Columns[1].TextAlign = HorizontalAlignment.Center;
                lblDOEResult.Text = string.Format("พบข้อมูล {0} รายการ", dt.Rows.Count.ToString());
            }
            else
            {
                lblDOEResult.Text = string.Format("พบข้อมูล {0} รายการ", "0");
            }
            #endregion
        }
        private void setPayorDetail()
        {
            #region Variable
            var dt = new DataTable();
            #endregion
            #region Procedure
            #region Payor
            try
            {
                dt = db.Select_InsuranceCompany();
                var drPayor = dt.NewRow();
                drPayor[0] = "0";
                drPayor[1] = "- ไม่ระบุ -";
                dt.Rows.InsertAt(drPayor, 0);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlPayor.DataSource = dt;
                    ddlPayor.DisplayMember = "CompanyName";
                    ddlPayor.ValueMember = "UID";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "setPayorDetail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion
            #endregion
        }
        private void setPayorDetail(int MassConvertPayorMapUID)
        {
            #region Variable
            var dt = new DataTable();
            var strSQL = new StringBuilder();
            var clsSQL = new clsSQLNative();
            #endregion
            #region Procedure
            #region Payor
            try
            {
                #region SQLQuery
                strSQL.Append("SELECT InsuranceCompanyUID,PayorAgreementUID,PayorDetailUID,PolicyMasterUID FROM MassConvertPayorMap WHERE UID="+ MassConvertPayorMapUID.ToString() + " AND StatusFlag='A';");
                #endregion
                dt = clsSQL.Bind(strSQL.ToString(), clsSQLNative.DBType.SQLServer, "MobieConnect");
                if(dt!=null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["InsuranceCompanyUID"].ToString() != "")
                    {
                        ddlPayor.SelectedValue = dt.Rows[0]["InsuranceCompanyUID"].ToString();
                    }
                    if (dt.Rows[0]["PayorAgreementUID"].ToString() != "")
                    {
                        ddlAgreement.SelectedValue = dt.Rows[0]["PayorAgreementUID"].ToString();
                    }
                    if (dt.Rows[0]["PayorDetailUID"].ToString() != "")
                    {
                        ddlPayorOffice.SelectedValue = dt.Rows[0]["PayorDetailUID"].ToString();
                    }
                    if (dt.Rows[0]["PolicyMasterUID"].ToString() != "")
                    {
                        ddlPolicy.SelectedValue = dt.Rows[0]["PolicyMasterUID"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "setPayorDetail(UID)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion
            #endregion
        }
        #region Function
        private void ListViewBuilderInvoke(ListView listView, Color? color, int columnFullWidth, params string[] value)
        {
            listView.Items.Add(new ListViewItem(value));
            if (color != null)
            {
                listView.Items[listView.Items.Count - 1].ForeColor = (Color)color;
            }
            listView.EnsureVisible(listView.Items.Count - 1);
            ListViewResizeColumn(listView, columnFullWidth);
        }
        public void ListViewBuilder(ListView listView, Color? color = null, int columnFullWidth = 99, params string[] value)
        {
            if (listView.InvokeRequired)
            {
                listView.Invoke(new MethodInvoker(delegate
                {
                    ListViewBuilderInvoke(listView, color, columnFullWidth, value);
                }));
            }
            else
            {
                ListViewBuilderInvoke(listView, color, columnFullWidth, value);
            }
        }
        private void ListViewResizeColumnInvoke(ListView listView, int column)
        {
            var totalColumnWidth = 0;
            var calculateColumnWidth = 0;
            for (int i = 0; i < listView.Columns.Count; i++)
            {
                if (column == 99)
                {
                    if (i < listView.Columns.Count - 1)
                    {
                        listView.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
                    }
                }
                else
                {
                    if (column != i)
                    {
                        listView.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
                        totalColumnWidth += listView.Columns[i].Width;
                    }
                }
            }
            #region FullFill
            if (column == 99)
            {
                listView.Columns[listView.Columns.Count - 1].Width = -2;
            }
            else
            {
                calculateColumnWidth = listView.Width - totalColumnWidth - (listView.Width / 10);//ลบด้วย 10% ของความกว้างทั้งหมดอีกรอบกันเลย
                listView.Columns[column].Width = calculateColumnWidth;
                listView.Columns[listView.Columns.Count - 1].Width = -2;
            }
            #endregion
        }
        public void ListViewResizeColumn(ListView listView, int column = 99)
        {
            if (listView.InvokeRequired)
            {
                #region Invoke
                listView.Invoke(new MethodInvoker(delegate
                {
                    ListViewResizeColumnInvoke(listView, column);
                }));
                #endregion
            }
            else
            {
                ListViewResizeColumnInvoke(listView, column);
            }
        }
        private string getDropDownListValue(ComboBox ddlName,string columnName)
        {
            #region Variable
            var result = "";
            #endregion
            #region Procedure
            try
            {
                var drv = (DataRowView)ddlName.SelectedItem;
                var dr = drv.Row;
                result = dr[columnName].ToString();
            }
            catch (Exception)
            {

            }
            #endregion
            return result;
        }
        #endregion
    }
}
