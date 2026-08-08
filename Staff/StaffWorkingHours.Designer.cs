namespace Staff {
    partial class StaffWorkingHours {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaffWorkingHours));
            CcTableLayoutPanelBase = new CcControl.CcTableLayoutPanel();
            CcMenuStrip1 = new CcControl.CcMenuStrip();
            CcStatusStrip1 = new CcControl.CcStatusStrip();
            PanelExUp = new CcControl.CcPanel();
            labelEx3 = new CcControl.CcLabel();
            CcComboBoxStaffDisplayName = new CcControl.CcComboBox();
            labelEx2 = new CcControl.CcLabel();
            labelEx1 = new CcControl.CcLabel();
            CcDateTimePickerOperationDate2 = new CcControl.CcDateTime();
            CcDateTimePickerOperationDate1 = new CcControl.CcDateTime();
            ButtonExUpdate = new CcControl.CcButton();
            SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("CcTableLayoutPanelBase.Controls"));
            SheetViewList = SpreadList.GetSheet(0);
            CcTableLayoutPanelBase.SuspendLayout();
            PanelExUp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SpreadList).BeginInit();
            SuspendLayout();
            // 
            // CcTableLayoutPanelBase
            // 
            CcTableLayoutPanelBase.ColumnCount = 3;
            CcTableLayoutPanelBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 500F));
            CcTableLayoutPanelBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            CcTableLayoutPanelBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 500F));
            CcTableLayoutPanelBase.Controls.Add(CcMenuStrip1, 0, 0);
            CcTableLayoutPanelBase.Controls.Add(CcStatusStrip1, 0, 3);
            CcTableLayoutPanelBase.Controls.Add(PanelExUp, 0, 1);
            CcTableLayoutPanelBase.Controls.Add(SpreadList, 1, 2);
            CcTableLayoutPanelBase.Dock = DockStyle.Fill;
            CcTableLayoutPanelBase.Location = new Point(0, 0);
            CcTableLayoutPanelBase.Name = "CcTableLayoutPanelBase";
            CcTableLayoutPanelBase.RowCount = 4;
            CcTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            CcTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            CcTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            CcTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            CcTableLayoutPanelBase.Size = new Size(1904, 1041);
            CcTableLayoutPanelBase.TabIndex = 0;
            // 
            // CcMenuStrip1
            // 
            CcMenuStrip1.BackColor = SystemColors.Control;
            CcTableLayoutPanelBase.SetColumnSpan(CcMenuStrip1, 3);
            CcMenuStrip1.Location = new Point(0, 0);
            CcMenuStrip1.Name = "CcMenuStrip1";
            CcMenuStrip1.Size = new Size(1904, 24);
            CcMenuStrip1.TabIndex = 0;
            CcMenuStrip1.Text = "menuStripEx1";
            CcMenuStrip1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // CcStatusStrip1
            // 
            CcTableLayoutPanelBase.SetColumnSpan(CcStatusStrip1, 3);
            CcStatusStrip1.Location = new Point(0, 1019);
            CcStatusStrip1.Name = "CcStatusStrip1";
            CcStatusStrip1.Size = new Size(1904, 22);
            CcStatusStrip1.SizingGrip = false;
            CcStatusStrip1.TabIndex = 1;
            CcStatusStrip1.Text = "statusStripEx1";
            // 
            // PanelExUp
            // 
            CcTableLayoutPanelBase.SetColumnSpan(PanelExUp, 3);
            PanelExUp.Controls.Add(labelEx3);
            PanelExUp.Controls.Add(CcComboBoxStaffDisplayName);
            PanelExUp.Controls.Add(labelEx2);
            PanelExUp.Controls.Add(labelEx1);
            PanelExUp.Controls.Add(CcDateTimePickerOperationDate2);
            PanelExUp.Controls.Add(CcDateTimePickerOperationDate1);
            PanelExUp.Controls.Add(ButtonExUpdate);
            PanelExUp.Dock = DockStyle.Fill;
            PanelExUp.Location = new Point(3, 27);
            PanelExUp.Name = "PanelExUp";
            PanelExUp.Size = new Size(1898, 40);
            PanelExUp.TabIndex = 2;
            // 
            // labelEx3
            // 
            labelEx3.AutoSize = true;
            labelEx3.Location = new Point(568, 12);
            labelEx3.Name = "labelEx3";
            labelEx3.Size = new Size(67, 15);
            labelEx3.TabIndex = 6;
            labelEx3.Text = "従事者氏名";
            // 
            // CcComboBoxStaffDisplayName
            // 
            CcComboBoxStaffDisplayName.FormattingEnabled = true;
            CcComboBoxStaffDisplayName.ImeMode = ImeMode.Hiragana;
            CcComboBoxStaffDisplayName.Location = new Point(640, 8);
            CcComboBoxStaffDisplayName.Name = "CcComboBoxStaffDisplayName";
            CcComboBoxStaffDisplayName.Size = new Size(244, 23);
            CcComboBoxStaffDisplayName.TabIndex = 5;
            // 
            // labelEx2
            // 
            labelEx2.AutoSize = true;
            labelEx2.Location = new Point(32, 12);
            labelEx2.Name = "labelEx2";
            labelEx2.Size = new Size(43, 15);
            labelEx2.TabIndex = 4;
            labelEx2.Text = "配車日";
            // 
            // labelEx1
            // 
            labelEx1.AutoSize = true;
            labelEx1.Location = new Point(268, 12);
            labelEx1.Name = "labelEx1";
            labelEx1.Size = new Size(19, 15);
            labelEx1.TabIndex = 3;
            labelEx1.Text = "～";
            // 
            // CcDateTimePickerOperationDate2
            // 
            CcDateTimePickerOperationDate2.CultureFlag = false;
            CcDateTimePickerOperationDate2.CustomFormat = " 明治33年01月01日(月曜日)";
            CcDateTimePickerOperationDate2.Format = DateTimePickerFormat.Custom;
            CcDateTimePickerOperationDate2.Location = new Point(292, 8);
            CcDateTimePickerOperationDate2.Name = "CcDateTimePickerOperationDate2";
            CcDateTimePickerOperationDate2.Size = new Size(184, 23);
            CcDateTimePickerOperationDate2.TabIndex = 2;
            CcDateTimePickerOperationDate2.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            CcDateTimePickerOperationDate2.ValueChanged += DateTimePickerExOperationDate2_ValueChanged;
            // 
            // CcDateTimePickerOperationDate1
            // 
            CcDateTimePickerOperationDate1.CultureFlag = false;
            CcDateTimePickerOperationDate1.CustomFormat = " 明治33年01月01日(月曜日)";
            CcDateTimePickerOperationDate1.Format = DateTimePickerFormat.Custom;
            CcDateTimePickerOperationDate1.Location = new Point(80, 8);
            CcDateTimePickerOperationDate1.Name = "CcDateTimePickerOperationDate1";
            CcDateTimePickerOperationDate1.Size = new Size(184, 23);
            CcDateTimePickerOperationDate1.TabIndex = 1;
            CcDateTimePickerOperationDate1.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            CcDateTimePickerOperationDate1.ValueChanged += DateTimePickerExOperationDate1_ValueChanged;
            // 
            // ButtonExUpdate
            // 
            ButtonExUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ButtonExUpdate.ForeColor = SystemColors.ControlText;
            ButtonExUpdate.Location = new Point(1676, 4);
            ButtonExUpdate.Name = "ButtonExUpdate";
            ButtonExUpdate.SetTextDirectionVertical = "";
            ButtonExUpdate.Size = new Size(180, 32);
            ButtonExUpdate.TabIndex = 0;
            ButtonExUpdate.Text = "最　新　化";
            ButtonExUpdate.UseVisualStyleBackColor = true;
            ButtonExUpdate.Click += CcButton_Click;
            // 
            // SpreadList
            // 
            SpreadList.AccessibleDescription = "SpreadList, Sheet1, Row 0, Column 0";
            SpreadList.Dock = DockStyle.Fill;
            SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F);
            SpreadList.Location = new Point(503, 73);
            SpreadList.Name = "SpreadList";
            SpreadList.Size = new Size(898, 941);
            SpreadList.TabIndex = 3;
            // 
            // StaffWorkingHours
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(CcTableLayoutPanelBase);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = CcMenuStrip1;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StaffWorkingHours";
            Text = "StaffWorkingHours";
            FormClosing += StaffWorkingHours_FormClosing;
            CcTableLayoutPanelBase.ResumeLayout(false);
            CcTableLayoutPanelBase.PerformLayout();
            PanelExUp.ResumeLayout(false);
            PanelExUp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SpreadList).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private CcControl.CcTableLayoutPanel CcTableLayoutPanelBase;
        private CcControl.CcMenuStrip CcMenuStrip1;
        private CcControl.CcStatusStrip CcStatusStrip1;
        private CcControl.CcPanel PanelExUp;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private CcControl.CcButton ButtonExUpdate;
        private CcControl.CcDateTime CcDateTimePickerOperationDate1;
        private CcControl.CcLabel labelEx1;
        private CcControl.CcDateTime CcDateTimePickerOperationDate2;
        private CcControl.CcLabel labelEx2;
        private CcControl.CcLabel labelEx3;
        private CcControl.CcComboBox CcComboBoxStaffDisplayName;
        private FarPoint.Win.Spread.SheetView SheetViewList;
    }
}