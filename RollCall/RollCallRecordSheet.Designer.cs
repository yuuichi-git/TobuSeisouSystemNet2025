namespace RollCall {
    partial class RollCallRecordSheet {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RollCallRecordSheet));
            TableLayoutPanelExBase = new CcControl.CcTableLayoutPanel();
            CcMenuStrip1 = new CcControl.CcMenuStrip();
            CcStatusStrip1 = new CcControl.CcStatusStrip();
            PanelExTop = new CcControl.CcPanel();
            ccLabel1 = new CcControl.CcLabel();
            CcComboBoxPrinterName = new CcControl.CcComboBox();
            labelEx2 = new CcControl.CcLabel();
            CcComboBoxManagedSpace = new CcControl.CcComboBox();
            ButtonExUpdate = new CcControl.CcButton();
            labelEx1 = new CcControl.CcLabel();
            CcDateTimePickerOperationDate = new CcControl.CcDateTime();
            SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("resource1"));
            SheetViewList = SpreadList.GetSheet(0);
            CcPanelLeft = new CcControl.CcPanel();
            CcTextBox1 = new CcControl.CcTextBox();
            PrintDocument1 = new System.Drawing.Printing.PrintDocument();
            TableLayoutPanelExBase.SuspendLayout();
            PanelExTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SpreadList).BeginInit();
            CcPanelLeft.SuspendLayout();
            SuspendLayout();
            // 
            // TableLayoutPanelExBase
            // 
            TableLayoutPanelExBase.ColumnCount = 3;
            TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            TableLayoutPanelExBase.Controls.Add(CcMenuStrip1, 0, 0);
            TableLayoutPanelExBase.Controls.Add(CcStatusStrip1, 0, 3);
            TableLayoutPanelExBase.Controls.Add(PanelExTop, 0, 1);
            TableLayoutPanelExBase.Controls.Add(SpreadList, 1, 2);
            TableLayoutPanelExBase.Controls.Add(CcPanelLeft, 0, 2);
            TableLayoutPanelExBase.Dock = DockStyle.Fill;
            TableLayoutPanelExBase.Location = new Point(0, 0);
            TableLayoutPanelExBase.Name = "TableLayoutPanelExBase";
            TableLayoutPanelExBase.RowCount = 4;
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            TableLayoutPanelExBase.Size = new Size(1904, 1041);
            TableLayoutPanelExBase.TabIndex = 0;
            // 
            // CcMenuStrip1
            // 
            TableLayoutPanelExBase.SetColumnSpan(CcMenuStrip1, 3);
            CcMenuStrip1.Location = new Point(0, 0);
            CcMenuStrip1.Name = "CcMenuStrip1";
            CcMenuStrip1.Size = new Size(1904, 24);
            CcMenuStrip1.TabIndex = 0;
            CcMenuStrip1.Text = "menuStripEx1";
            CcMenuStrip1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // CcStatusStrip1
            // 
            TableLayoutPanelExBase.SetColumnSpan(CcStatusStrip1, 3);
            CcStatusStrip1.Location = new Point(0, 1019);
            CcStatusStrip1.Name = "CcStatusStrip1";
            CcStatusStrip1.Size = new Size(1904, 22);
            CcStatusStrip1.SizingGrip = false;
            CcStatusStrip1.TabIndex = 1;
            CcStatusStrip1.Text = "statusStripEx1";
            // 
            // PanelExTop
            // 
            TableLayoutPanelExBase.SetColumnSpan(PanelExTop, 3);
            PanelExTop.Controls.Add(ccLabel1);
            PanelExTop.Controls.Add(CcComboBoxPrinterName);
            PanelExTop.Controls.Add(labelEx2);
            PanelExTop.Controls.Add(CcComboBoxManagedSpace);
            PanelExTop.Controls.Add(ButtonExUpdate);
            PanelExTop.Controls.Add(labelEx1);
            PanelExTop.Controls.Add(CcDateTimePickerOperationDate);
            PanelExTop.Dock = DockStyle.Fill;
            PanelExTop.Location = new Point(3, 27);
            PanelExTop.Name = "PanelExTop";
            PanelExTop.Size = new Size(1898, 54);
            PanelExTop.TabIndex = 2;
            // 
            // ccLabel1
            // 
            ccLabel1.AutoSize = true;
            ccLabel1.Location = new Point(604, 20);
            ccLabel1.Name = "ccLabel1";
            ccLabel1.Size = new Size(43, 15);
            ccLabel1.TabIndex = 11;
            ccLabel1.Text = "出力先";
            // 
            // CcComboBoxPrinterName
            // 
            CcComboBoxPrinterName.DropDownStyle = ComboBoxStyle.DropDownList;
            CcComboBoxPrinterName.FormattingEnabled = true;
            CcComboBoxPrinterName.Location = new Point(652, 16);
            CcComboBoxPrinterName.Name = "CcComboBoxPrinterName";
            CcComboBoxPrinterName.Size = new Size(212, 23);
            CcComboBoxPrinterName.TabIndex = 10;
            // 
            // labelEx2
            // 
            labelEx2.AutoSize = true;
            labelEx2.Location = new Point(336, 20);
            labelEx2.Name = "labelEx2";
            labelEx2.Size = new Size(55, 15);
            labelEx2.TabIndex = 4;
            labelEx2.Text = "点呼場所";
            // 
            // CcComboBoxManagedSpace
            // 
            CcComboBoxManagedSpace.DropDownStyle = ComboBoxStyle.DropDownList;
            CcComboBoxManagedSpace.FormattingEnabled = true;
            CcComboBoxManagedSpace.Items.AddRange(new object[] { "本社営業所", "三郷車庫" });
            CcComboBoxManagedSpace.Location = new Point(396, 16);
            CcComboBoxManagedSpace.Name = "CcComboBoxManagedSpace";
            CcComboBoxManagedSpace.Size = new Size(140, 23);
            CcComboBoxManagedSpace.TabIndex = 3;
            // 
            // ButtonExUpdate
            // 
            ButtonExUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ButtonExUpdate.Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            ButtonExUpdate.ForeColor = SystemColors.ControlText;
            ButtonExUpdate.Location = new Point(1682, 12);
            ButtonExUpdate.Name = "ButtonExUpdate";
            ButtonExUpdate.SetTextDirectionVertical = "";
            ButtonExUpdate.Size = new Size(170, 32);
            ButtonExUpdate.TabIndex = 2;
            ButtonExUpdate.Text = "最　新　化";
            ButtonExUpdate.UseVisualStyleBackColor = true;
            ButtonExUpdate.Click += ButtonExUpdate_Click;
            // 
            // labelEx1
            // 
            labelEx1.AutoSize = true;
            labelEx1.Location = new Point(24, 20);
            labelEx1.Name = "labelEx1";
            labelEx1.Size = new Size(55, 15);
            labelEx1.TabIndex = 1;
            labelEx1.Text = "配車日付";
            // 
            // CcDateTimePickerOperationDate
            // 
            CcDateTimePickerOperationDate.CustomFormat = " 明治33年01月01日(月曜日)";
            CcDateTimePickerOperationDate.Format = DateTimePickerFormat.Custom;
            CcDateTimePickerOperationDate.Location = new Point(84, 16);
            CcDateTimePickerOperationDate.Name = "CcDateTimePickerOperationDate";
            CcDateTimePickerOperationDate.Size = new Size(182, 23);
            CcDateTimePickerOperationDate.TabIndex = 0;
            CcDateTimePickerOperationDate.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            CcDateTimePickerOperationDate.ValueChanged += CcComboBoxManagedSpace_ValueChanged;
            // 
            // SpreadList
            // 
            SpreadList.AccessibleDescription = "SpreadList, Sheet1, Row 0, Column 0";
            SpreadList.Dock = DockStyle.Fill;
            SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F);
            SpreadList.Location = new Point(253, 87);
            SpreadList.Name = "SpreadList";
            SpreadList.Size = new Size(1398, 927);
            SpreadList.TabIndex = 3;
            // 
            // CcPanelLeft
            // 
            CcPanelLeft.Controls.Add(CcTextBox1);
            CcPanelLeft.Dock = DockStyle.Fill;
            CcPanelLeft.Location = new Point(3, 87);
            CcPanelLeft.Name = "CcPanelLeft";
            CcPanelLeft.Size = new Size(244, 927);
            CcPanelLeft.TabIndex = 4;
            // 
            // CcTextBox1
            // 
            CcTextBox1.Location = new Point(4, 28);
            CcTextBox1.Multiline = true;
            CcTextBox1.Name = "CcTextBox1";
            CcTextBox1.Size = new Size(236, 348);
            CcTextBox1.TabIndex = 0;
            CcTextBox1.Text = "点呼記録簿に読込まれる条件\r\n\r\n①配車先ラベルが確定していること\r\n且つ、種別が雇上/区契/臨時/清掃工場であること\r\n\r\n②車両ラベルが確定していること\r\n\r\n③運転者ラベルが確定していること";
            // 
            // RollCallRecordSheet
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(TableLayoutPanelExBase);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = CcMenuStrip1;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RollCallRecordSheet";
            Text = "RollCallRecordSheet";
            FormClosing += RollCallRecordSheet_FormClosing;
            TableLayoutPanelExBase.ResumeLayout(false);
            TableLayoutPanelExBase.PerformLayout();
            PanelExTop.ResumeLayout(false);
            PanelExTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SpreadList).EndInit();
            CcPanelLeft.ResumeLayout(false);
            CcPanelLeft.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private CcControl.CcTableLayoutPanel TableLayoutPanelExBase;
        private CcControl.CcMenuStrip CcMenuStrip1;
        private CcControl.CcStatusStrip CcStatusStrip1;
        private CcControl.CcPanel PanelExTop;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private CcControl.CcLabel labelEx1;
        private CcControl.CcDateTime CcDateTimePickerOperationDate;
        private CcControl.CcButton ButtonExUpdate;
        private CcControl.CcLabel labelEx2;
        private CcControl.CcComboBox CcComboBoxManagedSpace;
        private System.Drawing.Printing.PrintDocument PrintDocument1;
        private CcControl.CcLabel ccLabel1;
        private CcControl.CcComboBox CcComboBoxPrinterName;
        private CcControl.CcPanel CcPanelLeft;
        private CcControl.CcTextBox CcTextBox1;
        private FarPoint.Win.Spread.SheetView SheetViewList;
    }
}