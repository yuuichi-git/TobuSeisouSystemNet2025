namespace Toukanpo {
    partial class ToukanpoSpeedSurvey {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToukanpoSpeedSurvey));
            this.TableLayoutPanelExBase = new ControlEx.TableLayoutPanelEx();
            this.MenuStripEx1 = new ControlEx.MenuStripEx();
            this.StatusStripEx1 = new ControlEx.StatusStripEx();
            this.PanelExTop = new ControlEx.CcPanel();
            this.ButtonExUpdate = new ControlEx.CcButton();
            this.labelEx3 = new ControlEx.LabelEx();
            this.labelEx2 = new ControlEx.LabelEx();
            this.labelEx1 = new ControlEx.LabelEx();
            this.NumericUpDownExMonth = new ControlEx.NumericUpDownEx();
            this.NumericUpDownExYear = new ControlEx.NumericUpDownEx();
            this.SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls"));
            this.SheetViewList = this.SpreadList.GetSheet(0);
            this.TableLayoutPanelExBase.SuspendLayout();
            this.PanelExTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.NumericUpDownExMonth).BeginInit();
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
            this.TableLayoutPanelExBase.Size = new Size(743, 965);
            this.TableLayoutPanelExBase.TabIndex = 0;
            // 
            // MenuStripEx1
            // 
            this.MenuStripEx1.Location = new Point(0, 0);
            this.MenuStripEx1.Name = "MenuStripEx1";
            this.MenuStripEx1.Size = new Size(743, 24);
            this.MenuStripEx1.TabIndex = 0;
            this.MenuStripEx1.Text = "menuStripEx1";
            this.MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // StatusStripEx1
            // 
            this.StatusStripEx1.Location = new Point(0, 943);
            this.StatusStripEx1.Name = "StatusStripEx1";
            this.StatusStripEx1.Size = new Size(743, 22);
            this.StatusStripEx1.SizingGrip = false;
            this.StatusStripEx1.TabIndex = 1;
            this.StatusStripEx1.Text = "statusStripEx1";
            // 
            // PanelExTop
            // 
            this.PanelExTop.Controls.Add(this.ButtonExUpdate);
            this.PanelExTop.Controls.Add(this.labelEx3);
            this.PanelExTop.Controls.Add(this.labelEx2);
            this.PanelExTop.Controls.Add(this.labelEx1);
            this.PanelExTop.Controls.Add(this.NumericUpDownExMonth);
            this.PanelExTop.Controls.Add(this.NumericUpDownExYear);
            this.PanelExTop.Dock = DockStyle.Fill;
            this.PanelExTop.Location = new Point(3, 27);
            this.PanelExTop.Name = "PanelExTop";
            this.PanelExTop.Size = new Size(737, 54);
            this.PanelExTop.TabIndex = 2;
            // 
            // ButtonExUpdate
            // 
            this.ButtonExUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.ButtonExUpdate.Location = new Point(544, 12);
            this.ButtonExUpdate.Name = "ButtonExUpdate";
            this.ButtonExUpdate.SetTextDirectionVertical = "";
            this.ButtonExUpdate.Size = new Size(160, 32);
            this.ButtonExUpdate.TabIndex = 5;
            this.ButtonExUpdate.Text = "最　新　化";
            this.ButtonExUpdate.UseVisualStyleBackColor = true;
            this.ButtonExUpdate.Click += this.ButtonExUpdate_Click;
            // 
            // labelEx3
            // 
            this.labelEx3.AutoSize = true;
            this.labelEx3.Location = new Point(232, 20);
            this.labelEx3.Name = "labelEx3";
            this.labelEx3.Size = new Size(31, 15);
            this.labelEx3.TabIndex = 4;
            this.labelEx3.Text = "月分";
            // 
            // labelEx2
            // 
            this.labelEx2.AutoSize = true;
            this.labelEx2.Location = new Point(152, 20);
            this.labelEx2.Name = "labelEx2";
            this.labelEx2.Size = new Size(19, 15);
            this.labelEx2.TabIndex = 3;
            this.labelEx2.Text = "年";
            // 
            // labelEx1
            // 
            this.labelEx1.AutoSize = true;
            this.labelEx1.Location = new Point(16, 20);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.Size = new Size(79, 15);
            this.labelEx1.TabIndex = 2;
            this.labelEx1.Text = "集計対象年月";
            // 
            // NumericUpDownExMonth
            // 
            this.NumericUpDownExMonth.Location = new Point(176, 16);
            this.NumericUpDownExMonth.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            this.NumericUpDownExMonth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.NumericUpDownExMonth.Name = "NumericUpDownExMonth";
            this.NumericUpDownExMonth.Size = new Size(50, 23);
            this.NumericUpDownExMonth.TabIndex = 1;
            this.NumericUpDownExMonth.TextAlign = HorizontalAlignment.Right;
            this.NumericUpDownExMonth.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // NumericUpDownExYear
            // 
            this.NumericUpDownExYear.Location = new Point(100, 16);
            this.NumericUpDownExYear.Maximum = new decimal(new int[] { 2029, 0, 0, 0 });
            this.NumericUpDownExYear.Minimum = new decimal(new int[] { 2024, 0, 0, 0 });
            this.NumericUpDownExYear.Name = "NumericUpDownExYear";
            this.NumericUpDownExYear.Size = new Size(50, 23);
            this.NumericUpDownExYear.TabIndex = 0;
            this.NumericUpDownExYear.TextAlign = HorizontalAlignment.Right;
            this.NumericUpDownExYear.Value = new decimal(new int[] { 2025, 0, 0, 0 });
            // 
            // SpreadList
            // 
            this.SpreadList.AccessibleDescription = "SpreadList, 速度調査, Row 0, Column 0";
            this.SpreadList.Dock = DockStyle.Fill;
            this.SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F);
            this.SpreadList.Location = new Point(3, 87);
            this.SpreadList.Name = "SpreadList";
            this.SpreadList.Size = new Size(737, 851);
            this.SpreadList.TabIndex = 3;
            // 
            // ToukanpoSpeedSurvey
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(743, 965);
            this.Controls.Add(this.TableLayoutPanelExBase);
            this.MainMenuStrip = this.MenuStripEx1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ToukanpoSpeedSurvey";
            this.Text = "ToukanpoSpeedSurvey";
            this.FormClosing += this.ToukanpoSpeedSurvey_FormClosing;
            this.TableLayoutPanelExBase.ResumeLayout(false);
            this.TableLayoutPanelExBase.PerformLayout();
            this.PanelExTop.ResumeLayout(false);
            this.PanelExTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.NumericUpDownExMonth).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.NumericUpDownExYear).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.SpreadList).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private ControlEx.TableLayoutPanelEx TableLayoutPanelExBase;
        private ControlEx.MenuStripEx MenuStripEx1;
        private ControlEx.StatusStripEx StatusStripEx1;
        private ControlEx.CcPanel PanelExTop;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private ControlEx.NumericUpDownEx NumericUpDownExYear;
        private ControlEx.LabelEx labelEx3;
        private ControlEx.LabelEx labelEx2;
        private ControlEx.LabelEx labelEx1;
        private ControlEx.NumericUpDownEx NumericUpDownExMonth;
        private ControlEx.CcButton ButtonExUpdate;
        private FarPoint.Win.Spread.SheetView SheetViewList;
    }
}