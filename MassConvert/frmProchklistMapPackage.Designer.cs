namespace MassConvert
{
    partial class frmProchklistMapPackage
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
            this.ddlProChkList = new System.Windows.Forms.ComboBox();
            this.lblProChkList = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPackage = new System.Windows.Forms.TextBox();
            this.btFindPackage = new System.Windows.Forms.Button();
            this.gvPackage = new System.Windows.Forms.DataGridView();
            this.btSave = new System.Windows.Forms.Button();
            this.gvProchklistMapPackage = new System.Windows.Forms.DataGridView();
            this.btUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gvPackage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProchklistMapPackage)).BeginInit();
            this.SuspendLayout();
            // 
            // ddlProChkList
            // 
            this.ddlProChkList.FormattingEnabled = true;
            this.ddlProChkList.Location = new System.Drawing.Point(94, 12);
            this.ddlProChkList.Name = "ddlProChkList";
            this.ddlProChkList.Size = new System.Drawing.Size(843, 21);
            this.ddlProChkList.TabIndex = 0;
            this.ddlProChkList.SelectedIndexChanged += new System.EventHandler(this.ddlProChkList_SelectedIndexChanged);
            // 
            // lblProChkList
            // 
            this.lblProChkList.AutoSize = true;
            this.lblProChkList.Location = new System.Drawing.Point(12, 15);
            this.lblProChkList.Name = "lblProChkList";
            this.lblProChkList.Size = new System.Drawing.Size(76, 13);
            this.lblProChkList.TabIndex = 1;
            this.lblProChkList.Text = "Pro Check List";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Package";
            // 
            // txtPackage
            // 
            this.txtPackage.Location = new System.Drawing.Point(94, 39);
            this.txtPackage.Name = "txtPackage";
            this.txtPackage.Size = new System.Drawing.Size(762, 20);
            this.txtPackage.TabIndex = 3;
            // 
            // btFindPackage
            // 
            this.btFindPackage.Location = new System.Drawing.Point(862, 39);
            this.btFindPackage.Name = "btFindPackage";
            this.btFindPackage.Size = new System.Drawing.Size(75, 23);
            this.btFindPackage.TabIndex = 4;
            this.btFindPackage.Text = "Find";
            this.btFindPackage.UseVisualStyleBackColor = true;
            this.btFindPackage.Click += new System.EventHandler(this.btFindPackage_Click);
            // 
            // gvPackage
            // 
            this.gvPackage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvPackage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPackage.Location = new System.Drawing.Point(94, 68);
            this.gvPackage.Name = "gvPackage";
            this.gvPackage.Size = new System.Drawing.Size(843, 204);
            this.gvPackage.TabIndex = 5;
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(862, 278);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 6;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // gvProchklistMapPackage
            // 
            this.gvProchklistMapPackage.AllowUserToAddRows = false;
            this.gvProchklistMapPackage.AllowUserToDeleteRows = false;
            this.gvProchklistMapPackage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvProchklistMapPackage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvProchklistMapPackage.Location = new System.Drawing.Point(94, 307);
            this.gvProchklistMapPackage.Name = "gvProchklistMapPackage";
            this.gvProchklistMapPackage.Size = new System.Drawing.Size(843, 167);
            this.gvProchklistMapPackage.TabIndex = 7;
            // 
            // btUpdate
            // 
            this.btUpdate.Location = new System.Drawing.Point(862, 479);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(75, 23);
            this.btUpdate.TabIndex = 8;
            this.btUpdate.Text = "Delete";
            this.btUpdate.UseVisualStyleBackColor = true;
            this.btUpdate.Click += new System.EventHandler(this.btUpdate_Click);
            // 
            // frmProchklistMapPackage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(965, 514);
            this.Controls.Add(this.btUpdate);
            this.Controls.Add(this.gvProchklistMapPackage);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.gvPackage);
            this.Controls.Add(this.btFindPackage);
            this.Controls.Add(this.txtPackage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblProChkList);
            this.Controls.Add(this.ddlProChkList);
            this.Name = "frmProchklistMapPackage";
            this.Text = "frmProchklistMapPackage";
            this.Load += new System.EventHandler(this.frmProchklistMapPackage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvPackage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProchklistMapPackage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ddlProChkList;
        private System.Windows.Forms.Label lblProChkList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPackage;
        private System.Windows.Forms.Button btFindPackage;
        private System.Windows.Forms.DataGridView gvPackage;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.DataGridView gvProchklistMapPackage;
        private System.Windows.Forms.Button btUpdate;
    }
}