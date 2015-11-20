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
    public partial class ConvertResult : Form
    {
        public ConvertResult()
        {
            InitializeComponent();
        }

        private void ConvertResult_Load(object sender, EventArgs e)
        {
            #region Variable
            var clsTempData = new clsTempData();
            var dt = new DataTable();
            #endregion
            #region Procedure
            dt = clsTempData.dtConvertResult;
            if(dt!=null && dt.Rows.Count > 0)
            {
                lblDefault.Text = "พบข้อมูลทั้งหมด " + dt.Rows.Count.ToString() + "";
                gvDefault.DataSource = dt;
            }
            else
            {
                lblDefault.Text = "ไม่พบข้อมูล";
            }
            #endregion
        }

        private void gvDefault_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(gvDefault.Rows[e.RowIndex].Cells["Result"].Value!= null)
            { 
                if (gvDefault.Rows[e.RowIndex].Cells["Result"].Value.ToString() == "Success")
                {
                    e.CellStyle.ForeColor = Color.Green;
                }
                else if(gvDefault.Rows[e.RowIndex].Cells["Result"].Value.ToString() == "Fail")
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
            }
        }
    }
}
