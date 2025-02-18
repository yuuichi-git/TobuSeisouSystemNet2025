/*
 * 2024-11-06
 */
namespace EmploymentAgreement {
    public partial class EmploymentAgreementView : Form {
        private byte[] _picture;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="picture"></param>
        public EmploymentAgreementView(byte[] picture) {
            _picture = picture;
            /*
             * コントロール初期化
             */
            InitializeComponent();
            this.PictureBoxEx1.Image = Picture.Length != 0 ? (Image?)new ImageConverter().ConvertFrom(Picture) : null;
            this.TopMost = true;
        }

        private void ShowPicture_SizeChanged(object sender, EventArgs e) {
            this.Text = string.Concat("ShowPicture ", this.Size.Width, " - ", this.Size.Height);
        }

        /// <summary>
        /// 画像
        /// </summary>
        public byte[] Picture {
            get => this._picture;
            set => this._picture = value;
        }
    }
}
