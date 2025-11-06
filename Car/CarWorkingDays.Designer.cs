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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CarWorkingDays));
            this.TableLayoutPanelExBase = new ControlEx.TableLayoutPanelEx();
            this.MenuStripEx1 = new ControlEx.MenuStripEx();
            this.StatusStripEx1 = new ControlEx.StatusStripEx();
            this.PanelExTop = new ControlEx.PanelEx();
            this.SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("resource1"));
            this.SheetViewList = this.SpreadList.GetSheet(0);
            this.DateTimePickerEx1 = new ControlEx.DateTimePickerEx();
            this.DateTimePickerEx2 = new ControlEx.DateTimePickerEx();
            this.labelEx1 = new ControlEx.LabelEx();
            this.labelEx2 = new ControlEx.LabelEx();
            this.ButtonExUpdate = new ControlEx.ButtonEx();
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
            this.PanelExTop.Controls.Add(this.ButtonExUpdate);
            this.PanelExTop.Controls.Add(this.labelEx2);
            this.PanelExTop.Controls.Add(this.labelEx1);
            this.PanelExTop.Controls.Add(this.DateTimePickerEx2);
            this.PanelExTop.Controls.Add(this.DateTimePickerEx1);
            this.PanelExTop.Dock = DockStyle.Fill;
            this.PanelExTop.Location = new Point(3, 27);
            this.PanelExTop.Name = "PanelExTop";
            this.PanelExTop.Size = new Size(1898, 54);
            this.PanelExTop.TabIndex = 2;
            // 
            // SpreadList
            // 
            this.SpreadList.AccessibleDescription = "Book1, Sheet1, Row 0, Column 0";
            this.SpreadList.Dock = DockStyle.Fill;
            this.SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F);
            this.SpreadList.Location = new Point(3, 87);
            this.SpreadList.Name = "SpreadList";
            this.SpreadList.Size = new Size(1898, 927);
            this.SpreadList.TabIndex = 3;
            // 
            // DateTimePickerEx1
            // 
            this.DateTimePickerEx1.CultureFlag = false;
            this.DateTimePickerEx1.CustomFormat = " 明治33年01月01日(月曜日)";
            this.DateTimePickerEx1.Format = DateTimePickerFormat.Custom;
            this.DateTimePickerEx1.Location = new Point(76, 16);
            this.DateTimePickerEx1.Name = "DateTimePickerEx1";
            this.DateTimePickerEx1.Size = new Size(180, 23);
            this.DateTimePickerEx1.TabIndex = 0;
            this.DateTimePickerEx1.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // DateTimePickerEx2
            // 
            this.DateTimePickerEx2.CultureFlag = false;
            this.DateTimePickerEx2.CustomFormat = " 明治33年01月01日(月曜日)";
            this.DateTimePickerEx2.Format = DateTimePickerFormat.Custom;
            this.DateTimePickerEx2.Location = new Point(280, 16);
            this.DateTimePickerEx2.Name = "DateTimePickerEx2";
            this.DateTimePickerEx2.Size = new Size(180, 23);
            this.DateTimePickerEx2.TabIndex = 1;
            this.DateTimePickerEx2.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
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
            // labelEx2
            // 
            this.labelEx2.AutoSize = true;
            this.labelEx2.Location = new Point(28, 20);
            this.labelEx2.Name = "labelEx2";
            this.labelEx2.Size = new Size(43, 15);
            this.labelEx2.TabIndex = 3;
            this.labelEx2.Text = "稼働日";
            // 
            // ButtonExUpdate
            // 
            this.ButtonExUpdate.Location = new Point(1696, 12);
            this.ButtonExUpdate.Name = "ButtonExUpdate";
            this.ButtonExUpdate.SetTextDirectionVertical = "";
            this.ButtonExUpdate.Size = new Size(160, 32);
            this.ButtonExUpdate.TabIndex = 4;
            this.ButtonExUpdate.Text = "最　新　化";
            this.ButtonExUpdate.UseVisualStyleBackColor = true;
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
            this.TableLayoutPanelExBase.ResumeLayout(false);
            this.TableLayoutPanelExBase.PerformLayout();
            this.PanelExTop.ResumeLayout(false);
            this.PanelExTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadList).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private ControlEx.TableLayoutPanelEx TableLayoutPanelExBase;
        private ControlEx.MenuStripEx MenuStripEx1;
        private ControlEx.StatusStripEx StatusStripEx1;
        private ControlEx.PanelEx PanelExTop;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private FarPoint.Win.Spread.SheetView SheetViewList;
        private ControlEx.LabelEx labelEx2;
        private ControlEx.LabelEx labelEx1;
        private ControlEx.DateTimePickerEx DateTimePickerEx2;
        private ControlEx.DateTimePickerEx DateTimePickerEx1;
        private ControlEx.ButtonEx ButtonExUpdate;
    }
}