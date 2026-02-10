namespace Collection {
    partial class CollectionStaffsTaitou {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CollectionStaffsTaitou));
            this.TableLayoutPanelExBase = new ControlEx.CcTableLayoutPanel();
            this.MenuStripEx1 = new ControlEx.CcMenuStrip();
            this.StatusStripEx1 = new ControlEx.CcStatusStrip();
            this.PanelExTop = new ControlEx.CcPanel();
            this.labelEx2 = new ControlEx.CcLabelEx();
            this.labelEx1 = new ControlEx.CcLabelEx();
            this.NumericUpDownExYear = new ControlEx.NumericUpDownEx();
            this.ButtonExUpdate = new ControlEx.CcButton();
            this.SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls"));
            this.SheetViewList = this.SpreadList.GetSheet(0);
            this.TableLayoutPanelExBase.SuspendLayout();
            this.PanelExTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.NumericUpDownExYear).BeginInit();
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
            this.TableLayoutPanelExBase.Size = new Size(858, 961);
            this.TableLayoutPanelExBase.TabIndex = 0;
            // 
            // MenuStripEx1
            // 
            this.MenuStripEx1.Location = new Point(0, 0);
            this.MenuStripEx1.Name = "MenuStripEx1";
            this.MenuStripEx1.Size = new Size(858, 24);
            this.MenuStripEx1.TabIndex = 0;
            this.MenuStripEx1.Text = "menuStripEx1";
            this.MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // StatusStripEx1
            // 
            this.StatusStripEx1.Location = new Point(0, 939);
            this.StatusStripEx1.Name = "StatusStripEx1";
            this.StatusStripEx1.Size = new Size(858, 22);
            this.StatusStripEx1.SizingGrip = false;
            this.StatusStripEx1.TabIndex = 1;
            this.StatusStripEx1.Text = "statusStripEx1";
            // 
            // PanelExTop
            // 
            this.PanelExTop.Controls.Add(this.labelEx2);
            this.PanelExTop.Controls.Add(this.labelEx1);
            this.PanelExTop.Controls.Add(this.NumericUpDownExYear);
            this.PanelExTop.Controls.Add(this.ButtonExUpdate);
            this.PanelExTop.Dock = DockStyle.Fill;
            this.PanelExTop.Location = new Point(3, 27);
            this.PanelExTop.Name = "PanelExTop";
            this.PanelExTop.Size = new Size(852, 54);
            this.PanelExTop.TabIndex = 2;
            // 
            // labelEx2
            // 
            this.labelEx2.AutoSize = true;
            this.labelEx2.Location = new Point(156, 20);
            this.labelEx2.Name = "labelEx2";
            this.labelEx2.Size = new Size(31, 15);
            this.labelEx2.TabIndex = 9;
            this.labelEx2.Text = "年分";
            // 
            // labelEx1
            // 
            this.labelEx1.AutoSize = true;
            this.labelEx1.Location = new Point(20, 20);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.Size = new Size(79, 15);
            this.labelEx1.TabIndex = 8;
            this.labelEx1.Text = "集計対象年月";
            // 
            // NumericUpDownExYear
            // 
            this.NumericUpDownExYear.Location = new Point(104, 16);
            this.NumericUpDownExYear.Maximum = new decimal(new int[] { 2029, 0, 0, 0 });
            this.NumericUpDownExYear.Minimum = new decimal(new int[] { 2024, 0, 0, 0 });
            this.NumericUpDownExYear.Name = "NumericUpDownExYear";
            this.NumericUpDownExYear.Size = new Size(50, 23);
            this.NumericUpDownExYear.TabIndex = 7;
            this.NumericUpDownExYear.TextAlign = HorizontalAlignment.Right;
            this.NumericUpDownExYear.Value = new decimal(new int[] { 2025, 0, 0, 0 });
            // 
            // ButtonExUpdate
            // 
            this.ButtonExUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.ButtonExUpdate.Location = new Point(656, 10);
            this.ButtonExUpdate.Name = "ButtonExUpdate";
            this.ButtonExUpdate.SetTextDirectionVertical = "";
            this.ButtonExUpdate.Size = new Size(160, 32);
            this.ButtonExUpdate.TabIndex = 6;
            this.ButtonExUpdate.Text = "最　新　化";
            this.ButtonExUpdate.UseVisualStyleBackColor = true;
            this.ButtonExUpdate.Click += this.ButtonExUpdate_Click;
            // 
            // SpreadList
            // 
            this.SpreadList.AccessibleDescription = "SpreadList, Sheet1, Row 0, Column 0";
            this.SpreadList.Dock = DockStyle.Fill;
            this.SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F);
            this.SpreadList.Location = new Point(3, 87);
            this.SpreadList.Name = "SpreadList";
            this.SpreadList.Size = new Size(852, 847);
            this.SpreadList.TabIndex = 3;
            // 
            // CollectionStaffsTaitou
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(858, 961);
            this.Controls.Add(this.TableLayoutPanelExBase);
            this.MainMenuStrip = this.MenuStripEx1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CollectionStaffsTaitou";
            this.Text = "CollectionStaff";
            this.FormClosing += this.CollectionStaffsTaitou_FormClosing;
            this.TableLayoutPanelExBase.ResumeLayout(false);
            this.TableLayoutPanelExBase.PerformLayout();
            this.PanelExTop.ResumeLayout(false);
            this.PanelExTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.NumericUpDownExYear).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.SpreadList).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private ControlEx.CcTableLayoutPanel TableLayoutPanelExBase;
        private ControlEx.CcMenuStrip MenuStripEx1;
        private ControlEx.CcStatusStrip StatusStripEx1;
        private ControlEx.CcPanel PanelExTop;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private ControlEx.CcButton ButtonExUpdate;
        private ControlEx.CcLabelEx labelEx2;
        private ControlEx.CcLabelEx labelEx1;
        private ControlEx.NumericUpDownEx NumericUpDownExYear;
        private FarPoint.Win.Spread.SheetView SheetViewList;
    }
}