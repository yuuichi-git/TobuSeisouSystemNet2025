/*
 * 2024-10-14
 */
using Vo;

namespace ControlEx {
    public partial class CarLabel : Label {
        private bool _cursorEnterFlag = false;
        /*
         * Vo
         */
        private CarMasterVo _carMasterVo;
        /*
         * Labelのサイズ
         */
        private const float _panelWidth = 70;
        private const float _panelHeight = 116;

        /// <summary>
        /// Constractor
        /// </summary>
        /// <param name="carMasterVo"></param>
        public CarLabel(CarMasterVo carMasterVo) {
            /*
             * Vo
             */
            _carMasterVo = carMasterVo;
            /*
             * InitializeControl
             */
            InitializeComponent();
            this.AllowDrop = false;
            this.BackColor = Color.Transparent;
            this.BorderStyle = BorderStyle.None;
            this.Height = (int)_panelHeight;
            this.Margin = new Padding(2);
            this.Name = "CarLabel";
            this.Padding = new(0);
            this.Width = (int)_panelWidth;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
            /*
             * 背景画像
             */
            pe.Graphics.DrawImage(ByteArrayToImage(Properties.Resources.CarLabelImage), 0, 0, _panelWidth, _panelHeight);
            /*
             * カーソル関係
             */
            if (CursorEnterFlag)
                pe.Graphics.DrawImage(ByteArrayToImage(Properties.Resources.Filter), 0, 0, _panelWidth, _panelHeight);
            /*
             * 文字(車両)を描画
             */
            StringFormat stringFormat = new();
            stringFormat.LineAlignment = StringAlignment.Center;
            stringFormat.Alignment = StringAlignment.Center;
            string number = string.Concat(CarMasterVo.RegistrationNumber1, CarMasterVo.RegistrationNumber2, "\r\n",
                                          CarMasterVo.RegistrationNumber3, CarMasterVo.RegistrationNumber4, "\r\n",
                                          CarMasterVo.DisguiseKind1, CarMasterVo.DoorNumber != 0 ? CarMasterVo.DoorNumber : " ", "\r\n", " ");
            if (CarMasterVo.ExpirationDate < DateTime.Now.Date) {
                pe.Graphics.DrawString(number, new("Yu Gothic UI", 13, FontStyle.Regular, GraphicsUnit.Pixel), new SolidBrush(Color.Red), new Rectangle(0, 0, (int)_panelWidth, (int)_panelHeight), stringFormat);
            } else {
                pe.Graphics.DrawString(number, new("Yu Gothic UI", 13, FontStyle.Regular, GraphicsUnit.Pixel), new SolidBrush(Color.Black), new Rectangle(0, 0, (int)_panelWidth, (int)_panelHeight), stringFormat);
            }
        }

        /// <summary>
        /// バイト配列をImageオブジェクトに変換
        /// </summary>
        /// <param name="arrayByte"></param>
        /// <returns></returns>
        public Image ByteArrayToImage(byte[] arrayByte) {
            ImageConverter imageConverter = new();
            return (Image)imageConverter.ConvertFrom(arrayByte);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e) {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e) {
            CursorEnterFlag = true;
            Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e) {
            CursorEnterFlag = false;
            Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e) {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e) {

        }

        /*
         * プロパティ
         */
        /// <summary>
        /// 
        /// </summary>
        public CarMasterVo CarMasterVo {
            get => this._carMasterVo;
            set => this._carMasterVo = value;
        }
        /// <summary>
        /// True:カーソルが乗っている False:カーソルが外れている
        /// </summary>
        public bool CursorEnterFlag {
            get => this._cursorEnterFlag;
            set => this._cursorEnterFlag = value;
        }
        
    }
}
