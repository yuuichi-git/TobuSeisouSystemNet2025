﻿namespace Substitute {
    partial class SubstituteSheet {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubstituteSheet));
            this.TableLayoutPanelExBase = new ControlEx.TableLayoutPanelEx();
            this.MenuStripEx1 = new ControlEx.MenuStripEx();
            this.StatusStripEx1 = new ControlEx.StatusStripEx();
            this.PanelExUp = new ControlEx.PanelEx();
            this.ButtonExPrint = new ControlEx.ButtonEx();
            this.LabelExFaxNumber = new ControlEx.LabelEx();
            this.labelEx1 = new ControlEx.LabelEx();
            this.ComboBoxExPrinterName = new ControlEx.ComboBoxEx();
            this.SpreadSubstitute = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls"));
            this.SheetView1 = this.SpreadSubstitute.GetSheet(0);
            this.SheetView2 = this.SpreadSubstitute.GetSheet(1);
            this.TableLayoutPanelExBase.SuspendLayout();
            this.PanelExUp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadSubstitute).BeginInit();
            this.SuspendLayout();
            // 
            // TableLayoutPanelExBase
            // 
            this.TableLayoutPanelExBase.ColumnCount = 1;
            this.TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            this.TableLayoutPanelExBase.Controls.Add(this.MenuStripEx1, 0, 0);
            this.TableLayoutPanelExBase.Controls.Add(this.StatusStripEx1, 0, 3);
            this.TableLayoutPanelExBase.Controls.Add(this.PanelExUp, 0, 1);
            this.TableLayoutPanelExBase.Controls.Add(this.SpreadSubstitute, 0, 2);
            this.TableLayoutPanelExBase.Dock = DockStyle.Fill;
            this.TableLayoutPanelExBase.Location = new Point(0, 0);
            this.TableLayoutPanelExBase.Name = "TableLayoutPanelExBase";
            this.TableLayoutPanelExBase.RowCount = 4;
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.Size = new Size(1036, 961);
            this.TableLayoutPanelExBase.TabIndex = 0;
            // 
            // MenuStripEx1
            // 
            this.MenuStripEx1.Location = new Point(0, 0);
            this.MenuStripEx1.Name = "MenuStripEx1";
            this.MenuStripEx1.Size = new Size(1036, 24);
            this.MenuStripEx1.TabIndex = 0;
            this.MenuStripEx1.Text = "menuStripEx1";
            this.MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // StatusStripEx1
            // 
            this.StatusStripEx1.Location = new Point(0, 939);
            this.StatusStripEx1.Name = "StatusStripEx1";
            this.StatusStripEx1.Size = new Size(1036, 22);
            this.StatusStripEx1.TabIndex = 1;
            this.StatusStripEx1.Text = "statusStripEx1";
            // 
            // PanelExUp
            // 
            this.PanelExUp.Controls.Add(this.ButtonExPrint);
            this.PanelExUp.Controls.Add(this.LabelExFaxNumber);
            this.PanelExUp.Controls.Add(this.labelEx1);
            this.PanelExUp.Controls.Add(this.ComboBoxExPrinterName);
            this.PanelExUp.Dock = DockStyle.Fill;
            this.PanelExUp.Location = new Point(3, 27);
            this.PanelExUp.Name = "PanelExUp";
            this.PanelExUp.Size = new Size(1030, 54);
            this.PanelExUp.TabIndex = 2;
            // 
            // ButtonExPrint
            // 
            this.ButtonExPrint.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.ButtonExPrint.Location = new Point(804, 12);
            this.ButtonExPrint.Name = "ButtonExPrint";
            this.ButtonExPrint.SetTextDirectionVertical = "";
            this.ButtonExPrint.Size = new Size(192, 32);
            this.ButtonExPrint.TabIndex = 3;
            this.ButtonExPrint.Text = "印刷する";
            this.ButtonExPrint.UseVisualStyleBackColor = true;
            this.ButtonExPrint.Click += this.ButtonExPrint_Click;
            // 
            // LabelExFaxNumber
            // 
            this.LabelExFaxNumber.BorderStyle = BorderStyle.FixedSingle;
            this.LabelExFaxNumber.Font = new Font("Yu Gothic UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            this.LabelExFaxNumber.Location = new Point(312, 4);
            this.LabelExFaxNumber.Name = "LabelExFaxNumber";
            this.LabelExFaxNumber.Size = new Size(428, 44);
            this.LabelExFaxNumber.TabIndex = 2;
            this.LabelExFaxNumber.Text = "足立清掃事務所\r\nFAX 03-8888-8888";
            this.LabelExFaxNumber.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelEx1
            // 
            this.labelEx1.AutoSize = true;
            this.labelEx1.Location = new Point(28, 20);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.Size = new Size(43, 15);
            this.labelEx1.TabIndex = 1;
            this.labelEx1.Text = "出力先";
            // 
            // ComboBoxExPrinterName
            // 
            this.ComboBoxExPrinterName.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ComboBoxExPrinterName.FormattingEnabled = true;
            this.ComboBoxExPrinterName.Location = new Point(76, 16);
            this.ComboBoxExPrinterName.Name = "ComboBoxExPrinterName";
            this.ComboBoxExPrinterName.Size = new Size(212, 23);
            this.ComboBoxExPrinterName.TabIndex = 0;
            // 
            // SpreadSubstitute
            // 
            this.SpreadSubstitute.AccessibleDescription = "Book1, 共通, Row 0, Column 0";
            this.SpreadSubstitute.Dock = DockStyle.Fill;
            this.SpreadSubstitute.Font = new Font("ＭＳ Ｐゴシック", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.SpreadSubstitute.Location = new Point(3, 87);
            this.SpreadSubstitute.Name = "SpreadSubstitute";
            this.SpreadSubstitute.Size = new Size(1030, 847);
            this.SpreadSubstitute.TabIndex = 3;
            // 
            // SubstituteSheet
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1036, 961);
            this.Controls.Add(this.TableLayoutPanelExBase);
            this.MainMenuStrip = this.MenuStripEx1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SubstituteSheet";
            this.Text = "SubstituteSheet";
            this.FormClosing += this.SubstituteSheet_FormClosing;
            this.TableLayoutPanelExBase.ResumeLayout(false);
            this.TableLayoutPanelExBase.PerformLayout();
            this.PanelExUp.ResumeLayout(false);
            this.PanelExUp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadSubstitute).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private ControlEx.TableLayoutPanelEx TableLayoutPanelExBase;
        private ControlEx.MenuStripEx MenuStripEx1;
        private ControlEx.StatusStripEx StatusStripEx1;
        private ControlEx.PanelEx PanelExUp;
        private FarPoint.Win.Spread.FpSpread SpreadSubstitute;
        private FarPoint.Win.Spread.SheetView SheetView1;
        private FarPoint.Win.Spread.SheetView SheetView2;
        private ControlEx.LabelEx labelEx1;
        private ControlEx.ComboBoxEx ComboBoxExPrinterName;
        private ControlEx.LabelEx LabelExFaxNumber;
        private ControlEx.ButtonEx ButtonExPrint;
    }
}