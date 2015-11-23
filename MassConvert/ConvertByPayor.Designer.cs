namespace MassConvert
{
    partial class ConvertByPayor
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
            this.tbDefault = new System.Windows.Forms.TableLayoutPanel();
            this.tbSearch = new System.Windows.Forms.TableLayoutPanel();
            this.tbSearchMain = new System.Windows.Forms.TableLayoutPanel();
            this.tbSearchButton = new System.Windows.Forms.TableLayoutPanel();
            this.tbSearchResult = new System.Windows.Forms.TableLayoutPanel();
            this.lblSearchResult = new System.Windows.Forms.Label();
            this.btSearch = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSearchStatus = new System.Windows.Forms.TableLayoutPanel();
            this.rbRegister = new System.Windows.Forms.RadioButton();
            this.rbNotRegister = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dtDOETo = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtDOEFrom = new System.Windows.Forms.DateTimePicker();
            this.cbPayor = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbPatient = new System.Windows.Forms.TableLayoutPanel();
            this.tbPayor = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbButton = new System.Windows.Forms.TableLayoutPanel();
            this.btConvert = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cboPayor = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.tbPatientSelect = new System.Windows.Forms.TableLayoutPanel();
            this.tbFilter = new System.Windows.Forms.TableLayoutPanel();
            this.label11 = new System.Windows.Forms.Label();
            this.cbCheckAll = new System.Windows.Forms.CheckBox();
            this.ddlIsConverted = new System.Windows.Forms.ComboBox();
            this.gvPatient = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label12 = new System.Windows.Forms.Label();
            this.lblProcessDetail = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pbProcess = new System.Windows.Forms.ProgressBar();
            this.pbSearch = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblIsConvertCount = new System.Windows.Forms.Label();
            this.Choose = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tbDefault.SuspendLayout();
            this.tbSearch.SuspendLayout();
            this.tbSearchMain.SuspendLayout();
            this.tbSearchButton.SuspendLayout();
            this.tbSearchResult.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tbSearchStatus.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tbPatient.SuspendLayout();
            this.tbPayor.SuspendLayout();
            this.tbButton.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tbPatientSelect.SuspendLayout();
            this.tbFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPatient)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // tbDefault
            // 
            this.tbDefault.AutoSize = true;
            this.tbDefault.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tbDefault.ColumnCount = 1;
            this.tbDefault.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbDefault.Controls.Add(this.tbSearch, 0, 0);
            this.tbDefault.Controls.Add(this.tbPatient, 0, 1);
            this.tbDefault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDefault.Location = new System.Drawing.Point(0, 0);
            this.tbDefault.Margin = new System.Windows.Forms.Padding(0);
            this.tbDefault.Name = "tbDefault";
            this.tbDefault.RowCount = 2;
            this.tbDefault.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tbDefault.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbDefault.Size = new System.Drawing.Size(887, 469);
            this.tbDefault.TabIndex = 0;
            // 
            // tbSearch
            // 
            this.tbSearch.AutoSize = true;
            this.tbSearch.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbSearch.ColumnCount = 1;
            this.tbSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbSearch.Controls.Add(this.tbSearchMain, 0, 1);
            this.tbSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSearch.Location = new System.Drawing.Point(1, 1);
            this.tbSearch.Margin = new System.Windows.Forms.Padding(0);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.RowCount = 2;
            this.tbSearch.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbSearch.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbSearch.Size = new System.Drawing.Size(885, 100);
            this.tbSearch.TabIndex = 0;
            // 
            // tbSearchMain
            // 
            this.tbSearchMain.AutoSize = true;
            this.tbSearchMain.ColumnCount = 2;
            this.tbSearchMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbSearchMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbSearchMain.Controls.Add(this.tbSearchButton, 1, 0);
            this.tbSearchMain.Controls.Add(this.pbSearch, 0, 0);
            this.tbSearchMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSearchMain.Location = new System.Drawing.Point(0, 0);
            this.tbSearchMain.Margin = new System.Windows.Forms.Padding(0);
            this.tbSearchMain.Name = "tbSearchMain";
            this.tbSearchMain.RowCount = 1;
            this.tbSearchMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbSearchMain.Size = new System.Drawing.Size(885, 100);
            this.tbSearchMain.TabIndex = 0;
            // 
            // tbSearchButton
            // 
            this.tbSearchButton.AutoSize = true;
            this.tbSearchButton.ColumnCount = 2;
            this.tbSearchButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbSearchButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbSearchButton.Controls.Add(this.tbSearchResult, 1, 1);
            this.tbSearchButton.Controls.Add(this.tableLayoutPanel1, 1, 0);
            this.tbSearchButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSearchButton.Location = new System.Drawing.Point(98, 0);
            this.tbSearchButton.Margin = new System.Windows.Forms.Padding(0);
            this.tbSearchButton.Name = "tbSearchButton";
            this.tbSearchButton.Padding = new System.Windows.Forms.Padding(5);
            this.tbSearchButton.RowCount = 2;
            this.tbSearchButton.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbSearchButton.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbSearchButton.Size = new System.Drawing.Size(787, 100);
            this.tbSearchButton.TabIndex = 1;
            // 
            // tbSearchResult
            // 
            this.tbSearchResult.AutoSize = true;
            this.tbSearchResult.ColumnCount = 2;
            this.tbSearchResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbSearchResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbSearchResult.Controls.Add(this.lblSearchResult, 1, 0);
            this.tbSearchResult.Controls.Add(this.btSearch, 0, 0);
            this.tbSearchResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSearchResult.Location = new System.Drawing.Point(5, 52);
            this.tbSearchResult.Margin = new System.Windows.Forms.Padding(0);
            this.tbSearchResult.Name = "tbSearchResult";
            this.tbSearchResult.RowCount = 1;
            this.tbSearchResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbSearchResult.Size = new System.Drawing.Size(777, 43);
            this.tbSearchResult.TabIndex = 2;
            // 
            // lblSearchResult
            // 
            this.lblSearchResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSearchResult.AutoSize = true;
            this.lblSearchResult.Location = new System.Drawing.Point(109, 0);
            this.lblSearchResult.Name = "lblSearchResult";
            this.lblSearchResult.Padding = new System.Windows.Forms.Padding(5);
            this.lblSearchResult.Size = new System.Drawing.Size(20, 43);
            this.lblSearchResult.TabIndex = 1;
            this.lblSearchResult.Text = "-";
            this.lblSearchResult.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btSearch
            // 
            this.btSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btSearch.Location = new System.Drawing.Point(3, 6);
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(100, 30);
            this.btSearch.TabIndex = 0;
            this.btSearch.Text = "Search";
            this.btSearch.UseVisualStyleBackColor = true;
            this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbSearchStatus, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(777, 47);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(19, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "DOE From";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(3, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "MobileStatus";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbSearchStatus
            // 
            this.tbSearchStatus.ColumnCount = 3;
            this.tbSearchStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbSearchStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbSearchStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbSearchStatus.Controls.Add(this.rbRegister, 0, 0);
            this.tbSearchStatus.Controls.Add(this.rbNotRegister, 0, 0);
            this.tbSearchStatus.Controls.Add(this.rbAll, 0, 0);
            this.tbSearchStatus.Location = new System.Drawing.Point(86, 27);
            this.tbSearchStatus.Margin = new System.Windows.Forms.Padding(0);
            this.tbSearchStatus.Name = "tbSearchStatus";
            this.tbSearchStatus.RowCount = 1;
            this.tbSearchStatus.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbSearchStatus.Size = new System.Drawing.Size(286, 20);
            this.tbSearchStatus.TabIndex = 2;
            // 
            // rbRegister
            // 
            this.rbRegister.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbRegister.AutoSize = true;
            this.rbRegister.Checked = true;
            this.rbRegister.Location = new System.Drawing.Point(173, 3);
            this.rbRegister.Name = "rbRegister";
            this.rbRegister.Size = new System.Drawing.Size(95, 17);
            this.rbRegister.TabIndex = 48;
            this.rbRegister.TabStop = true;
            this.rbRegister.Text = "ลงทะเบียนแล้ว";
            this.rbRegister.UseVisualStyleBackColor = true;
            // 
            // rbNotRegister
            // 
            this.rbNotRegister.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbNotRegister.AutoSize = true;
            this.rbNotRegister.Location = new System.Drawing.Point(67, 3);
            this.rbNotRegister.Name = "rbNotRegister";
            this.rbNotRegister.Size = new System.Drawing.Size(100, 17);
            this.rbNotRegister.TabIndex = 47;
            this.rbNotRegister.Text = "ยังไม่ลงทะเบียน";
            this.rbNotRegister.UseVisualStyleBackColor = true;
            // 
            // rbAll
            // 
            this.rbAll.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbAll.AutoSize = true;
            this.rbAll.Location = new System.Drawing.Point(3, 3);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(58, 17);
            this.rbAll.TabIndex = 46;
            this.rbAll.Text = "ทั้งหมด";
            this.rbAll.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.dtDOETo, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.dtDOEFrom, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbPayor, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.label9, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(86, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(691, 27);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // dtDOETo
            // 
            this.dtDOETo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtDOETo.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtDOETo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDOETo.Location = new System.Drawing.Point(202, 3);
            this.dtDOETo.Name = "dtDOETo";
            this.dtDOETo.Size = new System.Drawing.Size(135, 20);
            this.dtDOETo.TabIndex = 1;
            this.dtDOETo.ValueChanged += new System.EventHandler(this.dtDOETo_ValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(144, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 27);
            this.label2.TabIndex = 0;
            this.label2.Text = "DOE To";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtDOEFrom
            // 
            this.dtDOEFrom.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtDOEFrom.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtDOEFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDOEFrom.Location = new System.Drawing.Point(3, 3);
            this.dtDOEFrom.Name = "dtDOEFrom";
            this.dtDOEFrom.Size = new System.Drawing.Size(135, 20);
            this.dtDOEFrom.TabIndex = 1;
            this.dtDOEFrom.ValueChanged += new System.EventHandler(this.dtDOEFrom_ValueChanged);
            // 
            // cbPayor
            // 
            this.cbPayor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbPayor.FormattingEnabled = true;
            this.cbPayor.Location = new System.Drawing.Point(388, 3);
            this.cbPayor.Name = "cbPayor";
            this.cbPayor.Size = new System.Drawing.Size(300, 21);
            this.cbPayor.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label9.Location = new System.Drawing.Point(343, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 27);
            this.label9.TabIndex = 0;
            this.label9.Text = "Payor";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbPatient
            // 
            this.tbPatient.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tbPatient.ColumnCount = 2;
            this.tbPatient.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbPatient.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 204F));
            this.tbPatient.Controls.Add(this.tbPayor, 1, 0);
            this.tbPatient.Controls.Add(this.tbPatientSelect, 0, 0);
            this.tbPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPatient.Location = new System.Drawing.Point(1, 102);
            this.tbPatient.Margin = new System.Windows.Forms.Padding(0);
            this.tbPatient.Name = "tbPatient";
            this.tbPatient.RowCount = 1;
            this.tbPatient.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbPatient.Size = new System.Drawing.Size(885, 366);
            this.tbPatient.TabIndex = 1;
            // 
            // tbPayor
            // 
            this.tbPayor.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbPayor.ColumnCount = 1;
            this.tbPayor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbPayor.Controls.Add(this.label5, 0, 1);
            this.tbPayor.Controls.Add(this.label6, 0, 3);
            this.tbPayor.Controls.Add(this.label7, 0, 5);
            this.tbPayor.Controls.Add(this.label8, 0, 7);
            this.tbPayor.Controls.Add(this.tbButton, 0, 9);
            this.tbPayor.Controls.Add(this.cboPayor, 0, 2);
            this.tbPayor.Controls.Add(this.comboBox1, 0, 4);
            this.tbPayor.Controls.Add(this.comboBox2, 0, 6);
            this.tbPayor.Controls.Add(this.comboBox3, 0, 8);
            this.tbPayor.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tbPayor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPayor.Location = new System.Drawing.Point(680, 1);
            this.tbPayor.Margin = new System.Windows.Forms.Padding(0);
            this.tbPayor.Name = "tbPayor";
            this.tbPayor.RowCount = 10;
            this.tbPayor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbPayor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbPayor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbPayor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbPayor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbPayor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbPayor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbPayor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbPayor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbPayor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbPayor.Size = new System.Drawing.Size(204, 364);
            this.tbPayor.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(157, 31);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(5);
            this.label5.Size = new System.Drawing.Size(44, 23);
            this.label5.TabIndex = 0;
            this.label5.Text = "Payor";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.Location = new System.Drawing.Point(133, 81);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(5);
            this.label6.Size = new System.Drawing.Size(68, 23);
            this.label6.TabIndex = 0;
            this.label6.Text = "Agreement";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.Location = new System.Drawing.Point(126, 131);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(5);
            this.label7.Size = new System.Drawing.Size(75, 23);
            this.label7.TabIndex = 0;
            this.label7.Text = "Payor Office";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label8.Location = new System.Drawing.Point(156, 181);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(5);
            this.label8.Size = new System.Drawing.Size(45, 23);
            this.label8.TabIndex = 0;
            this.label8.Text = "Policy";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbButton
            // 
            this.tbButton.AutoSize = true;
            this.tbButton.ColumnCount = 2;
            this.tbButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbButton.Controls.Add(this.btConvert, 0, 0);
            this.tbButton.Controls.Add(this.button1, 1, 0);
            this.tbButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbButton.Location = new System.Drawing.Point(0, 231);
            this.tbButton.Margin = new System.Windows.Forms.Padding(0);
            this.tbButton.Name = "tbButton";
            this.tbButton.RowCount = 1;
            this.tbButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbButton.Size = new System.Drawing.Size(204, 133);
            this.tbButton.TabIndex = 1;
            // 
            // btConvert
            // 
            this.btConvert.Dock = System.Windows.Forms.DockStyle.Top;
            this.btConvert.Location = new System.Drawing.Point(3, 3);
            this.btConvert.Name = "btConvert";
            this.btConvert.Size = new System.Drawing.Size(96, 50);
            this.btConvert.TabIndex = 0;
            this.btConvert.Text = "Convert";
            this.btConvert.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.Location = new System.Drawing.Point(105, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 50);
            this.button1.TabIndex = 0;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // cboPayor
            // 
            this.cboPayor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboPayor.FormattingEnabled = true;
            this.cboPayor.Location = new System.Drawing.Point(3, 57);
            this.cboPayor.Name = "cboPayor";
            this.cboPayor.Size = new System.Drawing.Size(198, 21);
            this.cboPayor.TabIndex = 2;
            // 
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(3, 107);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(198, 21);
            this.comboBox1.TabIndex = 2;
            // 
            // comboBox2
            // 
            this.comboBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(3, 157);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(198, 21);
            this.comboBox2.TabIndex = 2;
            // 
            // comboBox3
            // 
            this.comboBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(3, 207);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(198, 21);
            this.comboBox3.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.pictureBox1, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.Padding = new System.Windows.Forms.Padding(3);
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(204, 31);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(81, 3);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(5);
            this.label4.Size = new System.Drawing.Size(95, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "Payor Detail";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbPatientSelect
            // 
            this.tbPatientSelect.AutoSize = true;
            this.tbPatientSelect.ColumnCount = 1;
            this.tbPatientSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbPatientSelect.Controls.Add(this.tbFilter, 0, 0);
            this.tbPatientSelect.Controls.Add(this.gvPatient, 0, 1);
            this.tbPatientSelect.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tbPatientSelect.Controls.Add(this.pbProcess, 0, 3);
            this.tbPatientSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPatientSelect.Location = new System.Drawing.Point(1, 1);
            this.tbPatientSelect.Margin = new System.Windows.Forms.Padding(0);
            this.tbPatientSelect.Name = "tbPatientSelect";
            this.tbPatientSelect.RowCount = 4;
            this.tbPatientSelect.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbPatientSelect.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbPatientSelect.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbPatientSelect.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbPatientSelect.Size = new System.Drawing.Size(678, 364);
            this.tbPatientSelect.TabIndex = 1;
            // 
            // tbFilter
            // 
            this.tbFilter.AutoSize = true;
            this.tbFilter.ColumnCount = 6;
            this.tbFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tbFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tbFilter.Controls.Add(this.ddlIsConverted, 4, 0);
            this.tbFilter.Controls.Add(this.label11, 3, 0);
            this.tbFilter.Controls.Add(this.cbCheckAll, 2, 0);
            this.tbFilter.Controls.Add(this.pictureBox2, 0, 0);
            this.tbFilter.Controls.Add(this.lblIsConvertCount, 5, 0);
            this.tbFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbFilter.Location = new System.Drawing.Point(0, 0);
            this.tbFilter.Margin = new System.Windows.Forms.Padding(0);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.RowCount = 1;
            this.tbFilter.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbFilter.Size = new System.Drawing.Size(678, 27);
            this.tbFilter.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label11.Location = new System.Drawing.Point(97, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 27);
            this.label11.TabIndex = 0;
            this.label11.Text = "ConvertStatus";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbCheckAll
            // 
            this.cbCheckAll.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbCheckAll.AutoSize = true;
            this.cbCheckAll.Location = new System.Drawing.Point(23, 5);
            this.cbCheckAll.Name = "cbCheckAll";
            this.cbCheckAll.Size = new System.Drawing.Size(68, 17);
            this.cbCheckAll.TabIndex = 1;
            this.cbCheckAll.Text = "CheckAll";
            this.cbCheckAll.UseVisualStyleBackColor = true;
            this.cbCheckAll.CheckedChanged += new System.EventHandler(this.cbCheckAll_CheckedChanged);
            // 
            // ddlIsConverted
            // 
            this.ddlIsConverted.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ddlIsConverted.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlIsConverted.FormattingEnabled = true;
            this.ddlIsConverted.Items.AddRange(new object[] {
            "- ทั้งหมด -",
            "เฉพาะที่ยังไม่ Convert",
            "เฉพาะที่ Convert แล้ว"});
            this.ddlIsConverted.Location = new System.Drawing.Point(177, 3);
            this.ddlIsConverted.Name = "ddlIsConverted";
            this.ddlIsConverted.Size = new System.Drawing.Size(121, 21);
            this.ddlIsConverted.TabIndex = 47;
            this.ddlIsConverted.SelectedIndexChanged += new System.EventHandler(this.ddlIsConverted_SelectedIndexChanged);
            // 
            // gvPatient
            // 
            this.gvPatient.AllowUserToAddRows = false;
            this.gvPatient.AllowUserToDeleteRows = false;
            this.gvPatient.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gvPatient.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.gvPatient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPatient.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Choose});
            this.gvPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvPatient.Location = new System.Drawing.Point(3, 30);
            this.gvPatient.Name = "gvPatient";
            this.gvPatient.ReadOnly = true;
            this.gvPatient.Size = new System.Drawing.Size(672, 289);
            this.gvPatient.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.label12, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblProcessDetail, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 322);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(678, 13);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Crimson;
            this.label12.Location = new System.Drawing.Point(272, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(403, 13);
            this.label12.TabIndex = 56;
            this.label12.Text = "Tip : ไฮไลท์สีเหลือง=ลงทะเบียน Mobile แล้ว , ตัวหนังสือสีเขียว=ConvertPreOrder แล" +
    "้ว";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProcessDetail
            // 
            this.lblProcessDetail.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblProcessDetail.AutoSize = true;
            this.lblProcessDetail.Location = new System.Drawing.Point(3, 0);
            this.lblProcessDetail.Name = "lblProcessDetail";
            this.lblProcessDetail.Size = new System.Drawing.Size(72, 13);
            this.lblProcessDetail.TabIndex = 56;
            this.lblProcessDetail.Text = "ProcessDetail";
            this.lblProcessDetail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbProcess
            // 
            this.pbProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbProcess.Location = new System.Drawing.Point(3, 338);
            this.pbProcess.Name = "pbProcess";
            this.pbProcess.Size = new System.Drawing.Size(672, 23);
            this.pbProcess.TabIndex = 3;
            // 
            // pbSearch
            // 
            this.pbSearch.Image = global::MassConvert.Properties.Resources.icSearch;
            this.pbSearch.Location = new System.Drawing.Point(3, 3);
            this.pbSearch.Name = "pbSearch";
            this.pbSearch.Size = new System.Drawing.Size(92, 92);
            this.pbSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbSearch.TabIndex = 2;
            this.pbSearch.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::MassConvert.Properties.Resources.icPrice;
            this.pictureBox1.Location = new System.Drawing.Point(182, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = global::MassConvert.Properties.Resources.icFilter;
            this.pictureBox2.Location = new System.Drawing.Point(3, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(14, 21);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 48;
            this.pictureBox2.TabStop = false;
            // 
            // lblIsConvertCount
            // 
            this.lblIsConvertCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblIsConvertCount.AutoSize = true;
            this.lblIsConvertCount.Location = new System.Drawing.Point(304, 7);
            this.lblIsConvertCount.Name = "lblIsConvertCount";
            this.lblIsConvertCount.Size = new System.Drawing.Size(13, 13);
            this.lblIsConvertCount.TabIndex = 49;
            this.lblIsConvertCount.Text = "0";
            this.lblIsConvertCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Choose
            // 
            this.Choose.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Choose.HeaderText = "";
            this.Choose.Name = "Choose";
            this.Choose.ReadOnly = true;
            this.Choose.Width = 5;
            // 
            // ConvertByPayor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(887, 469);
            this.Controls.Add(this.tbDefault);
            this.Name = "ConvertByPayor";
            this.Text = "ConvertByPayor";
            this.Load += new System.EventHandler(this.ConvertByPayor_Load);
            this.tbDefault.ResumeLayout(false);
            this.tbDefault.PerformLayout();
            this.tbSearch.ResumeLayout(false);
            this.tbSearch.PerformLayout();
            this.tbSearchMain.ResumeLayout(false);
            this.tbSearchMain.PerformLayout();
            this.tbSearchButton.ResumeLayout(false);
            this.tbSearchButton.PerformLayout();
            this.tbSearchResult.ResumeLayout(false);
            this.tbSearchResult.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tbSearchStatus.ResumeLayout(false);
            this.tbSearchStatus.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tbPatient.ResumeLayout(false);
            this.tbPatient.PerformLayout();
            this.tbPayor.ResumeLayout(false);
            this.tbPayor.PerformLayout();
            this.tbButton.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tbPatientSelect.ResumeLayout(false);
            this.tbPatientSelect.PerformLayout();
            this.tbFilter.ResumeLayout(false);
            this.tbFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPatient)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tbDefault;
        private System.Windows.Forms.TableLayoutPanel tbSearch;
        private System.Windows.Forms.TableLayoutPanel tbPatient;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtDOETo;
        private System.Windows.Forms.TableLayoutPanel tbSearchButton;
        private System.Windows.Forms.Button btSearch;
        private System.Windows.Forms.Label lblSearchResult;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tbSearchStatus;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.RadioButton rbNotRegister;
        private System.Windows.Forms.RadioButton rbRegister;
        private System.Windows.Forms.TableLayoutPanel tbPayor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tbButton;
        private System.Windows.Forms.Button btConvert;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cboPayor;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.TableLayoutPanel tbSearchResult;
        private System.Windows.Forms.TableLayoutPanel tbSearchMain;
        private System.Windows.Forms.PictureBox pbSearch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DateTimePicker dtDOEFrom;
        private System.Windows.Forms.ComboBox cbPayor;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TableLayoutPanel tbPatientSelect;
        private System.Windows.Forms.TableLayoutPanel tbFilter;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox cbCheckAll;
        private System.Windows.Forms.ComboBox ddlIsConverted;
        private System.Windows.Forms.DataGridView gvPatient;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblProcessDetail;
        private System.Windows.Forms.ProgressBar pbProcess;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblIsConvertCount;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Choose;
    }
}