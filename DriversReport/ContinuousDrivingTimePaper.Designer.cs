namespace DriversReport {
    partial class ContinuousDrivingTimePaper {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContinuousDrivingTimePaper));
            CcTableLayoutPanelBase = new CcControl.CcTableLayoutPanel();
            CcMenuStrip1 = new CcControl.CcMenuStrip();
            CcStatusStrip1 = new CcControl.CcStatusStrip();
            CcPanelTop = new CcControl.CcPanel();
            ccLabel3 = new CcControl.CcLabel();
            CcComboBoxStaffMaster1 = new CcControl.CcComboBoxStaffMaster();
            CcButtonUpdate = new CcControl.CcButton();
            ccLabel2 = new CcControl.CcLabel();
            ccLabel1 = new CcControl.CcLabel();
            CcDateTimePickerOperationEndDate = new CcControl.CcDateTime();
            CcDateTimePickerOperationStartDate = new CcControl.CcDateTime();
            SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("CcTableLayoutPanelBase.Controls"));
            SheetViewList = SpreadList.GetSheet(0);
            CcTableLayoutPanelBase.SuspendLayout();
            CcPanelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SpreadList).BeginInit();
            SuspendLayout();
            // 
            // CcTableLayoutPanelBase
            // 
            CcTableLayoutPanelBase.ColumnCount = 1;
            CcTableLayoutPanelBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            CcTableLayoutPanelBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            CcTableLayoutPanelBase.Controls.Add(CcMenuStrip1, 0, 0);
            CcTableLayoutPanelBase.Controls.Add(CcStatusStrip1, 0, 3);
            CcTableLayoutPanelBase.Controls.Add(CcPanelTop, 0, 1);
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
            // CcPanelTop
            // 
            CcPanelTop.Controls.Add(ccLabel3);
            CcPanelTop.Controls.Add(CcComboBoxStaffMaster1);
            CcPanelTop.Controls.Add(CcButtonUpdate);
            CcPanelTop.Controls.Add(ccLabel2);
            CcPanelTop.Controls.Add(ccLabel1);
            CcPanelTop.Controls.Add(CcDateTimePickerOperationEndDate);
            CcPanelTop.Controls.Add(CcDateTimePickerOperationStartDate);
            CcPanelTop.Dock = DockStyle.Fill;
            CcPanelTop.Location = new Point(3, 27);
            CcPanelTop.Name = "CcPanelTop";
            CcPanelTop.Size = new Size(1898, 54);
            CcPanelTop.TabIndex = 2;
            // 
            // ccLabel3
            // 
            ccLabel3.AutoSize = true;
            ccLabel3.Location = new Point(524, 20);
            ccLabel3.Name = "ccLabel3";
            ccLabel3.Size = new Size(55, 15);
            ccLabel3.TabIndex = 7;
            ccLabel3.Text = "運転者名";
            // 
            // CcComboBoxStaffMaster1
            // 
            CcComboBoxStaffMaster1.FormattingEnabled = true;
            CcComboBoxStaffMaster1.Location = new Point(584, 16);
            CcComboBoxStaffMaster1.Name = "CcComboBoxStaffMaster1";
            CcComboBoxStaffMaster1.Size = new Size(248, 23);
            CcComboBoxStaffMaster1.TabIndex = 6;
            // 
            // CcButtonUpdate
            // 
            CcButtonUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CcButtonUpdate.ForeColor = SystemColors.ControlText;
            CcButtonUpdate.Location = new Point(1684, 11);
            CcButtonUpdate.Name = "CcButtonUpdate";
            CcButtonUpdate.SetTextDirectionVertical = "";
            CcButtonUpdate.Size = new Size(160, 32);
            CcButtonUpdate.TabIndex = 5;
            CcButtonUpdate.Text = "最　新　化";
            CcButtonUpdate.UseVisualStyleBackColor = true;
            CcButtonUpdate.Click += CcButtonUpdate_Click;
            // 
            // ccLabel2
            // 
            ccLabel2.AutoSize = true;
            ccLabel2.Location = new Point(28, 20);
            ccLabel2.Name = "ccLabel2";
            ccLabel2.Size = new Size(43, 15);
            ccLabel2.TabIndex = 4;
            ccLabel2.Text = "運行日";
            // 
            // ccLabel1
            // 
            ccLabel1.AutoSize = true;
            ccLabel1.Location = new Point(264, 20);
            ccLabel1.Name = "ccLabel1";
            ccLabel1.Size = new Size(19, 15);
            ccLabel1.TabIndex = 3;
            ccLabel1.Text = "～";
            // 
            // CcDateTimePickerOperationEndDate
            // 
            CcDateTimePickerOperationEndDate.CultureFlag = false;
            CcDateTimePickerOperationEndDate.CustomFormat = " 明治33年01月01日(月曜日)";
            CcDateTimePickerOperationEndDate.Format = DateTimePickerFormat.Custom;
            CcDateTimePickerOperationEndDate.Location = new Point(288, 16);
            CcDateTimePickerOperationEndDate.Name = "CcDateTimePickerOperationEndDate";
            CcDateTimePickerOperationEndDate.Size = new Size(184, 23);
            CcDateTimePickerOperationEndDate.TabIndex = 2;
            CcDateTimePickerOperationEndDate.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // CcDateTimePickerOperationStartDate
            // 
            CcDateTimePickerOperationStartDate.CultureFlag = false;
            CcDateTimePickerOperationStartDate.CustomFormat = " 明治33年01月01日(月曜日)";
            CcDateTimePickerOperationStartDate.Format = DateTimePickerFormat.Custom;
            CcDateTimePickerOperationStartDate.Location = new Point(76, 16);
            CcDateTimePickerOperationStartDate.Name = "CcDateTimePickerOperationStartDate";
            CcDateTimePickerOperationStartDate.Size = new Size(184, 23);
            CcDateTimePickerOperationStartDate.TabIndex = 1;
            CcDateTimePickerOperationStartDate.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // SpreadList
            // 
            SpreadList.AccessibleDescription = "SpreadList, List, Row 0, Column 0";
            SpreadList.Dock = DockStyle.Fill;
            SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F);
            SpreadList.Location = new Point(3, 87);
            SpreadList.Name = "SpreadList";
            SpreadList.Size = new Size(1898, 927);
            SpreadList.TabIndex = 3;
            // 
            // ContinuousDrivingTimePaper
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(CcTableLayoutPanelBase);
            MainMenuStrip = CcMenuStrip1;
            Name = "ContinuousDrivingTimePaper";
            Text = "ContinuousDrivingTimePaper";
            CcTableLayoutPanelBase.ResumeLayout(false);
            CcTableLayoutPanelBase.PerformLayout();
            CcPanelTop.ResumeLayout(false);
            CcPanelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SpreadList).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private CcControl.CcTableLayoutPanel CcTableLayoutPanelBase;
        private CcControl.CcMenuStrip CcMenuStrip1;
        private CcControl.CcStatusStrip CcStatusStrip1;
        private CcControl.CcPanel CcPanelTop;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private FarPoint.Win.Spread.SheetView SheetViewList;
        private CcControl.CcDateTime CcDateTimePickerOperationEndDate;
        private CcControl.CcDateTime CcDateTimePickerOperationStartDate;
        private CcControl.CcLabel ccLabel2;
        private CcControl.CcLabel ccLabel1;
        private CcControl.CcButton CcButtonUpdate;
        private CcControl.CcLabel ccLabel3;
        private CcControl.CcComboBoxStaffMaster CcComboBoxStaffMaster1;
    }
}