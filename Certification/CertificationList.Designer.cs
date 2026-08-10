namespace Certification {
    partial class CertificationList {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CertificationList));
            CcTableLayoutPanelBase = new CcControl.CcTableLayoutPanel();
            MenuStripEx1 = new CcControl.CcMenuStrip();
            StatusStripEx1 = new CcControl.CcStatusStrip();
            PanelExTop = new CcControl.CcPanel();
            ButtonExUpdate = new CcControl.CcButton();
            labelEx2 = new CcControl.CcLabel();
            ComboBoxExPrinterName = new CcControl.CcComboBox();
            SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("CcTableLayoutPanelBase.Controls"));
            SheetViewList = SpreadList.GetSheet(0);
            CcTableLayoutPanelBase.SuspendLayout();
            PanelExTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SpreadList).BeginInit();
            SuspendLayout();
            // 
            // CcTableLayoutPanelBase
            // 
            CcTableLayoutPanelBase.ColumnCount = 1;
            CcTableLayoutPanelBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            CcTableLayoutPanelBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            CcTableLayoutPanelBase.Controls.Add(MenuStripEx1, 0, 0);
            CcTableLayoutPanelBase.Controls.Add(StatusStripEx1, 0, 3);
            CcTableLayoutPanelBase.Controls.Add(PanelExTop, 0, 1);
            CcTableLayoutPanelBase.Controls.Add(SpreadList, 0, 2);
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
            // MenuStripEx1
            // 
            MenuStripEx1.Location = new Point(0, 0);
            MenuStripEx1.Name = "MenuStripEx1";
            MenuStripEx1.Size = new Size(1904, 24);
            MenuStripEx1.TabIndex = 0;
            MenuStripEx1.Text = "menuStripEx1";
            MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // StatusStripEx1
            // 
            StatusStripEx1.Location = new Point(0, 1019);
            StatusStripEx1.Name = "StatusStripEx1";
            StatusStripEx1.Size = new Size(1904, 22);
            StatusStripEx1.TabIndex = 1;
            StatusStripEx1.Text = "statusStripEx1";
            // 
            // PanelExTop
            // 
            PanelExTop.Controls.Add(ButtonExUpdate);
            PanelExTop.Controls.Add(labelEx2);
            PanelExTop.Controls.Add(ComboBoxExPrinterName);
            PanelExTop.Dock = DockStyle.Fill;
            PanelExTop.Location = new Point(3, 27);
            PanelExTop.Name = "PanelExTop";
            PanelExTop.Size = new Size(1898, 54);
            PanelExTop.TabIndex = 2;
            // 
            // ButtonExUpdate
            // 
            ButtonExUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ButtonExUpdate.Font = new Font("Yu Gothic UI", 9.75F);
            ButtonExUpdate.ForeColor = SystemColors.ControlText;
            ButtonExUpdate.Location = new Point(1688, 12);
            ButtonExUpdate.Name = "ButtonExUpdate";
            ButtonExUpdate.SetTextDirectionVertical = null;
            ButtonExUpdate.Size = new Size(160, 32);
            ButtonExUpdate.TabIndex = 12;
            ButtonExUpdate.Text = "最　新　化";
            ButtonExUpdate.UseVisualStyleBackColor = true;
            ButtonExUpdate.Click += ButtonExUpdate_Click;
            // 
            // labelEx2
            // 
            labelEx2.AutoSize = true;
            labelEx2.Location = new Point(28, 20);
            labelEx2.Name = "labelEx2";
            labelEx2.Size = new Size(43, 15);
            labelEx2.TabIndex = 11;
            labelEx2.Text = "出力先";
            // 
            // ComboBoxExPrinterName
            // 
            ComboBoxExPrinterName.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxExPrinterName.FormattingEnabled = true;
            ComboBoxExPrinterName.Location = new Point(76, 16);
            ComboBoxExPrinterName.Name = "ComboBoxExPrinterName";
            ComboBoxExPrinterName.Size = new Size(212, 23);
            ComboBoxExPrinterName.TabIndex = 10;
            // 
            // SpreadList
            // 
            SpreadList.AccessibleDescription = "SpreadList, Sheet1, Row 0, Column 0";
            SpreadList.Dock = DockStyle.Fill;
            SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F);
            SpreadList.Location = new Point(3, 87);
            SpreadList.Name = "SpreadList";
            SpreadList.Size = new Size(1898, 927);
            SpreadList.TabIndex = 3;
            SpreadList.CellDoubleClick += SpreadList_CellDoubleClick;
            // 
            // CertificationList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(CcTableLayoutPanelBase);
            MainMenuStrip = MenuStripEx1;
            Name = "CertificationList";
            Text = "CertificationList";
            FormClosing += CertificationList_FormClosing;
            CcTableLayoutPanelBase.ResumeLayout(false);
            CcTableLayoutPanelBase.PerformLayout();
            PanelExTop.ResumeLayout(false);
            PanelExTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SpreadList).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private CcControl.CcTableLayoutPanel CcTableLayoutPanelBase;
        private CcControl.CcMenuStrip MenuStripEx1;
        private CcControl.CcStatusStrip StatusStripEx1;
        private CcControl.CcPanel PanelExTop;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private CcControl.CcLabel labelEx2;
        private CcControl.CcComboBox ComboBoxExPrinterName;
        private CcControl.CcButton ButtonExUpdate;
        private FarPoint.Win.Spread.SheetView SheetViewList;
    }
}
