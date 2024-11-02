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
            this.TableLayoutPanelExBase = new ControlEx.TableLayoutPanelEx();
            this.MenuStripEx1 = new ControlEx.MenuStripEx();
            this.StatusStripEx1 = new ControlEx.StatusStripEx();
            this.PanelExTop = new ControlEx.PanelEx();
            this.ButtonExUpdate = new ControlEx.ButtonEx();
            this.labelEx1 = new ControlEx.LabelEx();
            this.DateTimePickerExOperationDate = new ControlEx.DateTimePickerEx();
            this.PanelExLeft = new ControlEx.PanelEx();
            this.PanelExRight = new ControlEx.PanelEx();
            this.TableLayoutPanelExBase.SuspendLayout();
            this.PanelExTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableLayoutPanelExBase
            // 
            this.TableLayoutPanelExBase.ColumnCount = 3;
            this.TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 52F));
            this.TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 52F));
            this.TableLayoutPanelExBase.Controls.Add(this.MenuStripEx1, 0, 0);
            this.TableLayoutPanelExBase.Controls.Add(this.StatusStripEx1, 0, 3);
            this.TableLayoutPanelExBase.Controls.Add(this.PanelExTop, 0, 1);
            this.TableLayoutPanelExBase.Controls.Add(this.PanelExLeft, 0, 2);
            this.TableLayoutPanelExBase.Controls.Add(this.PanelExRight, 2, 2);
            this.TableLayoutPanelExBase.Dock = DockStyle.Fill;
            this.TableLayoutPanelExBase.Location = new Point(0, 0);
            this.TableLayoutPanelExBase.Name = "TableLayoutPanelExBase";
            this.TableLayoutPanelExBase.RowCount = 4;
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.Size = new Size(1904, 1041);
            this.TableLayoutPanelExBase.TabIndex = 0;
            // 
            // MenuStripEx1
            // 
            this.TableLayoutPanelExBase.SetColumnSpan(this.MenuStripEx1, 3);
            this.MenuStripEx1.Location = new Point(0, 0);
            this.MenuStripEx1.Name = "MenuStripEx1";
            this.MenuStripEx1.Size = new Size(1904, 24);
            this.MenuStripEx1.TabIndex = 0;
            this.MenuStripEx1.Text = "menuStripEx1";
            this.MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // StatusStripEx1
            // 
            this.TableLayoutPanelExBase.SetColumnSpan(this.StatusStripEx1, 3);
            this.StatusStripEx1.Location = new Point(0, 1019);
            this.StatusStripEx1.Name = "StatusStripEx1";
            this.StatusStripEx1.Size = new Size(1904, 22);
            this.StatusStripEx1.TabIndex = 1;
            this.StatusStripEx1.Text = "statusStripEx1";
            // 
            // PanelExTop
            // 
            this.TableLayoutPanelExBase.SetColumnSpan(this.PanelExTop, 3);
            this.PanelExTop.Controls.Add(this.ButtonExUpdate);
            this.PanelExTop.Controls.Add(this.labelEx1);
            this.PanelExTop.Controls.Add(this.DateTimePickerExOperationDate);
            this.PanelExTop.Dock = DockStyle.Fill;
            this.PanelExTop.Location = new Point(0, 24);
            this.PanelExTop.Margin = new Padding(0);
            this.PanelExTop.Name = "PanelExTop";
            this.PanelExTop.Size = new Size(1904, 32);
            this.PanelExTop.TabIndex = 2;
            // 
            // ButtonExUpdate
            // 
            this.ButtonExUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.ButtonExUpdate.Font = new Font("Yu Gothic UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            this.ButtonExUpdate.Location = new Point(1668, 0);
            this.ButtonExUpdate.Name = "ButtonExUpdate";
            this.ButtonExUpdate.Size = new Size(184, 32);
            this.ButtonExUpdate.TabIndex = 2;
            this.ButtonExUpdate.Text = "最　新　化";
            this.ButtonExUpdate.UseVisualStyleBackColor = true;
            this.ButtonExUpdate.Click += this.ButtonEx_Click;
            // 
            // labelEx1
            // 
            this.labelEx1.AutoSize = true;
            this.labelEx1.Location = new Point(48, 8);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.Size = new Size(43, 15);
            this.labelEx1.TabIndex = 1;
            this.labelEx1.Text = "配車日";
            // 
            // DateTimePickerExOperationDate
            // 
            this.DateTimePickerExOperationDate.CultureFlag = false;
            this.DateTimePickerExOperationDate.CustomFormat = " 明治33年01月01日(月曜日)";
            this.DateTimePickerExOperationDate.Format = DateTimePickerFormat.Custom;
            this.DateTimePickerExOperationDate.Location = new Point(96, 4);
            this.DateTimePickerExOperationDate.Name = "DateTimePickerExOperationDate";
            this.DateTimePickerExOperationDate.Size = new Size(184, 23);
            this.DateTimePickerExOperationDate.TabIndex = 0;
            this.DateTimePickerExOperationDate.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // PanelExLeft
            // 
            this.PanelExLeft.Dock = DockStyle.Fill;
            this.PanelExLeft.Location = new Point(0, 56);
            this.PanelExLeft.Margin = new Padding(0);
            this.PanelExLeft.Name = "PanelExLeft";
            this.PanelExLeft.Size = new Size(52, 961);
            this.PanelExLeft.TabIndex = 3;
            // 
            // PanelExRight
            // 
            this.PanelExRight.Dock = DockStyle.Fill;
            this.PanelExRight.Location = new Point(1852, 56);
            this.PanelExRight.Margin = new Padding(0);
            this.PanelExRight.Name = "PanelExRight";
            this.PanelExRight.Size = new Size(52, 961);
            this.PanelExRight.TabIndex = 4;
            // 
            // VehicleDispatchBoard
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1904, 1041);
            this.Controls.Add(this.TableLayoutPanelExBase);
            this.MainMenuStrip = this.MenuStripEx1;
            this.Name = "VehicleDispatchBoard";
            this.Text = "VehicleDispatchBoard";
            this.TableLayoutPanelExBase.ResumeLayout(false);
            this.TableLayoutPanelExBase.PerformLayout();
            this.PanelExTop.ResumeLayout(false);
            this.PanelExTop.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private ControlEx.TableLayoutPanelEx TableLayoutPanelExBase;
        private ControlEx.MenuStripEx MenuStripEx1;
        private ControlEx.StatusStripEx StatusStripEx1;
        private ControlEx.PanelEx PanelExTop;
        private ControlEx.DateTimePickerEx DateTimePickerExOperationDate;
        private ControlEx.LabelEx labelEx1;
        private ControlEx.PanelEx PanelExLeft;
        private ControlEx.PanelEx PanelExRight;
        private ControlEx.ButtonEx ButtonExUpdate;
    }
}