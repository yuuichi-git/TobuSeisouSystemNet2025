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
            MenuStripEx1 = new CcControl.CcMenuStrip();
            StatusStripEx1 = new CcControl.CcStatusStrip();
            PanelExTop = new CcControl.CcPanel();
            ccLabel1 = new CcControl.CcLabel();
            ComboBoxExPrinterName = new CcControl.CcComboBox();
            labelEx2 = new CcControl.CcLabel();
            ComboBoxExManagedSpace = new CcControl.CcComboBox();
            ButtonExUpdate = new CcControl.CcButton();
            labelEx1 = new CcControl.CcLabel();
            DateTimePickerExOperationDate = new CcControl.CcDateTime();
            SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls"));
            SheetViewList = SpreadList.GetSheet(0);
            PrintDocument1 = new System.Drawing.Printing.PrintDocument();
            TableLayoutPanelExBase.SuspendLayout();
            PanelExTop.SuspendLayout();
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
            TableLayoutPanelExBase.Size = new Size(1354, 1041);
            TableLayoutPanelExBase.TabIndex = 0;
            // 
            // MenuStripEx1
            // 
            MenuStripEx1.Location = new Point(0, 0);
            MenuStripEx1.Name = "MenuStripEx1";
            MenuStripEx1.Size = new Size(1354, 24);
            MenuStripEx1.TabIndex = 0;
            MenuStripEx1.Text = "menuStripEx1";
            MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // StatusStripEx1
            // 
            StatusStripEx1.Location = new Point(0, 1019);
            StatusStripEx1.Name = "StatusStripEx1";
            StatusStripEx1.Size = new Size(1354, 22);
            StatusStripEx1.SizingGrip = false;
            StatusStripEx1.TabIndex = 1;
            StatusStripEx1.Text = "statusStripEx1";
            // 
            // PanelExTop
            // 
            PanelExTop.Controls.Add(ccLabel1);
            PanelExTop.Controls.Add(ComboBoxExPrinterName);
            PanelExTop.Controls.Add(labelEx2);
            PanelExTop.Controls.Add(ComboBoxExManagedSpace);
            PanelExTop.Controls.Add(ButtonExUpdate);
            PanelExTop.Controls.Add(labelEx1);
            PanelExTop.Controls.Add(DateTimePickerExOperationDate);
            PanelExTop.Dock = DockStyle.Fill;
            PanelExTop.Location = new Point(3, 27);
            PanelExTop.Name = "PanelExTop";
            PanelExTop.Size = new Size(1348, 54);
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
            // ComboBoxExPrinterName
            // 
            ComboBoxExPrinterName.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxExPrinterName.FormattingEnabled = true;
            ComboBoxExPrinterName.Location = new Point(652, 16);
            ComboBoxExPrinterName.Name = "ComboBoxExPrinterName";
            ComboBoxExPrinterName.Size = new Size(212, 23);
            ComboBoxExPrinterName.TabIndex = 10;
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
            // ComboBoxExManagedSpace
            // 
            ComboBoxExManagedSpace.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxExManagedSpace.FormattingEnabled = true;
            ComboBoxExManagedSpace.Items.AddRange(new object[] { "本社営業所", "三郷車庫" });
            ComboBoxExManagedSpace.Location = new Point(396, 16);
            ComboBoxExManagedSpace.Name = "ComboBoxExManagedSpace";
            ComboBoxExManagedSpace.Size = new Size(140, 23);
            ComboBoxExManagedSpace.TabIndex = 3;
            // 
            // ButtonExUpdate
            // 
            ButtonExUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ButtonExUpdate.Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            ButtonExUpdate.ForeColor = SystemColors.ControlText;
            ButtonExUpdate.Location = new Point(1132, 12);
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
            // DateTimePickerExOperationDate
            // 
            DateTimePickerExOperationDate.CultureFlag = false;
            DateTimePickerExOperationDate.CustomFormat = " 明治33年01月01日(月曜日)";
            DateTimePickerExOperationDate.Format = DateTimePickerFormat.Custom;
            DateTimePickerExOperationDate.Location = new Point(84, 16);
            DateTimePickerExOperationDate.Name = "DateTimePickerExOperationDate";
            DateTimePickerExOperationDate.Size = new Size(182, 23);
            DateTimePickerExOperationDate.TabIndex = 0;
            DateTimePickerExOperationDate.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            DateTimePickerExOperationDate.ValueChanged += DateTimePickerExOperationDate_ValueChanged;
            // 
            // SpreadList
            // 
            SpreadList.AccessibleDescription = "Book1, Sheet1, Row 0, Column 0";
            SpreadList.Dock = DockStyle.Fill;
            SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F);
            SpreadList.Location = new Point(3, 87);
            SpreadList.Name = "SpreadList";
            SpreadList.Size = new Size(1348, 927);
            SpreadList.TabIndex = 3;
            // 
            // RollCallRecordSheet
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1354, 1041);
            Controls.Add(TableLayoutPanelExBase);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = MenuStripEx1;
            Name = "RollCallRecordSheet";
            Text = "RollCallRecordSheet";
            FormClosing += RollCallRecordSheet_FormClosing;
            TableLayoutPanelExBase.ResumeLayout(false);
            TableLayoutPanelExBase.PerformLayout();
            PanelExTop.ResumeLayout(false);
            PanelExTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SpreadList).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private CcControl.CcTableLayoutPanel TableLayoutPanelExBase;
        private CcControl.CcMenuStrip MenuStripEx1;
        private CcControl.CcStatusStrip StatusStripEx1;
        private CcControl.CcPanel PanelExTop;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private CcControl.CcLabel labelEx1;
        private CcControl.CcDateTime DateTimePickerExOperationDate;
        private CcControl.CcButton ButtonExUpdate;
        private CcControl.CcLabel labelEx2;
        private CcControl.CcComboBox ComboBoxExManagedSpace;
        private System.Drawing.Printing.PrintDocument PrintDocument1;
        private FarPoint.Win.Spread.SheetView SheetViewList;
        private CcControl.CcLabel ccLabel1;
        private CcControl.CcComboBox ComboBoxExPrinterName;
    }
}