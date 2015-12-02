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
    public partial class MapPayorSummary : Form
    {
        public MapPayorSummary()
        {
            InitializeComponent();
        }
        private void MapPayorSummary_Load(object sender, EventArgs e)
        {
            setDefault();
        }
        private void setDefault()
        {
            #region Variable
            var clsTempData = new clsTempData();
            var dt = new DataTable();
            #endregion
            #region Procedure
            dt = clsTempData.getPayorMapSummary();
            if(dt!=null && dt.Rows.Count > 0)
            {
                gvDefault.DataSource = dt;
            }
            else
            {
                gvDefault.DataSource = null;
            }
            #endregion
        }
    }
}
