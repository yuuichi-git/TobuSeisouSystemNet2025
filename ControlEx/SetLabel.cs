/*
 * 2024-10-12
 */
using System.Diagnostics;

using ControlEx.Properties;

using Vo;

using Timer = System.Windows.Forms.Timer;

namespace ControlEx {
    public partial class SetLabel : Label {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        /*
         * デリゲート
         */
        public event EventHandler SetLabel_ContextMenuStrip_Opened = delegate { };
        public event EventHandler SetLabel_ToolStripMenuItem_Click = delegate { };
        public event MouseEventHandler SetLabel_OnMouseClick = delegate { };
        public event MouseEventHandler SetLabel_OnMouseDoubleClick = delegate { };
        public event MouseEventHandler SetLabel_OnMouseDown = delegate { };
        public event EventHandler SetLabel_OnMouseEnter = delegate { };
        public event EventHandler SetLabel_OnMouseLeave = delegate { };
        public event MouseEventHandler SetLabel_OnMouseMove = delegate { };
        public event MouseEventHandler SetLabel_OnMouseUp = delegate { };
        /*
         * プロパティ
         */
        private object _parentControl;
        private bool _cursorEnterFlag = false;
        private bool _operationFlag;

        private int _managedSpaceCode = 0;
        private int _classificationCode = 0;
        private bool _lastRollCallFlag = false;
        private DateTime _lastRollCallYmdHms = new DateTime(1900, 01, 01);
        private bool _memoFlag = false;
        private string _memo = string.Empty;
        private int _shiftCode = 0;
        private bool _standByFlag = false;
        private bool _addWorkerFlag = false;
        private bool _contactInfomationFlag = false;
        private bool _telCallingFlag = false;
        private bool _faxTransmissionFlag = false;
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
        /// コンストラクター
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
            contextMenuStrip.Name = "ContextMenuStripHSetLabel";
            contextMenuStrip.Opened += ContextMenuStrip_Opened;
            this.ContextMenuStrip = contextMenuStrip;
            /*
             * 配車先の情報を表示する
             */
            ToolStripMenuItem toolStripMenuItem00 = new("配車先の情報を表示する");
            toolStripMenuItem00.Name = "ToolStripMenuItemSetDetail";
            toolStripMenuItem00.Click += ToolStripMenuItem_Click;
            contextMenuStrip.Items.Add(toolStripMenuItem00);
            /*
             * 日報を作成する
             */
            ToolStripMenuItem toolStripMenuItem01 = new("日報を印刷する");
            toolStripMenuItem01.Name = "ToolStripMenuItemDriversReport";
            toolStripMenuItem01.Click += ToolStripMenuItem_Click;
            contextMenuStrip.Items.Add(toolStripMenuItem01);
            /*
             * スペーサー
             */
            contextMenuStrip.Items.Add(new ToolStripSeparator());
            /*
             * 配車の状態
             */
            ToolStripMenuItem toolStripMenuItem02 = new("配車の状態"); // 親アイテム
            toolStripMenuItem02.Name = "ToolStripMenuItemSetOperation";
            ToolStripMenuItem toolStripMenuItem02_0 = new("配車する"); // 子アイテム１
            toolStripMenuItem02_0.Name = "ToolStripMenuItemSetOperationTrue";
            toolStripMenuItem02_0.Click += ToolStripMenuItem_Click;
            toolStripMenuItem02.DropDownItems.Add(toolStripMenuItem02_0);
            contextMenuStrip.Items.Add(toolStripMenuItem02);
            ToolStripMenuItem toolStripMenuItem02_1 = new("休車する"); // 子アイテム２
            toolStripMenuItem02_1.Name = "ToolStripMenuItemSetOperationFalse";
            toolStripMenuItem02_1.Click += ToolStripMenuItem_Click;
            toolStripMenuItem02.DropDownItems.Add(toolStripMenuItem02_1);
            contextMenuStrip.Items.Add(toolStripMenuItem02);
            /*
             * 管理地
             */
            ToolStripMenuItem toolStripMenuItem03 = new("管理地"); // 親アイテム
            toolStripMenuItem03.Name = "ToolStripMenuItemSetWarehouse";
            ToolStripMenuItem toolStripMenuItem03_0 = new("本社管理"); // 子アイテム１
            toolStripMenuItem03_0.Name = "ToolStripMenuItemSetWarehouseAdachi";
            toolStripMenuItem03_0.Click += ToolStripMenuItem_Click;
            toolStripMenuItem03.DropDownItems.Add(toolStripMenuItem03_0);
            contextMenuStrip.Items.Add(toolStripMenuItem03);
            ToolStripMenuItem toolStripMenuItem03_1 = new("三郷管理"); // 子アイテム２
            toolStripMenuItem03_1.Name = "ToolStripMenuItemSetWarehouseMisato";
            toolStripMenuItem03_1.Click += ToolStripMenuItem_Click;
            toolStripMenuItem03.DropDownItems.Add(toolStripMenuItem03_1);
            contextMenuStrip.Items.Add(toolStripMenuItem03);
            /*
             * 雇上・区契
             */
            ToolStripMenuItem toolStripMenuItem04 = new("雇上・区契"); // 親アイテム
            toolStripMenuItem04.Name = "ToolStripMenuItemClassification";
            ToolStripMenuItem toolStripMenuItem04_0 = new("雇上契約に変更する"); // 子アイテム１
            toolStripMenuItem04_0.Name = "ToolStripMenuItemClassificationYOUJYOU";
            toolStripMenuItem04_0.Click += ToolStripMenuItem_Click;
            toolStripMenuItem04.DropDownItems.Add(toolStripMenuItem04_0);
            contextMenuStrip.Items.Add(toolStripMenuItem04);
            ToolStripMenuItem toolStripMenuItem04_1 = new("区契約に変更する"); // 子アイテム２
            toolStripMenuItem04_1.Name = "ToolStripMenuItemClassificationKUKEI";
            toolStripMenuItem04_1.Click += ToolStripMenuItem_Click;
            toolStripMenuItem04.DropDownItems.Add(toolStripMenuItem04_1);
            contextMenuStrip.Items.Add(toolStripMenuItem04);
            /*
             * 作業員付きの配置
             */
            ToolStripMenuItem toolStripMenuItem05 = new("作業員付きの配置"); // 親アイテム
            toolStripMenuItem05.Name = "ToolStripMenuItemAddWorker";
            ToolStripMenuItem toolStripMenuItem05_0 = new("作業員付きに変更する"); // 子アイテム１
            toolStripMenuItem05_0.Name = "ToolStripMenuItemAddWorkerTrue";
            toolStripMenuItem05_0.Click += ToolStripMenuItem_Click;
            toolStripMenuItem05.DropDownItems.Add(toolStripMenuItem05_0);
            contextMenuStrip.Items.Add(toolStripMenuItem05);
            ToolStripMenuItem toolStripMenuItem05_1 = new("作業員なしに変更する"); // 子アイテム２
            toolStripMenuItem05_1.Name = "ToolStripMenuItemAddWorkerFalse";
            toolStripMenuItem05_1.Click += ToolStripMenuItem_Click;
            toolStripMenuItem05.DropDownItems.Add(toolStripMenuItem05_1);
            contextMenuStrip.Items.Add(toolStripMenuItem05);
            /*
             * 早番・遅番
             */
            ToolStripMenuItem toolStripMenuItem06 = new("早番・遅番"); // 親アイテム
            toolStripMenuItem06.Name = "ToolStripMenuItemShift";
            ToolStripMenuItem toolStripMenuItem06_0 = new("早番に変更する"); // 子アイテム１
            toolStripMenuItem06_0.Name = "ToolStripMenuItemShiftFirst";
            toolStripMenuItem06_0.Click += ToolStripMenuItem_Click;
            toolStripMenuItem06.DropDownItems.Add(toolStripMenuItem06_0);
            contextMenuStrip.Items.Add(toolStripMenuItem06);
            ToolStripMenuItem toolStripMenuItem06_1 = new("遅番に変更する"); // 子アイテム２
            toolStripMenuItem06_1.Name = "ToolStripMenuItemShiftLater";
            toolStripMenuItem06_1.Click += ToolStripMenuItem_Click;
            toolStripMenuItem06.DropDownItems.Add(toolStripMenuItem06_1);
            contextMenuStrip.Items.Add(toolStripMenuItem06);
            ToolStripMenuItem toolStripMenuItem06_2 = new("解除する"); // 子アイテム３
            toolStripMenuItem06_2.Name = "ToolStripMenuItemShiftNone";
            toolStripMenuItem06_2.Click += ToolStripMenuItem_Click;
            toolStripMenuItem06.DropDownItems.Add(toolStripMenuItem06_2);
            contextMenuStrip.Items.Add(toolStripMenuItem06);
            /*
             * 待機
             */
            ToolStripMenuItem toolStripMenuItem07 = new("待機"); // 親アイテム
            toolStripMenuItem07.Name = "ToolStripMenuItemStandBy";
            ToolStripMenuItem toolStripMenuItem07_0 = new("待機に変更する"); // 子アイテム１
            toolStripMenuItem07_0.Name = "ToolStripMenuItemStandByTrue";
            toolStripMenuItem07_0.Click += ToolStripMenuItem_Click;
            toolStripMenuItem07.DropDownItems.Add(toolStripMenuItem07_0);
            contextMenuStrip.Items.Add(toolStripMenuItem07);
            ToolStripMenuItem toolStripMenuItem07_1 = new("待機を解除する"); // 子アイテム２
            toolStripMenuItem07_1.Name = "ToolStripMenuItemStandByFalse";
            toolStripMenuItem07_1.Click += ToolStripMenuItem_Click;
            toolStripMenuItem07.DropDownItems.Add(toolStripMenuItem07_1);
            contextMenuStrip.Items.Add(toolStripMenuItem07);
            /*
             * 連絡事項
             */
            ToolStripMenuItem toolStripMenuItem08 = new("連絡事項"); // 親アイテム
            toolStripMenuItem08.Name = "ToolStripMenuItemContactInformation";
            ToolStripMenuItem toolStripMenuItem08_0 = new("連絡事項あり"); // 子アイテム１
            toolStripMenuItem08_0.Name = "ToolStripMenuItemContactInformationTrue";
            toolStripMenuItem08_0.Click += ToolStripMenuItem_Click;
            toolStripMenuItem08.DropDownItems.Add(toolStripMenuItem08_0);
            contextMenuStrip.Items.Add(toolStripMenuItem08);
            ToolStripMenuItem toolStripMenuItem08_1 = new("連絡事項なし"); // 子アイテム２
            toolStripMenuItem08_1.Name = "ToolStripMenuItemContactInformationFalse";
            toolStripMenuItem08_1.Click += ToolStripMenuItem_Click;
            toolStripMenuItem08.DropDownItems.Add(toolStripMenuItem08_1);
            contextMenuStrip.Items.Add(toolStripMenuItem08);
            /*
             * メモを作成・編集する
             */
            ToolStripMenuItem toolStripMenuItem09 = new("メモを作成・編集する");
            toolStripMenuItem09.Name = "ToolStripMenuItemSetMemo";
            toolStripMenuItem09.Click += ToolStripMenuItem_Click;
            contextMenuStrip.Items.Add(toolStripMenuItem09);
            /*
             * スペーサー
             */
            contextMenuStrip.Items.Add(new ToolStripSeparator());
            /*
             * 代車・代番Fax作成確認
             */
            ToolStripMenuItem toolStripMenuItem10 = new("代車・代番Fax作成確認"); // 親アイテム
            toolStripMenuItem10.Name = "ToolStripMenuItemFaxInformation";
            ToolStripMenuItem toolStripMenuItem10_0 = new("Fax送信をする"); // 子アイテム１
            toolStripMenuItem10_0.Name = "ToolStripMenuItemFaxInformationTrue";
            toolStripMenuItem10_0.Click += ToolStripMenuItem_Click;
            toolStripMenuItem10.DropDownItems.Add(toolStripMenuItem10_0);
            contextMenuStrip.Items.Add(toolStripMenuItem10);
            ToolStripMenuItem toolStripMenuItem10_1 = new("Fax送信をしない"); // 子アイテム２
            toolStripMenuItem10_1.Name = "ToolStripMenuItemFaxInformationFalse";
            toolStripMenuItem10_1.Click += ToolStripMenuItem_Click;
            toolStripMenuItem10.DropDownItems.Add(toolStripMenuItem10_1);
            contextMenuStrip.Items.Add(toolStripMenuItem10);
            /*
             * 代車・代番Faxを作成する
             */
            ToolStripMenuItem toolStripMenuItem11 = new("代車・代番Faxを作成する");
            toolStripMenuItem11.Name = "ToolStripMenuItemCreateFax";
            toolStripMenuItem11.Click += ToolStripMenuItem_Click;
            contextMenuStrip.Items.Add(toolStripMenuItem11);
            /*
             * スペーサー
             */
            contextMenuStrip.Items.Add(new ToolStripSeparator());
            /*
             * 削除する
             */
            ToolStripMenuItem toolStripMenuItem12 = new("削除する");
            toolStripMenuItem12.Name = "ToolStripMenuItemSetDelete";
            toolStripMenuItem12.Click += ToolStripMenuItem_Click;
            contextMenuStrip.Items.Add(toolStripMenuItem12);
            /*
             * プロパティ
             */
            ToolStripMenuItem toolStripMenuItem13 = new("プロパティ");
            toolStripMenuItem13.Name = "ToolStripMenuItemSetProperty";
            toolStripMenuItem13.Click += ToolStripMenuItem_Click;
            contextMenuStrip.Items.Add(toolStripMenuItem13);
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
            switch (this.ClassificationCode) {
                case 10:
                    pe.Graphics.DrawImage(ByteArrayToImage(Resources.SetLabelImageY), 0, 0, Width, Height);
                    break;
                case 11:
                    pe.Graphics.DrawImage(ByteArrayToImage(Resources.SetLabelImageK), 0, 0, Width, Height);
                    break;
                default:
                    pe.Graphics.DrawImage(ByteArrayToImage(Resources.SetLabelImage), 0, 0, Width, Height);
                    break;
            }
            // 稼働
            if (OperationFlag) {
                // 三郷車庫
                if (this.ManagedSpaceCode == 2)
                    pe.Graphics.DrawImage(ByteArrayToImage(Resources.Misato), 0, 0, Width, Height);
                // 電話連絡・FAX送信
                switch (this.SetMasterVo.ContactMethod) {
                    case 10:
                        pe.Graphics.DrawImage(ByteArrayToImage(Resources.Tel), 0, 0, Width, Height);
                        break;
                    case 11:
                        if (this.FaxTransmissionFlag) {
                            pe.Graphics.DrawImage(ByteArrayToImage(Resources.FaxRed), 0, 0, Width, Height);
                        } else {
                            pe.Graphics.DrawImage(ByteArrayToImage(Resources.Fax), 0, 0, Width, Height);
                        }
                        break;
                    case 13:
                        pe.Graphics.DrawImage(ByteArrayToImage(Resources.Tel), 0, 0, Width, Height);
                        if (this.FaxTransmissionFlag) {
                            pe.Graphics.DrawImage(ByteArrayToImage(Resources.FaxRed), 0, 0, Width, Height);
                        } else {
                            pe.Graphics.DrawImage(ByteArrayToImage(Resources.Fax), 0, 0, Width, Height);
                        }
                        break;
                }
                // メモ
                if (this.MemoFlag)
                    pe.Graphics.DrawImage(ByteArrayToImage(Resources.Memo), 0, 0, Width, Height);
                // 帰庫点呼
                if (this.LastRollCallFlag)
                    pe.Graphics.DrawImage(ByteArrayToImage(Resources.SetLabelImageTenko), 0, 0, Width, Height);
                // 連絡事項
                if (this.ContactInfomationFlag)
                    pe.Graphics.DrawImage(ByteArrayToImage(Resources.SetLabelImageContactInfomation), 0, 0, Width, Height);
                // 番手コード
                switch (this.ShiftCode) {
                    case 1:
                        pe.Graphics.DrawString("早番", new("Yu Gothic UI", 11, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.Red, new Point(7, 90));
                        break;
                    case 2:
                        pe.Graphics.DrawString("遅番", new("Yu Gothic UI", 11, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.Red, new Point(7, 90));
                        break;
                }
                // 待機フラグ
                if (this.StandByFlag)
                    pe.Graphics.DrawString("待機", new("Yu Gothic UI", 11, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.Red, new Point(37, 90));
                // カーソル関係
                if (CursorEnterFlag)
                    pe.Graphics.DrawImage(ByteArrayToImage(Resources.Filter), 0, 0, Width, Height);
            } else {
                // 休車
                pe.Graphics.DrawImage(ByteArrayToImage(Resources.Operation), 0, 0, Width, Height);
                // カーソル関係
                if (this.CursorEnterFlag)
                    pe.Graphics.DrawImage(ByteArrayToImage(Resources.Filter), 0, 0, Width, Height);
            }
            /*
             * 文字(配車先)を描画
             */
            Font fontSetLabel = new("Yu Gothic UI", 13, FontStyle.Regular, GraphicsUnit.Pixel);
            Rectangle rectangle = new(0, 0, Width, Height);
            StringFormat stringFormat = new();
            stringFormat.LineAlignment = StringAlignment.Center;
            stringFormat.Alignment = StringAlignment.Center;
            pe.Graphics.DrawString(string.Concat(SetMasterVo.SetName1, "\r\n", SetMasterVo.SetName2, "\r\n", AddWorkerFlag ? "(作付)" : "  "), fontSetLabel, new SolidBrush(Color.Black), rectangle, stringFormat);
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
            Debug.WriteLine("SetLabel ContextMenuStripOpened");

            SetLabel_ContextMenuStrip_Opened.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object? sender, EventArgs e) {
            Debug.WriteLine("SetLabel ToolStripMenuItemClick");

            SetLabel_ToolStripMenuItem_Click.Invoke(sender, e);
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
                    SetLabel_OnMouseClick.Invoke(this, e);
                } else if ((ModifierKeys & Keys.Control) == Keys.Control) {
                    if (this.MemoFlag) {
                        _toolTip.Show(this.Memo, this, 4, 4);
                        return;
                    }
                }
                // Down
                SetLabel_OnMouseDown.Invoke(this, e);
            } else {
                // DoubleClick
                SetLabel_OnMouseDoubleClick.Invoke(this, e);
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
            SetLabel_OnMouseEnter.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e) {
            // ToolTipを消す
            _toolTip.Hide(this);
            CursorEnterFlag = false;
            Refresh();
            SetLabel_OnMouseLeave.Invoke(this, e);
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
        /// 
        /// </summary>
        public SetMasterVo SetMasterVo {
            get => this._setMasterVo;
            set => this._setMasterVo = value;
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
        /// 稼働フラグ
        /// true:稼働 false:休車
        /// </summary>
        public bool OperationFlag {
            get => this._operationFlag;
            set {
                this._operationFlag = value;
                this.Refresh();
                // SetControlのプロパティをセット
                ((SetControl)this.ParentControl).OperationFlag = this.OperationFlag;
            }
        }
        /// <summary>
        /// 0:該当なし 1:足立 2:三郷
        /// </summary>
        public int ManagedSpaceCode {
            get => this._managedSpaceCode;
            set {
                this._managedSpaceCode = value;
                this.Refresh();
            }
        }
        /// <summary>
        /// 10:雇上 11:区契 12:臨時 20:清掃工場 30:社内 50:一般 51:社用車 99:指定なし
        /// </summary>
        public int ClassificationCode {
            get => this._classificationCode;
            set {
                this._classificationCode = value;
                this.Refresh();
            }
        }
        /// <summary>
        /// true:帰庫点呼記録済 false:未点呼
        /// </summary>
        public bool LastRollCallFlag {
            get => this._lastRollCallFlag;
            set {
                this._lastRollCallFlag = value;
                Refresh();
            }
        }
        /// <summary>
        /// 帰庫点呼日時
        /// </summary>
        public DateTime LastRollCallYmdHms {
            get => this._lastRollCallYmdHms;
            set => this._lastRollCallYmdHms = value;
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
        /// 0:指定なし 1:早番 2:遅番
        /// </summary>
        public int ShiftCode {
            get => this._shiftCode;
            set {
                this._shiftCode = value;
                this.Refresh();
            }
        }
        /// <summary>
        /// true:待機 false:通常
        /// </summary>
        public bool StandByFlag {
            get => this._standByFlag;
            set {
                this._standByFlag = value;
                this.Refresh();
            }
        }
        /// <summary>
        /// true:作業員付き false:作業員なし
        /// </summary>
        public bool AddWorkerFlag {
            get => this._addWorkerFlag;
            set {
                this._addWorkerFlag = value;
                this.Refresh();
            }
        }
        /// <summary>
        /// true:連絡事項あり false:連絡事項なし
        /// </summary>
        public bool ContactInfomationFlag {
            get => this._contactInfomationFlag;
            set {
                this._contactInfomationFlag = value;
                this.Refresh();
            }
        }
        /// <summary>
        /// 代車・代番連絡
        /// true:電話する false:電話しない
        /// </summary>
        public bool TelCallingFlag {
            get => this._telCallingFlag;
            set {
                this._telCallingFlag = value;
                this.Refresh();
            }
        }
        /// <summary>
        /// 代車・代番連絡
        /// true:Fax送信 false:なし
        /// </summary>
        public bool FaxTransmissionFlag {
            get => this._faxTransmissionFlag;
            set {
                this._faxTransmissionFlag = value;
                this.Refresh();
            }
        }







    }
}
