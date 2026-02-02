namespace EmploymentAgreement {
    partial class EmploymentAgreementView {
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
            this.PictureBoxEx1 = new ControlEx.CcPictureBox();
            ((System.ComponentModel.ISupportInitialize)this.PictureBoxEx1).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBoxEx1
            // 
            this.PictureBoxEx1.Dock = DockStyle.Fill;
            this.PictureBoxEx1.Location = new Point(0, 0);
            this.PictureBoxEx1.Name = "PictureBoxEx1";
            this.PictureBoxEx1.Size = new Size(714, 1007);
            this.PictureBoxEx1.SizeMode = PictureBoxSizeMode.Zoom;
            this.PictureBoxEx1.TabIndex = 0;
            this.PictureBoxEx1.TabStop = false;
            // 
            // ShowPicture
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(714, 1007);
            this.Controls.Add(this.PictureBoxEx1);
            this.Name = "ShowPicture";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "ShowPicture";
            ((System.ComponentModel.ISupportInitialize)this.PictureBoxEx1).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private ControlEx.CcPictureBox PictureBoxEx1;
    }
}