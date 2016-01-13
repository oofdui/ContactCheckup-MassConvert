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
    public partial class AutoMassConvert : Form
    {
        #region GlobalVariable
        int syncTimerSecond = int.Parse(System.Configuration.ConfigurationManager.AppSettings["syncTimerSecond"]);
        int syncTimerDelaySecond = int.Parse(System.Configuration.ConfigurationManager.AppSettings["syncTimerDelaySecond"]);
        int syncTimer = 0;
        bool isCancel = false;
        #endregion
        public AutoMassConvert()
        {
            InitializeComponent();
        }
        #region Event
        private void AutoMassConvert_Load(object sender, EventArgs e)
        {
            setDefault();
        }
        private void bwDefault_DoWork(object sender, DoWorkEventArgs e)
        {
            #region Variable
            var clsTempData=new clsTempData();
            var clsInvoker = new clsInvoker();
            var dt = new DataTable();
            var doeFrom = "";
            var doeTo = "";
            var registerFrom = "";
            var registerTo = "";
            var payor = "";
            #endregion
            #region Procedure
            if (clsInvoker.getDateTimePickerChecked(dtDOEFrom)) doeFrom = clsInvoker.getDateTimePickerValue(dtDOEFrom).Value.ToString("yyyy-MM-dd HH:mm");
            if (clsInvoker.getDateTimePickerChecked(dtDOETo)) doeTo = clsInvoker.getDateTimePickerValue(dtDOETo).Value.ToString("yyyy-MM-dd HH:mm");
            if (clsInvoker.getDateTimePickerChecked(dtREGFrom)) registerFrom = clsInvoker.getDateTimePickerValue(dtREGFrom).Value.ToString("yyyy-MM-dd HH:mm");
            if (clsInvoker.getDateTimePickerChecked(dtREGTo)) registerTo = clsInvoker.getDateTimePickerValue(dtREGTo).Value.ToString("yyyy-MM-dd HH:mm");
            if (clsInvoker.getComboBox(ddlPayor)!="- ทั้งหมด -") payor = clsInvoker.getComboBox(ddlPayor);
            dt = clsTempData.getPatientAutoMassConvert(doeFrom, doeTo, registerFrom, registerTo, payor);
            if(dt!=null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!bwDefault.CancellationPending)
                    {
                        #region ListViewUpdate
                        clsInvoker.setListView(lvDefault, new string[]
                        {
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                            dt.Rows[i]["HN"].ToString(),
                            dt.Rows[i]["Name"].ToString()+" "+dt.Rows[i]["LastName"].ToString(),
                            dt.Rows[i]["Payor"].ToString(),
                            dt.Rows[i]["DOE"].ToString(),
                            dt.Rows[i]["InsuranceCompanyName"].ToString(),
                            dt.Rows[i]["PayorAgreementName"].ToString(),
                            dt.Rows[i]["PayorDetailName"].ToString(),
                            dt.Rows[i]["PolicyMasterName"].ToString()
                        });
                        #endregion

                    }
                    else
                    {
                        #region ListViewUpdate
                        clsInvoker.setListView(lvDefault, new string[]
                        {
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                            "",
                            "Cancel by User",
                            "",
                            "",
                            "",
                            "",
                            "",
                            ""
                        });
                        #endregion
                        break;
                    }
                }
            }
            else
            {
                #region ListViewUpdate
                clsInvoker.setListView(lvDefault, new string[]
                {
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                    "",
                    "Don't found data for convert",
                    "",
                    "",
                    "",
                    "",
                    "",
                    ""
                });
                #endregion
            }
            #endregion
        }
        private void dtDOEFrom_ValueChanged(object sender, EventArgs e)
        {
            //setPayor();
        }
        private void dtDOETo_ValueChanged(object sender, EventArgs e)
        {
            //setPayor();
        }
        private void dtREGFrom_ValueChanged(object sender, EventArgs e)
        {
            //setPayor();
        }
        private void dtREGTo_ValueChanged(object sender, EventArgs e)
        {
            //setPayor();
        }
        private void ddlPayor_Click(object sender, EventArgs e)
        {
            setPayor();
        }
        private void btStart_Click(object sender, EventArgs e)
        {
            isCancel = false;
            tmDefault.Enabled = true;
            tmDefault.Start();
            #region setControl
            var clsInvoker = new clsInvoker();
            clsInvoker.setButton(btStart, false);
            clsInvoker.setButton(btStop, true);
            clsInvoker.setPictureBox(anWaiting, true);
            #endregion
        }
        private void btStop_Click(object sender, EventArgs e)
        {
            isCancel = true;
            syncTimer = 0;
            tmDefault.Stop();
            tmDefault.Enabled = false;
            bwDefault.CancelAsync();
            lblSyncTimer.Text = "";
            #region setControl
            var clsInvoker = new clsInvoker();
            clsInvoker.setButton(btStart, true);
            clsInvoker.setButton(btStop, false);
            clsInvoker.setPictureBox(anWaiting, false);
            #endregion
        }
        private void tmDefault_Tick(object sender, EventArgs e)
        {
            if (syncTimer >= syncTimerSecond || syncTimer == 0)
            {
                if (!bwDefault.IsBusy)
                {
                    if (!isCancel)
                    {
                        syncTimer = 0;
                        bwDefault.RunWorkerAsync();
                    }
                }
                else
                {
                    syncTimer -= syncTimerDelaySecond;
                }
            }
            syncTimer += 1;

            lblSyncTimer.Text = string.Format("ระบบจะเริ่มทำงานเมื่อครบ {0} ขณะนี้อยู่ที่ {1}", syncTimerSecond.ToString(), syncTimer.ToString());
        }
        #endregion
        #region Set
        private void setDefault()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            dtDOEFrom.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");dtDOEFrom.Checked = false;
            dtDOETo.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");dtDOETo.Checked = false;
            dtREGFrom.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dtREGTo.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");

            setPayor();
            lblServerDetail.Text = string.Format("Checkup : {0}" + " | " + "B-Connect : {1}",
                System.Configuration.ConfigurationManager.AppSettings["MobieConnect"].Split(new string[] { ";uid=" }, StringSplitOptions.None)[0].Split('=')[1],
                System.Configuration.ConfigurationManager.AppSettings["csBConnect"].Split(new string[] { ";Initial" }, StringSplitOptions.None)[0].Split('=')[1]);
        }
        private void setPayor()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            #region Variable
            var strSQL = new StringBuilder();
            var clsSQL = new clsSQLNative();
            var dt = new DataTable();
            #endregion
            #region Procedure
            #region SQLQuery
            strSQL.Append("SELECT ");
            strSQL.Append("DISTINCT Payor ");
            strSQL.Append("FROM ");
            strSQL.Append("Patient ");
            strSQL.Append("LEFT JOIN tblCheckList ON Patient.rowguid=tblCheckList.PatientUID AND tblCheckList.WFID=1 ");
            strSQL.Append("WHERE ");
            strSQL.Append("1=1 ");
            if (dtDOEFrom.Checked)
            {
                strSQL.Append("AND Patient.DOE>='"+dtDOEFrom.Value.ToString("yyyy-MM-dd HH:mm")+"' ");
            }
            if (dtDOETo.Checked)
            {
                strSQL.Append("AND Patient.DOE<='" + dtDOETo.Value.ToString("yyyy-MM-dd HH:mm") + "' ");
            }
            if (dtREGFrom.Checked)
            {
                strSQL.Append("AND tblCheckList.RegDate>='" + dtREGFrom.Value.ToString("yyyy-MM-dd HH:mm") + "' ");
            }
            if (dtREGTo.Checked)
            {
                strSQL.Append("AND tblCheckList.RegDate<='" + dtREGTo.Value.ToString("yyyy-MM-dd HH:mm") + "' ");
            }
            strSQL.Append("ORDER BY Payor ASC;");
            #endregion
            if (ddlPayor.Items.Count > 0)
            {
                ddlPayor.Items.Clear();
            }
            dt = clsSQL.Bind(strSQL.ToString(), clsSQLNative.DBType.SQLServer, "MobieConnect");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddlPayor.Items.Add(dt.Rows[i]["Payor"].ToString().Trim());
                }
            }
            ddlPayor.Items.Insert(0,"- ทั้งหมด -");
            ddlPayor.SelectedIndex = 0;
            #endregion
        }
        #endregion
        #region Function

        #endregion
    }
}