namespace MassConvert
{
    partial class FormIndividual
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
            this.lblCountPT = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtHN = new System.Windows.Forms.MaskedTextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labPolicy = new System.Windows.Forms.Label();
            this.cboPolicy = new System.Windows.Forms.ComboBox();
            this.labPayorOffice = new System.Windows.Forms.Label();
            this.cboPayorOffice = new System.Windows.Forms.ComboBox();
            this.lblAgreement = new System.Windows.Forms.Label();
            this.cboAgreement = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboPayor = new System.Windows.Forms.ComboBox();
            this.PrnSticker = new System.Windows.Forms.Button();
            this.btGenLabNo = new System.Windows.Forms.Button();
            this.btConvert = new System.Windows.Forms.Button();
            this.grpBoxPayor = new System.Windows.Forms.GroupBox();
            this.grpBoxHN = new System.Windows.Forms.GroupBox();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpBoxPayor.SuspendLayout();
            this.grpBoxHN.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCountPT
            // 
            this.lblCountPT.AutoSize = true;
            this.lblCountPT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblCountPT.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblCountPT.Location = new System.Drawing.Point(650, 405);
            this.lblCountPT.Name = "lblCountPT";
            this.lblCountPT.Size = new System.Drawing.Size(17, 17);
            this.lblCountPT.TabIndex = 10;
            this.lblCountPT.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "HN :";
            // 
            // txtHN
            // 
            this.txtHN.Location = new System.Drawing.Point(80, 42);
            this.txtHN.Mask = "00-00-000000";
            this.txtHN.Name = "txtHN";
            this.txtHN.Size = new System.Drawing.Size(319, 20);
            this.txtHN.TabIndex = 11;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(18, 399);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(539, 23);
            this.progressBar1.TabIndex = 20;
            // 
            // labPolicy
            // 
            this.labPolicy.AutoSize = true;
            this.labPolicy.Location = new System.Drawing.Point(6, 110);
            this.labPolicy.Name = "labPolicy";
            this.labPolicy.Size = new System.Drawing.Size(35, 13);
            this.labPolicy.TabIndex = 31;
            this.labPolicy.Text = "Policy";
            // 
            // cboPolicy
            // 
            this.cboPolicy.FormattingEnabled = true;
            this.cboPolicy.Location = new System.Drawing.Point(112, 105);
            this.cboPolicy.Name = "cboPolicy";
            this.cboPolicy.Size = new System.Drawing.Size(287, 21);
            this.cboPolicy.TabIndex = 30;
            // 
            // labPayorOffice
            // 
            this.labPayorOffice.AutoSize = true;
            this.labPayorOffice.Location = new System.Drawing.Point(6, 84);
            this.labPayorOffice.Name = "labPayorOffice";
            this.labPayorOffice.Size = new System.Drawing.Size(62, 13);
            this.labPayorOffice.TabIndex = 29;
            this.labPayorOffice.Text = "PayorOffice";
            // 
            // cboPayorOffice
            // 
            this.cboPayorOffice.FormattingEnabled = true;
            this.cboPayorOffice.Location = new System.Drawing.Point(112, 78);
            this.cboPayorOffice.Name = "cboPayorOffice";
            this.cboPayorOffice.Size = new System.Drawing.Size(287, 21);
            this.cboPayorOffice.TabIndex = 28;
            // 
            // lblAgreement
            // 
            this.lblAgreement.AutoSize = true;
            this.lblAgreement.Location = new System.Drawing.Point(6, 55);
            this.lblAgreement.Name = "lblAgreement";
            this.lblAgreement.Size = new System.Drawing.Size(58, 13);
            this.lblAgreement.TabIndex = 27;
            this.lblAgreement.Text = "Agreement";
            // 
            // cboAgreement
            // 
            this.cboAgreement.FormattingEnabled = true;
            this.cboAgreement.Location = new System.Drawing.Point(112, 51);
            this.cboAgreement.Name = "cboAgreement";
            this.cboAgreement.Size = new System.Drawing.Size(287, 21);
            this.cboAgreement.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Payor";
            // 
            // cboPayor
            // 
            this.cboPayor.FormattingEnabled = true;
            this.cboPayor.Location = new System.Drawing.Point(112, 24);
            this.cboPayor.Name = "cboPayor";
            this.cboPayor.Size = new System.Drawing.Size(287, 21);
            this.cboPayor.TabIndex = 24;
            this.cboPayor.DropDown += new System.EventHandler(this.cboPayor_DropDown);
            this.cboPayor.SelectedIndexChanged += new System.EventHandler(this.cboPayor_SelectedIndexChanged);
            // 
            // PrnSticker
            // 
            this.PrnSticker.Location = new System.Drawing.Point(551, 78);
            this.PrnSticker.Name = "PrnSticker";
            this.PrnSticker.Size = new System.Drawing.Size(116, 40);
            this.PrnSticker.TabIndex = 23;
            this.PrnSticker.Text = "Print Sticker";
            this.PrnSticker.UseVisualStyleBackColor = true;
            this.PrnSticker.Click += new System.EventHandler(this.PrnSticker_Click);
            // 
            // btGenLabNo
            // 
            this.btGenLabNo.Location = new System.Drawing.Point(551, 30);
            this.btGenLabNo.Name = "btGenLabNo";
            this.btGenLabNo.Size = new System.Drawing.Size(116, 42);
            this.btGenLabNo.TabIndex = 22;
            this.btGenLabNo.Text = "Gen LabNo.";
            this.btGenLabNo.UseVisualStyleBackColor = true;
            this.btGenLabNo.Click += new System.EventHandler(this.btGenLabNo_Click);
            // 
            // btConvert
            // 
            this.btConvert.Location = new System.Drawing.Point(551, 209);
            this.btConvert.Name = "btConvert";
            this.btConvert.Size = new System.Drawing.Size(116, 43);
            this.btConvert.TabIndex = 21;
            this.btConvert.Text = "Convert";
            this.btConvert.UseVisualStyleBackColor = true;
            this.btConvert.Click += new System.EventHandler(this.btConvert_Click);
            // 
            // grpBoxPayor
            // 
            this.grpBoxPayor.BackColor = System.Drawing.Color.White;
            this.grpBoxPayor.Controls.Add(this.label5);
            this.grpBoxPayor.Controls.Add(this.cboPayor);
            this.grpBoxPayor.Controls.Add(this.cboPolicy);
            this.grpBoxPayor.Controls.Add(this.labPolicy);
            this.grpBoxPayor.Controls.Add(this.lblAgreement);
            this.grpBoxPayor.Controls.Add(this.cboAgreement);
            this.grpBoxPayor.Controls.Add(this.cboPayorOffice);
            this.grpBoxPayor.Controls.Add(this.labPayorOffice);
            this.grpBoxPayor.Location = new System.Drawing.Point(26, 111);
            this.grpBoxPayor.Name = "grpBoxPayor";
            this.grpBoxPayor.Size = new System.Drawing.Size(527, 148);
            this.grpBoxPayor.TabIndex = 32;
            this.grpBoxPayor.TabStop = false;
            this.grpBoxPayor.Text = "Payor Detail";
            // 
            // grpBoxHN
            // 
            this.grpBoxHN.BackColor = System.Drawing.Color.White;
            this.grpBoxHN.Controls.Add(this.dtpDateTo);
            this.grpBoxHN.Controls.Add(this.dtpDateFrom);
            this.grpBoxHN.Controls.Add(this.label2);
            this.grpBoxHN.Controls.Add(this.label1);
            this.grpBoxHN.Controls.Add(this.txtHN);
            this.grpBoxHN.Controls.Add(this.label8);
            this.grpBoxHN.Location = new System.Drawing.Point(26, 30);
            this.grpBoxHN.Name = "grpBoxHN";
            this.grpBoxHN.Size = new System.Drawing.Size(527, 75);
            this.grpBoxHN.TabIndex = 33;
            this.grpBoxHN.TabStop = false;
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.Location = new System.Drawing.Point(266, 16);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(133, 20);
            this.dtpDateTo.TabIndex = 15;
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.Location = new System.Drawing.Point(80, 16);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(122, 20);
            this.dtpDateFrom.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Date To :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Date From :";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.lblCountPT);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.btConvert);
            this.groupBox1.Controls.Add(this.PrnSticker);
            this.groupBox1.Controls.Add(this.btGenLabNo);
            this.groupBox1.Location = new System.Drawing.Point(8, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(688, 441);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            // 
            // FormIndividual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(708, 474);
            this.Controls.Add(this.grpBoxHN);
            this.Controls.Add(this.grpBoxPayor);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormIndividual";
            this.Text = "FormIndividual";
            this.Load += new System.EventHandler(this.FormIndividual_Load);
            this.grpBoxPayor.ResumeLayout(false);
            this.grpBoxPayor.PerformLayout();
            this.grpBoxHN.ResumeLayout(false);
            this.grpBoxHN.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCountPT;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.MaskedTextBox txtHN;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labPolicy;
        private System.Windows.Forms.ComboBox cboPolicy;
        private System.Windows.Forms.Label labPayorOffice;
        private System.Windows.Forms.ComboBox cboPayorOffice;
        private System.Windows.Forms.Label lblAgreement;
        private System.Windows.Forms.ComboBox cboAgreement;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboPayor;
        private System.Windows.Forms.Button PrnSticker;
        private System.Windows.Forms.Button btGenLabNo;
        private System.Windows.Forms.Button btConvert;
        private System.Windows.Forms.GroupBox grpBoxPayor;
        private System.Windows.Forms.GroupBox grpBoxHN;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}