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
            this.ContextMenuStripEx1 = new ControlEx.ContextMenuStripEx();
            this.ToolStripMenuItemExpiration = new ToolStripMenuItem();
            this.ToolStripMenuItemContractExpirationPartTimeJob = new ToolStripMenuItem();
            this.ToolStripMenuItemContractExpirationPartTimeEmployee = new ToolStripMenuItem();
            this.ToolStripMenuItemContractExpirationPartTimer = new ToolStripMenuItem();
            this.ToolStripMenuItemContractExpirationLongJob新産別 = new ToolStripMenuItem();
            this.ToolStripMenuItemContractExpirationLongJob自運労運転士 = new ToolStripMenuItem();
            this.ToolStripMenuItemContractExpirationLongJob自運労作業員 = new ToolStripMenuItem();
            this.ToolStripMenuItemContractExpirationShortJob = new ToolStripMenuItem();
            this.ToolStripMenuItemContractExpirationWrittenPledge = new ToolStripMenuItem();
            this.ToolStripMenuItemContractExpirationLossWrittenPledge = new ToolStripMenuItem();
            this.ToolStripMenuItemContractExpirationNotice = new ToolStripMenuItem();
            this.SheetViewList = this.SpreadList.GetSheet(0);
            this.PanelExTop = new ControlEx.PanelEx();
            this.CheckBoxExRetirementFlag = new ControlEx.CheckBoxEx();
            this.ButtonExUpdate = new ControlEx.ButtonEx();
            this.TableLayoutPanelExBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadList).BeginInit();
            this.ContextMenuStripEx1.SuspendLayout();
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
            this.SpreadList.ContextMenuStrip = this.ContextMenuStripEx1;
            this.SpreadList.Dock = DockStyle.Fill;
            this.SpreadList.Font = new Font("ＭＳ Ｐゴシック", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.SpreadList.Location = new Point(3, 67);
            this.SpreadList.Name = "SpreadList";
            this.SpreadList.Size = new Size(1898, 947);
            this.SpreadList.TabIndex = 2;
            this.SpreadList.CellDoubleClick += this.SpreadList_CellDoubleClick;
            // 
            // ContextMenuStripEx1
            // 
            this.ContextMenuStripEx1.Items.AddRange(new ToolStripItem[] { this.ToolStripMenuItemExpiration, this.ToolStripMenuItemContractExpirationPartTimeJob, this.ToolStripMenuItemContractExpirationPartTimeEmployee, this.ToolStripMenuItemContractExpirationPartTimer, this.ToolStripMenuItemContractExpirationLongJob新産別, this.ToolStripMenuItemContractExpirationLongJob自運労運転士, this.ToolStripMenuItemContractExpirationLongJob自運労作業員, this.ToolStripMenuItemContractExpirationShortJob, this.ToolStripMenuItemContractExpirationWrittenPledge, this.ToolStripMenuItemContractExpirationLossWrittenPledge, this.ToolStripMenuItemContractExpirationNotice });
            this.ContextMenuStripEx1.Name = "ContextMenuStripEx1";
            this.ContextMenuStripEx1.Size = new Size(261, 246);
            // 
            // ToolStripMenuItemExpiration
            // 
            this.ToolStripMenuItemExpiration.Name = "ToolStripMenuItemExpiration";
            this.ToolStripMenuItemExpiration.Size = new Size(260, 22);
            this.ToolStripMenuItemExpiration.Text = "体験入社契約書";
            this.ToolStripMenuItemExpiration.Click += this.ToolStripMenuItem_Click;
            // 
            // ToolStripMenuItemContractExpirationPartTimeJob
            // 
            this.ToolStripMenuItemContractExpirationPartTimeJob.Name = "ToolStripMenuItemContractExpirationPartTimeJob";
            this.ToolStripMenuItemContractExpirationPartTimeJob.Size = new Size(260, 22);
            this.ToolStripMenuItemContractExpirationPartTimeJob.Text = "継続アルバイト契約書";
            this.ToolStripMenuItemContractExpirationPartTimeJob.Click += this.ToolStripMenuItem_Click;
            // 
            // ToolStripMenuItemContractExpirationPartTimeEmployee
            // 
            this.ToolStripMenuItemContractExpirationPartTimeEmployee.Name = "ToolStripMenuItemContractExpirationPartTimeEmployee";
            this.ToolStripMenuItemContractExpirationPartTimeEmployee.Size = new Size(260, 22);
            this.ToolStripMenuItemContractExpirationPartTimeEmployee.Text = "嘱託雇用契約社員";
            this.ToolStripMenuItemContractExpirationPartTimeEmployee.Click += this.ToolStripMenuItem_Click;
            // 
            // ToolStripMenuItemContractExpirationPartTimer
            // 
            this.ToolStripMenuItemContractExpirationPartTimer.Name = "ToolStripMenuItemContractExpirationPartTimer";
            this.ToolStripMenuItemContractExpirationPartTimer.Size = new Size(260, 22);
            this.ToolStripMenuItemContractExpirationPartTimer.Text = "パートタイマー";
            this.ToolStripMenuItemContractExpirationPartTimer.Click += this.ToolStripMenuItem_Click;
            // 
            // ToolStripMenuItemContractExpirationLongJob新産別
            // 
            this.ToolStripMenuItemContractExpirationLongJob新産別.Name = "ToolStripMenuItemContractExpirationLongJob新産別";
            this.ToolStripMenuItemContractExpirationLongJob新産別.Size = new Size(260, 22);
            this.ToolStripMenuItemContractExpirationLongJob新産別.Text = "長期雇用契約書（新産別）";
            this.ToolStripMenuItemContractExpirationLongJob新産別.Click += this.ToolStripMenuItem_Click;
            // 
            // ToolStripMenuItemContractExpirationLongJob自運労運転士
            // 
            this.ToolStripMenuItemContractExpirationLongJob自運労運転士.Name = "ToolStripMenuItemContractExpirationLongJob自運労運転士";
            this.ToolStripMenuItemContractExpirationLongJob自運労運転士.Size = new Size(260, 22);
            this.ToolStripMenuItemContractExpirationLongJob自運労運転士.Text = "長期雇用契約書（自運労・運転手）";
            this.ToolStripMenuItemContractExpirationLongJob自運労運転士.Click += this.ToolStripMenuItem_Click;
            // 
            // ToolStripMenuItemContractExpirationLongJob自運労作業員
            // 
            this.ToolStripMenuItemContractExpirationLongJob自運労作業員.Name = "ToolStripMenuItemContractExpirationLongJob自運労作業員";
            this.ToolStripMenuItemContractExpirationLongJob自運労作業員.Size = new Size(260, 22);
            this.ToolStripMenuItemContractExpirationLongJob自運労作業員.Text = "長期雇用契約書（自運労・作業員）";
            this.ToolStripMenuItemContractExpirationLongJob自運労作業員.Click += this.ToolStripMenuItem_Click;
            // 
            // ToolStripMenuItemContractExpirationShortJob
            // 
            this.ToolStripMenuItemContractExpirationShortJob.Name = "ToolStripMenuItemContractExpirationShortJob";
            this.ToolStripMenuItemContractExpirationShortJob.Size = new Size(260, 22);
            this.ToolStripMenuItemContractExpirationShortJob.Text = "短期雇用契約書";
            this.ToolStripMenuItemContractExpirationShortJob.Click += this.ToolStripMenuItem_Click;
            // 
            // ToolStripMenuItemContractExpirationWrittenPledge
            // 
            this.ToolStripMenuItemContractExpirationWrittenPledge.Name = "ToolStripMenuItemContractExpirationWrittenPledge";
            this.ToolStripMenuItemContractExpirationWrittenPledge.Size = new Size(260, 22);
            this.ToolStripMenuItemContractExpirationWrittenPledge.Text = "誓約書";
            this.ToolStripMenuItemContractExpirationWrittenPledge.Click += this.ToolStripMenuItem_Click;
            // 
            // ToolStripMenuItemContractExpirationLossWrittenPledge
            // 
            this.ToolStripMenuItemContractExpirationLossWrittenPledge.Name = "ToolStripMenuItemContractExpirationLossWrittenPledge";
            this.ToolStripMenuItemContractExpirationLossWrittenPledge.Size = new Size(260, 22);
            this.ToolStripMenuItemContractExpirationLossWrittenPledge.Text = "失墜行為確認書";
            this.ToolStripMenuItemContractExpirationLossWrittenPledge.Click += this.ToolStripMenuItem_Click;
            // 
            // ToolStripMenuItemContractExpirationNotice
            // 
            this.ToolStripMenuItemContractExpirationNotice.Name = "ToolStripMenuItemContractExpirationNotice";
            this.ToolStripMenuItemContractExpirationNotice.Size = new Size(260, 22);
            this.ToolStripMenuItemContractExpirationNotice.Text = "契約満了通知";
            this.ToolStripMenuItemContractExpirationNotice.Click += this.ToolStripMenuItem_Click;
            // 
            // PanelExTop
            // 
            this.PanelExTop.Controls.Add(this.CheckBoxExRetirementFlag);
            this.PanelExTop.Controls.Add(this.ButtonExUpdate);
            this.PanelExTop.Dock = DockStyle.Fill;
            this.PanelExTop.Location = new Point(3, 27);
            this.PanelExTop.Name = "PanelExTop";
            this.PanelExTop.Size = new Size(1898, 34);
            this.PanelExTop.TabIndex = 3;
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
            this.ContextMenuStripEx1.ResumeLayout(false);
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
        private ControlEx.ContextMenuStripEx ContextMenuStripEx1;
        private ToolStripMenuItem ToolStripMenuItemExpiration;
        private ToolStripMenuItem ToolStripMenuItemContractExpirationPartTimeJob;
        private ToolStripMenuItem ToolStripMenuItemContractExpirationLongJob新産別;
        private ToolStripMenuItem ToolStripMenuItemContractExpirationShortJob;
        private ToolStripMenuItem ToolStripMenuItemContractExpirationWrittenPledge;
        private ToolStripMenuItem ToolStripMenuItemContractExpirationLossWrittenPledge;
        private ToolStripMenuItem ToolStripMenuItemContractExpirationNotice;
        private ToolStripMenuItem ToolStripMenuItemContractExpirationPartTimeEmployee;
        private ToolStripMenuItem ToolStripMenuItemContractExpirationPartTimer;
        private FarPoint.Win.Spread.SheetView SheetViewList;
        private ToolStripMenuItem ToolStripMenuItemContractExpirationLongJob自運労運転士;
        private ToolStripMenuItem ToolStripMenuItemContractExpirationLongJob自運労作業員;
    }
}