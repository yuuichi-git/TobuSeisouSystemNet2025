/*
 * 2024-11-06
 */
namespace EmploymentAgreement {
    public partial class ShowPicture : Form {
        private byte[] _picture;

        public ShowPicture(byte[] picture) {
            _picture = picture;
            /*
             * コントロール初期化
             */
            InitializeComponent();
            this.PictureBoxEx1.Image = Picture.Length != 0 ? (Image?)new ImageConverter().ConvertFrom(Picture) : null;
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
