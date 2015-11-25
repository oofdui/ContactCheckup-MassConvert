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
    public partial class MDIMassConvert : Form
    {
        private int childFormNumber = 0;
        string strVersion = "";

        public MDIMassConvert()
        {
            InitializeComponent();

            UsageLogBuilder();
            #region TitleBuilder
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            strVersion = version.Major.ToString() +
                                 "." + version.Minor.ToString();
            this.Text = this.Text + " v." + strVersion;
            #endregion
            #region FooterBuilder
            Database.SQL clsSQL = new Database.SQL();
            string[] splitBConnect1 = clsSQL.Path.Split(';');
            string[] splitBConnectIP = splitBConnect1[0].Split('=');
            string[] splitBConnectDB = splitBConnect1[1].Split('=');
            string[] splitCheckup1 = System.Configuration.ConfigurationManager.AppSettings["MobieConnect"].Split(';');
            string[] splitCheckupIP = splitCheckup1[0].Split('=');
            string[] splitCheckupDB = splitCheckup1[3].Split('=');
            radLabelElement1.Text = string.Format(
                "MassConvert v.{0} | LastUpdate : {1} | B-Connect : {2}/{3} | Checkup : {4}/{5}",
                strVersion,
                LastUpdateBuilder().ToString("dd/MM/yyyy HH:mm"),
                splitBConnectIP[1],
                splitBConnectDB[1],
                splitCheckupIP[1],
                splitCheckupDB[1]);
            #endregion
        }

        private DateTime LastUpdateBuilder()
        {
            string filePath = System.Reflection.Assembly.GetCallingAssembly().Location;
            const int c_PeHeaderOffset = 60;
            const int c_LinkerTimestampOffset = 8;
            byte[] b = new byte[2048];
            System.IO.Stream s = null;

            try
            {
                s = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                s.Read(b, 0, 2048);
            }
            finally
            {
                if (s != null)
                {
                    s.Close();
                }
            }

            int i = System.BitConverter.ToInt32(b, c_PeHeaderOffset);
            int secondsSince1970 = System.BitConverter.ToInt32(b, i + c_LinkerTimestampOffset);
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            dt = dt.AddSeconds(secondsSince1970);
            dt = dt.ToLocalTime();
            return dt;
        }
        private bool UsageLogBuilder()
        {
            var result = false;
            if (System.Configuration.ConfigurationManager.AppSettings["UsageLogEnable"].ToLower() == "true")
            {
                WSCenter.ServiceSoapClient wsCenter = new WSCenter.ServiceSoapClient();

                try
                {
                    wsCenter.InsertLogApplicationBySite(
                        "ContactCheckup",
                        "MassConvert",
                        System.Configuration.ConfigurationManager.AppSettings["SiteCode"],
                        "",
                        GetIPAddress(),
                        GetHostName());

                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                }
            }
            return result;
        }
        private string GetHostName()
        {
            string rtnValue = "";

            rtnValue = System.Environment.MachineName;

            return rtnValue;
        }
        private string GetIPAddress()
        {
            string rtnValue = "";
            System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(GetHostName());

            foreach (System.Net.IPAddress ipAddress in ipEntry.AddressList)
            {
                if (ipAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    rtnValue = ipAddress.ToString();
                    break;
                }
            }

            return rtnValue;
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }
        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }
        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }
        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }
        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }
        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
        private void CloseFrom()
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
        private void byPayorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseFrom();
            Form1 frmChild = new Form1();
            frmChild.MdiParent = this;
            frmChild.WindowState = FormWindowState.Maximized;
            frmChild.Text = "Convert by payor";
            frmChild.Show();
        }
        private void byPersonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseFrom();
            FormIndividual frmChild = new FormIndividual();
            frmChild.MdiParent = this;
            frmChild.WindowState = FormWindowState.Maximized;
            frmChild.Text = "Convert by personal";
            frmChild.Show();
        }
        private void ผกPackageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseFrom();
            frmProchklistMapPackage frmChild = new frmProchklistMapPackage();
            frmChild.MdiParent = this;
            frmChild.WindowState = FormWindowState.Maximized;
            frmChild.Text = "Map ProCheckList กับ Package";
            frmChild.Show();
        }
        private void patientPackageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseFrom();
            frmMapPackage frmChild = new frmMapPackage();
            frmChild.MdiParent = this;
            frmChild.WindowState = FormWindowState.Maximized;
            frmChild.Text = "Map Patient กับ Package";
            frmChild.Show();
        }
        private void genLabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseFrom();
            frmGenLabNo frmChild = new frmGenLabNo();
            frmChild.MdiParent = this;
            frmChild.WindowState = FormWindowState.Maximized;
            frmChild.Text = "Gen Lab Number";
            frmChild.Show();
        }
        private void btGenLabNo_Click(object sender, EventArgs e)
        {
            CloseFrom();
            frmGenLabNo frmChild = new frmGenLabNo();
            frmChild.MdiParent = this;
            frmChild.WindowState = FormWindowState.Maximized;
            frmChild.Text = "Gen Lab Number";
            frmChild.Show();
        }

        private void btConvertPayor_Click(object sender, EventArgs e)
        {
            CloseFrom();
            frmConvertPayor frmChild = new frmConvertPayor();
            //ConvertByPayor frmChild = new ConvertByPayor();
            frmChild.MdiParent = this;
            frmChild.WindowState = FormWindowState.Maximized;
            frmChild.Text = "Convert Order by Payor";
            frmChild.Show();
        }

        private void btMapPackage_Click(object sender, EventArgs e)
        {
            CloseFrom();
            frmMapPatientPackage frmChild = new frmMapPatientPackage();
            frmChild.MdiParent = this;
            frmChild.WindowState = FormWindowState.Maximized;
            frmChild.Text = "Map Patient vs Package";
            frmChild.Show();
        }

        private void btConvertIndividula_Click(object sender, EventArgs e)
        {
            CloseFrom();
            frmConvertIndividual frmChild = new frmConvertIndividual();
            frmChild.MdiParent = this;
            frmChild.WindowState = FormWindowState.Maximized;
            frmChild.Text = "Convert Order by Personal";
            frmChild.Show();
        }

        private void radImageButtonElement1_Click(object sender, EventArgs e)
        {
            CloseFrom();
            frmPrnStkLab frmChild = new frmPrnStkLab();
            frmChild.MdiParent = this;
            frmChild.WindowState = FormWindowState.Maximized;
            frmChild.Text = "Print Sticker Lab Barcode";
            frmChild.Show();
        }

        private void btConvertPayorByRegisterDate_Click(object sender, EventArgs e)
        {
            CloseFrom();
            frmConvertPayorByRegisterDate frmChild = new frmConvertPayorByRegisterDate();
            frmChild.MdiParent = this;
            frmChild.WindowState = FormWindowState.Maximized;
            frmChild.Text = "Convert Order by Payor (RegisterDate)";
            frmChild.Show();
        }
    }
}
