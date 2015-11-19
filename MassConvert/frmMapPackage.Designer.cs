namespace MassConvert
{
    partial class frmMapPackage
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btLoad = new System.Windows.Forms.Button();
            this.lblCountPT = new System.Windows.Forms.Label();
            this.gvPatient = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btFind = new System.Windows.Forms.Button();
            this.dtpTimeTo = new System.Windows.Forms.DateTimePicker();
            this.dtpTimeFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.chkBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPatient)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.chkBox);
            this.groupBox1.Controls.Add(this.btFind);
            this.groupBox1.Controls.Add(this.dtpTimeTo);
            this.groupBox1.Controls.Add(this.dtpTimeFrom);
            this.groupBox1.Controls.Add(this.btLoad);
            this.groupBox1.Controls.Add(this.dtpDateTo);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.lblDateFrom);
            this.groupBox1.Controls.Add(this.dtpDateFrom);
            this.groupBox1.Controls.Add(this.lblCountPT);
            this.groupBox1.Controls.Add(this.gvPatient);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(958, 294);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            // 
            // btLoad
            // 
            this.btLoad.Location = new System.Drawing.Point(859, 260);
            this.btLoad.Name = "btLoad";
            this.btLoad.Size = new System.Drawing.Size(81, 27);
            this.btLoad.TabIndex = 2;
            this.btLoad.Text = "Load";
            this.btLoad.UseVisualStyleBackColor = true;
            this.btLoad.Click += new System.EventHandler(this.btLoad_Click);
            // 
            // lblCountPT
            // 
            this.lblCountPT.AutoSize = true;
            this.lblCountPT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblCountPT.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblCountPT.Location = new System.Drawing.Point(636, 16);
            this.lblCountPT.Name = "lblCountPT";
            this.lblCountPT.Size = new System.Drawing.Size(17, 17);
            this.lblCountPT.TabIndex = 4;
            this.lblCountPT.Text = "0";
            // 
            // gvPatient
            // 
            this.gvPatient.AllowUserToAddRows = false;
            this.gvPatient.AllowUserToDeleteRows = false;
            this.gvPatient.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvPatient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPatient.Location = new System.Drawing.Point(15, 90);
            this.gvPatient.Name = "gvPatient";
            this.gvPatient.Size = new System.Drawing.Size(925, 164);
            this.gvPatient.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 312);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(958, 23);
            this.progressBar1.TabIndex = 27;
            // 
            // btFind
            // 
            this.btFind.Location = new System.Drawing.Point(493, 14);
            this.btFind.Name = "btFind";
            this.btFind.Size = new System.Drawing.Size(53, 48);
            this.btFind.TabIndex = 35;
            this.btFind.Text = "Find";
            this.btFind.UseVisualStyleBackColor = true;
            this.btFind.Click += new System.EventHandler(this.btFind_Click);
            // 
            // dtpTimeTo
            // 
            this.dtpTimeTo.AllowDrop = true;
            this.dtpTimeTo.CustomFormat = "HH:mm";
            this.dtpTimeTo.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTimeTo.Location = new System.Drawing.Point(317, 42);
            this.dtpTimeTo.Name = "dtpTimeTo";
            this.dtpTimeTo.ShowUpDown = true;
            this.dtpTimeTo.Size = new System.Drawing.Size(170, 20);
            this.dtpTimeTo.TabIndex = 34;
            // 
            // dtpTimeFrom
            // 
            this.dtpTimeFrom.AllowDrop = true;
            this.dtpTimeFrom.CustomFormat = "HH:mm";
            this.dtpTimeFrom.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTimeFrom.Location = new System.Drawing.Point(83, 42);
            this.dtpTimeFrom.Name = "dtpTimeFrom";
            this.dtpTimeFrom.ShowUpDown = true;
            this.dtpTimeFrom.Size = new System.Drawing.Size(170, 20);
            this.dtpTimeFrom.TabIndex = 33;
            this.dtpTimeFrom.Value = new System.DateTime(2014, 2, 10, 9, 21, 28, 0);
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.Location = new System.Drawing.Point(317, 14);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(170, 20);
            this.dtpDateTo.TabIndex = 32;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(259, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 13);
            this.label10.TabIndex = 31;
            this.label10.Text = "Date To :";
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.AutoSize = true;
            this.lblDateFrom.Location = new System.Drawing.Point(15, 20);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(62, 13);
            this.lblDateFrom.TabIndex = 30;
            this.lblDateFrom.Text = "Date From :";
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.Location = new System.Drawing.Point(83, 16);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(170, 20);
            this.dtpDateFrom.TabIndex = 29;
            // 
            // chkBox
            // 
            this.chkBox.AutoSize = true;
            this.chkBox.Location = new System.Drawing.Point(72, 67);
            this.chkBox.Name = "chkBox";
            this.chkBox.Size = new System.Drawing.Size(70, 17);
            this.chkBox.TabIndex = 36;
            this.chkBox.Text = "Select All";
            this.chkBox.UseVisualStyleBackColor = true;
            this.chkBox.CheckedChanged += new System.EventHandler(this.chkBox_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.SteelBlue;
            this.label2.Location = new System.Drawing.Point(506, 267);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(347, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "คลิกปุ่ม Load เพื่อทำการ Maping คนไข้กับ Package ต่าง ๆ ใน B-Connect";
            // 
            // frmMapPackage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(982, 345);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmMapPackage";
            this.Text = "frmMapPackage";
            this.Load += new System.EventHandler(this.frmMapPackage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPatient)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btLoad;
        private System.Windows.Forms.Label lblCountPT;
        private System.Windows.Forms.DataGridView gvPatient;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btFind;
        private System.Windows.Forms.DateTimePicker dtpTimeTo;
        private System.Windows.Forms.DateTimePicker dtpTimeFrom;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.CheckBox chkBox;
        private System.Windows.Forms.Label label2;
    }
}