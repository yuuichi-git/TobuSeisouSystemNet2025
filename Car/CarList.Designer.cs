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
            MenuStripEx1 = new CcControl.CcMenuStrip();
            CcStatusStrip1 = new CcControl.CcStatusStrip();
            SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls"));
            ContextMenuStripEx1 = new CcControl.CcContextMenuStrip();
            ToolStripMenuItemDelete = new ToolStripMenuItem();
            ToolStripMenuItemRemove = new ToolStripMenuItem();
            SheetViewList = SpreadList.GetSheet(0);
            SheetViewList東京都運輸事業者向け燃料費高騰緊急対策事業支援金 = SpreadList.GetSheet(1);
            PanelExUp = new CcControl.CcPanel();
            ButtonExUpdate = new CcControl.CcButton();
            CheckBoxExDeleteFlag = new CcControl.CcCheckBox();
            TableLayoutPanelExBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SpreadList).BeginInit();
            ContextMenuStripEx1.SuspendLayout();
            PanelExUp.SuspendLayout();
            SuspendLayout();
            // 
            // TableLayoutPanelExBase
            // 
            TableLayoutPanelExBase.ColumnCount = 1;
            TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            TableLayoutPanelExBase.Controls.Add(MenuStripEx1, 0, 0);
            TableLayoutPanelExBase.Controls.Add(CcStatusStrip1, 0, 3);
            TableLayoutPanelExBase.Controls.Add(SpreadList, 0, 2);
            TableLayoutPanelExBase.Controls.Add(PanelExUp, 0, 1);
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
            SpreadList.AccessibleDescription = "SpreadList, 車両台帳, Row 0, Column 0";
            SpreadList.ContextMenuStrip = ContextMenuStripEx1;
            SpreadList.Dock = DockStyle.Fill;
            SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F);
            SpreadList.Location = new Point(3, 87);
            SpreadList.Name = "SpreadList";
            SpreadList.Size = new Size(1898, 927);
            SpreadList.TabIndex = 2;
            SpreadList.CellDoubleClick += SpreadList_CellDoubleClick;
            // 
            // ContextMenuStripEx1
            // 
            ContextMenuStripEx1.Items.AddRange(new ToolStripItem[] { ToolStripMenuItemDelete, ToolStripMenuItemRemove });
            ContextMenuStripEx1.Name = "ContextMenuStripEx1";
            ContextMenuStripEx1.Size = new Size(178, 48);
            ContextMenuStripEx1.Opening += ContextMenuStripEx1_Opening;
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
            // PanelExUp
            // 
            PanelExUp.Controls.Add(ButtonExUpdate);
            PanelExUp.Controls.Add(CheckBoxExDeleteFlag);
            PanelExUp.Dock = DockStyle.Fill;
            PanelExUp.Location = new Point(3, 27);
            PanelExUp.Name = "PanelExUp";
            PanelExUp.Size = new Size(1898, 54);
            PanelExUp.TabIndex = 3;
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
            ButtonExUpdate.Click += ButtonExUpdate_Click;
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
            MainMenuStrip = MenuStripEx1;
            Name = "CarList";
            Text = "CarList";
            FormClosing += CarList_FormClosing;
            TableLayoutPanelExBase.ResumeLayout(false);
            TableLayoutPanelExBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SpreadList).EndInit();
            ContextMenuStripEx1.ResumeLayout(false);
            PanelExUp.ResumeLayout(false);
            PanelExUp.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private CcControl.CcTableLayoutPanel TableLayoutPanelExBase;
        private CcControl.CcMenuStrip MenuStripEx1;
        private CcControl.CcStatusStrip CcStatusStrip1;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private CcControl.CcPanel PanelExUp;
        private CcControl.CcCheckBox CheckBoxExDeleteFlag;
        private CcControl.CcButton ButtonExUpdate;
        private CcControl.CcContextMenuStrip ContextMenuStripEx1;
        private ToolStripMenuItem ToolStripMenuItemDelete;
        private ToolStripMenuItem ToolStripMenuItemRemove;
        private FarPoint.Win.Spread.SheetView SheetViewList;
        private FarPoint.Win.Spread.SheetView SheetViewList東京都運輸事業者向け燃料費高騰緊急対策事業支援金;
    }
}