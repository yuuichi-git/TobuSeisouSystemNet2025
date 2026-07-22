namespace PeakSeason {
    partial class PeakSeasonAllowanceList {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PeakSeasonAllowanceList));
            CcTableLayoutPanelBase = new CcControl.CcTableLayoutPanel();
            CcMenuStrip1 = new CcControl.CcMenuStrip();
            CcStatusStrip1 = new CcControl.CcStatusStrip();
            CcPanelTop = new CcControl.CcPanel();
            CcDateTimeOperationDate2 = new CcControl.CcDateTime();
            ccLabel2 = new CcControl.CcLabel();
            ccLabel1 = new CcControl.CcLabel();
            CcDateTimeOperationDate1 = new CcControl.CcDateTime();
            CcButtonUpdate = new CcControl.CcButton();
            SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("CcTableLayoutPanelBase.Controls"));
            SheetViewList = SpreadList.GetSheet(0);
            CcPanelLeft = new CcControl.CcPanel();
            CcLabelPeakSeasonAllowanceCount = new CcControl.CcLabel();
            ccTextBox1 = new CcControl.CcTextBox();
            CcTableLayoutPanelBase.SuspendLayout();
            CcPanelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SpreadList).BeginInit();
            CcPanelLeft.SuspendLayout();
            SuspendLayout();
            // 
            // CcTableLayoutPanelBase
            // 
            CcTableLayoutPanelBase.ColumnCount = 3;
            CcTableLayoutPanelBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 350F));
            CcTableLayoutPanelBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            CcTableLayoutPanelBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 350F));
            CcTableLayoutPanelBase.Controls.Add(CcMenuStrip1, 0, 0);
            CcTableLayoutPanelBase.Controls.Add(CcStatusStrip1, 0, 3);
            CcTableLayoutPanelBase.Controls.Add(CcPanelTop, 0, 1);
            CcTableLayoutPanelBase.Controls.Add(SpreadList, 1, 2);
            CcTableLayoutPanelBase.Controls.Add(CcPanelLeft, 0, 2);
            CcTableLayoutPanelBase.Dock = DockStyle.Fill;
            CcTableLayoutPanelBase.Location = new Point(0, 0);
            CcTableLayoutPanelBase.Name = "CcTableLayoutPanelBase";
            CcTableLayoutPanelBase.RowCount = 4;
            CcTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            CcTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            CcTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            CcTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            CcTableLayoutPanelBase.Size = new Size(1904, 1041);
            CcTableLayoutPanelBase.TabIndex = 0;
            // 
            // CcMenuStrip1
            // 
            CcTableLayoutPanelBase.SetColumnSpan(CcMenuStrip1, 3);
            CcMenuStrip1.Location = new Point(0, 0);
            CcMenuStrip1.Name = "CcMenuStrip1";
            CcMenuStrip1.Size = new Size(1904, 24);
            CcMenuStrip1.TabIndex = 0;
            CcMenuStrip1.Text = "ccMenuStrip1";
            CcMenuStrip1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // CcStatusStrip1
            // 
            CcTableLayoutPanelBase.SetColumnSpan(CcStatusStrip1, 3);
            CcStatusStrip1.Location = new Point(0, 1019);
            CcStatusStrip1.Name = "CcStatusStrip1";
            CcStatusStrip1.Size = new Size(1904, 22);
            CcStatusStrip1.TabIndex = 1;
            CcStatusStrip1.Text = "ccStatusStrip1";
            // 
            // CcPanelTop
            // 
            CcTableLayoutPanelBase.SetColumnSpan(CcPanelTop, 3);
            CcPanelTop.Controls.Add(CcDateTimeOperationDate2);
            CcPanelTop.Controls.Add(ccLabel2);
            CcPanelTop.Controls.Add(ccLabel1);
            CcPanelTop.Controls.Add(CcDateTimeOperationDate1);
            CcPanelTop.Controls.Add(CcButtonUpdate);
            CcPanelTop.Dock = DockStyle.Fill;
            CcPanelTop.Location = new Point(3, 27);
            CcPanelTop.Name = "CcPanelTop";
            CcPanelTop.Size = new Size(1898, 46);
            CcPanelTop.TabIndex = 2;
            // 
            // CcDateTimeOperationDate2
            // 
            CcDateTimeOperationDate2.CultureFlag = false;
            CcDateTimeOperationDate2.CustomFormat = " 明治33年01月01日(月曜日)";
            CcDateTimeOperationDate2.Format = DateTimePickerFormat.Custom;
            CcDateTimeOperationDate2.Location = new Point(284, 12);
            CcDateTimeOperationDate2.Name = "CcDateTimeOperationDate2";
            CcDateTimeOperationDate2.Size = new Size(184, 23);
            CcDateTimeOperationDate2.TabIndex = 20;
            CcDateTimeOperationDate2.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            CcDateTimeOperationDate2.ValueChanged += CcDateTime_ValueChanged;
            // 
            // ccLabel2
            // 
            ccLabel2.AutoSize = true;
            ccLabel2.Location = new Point(264, 16);
            ccLabel2.Name = "ccLabel2";
            ccLabel2.Size = new Size(19, 15);
            ccLabel2.TabIndex = 19;
            ccLabel2.Text = "～";
            // 
            // ccLabel1
            // 
            ccLabel1.AutoSize = true;
            ccLabel1.Location = new Point(16, 16);
            ccLabel1.Name = "ccLabel1";
            ccLabel1.Size = new Size(55, 15);
            ccLabel1.TabIndex = 18;
            ccLabel1.Text = "集計期間";
            // 
            // CcDateTimeOperationDate1
            // 
            CcDateTimeOperationDate1.CultureFlag = false;
            CcDateTimeOperationDate1.CustomFormat = " 明治33年01月01日(月曜日)";
            CcDateTimeOperationDate1.Format = DateTimePickerFormat.Custom;
            CcDateTimeOperationDate1.Location = new Point(76, 12);
            CcDateTimeOperationDate1.Name = "CcDateTimeOperationDate1";
            CcDateTimeOperationDate1.Size = new Size(184, 23);
            CcDateTimeOperationDate1.TabIndex = 17;
            CcDateTimeOperationDate1.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            CcDateTimeOperationDate1.ValueChanged += CcDateTime_ValueChanged;
            // 
            // CcButtonUpdate
            // 
            CcButtonUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CcButtonUpdate.Font = new Font("Yu Gothic UI", 9.75F);
            CcButtonUpdate.ForeColor = SystemColors.ControlText;
            CcButtonUpdate.Location = new Point(1693, 8);
            CcButtonUpdate.Name = "CcButtonUpdate";
            CcButtonUpdate.SetTextDirectionVertical = "";
            CcButtonUpdate.Size = new Size(160, 30);
            CcButtonUpdate.TabIndex = 13;
            CcButtonUpdate.Text = "最　新　化";
            CcButtonUpdate.UseVisualStyleBackColor = true;
            CcButtonUpdate.Click += CcButtonUpdate_Click;
            // 
            // SpreadList
            // 
            SpreadList.AccessibleDescription = "SpreadList, Sheet1, Row 0, Column 0";
            SpreadList.Dock = DockStyle.Fill;
            SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F);
            SpreadList.Location = new Point(353, 79);
            SpreadList.Name = "SpreadList";
            SpreadList.Size = new Size(1198, 935);
            SpreadList.TabIndex = 3;
            // 
            // CcPanelLeft
            // 
            CcPanelLeft.Controls.Add(ccTextBox1);
            CcPanelLeft.Controls.Add(CcLabelPeakSeasonAllowanceCount);
            CcPanelLeft.Dock = DockStyle.Fill;
            CcPanelLeft.Location = new Point(3, 79);
            CcPanelLeft.Name = "CcPanelLeft";
            CcPanelLeft.Size = new Size(344, 935);
            CcPanelLeft.TabIndex = 4;
            // 
            // CcLabelPeakSeasonAllowanceCount
            // 
            CcLabelPeakSeasonAllowanceCount.AutoSize = true;
            CcLabelPeakSeasonAllowanceCount.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            CcLabelPeakSeasonAllowanceCount.Location = new Point(44, 40);
            CcLabelPeakSeasonAllowanceCount.Name = "CcLabelPeakSeasonAllowanceCount";
            CcLabelPeakSeasonAllowanceCount.Size = new Size(240, 21);
            CcLabelPeakSeasonAllowanceCount.TabIndex = 12;
            CcLabelPeakSeasonAllowanceCount.Text = "集計期間内の対象日数合計：0日";
            // 
            // ccTextBox1
            // 
            ccTextBox1.Enabled = false;
            ccTextBox1.Location = new Point(8, 112);
            ccTextBox1.Multiline = true;
            ccTextBox1.Name = "ccTextBox1";
            ccTextBox1.Size = new Size(328, 812);
            ccTextBox1.TabIndex = 13;
            ccTextBox1.Text = resources.GetString("ccTextBox1.Text");
            // 
            // PeakSeasonAllowanceList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(CcTableLayoutPanelBase);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = CcMenuStrip1;
            Name = "PeakSeasonAllowanceList";
            Text = "PeakSeasonAllowanceList";
            FormClosing += PaidLeavePrint_FormClosing;
            CcTableLayoutPanelBase.ResumeLayout(false);
            CcTableLayoutPanelBase.PerformLayout();
            CcPanelTop.ResumeLayout(false);
            CcPanelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SpreadList).EndInit();
            CcPanelLeft.ResumeLayout(false);
            CcPanelLeft.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private CcControl.CcTableLayoutPanel CcTableLayoutPanelBase;
        private CcControl.CcMenuStrip CcMenuStrip1;
        private CcControl.CcStatusStrip CcStatusStrip1;
        private CcControl.CcPanel CcPanelTop;
        private CcControl.CcDateTime CcDateTimeOperationDate2;
        private CcControl.CcLabel ccLabel2;
        private CcControl.CcLabel ccLabel1;
        private CcControl.CcDateTime CcDateTimeOperationDate1;
        private CcControl.CcButton CcButtonUpdate;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private FarPoint.Win.Spread.SheetView SheetViewList;
        private CcControl.CcPanel CcPanelLeft;
        private CcControl.CcLabel CcLabelPeakSeasonAllowanceCount;
        private CcControl.CcTextBox ccTextBox1;
    }
}
