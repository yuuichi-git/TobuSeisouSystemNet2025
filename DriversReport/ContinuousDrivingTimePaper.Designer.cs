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
            this.CcTableLayoutPanelBase = new CcControl.CcTableLayoutPanel();
            this.CcMenuStrip1 = new CcControl.CcMenuStrip();
            this.CcStatusStrip1 = new CcControl.CcStatusStrip();
            this.CcPanelTop = new CcControl.CcPanel();
            this.ccLabel3 = new CcControl.CcLabel();
            this.CcComboBoxStaffMaster1 = new CcControl.CcComboBoxStaffMaster();
            this.CcButtonUpdate = new CcControl.CcButton();
            this.ccLabel2 = new CcControl.CcLabel();
            this.ccLabel1 = new CcControl.CcLabel();
            this.CcDateTimePickerOperationEndDate = new CcControl.CcDateTime();
            this.CcDateTimePickerOperationStartDate = new CcControl.CcDateTime();
            this.SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("CcTableLayoutPanelBase.Controls"));
            this.SheetViewList = this.SpreadList.GetSheet(0);
            this.CcTableLayoutPanelBase.SuspendLayout();
            this.CcPanelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadList).BeginInit();
            this.SuspendLayout();
            // 
            // CcTableLayoutPanelBase
            // 
            this.CcTableLayoutPanelBase.ColumnCount = 1;
            this.CcTableLayoutPanelBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.CcTableLayoutPanelBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            this.CcTableLayoutPanelBase.Controls.Add(this.CcMenuStrip1, 0, 0);
            this.CcTableLayoutPanelBase.Controls.Add(this.CcStatusStrip1, 0, 3);
            this.CcTableLayoutPanelBase.Controls.Add(this.CcPanelTop, 0, 1);
            this.CcTableLayoutPanelBase.Controls.Add(this.SpreadList, 0, 2);
            this.CcTableLayoutPanelBase.Dock = DockStyle.Fill;
            this.CcTableLayoutPanelBase.Location = new Point(0, 0);
            this.CcTableLayoutPanelBase.Name = "CcTableLayoutPanelBase";
            this.CcTableLayoutPanelBase.RowCount = 4;
            this.CcTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.CcTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            this.CcTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.CcTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.CcTableLayoutPanelBase.Size = new Size(1904, 1041);
            this.CcTableLayoutPanelBase.TabIndex = 0;
            // 
            // CcMenuStrip1
            // 
            this.CcMenuStrip1.Location = new Point(0, 0);
            this.CcMenuStrip1.Name = "CcMenuStrip1";
            this.CcMenuStrip1.Size = new Size(1904, 24);
            this.CcMenuStrip1.TabIndex = 0;
            this.CcMenuStrip1.Text = "ccMenuStrip1";
            this.CcMenuStrip1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // CcStatusStrip1
            // 
            this.CcStatusStrip1.Location = new Point(0, 1019);
            this.CcStatusStrip1.Name = "CcStatusStrip1";
            this.CcStatusStrip1.Size = new Size(1904, 22);
            this.CcStatusStrip1.TabIndex = 1;
            this.CcStatusStrip1.Text = "ccStatusStrip1";
            // 
            // CcPanelTop
            // 
            this.CcPanelTop.Controls.Add(this.ccLabel3);
            this.CcPanelTop.Controls.Add(this.CcComboBoxStaffMaster1);
            this.CcPanelTop.Controls.Add(this.CcButtonUpdate);
            this.CcPanelTop.Controls.Add(this.ccLabel2);
            this.CcPanelTop.Controls.Add(this.ccLabel1);
            this.CcPanelTop.Controls.Add(this.CcDateTimePickerOperationEndDate);
            this.CcPanelTop.Controls.Add(this.CcDateTimePickerOperationStartDate);
            this.CcPanelTop.Dock = DockStyle.Fill;
            this.CcPanelTop.Location = new Point(3, 27);
            this.CcPanelTop.Name = "CcPanelTop";
            this.CcPanelTop.Size = new Size(1898, 54);
            this.CcPanelTop.TabIndex = 2;
            // 
            // ccLabel3
            // 
            this.ccLabel3.AutoSize = true;
            this.ccLabel3.Location = new Point(524, 20);
            this.ccLabel3.Name = "ccLabel3";
            this.ccLabel3.Size = new Size(55, 15);
            this.ccLabel3.TabIndex = 7;
            this.ccLabel3.Text = "運転者名";
            // 
            // CcComboBoxStaffMaster1
            // 
            this.CcComboBoxStaffMaster1.FormattingEnabled = true;
            this.CcComboBoxStaffMaster1.Location = new Point(584, 16);
            this.CcComboBoxStaffMaster1.Name = "CcComboBoxStaffMaster1";
            this.CcComboBoxStaffMaster1.Size = new Size(248, 23);
            this.CcComboBoxStaffMaster1.TabIndex = 6;
            // 
            // CcButtonUpdate
            // 
            this.CcButtonUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.CcButtonUpdate.ForeColor = SystemColors.ControlText;
            this.CcButtonUpdate.Location = new Point(1684, 11);
            this.CcButtonUpdate.Name = "CcButtonUpdate";
            this.CcButtonUpdate.SetTextDirectionVertical = "";
            this.CcButtonUpdate.Size = new Size(160, 32);
            this.CcButtonUpdate.TabIndex = 5;
            this.CcButtonUpdate.Text = "最　新　化";
            this.CcButtonUpdate.UseVisualStyleBackColor = true;
            this.CcButtonUpdate.Click += this.CcButtonUpdate_Click;
            // 
            // ccLabel2
            // 
            this.ccLabel2.AutoSize = true;
            this.ccLabel2.Location = new Point(28, 20);
            this.ccLabel2.Name = "ccLabel2";
            this.ccLabel2.Size = new Size(43, 15);
            this.ccLabel2.TabIndex = 4;
            this.ccLabel2.Text = "運行日";
            // 
            // ccLabel1
            // 
            this.ccLabel1.AutoSize = true;
            this.ccLabel1.Location = new Point(264, 20);
            this.ccLabel1.Name = "ccLabel1";
            this.ccLabel1.Size = new Size(19, 15);
            this.ccLabel1.TabIndex = 3;
            this.ccLabel1.Text = "～";
            // 
            // CcDateTimePickerOperationEndDate
            // 
            this.CcDateTimePickerOperationEndDate.CultureFlag = false;
            this.CcDateTimePickerOperationEndDate.CustomFormat = " 明治33年01月01日(月曜日)";
            this.CcDateTimePickerOperationEndDate.Format = DateTimePickerFormat.Custom;
            this.CcDateTimePickerOperationEndDate.Location = new Point(288, 16);
            this.CcDateTimePickerOperationEndDate.Name = "CcDateTimePickerOperationEndDate";
            this.CcDateTimePickerOperationEndDate.Size = new Size(184, 23);
            this.CcDateTimePickerOperationEndDate.TabIndex = 2;
            this.CcDateTimePickerOperationEndDate.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.CcDateTimePickerOperationEndDate.ValueChanged += this.CcDateTimePicker_ValueChanged;
            // 
            // CcDateTimePickerOperationStartDate
            // 
            this.CcDateTimePickerOperationStartDate.CultureFlag = false;
            this.CcDateTimePickerOperationStartDate.CustomFormat = " 明治33年01月01日(月曜日)";
            this.CcDateTimePickerOperationStartDate.Format = DateTimePickerFormat.Custom;
            this.CcDateTimePickerOperationStartDate.Location = new Point(76, 16);
            this.CcDateTimePickerOperationStartDate.Name = "CcDateTimePickerOperationStartDate";
            this.CcDateTimePickerOperationStartDate.Size = new Size(184, 23);
            this.CcDateTimePickerOperationStartDate.TabIndex = 1;
            this.CcDateTimePickerOperationStartDate.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.CcDateTimePickerOperationStartDate.ValueChanged += this.CcDateTimePicker_ValueChanged;
            // 
            // SpreadList
            // 
            this.SpreadList.AccessibleDescription = "SpreadList, List, Row 0, Column 0";
            this.SpreadList.Dock = DockStyle.Fill;
            this.SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F);
            this.SpreadList.Location = new Point(3, 87);
            this.SpreadList.Name = "SpreadList";
            this.SpreadList.Size = new Size(1898, 927);
            this.SpreadList.TabIndex = 3;
            // 
            // ContinuousDrivingTimePaper
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1904, 1041);
            this.Controls.Add(this.CcTableLayoutPanelBase);
            this.MainMenuStrip = this.CcMenuStrip1;
            this.Name = "ContinuousDrivingTimePaper";
            this.Text = "ContinuousDrivingTimePaper";
            this.FormClosing += this.ContinuousDrivingTimePaper_FormClosing;
            this.CcTableLayoutPanelBase.ResumeLayout(false);
            this.CcTableLayoutPanelBase.PerformLayout();
            this.CcPanelTop.ResumeLayout(false);
            this.CcPanelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadList).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private CcControl.CcTableLayoutPanel CcTableLayoutPanelBase;
        private CcControl.CcMenuStrip CcMenuStrip1;
        private CcControl.CcStatusStrip CcStatusStrip1;
        private CcControl.CcPanel CcPanelTop;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private CcControl.CcDateTime CcDateTimePickerOperationEndDate;
        private CcControl.CcDateTime CcDateTimePickerOperationStartDate;
        private CcControl.CcLabel ccLabel2;
        private CcControl.CcLabel ccLabel1;
        private CcControl.CcButton CcButtonUpdate;
        private CcControl.CcLabel ccLabel3;
        private CcControl.CcComboBoxStaffMaster CcComboBoxStaffMaster1;
        private FarPoint.Win.Spread.SheetView SheetViewList;
    }
}