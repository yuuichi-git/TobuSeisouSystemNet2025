namespace Staff {
    partial class StaffWorkingHours {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaffWorkingHours));
            this.TableLayoutPanelExBase = new ControlEx.TableLayoutPanelEx();
            this.MenuStripEx1 = new ControlEx.MenuStripEx();
            this.StatusStripEx1 = new ControlEx.StatusStripEx();
            this.PanelExUp = new ControlEx.CcPanel();
            this.labelEx3 = new ControlEx.LabelEx();
            this.ComboBoxExStaffDisplayName = new ControlEx.ComboBoxEx();
            this.labelEx2 = new ControlEx.LabelEx();
            this.labelEx1 = new ControlEx.LabelEx();
            this.DateTimePickerExOperationDate2 = new ControlEx.CcDateTime();
            this.DateTimePickerExOperationDate1 = new ControlEx.CcDateTime();
            this.ButtonExUpdate = new ControlEx.CcButton();
            this.SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls"));
            this.SheetViewList = this.SpreadList.GetSheet(0);
            this.TableLayoutPanelExBase.SuspendLayout();
            this.PanelExUp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadList).BeginInit();
            this.SuspendLayout();
            // 
            // TableLayoutPanelExBase
            // 
            this.TableLayoutPanelExBase.ColumnCount = 1;
            this.TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelExBase.Controls.Add(this.MenuStripEx1, 0, 0);
            this.TableLayoutPanelExBase.Controls.Add(this.StatusStripEx1, 0, 3);
            this.TableLayoutPanelExBase.Controls.Add(this.PanelExUp, 0, 1);
            this.TableLayoutPanelExBase.Controls.Add(this.SpreadList, 0, 2);
            this.TableLayoutPanelExBase.Dock = DockStyle.Fill;
            this.TableLayoutPanelExBase.Location = new Point(0, 0);
            this.TableLayoutPanelExBase.Name = "TableLayoutPanelExBase";
            this.TableLayoutPanelExBase.RowCount = 4;
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 76F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.Size = new Size(776, 1011);
            this.TableLayoutPanelExBase.TabIndex = 0;
            // 
            // MenuStripEx1
            // 
            this.MenuStripEx1.Location = new Point(0, 0);
            this.MenuStripEx1.Name = "MenuStripEx1";
            this.MenuStripEx1.Size = new Size(776, 24);
            this.MenuStripEx1.TabIndex = 0;
            this.MenuStripEx1.Text = "menuStripEx1";
            this.MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // StatusStripEx1
            // 
            this.StatusStripEx1.Location = new Point(0, 989);
            this.StatusStripEx1.Name = "StatusStripEx1";
            this.StatusStripEx1.Size = new Size(776, 22);
            this.StatusStripEx1.TabIndex = 1;
            this.StatusStripEx1.Text = "statusStripEx1";
            // 
            // PanelExUp
            // 
            this.PanelExUp.Controls.Add(this.labelEx3);
            this.PanelExUp.Controls.Add(this.ComboBoxExStaffDisplayName);
            this.PanelExUp.Controls.Add(this.labelEx2);
            this.PanelExUp.Controls.Add(this.labelEx1);
            this.PanelExUp.Controls.Add(this.DateTimePickerExOperationDate2);
            this.PanelExUp.Controls.Add(this.DateTimePickerExOperationDate1);
            this.PanelExUp.Controls.Add(this.ButtonExUpdate);
            this.PanelExUp.Dock = DockStyle.Fill;
            this.PanelExUp.Location = new Point(3, 27);
            this.PanelExUp.Name = "PanelExUp";
            this.PanelExUp.Size = new Size(770, 70);
            this.PanelExUp.TabIndex = 2;
            // 
            // labelEx3
            // 
            this.labelEx3.AutoSize = true;
            this.labelEx3.Location = new Point(8, 44);
            this.labelEx3.Name = "labelEx3";
            this.labelEx3.Size = new Size(67, 15);
            this.labelEx3.TabIndex = 6;
            this.labelEx3.Text = "従事者氏名";
            // 
            // ComboBoxExStaffDisplayName
            // 
            this.ComboBoxExStaffDisplayName.FormattingEnabled = true;
            this.ComboBoxExStaffDisplayName.ImeMode = ImeMode.Hiragana;
            this.ComboBoxExStaffDisplayName.Location = new Point(80, 40);
            this.ComboBoxExStaffDisplayName.Name = "ComboBoxExStaffDisplayName";
            this.ComboBoxExStaffDisplayName.Size = new Size(244, 23);
            this.ComboBoxExStaffDisplayName.TabIndex = 5;
            // 
            // labelEx2
            // 
            this.labelEx2.AutoSize = true;
            this.labelEx2.Location = new Point(32, 12);
            this.labelEx2.Name = "labelEx2";
            this.labelEx2.Size = new Size(43, 15);
            this.labelEx2.TabIndex = 4;
            this.labelEx2.Text = "配車日";
            // 
            // labelEx1
            // 
            this.labelEx1.AutoSize = true;
            this.labelEx1.Location = new Point(268, 12);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.Size = new Size(19, 15);
            this.labelEx1.TabIndex = 3;
            this.labelEx1.Text = "～";
            // 
            // DateTimePickerExOperationDate2
            // 
            this.DateTimePickerExOperationDate2.CultureFlag = false;
            this.DateTimePickerExOperationDate2.CustomFormat = " 明治33年01月01日(月曜日)";
            this.DateTimePickerExOperationDate2.Format = DateTimePickerFormat.Custom;
            this.DateTimePickerExOperationDate2.Location = new Point(292, 8);
            this.DateTimePickerExOperationDate2.Name = "DateTimePickerExOperationDate2";
            this.DateTimePickerExOperationDate2.Size = new Size(184, 23);
            this.DateTimePickerExOperationDate2.TabIndex = 2;
            this.DateTimePickerExOperationDate2.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.DateTimePickerExOperationDate2.ValueChanged += this.DateTimePickerExOperationDate2_ValueChanged;
            // 
            // DateTimePickerExOperationDate1
            // 
            this.DateTimePickerExOperationDate1.CultureFlag = false;
            this.DateTimePickerExOperationDate1.CustomFormat = " 明治33年01月01日(月曜日)";
            this.DateTimePickerExOperationDate1.Format = DateTimePickerFormat.Custom;
            this.DateTimePickerExOperationDate1.Location = new Point(80, 8);
            this.DateTimePickerExOperationDate1.Name = "DateTimePickerExOperationDate1";
            this.DateTimePickerExOperationDate1.Size = new Size(184, 23);
            this.DateTimePickerExOperationDate1.TabIndex = 1;
            this.DateTimePickerExOperationDate1.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.DateTimePickerExOperationDate1.ValueChanged += this.DateTimePickerExOperationDate1_ValueChanged;
            // 
            // ButtonExUpdate
            // 
            this.ButtonExUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.ButtonExUpdate.Location = new Point(592, 16);
            this.ButtonExUpdate.Name = "ButtonExUpdate";
            this.ButtonExUpdate.SetTextDirectionVertical = "";
            this.ButtonExUpdate.Size = new Size(140, 36);
            this.ButtonExUpdate.TabIndex = 0;
            this.ButtonExUpdate.Text = "最　新　化";
            this.ButtonExUpdate.UseVisualStyleBackColor = true;
            this.ButtonExUpdate.Click += this.ButtonEx_Click;
            // 
            // SpreadList
            // 
            this.SpreadList.AccessibleDescription = "SpreadList, Sheet1, Row 0, Column 0";
            this.SpreadList.Dock = DockStyle.Fill;
            this.SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.SpreadList.Location = new Point(3, 103);
            this.SpreadList.Name = "SpreadList";
            this.SpreadList.Size = new Size(770, 881);
            this.SpreadList.TabIndex = 3;
            // 
            // StaffWorkingHours
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(776, 1011);
            this.Controls.Add(this.TableLayoutPanelExBase);
            this.MainMenuStrip = this.MenuStripEx1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StaffWorkingHours";
            this.Text = "StaffWorkingHours";
            this.FormClosing += this.StaffWorkingHours_FormClosing;
            this.TableLayoutPanelExBase.ResumeLayout(false);
            this.TableLayoutPanelExBase.PerformLayout();
            this.PanelExUp.ResumeLayout(false);
            this.PanelExUp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadList).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private ControlEx.TableLayoutPanelEx TableLayoutPanelExBase;
        private ControlEx.MenuStripEx MenuStripEx1;
        private ControlEx.StatusStripEx StatusStripEx1;
        private ControlEx.CcPanel PanelExUp;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private ControlEx.CcButton ButtonExUpdate;
        private ControlEx.CcDateTime DateTimePickerExOperationDate1;
        private ControlEx.LabelEx labelEx1;
        private ControlEx.CcDateTime DateTimePickerExOperationDate2;
        private ControlEx.LabelEx labelEx2;
        private ControlEx.LabelEx labelEx3;
        private ControlEx.ComboBoxEx ComboBoxExStaffDisplayName;
        private FarPoint.Win.Spread.SheetView SheetViewList;
    }
}