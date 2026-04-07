namespace VoluntaryAutomobileInsurance {
    partial class VoluntaryAutomobileInsuranceList {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VoluntaryAutomobileInsuranceList));
            this.CcTableLayoutPanelBase = new CcControl.CcTableLayoutPanel();
            this.CcMenuStrip1 = new CcControl.CcMenuStrip();
            this.CcStatusStrip1 = new CcControl.CcStatusStrip();
            this.CcPanelTop = new CcControl.CcPanel();
            this.ButtonExUpdate = new CcControl.CcButton();
            this.SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("CcTableLayoutPanelBase.Controls"));
            this.SheetViewList = this.SpreadList.GetSheet(0);
            this.CcTableLayoutPanelBase.SuspendLayout();
            this.CcPanelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadList).BeginInit();
            this.SuspendLayout();
            // 
            // CcTableLayoutPanelBase
            // 
            this.CcTableLayoutPanelBase.ColumnCount = 1;
            this.CcTableLayoutPanelBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.CcTableLayoutPanelBase.Controls.Add(this.CcMenuStrip1, 0, 0);
            this.CcTableLayoutPanelBase.Controls.Add(this.CcStatusStrip1, 0, 3);
            this.CcTableLayoutPanelBase.Controls.Add(this.CcPanelTop, 0, 1);
            this.CcTableLayoutPanelBase.Controls.Add(this.SpreadList, 0, 2);
            this.CcTableLayoutPanelBase.Dock = DockStyle.Fill;
            this.CcTableLayoutPanelBase.Location = new Point(0, 0);
            this.CcTableLayoutPanelBase.Name = "CcTableLayoutPanelBase";
            this.CcTableLayoutPanelBase.RowCount = 4;
            this.CcTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.CcTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            this.CcTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.CcTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.CcTableLayoutPanelBase.Size = new Size(1904, 1041);
            this.CcTableLayoutPanelBase.TabIndex = 0;
            // 
            // CcMenuStrip1
            // 
            this.CcMenuStrip1.Location = new Point(0, 0);
            this.CcMenuStrip1.Name = "CcMenuStrip1";
            this.CcMenuStrip1.Size = new Size(1904, 24);
            this.CcMenuStrip1.TabIndex = 0;
            this.CcMenuStrip1.Text = "ccMenuStrip1";
            this.CcMenuStrip1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // CcStatusStrip1
            // 
            this.CcStatusStrip1.Location = new Point(0, 1019);
            this.CcStatusStrip1.Name = "CcStatusStrip1";
            this.CcStatusStrip1.Size = new Size(1904, 22);
            this.CcStatusStrip1.SizingGrip = false;
            this.CcStatusStrip1.TabIndex = 1;
            this.CcStatusStrip1.Text = "ccStatusStrip1";
            // 
            // CcPanelTop
            // 
            this.CcPanelTop.Controls.Add(this.ButtonExUpdate);
            this.CcPanelTop.Dock = DockStyle.Fill;
            this.CcPanelTop.Location = new Point(3, 27);
            this.CcPanelTop.Name = "CcPanelTop";
            this.CcPanelTop.Size = new Size(1898, 54);
            this.CcPanelTop.TabIndex = 2;
            // 
            // ButtonExUpdate
            // 
            this.ButtonExUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.ButtonExUpdate.Font = new Font("Yu Gothic UI", 11.25F);
            this.ButtonExUpdate.ForeColor = SystemColors.ControlText;
            this.ButtonExUpdate.Location = new Point(1656, 11);
            this.ButtonExUpdate.Name = "ButtonExUpdate";
            this.ButtonExUpdate.SetTextDirectionVertical = "";
            this.ButtonExUpdate.Size = new Size(184, 32);
            this.ButtonExUpdate.TabIndex = 4;
            this.ButtonExUpdate.Text = "最　新　化";
            this.ButtonExUpdate.UseVisualStyleBackColor = true;
            this.ButtonExUpdate.Click += this.ButtonExUpdate_Click;
            // 
            // SpreadList
            // 
            this.SpreadList.AccessibleDescription = "SpreadList, Sheet1, Row 0, Column 0";
            this.SpreadList.Dock = DockStyle.Fill;
            this.SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F);
            this.SpreadList.Location = new Point(3, 87);
            this.SpreadList.Name = "SpreadList";
            this.SpreadList.Size = new Size(1898, 927);
            this.SpreadList.TabIndex = 3;
            // 
            // VoluntaryAutomobileInsuranceList
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1904, 1041);
            this.Controls.Add(this.CcTableLayoutPanelBase);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.CcMenuStrip1;
            this.Name = "VoluntaryAutomobileInsuranceList";
            this.Text = "VoluntaryAutomobileInsuranceList";
            this.CcTableLayoutPanelBase.ResumeLayout(false);
            this.CcTableLayoutPanelBase.PerformLayout();
            this.CcPanelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.SpreadList).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private CcControl.CcTableLayoutPanel CcTableLayoutPanelBase;
        private CcControl.CcMenuStrip CcMenuStrip1;
        private CcControl.CcStatusStrip CcStatusStrip1;
        private CcControl.CcPanel CcPanelTop;
        private CcControl.CcButton ButtonExUpdate;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private FarPoint.Win.Spread.SheetView SheetViewList;
    }
}
