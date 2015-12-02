namespace MassConvert
{
    partial class MapPayorSummary
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
            this.tbHeader = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tbHeaderText = new System.Windows.Forms.TableLayoutPanel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblHeaderDetail = new System.Windows.Forms.Label();
            this.gvDefault = new System.Windows.Forms.DataGridView();
            this.tbDefault.SuspendLayout();
            this.tbHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tbHeaderText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvDefault)).BeginInit();
            this.SuspendLayout();
            // 
            // tbDefault
            // 
            this.tbDefault.ColumnCount = 1;
            this.tbDefault.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbDefault.Controls.Add(this.tbHeader, 0, 0);
            this.tbDefault.Controls.Add(this.gvDefault, 0, 1);
            this.tbDefault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDefault.Location = new System.Drawing.Point(0, 0);
            this.tbDefault.Name = "tbDefault";
            this.tbDefault.RowCount = 2;
            this.tbDefault.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbDefault.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbDefault.Size = new System.Drawing.Size(555, 462);
            this.tbDefault.TabIndex = 0;
            // 
            // tbHeader
            // 
            this.tbHeader.AutoSize = true;
            this.tbHeader.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbHeader.ColumnCount = 2;
            this.tbHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbHeader.Controls.Add(this.pictureBox1, 0, 0);
            this.tbHeader.Controls.Add(this.tbHeaderText, 1, 0);
            this.tbHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbHeader.Location = new System.Drawing.Point(0, 0);
            this.tbHeader.Margin = new System.Windows.Forms.Padding(0);
            this.tbHeader.Name = "tbHeader";
            this.tbHeader.Padding = new System.Windows.Forms.Padding(3);
            this.tbHeader.RowCount = 1;
            this.tbHeader.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbHeader.Size = new System.Drawing.Size(555, 86);
            this.tbHeader.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MassConvert.Properties.Resources.icPayorMap;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(80, 80);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tbHeaderText
            // 
            this.tbHeaderText.AutoSize = true;
            this.tbHeaderText.ColumnCount = 1;
            this.tbHeaderText.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbHeaderText.Controls.Add(this.lblHeader, 0, 0);
            this.tbHeaderText.Controls.Add(this.lblHeaderDetail, 0, 1);
            this.tbHeaderText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbHeaderText.Location = new System.Drawing.Point(83, 3);
            this.tbHeaderText.Margin = new System.Windows.Forms.Padding(0);
            this.tbHeaderText.Name = "tbHeaderText";
            this.tbHeaderText.RowCount = 2;
            this.tbHeaderText.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbHeaderText.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbHeaderText.Size = new System.Drawing.Size(469, 80);
            this.tbHeaderText.TabIndex = 1;
            // 
            // lblHeader
            // 
            this.lblHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblHeader.Location = new System.Drawing.Point(3, 8);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(278, 32);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Map Payor Summary";
            // 
            // lblHeaderDetail
            // 
            this.lblHeaderDetail.AutoSize = true;
            this.lblHeaderDetail.Location = new System.Drawing.Point(3, 40);
            this.lblHeaderDetail.Name = "lblHeaderDetail";
            this.lblHeaderDetail.Size = new System.Drawing.Size(211, 13);
            this.lblHeaderDetail.TabIndex = 0;
            this.lblHeaderDetail.Text = "สรุปข้อมูลการแมพ Payor ที่มีทั้งหมดในระบบ";
            // 
            // gvDefault
            // 
            this.gvDefault.AllowUserToAddRows = false;
            this.gvDefault.AllowUserToDeleteRows = false;
            this.gvDefault.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvDefault.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.gvDefault.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gvDefault.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvDefault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvDefault.Location = new System.Drawing.Point(7, 93);
            this.gvDefault.Margin = new System.Windows.Forms.Padding(7);
            this.gvDefault.Name = "gvDefault";
            this.gvDefault.ReadOnly = true;
            this.gvDefault.Size = new System.Drawing.Size(541, 362);
            this.gvDefault.TabIndex = 2;
            // 
            // MapPayorSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(555, 462);
            this.Controls.Add(this.tbDefault);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MapPayorSummary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "สรุปข้อมูลการแมพ";
            this.Load += new System.EventHandler(this.MapPayorSummary_Load);
            this.tbDefault.ResumeLayout(false);
            this.tbDefault.PerformLayout();
            this.tbHeader.ResumeLayout(false);
            this.tbHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tbHeaderText.ResumeLayout(false);
            this.tbHeaderText.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvDefault)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tbDefault;
        private System.Windows.Forms.TableLayoutPanel tbHeader;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel tbHeaderText;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblHeaderDetail;
        private System.Windows.Forms.DataGridView gvDefault;
    }
}