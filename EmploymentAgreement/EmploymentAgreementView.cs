/*
 * 2024-11-06
 */
namespace EmploymentAgreement {
    public partial class EmploymentAgreementView : Form {
        private byte[] _image;

        public EmploymentAgreementView(byte[] image) {
            _image = image;
            InitializeComponent();

            this.CcPdfView1.SetPdfBytes(image);
            this.TopMost = true;
        }

        private void EmploymentAgreementView_SizeChanged(object sender, EventArgs e) {
            this.Text = string.Concat("ShowPicture ", this.Size.Width, " - ", this.Size.Height);
        }

        public byte[] Image {
            get => _image;
            set {
                _image = value;
                this.CcPdfView1.SetPdfBytes(value);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e) {
            base.OnFormClosed(e);

            // PdfiumViewer の破棄
            this.CcPdfView1.Dispose();
        }
    }
}
