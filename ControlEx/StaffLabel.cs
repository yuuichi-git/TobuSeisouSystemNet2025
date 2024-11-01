/*
 * 2024-10-14
 */
using ControlEx.Properties;

using Vo;

namespace ControlEx {
    public partial class StaffLabel : Label {
        private StaffLabel _thisLabel;
        private bool _cursorEnterFlag = false;
        private string _memo = string.Empty;
        private bool _memoFlag = false;
        private int _occupationCode = 99;
        private bool _proxyFlag = false;
        private bool _rollCallFlag = false;
        /*
         * Vo
         */
        private StaffMasterVo _staffMasterVo;
        /*
         * Labelのサイズ
         */
        private const float _panelWidth = 70;
        private const float _panelHeight = 116;

        /// <summary>
        /// Constractor
        /// </summary>
        /// <param name="staffMasterVo"></param>
        public StaffLabel(StaffMasterVo staffMasterVo) {
            /*
             * Vo
             */
            _staffMasterVo = staffMasterVo;
            /*
             * InitializeControl
             */
            InitializeComponent();
            this.AllowDrop = false;
            this.BackColor = Color.Transparent;
            this.BorderStyle = BorderStyle.None;
            this.Height = (int)_panelHeight;
            this.Margin = new Padding(2);
            this.Name = "StaffLabel";
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
            /*
             * 背景画像
             */
            pe.Graphics.DrawImage(ByteArrayToImage(Resources.CarLabelImage), 0, 0, Width, Height);
            // カーソル関係
            if (CursorEnterFlag)
                pe.Graphics.DrawImage(ByteArrayToImage(Resources.Filter), 0, 0, Width, Height);
            // メモ
            if (MemoFlag)
                pe.Graphics.DrawImage(ByteArrayToImage(Resources.Memo), 0, 0, Width, Height);
            // 代車
            if (ProxyFlag)
                pe.Graphics.DrawImage(ByteArrayToImage(Resources.Proxy), 0, 0, Width, Height);
            // 職種
            if (OccupationCode == 11)
                pe.Graphics.DrawImage(ByteArrayToImage(Resources.StaffLabelImageSagyouin), 0, 0, Width, Height);
            // 出庫点呼
            if (!RollCallFlag)
                pe.Graphics.DrawImage(ByteArrayToImage(Resources.StaffLabelImageTenko), 0, 0, Width, Height);
            /*
             * 氏名を描画
             */
            Font fontStaffLabel = new("メイリオ", 14, FontStyle.Regular, GraphicsUnit.Pixel);
            Rectangle rectangle = new(0, 0, Width, Height);
            StringFormat stringFormat = new();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            stringFormat.LineAlignment = StringAlignment.Center;
            pe.Graphics.DrawString(StaffMasterVo.DisplayName, fontStaffLabel, Brushes.Black, rectangle, stringFormat);
        }

        /// <summary>
        /// バイト配列をImageオブジェクトに変換
        /// </summary>
        /// <param name="arrayByte"></param>
        /// <returns></returns>
        private Image ByteArrayToImage(byte[] arrayByte) {
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
        public StaffLabel ThisLabel {
            get => this._thisLabel;
            set => this._thisLabel = value;
        }
        /// <summary>
        /// StaffMasterVo
        /// </summary>
        public StaffMasterVo StaffMasterVo {
            get => this._staffMasterVo;
            set => this._staffMasterVo = value;
        }
        /// <summary>
        /// True:カーソルが乗っている False:カーソルが外れている
        /// </summary>
        public bool CursorEnterFlag {
            get => this._cursorEnterFlag;
            set => this._cursorEnterFlag = value;
        }
        /// <summary>
        /// メモ本体
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
        /// 職種
        /// 10:運転手 11:作業員 20:事務職 99:指定なし
        /// </summary>
        public int OccupationCode {
            get => this._occupationCode;
            set => this._occupationCode = value;
        }
        /// <summary>
        /// true:代番 false:本番
        /// </summary>
        public bool ProxyFlag {
            get => this._proxyFlag;
            set => this._proxyFlag = value;
        }
        /// <summary>
        /// true:点呼実施済 false:点呼未実施
        /// </summary>
        public bool RollCallFlag {
            get => this._rollCallFlag;
            set => this._rollCallFlag = value;
        }
        
    }
}
