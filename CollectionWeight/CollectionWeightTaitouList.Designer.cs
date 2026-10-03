namespace Collection {
    partial class CollectionWeightTaitouList {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CollectionWeightTaitouList));
            TableLayoutPanelExBase = new CcControl.CcTableLayoutPanel();
            MenuStripEx1 = new CcControl.CcMenuStrip();
            StatusStripEx1 = new CcControl.CcStatusStrip();
            PanelExTop = new CcControl.CcPanel();
            ButtonExUpdate = new CcControl.CcButton();
            labelEx2 = new CcControl.CcLabel();
            labelEx1 = new CcControl.CcLabel();
            DateTimePickerEx2 = new CcControl.CcDateTime();
            DateTimePickerEx1 = new CcControl.CcDateTime();
            SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls"));
            SheetViewList = SpreadList.GetSheet(0);
            TableLayoutPanelExBase.SuspendLayout();
            PanelExTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SpreadList).BeginInit();
            SuspendLayout();
            // 
            // TableLayoutPanelExBase
            // 
            TableLayoutPanelExBase.ColumnCount = 1;
            TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            TableLayoutPanelExBase.Controls.Add(MenuStripEx1, 0, 0);
            TableLayoutPanelExBase.Controls.Add(StatusStripEx1, 0, 3);
            TableLayoutPanelExBase.Controls.Add(PanelExTop, 0, 1);
            TableLayoutPanelExBase.Controls.Add(SpreadList, 0, 2);
            TableLayoutPanelExBase.Dock = DockStyle.Fill;
            TableLayoutPanelExBase.Location = new Point(0, 0);
            TableLayoutPanelExBase.Name = "TableLayoutPanelExBase";
            TableLayoutPanelExBase.RowCount = 4;
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            TableLayoutPanelExBase.Size = new Size(1330, 919);
            TableLayoutPanelExBase.TabIndex = 0;
            // 
            // MenuStripEx1
            // 
            MenuStripEx1.Location = new Point(0, 0);
            MenuStripEx1.Name = "MenuStripEx1";
            MenuStripEx1.Size = new Size(1330, 24);
            MenuStripEx1.TabIndex = 0;
            MenuStripEx1.Text = "menuStripEx1";
            MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // StatusStripEx1
            // 
            StatusStripEx1.Location = new Point(0, 897);
            StatusStripEx1.Name = "StatusStripEx1";
            StatusStripEx1.Size = new Size(1330, 22);
            StatusStripEx1.SizingGrip = false;
            StatusStripEx1.TabIndex = 1;
            StatusStripEx1.Text = "statusStripEx1";
            // 
            // PanelExTop
            // 
            PanelExTop.Controls.Add(ButtonExUpdate);
            PanelExTop.Controls.Add(labelEx2);
            PanelExTop.Controls.Add(labelEx1);
            PanelExTop.Controls.Add(DateTimePickerEx2);
            PanelExTop.Controls.Add(DateTimePickerEx1);
            PanelExTop.Dock = DockStyle.Fill;
            PanelExTop.Location = new Point(3, 27);
            PanelExTop.Name = "PanelExTop";
            PanelExTop.Size = new Size(1324, 54);
            PanelExTop.TabIndex = 2;
            // 
            // ButtonExUpdate
            // 
            ButtonExUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ButtonExUpdate.ForeColor = SystemColors.ControlText;
            ButtonExUpdate.Location = new Point(1097, 12);
            ButtonExUpdate.Name = "ButtonExUpdate";
            ButtonExUpdate.SetTextDirectionVertical = "";
            ButtonExUpdate.Size = new Size(184, 32);
            ButtonExUpdate.TabIndex = 4;
            ButtonExUpdate.Text = "最　新　化";
            ButtonExUpdate.UseVisualStyleBackColor = true;
            ButtonExUpdate.Click += ButtonExUpdate_Click;
            // 
            // labelEx2
            // 
            labelEx2.AutoSize = true;
            labelEx2.Location = new Point(236, 20);
            labelEx2.Name = "labelEx2";
            labelEx2.Size = new Size(19, 15);
            labelEx2.TabIndex = 3;
            labelEx2.Text = "～";
            // 
            // labelEx1
            // 
            labelEx1.AutoSize = true;
            labelEx1.Location = new Point(8, 20);
            labelEx1.Name = "labelEx1";
            labelEx1.Size = new Size(43, 15);
            labelEx1.TabIndex = 2;
            labelEx1.Text = "稼働日";
            // 
            // DateTimePickerEx2
            // 
            DateTimePickerEx2.CustomFormat = " 明治33年01月01日(月曜日)";
            DateTimePickerEx2.Format = DateTimePickerFormat.Custom;
            DateTimePickerEx2.Location = new Point(260, 16);
            DateTimePickerEx2.Name = "DateTimePickerEx2";
            DateTimePickerEx2.Size = new Size(176, 23);
            DateTimePickerEx2.TabIndex = 1;
            DateTimePickerEx2.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            DateTimePickerEx2.ValueChanged += DateTimePickerEx2_ValueChanged;
            // 
            // DateTimePickerEx1
            // 
            DateTimePickerEx1.CustomFormat = " 明治33年01月01日(月曜日)";
            DateTimePickerEx1.Format = DateTimePickerFormat.Custom;
            DateTimePickerEx1.Location = new Point(56, 16);
            DateTimePickerEx1.Name = "DateTimePickerEx1";
            DateTimePickerEx1.Size = new Size(176, 23);
            DateTimePickerEx1.TabIndex = 0;
            DateTimePickerEx1.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            DateTimePickerEx1.ValueChanged += DateTimePickerEx1_ValueChanged;
            // 
            // SpreadList
            // 
            SpreadList.AccessibleDescription = "SpreadList, レコード, Row 0, Column 0";
            SpreadList.Dock = DockStyle.Fill;
            SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F);
            SpreadList.Location = new Point(3, 87);
            SpreadList.Name = "SpreadList";
            SpreadList.Size = new Size(1324, 805);
            SpreadList.TabIndex = 3;
            // 
            // CollectionWeightTaitouList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1330, 919);
            Controls.Add(TableLayoutPanelExBase);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = MenuStripEx1;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CollectionWeightTaitouList";
            Text = "CollectionWeightTaitouList";
            FormClosing += CollectionWeightTaitouList_FormClosing;
            TableLayoutPanelExBase.ResumeLayout(false);
            TableLayoutPanelExBase.PerformLayout();
            PanelExTop.ResumeLayout(false);
            PanelExTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SpreadList).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private CcControl.CcTableLayoutPanel TableLayoutPanelExBase;
        private CcControl.CcMenuStrip MenuStripEx1;
        private CcControl.CcStatusStrip StatusStripEx1;
        private CcControl.CcPanel PanelExTop;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private CcControl.CcDateTime DateTimePickerEx2;
        private CcControl.CcDateTime DateTimePickerEx1;
        private CcControl.CcLabel labelEx2;
        private CcControl.CcLabel labelEx1;
        private CcControl.CcButton ButtonExUpdate;
        private FarPoint.Win.Spread.SheetView SheetViewList;
    }
}