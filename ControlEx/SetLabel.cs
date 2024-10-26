/*
 * 2024-10-12
 */
using ControlEx.Properties;

using Vo;

namespace ControlEx {
    public partial class SetLabel : Label {
        private bool _addWorkerFlag = false;
        private int _classificationCode = 0;
        private bool contactInfomationFlag = false;
        private bool _cursorEnterFlag = false;
        private bool faxTransmissionFlag = false;
        private bool _lastRollCallFlag = false;
        private int _managedSpaceCode = 0;
        private bool _setMemoFlag = false;
        private int _shiftCode = 0;
        private bool _standByFlag = false;
        private bool _telCallingFlag = false;
        /*
         * Vo
         */
        private SetMasterVo _setMasterVo;
        /*
         * Labelのサイズ
         */
        private const float _panelWidth = 70;
        private const float _panelHeight = 116;
        /*
         * Fontの定義
         */
        private readonly Font _drawFontSetLabel = new("Yu Gothic UI", 13, FontStyle.Regular, GraphicsUnit.Pixel);

        /// <summary>
        /// Constractor
        /// </summary>
        /// <param name="setMasterVo"></param>
        public SetLabel(SetMasterVo setMasterVo) {
            /*
             * Vo
             */
            _setMasterVo = setMasterVo;
            /*
             * InitializeControl
             */
            InitializeComponent();
            this.AllowDrop = false;
            this.BackColor = Color.Transparent;
            this.BorderStyle = BorderStyle.None;
            this.Height = (int)_panelHeight;
            this.Margin = new Padding(2);
            this.Name = "SetLabel";
            this.Padding = new(0);
            this.Width = (int)_panelWidth;
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
                    pe.Graphics.DrawImage(ByteArrayToImage(Resources.SetLabelImageY), 0, 0, _panelWidth, _panelHeight);
                    break;
                case 11:
                    pe.Graphics.DrawImage(ByteArrayToImage(Resources.SetLabelImageK), 0, 0, _panelWidth, _panelHeight);
                    break;
                default:
                    pe.Graphics.DrawImage(ByteArrayToImage(Resources.SetLabelImage), 0, 0, _panelWidth, _panelHeight);
                    break;
            }
            // 三郷車庫
            if (ManagedSpaceCode == 2)
                pe.Graphics.DrawImage(ByteArrayToImage(Resources.Misato), 0, 0, _panelWidth, _panelHeight);
            // FAX送信
            if (FaxTransmissionFlag)
                pe.Graphics.DrawImage(ByteArrayToImage(Resources.SetLabelFax), 0, 0, _panelWidth, _panelHeight);
            // 電話連絡
            if (TelCallingFlag)
                pe.Graphics.DrawImage(ByteArrayToImage(Resources.SetLabelTel), 0, 0, _panelWidth, _panelHeight);
            // カーソル関係
            if (CursorEnterFlag)
                pe.Graphics.DrawImage(ByteArrayToImage(Resources.Filter), 0, 0, _panelWidth, _panelHeight);
            // メモ
            if (SetMemoFlag)
                pe.Graphics.DrawImage(ByteArrayToImage(Resources.SetLabelImageMemo), 0, 0, _panelWidth, _panelHeight);
            // 帰庫点呼
            if (LastRollCallFlag)
                pe.Graphics.DrawImage(ByteArrayToImage(Resources.SetLabelImageTenko), 0, 0, _panelWidth, _panelHeight);
            /*
             * 文字(配車先)を描画
             */
            StringFormat stringFormat = new();
            stringFormat.LineAlignment = StringAlignment.Center;
            stringFormat.Alignment = StringAlignment.Center;
            pe.Graphics.DrawString(string.Concat(SetMasterVo.SetName1, "\r\n", SetMasterVo.SetName2, "\r\n", AddWorkerFlag ? "(作付)" : "  "),
                                                 new("Yu Gothic UI", 13, FontStyle.Regular, GraphicsUnit.Pixel),
                                                 new SolidBrush(Color.Black),
                                                 new Rectangle(0, 0, (int)_panelWidth, (int)_panelHeight), stringFormat);
            // 番手コード
            switch (ShiftCode) {
                case 1:
                    pe.Graphics.DrawString("早番", new("Yu Gothic UI", 11, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.Red, new Point(7, 90));
                    break;
                case 2:
                    pe.Graphics.DrawString("遅番", new("Yu Gothic UI", 11, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.Red, new Point(7, 90));
                    break;
            }
            // 待機フラグ
            if (_standByFlag)
                pe.Graphics.DrawString("待機", new("Yu Gothic UI", 11, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.Red, new Point(37, 90));
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
        /// true:作業員付き false:作業員なし
        /// </summary>
        public bool AddWorkerFlag {
            get => this._addWorkerFlag;
            set => this._addWorkerFlag = value;
        }
        /// <summary>
        /// True:カーソルが乗っている False:カーソルが外れている
        /// </summary>
        public bool CursorEnterFlag {
            get => this._cursorEnterFlag;
            set => this._cursorEnterFlag = value;
        }
        /// <summary>
        /// 10:雇上 11:区契 12:臨時 20:清掃工場 30:社内 50:一般 51:社用車 99:指定なし
        /// </summary>
        public int ClassificationCode {
            get => this._classificationCode;
            set => this._classificationCode = value;
        }
        /// <summary>
        /// true:連絡事項あり false:連絡事項なし
        /// </summary>
        public bool ContactInfomationFlag {
            get => this.contactInfomationFlag;
            set => this.contactInfomationFlag = value;
        }
        /// <summary>
        /// 代車・代番連絡
        /// true:Fax送信 false:なし
        /// </summary>
        public bool FaxTransmissionFlag {
            get => this.faxTransmissionFlag;
            set => this.faxTransmissionFlag = value;
        }
        /// <summary>
        /// true:帰庫点呼記録済 false:未点呼
        /// </summary>
        public bool LastRollCallFlag {
            get => this._lastRollCallFlag;
            set => this._lastRollCallFlag = value;
        }
        /// <summary>
        /// 0:該当なし 1:足立 2:三郷
        /// </summary>
        public int ManagedSpaceCode {
            get => this._managedSpaceCode;
            set => this._managedSpaceCode = value;
        }
        /// <summary>
        /// 
        /// </summary>
        public SetMasterVo SetMasterVo {
            get => this._setMasterVo;
            set => this._setMasterVo = value;
        }
        /// <summary>
        /// true:メモが存在する false:メモが存在しない
        /// </summary>
        public bool SetMemoFlag {
            get => this._setMemoFlag;
            set => this._setMemoFlag = value;
        }
        /// <summary>
        /// 0:指定なし 1:早番 2:遅番
        /// </summary>
        public int ShiftCode {
            get => this._shiftCode;
            set => this._shiftCode = value;
        }
        /// <summary>
        /// true:待機 false:通常
        /// </summary>
        public bool StandByFlag {
            get => this._standByFlag;
            set => this._standByFlag = value;
        }
        /// <summary>
        /// 代車・代番連絡
        /// true:電話する false:電話しない
        /// </summary>
        public bool TelCallingFlag {
            get => this._telCallingFlag;
            set => this._telCallingFlag = value;
        }
    }
}
