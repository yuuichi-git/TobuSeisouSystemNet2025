namespace Seisou {
    partial class OracleAllTable {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OracleAllTable));
            this.TableLayoutPanelExBase = new ControlEx.CcTableLayoutPanel();
            this.StatusStripEx1 = new ControlEx.CcStatusStrip();
            this.MenuStripEx1 = new ControlEx.CcMenuStrip();
            this.PanelExTop = new ControlEx.CcPanel();
            this.TreeViewEx1 = new ControlEx.TreeViewEx();
            this.SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls"));
            this.SheetViewList = this.SpreadList.GetSheet(0);
            this.TableLayoutPanelExBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadList).BeginInit();
            this.SuspendLayout();
            // 
            // TableLayoutPanelExBase
            // 
            this.TableLayoutPanelExBase.ColumnCount = 2;
            this.TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300F));
            this.TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelExBase.Controls.Add(this.StatusStripEx1, 0, 3);
            this.TableLayoutPanelExBase.Controls.Add(this.MenuStripEx1, 0, 0);
            this.TableLayoutPanelExBase.Controls.Add(this.PanelExTop, 0, 1);
            this.TableLayoutPanelExBase.Controls.Add(this.TreeViewEx1, 0, 2);
            this.TableLayoutPanelExBase.Controls.Add(this.SpreadList, 1, 2);
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
            // StatusStripEx1
            // 
            this.TableLayoutPanelExBase.SetColumnSpan(this.StatusStripEx1, 2);
            this.StatusStripEx1.Location = new Point(0, 1019);
            this.StatusStripEx1.Name = "StatusStripEx1";
            this.StatusStripEx1.Size = new Size(1904, 22);
            this.StatusStripEx1.TabIndex = 2;
            this.StatusStripEx1.Text = "statusStripEx1";
            // 
            // MenuStripEx1
            // 
            this.TableLayoutPanelExBase.SetColumnSpan(this.MenuStripEx1, 2);
            this.MenuStripEx1.Location = new Point(0, 0);
            this.MenuStripEx1.Name = "MenuStripEx1";
            this.MenuStripEx1.Size = new Size(1904, 24);
            this.MenuStripEx1.TabIndex = 1;
            this.MenuStripEx1.Text = "menuStripEx1";
            this.MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // PanelExTop
            // 
            this.TableLayoutPanelExBase.SetColumnSpan(this.PanelExTop, 2);
            this.PanelExTop.Dock = DockStyle.Fill;
            this.PanelExTop.Location = new Point(3, 27);
            this.PanelExTop.Name = "PanelExTop";
            this.PanelExTop.Size = new Size(1898, 54);
            this.PanelExTop.TabIndex = 3;
            // 
            // TreeViewEx1
            // 
            this.TreeViewEx1.Dock = DockStyle.Fill;
            this.TreeViewEx1.Location = new Point(3, 87);
            this.TreeViewEx1.Name = "TreeViewEx1";
            this.TreeViewEx1.Size = new Size(294, 927);
            this.TreeViewEx1.TabIndex = 4;
            // 
            // SpreadList
            // 
            this.SpreadList.AccessibleDescription = "Book1, Sheet1, Row 0, Column 0";
            this.SpreadList.Dock = DockStyle.Fill;
            this.SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F);
            this.SpreadList.Location = new Point(303, 87);
            this.SpreadList.Name = "SpreadList";
            this.SpreadList.Size = new Size(1598, 927);
            this.SpreadList.TabIndex = 5;
            // 
            // OracleAllTable
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1904, 1041);
            this.Controls.Add(this.TableLayoutPanelExBase);
            this.MainMenuStrip = this.MenuStripEx1;
            this.Name = "OracleAllTable";
            this.Text = "OracleAllTable";
            this.FormClosing += this.OracleAllTable_FormClosing;
            this.TableLayoutPanelExBase.ResumeLayout(false);
            this.TableLayoutPanelExBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadList).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private ControlEx.CcTableLayoutPanel TableLayoutPanelExBase;
        private ControlEx.CcStatusStrip StatusStripEx1;
        private ControlEx.CcMenuStrip MenuStripEx1;
        private ControlEx.CcPanel PanelExTop;
        private ControlEx.TreeViewEx TreeViewEx1;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private FarPoint.Win.Spread.SheetView SheetViewList;
    }
}
