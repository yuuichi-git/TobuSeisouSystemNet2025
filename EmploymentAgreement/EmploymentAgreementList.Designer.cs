namespace EmploymentAgreement {
    partial class EmploymentAgreementList {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmploymentAgreementList));
            this.TableLayoutPanelExBase = new ControlEx.TableLayoutPanelEx();
            this.MenuStripEx1 = new ControlEx.MenuStripEx();
            this.StatusStripEx1 = new ControlEx.StatusStripEx();
            this.SpreadList = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls"));
            this.PanelExTop = new ControlEx.PanelEx();
            this.checkBoxEx3 = new ControlEx.CheckBoxEx();
            this.checkBoxEx2 = new ControlEx.CheckBoxEx();
            this.checkBoxEx1 = new ControlEx.CheckBoxEx();
            this.CheckBoxExRetirementFlag = new ControlEx.CheckBoxEx();
            this.ButtonExUpdate = new ControlEx.ButtonEx();
            this.SheetViewList = this.SpreadList.GetSheet(0);
            this.TableLayoutPanelExBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadList).BeginInit();
            this.PanelExTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableLayoutPanelExBase
            // 
            this.TableLayoutPanelExBase.ColumnCount = 1;
            this.TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelExBase.Controls.Add(this.MenuStripEx1, 0, 0);
            this.TableLayoutPanelExBase.Controls.Add(this.StatusStripEx1, 0, 3);
            this.TableLayoutPanelExBase.Controls.Add(this.SpreadList, 0, 2);
            this.TableLayoutPanelExBase.Controls.Add(this.PanelExTop, 0, 1);
            this.TableLayoutPanelExBase.Dock = DockStyle.Fill;
            this.TableLayoutPanelExBase.Location = new Point(0, 0);
            this.TableLayoutPanelExBase.Name = "TableLayoutPanelExBase";
            this.TableLayoutPanelExBase.RowCount = 4;
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
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
            // SpreadList
            // 
            this.SpreadList.AccessibleDescription = "SpreadList, Sheet1, Row 0, Column 0";
            this.SpreadList.Dock = DockStyle.Fill;
            this.SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.SpreadList.Location = new Point(3, 67);
            this.SpreadList.Name = "SpreadList";
            this.SpreadList.Size = new Size(1898, 947);
            this.SpreadList.TabIndex = 2;
            this.SpreadList.CellDoubleClick += this.SpreadList_CellDoubleClick;
            // 
            // PanelExTop
            // 
            this.PanelExTop.Controls.Add(this.checkBoxEx3);
            this.PanelExTop.Controls.Add(this.checkBoxEx2);
            this.PanelExTop.Controls.Add(this.checkBoxEx1);
            this.PanelExTop.Controls.Add(this.CheckBoxExRetirementFlag);
            this.PanelExTop.Controls.Add(this.ButtonExUpdate);
            this.PanelExTop.Dock = DockStyle.Fill;
            this.PanelExTop.Location = new Point(3, 27);
            this.PanelExTop.Name = "PanelExTop";
            this.PanelExTop.Size = new Size(1898, 34);
            this.PanelExTop.TabIndex = 3;
            // 
            // checkBoxEx3
            // 
            this.checkBoxEx3.AutoSize = true;
            this.checkBoxEx3.Location = new Point(200, 8);
            this.checkBoxEx3.Name = "checkBoxEx3";
            this.checkBoxEx3.Size = new Size(74, 19);
            this.checkBoxEx3.TabIndex = 7;
            this.checkBoxEx3.Text = "短期雇用";
            this.checkBoxEx3.UseVisualStyleBackColor = true;
            // 
            // checkBoxEx2
            // 
            this.checkBoxEx2.AutoSize = true;
            this.checkBoxEx2.Location = new Point(112, 8);
            this.checkBoxEx2.Name = "checkBoxEx2";
            this.checkBoxEx2.Size = new Size(74, 19);
            this.checkBoxEx2.TabIndex = 6;
            this.checkBoxEx2.Text = "長期雇用";
            this.checkBoxEx2.UseVisualStyleBackColor = true;
            // 
            // checkBoxEx1
            // 
            this.checkBoxEx1.AutoSize = true;
            this.checkBoxEx1.Location = new Point(28, 8);
            this.checkBoxEx1.Name = "checkBoxEx1";
            this.checkBoxEx1.Size = new Size(72, 19);
            this.checkBoxEx1.TabIndex = 5;
            this.checkBoxEx1.Text = "アルバイト";
            this.checkBoxEx1.UseVisualStyleBackColor = true;
            // 
            // CheckBoxExRetirementFlag
            // 
            this.CheckBoxExRetirementFlag.AutoSize = true;
            this.CheckBoxExRetirementFlag.Location = new Point(1532, 8);
            this.CheckBoxExRetirementFlag.Name = "CheckBoxExRetirementFlag";
            this.CheckBoxExRetirementFlag.Size = new Size(102, 19);
            this.CheckBoxExRetirementFlag.TabIndex = 4;
            this.CheckBoxExRetirementFlag.Text = "退職者も含める";
            this.CheckBoxExRetirementFlag.UseVisualStyleBackColor = true;
            // 
            // ButtonExUpdate
            // 
            this.ButtonExUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.ButtonExUpdate.Font = new Font("Yu Gothic UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            this.ButtonExUpdate.Location = new Point(1676, 0);
            this.ButtonExUpdate.Name = "ButtonExUpdate";
            this.ButtonExUpdate.Size = new Size(184, 32);
            this.ButtonExUpdate.TabIndex = 3;
            this.ButtonExUpdate.Text = "最　新　化";
            this.ButtonExUpdate.UseVisualStyleBackColor = true;
            this.ButtonExUpdate.Click += this.ButtonEx_Click;
            // 
            // EmploymentAgreementList
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1904, 1041);
            this.Controls.Add(this.TableLayoutPanelExBase);
            this.MainMenuStrip = this.MenuStripEx1;
            this.Name = "EmploymentAgreementList";
            this.Text = "EmploymentAgreementList";
            this.FormClosing += this.EmploymentAgreementList_FormClosing;
            this.TableLayoutPanelExBase.ResumeLayout(false);
            this.TableLayoutPanelExBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadList).EndInit();
            this.PanelExTop.ResumeLayout(false);
            this.PanelExTop.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private ControlEx.TableLayoutPanelEx TableLayoutPanelExBase;
        private ControlEx.MenuStripEx MenuStripEx1;
        private ControlEx.StatusStripEx StatusStripEx1;
        private FarPoint.Win.Spread.FpSpread SpreadList;
        private ControlEx.PanelEx PanelExTop;
        private ControlEx.ButtonEx ButtonExUpdate;
        private ControlEx.CheckBoxEx CheckBoxExRetirementFlag;
        private ControlEx.CheckBoxEx checkBoxEx3;
        private ControlEx.CheckBoxEx checkBoxEx2;
        private ControlEx.CheckBoxEx checkBoxEx1;
        private FarPoint.Win.Spread.SheetView SheetViewList;
    }
}