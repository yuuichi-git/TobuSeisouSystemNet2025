namespace VehicleDispatch {
    partial class VehicleDispatchBoard {
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
            TableLayoutPanelExBase = new CcControl.CcTableLayoutPanel();
            MenuStripEx1 = new CcControl.CcMenuStrip();
            StatusStripEx1 = new CcControl.CcStatusStrip();
            PanelExTop = new CcControl.CcPanel();
            ButtonExUpdate = new CcControl.CcButton();
            labelEx1 = new CcControl.CcLabel();
            DateTimePickerExOperationDate = new CcControl.CcDateTime();
            PanelExLeft = new CcControl.CcPanel();
            ButtonExStockBoxOpen = new CcControl.CcButton();
            TableLayoutPanelExBase.SuspendLayout();
            PanelExTop.SuspendLayout();
            PanelExLeft.SuspendLayout();
            SuspendLayout();
            // 
            // TableLayoutPanelExBase
            // 
            TableLayoutPanelExBase.ColumnCount = 2;
            TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 52F));
            TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            TableLayoutPanelExBase.Controls.Add(MenuStripEx1, 0, 0);
            TableLayoutPanelExBase.Controls.Add(StatusStripEx1, 0, 3);
            TableLayoutPanelExBase.Controls.Add(PanelExTop, 0, 1);
            TableLayoutPanelExBase.Controls.Add(PanelExLeft, 0, 2);
            TableLayoutPanelExBase.Dock = DockStyle.Fill;
            TableLayoutPanelExBase.Location = new Point(0, 0);
            TableLayoutPanelExBase.Name = "TableLayoutPanelExBase";
            TableLayoutPanelExBase.RowCount = 4;
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            TableLayoutPanelExBase.Size = new Size(1904, 1041);
            TableLayoutPanelExBase.TabIndex = 0;
            // 
            // MenuStripEx1
            // 
            TableLayoutPanelExBase.SetColumnSpan(MenuStripEx1, 2);
            MenuStripEx1.Location = new Point(0, 0);
            MenuStripEx1.Name = "MenuStripEx1";
            MenuStripEx1.Size = new Size(1904, 24);
            MenuStripEx1.TabIndex = 0;
            MenuStripEx1.Text = "menuStripEx1";
            MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // StatusStripEx1
            // 
            TableLayoutPanelExBase.SetColumnSpan(StatusStripEx1, 2);
            StatusStripEx1.Location = new Point(0, 1019);
            StatusStripEx1.Name = "StatusStripEx1";
            StatusStripEx1.Size = new Size(1904, 22);
            StatusStripEx1.TabIndex = 1;
            StatusStripEx1.Text = "statusStripEx1";
            // 
            // PanelExTop
            // 
            TableLayoutPanelExBase.SetColumnSpan(PanelExTop, 2);
            PanelExTop.Controls.Add(ButtonExUpdate);
            PanelExTop.Controls.Add(labelEx1);
            PanelExTop.Controls.Add(DateTimePickerExOperationDate);
            PanelExTop.Dock = DockStyle.Fill;
            PanelExTop.Location = new Point(0, 24);
            PanelExTop.Margin = new Padding(0);
            PanelExTop.Name = "PanelExTop";
            PanelExTop.Size = new Size(1904, 32);
            PanelExTop.TabIndex = 2;
            // 
            // ButtonExUpdate
            // 
            ButtonExUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ButtonExUpdate.Font = new Font("Yu Gothic UI", 11.25F);
            ButtonExUpdate.ForeColor = SystemColors.ControlText;
            ButtonExUpdate.Location = new Point(1668, 0);
            ButtonExUpdate.Name = "ButtonExUpdate";
            ButtonExUpdate.SetTextDirectionVertical = null;
            ButtonExUpdate.Size = new Size(180, 32);
            ButtonExUpdate.TabIndex = 2;
            ButtonExUpdate.Text = "最　新　化";
            ButtonExUpdate.UseVisualStyleBackColor = true;
            ButtonExUpdate.Click += ButtonEx_Click;
            // 
            // labelEx1
            // 
            labelEx1.AutoSize = true;
            labelEx1.Location = new Point(48, 8);
            labelEx1.Name = "labelEx1";
            labelEx1.Size = new Size(43, 15);
            labelEx1.TabIndex = 1;
            labelEx1.Text = "配車日";
            // 
            // DateTimePickerExOperationDate
            // 
            DateTimePickerExOperationDate.CultureFlag = false;
            DateTimePickerExOperationDate.CustomFormat = " 明治33年01月01日(月曜日)";
            DateTimePickerExOperationDate.Format = DateTimePickerFormat.Custom;
            DateTimePickerExOperationDate.Location = new Point(96, 4);
            DateTimePickerExOperationDate.Name = "DateTimePickerExOperationDate";
            DateTimePickerExOperationDate.Size = new Size(184, 23);
            DateTimePickerExOperationDate.TabIndex = 0;
            DateTimePickerExOperationDate.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // PanelExLeft
            // 
            PanelExLeft.Controls.Add(ButtonExStockBoxOpen);
            PanelExLeft.Dock = DockStyle.Fill;
            PanelExLeft.Location = new Point(0, 56);
            PanelExLeft.Margin = new Padding(0);
            PanelExLeft.Name = "PanelExLeft";
            PanelExLeft.Size = new Size(52, 961);
            PanelExLeft.TabIndex = 3;
            // 
            // ButtonExStockBoxOpen
            // 
            ButtonExStockBoxOpen.ForeColor = SystemColors.ControlText;
            ButtonExStockBoxOpen.Location = new Point(12, 8);
            ButtonExStockBoxOpen.Name = "ButtonExStockBoxOpen";
            ButtonExStockBoxOpen.SetTextDirectionVertical = null;
            ButtonExStockBoxOpen.Size = new Size(32, 184);
            ButtonExStockBoxOpen.TabIndex = 0;
            ButtonExStockBoxOpen.UseVisualStyleBackColor = true;
            ButtonExStockBoxOpen.Click += ButtonEx_Click;
            // 
            // VehicleDispatchBoard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(TableLayoutPanelExBase);
            MainMenuStrip = MenuStripEx1;
            Name = "VehicleDispatchBoard";
            Text = "VehicleDispatchBoard";
            FormClosing += VehicleDispatchBoard_FormClosing;
            TableLayoutPanelExBase.ResumeLayout(false);
            TableLayoutPanelExBase.PerformLayout();
            PanelExTop.ResumeLayout(false);
            PanelExTop.PerformLayout();
            PanelExLeft.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private CcControl.CcTableLayoutPanel TableLayoutPanelExBase;
        private CcControl.CcMenuStrip MenuStripEx1;
        private CcControl.CcStatusStrip StatusStripEx1;
        private CcControl.CcPanel PanelExTop;
        private CcControl.CcDateTime DateTimePickerExOperationDate;
        private CcControl.CcLabel labelEx1;
        private CcControl.CcPanel PanelExLeft;
        private CcControl.CcButton ButtonExUpdate;
        private CcControl.CcButton ButtonExStockBoxOpen;
    }
}