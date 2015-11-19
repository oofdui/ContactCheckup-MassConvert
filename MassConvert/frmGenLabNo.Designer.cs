namespace MassConvert
{
    partial class frmGenLabNo
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
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.btFind = new Telerik.WinControls.UI.RadButton();
            this.dtpTimeTo = new Telerik.WinControls.UI.RadDateTimePicker();
            this.dtpTimeFrom = new Telerik.WinControls.UI.RadDateTimePicker();
            this.dtpDateTo = new Telerik.WinControls.UI.RadDateTimePicker();
            this.dtpDateFrom = new Telerik.WinControls.UI.RadDateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCountPT = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.radGroupBox2 = new Telerik.WinControls.UI.RadGroupBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.chkBox = new Telerik.WinControls.UI.RadCheckBox();
            this.txtFilterPayor = new Telerik.WinControls.UI.RadTextBox();
            this.gvPatient = new Telerik.WinControls.UI.RadGridView();
            this.btGenLabNo = new Telerik.WinControls.UI.RadButton();
            this.btPrintSticker = new Telerik.WinControls.UI.RadButton();
            this.desertTheme1 = new Telerik.WinControls.Themes.DesertTheme();
            this.progressBar1 = new Telerik.WinControls.UI.RadProgressBar();
            this.lblStatus = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btFind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTimeTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTimeFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDateTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDateFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).BeginInit();
            this.radGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilterPayor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPatient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPatient.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btGenLabNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btPrintSticker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox1.Controls.Add(this.btFind);
            this.radGroupBox1.Controls.Add(this.dtpTimeTo);
            this.radGroupBox1.Controls.Add(this.dtpTimeFrom);
            this.radGroupBox1.Controls.Add(this.dtpDateTo);
            this.radGroupBox1.Controls.Add(this.dtpDateFrom);
            this.radGroupBox1.Controls.Add(this.label1);
            this.radGroupBox1.Controls.Add(this.lblCountPT);
            this.radGroupBox1.Controls.Add(this.label10);
            this.radGroupBox1.Controls.Add(this.lblDateFrom);
            this.radGroupBox1.FooterImageIndex = -1;
            this.radGroupBox1.FooterImageKey = "";
            this.radGroupBox1.HeaderImageIndex = -1;
            this.radGroupBox1.HeaderImageKey = "";
            this.radGroupBox1.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.radGroupBox1.HeaderText = "";
            this.radGroupBox1.Location = new System.Drawing.Point(12, 12);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Padding = new System.Windows.Forms.Padding(2, 18, 2, 2);
            // 
            // 
            // 
            this.radGroupBox1.RootElement.Padding = new System.Windows.Forms.Padding(2, 18, 2, 2);
            this.radGroupBox1.Size = new System.Drawing.Size(934, 78);
            this.radGroupBox1.TabIndex = 0;
            this.radGroupBox1.ThemeName = "Desert";
            // 
            // btFind
            // 
            this.btFind.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btFind.Location = new System.Drawing.Point(721, 12);
            this.btFind.Name = "btFind";
            this.btFind.Size = new System.Drawing.Size(105, 54);
            this.btFind.TabIndex = 43;
            this.btFind.Text = "Find";
            this.btFind.ThemeName = "Desert";
            this.btFind.Click += new System.EventHandler(this.btFind_Click);
            // 
            // dtpTimeTo
            // 
            this.dtpTimeTo.AllowDrop = true;
            this.dtpTimeTo.CustomFormat = "HH:mm";
            this.dtpTimeTo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpTimeTo.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTimeTo.Location = new System.Drawing.Point(418, 41);
            this.dtpTimeTo.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTimeTo.MinDate = new System.DateTime(((long)(0)));
            this.dtpTimeTo.Name = "dtpTimeTo";
            this.dtpTimeTo.NullableValue = new System.DateTime(2014, 4, 30, 10, 1, 23, 230);
            this.dtpTimeTo.NullDate = new System.DateTime(((long)(0)));
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
            this.dtpTimeFrom.CustomFormat = "HH:mm";
            this.dtpTimeFrom.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpTimeFrom.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTimeFrom.Location = new System.Drawing.Point(82, 40);
            this.dtpTimeFrom.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTimeFrom.MinDate = new System.DateTime(((long)(0)));
            this.dtpTimeFrom.Name = "dtpTimeFrom";
            this.dtpTimeFrom.NullableValue = new System.DateTime(2014, 4, 30, 9, 59, 31, 951);
            this.dtpTimeFrom.NullDate = new System.DateTime(((long)(0)));
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
            this.dtpDateTo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpDateTo.Location = new System.Drawing.Point(418, 12);
            this.dtpDateTo.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpDateTo.MinDate = new System.DateTime(((long)(0)));
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.NullableValue = new System.DateTime(2014, 4, 30, 9, 58, 39, 279);
            this.dtpDateTo.NullDate = new System.DateTime(((long)(0)));
            this.dtpDateTo.Size = new System.Drawing.Size(275, 25);
            this.dtpDateTo.TabIndex = 42;
            this.dtpDateTo.TabStop = false;
            this.dtpDateTo.Text = "radDateTimePicker1";
            this.dtpDateTo.ThemeName = "Desert";
            this.dtpDateTo.Value = new System.DateTime(2014, 4, 30, 9, 58, 39, 279);
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpDateFrom.Location = new System.Drawing.Point(82, 12);
            this.dtpDateFrom.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpDateFrom.MinDate = new System.DateTime(((long)(0)));
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.NullableValue = new System.DateTime(2014, 4, 30, 9, 57, 31, 619);
            this.dtpDateFrom.NullDate = new System.DateTime(((long)(0)));
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
            this.label10.Size = new System.Drawing.Size(36, 21);
            this.label10.TabIndex = 34;
            this.label10.Text = "To :";
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
            // radGroupBox2
            // 
            this.radGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
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
            this.radGroupBox2.Location = new System.Drawing.Point(12, 102);
            this.radGroupBox2.Name = "radGroupBox2";
            this.radGroupBox2.Padding = new System.Windows.Forms.Padding(2, 18, 2, 2);
            // 
            // 
            // 
            this.radGroupBox2.RootElement.Padding = new System.Windows.Forms.Padding(2, 18, 2, 2);
            this.radGroupBox2.Size = new System.Drawing.Size(934, 311);
            this.radGroupBox2.TabIndex = 1;
            this.radGroupBox2.ThemeName = "Desert";
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.radLabel1.Location = new System.Drawing.Point(418, 13);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(126, 19);
            this.radLabel1.TabIndex = 45;
            this.radLabel1.Text = "Filter by Company :";
            // 
            // chkBox
            // 
            this.chkBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkBox.Location = new System.Drawing.Point(46, 13);
            this.chkBox.Name = "chkBox";
            this.chkBox.Size = new System.Drawing.Size(75, 19);
            this.chkBox.TabIndex = 44;
            this.chkBox.Text = "Check All";
            this.chkBox.ThemeName = "Desert";
            this.chkBox.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkBox_ToggleStateChanged);
            // 
            // txtFilterPayor
            // 
            this.txtFilterPayor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFilterPayor.Location = new System.Drawing.Point(550, 9);
            this.txtFilterPayor.Name = "txtFilterPayor";
            this.txtFilterPayor.Size = new System.Drawing.Size(368, 25);
            this.txtFilterPayor.TabIndex = 43;
            this.txtFilterPayor.TabStop = false;
            this.txtFilterPayor.ThemeName = "Desert";
            this.txtFilterPayor.TextChanged += new System.EventHandler(this.txtFilterPayor_TextChanged);
            // 
            // gvPatient
            // 
            this.gvPatient.Location = new System.Drawing.Point(13, 38);
            // 
            // gvPatient
            // 
            this.gvPatient.MasterTemplate.AllowAddNewRow = false;
            this.gvPatient.Name = "gvPatient";
            this.gvPatient.Padding = new System.Windows.Forms.Padding(1);
            // 
            // 
            // 
            this.gvPatient.RootElement.Padding = new System.Windows.Forms.Padding(1);
            this.gvPatient.ShowGroupPanel = false;
            this.gvPatient.Size = new System.Drawing.Size(905, 261);
            this.gvPatient.TabIndex = 42;
            this.gvPatient.Text = "radGridView1";
            this.gvPatient.ThemeName = "Desert";
            // 
            // btGenLabNo
            // 
            this.btGenLabNo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btGenLabNo.Location = new System.Drawing.Point(718, 419);
            this.btGenLabNo.Name = "btGenLabNo";
            this.btGenLabNo.Size = new System.Drawing.Size(108, 43);
            this.btGenLabNo.TabIndex = 44;
            this.btGenLabNo.Text = "Gen LabNo.";
            this.btGenLabNo.ThemeName = "Desert";
            this.btGenLabNo.Click += new System.EventHandler(this.btGenLabNo_Click);
            // 
            // btPrintSticker
            // 
            this.btPrintSticker.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btPrintSticker.Location = new System.Drawing.Point(832, 419);
            this.btPrintSticker.Name = "btPrintSticker";
            this.btPrintSticker.Size = new System.Drawing.Size(114, 43);
            this.btPrintSticker.TabIndex = 45;
            this.btPrintSticker.Text = "Print Sticker";
            this.btPrintSticker.ThemeName = "Desert";
            this.btPrintSticker.Visible = false;
            this.btPrintSticker.Click += new System.EventHandler(this.btPrintSticker_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.progressBar1.ImageIndex = -1;
            this.progressBar1.ImageKey = "";
            this.progressBar1.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.progressBar1.Location = new System.Drawing.Point(13, 439);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.SeparatorColor1 = System.Drawing.Color.White;
            this.progressBar1.SeparatorColor2 = System.Drawing.Color.White;
            this.progressBar1.SeparatorColor3 = System.Drawing.Color.White;
            this.progressBar1.SeparatorColor4 = System.Drawing.Color.White;
            this.progressBar1.Size = new System.Drawing.Size(692, 23);
            this.progressBar1.TabIndex = 46;
            this.progressBar1.ThemeName = "Desert";
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.lblStatus.Location = new System.Drawing.Point(13, 418);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(52, 15);
            this.lblStatus.TabIndex = 46;
            this.lblStatus.Text = "Waiting...";
            // 
            // frmGenLabNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 470);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btPrintSticker);
            this.Controls.Add(this.btGenLabNo);
            this.Controls.Add(this.radGroupBox2);
            this.Controls.Add(this.radGroupBox1);
            this.Name = "frmGenLabNo";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Gen Lab Number";
            this.ThemeName = "Desert";
            this.Load += new System.EventHandler(this.frmGenLabNo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            this.radGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btFind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTimeTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTimeFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDateTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDateFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).EndInit();
            this.radGroupBox2.ResumeLayout(false);
            this.radGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilterPayor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPatient.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPatient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btGenLabNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btPrintSticker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblDateFrom;
        private Telerik.WinControls.UI.RadGridView gvPatient;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCountPT;
        private Telerik.WinControls.UI.RadButton btFind;
        private Telerik.WinControls.UI.RadDateTimePicker dtpTimeTo;
        private Telerik.WinControls.UI.RadDateTimePicker dtpTimeFrom;
        private Telerik.WinControls.UI.RadDateTimePicker dtpDateTo;
        private Telerik.WinControls.UI.RadDateTimePicker dtpDateFrom;
        private Telerik.WinControls.UI.RadCheckBox chkBox;
        private Telerik.WinControls.UI.RadTextBox txtFilterPayor;
        private Telerik.WinControls.UI.RadButton btGenLabNo;
        private Telerik.WinControls.UI.RadButton btPrintSticker;
        private Telerik.WinControls.Themes.DesertTheme desertTheme1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadProgressBar progressBar1;
        private Telerik.WinControls.UI.RadLabel lblStatus;
    }
}
