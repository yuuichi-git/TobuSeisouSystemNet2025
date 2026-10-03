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
            TableLayoutPanelExBase = new CcControl.CcTableLayoutPanel();
            CcMenuStrip1 = new CcControl.CcMenuStrip();
            CcStatusStrip1 = new CcControl.CcStatusStrip();
            SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls"));
            PanelExUp = new CcControl.CcPanel();
            ButtonExUpdate = new CcControl.CcButton();
            TabControlEx1 = new CcControl.CcTabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            tabPage4 = new TabPage();
            tabPage5 = new TabPage();
            tabPage6 = new TabPage();
            tabPage7 = new TabPage();
            tabPage8 = new TabPage();
            tabPage9 = new TabPage();
            tabPage10 = new TabPage();
            tabPage11 = new TabPage();
            SheetViewList = SpreadList.GetSheet(0);
            TableLayoutPanelExBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SpreadList).BeginInit();
            PanelExUp.SuspendLayout();
            TabControlEx1.SuspendLayout();
            SuspendLayout();
            // 
            // TableLayoutPanelExBase
            // 
            TableLayoutPanelExBase.ColumnCount = 3;
            TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 320F));
            TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 320F));
            TableLayoutPanelExBase.Controls.Add(SpreadList, 1, 3);
            TableLayoutPanelExBase.Controls.Add(TabControlEx1, 1, 2);
            TableLayoutPanelExBase.Controls.Add(CcMenuStrip1, 0, 0);
            TableLayoutPanelExBase.Controls.Add(PanelExUp, 0, 1);
            TableLayoutPanelExBase.Controls.Add(CcStatusStrip1, 0, 4);
            TableLayoutPanelExBase.Dock = DockStyle.Fill;
            TableLayoutPanelExBase.Location = new Point(0, 0);
            TableLayoutPanelExBase.Name = "TableLayoutPanelExBase";
            TableLayoutPanelExBase.RowCount = 5;
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            TableLayoutPanelExBase.Size = new Size(1904, 1041);
            TableLayoutPanelExBase.TabIndex = 0;
            // 
            // CcMenuStrip1
            // 
            TableLayoutPanelExBase.SetColumnSpan(CcMenuStrip1, 3);
            CcMenuStrip1.Location = new Point(0, 0);
            CcMenuStrip1.Name = "CcMenuStrip1";
            CcMenuStrip1.Size = new Size(1904, 24);
            CcMenuStrip1.TabIndex = 0;
            CcMenuStrip1.Text = "menuStripEx1";
            CcMenuStrip1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // CcStatusStrip1
            // 
            TableLayoutPanelExBase.SetColumnSpan(CcStatusStrip1, 3);
            CcStatusStrip1.Location = new Point(0, 1019);
            CcStatusStrip1.Name = "CcStatusStrip1";
            CcStatusStrip1.Size = new Size(1904, 22);
            CcStatusStrip1.SizingGrip = false;
            CcStatusStrip1.TabIndex = 1;
            CcStatusStrip1.Text = "statusStripEx1";
            // 
            // SpreadList
            // 
            SpreadList.AccessibleDescription = "SpreadList, LicenseList, Row 0, Column 0";
            SpreadList.Dock = DockStyle.Fill;
            SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F);
            SpreadList.Location = new Point(323, 119);
            SpreadList.Name = "SpreadList";
            SpreadList.Size = new Size(1258, 895);
            SpreadList.TabIndex = 2;
            SpreadList.CellDoubleClick += SpreadList_CellDoubleClick;
            // 
            // PanelExUp
            // 
            TableLayoutPanelExBase.SetColumnSpan(PanelExUp, 3);
            PanelExUp.Controls.Add(ButtonExUpdate);
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
            ButtonExUpdate.Location = new Point(1672, 10);
            ButtonExUpdate.Name = "ButtonExUpdate";
            ButtonExUpdate.SetTextDirectionVertical = "";
            ButtonExUpdate.Size = new Size(180, 32);
            ButtonExUpdate.TabIndex = 0;
            ButtonExUpdate.Text = "最　新　化";
            ButtonExUpdate.UseVisualStyleBackColor = true;
            ButtonExUpdate.Click += ButtonExUpdate_Click;
            // 
            // TabControlEx1
            // 
            TabControlEx1.Controls.Add(tabPage1);
            TabControlEx1.Controls.Add(tabPage2);
            TabControlEx1.Controls.Add(tabPage3);
            TabControlEx1.Controls.Add(tabPage4);
            TabControlEx1.Controls.Add(tabPage5);
            TabControlEx1.Controls.Add(tabPage6);
            TabControlEx1.Controls.Add(tabPage7);
            TabControlEx1.Controls.Add(tabPage8);
            TabControlEx1.Controls.Add(tabPage9);
            TabControlEx1.Controls.Add(tabPage10);
            TabControlEx1.Controls.Add(tabPage11);
            TabControlEx1.Dock = DockStyle.Fill;
            TabControlEx1.Location = new Point(323, 87);
            TabControlEx1.Name = "TabControlEx1";
            TabControlEx1.SelectedIndex = 0;
            TabControlEx1.Size = new Size(1258, 26);
            TabControlEx1.TabIndex = 4;
            TabControlEx1.Click += TabControlEx1_Click;
            // 
            // tabPage1
            // 
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1250, 0);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "全て";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1220, 0);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "あ行";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(1220, 0);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "か行";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new Size(1220, 0);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "さ行";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            tabPage5.Location = new Point(4, 24);
            tabPage5.Name = "tabPage5";
            tabPage5.Size = new Size(1220, 0);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "た行";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            tabPage6.Location = new Point(4, 24);
            tabPage6.Name = "tabPage6";
            tabPage6.Size = new Size(1220, 0);
            tabPage6.TabIndex = 5;
            tabPage6.Text = "な行";
            tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            tabPage7.Location = new Point(4, 24);
            tabPage7.Name = "tabPage7";
            tabPage7.Size = new Size(1220, 0);
            tabPage7.TabIndex = 6;
            tabPage7.Text = "は行";
            tabPage7.UseVisualStyleBackColor = true;
            // 
            // tabPage8
            // 
            tabPage8.Location = new Point(4, 24);
            tabPage8.Name = "tabPage8";
            tabPage8.Size = new Size(1220, 0);
            tabPage8.TabIndex = 7;
            tabPage8.Text = "ま行";
            tabPage8.UseVisualStyleBackColor = true;
            // 
            // tabPage9
            // 
            tabPage9.Location = new Point(4, 24);
            tabPage9.Name = "tabPage9";
            tabPage9.Size = new Size(1220, 0);
            tabPage9.TabIndex = 8;
            tabPage9.Text = "や行";
            tabPage9.UseVisualStyleBackColor = true;
            // 
            // tabPage10
            // 
            tabPage10.Location = new Point(4, 24);
            tabPage10.Name = "tabPage10";
            tabPage10.Size = new Size(1220, 0);
            tabPage10.TabIndex = 9;
            tabPage10.Text = "ら行";
            tabPage10.UseVisualStyleBackColor = true;
            // 
            // tabPage11
            // 
            tabPage11.Location = new Point(4, 24);
            tabPage11.Name = "tabPage11";
            tabPage11.Size = new Size(1220, 0);
            tabPage11.TabIndex = 10;
            tabPage11.Text = "わ行";
            tabPage11.UseVisualStyleBackColor = true;
            // 
            // ToukanpoList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(TableLayoutPanelExBase);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = CcMenuStrip1;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ToukanpoList";
            Text = "ToukanpoList";
            FormClosing += ToukanpoList_FormClosing;
            TableLayoutPanelExBase.ResumeLayout(false);
            TableLayoutPanelExBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SpreadList).EndInit();
            PanelExUp.ResumeLayout(false);
            TabControlEx1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private CcControl.CcTableLayoutPanel TableLayoutPanelExBase;
        private CcControl.CcMenuStrip CcMenuStrip1;
        private CcControl.CcStatusStrip CcStatusStrip1;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private CcControl.CcPanel PanelExUp;
        private CcControl.CcTabControl TabControlEx1;
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
        private CcControl.CcButton ButtonExUpdate;
        private FarPoint.Win.Spread.SheetView SheetViewList;
    }
}