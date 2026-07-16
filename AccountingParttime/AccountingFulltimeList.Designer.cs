namespace Accounting {
    partial class AccountingFulltimeList {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountingFulltimeList));
            TableLayoutPanelExBase = new CcControl.CcTableLayoutPanel();
            MenuStripEx1 = new CcControl.CcMenuStrip();
            StatusStripEx = new CcControl.CcStatusStrip();
            PanelExTop = new CcControl.CcPanel();
            ButtonExUpdate = new CcControl.CcButton();
            labelEx1 = new CcControl.CcLabel();
            DateTimePickerExOperationDate = new CcControl.CcDateTime();
            SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls"));
            SheetViewList = SpreadList.GetSheet(0);
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
            TableLayoutPanelExBase.Controls.Add(StatusStripEx, 0, 3);
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
            TableLayoutPanelExBase.Size = new Size(829, 1041);
            TableLayoutPanelExBase.TabIndex = 0;
            // 
            // MenuStripEx1
            // 
            MenuStripEx1.Location = new Point(0, 0);
            MenuStripEx1.Name = "MenuStripEx1";
            MenuStripEx1.Size = new Size(829, 24);
            MenuStripEx1.TabIndex = 0;
            MenuStripEx1.Text = "menuStripEx1";
            MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // StatusStripEx
            // 
            StatusStripEx.Location = new Point(0, 1019);
            StatusStripEx.Name = "StatusStripEx";
            StatusStripEx.Size = new Size(829, 22);
            StatusStripEx.TabIndex = 1;
            StatusStripEx.Text = "statusStripEx1";
            // 
            // PanelExTop
            // 
            PanelExTop.Controls.Add(ButtonExUpdate);
            PanelExTop.Controls.Add(labelEx1);
            PanelExTop.Controls.Add(DateTimePickerExOperationDate);
            PanelExTop.Dock = DockStyle.Fill;
            PanelExTop.Location = new Point(3, 27);
            PanelExTop.Name = "PanelExTop";
            PanelExTop.Size = new Size(823, 54);
            PanelExTop.TabIndex = 2;
            // 
            // ButtonExUpdate
            // 
            ButtonExUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ButtonExUpdate.Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            ButtonExUpdate.ForeColor = SystemColors.ControlText;
            ButtonExUpdate.Location = new Point(639, 9);
            ButtonExUpdate.Name = "ButtonExUpdate";
            ButtonExUpdate.SetTextDirectionVertical = "";
            ButtonExUpdate.Size = new Size(164, 36);
            ButtonExUpdate.TabIndex = 4;
            ButtonExUpdate.Text = "最　新　化";
            ButtonExUpdate.UseVisualStyleBackColor = true;
            ButtonExUpdate.Click += ButtonExUpdate_Click;
            // 
            // labelEx1
            // 
            labelEx1.AutoSize = true;
            labelEx1.Location = new Point(20, 20);
            labelEx1.Name = "labelEx1";
            labelEx1.Size = new Size(43, 15);
            labelEx1.TabIndex = 3;
            labelEx1.Text = "配車日";
            // 
            // DateTimePickerExOperationDate
            // 
            DateTimePickerExOperationDate.CultureFlag = false;
            DateTimePickerExOperationDate.CustomFormat = " 明治33年01月01日(月曜日)";
            DateTimePickerExOperationDate.Format = DateTimePickerFormat.Custom;
            DateTimePickerExOperationDate.Location = new Point(68, 16);
            DateTimePickerExOperationDate.Name = "DateTimePickerExOperationDate";
            DateTimePickerExOperationDate.Size = new Size(184, 23);
            DateTimePickerExOperationDate.TabIndex = 2;
            DateTimePickerExOperationDate.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // SpreadList
            // 
            SpreadList.AccessibleDescription = "SpreadList, Sheet1, Row 0, Column 0";
            SpreadList.Dock = DockStyle.Fill;
            SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F);
            SpreadList.Location = new Point(3, 87);
            SpreadList.Name = "SpreadList";
            SpreadList.Size = new Size(823, 927);
            SpreadList.TabIndex = 3;
            // 
            // AccountingFulltimeList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(829, 1041);
            Controls.Add(TableLayoutPanelExBase);
            MainMenuStrip = MenuStripEx1;
            Name = "AccountingFulltimeList";
            Text = "AccountingFulltimeList";
            FormClosing += AccountingFulltime_FormClosing;
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
        private CcControl.CcStatusStrip StatusStripEx;
        private CcControl.CcPanel PanelExTop;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private CcControl.CcLabel labelEx1;
        private CcControl.CcDateTime DateTimePickerExOperationDate;
        private CcControl.CcButton ButtonExUpdate;
        private FarPoint.Win.Spread.SheetView SheetViewList;
    }
}