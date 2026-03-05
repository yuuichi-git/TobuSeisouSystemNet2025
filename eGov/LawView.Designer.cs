namespace EGov {
    partial class LawView {
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
            this.CcTextBoxDetail = new ControlEx.CcTextBox();
            this.SuspendLayout();
            // 
            // CcTextBoxDetail
            // 
            this.CcTextBoxDetail.Location = new Point(236, 360);
            this.CcTextBoxDetail.Multiline = true;
            this.CcTextBoxDetail.Name = "CcTextBoxDetail";
            this.CcTextBoxDetail.Size = new Size(772, 208);
            this.CcTextBoxDetail.TabIndex = 0;
            // 
            // LawView
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1220, 688);
            this.Controls.Add(this.CcTextBoxDetail);
            this.Name = "LawView";
            this.Text = "LawView";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private ControlEx.CcTextBox CcTextBoxDetail;
    }
}