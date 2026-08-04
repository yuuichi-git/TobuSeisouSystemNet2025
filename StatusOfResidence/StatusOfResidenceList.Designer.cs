namespace StatusOfResidence {
    partial class StatusOfResidenceList {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatusOfResidenceList));
            TableLayoutPanelExBase = new CcControl.CcTableLayoutPanel();
            MenuStripEx1 = new CcControl.CcMenuStrip();
            StatusStripEx1 = new CcControl.CcStatusStrip();
            PanelExTop = new CcControl.CcPanel();
            ButtonExUpdate = new CcControl.CcButton();
            SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls"));
            SheetViewList = SpreadList.GetSheet(0);
            CheckBoxExRetirementFlag = new CcControl.CcCheckBox();
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
            TableLayoutPanelExBase.Size = new Size(1904, 1041);
            TableLayoutPanelExBase.TabIndex = 0;
            // 
            // MenuStripEx1
            // 
            MenuStripEx1.Location = new Point(0, 0);
            MenuStripEx1.Name = "MenuStripEx1";
            MenuStripEx1.Size = new Size(1904, 24);
            MenuStripEx1.TabIndex = 0;
            MenuStripEx1.Text = "menuStripEx1";
            MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // StatusStripEx1
            // 
            StatusStripEx1.Location = new Point(0, 1019);
            StatusStripEx1.Name = "StatusStripEx1";
            StatusStripEx1.Size = new Size(1904, 22);
            StatusStripEx1.TabIndex = 1;
            StatusStripEx1.Text = "statusStripEx1";
            // 
            // PanelExTop
            // 
            PanelExTop.Controls.Add(CheckBoxExRetirementFlag);
            PanelExTop.Controls.Add(ButtonExUpdate);
            PanelExTop.Dock = DockStyle.Fill;
            PanelExTop.Location = new Point(3, 27);
            PanelExTop.Name = "PanelExTop";
            PanelExTop.Size = new Size(1898, 54);
            PanelExTop.TabIndex = 2;
            // 
            // ButtonExUpdate
            // 
            ButtonExUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ButtonExUpdate.Font = new Font("Yu Gothic UI", 9.75F);
            ButtonExUpdate.ForeColor = SystemColors.ControlText;
            ButtonExUpdate.Location = new Point(1696, 12);
            ButtonExUpdate.Name = "ButtonExUpdate";
            ButtonExUpdate.SetTextDirectionVertical = null;
            ButtonExUpdate.Size = new Size(160, 32);
            ButtonExUpdate.TabIndex = 6;
            ButtonExUpdate.Text = "最　新　化";
            ButtonExUpdate.UseVisualStyleBackColor = true;
            ButtonExUpdate.Click += ButtonEx_Click;
            // 
            // SpreadList
            // 
            SpreadList.AccessibleDescription = "Book1, StatusOfResidenceList, Row 0, Column 0";
            SpreadList.Dock = DockStyle.Fill;
            SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F);
            SpreadList.Location = new Point(3, 87);
            SpreadList.Name = "SpreadList";
            SpreadList.Size = new Size(1898, 927);
            SpreadList.TabIndex = 3;
            SpreadList.CellDoubleClick += SpreadList_CellDoubleClick;
            // 
            // CheckBoxExRetirementFlag
            // 
            CheckBoxExRetirementFlag.AutoSize = true;
            CheckBoxExRetirementFlag.Location = new Point(1596, 20);
            CheckBoxExRetirementFlag.Name = "CheckBoxExRetirementFlag";
            CheckBoxExRetirementFlag.Size = new Size(95, 19);
            CheckBoxExRetirementFlag.TabIndex = 7;
            CheckBoxExRetirementFlag.Text = "退職者も表示";
            CheckBoxExRetirementFlag.UseVisualStyleBackColor = true;
            // 
            // StatusOfResidenceList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(TableLayoutPanelExBase);
            MainMenuStrip = MenuStripEx1;
            Name = "StatusOfResidenceList";
            Text = "StatusOfResidenceList";
            FormClosing += StatusOfResidenceList_FormClosing;
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
        private FarPoint.Win.Spread.SheetView SheetViewList;
        private CcControl.CcButton ButtonExUpdate;
        private CcControl.CcCheckBox CheckBoxExRetirementFlag;
    }
}
