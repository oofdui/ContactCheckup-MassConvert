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
    public partial class MapPayorDOE : Form
    {
        #region Property
        private int _massConvertPayorMapUID = 0;
        public int MassConvertPayorMapUID
        {
            get { return _massConvertPayorMapUID; }
            set { _massConvertPayorMapUID = value; }
        }
        #endregion
        public MapPayorDOE()
        {
            InitializeComponent();
        }
        private void MapPayorDOE_Load(object sender, EventArgs e)
        {
            setDOE();
        }
        private void setDOE()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            if (_massConvertPayorMapUID != 0)
            {
                var dt = new DataTable();
                var clsSQL = new clsSQLNative();
                var strSQL = new StringBuilder();

                strSQL.Append("SELECT Payor,DOEFrom,DOETo FROM MassConvertPayorMap WHERE StatusFlag='A' AND UID="+_massConvertPayorMapUID);
                dt = clsSQL.Bind(strSQL.ToString(), clsSQLNative.DBType.SQLServer, "MobieConnect");
                if(dt!=null && dt.Rows.Count > 0)
                {
                    btDOESubmit.Enabled = true; btDelete.Enabled = true;
                    lblPayor.Text = dt.Rows[0]["Payor"].ToString();
                    dtDOEFrom.Value = DateTime.Parse(dt.Rows[0]["DOEFrom"].ToString());
                    dtDOETo.Value = DateTime.Parse(dt.Rows[0]["DOETo"].ToString());
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูลช่วงวันที่ออกตรวจที่คุณเลือก");
                }
            }
            else
            {
                MessageBox.Show("โปรดเลือกช่วงวันที่ออกตรวจก่อน");
            }
        }
        private void btDOESubmit_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            if (_massConvertPayorMapUID != 0)
            {
                var clsSQL = new clsSQLNative();
                var strSQL = new StringBuilder();

                strSQL.Append("UPDATE MassConvertPayorMap SET DOEFrom='"+dtDOEFrom.Value.ToString("yyyy-MM-dd HH:mm")+ "',DOETo='" + dtDOETo.Value.ToString("yyyy-MM-dd HH:mm") + "' WHERE StatusFlag='A' AND UID=" + _massConvertPayorMapUID);
                if(clsSQL.Execute(strSQL.ToString(), clsSQLNative.DBType.SQLServer, "MobieConnect"))
                {
                    MessageBox.Show("บันทึกข้อมูลเสร็จสิ้น","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("เกิดข้อผิดพลาดขณะบันทึกข้อมูล", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("โปรดเลือกช่วงวันที่ออกตรวจก่อน");
            }
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("ยืนยันการลบข้อมูล", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                var strSQL = "";
                var clsSQL = new clsSQLNative();
                strSQL = "UPDATE MassConvertPayorMap SET StatusFlag='D' WHERE UID="+_massConvertPayorMapUID+";";
                if (clsSQL.Execute(strSQL, clsSQLNative.DBType.SQLServer, "MobieConnect"))
                {
                    MessageBox.Show("ลบข้อมูลเสร็จสิ้น", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("เกิดข้อผิดพลาดขณะลบข้อมูล", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
