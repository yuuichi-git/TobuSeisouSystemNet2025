namespace License {
    partial class LicenseCard {
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
            this.TableLayoutPanelExBase = new ControlEx.CcTableLayoutPanel();
            this.MenuStripEx1 = new ControlEx.CcMenuStrip();
            this.StatusStripEx1 = new ControlEx.CcStatusStrip();
            this.PictureBoxExHead = new ControlEx.CcPictureBox();
            this.PictureBoxExTail = new ControlEx.CcPictureBox();
            this.TableLayoutPanelExBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.PictureBoxExHead).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.PictureBoxExTail).BeginInit();
            this.SuspendLayout();
            // 
            // TableLayoutPanelExBase
            // 
            this.TableLayoutPanelExBase.ColumnCount = 1;
            this.TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelExBase.Controls.Add(this.MenuStripEx1, 0, 0);
            this.TableLayoutPanelExBase.Controls.Add(this.StatusStripEx1, 0, 3);
            this.TableLayoutPanelExBase.Controls.Add(this.PictureBoxExHead, 0, 1);
            this.TableLayoutPanelExBase.Controls.Add(this.PictureBoxExTail, 0, 2);
            this.TableLayoutPanelExBase.Dock = DockStyle.Fill;
            this.TableLayoutPanelExBase.Location = new Point(0, 0);
            this.TableLayoutPanelExBase.Name = "TableLayoutPanelExBase";
            this.TableLayoutPanelExBase.RowCount = 4;
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.Size = new Size(492, 684);
            this.TableLayoutPanelExBase.TabIndex = 0;
            // 
            // MenuStripEx1
            // 
            this.MenuStripEx1.Location = new Point(0, 0);
            this.MenuStripEx1.Name = "MenuStripEx1";
            this.MenuStripEx1.Size = new Size(492, 24);
            this.MenuStripEx1.TabIndex = 1;
            this.MenuStripEx1.Text = "menuStripEx1";
            this.MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // StatusStripEx1
            // 
            this.StatusStripEx1.Location = new Point(0, 662);
            this.StatusStripEx1.Name = "StatusStripEx1";
            this.StatusStripEx1.Size = new Size(492, 22);
            this.StatusStripEx1.SizingGrip = false;
            this.StatusStripEx1.TabIndex = 0;
            this.StatusStripEx1.Text = "statusStripEx1";
            // 
            // PictureBoxExHead
            // 
            this.PictureBoxExHead.BorderStyle = BorderStyle.Fixed3D;
            this.PictureBoxExHead.Dock = DockStyle.Fill;
            this.PictureBoxExHead.Location = new Point(3, 27);
            this.PictureBoxExHead.Name = "PictureBoxExHead";
            this.PictureBoxExHead.Size = new Size(486, 312);
            this.PictureBoxExHead.SizeMode = PictureBoxSizeMode.Zoom;
            this.PictureBoxExHead.TabIndex = 2;
            this.PictureBoxExHead.TabStop = false;
            // 
            // PictureBoxExTail
            // 
            this.PictureBoxExTail.BorderStyle = BorderStyle.Fixed3D;
            this.PictureBoxExTail.Dock = DockStyle.Fill;
            this.PictureBoxExTail.Location = new Point(3, 345);
            this.PictureBoxExTail.Name = "PictureBoxExTail";
            this.PictureBoxExTail.Size = new Size(486, 312);
            this.PictureBoxExTail.SizeMode = PictureBoxSizeMode.Zoom;
            this.PictureBoxExTail.TabIndex = 3;
            this.PictureBoxExTail.TabStop = false;
            // 
            // LicenseCard
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(492, 684);
            this.Controls.Add(this.TableLayoutPanelExBase);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.MenuStripEx1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LicenseCard";
            this.StartPosition = FormStartPosition.Manual;
            this.Text = "LicenseCard";
            this.FormClosing += this.LicenseCard_FormClosing;
            this.TableLayoutPanelExBase.ResumeLayout(false);
            this.TableLayoutPanelExBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.PictureBoxExHead).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.PictureBoxExTail).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private ControlEx.CcTableLayoutPanel TableLayoutPanelExBase;
        private ControlEx.CcMenuStrip MenuStripEx1;
        private ControlEx.CcStatusStrip StatusStripEx1;
        private ControlEx.CcPictureBox PictureBoxExHead;
        private ControlEx.CcPictureBox PictureBoxExTail;
    }
}