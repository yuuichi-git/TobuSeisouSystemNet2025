namespace DriversReport {
    partial class DriversReportPaper {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DriversReportPaper));
            this.TableLayoutPanelExBase = new ControlEx.TableLayoutPanelEx();
            this.MenuStripEx1 = new ControlEx.MenuStripEx();
            this.StatusStripEx1 = new ControlEx.StatusStripEx();
            this.PanelExUp = new ControlEx.PanelEx();
            this.labelEx1 = new ControlEx.LabelEx();
            this.ComboBoxExPrinterName = new ControlEx.ComboBoxEx();
            this.SpreadDriversReportPaper = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls"));
            this.SheetViewDriversReport = this.SpreadDriversReportPaper.GetSheet(0);
            this.TableLayoutPanelExBase.SuspendLayout();
            this.PanelExUp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadDriversReportPaper).BeginInit();
            this.SuspendLayout();
            // 
            // TableLayoutPanelExBase
            // 
            this.TableLayoutPanelExBase.ColumnCount = 1;
            this.TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelExBase.Controls.Add(this.MenuStripEx1, 0, 0);
            this.TableLayoutPanelExBase.Controls.Add(this.StatusStripEx1, 0, 3);
            this.TableLayoutPanelExBase.Controls.Add(this.PanelExUp, 0, 1);
            this.TableLayoutPanelExBase.Controls.Add(this.SpreadDriversReportPaper, 0, 2);
            this.TableLayoutPanelExBase.Dock = DockStyle.Fill;
            this.TableLayoutPanelExBase.Location = new Point(0, 0);
            this.TableLayoutPanelExBase.Name = "TableLayoutPanelExBase";
            this.TableLayoutPanelExBase.RowCount = 4;
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.Size = new Size(1174, 961);
            this.TableLayoutPanelExBase.TabIndex = 0;
            // 
            // MenuStripEx1
            // 
            this.MenuStripEx1.Location = new Point(0, 0);
            this.MenuStripEx1.Name = "MenuStripEx1";
            this.MenuStripEx1.Size = new Size(1174, 24);
            this.MenuStripEx1.TabIndex = 0;
            this.MenuStripEx1.Text = "menuStripEx1";
            this.MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // StatusStripEx1
            // 
            this.StatusStripEx1.Location = new Point(0, 939);
            this.StatusStripEx1.Name = "StatusStripEx1";
            this.StatusStripEx1.Size = new Size(1174, 22);
            this.StatusStripEx1.TabIndex = 1;
            this.StatusStripEx1.Text = "statusStripEx1";
            // 
            // PanelExUp
            // 
            this.PanelExUp.Controls.Add(this.labelEx1);
            this.PanelExUp.Controls.Add(this.ComboBoxExPrinterName);
            this.PanelExUp.Dock = DockStyle.Fill;
            this.PanelExUp.Location = new Point(3, 27);
            this.PanelExUp.Name = "PanelExUp";
            this.PanelExUp.Size = new Size(1168, 54);
            this.PanelExUp.TabIndex = 2;
            // 
            // labelEx1
            // 
            this.labelEx1.AutoSize = true;
            this.labelEx1.Location = new Point(28, 20);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.Size = new Size(43, 15);
            this.labelEx1.TabIndex = 3;
            this.labelEx1.Text = "出力先";
            // 
            // ComboBoxExPrinterName
            // 
            this.ComboBoxExPrinterName.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ComboBoxExPrinterName.FormattingEnabled = true;
            this.ComboBoxExPrinterName.Location = new Point(76, 16);
            this.ComboBoxExPrinterName.Name = "ComboBoxExPrinterName";
            this.ComboBoxExPrinterName.Size = new Size(212, 23);
            this.ComboBoxExPrinterName.TabIndex = 2;
            // 
            // SpreadDriversReportPaper
            // 
            this.SpreadDriversReportPaper.AccessibleDescription = "SpreadDriversReportPaper, Sheet1, Row 0, Column 0";
            this.SpreadDriversReportPaper.Dock = DockStyle.Fill;
            this.SpreadDriversReportPaper.Font = new Font("ＭＳ Ｐゴシック", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.SpreadDriversReportPaper.Location = new Point(3, 87);
            this.SpreadDriversReportPaper.Name = "SpreadDriversReportPaper";
            this.SpreadDriversReportPaper.Size = new Size(1168, 847);
            this.SpreadDriversReportPaper.TabIndex = 3;
            // 
            // DriversReportPaper
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1174, 961);
            this.Controls.Add(this.TableLayoutPanelExBase);
            this.MainMenuStrip = this.MenuStripEx1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DriversReportPaper";
            this.Text = "DriversReportPaper";
            this.FormClosing += this.DriversReportPaper_FormClosing;
            this.TableLayoutPanelExBase.ResumeLayout(false);
            this.TableLayoutPanelExBase.PerformLayout();
            this.PanelExUp.ResumeLayout(false);
            this.PanelExUp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadDriversReportPaper).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private ControlEx.TableLayoutPanelEx TableLayoutPanelExBase;
        private ControlEx.MenuStripEx MenuStripEx1;
        private ControlEx.StatusStripEx StatusStripEx1;
        private ControlEx.PanelEx PanelExUp;
        private FarPoint.Win.Spread.FpSpread SpreadDriversReportPaper;
        private ControlEx.LabelEx labelEx1;
        private ControlEx.ComboBoxEx ComboBoxExPrinterName;
        private FarPoint.Win.Spread.SheetView SheetViewDriversReport;
    }
}