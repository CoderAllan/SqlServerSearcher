namespace SQLServerSearcher
{
    partial class FrmViewSource
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmViewSource));
            this.pnlText = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslblColumn = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblLine = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlText
            // 
            this.pnlText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlText.Location = new System.Drawing.Point(12, 12);
            this.pnlText.Name = "pnlText";
            this.pnlText.Size = new System.Drawing.Size(862, 873);
            this.pnlText.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblColumn,
            this.tslblLine});
            this.statusStrip1.Location = new System.Drawing.Point(0, 888);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(886, 30);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslblColumn
            // 
            this.tslblColumn.Name = "tslblColumn";
            this.tslblColumn.Size = new System.Drawing.Size(57, 25);
            this.tslblColumn.Text = "Col: 1";
            // 
            // tslblLine
            // 
            this.tslblLine.Name = "tslblLine";
            this.tslblLine.Size = new System.Drawing.Size(49, 25);
            this.tslblLine.Text = "Ln: 1";
            // 
            // FrmViewSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 918);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pnlText);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmViewSource";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FrmViewSource";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmViewSource_FormClosing);
            this.Load += new System.EventHandler(this.FrmViewSource_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlText;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tslblColumn;
        private System.Windows.Forms.ToolStripStatusLabel tslblLine;
    }
}