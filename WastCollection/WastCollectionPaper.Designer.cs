namespace WastCollection {
    partial class WastCollectionPaper {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WastCollectionPaper));
            this.CcTableLayoutPanelBase = new ControlEx.CcTableLayoutPanel();
            this.StatusStripEx1 = new ControlEx.StatusStripEx();
            this.CcPictureBox1 = new ControlEx.CcPictureBox();
            this.MenuStripEx1 = new ControlEx.MenuStripEx();
            this.CcTableLayoutPanelBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.CcPictureBox1).BeginInit();
            this.SuspendLayout();
            // 
            // CcTableLayoutPanelBase
            // 
            this.CcTableLayoutPanelBase.ColumnCount = 1;
            this.CcTableLayoutPanelBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.CcTableLayoutPanelBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            this.CcTableLayoutPanelBase.Controls.Add(this.StatusStripEx1, 0, 2);
            this.CcTableLayoutPanelBase.Controls.Add(this.CcPictureBox1, 0, 1);
            this.CcTableLayoutPanelBase.Controls.Add(this.MenuStripEx1, 0, 0);
            this.CcTableLayoutPanelBase.Dock = DockStyle.Fill;
            this.CcTableLayoutPanelBase.Location = new Point(0, 0);
            this.CcTableLayoutPanelBase.Name = "CcTableLayoutPanelBase";
            this.CcTableLayoutPanelBase.RowCount = 3;
            this.CcTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.CcTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.CcTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.CcTableLayoutPanelBase.Size = new Size(575, 861);
            this.CcTableLayoutPanelBase.TabIndex = 0;
            // 
            // StatusStripEx1
            // 
            this.StatusStripEx1.Location = new Point(0, 839);
            this.StatusStripEx1.Name = "StatusStripEx1";
            this.StatusStripEx1.Size = new Size(575, 22);
            this.StatusStripEx1.TabIndex = 2;
            this.StatusStripEx1.Text = "statusStripEx1";
            // 
            // CcPictureBox1
            // 
            this.CcPictureBox1.BorderStyle = BorderStyle.Fixed3D;
            this.CcPictureBox1.Dock = DockStyle.Fill;
            this.CcPictureBox1.Image = (Image)resources.GetObject("CcPictureBox1.Image");
            this.CcPictureBox1.Location = new Point(3, 27);
            this.CcPictureBox1.Name = "CcPictureBox1";
            this.CcPictureBox1.Size = new Size(569, 807);
            this.CcPictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.CcPictureBox1.TabIndex = 0;
            this.CcPictureBox1.TabStop = false;
            // 
            // MenuStripEx1
            // 
            this.MenuStripEx1.Location = new Point(0, 0);
            this.MenuStripEx1.Name = "MenuStripEx1";
            this.MenuStripEx1.Size = new Size(575, 24);
            this.MenuStripEx1.TabIndex = 1;
            this.MenuStripEx1.Text = "menuStripEx1";
            this.MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // WastCollectionPaper
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(575, 861);
            this.Controls.Add(this.CcTableLayoutPanelBase);
            this.MainMenuStrip = this.MenuStripEx1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WastCollectionPaper";
            this.Text = "WastCollectionPaper";
            this.FormClosing += this.WastCollectionPaper_FormClosing;
            this.CcTableLayoutPanelBase.ResumeLayout(false);
            this.CcTableLayoutPanelBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.CcPictureBox1).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private ControlEx.CcTableLayoutPanel CcTableLayoutPanelBase;
        private ControlEx.StatusStripEx StatusStripEx1;
        private ControlEx.CcPictureBox CcPictureBox1;
        private ControlEx.MenuStripEx MenuStripEx1;
    }
}