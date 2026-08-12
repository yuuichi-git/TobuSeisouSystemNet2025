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
            CcPdfView1 = new CcControl.CcPdfView();
            SuspendLayout();
            // 
            // CcPdfView1
            // 
            CcPdfView1.BorderStyle = BorderStyle.Fixed3D;
            CcPdfView1.Dock = DockStyle.Fill;
            CcPdfView1.Location = new Point(0, 0);
            CcPdfView1.Margin = new Padding(4, 3, 4, 3);
            CcPdfView1.MemoryStream = null;
            CcPdfView1.Name = "CcPdfView1";
            CcPdfView1.PdfDocument = null;
            CcPdfView1.Size = new Size(784, 1041);
            CcPdfView1.TabIndex = 0;
            // 
            // EmploymentAgreementView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 1041);
            Controls.Add(CcPdfView1);
            Name = "EmploymentAgreementView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ShowPicture";
            ResumeLayout(false);
        }

        #endregion

        private CcControl.CcPdfView CcPdfView1;
    }
}