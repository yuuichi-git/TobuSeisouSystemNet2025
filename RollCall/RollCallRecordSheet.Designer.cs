namespace RollCall {
    partial class RollCallRecordSheet {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RollCallRecordSheet));
            this.TableLayoutPanelExBase = new CcControl.CcTableLayoutPanel();
            this.MenuStripEx1 = new CcControl.CcMenuStrip();
            this.StatusStripEx1 = new CcControl.CcStatusStrip();
            this.PanelExTop = new CcControl.CcPanel();
            this.ccLabel1 = new CcControl.CcLabel();
            this.ComboBoxExPrinterName = new CcControl.CcComboBox();
            this.labelEx2 = new CcControl.CcLabel();
            this.ComboBoxExManagedSpace = new CcControl.CcComboBox();
            this.ButtonExUpdate = new CcControl.CcButton();
            this.labelEx1 = new CcControl.CcLabel();
            this.DateTimePickerExOperationDate = new CcControl.CcDateTime();
            this.SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls"));
            this.SheetViewList = this.SpreadList.GetSheet(0);
            this.PrintDocument1 = new System.Drawing.Printing.PrintDocument();
            this.TableLayoutPanelExBase.SuspendLayout();
            this.PanelExTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadList).BeginInit();
            this.SuspendLayout();
            // 
            // TableLayoutPanelExBase
            // 
            this.TableLayoutPanelExBase.ColumnCount = 1;
            this.TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            this.TableLayoutPanelExBase.Controls.Add(this.MenuStripEx1, 0, 0);
            this.TableLayoutPanelExBase.Controls.Add(this.StatusStripEx1, 0, 3);
            this.TableLayoutPanelExBase.Controls.Add(this.PanelExTop, 0, 1);
            this.TableLayoutPanelExBase.Controls.Add(this.SpreadList, 0, 2);
            this.TableLayoutPanelExBase.Dock = DockStyle.Fill;
            this.TableLayoutPanelExBase.Location = new Point(0, 0);
            this.TableLayoutPanelExBase.Name = "TableLayoutPanelExBase";
            this.TableLayoutPanelExBase.RowCount = 4;
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.Size = new Size(1354, 1041);
            this.TableLayoutPanelExBase.TabIndex = 0;
            // 
            // MenuStripEx1
            // 
            this.MenuStripEx1.Location = new Point(0, 0);
            this.MenuStripEx1.Name = "MenuStripEx1";
            this.MenuStripEx1.Size = new Size(1354, 24);
            this.MenuStripEx1.TabIndex = 0;
            this.MenuStripEx1.Text = "menuStripEx1";
            this.MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // StatusStripEx1
            // 
            this.StatusStripEx1.Location = new Point(0, 1019);
            this.StatusStripEx1.Name = "StatusStripEx1";
            this.StatusStripEx1.Size = new Size(1354, 22);
            this.StatusStripEx1.SizingGrip = false;
            this.StatusStripEx1.TabIndex = 1;
            this.StatusStripEx1.Text = "statusStripEx1";
            // 
            // PanelExTop
            // 
            this.PanelExTop.Controls.Add(this.ccLabel1);
            this.PanelExTop.Controls.Add(this.ComboBoxExPrinterName);
            this.PanelExTop.Controls.Add(this.labelEx2);
            this.PanelExTop.Controls.Add(this.ComboBoxExManagedSpace);
            this.PanelExTop.Controls.Add(this.ButtonExUpdate);
            this.PanelExTop.Controls.Add(this.labelEx1);
            this.PanelExTop.Controls.Add(this.DateTimePickerExOperationDate);
            this.PanelExTop.Dock = DockStyle.Fill;
            this.PanelExTop.Location = new Point(3, 27);
            this.PanelExTop.Name = "PanelExTop";
            this.PanelExTop.Size = new Size(1348, 54);
            this.PanelExTop.TabIndex = 2;
            // 
            // ccLabel1
            // 
            this.ccLabel1.AutoSize = true;
            this.ccLabel1.Location = new Point(604, 20);
            this.ccLabel1.Name = "ccLabel1";
            this.ccLabel1.Size = new Size(43, 15);
            this.ccLabel1.TabIndex = 11;
            this.ccLabel1.Text = "出力先";
            // 
            // ComboBoxExPrinterName
            // 
            this.ComboBoxExPrinterName.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ComboBoxExPrinterName.FormattingEnabled = true;
            this.ComboBoxExPrinterName.Location = new Point(652, 16);
            this.ComboBoxExPrinterName.Name = "ComboBoxExPrinterName";
            this.ComboBoxExPrinterName.Size = new Size(212, 23);
            this.ComboBoxExPrinterName.TabIndex = 10;
            // 
            // labelEx2
            // 
            this.labelEx2.AutoSize = true;
            this.labelEx2.Location = new Point(336, 20);
            this.labelEx2.Name = "labelEx2";
            this.labelEx2.Size = new Size(55, 15);
            this.labelEx2.TabIndex = 4;
            this.labelEx2.Text = "点呼場所";
            // 
            // ComboBoxExManagedSpace
            // 
            this.ComboBoxExManagedSpace.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ComboBoxExManagedSpace.FormattingEnabled = true;
            this.ComboBoxExManagedSpace.Items.AddRange(new object[] { "本社営業所", "三郷車庫" });
            this.ComboBoxExManagedSpace.Location = new Point(396, 16);
            this.ComboBoxExManagedSpace.Name = "ComboBoxExManagedSpace";
            this.ComboBoxExManagedSpace.Size = new Size(140, 23);
            this.ComboBoxExManagedSpace.TabIndex = 3;
            // 
            // ButtonExUpdate
            // 
            this.ButtonExUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.ButtonExUpdate.Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            this.ButtonExUpdate.ForeColor = SystemColors.ControlText;
            this.ButtonExUpdate.Location = new Point(1132, 12);
            this.ButtonExUpdate.Name = "ButtonExUpdate";
            this.ButtonExUpdate.SetTextDirectionVertical = "";
            this.ButtonExUpdate.Size = new Size(170, 32);
            this.ButtonExUpdate.TabIndex = 2;
            this.ButtonExUpdate.Text = "最　新　化";
            this.ButtonExUpdate.UseVisualStyleBackColor = true;
            this.ButtonExUpdate.Click += this.ButtonExUpdate_Click;
            // 
            // labelEx1
            // 
            this.labelEx1.AutoSize = true;
            this.labelEx1.Location = new Point(24, 20);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.Size = new Size(55, 15);
            this.labelEx1.TabIndex = 1;
            this.labelEx1.Text = "配車日付";
            // 
            // DateTimePickerExOperationDate
            // 
            this.DateTimePickerExOperationDate.CultureFlag = false;
            this.DateTimePickerExOperationDate.CustomFormat = " 明治33年01月01日(月曜日)";
            this.DateTimePickerExOperationDate.Format = DateTimePickerFormat.Custom;
            this.DateTimePickerExOperationDate.Location = new Point(84, 16);
            this.DateTimePickerExOperationDate.Name = "DateTimePickerExOperationDate";
            this.DateTimePickerExOperationDate.Size = new Size(182, 23);
            this.DateTimePickerExOperationDate.TabIndex = 0;
            this.DateTimePickerExOperationDate.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // SpreadList
            // 
            this.SpreadList.AccessibleDescription = "Book1, Sheet1, Row 0, Column 0";
            this.SpreadList.Dock = DockStyle.Fill;
            this.SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F);
            this.SpreadList.Location = new Point(3, 87);
            this.SpreadList.Name = "SpreadList";
            this.SpreadList.Size = new Size(1348, 927);
            this.SpreadList.TabIndex = 3;
            // 
            // RollCallRecordSheet
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1354, 1041);
            this.Controls.Add(this.TableLayoutPanelExBase);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.MenuStripEx1;
            this.Name = "RollCallRecordSheet";
            this.Text = "RollCallRecordSheet";
            this.FormClosing += this.RollCallRecordSheet_FormClosing;
            this.TableLayoutPanelExBase.ResumeLayout(false);
            this.TableLayoutPanelExBase.PerformLayout();
            this.PanelExTop.ResumeLayout(false);
            this.PanelExTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadList).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private CcControl.CcTableLayoutPanel TableLayoutPanelExBase;
        private CcControl.CcMenuStrip MenuStripEx1;
        private CcControl.CcStatusStrip StatusStripEx1;
        private CcControl.CcPanel PanelExTop;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private CcControl.CcLabel labelEx1;
        private CcControl.CcDateTime DateTimePickerExOperationDate;
        private CcControl.CcButton ButtonExUpdate;
        private CcControl.CcLabel labelEx2;
        private CcControl.CcComboBox ComboBoxExManagedSpace;
        private System.Drawing.Printing.PrintDocument PrintDocument1;
        private FarPoint.Win.Spread.SheetView SheetViewList;
        private CcControl.CcLabel ccLabel1;
        private CcControl.CcComboBox ComboBoxExPrinterName;
    }
}