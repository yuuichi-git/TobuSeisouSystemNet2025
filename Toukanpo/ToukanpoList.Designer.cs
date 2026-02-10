namespace Toukanpo {
    partial class ToukanpoList {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToukanpoList));
            this.TableLayoutPanelExBase = new ControlEx.CcTableLayoutPanel();
            this.MenuStripEx1 = new ControlEx.CcMenuStrip();
            this.StatusStripEx1 = new ControlEx.CcStatusStrip();
            this.SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls"));
            this.SheetViewList = this.SpreadList.GetSheet(0);
            this.PanelExUp = new ControlEx.CcPanel();
            this.ButtonExUpdate = new ControlEx.CcButton();
            this.TabControlEx1 = new ControlEx.TabControlEx();
            this.tabPage1 = new TabPage();
            this.tabPage2 = new TabPage();
            this.tabPage3 = new TabPage();
            this.tabPage4 = new TabPage();
            this.tabPage5 = new TabPage();
            this.tabPage6 = new TabPage();
            this.tabPage7 = new TabPage();
            this.tabPage8 = new TabPage();
            this.tabPage9 = new TabPage();
            this.tabPage10 = new TabPage();
            this.tabPage11 = new TabPage();
            this.TableLayoutPanelExBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadList).BeginInit();
            this.PanelExUp.SuspendLayout();
            this.TabControlEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableLayoutPanelExBase
            // 
            this.TableLayoutPanelExBase.ColumnCount = 1;
            this.TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelExBase.Controls.Add(this.MenuStripEx1, 0, 0);
            this.TableLayoutPanelExBase.Controls.Add(this.StatusStripEx1, 0, 4);
            this.TableLayoutPanelExBase.Controls.Add(this.SpreadList, 0, 3);
            this.TableLayoutPanelExBase.Controls.Add(this.PanelExUp, 0, 1);
            this.TableLayoutPanelExBase.Controls.Add(this.TabControlEx1, 0, 2);
            this.TableLayoutPanelExBase.Dock = DockStyle.Fill;
            this.TableLayoutPanelExBase.Location = new Point(0, 0);
            this.TableLayoutPanelExBase.Name = "TableLayoutPanelExBase";
            this.TableLayoutPanelExBase.RowCount = 5;
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.Size = new Size(1234, 961);
            this.TableLayoutPanelExBase.TabIndex = 0;
            // 
            // MenuStripEx1
            // 
            this.MenuStripEx1.Location = new Point(0, 0);
            this.MenuStripEx1.Name = "MenuStripEx1";
            this.MenuStripEx1.Size = new Size(1234, 24);
            this.MenuStripEx1.TabIndex = 0;
            this.MenuStripEx1.Text = "menuStripEx1";
            this.MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // StatusStripEx1
            // 
            this.StatusStripEx1.Location = new Point(0, 939);
            this.StatusStripEx1.Name = "StatusStripEx1";
            this.StatusStripEx1.Size = new Size(1234, 22);
            this.StatusStripEx1.TabIndex = 1;
            this.StatusStripEx1.Text = "statusStripEx1";
            // 
            // SpreadList
            // 
            this.SpreadList.AccessibleDescription = "SpreadList, LicenseList, Row 0, Column 0";
            this.SpreadList.Dock = DockStyle.Fill;
            this.SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F);
            this.SpreadList.Location = new Point(3, 119);
            this.SpreadList.Name = "SpreadList";
            this.SpreadList.Size = new Size(1228, 815);
            this.SpreadList.TabIndex = 2;
            this.SpreadList.CellDoubleClick += this.SpreadList_CellDoubleClick;
            // 
            // PanelExUp
            // 
            this.PanelExUp.Controls.Add(this.ButtonExUpdate);
            this.PanelExUp.Dock = DockStyle.Fill;
            this.PanelExUp.Location = new Point(3, 27);
            this.PanelExUp.Name = "PanelExUp";
            this.PanelExUp.Size = new Size(1228, 54);
            this.PanelExUp.TabIndex = 3;
            // 
            // ButtonExUpdate
            // 
            this.ButtonExUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.ButtonExUpdate.Location = new Point(1030, 10);
            this.ButtonExUpdate.Name = "ButtonExUpdate";
            this.ButtonExUpdate.SetTextDirectionVertical = "";
            this.ButtonExUpdate.Size = new Size(160, 32);
            this.ButtonExUpdate.TabIndex = 0;
            this.ButtonExUpdate.Text = "最　新　化";
            this.ButtonExUpdate.UseVisualStyleBackColor = true;
            this.ButtonExUpdate.Click += this.ButtonExUpdate_Click;
            // 
            // TabControlEx1
            // 
            this.TabControlEx1.Controls.Add(this.tabPage1);
            this.TabControlEx1.Controls.Add(this.tabPage2);
            this.TabControlEx1.Controls.Add(this.tabPage3);
            this.TabControlEx1.Controls.Add(this.tabPage4);
            this.TabControlEx1.Controls.Add(this.tabPage5);
            this.TabControlEx1.Controls.Add(this.tabPage6);
            this.TabControlEx1.Controls.Add(this.tabPage7);
            this.TabControlEx1.Controls.Add(this.tabPage8);
            this.TabControlEx1.Controls.Add(this.tabPage9);
            this.TabControlEx1.Controls.Add(this.tabPage10);
            this.TabControlEx1.Controls.Add(this.tabPage11);
            this.TabControlEx1.Dock = DockStyle.Fill;
            this.TabControlEx1.Location = new Point(3, 87);
            this.TabControlEx1.Name = "TabControlEx1";
            this.TabControlEx1.SelectedIndex = 0;
            this.TabControlEx1.Size = new Size(1228, 26);
            this.TabControlEx1.TabIndex = 4;
            this.TabControlEx1.Click += this.TabControlEx1_Click;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(1220, 0);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "全て";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3);
            this.tabPage2.Size = new Size(1221, 0);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "あ行";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new Size(1221, 0);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "か行";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new Size(1221, 0);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "さ行";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new Point(4, 24);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new Size(1221, 0);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "た行";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new Point(4, 24);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new Size(1221, 0);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "な行";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Location = new Point(4, 24);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new Size(1221, 0);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "は行";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // tabPage8
            // 
            this.tabPage8.Location = new Point(4, 24);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new Size(1221, 0);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "ま行";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // tabPage9
            // 
            this.tabPage9.Location = new Point(4, 24);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Size = new Size(1221, 0);
            this.tabPage9.TabIndex = 8;
            this.tabPage9.Text = "や行";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // tabPage10
            // 
            this.tabPage10.Location = new Point(4, 24);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Size = new Size(1221, 0);
            this.tabPage10.TabIndex = 9;
            this.tabPage10.Text = "ら行";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // tabPage11
            // 
            this.tabPage11.Location = new Point(4, 24);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Size = new Size(1221, 0);
            this.tabPage11.TabIndex = 10;
            this.tabPage11.Text = "わ行";
            this.tabPage11.UseVisualStyleBackColor = true;
            // 
            // ToukanpoList
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1234, 961);
            this.Controls.Add(this.TableLayoutPanelExBase);
            this.MainMenuStrip = this.MenuStripEx1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ToukanpoList";
            this.Text = "ToukanpoList";
            this.FormClosing += this.ToukanpoList_FormClosing;
            this.TableLayoutPanelExBase.ResumeLayout(false);
            this.TableLayoutPanelExBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadList).EndInit();
            this.PanelExUp.ResumeLayout(false);
            this.TabControlEx1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private ControlEx.CcTableLayoutPanel TableLayoutPanelExBase;
        private ControlEx.CcMenuStrip MenuStripEx1;
        private ControlEx.CcStatusStrip StatusStripEx1;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private ControlEx.CcPanel PanelExUp;
        private ControlEx.TabControlEx TabControlEx1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private TabPage tabPage6;
        private TabPage tabPage7;
        private TabPage tabPage8;
        private TabPage tabPage9;
        private TabPage tabPage10;
        private TabPage tabPage11;
        private ControlEx.CcButton ButtonExUpdate;
        private FarPoint.Win.Spread.SheetView SheetViewList;
    }
}