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
            this.TableLayoutPanelEx1 = new ControlEx.TableLayoutPanelEx();
            this.MenuStripEx1 = new ControlEx.MenuStripEx();
            this.StatusStripEx1 = new ControlEx.StatusStripEx();
            this.SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelEx1.Controls"));
            this.SheetViewList = this.SpreadList.GetSheet(0);
            this.SheetViewList東京都運輸事業者向け燃料費高騰緊急対策事業支援金 = this.SpreadList.GetSheet(1);
            this.PanelExUp = new ControlEx.PanelEx();
            this.ButtonExUpdate = new ControlEx.ButtonEx();
            this.CheckBoxExDeleteFlag = new ControlEx.CheckBoxEx();
            this.TableLayoutPanelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadList).BeginInit();
            this.PanelExUp.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableLayoutPanelEx1
            // 
            this.TableLayoutPanelEx1.ColumnCount = 1;
            this.TableLayoutPanelEx1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelEx1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            this.TableLayoutPanelEx1.Controls.Add(this.MenuStripEx1, 0, 0);
            this.TableLayoutPanelEx1.Controls.Add(this.StatusStripEx1, 0, 3);
            this.TableLayoutPanelEx1.Controls.Add(this.SpreadList, 0, 2);
            this.TableLayoutPanelEx1.Controls.Add(this.PanelExUp, 0, 1);
            this.TableLayoutPanelEx1.Dock = DockStyle.Fill;
            this.TableLayoutPanelEx1.Location = new Point(0, 0);
            this.TableLayoutPanelEx1.Name = "TableLayoutPanelEx1";
            this.TableLayoutPanelEx1.RowCount = 4;
            this.TableLayoutPanelEx1.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelEx1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            this.TableLayoutPanelEx1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelEx1.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelEx1.Size = new Size(1904, 1041);
            this.TableLayoutPanelEx1.TabIndex = 0;
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
            this.SpreadList.Dock = DockStyle.Fill;
            this.SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F);
            this.SpreadList.Location = new Point(3, 87);
            this.SpreadList.Name = "SpreadList";
            this.SpreadList.Size = new Size(1898, 927);
            this.SpreadList.TabIndex = 2;
            this.SpreadList.CellDoubleClick += this.SpreadList_CellDoubleClick;
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
            this.Controls.Add(this.TableLayoutPanelEx1);
            this.MainMenuStrip = this.MenuStripEx1;
            this.Name = "CarList";
            this.Text = "CarList";
            this.FormClosing += this.CarList_FormClosing;
            this.TableLayoutPanelEx1.ResumeLayout(false);
            this.TableLayoutPanelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadList).EndInit();
            this.PanelExUp.ResumeLayout(false);
            this.PanelExUp.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private ControlEx.TableLayoutPanelEx TableLayoutPanelEx1;
        private ControlEx.MenuStripEx MenuStripEx1;
        private ControlEx.StatusStripEx StatusStripEx1;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private ControlEx.PanelEx PanelExUp;
        private ControlEx.CheckBoxEx CheckBoxExDeleteFlag;
        private ControlEx.ButtonEx ButtonExUpdate;
        private FarPoint.Win.Spread.SheetView SheetViewList;
        private FarPoint.Win.Spread.SheetView SheetViewList東京都運輸事業者向け燃料費高騰緊急対策事業支援金;
    }
}