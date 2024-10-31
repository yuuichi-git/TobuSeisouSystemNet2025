/*
 * 2024-10-14
 */
using ControlEx.Properties;

using Vo;

namespace ControlEx {
    public partial class CarLabel : Label {
        private CarLabel _thisLabel;
        private int _classificationCode = 0;
        private bool _cursorEnterFlag = false;
        private int _managedSpaceCode = 0;
        private string _memo = string.Empty;
        private bool _memoFlag = false;
        private bool _proxyFlag = false;
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
            // 自分自身の参照を退避
            _thisLabel = this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
            // 背景画像
            switch (ClassificationCode) {
                case 10:
                    pe.Graphics.DrawImage(ByteArrayToImage(Resources.CarLabelImageY), 0, 0, Width, Height);
                    break;
                case 11:
                    pe.Graphics.DrawImage(ByteArrayToImage(Resources.CarLabelImageK), 0, 0, Width, Height);
                    break;
                default:
                    pe.Graphics.DrawImage(ByteArrayToImage(Resources.CarLabelImage), 0, 0, Width, Height);
                    break;
            }
            // 三郷車庫
            if (ManagedSpaceCode == 2)
                pe.Graphics.DrawImage(ByteArrayToImage(Resources.Misato), 0, 0, Width, Height);
            /*
             * カーソル関係
             */
            if (CursorEnterFlag)
                pe.Graphics.DrawImage(ByteArrayToImage(Resources.Filter), 0, 0, Width, Height);
            // メモ
            if (MemoFlag)
                pe.Graphics.DrawImage(ByteArrayToImage(Resources.Memo), 0, 0, Width, Height);
            // 代車
            if (ProxyFlag)
                pe.Graphics.DrawImage(ByteArrayToImage(Resources.Proxy), 0, 0, Width, Height);
            /*
             * 文字(車両)を描画
             */
            Font fontCarLabel = new("Yu Gothic UI", 13, FontStyle.Regular, GraphicsUnit.Pixel);
            Rectangle rectangle = new(0, 0, Width, Height);
            StringFormat stringFormat = new();
            stringFormat.LineAlignment = StringAlignment.Center;
            stringFormat.Alignment = StringAlignment.Center;
            string number = string.Concat(CarMasterVo.RegistrationNumber1, CarMasterVo.RegistrationNumber2, "\r\n",
                                          CarMasterVo.RegistrationNumber3, CarMasterVo.RegistrationNumber4, "\r\n",
                                          CarMasterVo.DisguiseKind1, CarMasterVo.DoorNumber != 0 ? CarMasterVo.DoorNumber : " ", "\r\n", " ");
            if (CarMasterVo.ExpirationDate < DateTime.Now.Date) {
                pe.Graphics.DrawString(number, fontCarLabel, new SolidBrush(Color.Red), rectangle, stringFormat);
            } else {
                pe.Graphics.DrawString(number, fontCarLabel, new SolidBrush(Color.Black), rectangle, stringFormat);
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
        /// 自分自身の参照を保持
        /// </summary>
        public CarLabel ThisLabel {
            get => this._thisLabel;
            set => this._thisLabel = value;
        }
        /// <summary>
        /// 
        /// </summary>
        public CarMasterVo CarMasterVo {
            get => this._carMasterVo;
            set => this._carMasterVo = value;
        }
        /// <summary>
        /// 10:雇上 11:区契 12:臨時 20:清掃工場 30:社内 50:一般 51:社用車 99:指定なし
        /// </summary>
        public int ClassificationCode {
            get => this._classificationCode;
            set => this._classificationCode = value;
        }
        /// <summary>
        /// True:カーソルが乗っている False:カーソルが外れている
        /// </summary>
        public bool CursorEnterFlag {
            get => this._cursorEnterFlag;
            set => this._cursorEnterFlag = value;
        }
        /// <summary>
        /// 0:該当なし 1:足立 2:三郷
        /// </summary>
        public int ManagedSpaceCode {
            get => this._managedSpaceCode;
            set => this._managedSpaceCode = value;
        }
        /// <summary>
        /// メモ
        /// </summary>
        public string Memo {
            get => this._memo;
            set => this._memo = value;
        }
        /// <summary>
        /// true:メモが存在する false:メモが存在しない
        /// </summary>
        public bool MemoFlag {
            get => this._memoFlag;
            set => this._memoFlag = value;
        }
        /// <summary>
        /// true:代車 false:なし
        /// </summary>
        public bool ProxyFlag {
            get => this._proxyFlag;
            set => this._proxyFlag = value;
        }

    }
}
