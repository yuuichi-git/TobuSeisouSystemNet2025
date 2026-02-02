/*
 * 2024-10-14
 */
using System.Diagnostics;

using ControlEx.Properties;

using Vo;

using Timer = System.Windows.Forms.Timer;

namespace ControlEx {
    public partial class CarLabel : Label {
        /*
         * デリゲート
         */
        public event EventHandler CarLabel_ContextMenuStrip_Opened = delegate { };
        public event EventHandler CarLabel_ToolStripMenuItem_Click = delegate { };
        public event MouseEventHandler CarLabel_OnMouseClick = delegate { };
        public event MouseEventHandler CarLabel_OnMouseDoubleClick = delegate { };
        public event MouseEventHandler CarLabel_OnMouseDown = delegate { };
        public event EventHandler CarLabel_OnMouseEnter = delegate { };
        public event EventHandler CarLabel_OnMouseLeave = delegate { };
        public event MouseEventHandler CarLabel_OnMouseMove = delegate { };
        public event MouseEventHandler CarLabel_OnMouseUp = delegate { };
        /*
         * プロパティ
         */
        private object _parentControl;
        private bool _cursorEnterFlag = false;
        private int _classificationCode = 0;

        private int _carGarageCode = 0;
        private bool _proxyFlag = false;
        private bool _memoFlag = false;
        private string _memo = string.Empty;
        private bool _emergencyVehicleFlag = false;     // 2025-07-31追加
        /*
         * Vo
         */
        private CarMasterVo _carMasterVo;
        /*
         * Labelのサイズ
         */
        private const float _panelWidth = 70;
        private const float _panelHeight = 116;
        // ToolTip
        private ToolTip _toolTip = new();
        /*
         * Timer
         */
        private Timer _timerControl = new();
        bool _doubleClickFlag = false;                                                              // シングルとダブルクリックの判別用
        private int _clickTime = 0;                                                                 // クリック間の時間を保持
        private int _doubleClickInterval = SystemInformation.DoubleClickTime;                       // ダブルクリックが有効な時間間隔(初期値500)

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
            // ContextMenuStripを初期化
            this.CreateContextMenuStrip();
            /*
             * ToolTip初期化
             */
            _toolTip.InitialDelay = 50; // ToolTipが表示されるまでの時間
            _toolTip.ReshowDelay = 1000; // ToolTipが表示されている時に、別のToolTipを表示するまでの時間
            _toolTip.AutoPopDelay = 10000; // ToolTipを表示する時間
            // Timer イベント登録
            _timerControl.Tick += this._timer_Tick;
        }

        /*
         * ContextMenuStrip
         */
        ContextMenuStrip contextMenuStrip = new();
        ToolStripMenuItem toolStripMenuItem00 = new("車両台帳を表示");
        ToolStripMenuItem toolStripMenuItem01 = new("出庫地"); // 親アイテム
        ToolStripMenuItem toolStripMenuItem01_0 = new("本社から出庫"); // 子アイテム１
        ToolStripMenuItem toolStripMenuItem01_1 = new("三郷から出庫"); // 子アイテム２
        ToolStripMenuItem toolStripMenuItem02 = new("代車処理"); // 親アイテム
        ToolStripMenuItem toolStripMenuItem02_0 = new("代車として記録する"); // 子アイテム１
        ToolStripMenuItem toolStripMenuItem02_1 = new("代車を解除する"); // 子アイテム２
        ToolStripMenuItem toolStripMenuItem03 = new("メモを作成・編集する");
        ToolStripMenuItem toolStripMenuItem04 = new("プロパティ");
        /// <summary>
        /// CreateContextMenuStrip
        /// </summary>
        private void CreateContextMenuStrip() {
            contextMenuStrip.Name = "ContextMenuStripHCarLabel";
            contextMenuStrip.Opened += ContextMenuStrip_Opened;
            this.ContextMenuStrip = contextMenuStrip;
            /*
             * 車両台帳を表示する
             */
            toolStripMenuItem00.Name = "ToolStripMenuItemCarVerification";
            toolStripMenuItem00.Click += ToolStripMenuItem_Click;
            contextMenuStrip.Items.Add(toolStripMenuItem00);
            /*
             * スペーサー
             */
            contextMenuStrip.Items.Add(new ToolStripSeparator());
            /*
             * 車庫地コード
             */
            toolStripMenuItem01.Name = "ToolStripMenuItemCarWarehouse";
            toolStripMenuItem01_0.Name = "ToolStripMenuItemCarWarehouseAdachi";
            toolStripMenuItem01_0.Click += ToolStripMenuItem_Click;
            toolStripMenuItem01.DropDownItems.Add(toolStripMenuItem01_0);
            contextMenuStrip.Items.Add(toolStripMenuItem01);
            toolStripMenuItem01_1.Name = "ToolStripMenuItemCarWarehouseMisato";
            toolStripMenuItem01_1.Click += ToolStripMenuItem_Click;
            toolStripMenuItem01.DropDownItems.Add(toolStripMenuItem01_1);
            contextMenuStrip.Items.Add(toolStripMenuItem01);
            /*
             * 代番処理
             */
            toolStripMenuItem02.Name = "ToolStripMenuItemCarProxy";
            toolStripMenuItem02_0.Name = "ToolStripMenuItemCarProxyTrue";
            toolStripMenuItem02_0.Click += ToolStripMenuItem_Click;
            toolStripMenuItem02.DropDownItems.Add(toolStripMenuItem02_0);
            contextMenuStrip.Items.Add(toolStripMenuItem02);
            toolStripMenuItem02_1.Name = "ToolStripMenuItemCarProxyFalse";
            toolStripMenuItem02_1.Click += ToolStripMenuItem_Click;
            toolStripMenuItem02.DropDownItems.Add(toolStripMenuItem02_1);
            contextMenuStrip.Items.Add(toolStripMenuItem02);
            /*
             * メモを作成・編集する
             */
            toolStripMenuItem03.Name = "ToolStripMenuItemCarMemo";
            toolStripMenuItem03.Click += ToolStripMenuItem_Click;
            contextMenuStrip.Items.Add(toolStripMenuItem03);
            /*
             * プロパティ
             */
            toolStripMenuItem04.Name = "ToolStripMenuItemCarProperty";
            toolStripMenuItem04.Click += ToolStripMenuItem_Click;
            contextMenuStrip.Items.Add(toolStripMenuItem04);
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
            if (CarGarageCode == 2)
                pe.Graphics.DrawImage(ByteArrayToImage(Resources.Misato), 0, 0, Width, Height);
            // メモ
            if (MemoFlag)
                pe.Graphics.DrawImage(ByteArrayToImage(Resources.Memo), 0, 0, Width, Height);
            // 代車
            if (ProxyFlag)
                pe.Graphics.DrawImage(ByteArrayToImage(Resources.Proxy), 0, 0, Width, Height);
            /*
             * 2025-07-31追加
             * 緊急車両登録
             */
            if (!EmergencyVehicleFlag)
                pe.Graphics.DrawImage(ByteArrayToImage(Resources.CarLabelImageEmergency), 0, 0, Width, Height);
            // カーソル関係
            if (CursorEnterFlag)
                pe.Graphics.DrawImage(ByteArrayToImage(Resources.Filter), 0, 0, Width, Height);
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
        private Image ByteArrayToImage(byte[] arrayByte) {
            ImageConverter imageConverter = new();
            return (Image)imageConverter.ConvertFrom(arrayByte);
        }

        /*
         * Event処理
         */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _timer_Tick(object sender, EventArgs e) {
            _clickTime += _timerControl.Interval; // 計測時間を保存しておく
            if (_clickTime > _doubleClickInterval) { // インターバルを過ぎたら
                _timerControl.Stop(); // タイマー停止
                _doubleClickFlag = false; // DoubleClickFlagを初期化
                _clickTime = 0; // 計測時間を初期化
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStrip_Opened(object sender, EventArgs e) {
            Debug.WriteLine("CarLabel ContextMenuStripOpened");

            CarLabel_ContextMenuStrip_Opened.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            Debug.WriteLine("CarLabel ToolStripMenuItemClick");

            CarLabel_ToolStripMenuItem_Click.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e) {
            /*
             * Click DoubleClickを判別
             */
            if (e.Clicks == 1) {
                _timerControl.Start(); // タイマーをスタートする
            } else {
                if (_clickTime < _doubleClickInterval)
                    _doubleClickFlag = true;
            }

            if (!_doubleClickFlag) {
                if ((ModifierKeys & Keys.Shift) == Keys.Shift) {
                    CarLabel_OnMouseClick.Invoke(this, e);
                } else if ((ModifierKeys & Keys.Control) == Keys.Control) {
                    if (this.MemoFlag) {
                        _toolTip.Show(this.Memo, this, 4, 4);
                        return;
                    }
                }
                // Down
                CarLabel_OnMouseDown.Invoke(this, e);
            } else {
                // DoubleClick
                CarLabel_OnMouseDoubleClick.Invoke(this, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e) {
            CursorEnterFlag = true;
            Refresh();
            //
            CarLabel_OnMouseEnter.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e) {
            // ToolTipを消す
            _toolTip.Hide(this);
            /*
             * 
             */
            CursorEnterFlag = false;
            Refresh();
            //
            CarLabel_OnMouseLeave.Invoke(this, e);
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
         * 
         * プロパティ
         * 
         */
        /// <summary>
        /// 格納されているSetControlを退避
        /// </summary>
        public object ParentControl {
            get => this._parentControl;
            set => this._parentControl = value;
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
        public int CarGarageCode {
            get => this._carGarageCode;
            set {
                this._carGarageCode = value;
                Refresh();
            }
        }
        /// <summary>
        /// true:メモが存在する false:メモが存在しない
        /// </summary>
        public bool MemoFlag {
            get => this._memoFlag;
            set => this._memoFlag = value;
        }
        /// <summary>
        /// メモ
        /// </summary>
        public string Memo {
            get => this._memo;
            set => this._memo = value;
        }
        /// <summary>
        /// true:代車 false:なし
        /// </summary>
        public bool ProxyFlag {
            get => this._proxyFlag;
            set {
                this._proxyFlag = value;
                Refresh();
            }
        }
        /// <summary>
        /// 緊急車両登録
        /// true:登録済 false:未登録
        /// </summary>
        public bool EmergencyVehicleFlag {
            get => this._emergencyVehicleFlag;
            set => this._emergencyVehicleFlag = value;
        }
    }
}
