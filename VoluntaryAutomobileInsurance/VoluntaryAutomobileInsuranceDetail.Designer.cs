namespace VoluntaryAutomobileInsurance {
    partial class VoluntaryAutomobileInsuranceDetail {
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
            this.components = new System.ComponentModel.Container();
            this.CcTableLayoutPanelBase = new CcControl.CcTableLayoutPanel();
            this.CcTabControl1 = new CcControl.CcTabControl();
            this.TabPage1 = new TabPage();
            this.TabPage2 = new TabPage();
            this.TabPage3 = new TabPage();
            this.TabPage4 = new TabPage();
            this.CcMenuStrip1 = new CcControl.CcMenuStrip();
            this.CcStatusStrip1 = new CcControl.CcStatusStrip();
            this.CcPanelTop = new CcControl.CcPanel();
            this.CcButtonUpdate = new CcControl.CcButton();
            this.CcPanelMiddle = new CcControl.CcPanel();
            this.CcDateTimePickerEndDate = new CcControl.CcDateTime();
            this.CcDateTimePickerStartDate = new CcControl.CcDateTime();
            this.ccLabel4 = new CcControl.CcLabel();
            this.ccLabel3 = new CcControl.CcLabel();
            this.ccLabel2 = new CcControl.CcLabel();
            this.CcComboBoxCompanyName = new CcControl.CcComboBox();
            this.ccLabel1 = new CcControl.CcLabel();
            this.CcComboBoxVehicleType = new CcControl.CcComboBox();
            this.CcContextMenuStrip1 = new CcControl.ContextMenuStripEx();
            this.ToolStripMenuItemOpen = new ToolStripMenuItem();
            this.ToolStripMenuItemDelete = new ToolStripMenuItem();
            this.flowLayout1 = new Syncfusion.Windows.Forms.Tools.FlowLayout(this.components);
            this.CcTableLayoutPanelBase.SuspendLayout();
            this.CcTabControl1.SuspendLayout();
            this.CcPanelTop.SuspendLayout();
            this.CcPanelMiddle.SuspendLayout();
            this.CcContextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.flowLayout1).BeginInit();
            this.SuspendLayout();
            // 
            // CcTableLayoutPanelBase
            // 
            this.CcTableLayoutPanelBase.ColumnCount = 2;
            this.CcTableLayoutPanelBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 410F));
            this.CcTableLayoutPanelBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.CcTableLayoutPanelBase.Controls.Add(this.CcTabControl1, 1, 2);
            this.CcTableLayoutPanelBase.Controls.Add(this.CcMenuStrip1, 0, 0);
            this.CcTableLayoutPanelBase.Controls.Add(this.CcStatusStrip1, 0, 3);
            this.CcTableLayoutPanelBase.Controls.Add(this.CcPanelTop, 0, 1);
            this.CcTableLayoutPanelBase.Controls.Add(this.CcPanelMiddle, 0, 2);
            this.CcTableLayoutPanelBase.Dock = DockStyle.Fill;
            this.CcTableLayoutPanelBase.Location = new Point(0, 0);
            this.CcTableLayoutPanelBase.Name = "CcTableLayoutPanelBase";
            this.CcTableLayoutPanelBase.RowCount = 4;
            this.CcTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.CcTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            this.CcTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.CcTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.CcTableLayoutPanelBase.Size = new Size(1389, 1041);
            this.CcTableLayoutPanelBase.TabIndex = 0;
            // 
            // CcTabControl1
            // 
            this.CcTabControl1.Controls.Add(this.TabPage1);
            this.CcTabControl1.Controls.Add(this.TabPage2);
            this.CcTabControl1.Controls.Add(this.TabPage3);
            this.CcTabControl1.Controls.Add(this.TabPage4);
            this.CcTabControl1.Dock = DockStyle.Fill;
            this.CcTabControl1.Location = new Point(413, 87);
            this.CcTabControl1.Name = "CcTabControl1";
            this.CcTabControl1.SelectedIndex = 0;
            this.CcTabControl1.Size = new Size(973, 927);
            this.CcTabControl1.SizeMode = TabSizeMode.Fixed;
            this.CcTabControl1.TabIndex = 8;
            // 
            // TabPage1
            // 
            this.TabPage1.Location = new Point(4, 24);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new Padding(3);
            this.TabPage1.Size = new Size(965, 899);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Tag = "Route";
            this.TabPage1.Text = "経路図";
            this.TabPage1.UseVisualStyleBackColor = true;
            // 
            // TabPage2
            // 
            this.TabPage2.Location = new Point(4, 24);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new Padding(3);
            this.TabPage2.Size = new Size(965, 899);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Tag = "Compulsory";
            this.TabPage2.Text = "自賠責";
            this.TabPage2.UseVisualStyleBackColor = true;
            // 
            // TabPage3
            // 
            this.TabPage3.Location = new Point(4, 24);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new Size(965, 899);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Tag = "Voluntary";
            this.TabPage3.Text = "任意保険";
            this.TabPage3.UseVisualStyleBackColor = true;
            // 
            // TabPage4
            // 
            this.TabPage4.Location = new Point(4, 24);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new Size(965, 899);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Tag = "WorkCommutinPermit";
            this.TabPage4.Text = "通勤許可証";
            this.TabPage4.UseVisualStyleBackColor = true;
            // 
            // CcMenuStrip1
            // 
            this.CcTableLayoutPanelBase.SetColumnSpan(this.CcMenuStrip1, 2);
            this.CcMenuStrip1.Location = new Point(0, 0);
            this.CcMenuStrip1.Name = "CcMenuStrip1";
            this.CcMenuStrip1.Size = new Size(1389, 24);
            this.CcMenuStrip1.TabIndex = 0;
            this.CcMenuStrip1.Text = "ccMenuStrip1";
            this.CcMenuStrip1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // CcStatusStrip1
            // 
            this.CcTableLayoutPanelBase.SetColumnSpan(this.CcStatusStrip1, 2);
            this.CcStatusStrip1.Location = new Point(0, 1019);
            this.CcStatusStrip1.Name = "CcStatusStrip1";
            this.CcStatusStrip1.Size = new Size(1389, 22);
            this.CcStatusStrip1.TabIndex = 1;
            this.CcStatusStrip1.Text = "ccStatusStrip1";
            // 
            // CcPanelTop
            // 
            this.CcTableLayoutPanelBase.SetColumnSpan(this.CcPanelTop, 2);
            this.CcPanelTop.Controls.Add(this.CcButtonUpdate);
            this.CcPanelTop.Dock = DockStyle.Fill;
            this.CcPanelTop.Location = new Point(3, 27);
            this.CcPanelTop.Name = "CcPanelTop";
            this.CcPanelTop.Size = new Size(1383, 54);
            this.CcPanelTop.TabIndex = 2;
            // 
            // CcButtonUpdate
            // 
            this.CcButtonUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.CcButtonUpdate.Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            this.CcButtonUpdate.ForeColor = SystemColors.ControlText;
            this.CcButtonUpdate.Location = new Point(1152, 10);
            this.CcButtonUpdate.Name = "CcButtonUpdate";
            this.CcButtonUpdate.SetTextDirectionVertical = "";
            this.CcButtonUpdate.Size = new Size(184, 32);
            this.CcButtonUpdate.TabIndex = 5;
            this.CcButtonUpdate.Text = "UPDATE";
            this.CcButtonUpdate.UseVisualStyleBackColor = true;
            this.CcButtonUpdate.Click += this.CcButtonUpdate_Click;
            // 
            // CcPanelMiddle
            // 
            this.CcPanelMiddle.Controls.Add(this.CcDateTimePickerEndDate);
            this.CcPanelMiddle.Controls.Add(this.CcDateTimePickerStartDate);
            this.CcPanelMiddle.Controls.Add(this.ccLabel4);
            this.CcPanelMiddle.Controls.Add(this.ccLabel3);
            this.CcPanelMiddle.Controls.Add(this.ccLabel2);
            this.CcPanelMiddle.Controls.Add(this.CcComboBoxCompanyName);
            this.CcPanelMiddle.Controls.Add(this.ccLabel1);
            this.CcPanelMiddle.Controls.Add(this.CcComboBoxVehicleType);
            this.CcPanelMiddle.Dock = DockStyle.Fill;
            this.CcPanelMiddle.Location = new Point(3, 87);
            this.CcPanelMiddle.Name = "CcPanelMiddle";
            this.CcPanelMiddle.Size = new Size(404, 927);
            this.CcPanelMiddle.TabIndex = 3;
            // 
            // CcDateTimePickerEndDate
            // 
            this.CcDateTimePickerEndDate.CultureFlag = false;
            this.CcDateTimePickerEndDate.CustomFormat = " 明治33年01月01日(月曜日)";
            this.CcDateTimePickerEndDate.Format = DateTimePickerFormat.Custom;
            this.CcDateTimePickerEndDate.Location = new Point(104, 116);
            this.CcDateTimePickerEndDate.Name = "CcDateTimePickerEndDate";
            this.CcDateTimePickerEndDate.Size = new Size(180, 23);
            this.CcDateTimePickerEndDate.TabIndex = 7;
            this.CcDateTimePickerEndDate.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // CcDateTimePickerStartDate
            // 
            this.CcDateTimePickerStartDate.CultureFlag = false;
            this.CcDateTimePickerStartDate.CustomFormat = " 明治33年01月01日(月曜日)";
            this.CcDateTimePickerStartDate.Format = DateTimePickerFormat.Custom;
            this.CcDateTimePickerStartDate.Location = new Point(104, 88);
            this.CcDateTimePickerStartDate.Name = "CcDateTimePickerStartDate";
            this.CcDateTimePickerStartDate.Size = new Size(180, 23);
            this.CcDateTimePickerStartDate.TabIndex = 6;
            this.CcDateTimePickerStartDate.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // ccLabel4
            // 
            this.ccLabel4.Location = new Point(8, 120);
            this.ccLabel4.Name = "ccLabel4";
            this.ccLabel4.Size = new Size(92, 20);
            this.ccLabel4.TabIndex = 5;
            this.ccLabel4.Text = "契約終了日";
            this.ccLabel4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ccLabel3
            // 
            this.ccLabel3.Location = new Point(8, 92);
            this.ccLabel3.Name = "ccLabel3";
            this.ccLabel3.Size = new Size(92, 20);
            this.ccLabel3.TabIndex = 4;
            this.ccLabel3.Text = "契約開始日";
            this.ccLabel3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ccLabel2
            // 
            this.ccLabel2.Location = new Point(8, 62);
            this.ccLabel2.Name = "ccLabel2";
            this.ccLabel2.Size = new Size(92, 20);
            this.ccLabel2.TabIndex = 3;
            this.ccLabel2.Text = "保険会社名";
            this.ccLabel2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // CcComboBoxCompanyName
            // 
            this.CcComboBoxCompanyName.FormattingEnabled = true;
            this.CcComboBoxCompanyName.Location = new Point(104, 60);
            this.CcComboBoxCompanyName.Name = "CcComboBoxCompanyName";
            this.CcComboBoxCompanyName.Size = new Size(288, 23);
            this.CcComboBoxCompanyName.TabIndex = 2;
            // 
            // ccLabel1
            // 
            this.ccLabel1.Location = new Point(8, 34);
            this.ccLabel1.Name = "ccLabel1";
            this.ccLabel1.Size = new Size(92, 20);
            this.ccLabel1.TabIndex = 1;
            this.ccLabel1.Text = "対象車両種別";
            this.ccLabel1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // CcComboBoxVehicleType
            // 
            this.CcComboBoxVehicleType.FormattingEnabled = true;
            this.CcComboBoxVehicleType.Items.AddRange(new object[] { "原付", "小型二輪", "自動二輪", "自家用軽四輪", "自家用小型乗用車", "自家用普通乗用車", "営業用軽貨物", "営業用小型貨物", "営業用普通貨物" });
            this.CcComboBoxVehicleType.Location = new Point(104, 32);
            this.CcComboBoxVehicleType.Name = "CcComboBoxVehicleType";
            this.CcComboBoxVehicleType.Size = new Size(288, 23);
            this.CcComboBoxVehicleType.TabIndex = 0;
            // 
            // CcContextMenuStrip1
            // 
            this.CcContextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.ToolStripMenuItemOpen, this.ToolStripMenuItemDelete });
            this.CcContextMenuStrip1.Name = "CcContextMenuStrip1";
            this.CcContextMenuStrip1.Size = new Size(133, 48);
            this.CcContextMenuStrip1.ItemClicked += this.ContextMenuStripEx_ItemClicked;
            // 
            // ToolStripMenuItemOpen
            // 
            this.ToolStripMenuItemOpen.Name = "ToolStripMenuItemOpen";
            this.ToolStripMenuItemOpen.Size = new Size(132, 22);
            this.ToolStripMenuItemOpen.Text = "Open(PDF)";
            // 
            // ToolStripMenuItemDelete
            // 
            this.ToolStripMenuItemDelete.Name = "ToolStripMenuItemDelete";
            this.ToolStripMenuItemDelete.Size = new Size(132, 22);
            this.ToolStripMenuItemDelete.Text = "Delete";
            // 
            // VoluntaryAutomobileInsuranceDetail
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1389, 1041);
            this.Controls.Add(this.CcTableLayoutPanelBase);
            this.MainMenuStrip = this.CcMenuStrip1;
            this.Name = "VoluntaryAutomobileInsuranceDetail";
            this.Text = "VoluntaryAutomobileInsuranceDetail";
            this.FormClosing += this.VoluntaryAutomobileInsuranceDetail_FormClosing;
            this.CcTableLayoutPanelBase.ResumeLayout(false);
            this.CcTableLayoutPanelBase.PerformLayout();
            this.CcTabControl1.ResumeLayout(false);
            this.CcPanelTop.ResumeLayout(false);
            this.CcPanelMiddle.ResumeLayout(false);
            this.CcContextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.flowLayout1).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private CcControl.CcTableLayoutPanel CcTableLayoutPanelBase;
        private CcControl.CcMenuStrip CcMenuStrip1;
        private CcControl.CcStatusStrip CcStatusStrip1;
        private CcControl.CcPanel CcPanelTop;
        private CcControl.CcButton CcButtonUpdate;
        private CcControl.CcPanel CcPanelMiddle;
        private CcControl.CcLabel ccLabel1;
        private CcControl.CcComboBox CcComboBoxVehicleType;
        private CcControl.CcLabel ccLabel4;
        private CcControl.CcLabel ccLabel3;
        private CcControl.CcLabel ccLabel2;
        private CcControl.CcComboBox CcComboBoxCompanyName;
        private CcControl.CcDateTime CcDateTimePickerEndDate;
        private CcControl.CcDateTime CcDateTimePickerStartDate;
        private CcControl.CcTabControl CcTabControl1;
        private TabPage TabPage1;
        private TabPage TabPage2;
        private TabPage TabPage3;
        private TabPage TabPage4;
        private CcControl.ContextMenuStripEx CcContextMenuStrip1;
        private ToolStripMenuItem ToolStripMenuItemOpen;
        private ToolStripMenuItem ToolStripMenuItemDelete;
        private Syncfusion.Windows.Forms.Tools.FlowLayout flowLayout1;
    }
}