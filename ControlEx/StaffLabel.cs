/*
 * 2024-10-14
 */
using System.Diagnostics;

using ControlEx.Properties;

using Vo;

using Timer = System.Windows.Forms.Timer;

namespace ControlEx {
    public partial class StaffLabel : Label {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        /*
         * デリゲート
         */
        public event EventHandler StaffLabel_ContextMenuStrip_Opened = delegate { };
        public event EventHandler StaffLabel_ToolStripMenuItem_Click = delegate { };
        public event MouseEventHandler StaffLabel_OnMouseClick = delegate { };
        public event MouseEventHandler StaffLabel_OnMouseDoubleClick = delegate { };
        public event MouseEventHandler StaffLabel_OnMouseDown = delegate { };
        public event EventHandler StaffLabel_OnMouseEnter = delegate { };
        public event EventHandler StaffLabel_OnMouseLeave = delegate { };
        public event MouseEventHandler StaffLabel_OnMouseMove = delegate { };
        public event MouseEventHandler StaffLabel_OnMouseUp = delegate { };
        /*
         * プロパティ
         */
        private object _parentControl;
        private bool _cursorEnterFlag = false;

        private int _occupationCode = 99;
        private bool _proxyFlag = false;
        private bool _rollCallFlag = false;
        private DateTime _rollCallYmdHms = new DateTime(1900, 01, 01);
        private bool _memoFlag = false;
        private string _memo = string.Empty;
        /*
         * Vo
         */
        private StaffMasterVo _staffMasterVo;
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
        bool _doubleClickFlag = false; // シングルとダブルクリックの判別用
        private int _clickTime = 0;// クリック間の時間を保持
        private int _doubleClickInterval = SystemInformation.DoubleClickTime;// ダブルクリックが有効な時間間隔(初期値500)

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

        /// <summary>
        /// CreateContextMenuStrip
        /// </summary>
        private void CreateContextMenuStrip() {
            ContextMenuStrip contextMenuStrip = new();
            contextMenuStrip.Name = "ContextMenuStripHStaffLabel";
            contextMenuStrip.Opened += ContextMenuStrip_Opened;
            this.ContextMenuStrip = contextMenuStrip;
            /*
             * 従事者台帳を表示する
             */
            ToolStripMenuItem toolStripMenuItem00 = new("従事者台帳を表示");
            toolStripMenuItem00.Name = "ToolStripMenuItemStaffDetail";
            toolStripMenuItem00.Click += ToolStripMenuItem_Click;
            contextMenuStrip.Items.Add(toolStripMenuItem00);
            /*
             * 従事者免許証を表示する
             */
            ToolStripMenuItem toolStripMenuItem01 = new("免許証を表示");
            toolStripMenuItem01.Name = "ToolStripMenuItemStaffLicense";
            toolStripMenuItem01.Click += ToolStripMenuItem_Click;
            contextMenuStrip.Items.Add(toolStripMenuItem01);
            /*
             * スペーサー
             */
            contextMenuStrip.Items.Add(new ToolStripSeparator());
            /*
             * 代番処理
             */
            ToolStripMenuItem toolStripMenuItem02 = new("代番処理"); // 親アイテム
            toolStripMenuItem02.Name = "ToolStripMenuItemStaffProxy";

            ToolStripMenuItem toolStripMenuItem02_0 = new("代番として記録する"); // 子アイテム１
            toolStripMenuItem02_0.Name = "ToolStripMenuItemStaffProxyTrue";
            toolStripMenuItem02_0.Click += ToolStripMenuItem_Click;
            toolStripMenuItem02.DropDownItems.Add(toolStripMenuItem02_0);
            contextMenuStrip.Items.Add(toolStripMenuItem02);

            ToolStripMenuItem toolStripMenuItem02_1 = new("代番を解除する"); // 子アイテム２
            toolStripMenuItem02_1.Name = "ToolStripMenuItemStaffProxyFalse";
            toolStripMenuItem02_1.Click += ToolStripMenuItem_Click;
            toolStripMenuItem02.DropDownItems.Add(toolStripMenuItem02_1);
            contextMenuStrip.Items.Add(toolStripMenuItem02);
            /*
             * 料金設定
             */
            ToolStripMenuItem toolStripMenuItem03 = new("職種設定"); // 親アイテム
            toolStripMenuItem03.Name = "ToolStripMenuItemStaffOccupation";

            ToolStripMenuItem toolStripMenuItem03_0 = new("運転手の料金設定にする(運賃コードに依存)"); // 子アイテム１
            toolStripMenuItem03_0.Name = "ToolStripMenuItemStaffOccupation10";
            toolStripMenuItem03_0.Click += ToolStripMenuItem_Click;
            toolStripMenuItem03.DropDownItems.Add(toolStripMenuItem03_0);
            contextMenuStrip.Items.Add(toolStripMenuItem03);

            ToolStripMenuItem toolStripMenuItem03_1 = new("作業員の料金設定にする"); // 子アイテム２
            toolStripMenuItem03_1.Name = "ToolStripMenuItemStaffOccupation11";
            toolStripMenuItem03_1.Click += ToolStripMenuItem_Click;
            toolStripMenuItem03.DropDownItems.Add(toolStripMenuItem03_1);
            contextMenuStrip.Items.Add(toolStripMenuItem03);
            /*
             * 電話連絡・出勤確認
             */
            ToolStripMenuItem toolStripMenuItem04 = new("出勤確認(電話確認)"); // 親アイテム
            toolStripMenuItem04.Name = "ToolStripMenuItemStaffTelephoneMark";

            ToolStripMenuItem toolStripMenuItem04_0 = new("出勤を確認済"); // 子アイテム１
            toolStripMenuItem04_0.Name = "ToolStripMenuItemTelephoneMarkTrue";
            toolStripMenuItem04_0.Click += ToolStripMenuItem_Click;
            toolStripMenuItem04.DropDownItems.Add(toolStripMenuItem04_0);
            contextMenuStrip.Items.Add(toolStripMenuItem04);

            ToolStripMenuItem toolStripMenuItem04_1 = new("出勤を未確認"); // 子アイテム２
            toolStripMenuItem04_1.Name = "ToolStripMenuItemStaffTelephoneMarkFalse";
            toolStripMenuItem04_1.Click += ToolStripMenuItem_Click;
            toolStripMenuItem04.DropDownItems.Add(toolStripMenuItem04_1);
            contextMenuStrip.Items.Add(toolStripMenuItem04);
            /*
             * メモを作成・編集する("Ctrl + Click")
             */
            ToolStripMenuItem toolStripMenuItem05 = new("メモを作成・編集する('Ctrl + Click')");
            toolStripMenuItem05.Name = "ToolStripMenuItemStaffMemo";
            toolStripMenuItem05.Click += ToolStripMenuItem_Click;
            contextMenuStrip.Items.Add(toolStripMenuItem05);
            /*
             * プロパティ
             */
            ToolStripMenuItem toolStripMenuItem07 = new("プロパティ");
            toolStripMenuItem07.Name = "ToolStripMenuItemStaffProperty";
            toolStripMenuItem07.Click += ToolStripMenuItem_Click;
            contextMenuStrip.Items.Add(toolStripMenuItem07);
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
            switch (this.StaffMasterVo.Belongs) {
                case 12:
                    pe.Graphics.DrawImage(ByteArrayToImage(Resources.StaffLabelImagePartTime), 0, 0, Width, Height);
                    break;
                case 20:
                case 21:
                    switch (this.StaffMasterVo.JobForm) {
                        case 10:
                            pe.Graphics.DrawImage(ByteArrayToImage(Resources.StaffLabelImage), 0, 0, Width, Height);
                            break;
                        case 11:
                            pe.Graphics.DrawImage(ByteArrayToImage(Resources.StaffLabelImageShortTime), 0, 0, Width, Height);
                            break;
                    }
                    break;
                default:
                    pe.Graphics.DrawImage(ByteArrayToImage(Resources.CarLabelImage), 0, 0, Width, Height);
                    break;
            }
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
            // カーソル関係
            if (CursorEnterFlag)
                pe.Graphics.DrawImage(ByteArrayToImage(Resources.Filter), 0, 0, Width, Height);
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
            Debug.WriteLine("StaffLabel ContextMenuStripOpened");

            StaffLabel_ContextMenuStrip_Opened.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            Debug.WriteLine("StaffLabel ToolStripMenuItemClick");

            StaffLabel_ToolStripMenuItem_Click.Invoke(sender, e);
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
                    StaffLabel_OnMouseClick.Invoke(this, e);
                } else if ((ModifierKeys & Keys.Control) == Keys.Control) {
                    if (this.MemoFlag) {
                        _toolTip.Show(this.Memo, this, 4, 4);
                        return;
                    }
                }
                // Down
                StaffLabel_OnMouseDown.Invoke(this, e);
            } else {
                // DoubleClick
                StaffLabel_OnMouseDoubleClick.Invoke(this, e);
            }
        }

        /// <summary>
        /// このクラス内だけで利用する
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e) {
            CursorEnterFlag = true;
            Refresh();
            //
            StaffLabel_OnMouseEnter.Invoke(this, e);
        }

        /// <summary>
        /// このクラス内だけで利用する
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
            StaffLabel_OnMouseLeave.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e) {
            //
            StaffLabel_OnMouseMove.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e) {
            Debug.WriteLine("StaffLabel MouseUp");
            //
            StaffLabel_OnMouseUp.Invoke(this, e);
        }

        /*
         * 
         * プロパティ
         * 
         */
        /// <summary>
        /// StaffMasterVo
        /// </summary>
        public StaffMasterVo StaffMasterVo {
            get => this._staffMasterVo;
            set => this._staffMasterVo = value;
        }
        /// <summary>
        /// 格納されているSetControlを退避
        /// </summary>
        public object ParentControl {
            get => this._parentControl;
            set => this._parentControl = value;
        }
        /// <summary>
        /// True:カーソルが乗っている False:カーソルが外れている
        /// </summary>
        public bool CursorEnterFlag {
            get => this._cursorEnterFlag;
            set => this._cursorEnterFlag = value;
        }

        /// <summary>
        /// 職種
        /// 10:運転手 11:作業員 20:事務職 99:指定なし
        /// </summary>
        public int OccupationCode {
            get => this._occupationCode;
            set {
                this._occupationCode = value;
                Refresh();
            }
        }
        /// <summary>
        /// true:代番 false:本番
        /// </summary>
        public bool ProxyFlag {
            get => this._proxyFlag;
            set {
                this._proxyFlag = value;
                Refresh();
            }
        }
        /// <summary>
        /// true:点呼実施済 false:点呼未実施
        /// </summary>
        public bool RollCallFlag {
            get => this._rollCallFlag;
            set {
                this._rollCallFlag = value;
                Refresh();
                if (this.RollCallFlag) {
                    this.RollCallYmdHms = DateTime.Now;
                } else {
                    this.RollCallYmdHms = _defaultDateTime;
                }
            }
        }
        /// <summary>
        /// 点呼日時
        /// </summary>
        public DateTime RollCallYmdHms {
            get => this._rollCallYmdHms;
            set {
                this._rollCallYmdHms = value;
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
        /// メモ本体
        /// </summary>
        public string Memo {
            get => this._memo;
            set => this._memo = value;
        }
    }
}
