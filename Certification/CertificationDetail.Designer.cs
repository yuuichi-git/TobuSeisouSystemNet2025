namespace Certification {
    partial class CertificationDetail {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            TableLayoutPanelExBase = new CcControl.CcTableLayoutPanel();
            CcStatusStrip1 = new CcControl.CcStatusStrip();
            tableLayoutPanelEx1 = new CcControl.CcTableLayoutPanel();
            CcPdfView1 = new CcControl.CcPdfView();
            CcContextMenuStrip1 = new CcControl.CcContextMenuStrip();
            ToolStripMenuItemOpen = new ToolStripMenuItem();
            ToolStripMenuItemPaste = new ToolStripMenuItem();
            ToolStripMenuItemDelete = new ToolStripMenuItem();
            CcPdfView2 = new CcControl.CcPdfView();
            CcMenuStrip1 = new CcControl.CcMenuStrip();
            CcPanelTop = new CcControl.CcPanel();
            CcButtonUpdate = new CcControl.CcButton();
            TableLayoutPanelExBase.SuspendLayout();
            tableLayoutPanelEx1.SuspendLayout();
            CcContextMenuStrip1.SuspendLayout();
            CcPanelTop.SuspendLayout();
            SuspendLayout();
            // 
            // TableLayoutPanelExBase
            // 
            TableLayoutPanelExBase.ColumnCount = 1;
            TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            TableLayoutPanelExBase.Controls.Add(CcStatusStrip1, 0, 3);
            TableLayoutPanelExBase.Controls.Add(tableLayoutPanelEx1, 0, 2);
            TableLayoutPanelExBase.Controls.Add(CcMenuStrip1, 0, 0);
            TableLayoutPanelExBase.Controls.Add(CcPanelTop, 0, 1);
            TableLayoutPanelExBase.Dock = DockStyle.Fill;
            TableLayoutPanelExBase.Location = new Point(0, 0);
            TableLayoutPanelExBase.Name = "TableLayoutPanelExBase";
            TableLayoutPanelExBase.RowCount = 4;
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            TableLayoutPanelExBase.Size = new Size(1219, 1041);
            TableLayoutPanelExBase.TabIndex = 0;
            // 
            // CcStatusStrip1
            // 
            CcStatusStrip1.Location = new Point(0, 1019);
            CcStatusStrip1.Name = "CcStatusStrip1";
            CcStatusStrip1.Size = new Size(1219, 22);
            CcStatusStrip1.TabIndex = 2;
            CcStatusStrip1.Text = "statusStripEx1";
            // 
            // tableLayoutPanelEx1
            // 
            tableLayoutPanelEx1.ColumnCount = 2;
            tableLayoutPanelEx1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelEx1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelEx1.Controls.Add(CcPdfView1, 0, 0);
            tableLayoutPanelEx1.Controls.Add(CcPdfView2, 1, 0);
            tableLayoutPanelEx1.Dock = DockStyle.Fill;
            tableLayoutPanelEx1.Location = new Point(3, 87);
            tableLayoutPanelEx1.Name = "tableLayoutPanelEx1";
            tableLayoutPanelEx1.RowCount = 1;
            tableLayoutPanelEx1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelEx1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanelEx1.Size = new Size(1213, 927);
            tableLayoutPanelEx1.TabIndex = 0;
            // 
            // CcPdfView1
            // 
            CcPdfView1.ContextMenuStrip = CcContextMenuStrip1;
            CcPdfView1.Dock = DockStyle.Fill;
            CcPdfView1.Location = new Point(4, 3);
            CcPdfView1.Margin = new Padding(4, 3, 4, 3);
            CcPdfView1.MemoryStream = null;
            CcPdfView1.Name = "CcPdfView1";
            CcPdfView1.Padding = new Padding(4);
            CcPdfView1.PdfDocument = null;
            CcPdfView1.Size = new Size(598, 921);
            CcPdfView1.TabIndex = 0;
            CcPdfView1.Tag = "0";
            CcPdfView1.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitBest;
            // 
            // CcContextMenuStrip1
            // 
            CcContextMenuStrip1.Items.AddRange(new ToolStripItem[] { ToolStripMenuItemOpen, ToolStripMenuItemPaste, ToolStripMenuItemDelete });
            CcContextMenuStrip1.Name = "ContextMenuStripEx1";
            CcContextMenuStrip1.Size = new Size(162, 70);
            CcContextMenuStrip1.ItemClicked += CcContextMenuStrip1_ItemClicked;
            // 
            // ToolStripMenuItemOpen
            // 
            ToolStripMenuItemOpen.Name = "ToolStripMenuItemOpen";
            ToolStripMenuItemOpen.Size = new Size(161, 22);
            ToolStripMenuItemOpen.Text = "Open(PDF)";
            // 
            // ToolStripMenuItemPaste
            // 
            ToolStripMenuItemPaste.Name = "ToolStripMenuItemPaste";
            ToolStripMenuItemPaste.Size = new Size(161, 22);
            ToolStripMenuItemPaste.Text = "Paste(ClipBoard)";
            // 
            // ToolStripMenuItemDelete
            // 
            ToolStripMenuItemDelete.Name = "ToolStripMenuItemDelete";
            ToolStripMenuItemDelete.Size = new Size(161, 22);
            ToolStripMenuItemDelete.Text = "Delete";
            // 
            // CcPdfView2
            // 
            CcPdfView2.ContextMenuStrip = CcContextMenuStrip1;
            CcPdfView2.Dock = DockStyle.Fill;
            CcPdfView2.Location = new Point(610, 3);
            CcPdfView2.Margin = new Padding(4, 3, 4, 3);
            CcPdfView2.MemoryStream = null;
            CcPdfView2.Name = "CcPdfView2";
            CcPdfView2.Padding = new Padding(4);
            CcPdfView2.PdfDocument = null;
            CcPdfView2.Size = new Size(599, 921);
            CcPdfView2.TabIndex = 1;
            CcPdfView2.Tag = "1";
            CcPdfView2.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitBest;
            // 
            // CcMenuStrip1
            // 
            CcMenuStrip1.Location = new Point(0, 0);
            CcMenuStrip1.Name = "CcMenuStrip1";
            CcMenuStrip1.Size = new Size(1219, 24);
            CcMenuStrip1.TabIndex = 1;
            CcMenuStrip1.Text = "menuStripEx1";
            CcMenuStrip1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // CcPanelTop
            // 
            CcPanelTop.Controls.Add(CcButtonUpdate);
            CcPanelTop.Dock = DockStyle.Fill;
            CcPanelTop.Location = new Point(3, 27);
            CcPanelTop.Name = "CcPanelTop";
            CcPanelTop.Size = new Size(1213, 54);
            CcPanelTop.TabIndex = 3;
            // 
            // CcButtonUpdate
            // 
            CcButtonUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CcButtonUpdate.Font = new Font("Yu Gothic UI", 9.75F);
            CcButtonUpdate.ForeColor = SystemColors.ControlText;
            CcButtonUpdate.Location = new Point(1005, 10);
            CcButtonUpdate.Name = "CcButtonUpdate";
            CcButtonUpdate.SetTextDirectionVertical = null;
            CcButtonUpdate.Size = new Size(160, 32);
            CcButtonUpdate.TabIndex = 13;
            CcButtonUpdate.Text = "UPDATE";
            CcButtonUpdate.UseVisualStyleBackColor = true;
            CcButtonUpdate.Click += ButtonExUpdate_Click;
            // 
            // CertificationDetail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1219, 1041);
            Controls.Add(TableLayoutPanelExBase);
            MainMenuStrip = CcMenuStrip1;
            Name = "CertificationDetail";
            Text = "CertificationDetail";
            FormClosing += CertificationDetail_FormClosing;
            TableLayoutPanelExBase.ResumeLayout(false);
            TableLayoutPanelExBase.PerformLayout();
            tableLayoutPanelEx1.ResumeLayout(false);
            CcContextMenuStrip1.ResumeLayout(false);
            CcPanelTop.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private CcControl.CcTableLayoutPanel TableLayoutPanelExBase;
        private CcControl.CcTableLayoutPanel tableLayoutPanelEx1;
        private CcControl.CcStatusStrip CcStatusStrip1;
        private CcControl.CcMenuStrip CcMenuStrip1;
        private CcControl.CcPanel CcPanelTop;
        private CcControl.CcButton CcButtonUpdate;
        private CcControl.CcContextMenuStrip CcContextMenuStrip1;
        private ToolStripMenuItem ToolStripMenuItemPaste;
        private ToolStripMenuItem ToolStripMenuItemDelete;
        private ToolStripMenuItem ToolStripMenuItemOpen;
        private CcControl.CcPdfView CcPdfView1;
        private CcControl.CcPdfView CcPdfView2;
    }
}