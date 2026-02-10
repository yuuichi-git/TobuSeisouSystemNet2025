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
            this.TableLayoutPanelExBase = new ControlEx.CcTableLayoutPanel();
            this.MenuStripEx1 = new ControlEx.CcMenuStrip();
            this.StatusStripEx1 = new ControlEx.CcStatusStrip();
            this.SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls"));
            this.ContextMenuStripEx1 = new ControlEx.ContextMenuStripEx();
            this.ToolStripMenuItemDelete = new ToolStripMenuItem();
            this.ToolStripMenuItemRemove = new ToolStripMenuItem();
            this.SheetViewList = this.SpreadList.GetSheet(0);
            this.SheetViewList東京都運輸事業者向け燃料費高騰緊急対策事業支援金 = this.SpreadList.GetSheet(1);
            this.PanelExUp = new ControlEx.CcPanel();
            this.ButtonExUpdate = new ControlEx.CcButton();
            this.CheckBoxExDeleteFlag = new ControlEx.CheckBoxEx();
            this.TableLayoutPanelExBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadList).BeginInit();
            this.ContextMenuStripEx1.SuspendLayout();
            this.PanelExUp.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableLayoutPanelExBase
            // 
            this.TableLayoutPanelExBase.ColumnCount = 1;
            this.TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            this.TableLayoutPanelExBase.Controls.Add(this.MenuStripEx1, 0, 0);
            this.TableLayoutPanelExBase.Controls.Add(this.StatusStripEx1, 0, 3);
            this.TableLayoutPanelExBase.Controls.Add(this.SpreadList, 0, 2);
            this.TableLayoutPanelExBase.Controls.Add(this.PanelExUp, 0, 1);
            this.TableLayoutPanelExBase.Dock = DockStyle.Fill;
            this.TableLayoutPanelExBase.Location = new Point(0, 0);
            this.TableLayoutPanelExBase.Name = "TableLayoutPanelExBase";
            this.TableLayoutPanelExBase.RowCount = 4;
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.Size = new Size(1904, 1041);
            this.TableLayoutPanelExBase.TabIndex = 0;
            // 
            // MenuStripEx1
            // 
            this.MenuStripEx1.Location = new Point(0, 0);
            this.MenuStripEx1.Name = "MenuStripEx1";
            this.MenuStripEx1.Size = new Size(1904, 24);
            this.MenuStripEx1.TabIndex = 0;
            this.MenuStripEx1.Text = "menuStripEx1";
            this.MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // StatusStripEx1
            // 
            this.StatusStripEx1.Location = new Point(0, 1019);
            this.StatusStripEx1.Name = "StatusStripEx1";
            this.StatusStripEx1.Size = new Size(1904, 22);
            this.StatusStripEx1.TabIndex = 1;
            this.StatusStripEx1.Text = "statusStripEx1";
            // 
            // SpreadList
            // 
            this.SpreadList.AccessibleDescription = "SpreadList, 車両台帳, Row 0, Column 0";
            this.SpreadList.ContextMenuStrip = this.ContextMenuStripEx1;
            this.SpreadList.Dock = DockStyle.Fill;
            this.SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F);
            this.SpreadList.Location = new Point(3, 87);
            this.SpreadList.Name = "SpreadList";
            this.SpreadList.Size = new Size(1898, 927);
            this.SpreadList.TabIndex = 2;
            this.SpreadList.CellDoubleClick += this.SpreadList_CellDoubleClick;
            // 
            // ContextMenuStripEx1
            // 
            this.ContextMenuStripEx1.Items.AddRange(new ToolStripItem[] { this.ToolStripMenuItemDelete, this.ToolStripMenuItemRemove });
            this.ContextMenuStripEx1.Name = "ContextMenuStripEx1";
            this.ContextMenuStripEx1.Size = new Size(178, 48);
            this.ContextMenuStripEx1.Opening += this.ContextMenuStripEx1_Opening;
            // 
            // ToolStripMenuItemDelete
            // 
            this.ToolStripMenuItemDelete.Name = "ToolStripMenuItemDelete";
            this.ToolStripMenuItemDelete.Size = new Size(177, 22);
            this.ToolStripMenuItemDelete.Text = "このレコードを削除する";
            this.ToolStripMenuItemDelete.Click += this.ToolStripMenuItem_Click;
            // 
            // ToolStripMenuItemRemove
            // 
            this.ToolStripMenuItemRemove.Name = "ToolStripMenuItemRemove";
            this.ToolStripMenuItemRemove.Size = new Size(177, 22);
            this.ToolStripMenuItemRemove.Text = "このレコードを戻す";
            this.ToolStripMenuItemRemove.Click += this.ToolStripMenuItem_Click;
            // 
            // PanelExUp
            // 
            this.PanelExUp.Controls.Add(this.ButtonExUpdate);
            this.PanelExUp.Controls.Add(this.CheckBoxExDeleteFlag);
            this.PanelExUp.Dock = DockStyle.Fill;
            this.PanelExUp.Location = new Point(3, 27);
            this.PanelExUp.Name = "PanelExUp";
            this.PanelExUp.Size = new Size(1898, 54);
            this.PanelExUp.TabIndex = 3;
            // 
            // ButtonExUpdate
            // 
            this.ButtonExUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.ButtonExUpdate.Location = new Point(1692, 12);
            this.ButtonExUpdate.Name = "ButtonExUpdate";
            this.ButtonExUpdate.SetTextDirectionVertical = "";
            this.ButtonExUpdate.Size = new Size(160, 32);
            this.ButtonExUpdate.TabIndex = 1;
            this.ButtonExUpdate.Text = "最　新　化";
            this.ButtonExUpdate.UseVisualStyleBackColor = true;
            this.ButtonExUpdate.Click += this.ButtonExUpdate_Click;
            // 
            // CheckBoxExDeleteFlag
            // 
            this.CheckBoxExDeleteFlag.AutoSize = true;
            this.CheckBoxExDeleteFlag.Location = new Point(1580, 20);
            this.CheckBoxExDeleteFlag.Name = "CheckBoxExDeleteFlag";
            this.CheckBoxExDeleteFlag.Size = new Size(95, 19);
            this.CheckBoxExDeleteFlag.TabIndex = 0;
            this.CheckBoxExDeleteFlag.Text = "削除済も表示";
            this.CheckBoxExDeleteFlag.UseVisualStyleBackColor = true;
            // 
            // CarList
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1904, 1041);
            this.Controls.Add(this.TableLayoutPanelExBase);
            this.MainMenuStrip = this.MenuStripEx1;
            this.Name = "CarList";
            this.Text = "CarList";
            this.FormClosing += this.CarList_FormClosing;
            this.TableLayoutPanelExBase.ResumeLayout(false);
            this.TableLayoutPanelExBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadList).EndInit();
            this.ContextMenuStripEx1.ResumeLayout(false);
            this.PanelExUp.ResumeLayout(false);
            this.PanelExUp.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private ControlEx.CcTableLayoutPanel TableLayoutPanelExBase;
        private ControlEx.CcMenuStrip MenuStripEx1;
        private ControlEx.CcStatusStrip StatusStripEx1;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private ControlEx.CcPanel PanelExUp;
        private ControlEx.CheckBoxEx CheckBoxExDeleteFlag;
        private ControlEx.CcButton ButtonExUpdate;
        private FarPoint.Win.Spread.SheetView SheetViewList;
        private FarPoint.Win.Spread.SheetView SheetViewList東京都運輸事業者向け燃料費高騰緊急対策事業支援金;
        private ControlEx.ContextMenuStripEx ContextMenuStripEx1;
        private ToolStripMenuItem ToolStripMenuItemDelete;
        private ToolStripMenuItem ToolStripMenuItemRemove;
    }
}