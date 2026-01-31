namespace StockBox {
    partial class StockBoxs {
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
            this.PanelExUp = new ControlEx.CcPanel();
            this.ButtonExDispatch = new ControlEx.CcButton();
            this.ButtonExShortTime = new ControlEx.CcButton();
            this.ButtonExLongTime = new ControlEx.CcButton();
            this.ButtonExPartTime = new ControlEx.CcButton();
            this.ButtonExFullTime = new ControlEx.CcButton();
            this.ButtonExCar = new ControlEx.CcButton();
            this.ButtonExSet = new ControlEx.CcButton();
            this.TableLayoutPanelExBase.SuspendLayout();
            this.PanelExUp.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableLayoutPanelExBase
            // 
            this.TableLayoutPanelExBase.ColumnCount = 1;
            this.TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            this.TableLayoutPanelExBase.Controls.Add(this.MenuStripEx1, 0, 0);
            this.TableLayoutPanelExBase.Controls.Add(this.StatusStripEx1, 0, 3);
            this.TableLayoutPanelExBase.Controls.Add(this.PanelExUp, 0, 1);
            this.TableLayoutPanelExBase.Dock = DockStyle.Fill;
            this.TableLayoutPanelExBase.Location = new Point(0, 0);
            this.TableLayoutPanelExBase.Name = "TableLayoutPanelExBase";
            this.TableLayoutPanelExBase.RowCount = 4;
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.Size = new Size(921, 709);
            this.TableLayoutPanelExBase.TabIndex = 0;
            // 
            // MenuStripEx1
            // 
            this.MenuStripEx1.Location = new Point(0, 0);
            this.MenuStripEx1.Name = "MenuStripEx1";
            this.MenuStripEx1.Size = new Size(921, 24);
            this.MenuStripEx1.TabIndex = 0;
            this.MenuStripEx1.Text = "menuStripEx1";
            this.MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // StatusStripEx1
            // 
            this.StatusStripEx1.Location = new Point(0, 687);
            this.StatusStripEx1.Name = "StatusStripEx1";
            this.StatusStripEx1.Size = new Size(921, 22);
            this.StatusStripEx1.TabIndex = 1;
            this.StatusStripEx1.Text = "statusStripEx1";
            // 
            // PanelExUp
            // 
            this.PanelExUp.Controls.Add(this.ButtonExDispatch);
            this.PanelExUp.Controls.Add(this.ButtonExShortTime);
            this.PanelExUp.Controls.Add(this.ButtonExLongTime);
            this.PanelExUp.Controls.Add(this.ButtonExPartTime);
            this.PanelExUp.Controls.Add(this.ButtonExFullTime);
            this.PanelExUp.Controls.Add(this.ButtonExCar);
            this.PanelExUp.Controls.Add(this.ButtonExSet);
            this.PanelExUp.Dock = DockStyle.Fill;
            this.PanelExUp.Location = new Point(3, 27);
            this.PanelExUp.Name = "PanelExUp";
            this.PanelExUp.Size = new Size(915, 54);
            this.PanelExUp.TabIndex = 2;
            // 
            // ButtonExDispatch
            // 
            this.ButtonExDispatch.Location = new Point(776, 12);
            this.ButtonExDispatch.Name = "ButtonExDispatch";
            this.ButtonExDispatch.SetTextDirectionVertical = "";
            this.ButtonExDispatch.Size = new Size(124, 28);
            this.ButtonExDispatch.TabIndex = 6;
            this.ButtonExDispatch.Text = "派遣";
            this.ButtonExDispatch.UseVisualStyleBackColor = true;
            this.ButtonExDispatch.Click += this.ButtonEx_Click;
            // 
            // ButtonExShortTime
            // 
            this.ButtonExShortTime.Location = new Point(648, 12);
            this.ButtonExShortTime.Name = "ButtonExShortTime";
            this.ButtonExShortTime.SetTextDirectionVertical = "";
            this.ButtonExShortTime.Size = new Size(124, 28);
            this.ButtonExShortTime.TabIndex = 5;
            this.ButtonExShortTime.Text = "労供（短期）";
            this.ButtonExShortTime.UseVisualStyleBackColor = true;
            this.ButtonExShortTime.Click += this.ButtonEx_Click;
            // 
            // ButtonExLongTime
            // 
            this.ButtonExLongTime.Location = new Point(520, 12);
            this.ButtonExLongTime.Name = "ButtonExLongTime";
            this.ButtonExLongTime.SetTextDirectionVertical = "";
            this.ButtonExLongTime.Size = new Size(124, 28);
            this.ButtonExLongTime.TabIndex = 4;
            this.ButtonExLongTime.Text = "労供（長期）";
            this.ButtonExLongTime.UseVisualStyleBackColor = true;
            this.ButtonExLongTime.Click += this.ButtonEx_Click;
            // 
            // ButtonExPartTime
            // 
            this.ButtonExPartTime.Location = new Point(392, 12);
            this.ButtonExPartTime.Name = "ButtonExPartTime";
            this.ButtonExPartTime.SetTextDirectionVertical = "";
            this.ButtonExPartTime.Size = new Size(124, 28);
            this.ButtonExPartTime.TabIndex = 3;
            this.ButtonExPartTime.Text = "アルバイト";
            this.ButtonExPartTime.UseVisualStyleBackColor = true;
            this.ButtonExPartTime.Click += this.ButtonEx_Click;
            // 
            // ButtonExFullTime
            // 
            this.ButtonExFullTime.Location = new Point(264, 12);
            this.ButtonExFullTime.Name = "ButtonExFullTime";
            this.ButtonExFullTime.SetTextDirectionVertical = "";
            this.ButtonExFullTime.Size = new Size(124, 28);
            this.ButtonExFullTime.TabIndex = 2;
            this.ButtonExFullTime.Text = "社員等";
            this.ButtonExFullTime.UseVisualStyleBackColor = true;
            this.ButtonExFullTime.Click += this.ButtonEx_Click;
            // 
            // ButtonExCar
            // 
            this.ButtonExCar.Location = new Point(136, 12);
            this.ButtonExCar.Name = "ButtonExCar";
            this.ButtonExCar.SetTextDirectionVertical = "";
            this.ButtonExCar.Size = new Size(124, 28);
            this.ButtonExCar.TabIndex = 1;
            this.ButtonExCar.Text = "車両";
            this.ButtonExCar.UseVisualStyleBackColor = true;
            this.ButtonExCar.Click += this.ButtonEx_Click;
            // 
            // ButtonExSet
            // 
            this.ButtonExSet.Location = new Point(8, 12);
            this.ButtonExSet.Name = "ButtonExSet";
            this.ButtonExSet.SetTextDirectionVertical = "";
            this.ButtonExSet.Size = new Size(124, 28);
            this.ButtonExSet.TabIndex = 0;
            this.ButtonExSet.Text = "配車先";
            this.ButtonExSet.UseVisualStyleBackColor = true;
            this.ButtonExSet.Click += this.ButtonEx_Click;
            // 
            // StockBoxs
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(921, 709);
            this.Controls.Add(this.TableLayoutPanelExBase);
            this.MainMenuStrip = this.MenuStripEx1;
            this.Name = "StockBoxs";
            this.Text = "StockBoxs";
            this.FormClosing += this.StockBoxs_FormClosing;
            this.TableLayoutPanelExBase.ResumeLayout(false);
            this.TableLayoutPanelExBase.PerformLayout();
            this.PanelExUp.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private ControlEx.TableLayoutPanelEx TableLayoutPanelExBase;
        private ControlEx.MenuStripEx MenuStripEx1;
        private ControlEx.StatusStripEx StatusStripEx1;
        private ControlEx.CcPanel PanelExUp;
        private ControlEx.CcButton ButtonExSet;
        private ControlEx.CcButton ButtonExDispatch;
        private ControlEx.CcButton ButtonExShortTime;
        private ControlEx.CcButton ButtonExLongTime;
        private ControlEx.CcButton ButtonExPartTime;
        private ControlEx.CcButton ButtonExFullTime;
        private ControlEx.CcButton ButtonExCar;
    }
}