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
            this.TableLayoutPanelEx1 = new ControlEx.TableLayoutPanelEx();
            this.MenuStripEx1 = new ControlEx.MenuStripEx();
            this.StatusStripEx1 = new ControlEx.StatusStripEx();
            this.TableLayoutPanelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableLayoutPanelEx1
            // 
            this.TableLayoutPanelEx1.ColumnCount = 1;
            this.TableLayoutPanelEx1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelEx1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            this.TableLayoutPanelEx1.Controls.Add(this.MenuStripEx1, 0, 0);
            this.TableLayoutPanelEx1.Controls.Add(this.StatusStripEx1, 0, 3);
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
            // CarList
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1904, 1041);
            this.Controls.Add(this.TableLayoutPanelEx1);
            this.MainMenuStrip = this.MenuStripEx1;
            this.Name = "CarList";
            this.Text = "CarList";
            this.TableLayoutPanelEx1.ResumeLayout(false);
            this.TableLayoutPanelEx1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private ControlEx.TableLayoutPanelEx TableLayoutPanelEx1;
        private ControlEx.MenuStripEx MenuStripEx1;
        private ControlEx.StatusStripEx StatusStripEx1;
    }
}