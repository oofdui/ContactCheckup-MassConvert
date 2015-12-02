namespace MassConvert
{
    partial class MapPayorDOE
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
            this.tbDOEAdd = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtDOEFrom = new System.Windows.Forms.DateTimePicker();
            this.dtDOETo = new System.Windows.Forms.DateTimePicker();
            this.tbDOEButton = new System.Windows.Forms.TableLayoutPanel();
            this.btDOESubmit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPayor = new System.Windows.Forms.Label();
            this.btDelete = new System.Windows.Forms.Button();
            this.tbDOEAdd.SuspendLayout();
            this.tbDOEButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbDOEAdd
            // 
            this.tbDOEAdd.AutoSize = true;
            this.tbDOEAdd.ColumnCount = 2;
            this.tbDOEAdd.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbDOEAdd.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbDOEAdd.Controls.Add(this.label3, 0, 1);
            this.tbDOEAdd.Controls.Add(this.label4, 0, 2);
            this.tbDOEAdd.Controls.Add(this.dtDOEFrom, 1, 1);
            this.tbDOEAdd.Controls.Add(this.dtDOETo, 1, 2);
            this.tbDOEAdd.Controls.Add(this.tbDOEButton, 1, 3);
            this.tbDOEAdd.Controls.Add(this.label1, 0, 0);
            this.tbDOEAdd.Controls.Add(this.lblPayor, 1, 0);
            this.tbDOEAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDOEAdd.Location = new System.Drawing.Point(0, 0);
            this.tbDOEAdd.Name = "tbDOEAdd";
            this.tbDOEAdd.Padding = new System.Windows.Forms.Padding(3);
            this.tbDOEAdd.RowCount = 4;
            this.tbDOEAdd.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbDOEAdd.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbDOEAdd.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbDOEAdd.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbDOEAdd.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tbDOEAdd.Size = new System.Drawing.Size(318, 104);
            this.tbDOEAdd.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(11, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "From";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(23, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "To";
            // 
            // dtDOEFrom
            // 
            this.dtDOEFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtDOEFrom.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtDOEFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDOEFrom.Location = new System.Drawing.Point(51, 25);
            this.dtDOEFrom.Name = "dtDOEFrom";
            this.dtDOEFrom.Size = new System.Drawing.Size(261, 20);
            this.dtDOEFrom.TabIndex = 2;
            // 
            // dtDOETo
            // 
            this.dtDOETo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtDOETo.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtDOETo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDOETo.Location = new System.Drawing.Point(51, 51);
            this.dtDOETo.Name = "dtDOETo";
            this.dtDOETo.Size = new System.Drawing.Size(261, 20);
            this.dtDOETo.TabIndex = 2;
            // 
            // tbDOEButton
            // 
            this.tbDOEButton.AutoSize = true;
            this.tbDOEButton.ColumnCount = 2;
            this.tbDOEButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbDOEButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbDOEButton.Controls.Add(this.btDOESubmit, 1, 0);
            this.tbDOEButton.Controls.Add(this.btDelete, 0, 0);
            this.tbDOEButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDOEButton.Location = new System.Drawing.Point(48, 74);
            this.tbDOEButton.Margin = new System.Windows.Forms.Padding(0);
            this.tbDOEButton.Name = "tbDOEButton";
            this.tbDOEButton.RowCount = 1;
            this.tbDOEButton.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbDOEButton.Size = new System.Drawing.Size(267, 29);
            this.tbDOEButton.TabIndex = 3;
            // 
            // btDOESubmit
            // 
            this.btDOESubmit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btDOESubmit.Enabled = false;
            this.btDOESubmit.Location = new System.Drawing.Point(189, 3);
            this.btDOESubmit.Name = "btDOESubmit";
            this.btDOESubmit.Size = new System.Drawing.Size(75, 23);
            this.btDOESubmit.TabIndex = 1;
            this.btDOESubmit.Text = "UPDATE";
            this.btDOESubmit.UseVisualStyleBackColor = true;
            this.btDOESubmit.Click += new System.EventHandler(this.btDOESubmit_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Payor";
            // 
            // lblPayor
            // 
            this.lblPayor.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPayor.AutoSize = true;
            this.lblPayor.Location = new System.Drawing.Point(51, 3);
            this.lblPayor.Name = "lblPayor";
            this.lblPayor.Padding = new System.Windows.Forms.Padding(3);
            this.lblPayor.Size = new System.Drawing.Size(40, 19);
            this.lblPayor.TabIndex = 0;
            this.lblPayor.Text = "Payor";
            // 
            // btDelete
            // 
            this.btDelete.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btDelete.Enabled = false;
            this.btDelete.Location = new System.Drawing.Point(108, 3);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(75, 23);
            this.btDelete.TabIndex = 1;
            this.btDelete.Text = "DELETE";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // MapPayorDOE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(318, 104);
            this.Controls.Add(this.tbDOEAdd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MapPayorDOE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "แก้ไขช่วงวันที่ออกหน่วย";
            this.Load += new System.EventHandler(this.MapPayorDOE_Load);
            this.tbDOEAdd.ResumeLayout(false);
            this.tbDOEAdd.PerformLayout();
            this.tbDOEButton.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tbDOEAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtDOEFrom;
        private System.Windows.Forms.DateTimePicker dtDOETo;
        private System.Windows.Forms.TableLayoutPanel tbDOEButton;
        private System.Windows.Forms.Button btDOESubmit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPayor;
        private System.Windows.Forms.Button btDelete;
    }
}