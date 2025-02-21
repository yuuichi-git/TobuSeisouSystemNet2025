namespace License {
    partial class LicenseList {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LicenseList));
            this.TableLayoutPanelExBase = new ControlEx.TableLayoutPanelEx();
            this.TabControlExKana = new ControlEx.TabControlEx();
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
            this.MenuStripEx1 = new ControlEx.MenuStripEx();
            this.StatusStripEx1 = new ControlEx.StatusStripEx();
            this.PanelExUp = new ControlEx.PanelEx();
            this.CheckBoxExRetirementFlag = new ControlEx.CheckBoxEx();
            this.ButtonExUpdate = new ControlEx.ButtonEx();
            this.SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls"));
            this.SheetViewList = this.SpreadList.GetSheet(0);
            this.SheetViewToukaidenshi = this.SpreadList.GetSheet(1);
            this.TableLayoutPanelExBase.SuspendLayout();
            this.TabControlExKana.SuspendLayout();
            this.PanelExUp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadList).BeginInit();
            this.SuspendLayout();
            // 
            // TableLayoutPanelExBase
            // 
            this.TableLayoutPanelExBase.ColumnCount = 1;
            this.TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelExBase.Controls.Add(this.TabControlExKana, 0, 2);
            this.TableLayoutPanelExBase.Controls.Add(this.MenuStripEx1, 0, 0);
            this.TableLayoutPanelExBase.Controls.Add(this.StatusStripEx1, 0, 4);
            this.TableLayoutPanelExBase.Controls.Add(this.PanelExUp, 0, 1);
            this.TableLayoutPanelExBase.Controls.Add(this.SpreadList, 0, 3);
            this.TableLayoutPanelExBase.Dock = DockStyle.Fill;
            this.TableLayoutPanelExBase.Location = new Point(0, 0);
            this.TableLayoutPanelExBase.Name = "TableLayoutPanelExBase";
            this.TableLayoutPanelExBase.RowCount = 5;
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.Size = new Size(1904, 1041);
            this.TableLayoutPanelExBase.TabIndex = 0;
            // 
            // TabControlExKana
            // 
            this.TabControlExKana.Controls.Add(this.tabPage1);
            this.TabControlExKana.Controls.Add(this.tabPage2);
            this.TabControlExKana.Controls.Add(this.tabPage3);
            this.TabControlExKana.Controls.Add(this.tabPage4);
            this.TabControlExKana.Controls.Add(this.tabPage5);
            this.TabControlExKana.Controls.Add(this.tabPage6);
            this.TabControlExKana.Controls.Add(this.tabPage7);
            this.TabControlExKana.Controls.Add(this.tabPage8);
            this.TabControlExKana.Controls.Add(this.tabPage9);
            this.TabControlExKana.Controls.Add(this.tabPage10);
            this.TabControlExKana.Controls.Add(this.tabPage11);
            this.TabControlExKana.Dock = DockStyle.Fill;
            this.TabControlExKana.Location = new Point(3, 87);
            this.TabControlExKana.Name = "TabControlExKana";
            this.TabControlExKana.SelectedIndex = 0;
            this.TabControlExKana.Size = new Size(1898, 26);
            this.TabControlExKana.TabIndex = 4;
            this.TabControlExKana.Click += this.TabControlExKana_Click;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(1890, 0);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "全て";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3);
            this.tabPage2.Size = new Size(1890, 0);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "あ行";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new Size(1890, 0);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "か行";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new Size(1890, 0);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "さ行";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new Point(4, 24);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new Size(1890, 0);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "た行";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new Point(4, 24);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new Size(1890, 0);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "な行";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Location = new Point(4, 24);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new Size(1890, 0);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "は行";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // tabPage8
            // 
            this.tabPage8.Location = new Point(4, 24);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new Size(1890, 0);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "ま行";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // tabPage9
            // 
            this.tabPage9.Location = new Point(4, 24);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Size = new Size(1890, 0);
            this.tabPage9.TabIndex = 8;
            this.tabPage9.Text = "や行";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // tabPage10
            // 
            this.tabPage10.Location = new Point(4, 24);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Size = new Size(1890, 0);
            this.tabPage10.TabIndex = 9;
            this.tabPage10.Text = "ら行";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // tabPage11
            // 
            this.tabPage11.Location = new Point(4, 24);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Size = new Size(1890, 0);
            this.tabPage11.TabIndex = 10;
            this.tabPage11.Text = "わ行";
            this.tabPage11.UseVisualStyleBackColor = true;
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
            // PanelExUp
            // 
            this.PanelExUp.Controls.Add(this.CheckBoxExRetirementFlag);
            this.PanelExUp.Controls.Add(this.ButtonExUpdate);
            this.PanelExUp.Dock = DockStyle.Fill;
            this.PanelExUp.Location = new Point(3, 27);
            this.PanelExUp.Name = "PanelExUp";
            this.PanelExUp.Size = new Size(1898, 54);
            this.PanelExUp.TabIndex = 2;
            // 
            // CheckBoxExRetirementFlag
            // 
            this.CheckBoxExRetirementFlag.AutoSize = true;
            this.CheckBoxExRetirementFlag.Location = new Point(1596, 16);
            this.CheckBoxExRetirementFlag.Name = "CheckBoxExRetirementFlag";
            this.CheckBoxExRetirementFlag.Size = new Size(95, 19);
            this.CheckBoxExRetirementFlag.TabIndex = 7;
            this.CheckBoxExRetirementFlag.Text = "退職者も表示";
            this.CheckBoxExRetirementFlag.UseVisualStyleBackColor = true;
            // 
            // ButtonExUpdate
            // 
            this.ButtonExUpdate.Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            this.ButtonExUpdate.Location = new Point(1700, 8);
            this.ButtonExUpdate.Name = "ButtonExUpdate";
            this.ButtonExUpdate.SetTextDirectionVertical = "";
            this.ButtonExUpdate.Size = new Size(160, 32);
            this.ButtonExUpdate.TabIndex = 0;
            this.ButtonExUpdate.Text = "最　新　化";
            this.ButtonExUpdate.UseVisualStyleBackColor = true;
            this.ButtonExUpdate.Click += this.ButtonExUpdate_Click;
            // 
            // SpreadList
            // 
            this.SpreadList.AccessibleDescription = "Book1, LicenseList, Row 0, Column 0";
            this.SpreadList.Dock = DockStyle.Fill;
            this.SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.SpreadList.Location = new Point(3, 119);
            this.SpreadList.Name = "SpreadList";
            this.SpreadList.Size = new Size(1898, 895);
            this.SpreadList.TabIndex = 3;
            this.SpreadList.ActiveSheetChanged += this.SpreadList_ActiveSheetChanged;
            this.SpreadList.CellDoubleClick += this.SpreadList_CellDoubleClick;
            // 
            // LicenseList
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1904, 1041);
            this.Controls.Add(this.TableLayoutPanelExBase);
            this.MainMenuStrip = this.MenuStripEx1;
            this.Name = "LicenseList";
            this.Text = "LicenseList";
            this.FormClosing += this.LicenseList_FormClosing;
            this.TableLayoutPanelExBase.ResumeLayout(false);
            this.TableLayoutPanelExBase.PerformLayout();
            this.TabControlExKana.ResumeLayout(false);
            this.PanelExUp.ResumeLayout(false);
            this.PanelExUp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadList).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private ControlEx.TableLayoutPanelEx TableLayoutPanelExBase;
        private ControlEx.MenuStripEx MenuStripEx1;
        private ControlEx.StatusStripEx StatusStripEx1;
        private ControlEx.PanelEx PanelExUp;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private ControlEx.ButtonEx ButtonExUpdate;
        private ControlEx.TabControlEx TabControlExKana;
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
        private ControlEx.CheckBoxEx CheckBoxExRetirementFlag;
        private FarPoint.Win.Spread.SheetView SheetViewList;
        private FarPoint.Win.Spread.SheetView SheetViewToukaidenshi;
    }
}