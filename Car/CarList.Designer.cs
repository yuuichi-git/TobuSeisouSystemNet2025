namespace Car {
    partial class CarList {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CarList));
            TableLayoutPanelExBase = new CcControl.CcTableLayoutPanel();
            CcMenuStrip1 = new CcControl.CcMenuStrip();
            CcStatusStrip1 = new CcControl.CcStatusStrip();
            SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls"));
            CcContextMenuStrip1 = new CcControl.CcContextMenuStrip();
            ToolStripMenuItemDelete = new ToolStripMenuItem();
            ToolStripMenuItemRemove = new ToolStripMenuItem();
            SheetViewList = SpreadList.GetSheet(0);
            SheetViewList東京都運輸事業者向け燃料費高騰緊急対策事業支援金 = SpreadList.GetSheet(1);
            SheetViewList緊急通行車両 = SpreadList.GetSheet(2);
            CcPanelTop = new CcControl.CcPanel();
            ButtonExUpdate = new CcControl.CcButton();
            CheckBoxExDeleteFlag = new CcControl.CcCheckBox();
            TableLayoutPanelExBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SpreadList).BeginInit();
            CcContextMenuStrip1.SuspendLayout();
            CcPanelTop.SuspendLayout();
            SuspendLayout();
            // 
            // TableLayoutPanelExBase
            // 
            TableLayoutPanelExBase.ColumnCount = 1;
            TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            TableLayoutPanelExBase.Controls.Add(CcMenuStrip1, 0, 0);
            TableLayoutPanelExBase.Controls.Add(CcStatusStrip1, 0, 3);
            TableLayoutPanelExBase.Controls.Add(SpreadList, 0, 2);
            TableLayoutPanelExBase.Controls.Add(CcPanelTop, 0, 1);
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
            CcMenuStrip1.Location = new Point(0, 0);
            CcMenuStrip1.Name = "CcMenuStrip1";
            CcMenuStrip1.Size = new Size(1904, 24);
            CcMenuStrip1.TabIndex = 0;
            CcMenuStrip1.Text = "menuStripEx1";
            CcMenuStrip1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // CcStatusStrip1
            // 
            CcStatusStrip1.Location = new Point(0, 1019);
            CcStatusStrip1.Name = "CcStatusStrip1";
            CcStatusStrip1.Size = new Size(1904, 22);
            CcStatusStrip1.TabIndex = 1;
            CcStatusStrip1.Text = "statusStripEx1";
            // 
            // SpreadList
            // 
            SpreadList.AccessibleDescription = "SpreadList, 緊急通行車両, Row 0, Column 0";
            SpreadList.ContextMenuStrip = CcContextMenuStrip1;
            SpreadList.Dock = DockStyle.Fill;
            SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F);
            SpreadList.Location = new Point(3, 87);
            SpreadList.Name = "SpreadList";
            SpreadList.Size = new Size(1898, 927);
            SpreadList.TabIndex = 2;
            SpreadList.SheetTabClick += SpreadList_SheetTabClick;
            SpreadList.CellDoubleClick += SpreadList_CellDoubleClick;
            // 
            // CcContextMenuStrip1
            // 
            CcContextMenuStrip1.Items.AddRange(new ToolStripItem[] { ToolStripMenuItemDelete, ToolStripMenuItemRemove });
            CcContextMenuStrip1.Name = "ContextMenuStripEx1";
            CcContextMenuStrip1.Size = new Size(178, 48);
            CcContextMenuStrip1.Opening += ContextMenuStripEx1_Opening;
            // 
            // ToolStripMenuItemDelete
            // 
            ToolStripMenuItemDelete.Name = "ToolStripMenuItemDelete";
            ToolStripMenuItemDelete.Size = new Size(177, 22);
            ToolStripMenuItemDelete.Text = "このレコードを削除する";
            ToolStripMenuItemDelete.Click += ToolStripMenuItem_Click;
            // 
            // ToolStripMenuItemRemove
            // 
            ToolStripMenuItemRemove.Name = "ToolStripMenuItemRemove";
            ToolStripMenuItemRemove.Size = new Size(177, 22);
            ToolStripMenuItemRemove.Text = "このレコードを戻す";
            ToolStripMenuItemRemove.Click += ToolStripMenuItem_Click;
            // 
            // CcPanelTop
            // 
            CcPanelTop.Controls.Add(ButtonExUpdate);
            CcPanelTop.Controls.Add(CheckBoxExDeleteFlag);
            CcPanelTop.Dock = DockStyle.Fill;
            CcPanelTop.Location = new Point(3, 27);
            CcPanelTop.Name = "CcPanelTop";
            CcPanelTop.Size = new Size(1898, 54);
            CcPanelTop.TabIndex = 3;
            // 
            // ButtonExUpdate
            // 
            ButtonExUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ButtonExUpdate.ForeColor = SystemColors.ControlText;
            ButtonExUpdate.Location = new Point(1692, 12);
            ButtonExUpdate.Name = "ButtonExUpdate";
            ButtonExUpdate.SetTextDirectionVertical = "";
            ButtonExUpdate.Size = new Size(160, 32);
            ButtonExUpdate.TabIndex = 1;
            ButtonExUpdate.Text = "最　新　化";
            ButtonExUpdate.UseVisualStyleBackColor = true;
            ButtonExUpdate.Click += CcButtonUpdate_Click;
            // 
            // CheckBoxExDeleteFlag
            // 
            CheckBoxExDeleteFlag.AutoSize = true;
            CheckBoxExDeleteFlag.Location = new Point(1580, 20);
            CheckBoxExDeleteFlag.Name = "CheckBoxExDeleteFlag";
            CheckBoxExDeleteFlag.Size = new Size(95, 19);
            CheckBoxExDeleteFlag.TabIndex = 0;
            CheckBoxExDeleteFlag.Text = "削除済も表示";
            CheckBoxExDeleteFlag.UseVisualStyleBackColor = true;
            // 
            // CarList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(TableLayoutPanelExBase);
            MainMenuStrip = CcMenuStrip1;
            Name = "CarList";
            Text = "CarList";
            FormClosing += CarList_FormClosing;
            TableLayoutPanelExBase.ResumeLayout(false);
            TableLayoutPanelExBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SpreadList).EndInit();
            CcContextMenuStrip1.ResumeLayout(false);
            CcPanelTop.ResumeLayout(false);
            CcPanelTop.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private CcControl.CcTableLayoutPanel TableLayoutPanelExBase;
        private CcControl.CcMenuStrip CcMenuStrip1;
        private CcControl.CcStatusStrip CcStatusStrip1;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private CcControl.CcPanel CcPanelTop;
        private CcControl.CcCheckBox CheckBoxExDeleteFlag;
        private CcControl.CcButton ButtonExUpdate;
        private CcControl.CcContextMenuStrip CcContextMenuStrip1;
        private ToolStripMenuItem ToolStripMenuItemDelete;
        private ToolStripMenuItem ToolStripMenuItemRemove;
        private FarPoint.Win.Spread.SheetView SheetViewList;
        private FarPoint.Win.Spread.SheetView SheetViewList東京都運輸事業者向け燃料費高騰緊急対策事業支援金;
        private FarPoint.Win.Spread.SheetView SheetViewList緊急通行車両;
    }
}