namespace PdfView {
    partial class PdfControlView {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            CcTableLayoutPanelBase = new CcControl.CcTableLayoutPanel();
            CcMenuStrip1 = new CcControl.CcMenuStrip();
            CcStatusStrip1 = new CcControl.CcStatusStrip();
            CcPdfView1 = new CcControl.CcPdfView();
            CcPanelTop = new CcControl.CcPanel();
            CcButtonUpdate = new CcControl.CcButton();
            CcTableLayoutPanelBase.SuspendLayout();
            CcPanelTop.SuspendLayout();
            SuspendLayout();
            // 
            // CcTableLayoutPanelBase
            // 
            CcTableLayoutPanelBase.ColumnCount = 1;
            CcTableLayoutPanelBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            CcTableLayoutPanelBase.Controls.Add(CcMenuStrip1, 0, 0);
            CcTableLayoutPanelBase.Controls.Add(CcStatusStrip1, 0, 3);
            CcTableLayoutPanelBase.Controls.Add(CcPdfView1, 0, 2);
            CcTableLayoutPanelBase.Controls.Add(CcPanelTop, 0, 1);
            CcTableLayoutPanelBase.Dock = DockStyle.Fill;
            CcTableLayoutPanelBase.Location = new Point(0, 0);
            CcTableLayoutPanelBase.Name = "CcTableLayoutPanelBase";
            CcTableLayoutPanelBase.RowCount = 4;
            CcTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            CcTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            CcTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            CcTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            CcTableLayoutPanelBase.Size = new Size(1904, 1041);
            CcTableLayoutPanelBase.TabIndex = 0;
            // 
            // CcMenuStrip1
            // 
            CcMenuStrip1.Location = new Point(0, 0);
            CcMenuStrip1.Name = "CcMenuStrip1";
            CcMenuStrip1.Size = new Size(1904, 24);
            CcMenuStrip1.TabIndex = 0;
            CcMenuStrip1.Text = "ccMenuStrip1";
            CcMenuStrip1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // CcStatusStrip1
            // 
            CcStatusStrip1.Location = new Point(0, 1019);
            CcStatusStrip1.Name = "CcStatusStrip1";
            CcStatusStrip1.Size = new Size(1904, 22);
            CcStatusStrip1.TabIndex = 1;
            CcStatusStrip1.Text = "ccStatusStrip1";
            // 
            // CcPdfView1
            // 
            CcPdfView1.Dock = DockStyle.Fill;
            CcPdfView1.Location = new Point(4, 87);
            CcPdfView1.Margin = new Padding(4, 3, 4, 3);
            CcPdfView1.Name = "CcPdfView1";
            CcPdfView1.Size = new Size(1896, 927);
            CcPdfView1.TabIndex = 2;
            // 
            // CcPanelTop
            // 
            CcPanelTop.Controls.Add(CcButtonUpdate);
            CcPanelTop.Dock = DockStyle.Fill;
            CcPanelTop.Location = new Point(3, 27);
            CcPanelTop.Name = "CcPanelTop";
            CcPanelTop.Size = new Size(1898, 54);
            CcPanelTop.TabIndex = 3;
            // 
            // CcButtonUpdate
            // 
            CcButtonUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CcButtonUpdate.ForeColor = SystemColors.ControlText;
            CcButtonUpdate.Location = new Point(1673, 10);
            CcButtonUpdate.Name = "CcButtonUpdate";
            CcButtonUpdate.SetTextDirectionVertical = "";
            CcButtonUpdate.Size = new Size(180, 30);
            CcButtonUpdate.TabIndex = 1;
            CcButtonUpdate.Text = "UPDATE";
            CcButtonUpdate.UseVisualStyleBackColor = true;
            CcButtonUpdate.Click += CcButtonUpdate_Click;
            // 
            // PdfControlView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(CcTableLayoutPanelBase);
            MainMenuStrip = CcMenuStrip1;
            Name = "PdfControlView";
            Text = "PdfView";
            CcTableLayoutPanelBase.ResumeLayout(false);
            CcTableLayoutPanelBase.PerformLayout();
            CcPanelTop.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private CcControl.CcTableLayoutPanel CcTableLayoutPanelBase;
        private CcControl.CcMenuStrip CcMenuStrip1;
        private CcControl.CcStatusStrip CcStatusStrip1;
        private CcControl.CcPdfView CcPdfView1;
        private CcControl.CcPanel CcPanelTop;
        private CcControl.CcButton CcButtonUpdate;
    }
}
