namespace LegalTwelveItem {
    partial class LegalTwelveItemList {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LegalTwelveItemList));
            TableLayoutPanelExBase = new CcControl.CcTableLayoutPanel();
            MenuStripEx1 = new CcControl.CcMenuStrip();
            StatusStripEx1 = new CcControl.CcStatusStrip();
            PanelExTop = new CcControl.CcPanel();
            labelEx2 = new CcControl.CcLabel();
            ComboBoxExPrinterName = new CcControl.CcComboBox();
            labelEx1 = new CcControl.CcLabel();
            ButtonExUpdate = new CcControl.CcButton();
            NumericUpDownExFiscalYear = new CcControl.CcNumericUpDown();
            SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls"));
            SheetViewList = SpreadList.GetSheet(0);
            TableLayoutPanelExBase.SuspendLayout();
            PanelExTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumericUpDownExFiscalYear).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SpreadList).BeginInit();
            SuspendLayout();
            // 
            // TableLayoutPanelExBase
            // 
            TableLayoutPanelExBase.ColumnCount = 1;
            TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            TableLayoutPanelExBase.Controls.Add(MenuStripEx1, 0, 0);
            TableLayoutPanelExBase.Controls.Add(StatusStripEx1, 0, 3);
            TableLayoutPanelExBase.Controls.Add(PanelExTop, 0, 1);
            TableLayoutPanelExBase.Controls.Add(SpreadList, 0, 2);
            TableLayoutPanelExBase.Dock = DockStyle.Fill;
            TableLayoutPanelExBase.Location = new Point(0, 0);
            TableLayoutPanelExBase.Name = "TableLayoutPanelExBase";
            TableLayoutPanelExBase.RowCount = 4;
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            TableLayoutPanelExBase.Size = new Size(1231, 1041);
            TableLayoutPanelExBase.TabIndex = 0;
            // 
            // MenuStripEx1
            // 
            MenuStripEx1.Location = new Point(0, 0);
            MenuStripEx1.Name = "MenuStripEx1";
            MenuStripEx1.Size = new Size(1231, 24);
            MenuStripEx1.TabIndex = 0;
            MenuStripEx1.Text = "menuStripEx1";
            MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // StatusStripEx1
            // 
            StatusStripEx1.Location = new Point(0, 1019);
            StatusStripEx1.Name = "StatusStripEx1";
            StatusStripEx1.Size = new Size(1231, 22);
            StatusStripEx1.SizingGrip = false;
            StatusStripEx1.TabIndex = 1;
            StatusStripEx1.Text = "statusStripEx1";
            // 
            // PanelExTop
            // 
            PanelExTop.Controls.Add(labelEx2);
            PanelExTop.Controls.Add(ComboBoxExPrinterName);
            PanelExTop.Controls.Add(labelEx1);
            PanelExTop.Controls.Add(ButtonExUpdate);
            PanelExTop.Controls.Add(NumericUpDownExFiscalYear);
            PanelExTop.Dock = DockStyle.Fill;
            PanelExTop.Location = new Point(3, 27);
            PanelExTop.Name = "PanelExTop";
            PanelExTop.Size = new Size(1225, 54);
            PanelExTop.TabIndex = 2;
            // 
            // labelEx2
            // 
            labelEx2.AutoSize = true;
            labelEx2.Location = new Point(176, 20);
            labelEx2.Name = "labelEx2";
            labelEx2.Size = new Size(43, 15);
            labelEx2.TabIndex = 9;
            labelEx2.Text = "出力先";
            // 
            // ComboBoxExPrinterName
            // 
            ComboBoxExPrinterName.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxExPrinterName.FormattingEnabled = true;
            ComboBoxExPrinterName.Location = new Point(224, 16);
            ComboBoxExPrinterName.Name = "ComboBoxExPrinterName";
            ComboBoxExPrinterName.Size = new Size(212, 23);
            ComboBoxExPrinterName.TabIndex = 8;
            // 
            // labelEx1
            // 
            labelEx1.AutoSize = true;
            labelEx1.Location = new Point(36, 20);
            labelEx1.Name = "labelEx1";
            labelEx1.Size = new Size(55, 15);
            labelEx1.TabIndex = 7;
            labelEx1.Text = "対象年度";
            // 
            // ButtonExUpdate
            // 
            ButtonExUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ButtonExUpdate.Font = new Font("Yu Gothic UI", 9.75F);
            ButtonExUpdate.ForeColor = SystemColors.ControlText;
            ButtonExUpdate.Location = new Point(1023, 12);
            ButtonExUpdate.Name = "ButtonExUpdate";
            ButtonExUpdate.SetTextDirectionVertical = null;
            ButtonExUpdate.Size = new Size(160, 32);
            ButtonExUpdate.TabIndex = 6;
            ButtonExUpdate.Text = "最　新　化";
            ButtonExUpdate.UseVisualStyleBackColor = true;
            ButtonExUpdate.Click += ButtonExUpdate_Click;
            // 
            // NumericUpDownExFiscalYear
            // 
            NumericUpDownExFiscalYear.Location = new Point(96, 16);
            NumericUpDownExFiscalYear.Maximum = new decimal(new int[] { 2029, 0, 0, 0 });
            NumericUpDownExFiscalYear.Minimum = new decimal(new int[] { 2024, 0, 0, 0 });
            NumericUpDownExFiscalYear.Name = "NumericUpDownExFiscalYear";
            NumericUpDownExFiscalYear.Size = new Size(56, 23);
            NumericUpDownExFiscalYear.TabIndex = 0;
            NumericUpDownExFiscalYear.TextAlign = HorizontalAlignment.Right;
            NumericUpDownExFiscalYear.Value = new decimal(new int[] { 2024, 0, 0, 0 });
            // 
            // SpreadList
            // 
            SpreadList.AccessibleDescription = "SpreadList, Sheet1, Row 0, Column 0";
            SpreadList.Dock = DockStyle.Fill;
            SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F);
            SpreadList.Location = new Point(3, 87);
            SpreadList.Name = "SpreadList";
            SpreadList.Size = new Size(1225, 927);
            SpreadList.TabIndex = 3;
            SpreadList.CellDoubleClick += SpreadList_CellDoubleClick;
            // 
            // LegalTwelveItemList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1231, 1041);
            Controls.Add(TableLayoutPanelExBase);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = MenuStripEx1;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LegalTwelveItemList";
            Text = "LegalTwelveItemList";
            FormClosing += EmploymentAgreementList_FormClosing;
            TableLayoutPanelExBase.ResumeLayout(false);
            TableLayoutPanelExBase.PerformLayout();
            PanelExTop.ResumeLayout(false);
            PanelExTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NumericUpDownExFiscalYear).EndInit();
            ((System.ComponentModel.ISupportInitialize)SpreadList).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private CcControl.CcTableLayoutPanel TableLayoutPanelExBase;
        private CcControl.CcMenuStrip MenuStripEx1;
        private CcControl.CcStatusStrip StatusStripEx1;
        private CcControl.CcPanel PanelExTop;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private CcControl.CcNumericUpDown NumericUpDownExFiscalYear;
        private CcControl.CcLabel labelEx1;
        private CcControl.CcButton ButtonExUpdate;
        private CcControl.CcLabel labelEx2;
        private CcControl.CcComboBox ComboBoxExPrinterName;
        private FarPoint.Win.Spread.SheetView SheetViewList;
    }
}
