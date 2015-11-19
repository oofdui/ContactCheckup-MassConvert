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

namespace MassConvert
{
    public partial class frmProchklistMapPackage : Form
    {
        ExcData exc;
        SQL db;
        public frmProchklistMapPackage()
        {
            InitializeComponent();
        }

        private void frmProchklistMapPackage_Load(object sender, EventArgs e)
        {
            BindProChkList();
            //Add ปุ่ม Check box เข้าไปให้ทุก row ของ gridview
            AddingCheckBoxColumn();
        }
        private void BindProChkList()
        {
            exc = new Database.ExcData();
            string SQL = "SELECT [CheckList] ,[CheckListDesc] FROM [Checklist] ORDER BY CheckList DESC";
            ddlProChkList.DataSource = exc.data_Table(SQL);
            ddlProChkList.DisplayMember = "CheckList";
            ddlProChkList.ValueMember = "CheckList";
            ddlProChkList.Refresh();
        }

        private void btFindPackage_Click(object sender, EventArgs e)
        {
            db = new SQL();
            gvPackage.DataSource = db.Select_Package_by_PackageName(txtPackage.Text.Trim());
            //gvPackage.Columns["Chk"].Width = 50;
            gvPackage.Columns["UID"].Width = 50;
            gvPackage.Columns["Code"].Width = 150;
            
            gvPackage.Refresh();
        }

        private void ddlProChkList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindProchklistMapPackage(ddlProChkList.SelectedValue.ToString());
        }
        private void BindProchklistMapPackage(string Prochklist)
        {
            exc = new ExcData();
            string SQL = "SELECT [ProChkList] ,[PackageCode] FROM [tblProchklistMapPackage] where [ProChkList] = '" + Prochklist  + "' AND StatusFlag = 'A'";
            gvProchklistMapPackage.DataSource = exc.data_Table(SQL);
            gvProchklistMapPackage.Refresh();
        }
        private void AddingCheckBoxColumn()
        {
            DataGridViewCheckBoxColumn colChk = new DataGridViewCheckBoxColumn();
            colChk.Name = "Check";
            colChk.HeaderText = "เลือก";
            gvPackage.Columns.Add(colChk);
            //gvPatient.AutoSize = true;
            gvPackage.AllowUserToAddRows = false;
            gvPackage.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gvPackage.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            for (int i = 0; i < gvPackage.RowCount; i++)
            {
                gvPackage.Rows[i].Cells["colChk"].Value = false;
            }
        }
        private void btSave_Click(object sender, EventArgs e)
        {
            exc = new ExcData();
            string SQL = string.Empty;
            string SQLChk = string.Empty;
            int NowRow = 0;
            foreach (DataGridViewRow row in gvPackage.Rows)
            {
                //Loop ทำงานเฉพาะคนที่ Check Box
                if (Convert.ToBoolean(row.Cells["Check"].Value) == true)
                {
                    SQLChk = "SELECT * FROM [tblProchklistMapPackage] where ProChkList = '" + ddlProChkList.SelectedValue.ToString() + "' and PackageCode = '" + gvPackage.Rows[NowRow].Cells["Code"].Value.ToString() + "' and StatusFlag = 'A'";
                    DataTable dtChk = new DataTable();
                    dtChk = exc.data_Table(SQLChk);
                    if (dtChk.Rows.Count <= 0 || dtChk == null)
                    {
                        SQL = "INSERT INTO [tblProchklistMapPackage]([ProChkList] ,[PackageCode] ,[StatusFlag])"
                            + " VALUES('" + ddlProChkList.SelectedValue.ToString() + "' ,'" + gvPackage.Rows[NowRow].Cells["Code"].Value.ToString() + "' ,'A')";
                        if (exc.ExecData(SQL) == false)
                        {
                            MessageBox.Show("Save package code : " + gvPackage.Rows[NowRow].Cells["Code"].Value.ToString() + " failed.");
                        }
                    }
                }
                NowRow++;
            }
            BindProchklistMapPackage(ddlProChkList.SelectedValue.ToString());
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            exc = new ExcData();
            string SQL = "update tblProchklistMapPackage set StatusFlag = 'D' where ProChkList = '" + gvProchklistMapPackage.CurrentRow.Cells["ProChkList"].Value.ToString() + "' and PackageCode = '" + gvProchklistMapPackage.CurrentRow.Cells["PackageCode"].Value.ToString() + "'";
            if(exc.ExecData(SQL) == false)
            {
                MessageBox.Show("Cannot delete this package");
            }
            BindProchklistMapPackage(ddlProChkList.SelectedValue.ToString());
        }


    }
}