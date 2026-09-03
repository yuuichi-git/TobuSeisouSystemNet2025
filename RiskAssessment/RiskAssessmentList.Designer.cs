namespace RiskAssessment {
    partial class RiskAssessmentList {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RiskAssessmentList));
            CcTableLayoutPanelBase = new CcControl.CcTableLayoutPanel();
            CcStatusStrip1 = new CcControl.CcStatusStrip();
            SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("CcTableLayoutPanelBase.Controls"));
            SheetViewList = SpreadList.GetSheet(0);
            CcMenuStrip1 = new CcControl.CcMenuStrip();
            CcPanelUp = new CcControl.CcPanel();
            labelEx2 = new CcControl.CcLabel();
            CcComboBoxPrinterName = new CcControl.CcComboBox();
            labelEx1 = new CcControl.CcLabel();
            CcButtonUpdate = new CcControl.CcButton();
            NumericUpDownExFiscalYear = new CcControl.CcNumericUpDown();
            CcTableLayoutPanelBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SpreadList).BeginInit();
            CcPanelUp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumericUpDownExFiscalYear).BeginInit();
            SuspendLayout();
            // 
            // CcTableLayoutPanelBase
            // 
            CcTableLayoutPanelBase.ColumnCount = 3;
            CcTableLayoutPanelBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 450F));
            CcTableLayoutPanelBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            CcTableLayoutPanelBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 450F));
            CcTableLayoutPanelBase.Controls.Add(CcStatusStrip1, 0, 3);
            CcTableLayoutPanelBase.Controls.Add(SpreadList, 1, 2);
            CcTableLayoutPanelBase.Controls.Add(CcMenuStrip1, 0, 0);
            CcTableLayoutPanelBase.Controls.Add(CcPanelUp, 0, 1);
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
            // CcStatusStrip1
            // 
            CcTableLayoutPanelBase.SetColumnSpan(CcStatusStrip1, 3);
            CcStatusStrip1.Location = new Point(0, 1019);
            CcStatusStrip1.Name = "CcStatusStrip1";
            CcStatusStrip1.Size = new Size(1904, 22);
            CcStatusStrip1.SizingGrip = false;
            CcStatusStrip1.TabIndex = 2;
            CcStatusStrip1.Text = "ccStatusStrip1";
            // 
            // SpreadList
            // 
            SpreadList.AccessibleDescription = "SpreadList, Sheet1, Row 0, Column 0";
            SpreadList.Dock = DockStyle.Fill;
            SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F);
            SpreadList.Location = new Point(453, 87);
            SpreadList.Name = "SpreadList";
            SpreadList.Size = new Size(998, 927);
            SpreadList.TabIndex = 0;
            SpreadList.CellDoubleClick += SpreadList_CellDoubleClick;
            // 
            // CcMenuStrip1
            // 
            CcTableLayoutPanelBase.SetColumnSpan(CcMenuStrip1, 3);
            CcMenuStrip1.Location = new Point(0, 0);
            CcMenuStrip1.Name = "CcMenuStrip1";
            CcMenuStrip1.Size = new Size(1904, 24);
            CcMenuStrip1.TabIndex = 1;
            CcMenuStrip1.Text = "ccMenuStrip1";
            CcMenuStrip1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // CcPanelUp
            // 
            CcTableLayoutPanelBase.SetColumnSpan(CcPanelUp, 3);
            CcPanelUp.Controls.Add(labelEx2);
            CcPanelUp.Controls.Add(CcComboBoxPrinterName);
            CcPanelUp.Controls.Add(labelEx1);
            CcPanelUp.Controls.Add(CcButtonUpdate);
            CcPanelUp.Controls.Add(NumericUpDownExFiscalYear);
            CcPanelUp.Dock = DockStyle.Fill;
            CcPanelUp.Location = new Point(3, 27);
            CcPanelUp.Name = "CcPanelUp";
            CcPanelUp.Size = new Size(1898, 54);
            CcPanelUp.TabIndex = 3;
            // 
            // labelEx2
            // 
            labelEx2.AutoSize = true;
            labelEx2.Location = new Point(179, 20);
            labelEx2.Name = "labelEx2";
            labelEx2.Size = new Size(43, 15);
            labelEx2.TabIndex = 14;
            labelEx2.Text = "出力先";
            // 
            // CcComboBoxPrinterName
            // 
            CcComboBoxPrinterName.DropDownStyle = ComboBoxStyle.DropDownList;
            CcComboBoxPrinterName.FormattingEnabled = true;
            CcComboBoxPrinterName.Location = new Point(227, 16);
            CcComboBoxPrinterName.Name = "CcComboBoxPrinterName";
            CcComboBoxPrinterName.Size = new Size(212, 23);
            CcComboBoxPrinterName.TabIndex = 13;
            // 
            // labelEx1
            // 
            labelEx1.AutoSize = true;
            labelEx1.Location = new Point(39, 20);
            labelEx1.Name = "labelEx1";
            labelEx1.Size = new Size(55, 15);
            labelEx1.TabIndex = 12;
            labelEx1.Text = "対象年度";
            // 
            // CcButtonUpdate
            // 
            CcButtonUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CcButtonUpdate.Font = new Font("Yu Gothic UI", 9.75F);
            CcButtonUpdate.ForeColor = SystemColors.ControlText;
            CcButtonUpdate.Location = new Point(1699, 12);
            CcButtonUpdate.Name = "CcButtonUpdate";
            CcButtonUpdate.SetTextDirectionVertical = null;
            CcButtonUpdate.Size = new Size(160, 32);
            CcButtonUpdate.TabIndex = 11;
            CcButtonUpdate.Text = "最　新　化";
            CcButtonUpdate.UseVisualStyleBackColor = true;
            CcButtonUpdate.Click += CcButtonUpdate_Click;
            // 
            // NumericUpDownExFiscalYear
            // 
            NumericUpDownExFiscalYear.Location = new Point(99, 16);
            NumericUpDownExFiscalYear.Maximum = new decimal(new int[] { 2029, 0, 0, 0 });
            NumericUpDownExFiscalYear.Minimum = new decimal(new int[] { 2024, 0, 0, 0 });
            NumericUpDownExFiscalYear.Name = "NumericUpDownExFiscalYear";
            NumericUpDownExFiscalYear.Size = new Size(56, 23);
            NumericUpDownExFiscalYear.TabIndex = 10;
            NumericUpDownExFiscalYear.TextAlign = HorizontalAlignment.Right;
            NumericUpDownExFiscalYear.Value = new decimal(new int[] { 2024, 0, 0, 0 });
            // 
            // RiskAssessmentList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(CcTableLayoutPanelBase);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = CcMenuStrip1;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RiskAssessmentList";
            Text = "RiskAssessmentList";
            FormClosing += RiskAssessmentList_FormClosing;
            CcTableLayoutPanelBase.ResumeLayout(false);
            CcTableLayoutPanelBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SpreadList).EndInit();
            CcPanelUp.ResumeLayout(false);
            CcPanelUp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NumericUpDownExFiscalYear).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private CcControl.CcTableLayoutPanel CcTableLayoutPanelBase;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private CcControl.CcStatusStrip CcStatusStrip1;
        private CcControl.CcMenuStrip CcMenuStrip1;
        private CcControl.CcPanel CcPanelUp;
        private CcControl.CcLabel labelEx2;
        private CcControl.CcComboBox CcComboBoxPrinterName;
        private CcControl.CcLabel labelEx1;
        private CcControl.CcButton CcButtonUpdate;
        private CcControl.CcNumericUpDown NumericUpDownExFiscalYear;
        private FarPoint.Win.Spread.SheetView SheetViewList;
    }
}
