namespace Substitute {
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
            TableLayoutPanelExBase = new CcControl.CcTableLayoutPanel();
            MenuStripEx1 = new CcControl.CcMenuStrip();
            StatusStripEx1 = new CcControl.CcStatusStrip();
            PanelExUp = new CcControl.CcPanel();
            ButtonExPrint2 = new CcControl.CcButton();
            ButtonExPrint1 = new CcControl.CcButton();
            LabelExFaxNumber = new CcControl.CcLabel();
            labelEx1 = new CcControl.CcLabel();
            ComboBoxExPrinterName = new CcControl.CcComboBox();
            SpreadSubstitute = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls"));
            SheetView1 = SpreadSubstitute.GetSheet(0);
            SheetView2 = SpreadSubstitute.GetSheet(1);
            SheetView3 = SpreadSubstitute.GetSheet(2);
            TableLayoutPanelExBase.SuspendLayout();
            PanelExUp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SpreadSubstitute).BeginInit();
            SuspendLayout();
            // 
            // TableLayoutPanelExBase
            // 
            TableLayoutPanelExBase.ColumnCount = 3;
            TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
            TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
            TableLayoutPanelExBase.Controls.Add(MenuStripEx1, 0, 0);
            TableLayoutPanelExBase.Controls.Add(StatusStripEx1, 0, 3);
            TableLayoutPanelExBase.Controls.Add(PanelExUp, 0, 1);
            TableLayoutPanelExBase.Controls.Add(SpreadSubstitute, 1, 2);
            TableLayoutPanelExBase.Dock = DockStyle.Fill;
            TableLayoutPanelExBase.Location = new Point(0, 0);
            TableLayoutPanelExBase.Name = "TableLayoutPanelExBase";
            TableLayoutPanelExBase.RowCount = 4;
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            TableLayoutPanelExBase.Size = new Size(1904, 1041);
            TableLayoutPanelExBase.TabIndex = 0;
            // 
            // MenuStripEx1
            // 
            TableLayoutPanelExBase.SetColumnSpan(MenuStripEx1, 3);
            MenuStripEx1.Location = new Point(0, 0);
            MenuStripEx1.Name = "MenuStripEx1";
            MenuStripEx1.Size = new Size(1904, 24);
            MenuStripEx1.TabIndex = 0;
            MenuStripEx1.Text = "menuStripEx1";
            MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // StatusStripEx1
            // 
            TableLayoutPanelExBase.SetColumnSpan(StatusStripEx1, 3);
            StatusStripEx1.Location = new Point(0, 1019);
            StatusStripEx1.Name = "StatusStripEx1";
            StatusStripEx1.Size = new Size(1904, 22);
            StatusStripEx1.SizingGrip = false;
            StatusStripEx1.TabIndex = 1;
            StatusStripEx1.Text = "statusStripEx1";
            // 
            // PanelExUp
            // 
            TableLayoutPanelExBase.SetColumnSpan(PanelExUp, 3);
            PanelExUp.Controls.Add(ButtonExPrint2);
            PanelExUp.Controls.Add(ButtonExPrint1);
            PanelExUp.Controls.Add(LabelExFaxNumber);
            PanelExUp.Controls.Add(labelEx1);
            PanelExUp.Controls.Add(ComboBoxExPrinterName);
            PanelExUp.Dock = DockStyle.Fill;
            PanelExUp.Location = new Point(3, 27);
            PanelExUp.Name = "PanelExUp";
            PanelExUp.Size = new Size(1898, 54);
            PanelExUp.TabIndex = 2;
            // 
            // ButtonExPrint2
            // 
            ButtonExPrint2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ButtonExPrint2.ForeColor = SystemColors.ControlText;
            ButtonExPrint2.Location = new Point(1688, 12);
            ButtonExPrint2.Name = "ButtonExPrint2";
            ButtonExPrint2.SetTextDirectionVertical = "";
            ButtonExPrint2.Size = new Size(172, 32);
            ButtonExPrint2.TabIndex = 4;
            ButtonExPrint2.Text = "FAX(文京支部宛て)";
            ButtonExPrint2.UseVisualStyleBackColor = true;
            ButtonExPrint2.Click += ButtonExPrint2_Click;
            // 
            // ButtonExPrint1
            // 
            ButtonExPrint1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ButtonExPrint1.ForeColor = SystemColors.ControlText;
            ButtonExPrint1.Location = new Point(1508, 12);
            ButtonExPrint1.Name = "ButtonExPrint1";
            ButtonExPrint1.SetTextDirectionVertical = "";
            ButtonExPrint1.Size = new Size(172, 32);
            ButtonExPrint1.TabIndex = 3;
            ButtonExPrint1.Text = "印刷する";
            ButtonExPrint1.UseVisualStyleBackColor = true;
            ButtonExPrint1.Click += ButtonExPrint1_Click;
            // 
            // LabelExFaxNumber
            // 
            LabelExFaxNumber.BorderStyle = BorderStyle.FixedSingle;
            LabelExFaxNumber.Font = new Font("Yu Gothic UI", 11.25F);
            LabelExFaxNumber.Location = new Point(740, 4);
            LabelExFaxNumber.Name = "LabelExFaxNumber";
            LabelExFaxNumber.Size = new Size(428, 44);
            LabelExFaxNumber.TabIndex = 2;
            LabelExFaxNumber.Text = "足立清掃事務所\r\nFAX 03-8888-8888";
            LabelExFaxNumber.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelEx1
            // 
            labelEx1.AutoSize = true;
            labelEx1.Location = new Point(28, 20);
            labelEx1.Name = "labelEx1";
            labelEx1.Size = new Size(43, 15);
            labelEx1.TabIndex = 1;
            labelEx1.Text = "出力先";
            // 
            // ComboBoxExPrinterName
            // 
            ComboBoxExPrinterName.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxExPrinterName.FormattingEnabled = true;
            ComboBoxExPrinterName.Location = new Point(76, 16);
            ComboBoxExPrinterName.Name = "ComboBoxExPrinterName";
            ComboBoxExPrinterName.Size = new Size(212, 23);
            ComboBoxExPrinterName.TabIndex = 0;
            // 
            // SpreadSubstitute
            // 
            SpreadSubstitute.AccessibleDescription = "SpreadSubstitute, 共通, Row 0, Column 0";
            SpreadSubstitute.Dock = DockStyle.Fill;
            SpreadSubstitute.Font = new Font("ＭＳ Ｐゴシック", 11F);
            SpreadSubstitute.Location = new Point(173, 87);
            SpreadSubstitute.Name = "SpreadSubstitute";
            SpreadSubstitute.Size = new Size(1558, 927);
            SpreadSubstitute.TabIndex = 3;
            // 
            // SubstituteSheet
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(TableLayoutPanelExBase);
            MainMenuStrip = MenuStripEx1;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SubstituteSheet";
            Text = "SubstituteSheet";
            FormClosing += SubstituteSheet_FormClosing;
            TableLayoutPanelExBase.ResumeLayout(false);
            TableLayoutPanelExBase.PerformLayout();
            PanelExUp.ResumeLayout(false);
            PanelExUp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SpreadSubstitute).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private CcControl.CcTableLayoutPanel TableLayoutPanelExBase;
        private CcControl.CcMenuStrip MenuStripEx1;
        private CcControl.CcStatusStrip StatusStripEx1;
        private CcControl.CcPanel PanelExUp;
        private FarPoint.Win.Spread.FpSpread SpreadSubstitute;
        private CcControl.CcLabel labelEx1;
        private CcControl.CcComboBox ComboBoxExPrinterName;
        private CcControl.CcLabel LabelExFaxNumber;
        private CcControl.CcButton ButtonExPrint1;
        private CcControl.CcButton ButtonExPrint2;
        private FarPoint.Win.Spread.SheetView SheetView1;
        private FarPoint.Win.Spread.SheetView SheetView2;
        private FarPoint.Win.Spread.SheetView SheetView3;
    }
}