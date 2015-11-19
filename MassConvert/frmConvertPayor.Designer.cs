namespace MassConvert
{
    partial class frmConvertPayor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.desertTheme1 = new Telerik.WinControls.Themes.DesertTheme();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.lblStatus = new Telerik.WinControls.UI.RadLabel();
            this.chkBox = new Telerik.WinControls.UI.RadCheckBox();
            this.progressBar1 = new Telerik.WinControls.UI.RadProgressBar();
            this.txtFilterPayor = new Telerik.WinControls.UI.RadTextBox();
            this.gvPatient = new Telerik.WinControls.UI.RadGridView();
            this.btConvert = new Telerik.WinControls.UI.RadButton();
            this.desertTheme2 = new Telerik.WinControls.Themes.DesertTheme();
            this.radGroupBox2 = new Telerik.WinControls.UI.RadGroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btFind = new Telerik.WinControls.UI.RadButton();
            this.dtpTimeTo = new Telerik.WinControls.UI.RadDateTimePicker();
            this.dtpTimeFrom = new Telerik.WinControls.UI.RadDateTimePicker();
            this.dtpDateTo = new Telerik.WinControls.UI.RadDateTimePicker();
            this.dtpDateFrom = new Telerik.WinControls.UI.RadDateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.rbRegister = new System.Windows.Forms.RadioButton();
            this.rbNotRegister = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.lblCountPT = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.radGroupBox3 = new Telerik.WinControls.UI.RadGroupBox();
            this.lblPercentTag = new Telerik.WinControls.UI.RadLabel();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.cboPolicy = new Telerik.WinControls.UI.RadDropDownList();
            this.cboPayorOffice = new Telerik.WinControls.UI.RadDropDownList();
            this.cboAgreement = new Telerik.WinControls.UI.RadDropDownList();
            this.cboPayor = new Telerik.WinControls.UI.RadDropDownList();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilterPayor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPatient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPatient.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btConvert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).BeginInit();
            this.radGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btFind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTimeTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTimeFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDateTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDateFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox3)).BeginInit();
            this.radGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblPercentTag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPolicy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPayorOffice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboAgreement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPayor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radLabel1
            // 
            this.radLabel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.radLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.radLabel1.Location = new System.Drawing.Point(316, 15);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(126, 19);
            this.radLabel1.TabIndex = 45;
            this.radLabel1.Text = "Filter by Company :";
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.lblStatus.Location = new System.Drawing.Point(12, 416);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(52, 15);
            this.lblStatus.TabIndex = 51;
            this.lblStatus.Text = "Waiting...";
            // 
            // chkBox
            // 
            this.chkBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.chkBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkBox.Location = new System.Drawing.Point(43, 15);
            this.chkBox.Name = "chkBox";
            // 
            // 
            // 
            this.chkBox.RootElement.ControlBounds = new System.Drawing.Rectangle(43, 15, 100, 18);
            this.chkBox.RootElement.StretchHorizontally = true;
            this.chkBox.RootElement.StretchVertically = true;
            this.chkBox.Size = new System.Drawing.Size(75, 19);
            this.chkBox.TabIndex = 44;
            this.chkBox.Text = "Check All";
            this.chkBox.ThemeName = "Desert";
            this.chkBox.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkBox_ToggleStateChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.progressBar1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.progressBar1.ImageIndex = -1;
            this.progressBar1.ImageKey = "";
            this.progressBar1.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.progressBar1.Location = new System.Drawing.Point(12, 437);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.SeparatorColor1 = System.Drawing.Color.White;
            this.progressBar1.SeparatorColor2 = System.Drawing.Color.White;
            this.progressBar1.SeparatorColor3 = System.Drawing.Color.White;
            this.progressBar1.SeparatorColor4 = System.Drawing.Color.White;
            this.progressBar1.Size = new System.Drawing.Size(692, 23);
            this.progressBar1.TabIndex = 52;
            this.progressBar1.ThemeName = "Desert";
            // 
            // txtFilterPayor
            // 
            this.txtFilterPayor.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtFilterPayor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFilterPayor.Location = new System.Drawing.Point(448, 11);
            this.txtFilterPayor.Name = "txtFilterPayor";
            // 
            // 
            // 
            this.txtFilterPayor.RootElement.ControlBounds = new System.Drawing.Rectangle(448, 11, 100, 20);
            this.txtFilterPayor.RootElement.StretchVertically = true;
            this.txtFilterPayor.Size = new System.Drawing.Size(229, 25);
            this.txtFilterPayor.TabIndex = 43;
            this.txtFilterPayor.TabStop = false;
            this.txtFilterPayor.ThemeName = "Desert";
            this.txtFilterPayor.TextChanged += new System.EventHandler(this.txtFilterPayor_TextChanged);
            // 
            // gvPatient
            // 
            this.gvPatient.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.gvPatient.Location = new System.Drawing.Point(12, 43);
            // 
            // gvPatient
            // 
            this.gvPatient.MasterTemplate.AllowAddNewRow = false;
            this.gvPatient.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gvPatient.Name = "gvPatient";
            this.gvPatient.Padding = new System.Windows.Forms.Padding(1);
            // 
            // 
            // 
            this.gvPatient.RootElement.ControlBounds = new System.Drawing.Rectangle(12, 43, 240, 150);
            this.gvPatient.RootElement.Padding = new System.Windows.Forms.Padding(1);
            this.gvPatient.ShowGroupPanel = false;
            this.gvPatient.Size = new System.Drawing.Size(667, 243);
            this.gvPatient.TabIndex = 42;
            this.gvPatient.Text = "radGridView1";
            this.gvPatient.ThemeName = "Desert";
            // 
            // btConvert
            // 
            this.btConvert.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btConvert.Enabled = false;
            this.btConvert.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btConvert.Location = new System.Drawing.Point(717, 417);
            this.btConvert.Name = "btConvert";
            // 
            // 
            // 
            this.btConvert.RootElement.ControlBounds = new System.Drawing.Rectangle(717, 417, 130, 24);
            this.btConvert.RootElement.Enabled = false;
            this.btConvert.Size = new System.Drawing.Size(227, 43);
            this.btConvert.TabIndex = 49;
            this.btConvert.Text = "Convert.";
            this.btConvert.ThemeName = "Desert";
            this.btConvert.Click += new System.EventHandler(this.btConvert_Click);
            // 
            // radGroupBox2
            // 
            this.radGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.radGroupBox2.Controls.Add(this.radLabel1);
            this.radGroupBox2.Controls.Add(this.chkBox);
            this.radGroupBox2.Controls.Add(this.txtFilterPayor);
            this.radGroupBox2.Controls.Add(this.gvPatient);
            this.radGroupBox2.FooterImageIndex = -1;
            this.radGroupBox2.FooterImageKey = "";
            this.radGroupBox2.HeaderImageIndex = -1;
            this.radGroupBox2.HeaderImageKey = "";
            this.radGroupBox2.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.radGroupBox2.HeaderText = "";
            this.radGroupBox2.Location = new System.Drawing.Point(11, 120);
            this.radGroupBox2.Name = "radGroupBox2";
            this.radGroupBox2.Padding = new System.Windows.Forms.Padding(2, 18, 2, 2);
            // 
            // 
            // 
            this.radGroupBox2.RootElement.ControlBounds = new System.Drawing.Rectangle(11, 120, 200, 100);
            this.radGroupBox2.RootElement.Padding = new System.Windows.Forms.Padding(2, 18, 2, 2);
            this.radGroupBox2.Size = new System.Drawing.Size(693, 291);
            this.radGroupBox2.TabIndex = 48;
            this.radGroupBox2.ThemeName = "Desert";
            // 
            // btFind
            // 
            this.btFind.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btFind.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btFind.Location = new System.Drawing.Point(706, 12);
            this.btFind.Name = "btFind";
            // 
            // 
            // 
            this.btFind.RootElement.ControlBounds = new System.Drawing.Rectangle(706, 12, 130, 24);
            this.btFind.Size = new System.Drawing.Size(120, 54);
            this.btFind.TabIndex = 43;
            this.btFind.Text = "Find";
            this.btFind.ThemeName = "Desert";
            this.btFind.Click += new System.EventHandler(this.btFind_Click);
            // 
            // dtpTimeTo
            // 
            this.dtpTimeTo.AllowDrop = true;
            this.dtpTimeTo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpTimeTo.CustomFormat = "HH:mm";
            this.dtpTimeTo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpTimeTo.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTimeTo.Location = new System.Drawing.Point(418, 41);
            this.dtpTimeTo.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTimeTo.MinDate = new System.DateTime(((long)(0)));
            this.dtpTimeTo.Name = "dtpTimeTo";
            this.dtpTimeTo.NullableValue = new System.DateTime(2014, 4, 30, 10, 1, 23, 230);
            this.dtpTimeTo.NullDate = new System.DateTime(((long)(0)));
            // 
            // 
            // 
            this.dtpTimeTo.RootElement.ControlBounds = new System.Drawing.Rectangle(418, 41, 150, 18);
            this.dtpTimeTo.RootElement.StretchVertically = true;
            this.dtpTimeTo.ShowUpDown = true;
            this.dtpTimeTo.Size = new System.Drawing.Size(275, 25);
            this.dtpTimeTo.TabIndex = 44;
            this.dtpTimeTo.TabStop = false;
            this.dtpTimeTo.Text = "radDateTimePicker1";
            this.dtpTimeTo.ThemeName = "Desert";
            this.dtpTimeTo.Value = new System.DateTime(2014, 4, 30, 10, 1, 23, 230);
            // 
            // dtpTimeFrom
            // 
            this.dtpTimeFrom.AllowDrop = true;
            this.dtpTimeFrom.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpTimeFrom.CustomFormat = "HH:mm";
            this.dtpTimeFrom.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpTimeFrom.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTimeFrom.Location = new System.Drawing.Point(82, 40);
            this.dtpTimeFrom.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTimeFrom.MinDate = new System.DateTime(((long)(0)));
            this.dtpTimeFrom.Name = "dtpTimeFrom";
            this.dtpTimeFrom.NullableValue = new System.DateTime(2014, 4, 30, 9, 59, 31, 951);
            this.dtpTimeFrom.NullDate = new System.DateTime(((long)(0)));
            // 
            // 
            // 
            this.dtpTimeFrom.RootElement.ControlBounds = new System.Drawing.Rectangle(82, 40, 150, 18);
            this.dtpTimeFrom.RootElement.StretchVertically = true;
            this.dtpTimeFrom.ShowUpDown = true;
            this.dtpTimeFrom.Size = new System.Drawing.Size(280, 25);
            this.dtpTimeFrom.TabIndex = 43;
            this.dtpTimeFrom.TabStop = false;
            this.dtpTimeFrom.Text = "radDateTimePicker1";
            this.dtpTimeFrom.ThemeName = "Desert";
            this.dtpTimeFrom.Value = new System.DateTime(2014, 4, 30, 9, 59, 31, 951);
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpDateTo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpDateTo.Location = new System.Drawing.Point(418, 12);
            this.dtpDateTo.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpDateTo.MinDate = new System.DateTime(((long)(0)));
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.NullableValue = new System.DateTime(2014, 4, 30, 9, 58, 39, 279);
            this.dtpDateTo.NullDate = new System.DateTime(((long)(0)));
            // 
            // 
            // 
            this.dtpDateTo.RootElement.ControlBounds = new System.Drawing.Rectangle(418, 12, 150, 18);
            this.dtpDateTo.RootElement.StretchVertically = true;
            this.dtpDateTo.Size = new System.Drawing.Size(275, 25);
            this.dtpDateTo.TabIndex = 42;
            this.dtpDateTo.TabStop = false;
            this.dtpDateTo.Text = "radDateTimePicker1";
            this.dtpDateTo.ThemeName = "Desert";
            this.dtpDateTo.Value = new System.DateTime(2014, 4, 30, 9, 58, 39, 279);
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpDateFrom.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpDateFrom.Location = new System.Drawing.Point(82, 12);
            this.dtpDateFrom.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpDateFrom.MinDate = new System.DateTime(((long)(0)));
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.NullableValue = new System.DateTime(2014, 4, 30, 9, 57, 31, 619);
            this.dtpDateFrom.NullDate = new System.DateTime(((long)(0)));
            // 
            // 
            // 
            this.dtpDateFrom.RootElement.ControlBounds = new System.Drawing.Rectangle(82, 12, 150, 18);
            this.dtpDateFrom.RootElement.StretchVertically = true;
            this.dtpDateFrom.Size = new System.Drawing.Size(280, 25);
            this.dtpDateFrom.TabIndex = 41;
            this.dtpDateFrom.TabStop = false;
            this.dtpDateFrom.Text = "radDateTimePicker1";
            this.dtpDateFrom.ThemeName = "Desert";
            this.dtpDateFrom.Value = new System.DateTime(2014, 4, 30, 9, 57, 31, 619);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(832, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 40;
            this.label1.Text = "ผลการค้นหา";
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.radGroupBox1.Controls.Add(this.rbRegister);
            this.radGroupBox1.Controls.Add(this.rbNotRegister);
            this.radGroupBox1.Controls.Add(this.rbAll);
            this.radGroupBox1.Controls.Add(this.btFind);
            this.radGroupBox1.Controls.Add(this.dtpTimeTo);
            this.radGroupBox1.Controls.Add(this.dtpTimeFrom);
            this.radGroupBox1.Controls.Add(this.dtpDateTo);
            this.radGroupBox1.Controls.Add(this.dtpDateFrom);
            this.radGroupBox1.Controls.Add(this.label1);
            this.radGroupBox1.Controls.Add(this.lblCountPT);
            this.radGroupBox1.Controls.Add(this.label10);
            this.radGroupBox1.Controls.Add(this.label2);
            this.radGroupBox1.Controls.Add(this.lblDateFrom);
            this.radGroupBox1.FooterImageIndex = -1;
            this.radGroupBox1.FooterImageKey = "";
            this.radGroupBox1.HeaderImageIndex = -1;
            this.radGroupBox1.HeaderImageKey = "";
            this.radGroupBox1.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.radGroupBox1.HeaderText = "";
            this.radGroupBox1.Location = new System.Drawing.Point(11, 10);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Padding = new System.Windows.Forms.Padding(2, 18, 2, 2);
            // 
            // 
            // 
            this.radGroupBox1.RootElement.ControlBounds = new System.Drawing.Rectangle(11, 10, 200, 100);
            this.radGroupBox1.RootElement.Padding = new System.Windows.Forms.Padding(2, 18, 2, 2);
            this.radGroupBox1.Size = new System.Drawing.Size(934, 96);
            this.radGroupBox1.TabIndex = 47;
            this.radGroupBox1.ThemeName = "Desert";
            // 
            // rbRegister
            // 
            this.rbRegister.AutoSize = true;
            this.rbRegister.Checked = true;
            this.rbRegister.Location = new System.Drawing.Point(252, 71);
            this.rbRegister.Name = "rbRegister";
            this.rbRegister.Size = new System.Drawing.Size(95, 17);
            this.rbRegister.TabIndex = 45;
            this.rbRegister.TabStop = true;
            this.rbRegister.Text = "ลงทะเบียนแล้ว";
            this.rbRegister.UseVisualStyleBackColor = true;
            // 
            // rbNotRegister
            // 
            this.rbNotRegister.AutoSize = true;
            this.rbNotRegister.Location = new System.Drawing.Point(146, 72);
            this.rbNotRegister.Name = "rbNotRegister";
            this.rbNotRegister.Size = new System.Drawing.Size(100, 17);
            this.rbNotRegister.TabIndex = 45;
            this.rbNotRegister.Text = "ยังไม่ลงทะเบียน";
            this.rbNotRegister.UseVisualStyleBackColor = true;
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Location = new System.Drawing.Point(82, 72);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(58, 17);
            this.rbAll.TabIndex = 45;
            this.rbAll.Text = "ทั้งหมด";
            this.rbAll.UseVisualStyleBackColor = true;
            // 
            // lblCountPT
            // 
            this.lblCountPT.AutoSize = true;
            this.lblCountPT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblCountPT.Location = new System.Drawing.Point(832, 46);
            this.lblCountPT.Name = "lblCountPT";
            this.lblCountPT.Size = new System.Drawing.Size(59, 13);
            this.lblCountPT.TabIndex = 39;
            this.lblCountPT.Text = "0 Record";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(376, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 21);
            this.label10.TabIndex = 34;
            this.label10.Text = "To :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(15, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 21);
            this.label2.TabIndex = 33;
            this.label2.Text = "Status :";
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.AutoSize = true;
            this.lblDateFrom.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDateFrom.Location = new System.Drawing.Point(22, 24);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(57, 21);
            this.lblDateFrom.TabIndex = 33;
            this.lblDateFrom.Text = "From :";
            // 
            // radGroupBox3
            // 
            this.radGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.radGroupBox3.Controls.Add(this.lblPercentTag);
            this.radGroupBox3.Controls.Add(this.radLabel5);
            this.radGroupBox3.Controls.Add(this.radLabel4);
            this.radGroupBox3.Controls.Add(this.radLabel3);
            this.radGroupBox3.Controls.Add(this.radLabel2);
            this.radGroupBox3.Controls.Add(this.cboPolicy);
            this.radGroupBox3.Controls.Add(this.cboPayorOffice);
            this.radGroupBox3.Controls.Add(this.cboAgreement);
            this.radGroupBox3.Controls.Add(this.cboPayor);
            this.radGroupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.radGroupBox3.FooterImageIndex = -1;
            this.radGroupBox3.FooterImageKey = "";
            this.radGroupBox3.HeaderImageIndex = -1;
            this.radGroupBox3.HeaderImageKey = "";
            this.radGroupBox3.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.radGroupBox3.HeaderText = "Payor Detail";
            this.radGroupBox3.Location = new System.Drawing.Point(717, 112);
            this.radGroupBox3.Name = "radGroupBox3";
            this.radGroupBox3.Padding = new System.Windows.Forms.Padding(2, 18, 2, 2);
            // 
            // 
            // 
            this.radGroupBox3.RootElement.ControlBounds = new System.Drawing.Rectangle(717, 112, 200, 100);
            this.radGroupBox3.RootElement.Padding = new System.Windows.Forms.Padding(2, 18, 2, 2);
            this.radGroupBox3.Size = new System.Drawing.Size(228, 299);
            this.radGroupBox3.TabIndex = 53;
            this.radGroupBox3.Text = "Payor Detail";
            this.radGroupBox3.ThemeName = "Desert";
            // 
            // lblPercentTag
            // 
            this.lblPercentTag.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblPercentTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.lblPercentTag.Location = new System.Drawing.Point(103, 96);
            this.lblPercentTag.Name = "lblPercentTag";
            this.lblPercentTag.Size = new System.Drawing.Size(12, 15);
            this.lblPercentTag.TabIndex = 52;
            this.lblPercentTag.Text = "0";
            // 
            // radLabel5
            // 
            this.radLabel5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.radLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.radLabel5.Location = new System.Drawing.Point(15, 237);
            this.radLabel5.Name = "radLabel5";
            this.radLabel5.Size = new System.Drawing.Size(51, 19);
            this.radLabel5.TabIndex = 57;
            this.radLabel5.Text = "Policy :";
            // 
            // radLabel4
            // 
            this.radLabel4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.radLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.radLabel4.Location = new System.Drawing.Point(15, 163);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(90, 19);
            this.radLabel4.TabIndex = 56;
            this.radLabel4.Text = "Payor Office :";
            // 
            // radLabel3
            // 
            this.radLabel3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.radLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.radLabel3.Location = new System.Drawing.Point(15, 92);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(82, 19);
            this.radLabel3.TabIndex = 55;
            this.radLabel3.Text = "Agreement :";
            // 
            // radLabel2
            // 
            this.radLabel2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.radLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.radLabel2.Location = new System.Drawing.Point(15, 26);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(50, 19);
            this.radLabel2.TabIndex = 54;
            this.radLabel2.Text = "Payor :";
            // 
            // cboPolicy
            // 
            this.cboPolicy.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboPolicy.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cboPolicy.DropDownAnimationEnabled = true;
            this.cboPolicy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cboPolicy.Location = new System.Drawing.Point(15, 262);
            this.cboPolicy.Name = "cboPolicy";
            // 
            // 
            // 
            this.cboPolicy.RootElement.ControlBounds = new System.Drawing.Rectangle(15, 262, 106, 20);
            this.cboPolicy.RootElement.StretchVertically = true;
            this.cboPolicy.ShowImageInEditorArea = true;
            this.cboPolicy.Size = new System.Drawing.Size(198, 25);
            this.cboPolicy.TabIndex = 3;
            this.cboPolicy.ThemeName = "Desert";
            // 
            // cboPayorOffice
            // 
            this.cboPayorOffice.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboPayorOffice.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cboPayorOffice.DropDownAnimationEnabled = true;
            this.cboPayorOffice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cboPayorOffice.Location = new System.Drawing.Point(15, 188);
            this.cboPayorOffice.Name = "cboPayorOffice";
            // 
            // 
            // 
            this.cboPayorOffice.RootElement.ControlBounds = new System.Drawing.Rectangle(15, 188, 106, 20);
            this.cboPayorOffice.RootElement.StretchVertically = true;
            this.cboPayorOffice.ShowImageInEditorArea = true;
            this.cboPayorOffice.Size = new System.Drawing.Size(198, 25);
            this.cboPayorOffice.TabIndex = 2;
            this.cboPayorOffice.ThemeName = "Desert";
            // 
            // cboAgreement
            // 
            this.cboAgreement.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboAgreement.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cboAgreement.DropDownAnimationEnabled = true;
            this.cboAgreement.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cboAgreement.Location = new System.Drawing.Point(15, 117);
            this.cboAgreement.Name = "cboAgreement";
            // 
            // 
            // 
            this.cboAgreement.RootElement.ControlBounds = new System.Drawing.Rectangle(15, 117, 106, 20);
            this.cboAgreement.RootElement.StretchVertically = true;
            this.cboAgreement.ShowImageInEditorArea = true;
            this.cboAgreement.Size = new System.Drawing.Size(198, 25);
            this.cboAgreement.TabIndex = 1;
            this.cboAgreement.ThemeName = "Desert";
            this.cboAgreement.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.cboAgreement_SelectedIndexChanged);
            // 
            // cboPayor
            // 
            this.cboPayor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboPayor.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cboPayor.DropDownAnimationEnabled = true;
            this.cboPayor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cboPayor.Location = new System.Drawing.Point(15, 51);
            this.cboPayor.Name = "cboPayor";
            // 
            // 
            // 
            this.cboPayor.RootElement.ControlBounds = new System.Drawing.Rectangle(15, 51, 106, 20);
            this.cboPayor.RootElement.StretchVertically = true;
            this.cboPayor.ShowImageInEditorArea = true;
            this.cboPayor.Size = new System.Drawing.Size(198, 25);
            this.cboPayor.SortStyle = Telerik.WinControls.Enumerations.SortStyle.Ascending;
            this.cboPayor.TabIndex = 0;
            this.cboPayor.ThemeName = "Desert";
            this.cboPayor.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.cboPayor_SelectedIndexChanged_1);
            this.cboPayor.Click += new System.EventHandler(this.cboPayor_Click);
            // 
            // frmConvertPayor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 470);
            this.Controls.Add(this.radGroupBox3);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btConvert);
            this.Controls.Add(this.radGroupBox2);
            this.Controls.Add(this.radGroupBox1);
            this.Name = "frmConvertPayor";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Convert Order By Payor";
            this.ThemeName = "Desert";
            this.Load += new System.EventHandler(this.frmConvertPayor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilterPayor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPatient.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPatient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btConvert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).EndInit();
            this.radGroupBox2.ResumeLayout(false);
            this.radGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btFind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTimeTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTimeFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDateTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDateFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            this.radGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox3)).EndInit();
            this.radGroupBox3.ResumeLayout(false);
            this.radGroupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblPercentTag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPolicy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPayorOffice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboAgreement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPayor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.Themes.DesertTheme desertTheme1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadLabel lblStatus;
        private Telerik.WinControls.UI.RadCheckBox chkBox;
        private Telerik.WinControls.UI.RadProgressBar progressBar1;
        private Telerik.WinControls.UI.RadTextBox txtFilterPayor;
        private Telerik.WinControls.UI.RadGridView gvPatient;
        private Telerik.WinControls.UI.RadButton btConvert;
        private Telerik.WinControls.Themes.DesertTheme desertTheme2;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Telerik.WinControls.UI.RadButton btFind;
        private Telerik.WinControls.UI.RadDateTimePicker dtpTimeTo;
        private Telerik.WinControls.UI.RadDateTimePicker dtpTimeFrom;
        private Telerik.WinControls.UI.RadDateTimePicker dtpDateTo;
        private Telerik.WinControls.UI.RadDateTimePicker dtpDateFrom;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private System.Windows.Forms.Label lblCountPT;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblDateFrom;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox3;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadDropDownList cboPolicy;
        private Telerik.WinControls.UI.RadDropDownList cboPayorOffice;
        private Telerik.WinControls.UI.RadDropDownList cboAgreement;
        private Telerik.WinControls.UI.RadDropDownList cboPayor;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadLabel radLabel5;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private Telerik.WinControls.UI.RadLabel lblPercentTag;
        private System.Windows.Forms.RadioButton rbRegister;
        private System.Windows.Forms.RadioButton rbNotRegister;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.Label label2;
    }
}
