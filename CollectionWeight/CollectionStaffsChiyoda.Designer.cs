namespace Collection {
    partial class CollectionStaffsChiyoda {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CollectionStaffsChiyoda));
            this.TableLayoutPanelExBase = new ControlEx.TableLayoutPanelEx();
            this.MenuStripEx1 = new ControlEx.MenuStripEx();
            this.StatusStripEx1 = new ControlEx.StatusStripEx();
            this.PanelExUp = new ControlEx.PanelEx();
            this.labelEx4 = new ControlEx.LabelEx();
            this.labelEx3 = new ControlEx.LabelEx();
            this.ButtonExUpdate = new ControlEx.ButtonEx();
            this.labelEx2 = new ControlEx.LabelEx();
            this.labelEx1 = new ControlEx.LabelEx();
            this.DateTimePickerEx2 = new ControlEx.DateTimePickerEx();
            this.DateTimePickerEx1 = new ControlEx.DateTimePickerEx();
            this.SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls"));
            this.SheetViewList = this.SpreadList.GetSheet(0);
            this.SpreadAggregate = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls1"));
            this.SheetViewAggregate = this.SpreadAggregate.GetSheet(0);
            this.TableLayoutPanelExBase.SuspendLayout();
            this.PanelExUp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.SpreadAggregate).BeginInit();
            this.SuspendLayout();
            // 
            // TableLayoutPanelExBase
            // 
            this.TableLayoutPanelExBase.ColumnCount = 2;
            this.TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.TableLayoutPanelExBase.Controls.Add(this.MenuStripEx1, 0, 0);
            this.TableLayoutPanelExBase.Controls.Add(this.StatusStripEx1, 0, 3);
            this.TableLayoutPanelExBase.Controls.Add(this.PanelExUp, 0, 1);
            this.TableLayoutPanelExBase.Controls.Add(this.SpreadList, 0, 2);
            this.TableLayoutPanelExBase.Controls.Add(this.SpreadAggregate, 1, 2);
            this.TableLayoutPanelExBase.Dock = DockStyle.Fill;
            this.TableLayoutPanelExBase.Location = new Point(0, 0);
            this.TableLayoutPanelExBase.Name = "TableLayoutPanelExBase";
            this.TableLayoutPanelExBase.RowCount = 4;
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.Size = new Size(984, 761);
            this.TableLayoutPanelExBase.TabIndex = 0;
            // 
            // MenuStripEx1
            // 
            this.TableLayoutPanelExBase.SetColumnSpan(this.MenuStripEx1, 2);
            this.MenuStripEx1.Location = new Point(0, 0);
            this.MenuStripEx1.Name = "MenuStripEx1";
            this.MenuStripEx1.Size = new Size(984, 24);
            this.MenuStripEx1.TabIndex = 0;
            this.MenuStripEx1.Text = "menuStripEx1";
            this.MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // StatusStripEx1
            // 
            this.TableLayoutPanelExBase.SetColumnSpan(this.StatusStripEx1, 2);
            this.StatusStripEx1.Location = new Point(0, 739);
            this.StatusStripEx1.Name = "StatusStripEx1";
            this.StatusStripEx1.Size = new Size(984, 22);
            this.StatusStripEx1.SizingGrip = false;
            this.StatusStripEx1.TabIndex = 1;
            this.StatusStripEx1.Text = "statusStripEx1";
            // 
            // PanelExUp
            // 
            this.TableLayoutPanelExBase.SetColumnSpan(this.PanelExUp, 2);
            this.PanelExUp.Controls.Add(this.labelEx4);
            this.PanelExUp.Controls.Add(this.labelEx3);
            this.PanelExUp.Controls.Add(this.ButtonExUpdate);
            this.PanelExUp.Controls.Add(this.labelEx2);
            this.PanelExUp.Controls.Add(this.labelEx1);
            this.PanelExUp.Controls.Add(this.DateTimePickerEx2);
            this.PanelExUp.Controls.Add(this.DateTimePickerEx1);
            this.PanelExUp.Dock = DockStyle.Fill;
            this.PanelExUp.Location = new Point(3, 27);
            this.PanelExUp.Name = "PanelExUp";
            this.PanelExUp.Size = new Size(978, 74);
            this.PanelExUp.TabIndex = 2;
            // 
            // labelEx4
            // 
            this.labelEx4.AutoSize = true;
            this.labelEx4.Location = new Point(496, 56);
            this.labelEx4.Name = "labelEx4";
            this.labelEx4.Size = new Size(125, 15);
            this.labelEx4.TabIndex = 6;
            this.labelEx4.Text = "期間内の従事者集計表";
            // 
            // labelEx3
            // 
            this.labelEx3.AutoSize = true;
            this.labelEx3.Location = new Point(12, 56);
            this.labelEx3.Name = "labelEx3";
            this.labelEx3.Size = new Size(125, 15);
            this.labelEx3.TabIndex = 5;
            this.labelEx3.Text = "期間内の従事者一覧表";
            // 
            // ButtonExUpdate
            // 
            this.ButtonExUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.ButtonExUpdate.Location = new Point(784, 20);
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
            this.labelEx2.Location = new Point(256, 12);
            this.labelEx2.Name = "labelEx2";
            this.labelEx2.Size = new Size(19, 15);
            this.labelEx2.TabIndex = 3;
            this.labelEx2.Text = "～";
            // 
            // labelEx1
            // 
            this.labelEx1.AutoSize = true;
            this.labelEx1.Location = new Point(12, 12);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.Size = new Size(55, 15);
            this.labelEx1.TabIndex = 2;
            this.labelEx1.Text = "配車日付";
            // 
            // DateTimePickerEx2
            // 
            this.DateTimePickerEx2.CultureFlag = false;
            this.DateTimePickerEx2.CustomFormat = " 明治33年01月01日(月曜日)";
            this.DateTimePickerEx2.Format = DateTimePickerFormat.Custom;
            this.DateTimePickerEx2.Location = new Point(280, 8);
            this.DateTimePickerEx2.Name = "DateTimePickerEx2";
            this.DateTimePickerEx2.Size = new Size(180, 23);
            this.DateTimePickerEx2.TabIndex = 1;
            this.DateTimePickerEx2.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.DateTimePickerEx2.ValueChanged += this.DateTimePickerEx2_ValueChanged;
            // 
            // DateTimePickerEx1
            // 
            this.DateTimePickerEx1.CultureFlag = false;
            this.DateTimePickerEx1.CustomFormat = " 明治33年01月01日(月曜日)";
            this.DateTimePickerEx1.Format = DateTimePickerFormat.Custom;
            this.DateTimePickerEx1.Location = new Point(72, 8);
            this.DateTimePickerEx1.Name = "DateTimePickerEx1";
            this.DateTimePickerEx1.Size = new Size(180, 23);
            this.DateTimePickerEx1.TabIndex = 0;
            this.DateTimePickerEx1.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.DateTimePickerEx1.ValueChanged += this.DateTimePickerEx1_ValueChanged;
            // 
            // SpreadList
            // 
            this.SpreadList.AccessibleDescription = "Book1, 一覧表示, Row 0, Column 0";
            this.SpreadList.Dock = DockStyle.Fill;
            this.SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.SpreadList.Location = new Point(3, 107);
            this.SpreadList.Name = "SpreadList";
            this.SpreadList.Size = new Size(486, 627);
            this.SpreadList.TabIndex = 3;
            // 
            // SpreadAggregate
            // 
            this.SpreadAggregate.AccessibleDescription = "Book1, 集計値, Row 0, Column 0";
            this.SpreadAggregate.Dock = DockStyle.Fill;
            this.SpreadAggregate.Font = new Font("ＭＳ Ｐゴシック", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.SpreadAggregate.Location = new Point(495, 107);
            this.SpreadAggregate.Name = "SpreadAggregate";
            this.SpreadAggregate.Size = new Size(486, 627);
            this.SpreadAggregate.TabIndex = 4;
            // 
            // CollectionWeightChiyoda
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(984, 761);
            this.Controls.Add(this.TableLayoutPanelExBase);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.MenuStripEx1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CollectionWeightChiyoda";
            this.Text = "CollectionWeightChiyoda";
            this.FormClosing += this.CollectionWeightChiyoda_FormClosing;
            this.TableLayoutPanelExBase.ResumeLayout(false);
            this.TableLayoutPanelExBase.PerformLayout();
            this.PanelExUp.ResumeLayout(false);
            this.PanelExUp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadList).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.SpreadAggregate).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private ControlEx.TableLayoutPanelEx TableLayoutPanelExBase;
        private ControlEx.MenuStripEx MenuStripEx1;
        private ControlEx.StatusStripEx StatusStripEx1;
        private ControlEx.PanelEx PanelExUp;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private FarPoint.Win.Spread.FpSpread SpreadAggregate;
        private FarPoint.Win.Spread.SheetView SheetViewList;
        private FarPoint.Win.Spread.SheetView SheetViewAggregate;
        private ControlEx.LabelEx labelEx1;
        private ControlEx.DateTimePickerEx DateTimePickerEx2;
        private ControlEx.DateTimePickerEx DateTimePickerEx1;
        private ControlEx.ButtonEx ButtonExUpdate;
        private ControlEx.LabelEx labelEx2;
        private ControlEx.LabelEx labelEx4;
        private ControlEx.LabelEx labelEx3;
    }
}