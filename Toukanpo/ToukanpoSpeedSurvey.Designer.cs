namespace Toukanpo {
    partial class ToukanpoSpeedSurvey {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToukanpoSpeedSurvey));
            TableLayoutPanelExBase = new CcControl.CcTableLayoutPanel();
            MenuStripEx1 = new CcControl.CcMenuStrip();
            StatusStripEx1 = new CcControl.CcStatusStrip();
            PanelExTop = new CcControl.CcPanel();
            ButtonExUpdate = new CcControl.CcButton();
            labelEx3 = new CcControl.CcLabel();
            labelEx2 = new CcControl.CcLabel();
            labelEx1 = new CcControl.CcLabel();
            NumericUpDownExMonth = new CcControl.CcNumericUpDown();
            NumericUpDownExYear = new CcControl.CcNumericUpDown();
            SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls"));
            SheetViewList = SpreadList.GetSheet(0);
            TableLayoutPanelExBase.SuspendLayout();
            PanelExTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumericUpDownExMonth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumericUpDownExYear).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SpreadList).BeginInit();
            SuspendLayout();
            // 
            // TableLayoutPanelExBase
            // 
            TableLayoutPanelExBase.ColumnCount = 3;
            TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 550F));
            TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 550F));
            TableLayoutPanelExBase.Controls.Add(MenuStripEx1, 0, 0);
            TableLayoutPanelExBase.Controls.Add(StatusStripEx1, 0, 3);
            TableLayoutPanelExBase.Controls.Add(PanelExTop, 0, 1);
            TableLayoutPanelExBase.Controls.Add(SpreadList, 1, 2);
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
            // MenuStripEx1
            // 
            TableLayoutPanelExBase.SetColumnSpan(MenuStripEx1, 3);
            MenuStripEx1.Location = new Point(0, 0);
            MenuStripEx1.Name = "MenuStripEx1";
            MenuStripEx1.Size = new Size(1904, 24);
            MenuStripEx1.TabIndex = 0;
            MenuStripEx1.Text = "menuStripEx1";
            MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // StatusStripEx1
            // 
            TableLayoutPanelExBase.SetColumnSpan(StatusStripEx1, 3);
            StatusStripEx1.Location = new Point(0, 1019);
            StatusStripEx1.Name = "StatusStripEx1";
            StatusStripEx1.Size = new Size(1904, 22);
            StatusStripEx1.SizingGrip = false;
            StatusStripEx1.TabIndex = 1;
            StatusStripEx1.Text = "statusStripEx1";
            // 
            // PanelExTop
            // 
            TableLayoutPanelExBase.SetColumnSpan(PanelExTop, 3);
            PanelExTop.Controls.Add(ButtonExUpdate);
            PanelExTop.Controls.Add(labelEx3);
            PanelExTop.Controls.Add(labelEx2);
            PanelExTop.Controls.Add(labelEx1);
            PanelExTop.Controls.Add(NumericUpDownExMonth);
            PanelExTop.Controls.Add(NumericUpDownExYear);
            PanelExTop.Dock = DockStyle.Fill;
            PanelExTop.Location = new Point(3, 27);
            PanelExTop.Name = "PanelExTop";
            PanelExTop.Size = new Size(1898, 54);
            PanelExTop.TabIndex = 2;
            // 
            // ButtonExUpdate
            // 
            ButtonExUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ButtonExUpdate.ForeColor = SystemColors.ControlText;
            ButtonExUpdate.Location = new Point(1705, 12);
            ButtonExUpdate.Name = "ButtonExUpdate";
            ButtonExUpdate.SetTextDirectionVertical = "";
            ButtonExUpdate.Size = new Size(160, 32);
            ButtonExUpdate.TabIndex = 5;
            ButtonExUpdate.Text = "最　新　化";
            ButtonExUpdate.UseVisualStyleBackColor = true;
            ButtonExUpdate.Click += ButtonExUpdate_Click;
            // 
            // labelEx3
            // 
            labelEx3.AutoSize = true;
            labelEx3.Location = new Point(232, 20);
            labelEx3.Name = "labelEx3";
            labelEx3.Size = new Size(31, 15);
            labelEx3.TabIndex = 4;
            labelEx3.Text = "月分";
            // 
            // labelEx2
            // 
            labelEx2.AutoSize = true;
            labelEx2.Location = new Point(152, 20);
            labelEx2.Name = "labelEx2";
            labelEx2.Size = new Size(19, 15);
            labelEx2.TabIndex = 3;
            labelEx2.Text = "年";
            // 
            // labelEx1
            // 
            labelEx1.AutoSize = true;
            labelEx1.Location = new Point(16, 20);
            labelEx1.Name = "labelEx1";
            labelEx1.Size = new Size(79, 15);
            labelEx1.TabIndex = 2;
            labelEx1.Text = "集計対象年月";
            // 
            // NumericUpDownExMonth
            // 
            NumericUpDownExMonth.Location = new Point(176, 16);
            NumericUpDownExMonth.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            NumericUpDownExMonth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NumericUpDownExMonth.Name = "NumericUpDownExMonth";
            NumericUpDownExMonth.Size = new Size(50, 23);
            NumericUpDownExMonth.TabIndex = 1;
            NumericUpDownExMonth.TextAlign = HorizontalAlignment.Right;
            NumericUpDownExMonth.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // NumericUpDownExYear
            // 
            NumericUpDownExYear.Location = new Point(100, 16);
            NumericUpDownExYear.Maximum = new decimal(new int[] { 2029, 0, 0, 0 });
            NumericUpDownExYear.Minimum = new decimal(new int[] { 2024, 0, 0, 0 });
            NumericUpDownExYear.Name = "NumericUpDownExYear";
            NumericUpDownExYear.Size = new Size(50, 23);
            NumericUpDownExYear.TabIndex = 0;
            NumericUpDownExYear.TextAlign = HorizontalAlignment.Right;
            NumericUpDownExYear.Value = new decimal(new int[] { 2025, 0, 0, 0 });
            // 
            // SpreadList
            // 
            SpreadList.AccessibleDescription = "SpreadList, 速度調査, Row 0, Column 0";
            SpreadList.Dock = DockStyle.Fill;
            SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F);
            SpreadList.Location = new Point(553, 87);
            SpreadList.Name = "SpreadList";
            SpreadList.Size = new Size(798, 927);
            SpreadList.TabIndex = 3;
            // 
            // ToukanpoSpeedSurvey
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(TableLayoutPanelExBase);
            MainMenuStrip = MenuStripEx1;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ToukanpoSpeedSurvey";
            Text = "ToukanpoSpeedSurvey";
            FormClosing += ToukanpoSpeedSurvey_FormClosing;
            TableLayoutPanelExBase.ResumeLayout(false);
            TableLayoutPanelExBase.PerformLayout();
            PanelExTop.ResumeLayout(false);
            PanelExTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NumericUpDownExMonth).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumericUpDownExYear).EndInit();
            ((System.ComponentModel.ISupportInitialize)SpreadList).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private CcControl.CcTableLayoutPanel TableLayoutPanelExBase;
        private CcControl.CcMenuStrip MenuStripEx1;
        private CcControl.CcStatusStrip StatusStripEx1;
        private CcControl.CcPanel PanelExTop;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private CcControl.CcNumericUpDown NumericUpDownExYear;
        private CcControl.CcLabel labelEx3;
        private CcControl.CcLabel labelEx2;
        private CcControl.CcLabel labelEx1;
        private CcControl.CcNumericUpDown NumericUpDownExMonth;
        private CcControl.CcButton ButtonExUpdate;
        private FarPoint.Win.Spread.SheetView SheetViewList;
    }
}