namespace MassConvert
{
    partial class ConvertResult
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
            this.lblDefault = new System.Windows.Forms.Label();
            this.gvDefault = new System.Windows.Forms.DataGridView();
            this.tbDefault.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvDefault)).BeginInit();
            this.SuspendLayout();
            // 
            // tbDefault
            // 
            this.tbDefault.AutoSize = true;
            this.tbDefault.ColumnCount = 1;
            this.tbDefault.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbDefault.Controls.Add(this.lblDefault, 0, 0);
            this.tbDefault.Controls.Add(this.gvDefault, 0, 1);
            this.tbDefault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDefault.Location = new System.Drawing.Point(0, 0);
            this.tbDefault.Name = "tbDefault";
            this.tbDefault.RowCount = 2;
            this.tbDefault.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbDefault.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbDefault.Size = new System.Drawing.Size(662, 475);
            this.tbDefault.TabIndex = 0;
            // 
            // lblDefault
            // 
            this.lblDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDefault.AutoSize = true;
            this.lblDefault.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblDefault.Location = new System.Drawing.Point(3, 0);
            this.lblDefault.Name = "lblDefault";
            this.lblDefault.Padding = new System.Windows.Forms.Padding(10);
            this.lblDefault.Size = new System.Drawing.Size(656, 37);
            this.lblDefault.TabIndex = 0;
            this.lblDefault.Text = "-";
            this.lblDefault.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gvDefault
            // 
            this.gvDefault.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvDefault.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.gvDefault.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvDefault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvDefault.Location = new System.Drawing.Point(3, 40);
            this.gvDefault.Name = "gvDefault";
            this.gvDefault.Size = new System.Drawing.Size(656, 432);
            this.gvDefault.TabIndex = 1;
            this.gvDefault.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gvDefault_CellFormatting);
            // 
            // ConvertResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(662, 475);
            this.Controls.Add(this.tbDefault);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ConvertResult";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ConvertResult";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ConvertResult_Load);
            this.tbDefault.ResumeLayout(false);
            this.tbDefault.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvDefault)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tbDefault;
        private System.Windows.Forms.Label lblDefault;
        private System.Windows.Forms.DataGridView gvDefault;
    }
}