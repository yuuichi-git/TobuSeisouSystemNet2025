namespace PaidLeave {
    partial class Remark {
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
            this.CcButtonTimeStamp = new CcControl.CcButton();
            this.CcButtonUpdate = new CcControl.CcButton();
            this.CcTextBoxRemark = new CcControl.CcTextBox();
            this.SuspendLayout();
            // 
            // CcButtonTimeStamp
            // 
            this.CcButtonTimeStamp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.CcButtonTimeStamp.ForeColor = SystemColors.ControlText;
            this.CcButtonTimeStamp.Location = new Point(41, 120);
            this.CcButtonTimeStamp.Name = "CcButtonTimeStamp";
            this.CcButtonTimeStamp.SetTextDirectionVertical = "";
            this.CcButtonTimeStamp.Size = new Size(116, 28);
            this.CcButtonTimeStamp.TabIndex = 5;
            this.CcButtonTimeStamp.Text = "タイムスタンプ";
            this.CcButtonTimeStamp.UseVisualStyleBackColor = true;
            this.CcButtonTimeStamp.Click += this.CcButton_Click;
            // 
            // CcButtonUpdate
            // 
            this.CcButtonUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.CcButtonUpdate.ForeColor = SystemColors.ControlText;
            this.CcButtonUpdate.Location = new Point(357, 116);
            this.CcButtonUpdate.Name = "CcButtonUpdate";
            this.CcButtonUpdate.SetTextDirectionVertical = "";
            this.CcButtonUpdate.Size = new Size(156, 36);
            this.CcButtonUpdate.TabIndex = 4;
            this.CcButtonUpdate.Text = "UPDATE";
            this.CcButtonUpdate.UseVisualStyleBackColor = true;
            this.CcButtonUpdate.Click += this.CcButton_Click;
            // 
            // CcTextBoxRemark
            // 
            this.CcTextBoxRemark.Font = new Font("Yu Gothic UI", 9.75F);
            this.CcTextBoxRemark.ImeMode = ImeMode.Hiragana;
            this.CcTextBoxRemark.Location = new Point(5, 8);
            this.CcTextBoxRemark.Multiline = true;
            this.CcTextBoxRemark.Name = "CcTextBoxRemark";
            this.CcTextBoxRemark.Size = new Size(524, 100);
            this.CcTextBoxRemark.TabIndex = 3;
            // 
            // Remark
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(534, 161);
            this.Controls.Add(this.CcButtonTimeStamp);
            this.Controls.Add(this.CcButtonUpdate);
            this.Controls.Add(this.CcTextBoxRemark);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Remark";
            this.Text = "Remark";
            this.FormClosing += this.Remark_FormClosing;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private CcControl.CcButton CcButtonTimeStamp;
        private CcControl.CcButton CcButtonUpdate;
        private CcControl.CcTextBox CcTextBoxRemark;
    }
}