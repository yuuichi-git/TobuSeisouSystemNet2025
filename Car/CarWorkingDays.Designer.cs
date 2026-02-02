namespace Car {
    partial class CarWorkingDays {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CarWorkingDays));
            this.TableLayoutPanelExBase = new ControlEx.CcTableLayoutPanel();
            this.MenuStripEx1 = new ControlEx.MenuStripEx();
            this.StatusStripEx1 = new ControlEx.StatusStripEx();
            this.PanelExTop = new ControlEx.CcPanel();
            this.ComboBoxExCarMaster1 = new ControlEx.CcComboBoxCarMaster();
            this.labelEx4 = new ControlEx.LabelEx();
            this.ButtonExUpdate = new ControlEx.CcButton();
            this.labelEx2 = new ControlEx.LabelEx();
            this.labelEx1 = new ControlEx.LabelEx();
            this.DateTimePickerExOperationDate2 = new ControlEx.CcDateTime();
            this.DateTimePickerExOperationDate1 = new ControlEx.CcDateTime();
            this.SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls"));
            this.SheetViewList = this.SpreadList.GetSheet(0);
            this.ToolTip1 = new ToolTip(this.components);
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
            this.TableLayoutPanelExBase.Size = new Size(1904, 1041);
            this.TableLayoutPanelExBase.TabIndex = 0;
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
            // PanelExTop
            // 
            this.PanelExTop.Controls.Add(this.ComboBoxExCarMaster1);
            this.PanelExTop.Controls.Add(this.labelEx4);
            this.PanelExTop.Controls.Add(this.ButtonExUpdate);
            this.PanelExTop.Controls.Add(this.labelEx2);
            this.PanelExTop.Controls.Add(this.labelEx1);
            this.PanelExTop.Controls.Add(this.DateTimePickerExOperationDate2);
            this.PanelExTop.Controls.Add(this.DateTimePickerExOperationDate1);
            this.PanelExTop.Dock = DockStyle.Fill;
            this.PanelExTop.Location = new Point(3, 27);
            this.PanelExTop.Name = "PanelExTop";
            this.PanelExTop.Size = new Size(1898, 54);
            this.PanelExTop.TabIndex = 2;
            // 
            // ComboBoxExCarMaster1
            // 
            this.ComboBoxExCarMaster1.Location = new Point(540, 16);
            this.ComboBoxExCarMaster1.Name = "ComboBoxExCarMaster1";
            this.ComboBoxExCarMaster1.Size = new Size(148, 23);
            this.ComboBoxExCarMaster1.TabIndex = 9;
            this.ComboBoxExCarMaster1.Text = "足立800あ6661 (888)";
            this.ToolTip1.SetToolTip(this.ComboBoxExCarMaster1, "車両番号の数字部分又はドア番号を指定");
            // 
            // labelEx4
            // 
            this.labelEx4.AutoSize = true;
            this.labelEx4.Location = new Point(480, 20);
            this.labelEx4.Name = "labelEx4";
            this.labelEx4.Size = new Size(55, 15);
            this.labelEx4.TabIndex = 8;
            this.labelEx4.Text = "登録番号";
            // 
            // ButtonExUpdate
            // 
            this.ButtonExUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.ButtonExUpdate.Location = new Point(1696, 12);
            this.ButtonExUpdate.Name = "ButtonExUpdate";
            this.ButtonExUpdate.SetTextDirectionVertical = "";
            this.ButtonExUpdate.Size = new Size(160, 32);
            this.ButtonExUpdate.TabIndex = 4;
            this.ButtonExUpdate.Text = "最　新　化";
            this.ButtonExUpdate.UseVisualStyleBackColor = true;
            this.ButtonExUpdate.Click += this.ButtonExUpdate_Click;
            // 
            // labelEx2
            // 
            this.labelEx2.AutoSize = true;
            this.labelEx2.Location = new Point(28, 20);
            this.labelEx2.Name = "labelEx2";
            this.labelEx2.Size = new Size(43, 15);
            this.labelEx2.TabIndex = 3;
            this.labelEx2.Text = "稼働日";
            // 
            // labelEx1
            // 
            this.labelEx1.AutoSize = true;
            this.labelEx1.Location = new Point(260, 20);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.Size = new Size(19, 15);
            this.labelEx1.TabIndex = 2;
            this.labelEx1.Text = "～";
            // 
            // DateTimePickerExOperationDate2
            // 
            this.DateTimePickerExOperationDate2.CultureFlag = false;
            this.DateTimePickerExOperationDate2.CustomFormat = " 明治33年01月01日(月曜日)";
            this.DateTimePickerExOperationDate2.Format = DateTimePickerFormat.Custom;
            this.DateTimePickerExOperationDate2.Location = new Point(280, 16);
            this.DateTimePickerExOperationDate2.Name = "DateTimePickerExOperationDate2";
            this.DateTimePickerExOperationDate2.Size = new Size(180, 23);
            this.DateTimePickerExOperationDate2.TabIndex = 1;
            this.DateTimePickerExOperationDate2.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.DateTimePickerExOperationDate2.ValueChanged += this.DateTimePickerEx2_ValueChanged;
            // 
            // DateTimePickerExOperationDate1
            // 
            this.DateTimePickerExOperationDate1.CultureFlag = false;
            this.DateTimePickerExOperationDate1.CustomFormat = " 明治33年01月01日(月曜日)";
            this.DateTimePickerExOperationDate1.Format = DateTimePickerFormat.Custom;
            this.DateTimePickerExOperationDate1.Location = new Point(76, 16);
            this.DateTimePickerExOperationDate1.Name = "DateTimePickerExOperationDate1";
            this.DateTimePickerExOperationDate1.Size = new Size(180, 23);
            this.DateTimePickerExOperationDate1.TabIndex = 0;
            this.DateTimePickerExOperationDate1.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.DateTimePickerExOperationDate1.ValueChanged += this.DateTimePickerEx1_ValueChanged;
            // 
            // SpreadList
            // 
            this.SpreadList.AccessibleDescription = "SpreadList, 稼働車両一覧, Row 0, Column 0";
            this.SpreadList.Dock = DockStyle.Fill;
            this.SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F);
            this.SpreadList.Location = new Point(3, 87);
            this.SpreadList.Name = "SpreadList";
            this.SpreadList.Size = new Size(1898, 927);
            this.SpreadList.TabIndex = 3;
            // 
            // CarWorkingDays
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1904, 1041);
            this.Controls.Add(this.TableLayoutPanelExBase);
            this.MainMenuStrip = this.MenuStripEx1;
            this.Name = "CarWorkingDays";
            this.Text = "CarWorkingDays";
            this.FormClosing += this.CarWorkingDays_FormClosing;
            this.TableLayoutPanelExBase.ResumeLayout(false);
            this.TableLayoutPanelExBase.PerformLayout();
            this.PanelExTop.ResumeLayout(false);
            this.PanelExTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadList).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private ControlEx.CcTableLayoutPanel TableLayoutPanelExBase;
        private ControlEx.MenuStripEx MenuStripEx1;
        private ControlEx.StatusStripEx StatusStripEx1;
        private ControlEx.CcPanel PanelExTop;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private ControlEx.LabelEx labelEx2;
        private ControlEx.LabelEx labelEx1;
        private ControlEx.CcDateTime DateTimePickerExOperationDate2;
        private ControlEx.CcDateTime DateTimePickerExOperationDate1;
        private ControlEx.CcButton ButtonExUpdate;
        private ControlEx.LabelEx labelEx4;
        private ControlEx.CcComboBoxCarMaster ComboBoxExCarMaster1;
        private ToolTip ToolTip1;
        private FarPoint.Win.Spread.SheetView SheetViewList;
    }
}