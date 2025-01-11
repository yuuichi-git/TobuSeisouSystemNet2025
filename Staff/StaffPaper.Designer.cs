namespace Staff {
    partial class StaffPaper {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaffPaper));
            this.TableLayoutPanelExBase = new ControlEx.TableLayoutPanelEx();
            this.MenuStripEx1 = new ControlEx.MenuStripEx();
            this.StatusStripEx1 = new ControlEx.StatusStripEx();
            this.SpreadStaffRegisterHead = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls"));
            this.SheetViewHead = this.SpreadStaffRegisterHead.GetSheet(0);
            this.SpreadStaffRegisterTail = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls1"));
            this.SheetViewTail = this.SpreadStaffRegisterTail.GetSheet(0);
            this.TableLayoutPanelExBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadStaffRegisterHead).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.SpreadStaffRegisterTail).BeginInit();
            this.SuspendLayout();
            // 
            // TableLayoutPanelExBase
            // 
            this.TableLayoutPanelExBase.ColumnCount = 2;
            this.TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.TableLayoutPanelExBase.Controls.Add(this.MenuStripEx1, 0, 0);
            this.TableLayoutPanelExBase.Controls.Add(this.StatusStripEx1, 0, 2);
            this.TableLayoutPanelExBase.Controls.Add(this.SpreadStaffRegisterHead, 0, 1);
            this.TableLayoutPanelExBase.Controls.Add(this.SpreadStaffRegisterTail, 1, 1);
            this.TableLayoutPanelExBase.Dock = DockStyle.Fill;
            this.TableLayoutPanelExBase.Location = new Point(0, 0);
            this.TableLayoutPanelExBase.Name = "TableLayoutPanelExBase";
            this.TableLayoutPanelExBase.RowCount = 3;
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            this.TableLayoutPanelExBase.Size = new Size(1634, 961);
            this.TableLayoutPanelExBase.TabIndex = 0;
            // 
            // MenuStripEx1
            // 
            this.TableLayoutPanelExBase.SetColumnSpan(this.MenuStripEx1, 2);
            this.MenuStripEx1.Location = new Point(0, 0);
            this.MenuStripEx1.Name = "MenuStripEx1";
            this.MenuStripEx1.Size = new Size(1634, 24);
            this.MenuStripEx1.TabIndex = 0;
            this.MenuStripEx1.Text = "menuStripEx1";
            this.MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // StatusStripEx1
            // 
            this.TableLayoutPanelExBase.SetColumnSpan(this.StatusStripEx1, 2);
            this.StatusStripEx1.Location = new Point(0, 939);
            this.StatusStripEx1.Name = "StatusStripEx1";
            this.StatusStripEx1.Size = new Size(1634, 22);
            this.StatusStripEx1.TabIndex = 1;
            this.StatusStripEx1.Text = "statusStripEx1";
            // 
            // SpreadStaffRegisterHead
            // 
            this.SpreadStaffRegisterHead.AccessibleDescription = "Book1, Sheet1, Row 0, Column 0";
            this.SpreadStaffRegisterHead.Dock = DockStyle.Fill;
            this.SpreadStaffRegisterHead.Font = new Font("ＭＳ Ｐゴシック", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.SpreadStaffRegisterHead.Location = new Point(3, 27);
            this.SpreadStaffRegisterHead.Name = "SpreadStaffRegisterHead";
            this.SpreadStaffRegisterHead.Size = new Size(811, 907);
            this.SpreadStaffRegisterHead.TabIndex = 2;
            // 
            // SpreadStaffRegisterTail
            // 
            this.SpreadStaffRegisterTail.AccessibleDescription = "Book1, Sheet1, Row 0, Column 0";
            this.SpreadStaffRegisterTail.Dock = DockStyle.Fill;
            this.SpreadStaffRegisterTail.Font = new Font("ＭＳ Ｐゴシック", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.SpreadStaffRegisterTail.Location = new Point(820, 27);
            this.SpreadStaffRegisterTail.Name = "SpreadStaffRegisterTail";
            this.SpreadStaffRegisterTail.Size = new Size(811, 907);
            this.SpreadStaffRegisterTail.TabIndex = 3;
            // 
            // StaffPaper
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1634, 961);
            this.Controls.Add(this.TableLayoutPanelExBase);
            this.MainMenuStrip = this.MenuStripEx1;
            this.Name = "StaffPaper";
            this.Text = "StaffPaper";
            this.FormClosing += this.StaffPaper_FormClosing;
            this.TableLayoutPanelExBase.ResumeLayout(false);
            this.TableLayoutPanelExBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadStaffRegisterHead).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.SpreadStaffRegisterTail).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private ControlEx.TableLayoutPanelEx TableLayoutPanelExBase;
        private ControlEx.MenuStripEx MenuStripEx1;
        private ControlEx.StatusStripEx StatusStripEx1;
        private FarPoint.Win.Spread.FpSpread SpreadStaffRegisterHead;
        private FarPoint.Win.Spread.FpSpread SpreadStaffRegisterTail;
        private FarPoint.Win.Spread.SheetView SheetViewHead;
        private FarPoint.Win.Spread.SheetView SheetViewTail;
    }
}