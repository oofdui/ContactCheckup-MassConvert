namespace MassConvert
{
    partial class Form1
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.labPolicy = new System.Windows.Forms.Label();
            this.cboPolicy = new System.Windows.Forms.ComboBox();
            this.labPayorOffice = new System.Windows.Forms.Label();
            this.cboPayorOffice = new System.Windows.Forms.ComboBox();
            this.lblAgreement = new System.Windows.Forms.Label();
            this.cboAgreement = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboPayor = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbLocationOrder = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label8 = new System.Windows.Forms.Label();
            this.txtFileLoad = new System.Windows.Forms.TextBox();
            this.btLoad = new System.Windows.Forms.Button();
            this.btBrows = new System.Windows.Forms.Button();
            this.gvPatient = new System.Windows.Forms.DataGridView();
            this.lblCountPT = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtFilterPayor = new System.Windows.Forms.TextBox();
            this.chkBox = new System.Windows.Forms.CheckBox();
            this.btFind = new System.Windows.Forms.Button();
            this.dtpTimeTo = new System.Windows.Forms.DateTimePicker();
            this.dtpTimeFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblGenLab = new System.Windows.Forms.RadioButton();
            this.lblGenLabNo = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPatient)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 475);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(560, 23);
            this.progressBar1.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(305, 501);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "to";
            this.label7.Visible = false;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(300, 517);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(64, 20);
            this.textBox3.TabIndex = 23;
            this.textBox3.Text = "30";
            this.textBox3.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(151, 501);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Careprovider";
            this.label6.Visible = false;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(145, 520);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(67, 21);
            this.comboBox2.TabIndex = 21;
            this.comboBox2.Visible = false;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(437, 517);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 20;
            this.button4.Text = "InsertTest";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // labPolicy
            // 
            this.labPolicy.AutoSize = true;
            this.labPolicy.Location = new System.Drawing.Point(13, 109);
            this.labPolicy.Name = "labPolicy";
            this.labPolicy.Size = new System.Drawing.Size(35, 13);
            this.labPolicy.TabIndex = 19;
            this.labPolicy.Text = "Policy";
            // 
            // cboPolicy
            // 
            this.cboPolicy.FormattingEnabled = true;
            this.cboPolicy.Location = new System.Drawing.Point(103, 109);
            this.cboPolicy.Name = "cboPolicy";
            this.cboPolicy.Size = new System.Drawing.Size(197, 21);
            this.cboPolicy.TabIndex = 18;
            // 
            // labPayorOffice
            // 
            this.labPayorOffice.AutoSize = true;
            this.labPayorOffice.Location = new System.Drawing.Point(12, 82);
            this.labPayorOffice.Name = "labPayorOffice";
            this.labPayorOffice.Size = new System.Drawing.Size(62, 13);
            this.labPayorOffice.TabIndex = 17;
            this.labPayorOffice.Text = "PayorOffice";
            // 
            // cboPayorOffice
            // 
            this.cboPayorOffice.FormattingEnabled = true;
            this.cboPayorOffice.Location = new System.Drawing.Point(103, 82);
            this.cboPayorOffice.Name = "cboPayorOffice";
            this.cboPayorOffice.Size = new System.Drawing.Size(197, 21);
            this.cboPayorOffice.TabIndex = 16;
            // 
            // lblAgreement
            // 
            this.lblAgreement.AutoSize = true;
            this.lblAgreement.Location = new System.Drawing.Point(12, 55);
            this.lblAgreement.Name = "lblAgreement";
            this.lblAgreement.Size = new System.Drawing.Size(58, 13);
            this.lblAgreement.TabIndex = 15;
            this.lblAgreement.Text = "Agreement";
            // 
            // cboAgreement
            // 
            this.cboAgreement.FormattingEnabled = true;
            this.cboAgreement.Location = new System.Drawing.Point(103, 55);
            this.cboAgreement.Name = "cboAgreement";
            this.cboAgreement.Size = new System.Drawing.Size(197, 21);
            this.cboAgreement.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Payor";
            // 
            // cboPayor
            // 
            this.cboPayor.FormattingEnabled = true;
            this.cboPayor.Location = new System.Drawing.Point(103, 28);
            this.cboPayor.Name = "cboPayor";
            this.cboPayor.Size = new System.Drawing.Size(197, 21);
            this.cboPayor.TabIndex = 12;
            this.cboPayor.DropDown += new System.EventHandler(this.cboPayor_DropDown);
            this.cboPayor.SelectedIndexChanged += new System.EventHandler(this.cboPayor_SelectedIndexChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(578, 148);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(116, 38);
            this.button3.TabIndex = 11;
            this.button3.Text = "Print Sticker";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(578, 99);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(116, 43);
            this.button2.TabIndex = 10;
            this.button2.Text = "Gen LabNo.";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(227, 501);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Schedule No.";
            this.label4.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(230, 517);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(64, 20);
            this.textBox2.TabIndex = 8;
            this.textBox2.Text = "30";
            this.textBox2.Visible = false;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(587, 377);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 66);
            this.button1.TabIndex = 7;
            this.button1.Text = "Convert";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 521);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(55, 20);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = "502154";
            this.textBox1.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 501);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "LoginName";
            this.label3.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 501);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "OrderfromLoc";
            this.label2.Visible = false;
            // 
            // cbLocationOrder
            // 
            this.cbLocationOrder.FormattingEnabled = true;
            this.cbLocationOrder.Location = new System.Drawing.Point(73, 521);
            this.cbLocationOrder.Name = "cbLocationOrder";
            this.cbLocationOrder.Size = new System.Drawing.Size(55, 21);
            this.cbLocationOrder.TabIndex = 2;
            this.cbLocationOrder.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(375, 501);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Encounter";
            this.label1.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(375, 520);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(56, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.Visible = false;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(325, 16);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.Size = new System.Drawing.Size(219, 52);
            this.dgv.TabIndex = 1;
            this.dgv.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(325, 81);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(219, 49);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.Visible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(209, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Choose file :";
            this.label8.Visible = false;
            // 
            // txtFileLoad
            // 
            this.txtFileLoad.Location = new System.Drawing.Point(280, 19);
            this.txtFileLoad.Name = "txtFileLoad";
            this.txtFileLoad.Size = new System.Drawing.Size(217, 20);
            this.txtFileLoad.TabIndex = 1;
            this.txtFileLoad.Visible = false;
            // 
            // btLoad
            // 
            this.btLoad.Location = new System.Drawing.Point(578, 12);
            this.btLoad.Name = "btLoad";
            this.btLoad.Size = new System.Drawing.Size(81, 27);
            this.btLoad.TabIndex = 2;
            this.btLoad.Text = "Import";
            this.btLoad.UseVisualStyleBackColor = true;
            this.btLoad.Visible = false;
            this.btLoad.Click += new System.EventHandler(this.btLoad_Click);
            // 
            // btBrows
            // 
            this.btBrows.Location = new System.Drawing.Point(578, 45);
            this.btBrows.Name = "btBrows";
            this.btBrows.Size = new System.Drawing.Size(81, 27);
            this.btBrows.TabIndex = 3;
            this.btBrows.Text = "Brows...";
            this.btBrows.UseVisualStyleBackColor = true;
            this.btBrows.Visible = false;
            this.btBrows.Click += new System.EventHandler(this.btBrows_Click);
            // 
            // gvPatient
            // 
            this.gvPatient.AllowDrop = true;
            this.gvPatient.AllowUserToAddRows = false;
            this.gvPatient.AllowUserToDeleteRows = false;
            this.gvPatient.AllowUserToOrderColumns = true;
            this.gvPatient.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvPatient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPatient.Location = new System.Drawing.Point(15, 91);
            this.gvPatient.Name = "gvPatient";
            this.gvPatient.Size = new System.Drawing.Size(529, 161);
            this.gvPatient.TabIndex = 2;
            // 
            // lblCountPT
            // 
            this.lblCountPT.AutoSize = true;
            this.lblCountPT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblCountPT.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblCountPT.Location = new System.Drawing.Point(503, 20);
            this.lblCountPT.Name = "lblCountPT";
            this.lblCountPT.Size = new System.Drawing.Size(17, 17);
            this.lblCountPT.TabIndex = 4;
            this.lblCountPT.Text = "0";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtFilterPayor);
            this.groupBox1.Controls.Add(this.chkBox);
            this.groupBox1.Controls.Add(this.btFind);
            this.groupBox1.Controls.Add(this.dtpTimeTo);
            this.groupBox1.Controls.Add(this.dtpTimeFrom);
            this.groupBox1.Controls.Add(this.dtpDateTo);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.lblDateFrom);
            this.groupBox1.Controls.Add(this.dtpDateFrom);
            this.groupBox1.Controls.Add(this.gvPatient);
            this.groupBox1.Location = new System.Drawing.Point(12, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(560, 258);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(281, 72);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 13);
            this.label11.TabIndex = 31;
            this.label11.Text = "Filter by Company :";
            // 
            // txtFilterPayor
            // 
            this.txtFilterPayor.Location = new System.Drawing.Point(383, 68);
            this.txtFilterPayor.Name = "txtFilterPayor";
            this.txtFilterPayor.Size = new System.Drawing.Size(100, 20);
            this.txtFilterPayor.TabIndex = 30;
            this.txtFilterPayor.TextChanged += new System.EventHandler(this.txtFilterPayor_TextChanged);
            // 
            // chkBox
            // 
            this.chkBox.AutoSize = true;
            this.chkBox.Location = new System.Drawing.Point(82, 68);
            this.chkBox.Name = "chkBox";
            this.chkBox.Size = new System.Drawing.Size(70, 17);
            this.chkBox.TabIndex = 29;
            this.chkBox.Text = "Select All";
            this.chkBox.UseVisualStyleBackColor = true;
            this.chkBox.CheckedChanged += new System.EventHandler(this.chkBox_CheckedChanged);
            // 
            // btFind
            // 
            this.btFind.Location = new System.Drawing.Point(491, 14);
            this.btFind.Name = "btFind";
            this.btFind.Size = new System.Drawing.Size(53, 48);
            this.btFind.TabIndex = 28;
            this.btFind.Text = "Find";
            this.btFind.UseVisualStyleBackColor = true;
            this.btFind.Click += new System.EventHandler(this.btFind_Click);
            // 
            // dtpTimeTo
            // 
            this.dtpTimeTo.AllowDrop = true;
            this.dtpTimeTo.CustomFormat = "HH:mm";
            this.dtpTimeTo.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTimeTo.Location = new System.Drawing.Point(315, 42);
            this.dtpTimeTo.Name = "dtpTimeTo";
            this.dtpTimeTo.ShowUpDown = true;
            this.dtpTimeTo.Size = new System.Drawing.Size(170, 20);
            this.dtpTimeTo.TabIndex = 10;
            // 
            // dtpTimeFrom
            // 
            this.dtpTimeFrom.AllowDrop = true;
            this.dtpTimeFrom.CustomFormat = "HH:mm";
            this.dtpTimeFrom.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTimeFrom.Location = new System.Drawing.Point(81, 42);
            this.dtpTimeFrom.Name = "dtpTimeFrom";
            this.dtpTimeFrom.ShowUpDown = true;
            this.dtpTimeFrom.Size = new System.Drawing.Size(170, 20);
            this.dtpTimeFrom.TabIndex = 9;
            this.dtpTimeFrom.Value = new System.DateTime(2014, 2, 10, 9, 21, 28, 0);
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.Location = new System.Drawing.Point(315, 14);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(170, 20);
            this.dtpDateTo.TabIndex = 8;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(257, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 13);
            this.label10.TabIndex = 7;
            this.label10.Text = "Date To :";
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.AutoSize = true;
            this.lblDateFrom.Location = new System.Drawing.Point(13, 20);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(62, 13);
            this.lblDateFrom.TabIndex = 6;
            this.lblDateFrom.Text = "Date From :";
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.Location = new System.Drawing.Point(81, 16);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(170, 20);
            this.dtpDateFrom.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.SteelBlue;
            this.label9.Location = new System.Drawing.Point(578, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "(ใช้ไฟล์ Bulk Regis)";
            this.label9.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cboAgreement);
            this.groupBox2.Controls.Add(this.lblAgreement);
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Controls.Add(this.cboPayor);
            this.groupBox2.Controls.Add(this.cboPayorOffice);
            this.groupBox2.Controls.Add(this.dgv);
            this.groupBox2.Controls.Add(this.labPayorOffice);
            this.groupBox2.Controls.Add(this.cboPolicy);
            this.groupBox2.Controls.Add(this.labPolicy);
            this.groupBox2.Location = new System.Drawing.Point(12, 321);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(560, 148);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Payor Detail";
            this.groupBox2.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblGenLab);
            this.groupBox3.Controls.Add(this.txtFileLoad);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.lblGenLabNo);
            this.groupBox3.Controls.Add(this.lblCountPT);
            this.groupBox3.Location = new System.Drawing.Point(15, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(557, 45);
            this.groupBox3.TabIndex = 27;
            this.groupBox3.TabStop = false;
            // 
            // lblGenLab
            // 
            this.lblGenLab.AutoSize = true;
            this.lblGenLab.Location = new System.Drawing.Point(139, 20);
            this.lblGenLab.Name = "lblGenLab";
            this.lblGenLab.Size = new System.Drawing.Size(62, 17);
            this.lblGenLab.TabIndex = 1;
            this.lblGenLab.Text = "Convert";
            this.lblGenLab.UseVisualStyleBackColor = true;
            this.lblGenLab.Click += new System.EventHandler(this.lblGenLab_Click);
            // 
            // lblGenLabNo
            // 
            this.lblGenLabNo.AutoSize = true;
            this.lblGenLabNo.Checked = true;
            this.lblGenLabNo.Location = new System.Drawing.Point(13, 20);
            this.lblGenLabNo.Name = "lblGenLabNo";
            this.lblGenLabNo.Size = new System.Drawing.Size(80, 17);
            this.lblGenLabNo.TabIndex = 0;
            this.lblGenLabNo.TabStop = true;
            this.lblGenLabNo.Text = "Gen LabNo";
            this.lblGenLabNo.UseVisualStyleBackColor = true;
            this.lblGenLabNo.Click += new System.EventHandler(this.lblGenLabNo_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(705, 556);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btLoad);
            this.Controls.Add(this.btBrows);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbLocationOrder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Name = "Form1";
            this.Text = "Update : 04/04/2014 18:40";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPatient)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbLocationOrder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label lblAgreement;
        private System.Windows.Forms.ComboBox cboAgreement;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboPayor;
        private System.Windows.Forms.Label labPolicy;
        private System.Windows.Forms.ComboBox cboPolicy;
        private System.Windows.Forms.Label labPayorOffice;
        private System.Windows.Forms.ComboBox cboPayorOffice;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtFileLoad;
        private System.Windows.Forms.Button btLoad;
        private System.Windows.Forms.Button btBrows;
        private System.Windows.Forms.DataGridView gvPatient;
        private System.Windows.Forms.Label lblCountPT;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton lblGenLab;
        private System.Windows.Forms.RadioButton lblGenLabNo;
        private System.Windows.Forms.Button btFind;
        private System.Windows.Forms.DateTimePicker dtpTimeTo;
        private System.Windows.Forms.DateTimePicker dtpTimeFrom;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.CheckBox chkBox;
        private System.Windows.Forms.TextBox txtFilterPayor;
        private System.Windows.Forms.Label label11;
    }
}

