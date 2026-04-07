/*
 * 2024-10-09
 */
using Vo;

namespace CcControl {
    public partial class Board : TableLayoutPanel {
        /*
         * デリゲート
         */
        public event EventHandler Board_ContextMenuStrip_Opened = delegate { };
        public event EventHandler Board_ToolStripMenuItem_Click = delegate { };
        public event DragEventHandler Board_OnDragDrop = delegate { };
        public event DragEventHandler Board_OnDragEnter = delegate { };
        public event DragEventHandler Board_OnDragOver = delegate { };
        public event MouseEventHandler Board_OnMouseClick = delegate { };
        public event MouseEventHandler Board_OnMouseDoubleClick = delegate { };
        public event MouseEventHandler Board_OnMouseDown = delegate { };
        public event EventHandler Board_OnMouseEnter = delegate { };
        public event EventHandler Board_OnMouseLeave = delegate { };
        public event MouseEventHandler Board_OnMouseMove = delegate { };
        public event MouseEventHandler Board_OnMouseUp = delegate { };
        /*
         * Cellのサイズ
         */
        private const float _cellWidth = 74;
        private const float _cellHeight = 120;
        /*
         * Cellの数
         */
        private const int _columnNumber = 50;                       // Column数
        private const int _rowNumber = 4;                           // SetControlを配置出来るRow数(1,3,5,7)
        private const int _rowAllNumber = 8;                        // ダミーを含めたRow数(0,2,4,6はダミー)
        /*
         * UpdateMarkのサイズ
         */
        private const float _updateMarkWidth = 74;
        private const float _updateMarkHeight = 18;
        /*
         * Cellのサイズ
         */
        private const float _columnWidth = _cellWidth;
        private const float _rowHeight = _cellHeight * _rowNumber;
        private const float _rowDummyHeight = 18;
        /*
         * 変数定義
         */
        private Point _oldMousePoint;
        private Point _oldAutoScrollPosition;

        /// <summary>
        /// Constructor
        /// </summary>
        public Board() {
            /*
             * InitializeControl
             */
            InitializeComponent();
            this.AllowDrop = true;
            this.AutoScroll = true;
            this.Dock = DockStyle.Fill;
            this.Margin = new Padding(16, 8, 16, 8);
            this.Name = "Board";
            this.Padding = new Padding(0);
            /*
             * Column追加
             */
            this.ColumnCount = _columnNumber;
            for (int i = 0; i < _columnNumber; i++)
                this.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, _columnWidth));
            /*
             * Row追加
             */
            this.RowCount = _rowAllNumber;
            for (int i = 0; i < _rowAllNumber; i++) {
                switch (i) {
                    case 0 or 2 or 4 or 6: // 空のCell
                        this.RowStyles.Add(new RowStyle(SizeType.Absolute, _rowDummyHeight));
                        break;
                    case 1 or 3 or 5 or 7: // SetControlが入るCell
                        this.RowStyles.Add(new RowStyle(SizeType.Absolute, _rowHeight));
                        break;
                }
            }
        }

        /// <summary>
        /// SetControl追加(１個分のSetControlを処理)
        /// </summary>
        /// <param name="cellNumber"></param>
        /// <param name="vehicleDispatchDetailVo"></param>
        /// <param name="setMasterVo"></param>
        /// <param name="carMasterVo"></param>
        /// <param name="listStaffMasterVo"></param>
        /// <param name="listStaffProperVo">初任・適齢の判断をするために使用する</param>
        public void AddOneSetControl(int cellNumber, VehicleDispatchDetailVo vehicleDispatchDetailVo, SetMasterVo setMasterVo, CarMasterVo carMasterVo, List<StaffMasterVo> listStaffMasterVo, List<StaffProperVo> listStaffProperVo) {
            SetControl setControl = new(vehicleDispatchDetailVo);
            setControl.CellNumber = cellNumber;
            setControl.NumberOfPeople = setMasterVo is not null ? setMasterVo.NumberOfPeople : 0;
            setControl.AddSetLabel(setMasterVo);
            setControl.AddCarLabel(carMasterVo);
            setControl.AddStaffLabels(listStaffMasterVo, listStaffProperVo);
            /*
             * Event
             */
            setControl.SetControl_ContextMenuStrip_Opened += this.ContextMenuStripEx_Opened;
            setControl.SetControl_ToolStripMenuItem_Click += this.ToolStripMenuItem_Click;
            setControl.SetControl_OnDragDrop += this.OnDragDrop;
            setControl.SetControl_OnDragEnter += this.OnDragEnter;
            setControl.SetControl_OnDragOver += this.OnDragOver;
            setControl.SetControl_OnMouseClick += this.OnMouseClick;
            setControl.SetControl_OnMouseDoubleClick += this.OnMouseDoubleClick;
            setControl.SetControl_OnMouseDown += this.OnMouseDown;
            setControl.SetControl_OnMouseEnter += this.OnMouseEnter;
            setControl.SetControl_OnMouseLeave += this.OnMouseLeave;
            setControl.SetControl_OnMouseMove += this.OnMouseMove;
            setControl.SetControl_OnMouseUp += this.OnMouseUp;

            this.Controls.Add(setControl, GetCellPoint(cellNumber).X, GetCellPoint(cellNumber).Y);
            this.SetColumnSpan(setControl, vehicleDispatchDetailVo.PurposeFlag ? 2 : 1);
        }

        /*
         * WM_SETREDRAW で描画そのものを止める
         * 配置されている全てのControlを解放する
         * メソッドをClear呼び出してもコントロール ハンドルはメモリから削除されません。 メモリリークを回避するにはメソッドをDispose明示的に呼び出す必要があります。
         * ※後ろから解放している点が重要らしい。
         */
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, bool wParam, int lParam);

        private const int WM_SETREDRAW = 0x000B;

        public void RemoveAllControls() {
            // 描画停止
            SendMessage(this.Handle, WM_SETREDRAW, false, 0);

            try {
                for (int i = this.Controls.Count - 1; 0 <= i; i--)
                    this.Controls[i].Dispose();
            } finally {
                // 描画再開
                SendMessage(this.Handle, WM_SETREDRAW, true, 0);
                this.Invalidate();
                this.Update();
            }
        }

        /// <summary>
        /// cellNumberをPointへ変換
        /// </summary>
        /// <param name="cellNumber"></param>
        /// <returns></returns>
        private Point GetCellPoint(int cellNumber) {
            return new Point(cellNumber % _columnNumber, cellNumber / _columnNumber * 2 + 1);
        }

        /// <summary>
        /// Boardに配置されているSetLabelを走査する
        /// </summary>
        /// <returns></returns>
        public List<SetMasterVo> GetAllSetLabel() {
            List<SetMasterVo> listSetMasterVo = new();
            foreach (Control control in this.Controls) {
                if (control is SetControl setControl) {
                    if (setControl.DeployedSetLabel is SetLabel setLabel)
                        listSetMasterVo.Add(setLabel.SetMasterVo);
                }
            }
            return listSetMasterVo;
        }

        /// <summary>
        /// Boardに配置されているCarLabelを走査する
        /// </summary>
        /// <returns></returns>
        public List<CarMasterVo> GetAllCarLabel() {
            List<CarMasterVo> listCarMasterVo = new();
            foreach (Control control in this.Controls) {
                if (control is SetControl setControl) {
                    if (setControl.DeployedCarLabel is CarLabel carLabel)
                        listCarMasterVo.Add(carLabel.CarMasterVo);
                }
            }
            return listCarMasterVo;

        }

        /// <summary>
        /// Boardに配置されているStaffLabelを走査する
        /// </summary>
        /// <returns></returns>
        public List<StaffMasterVo> GetAllStaffLabel() {
            List<StaffMasterVo> listStaffMasterVo = new();
            foreach (Control control in this.Controls) {
                if (control is SetControl setControl) {
                    // DeployedStaffLabel1〜4 をまとめて処理
                    Control[] controls = new[] { setControl.DeployedStaffLabel1, setControl.DeployedStaffLabel2, setControl.DeployedStaffLabel3, setControl.DeployedStaffLabel4 };
                    foreach (Control label in controls) {
                        if (label is StaffLabel staffLabel)
                            listStaffMasterVo.Add(staffLabel.StaffMasterVo);
                    }
                }
            }
            return listStaffMasterVo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<VehicleDispatchDetailVo> GetListVehicleDispatchDetailVo() {
            List<VehicleDispatchDetailVo> listVehicleDispatchDetailVo = new();
            foreach (SetControl setControl in this.Controls)
                listVehicleDispatchDetailVo.Add(setControl.GetVehicleDispatchDetailVo());
            return listVehicleDispatchDetailVo;
        }

        /*
         * Event処理
         */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStripEx_Opened(object sender, EventArgs e) {
            //
            Board_ContextMenuStrip_Opened.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            //
            Board_ToolStripMenuItem_Click.Invoke(sender, e);
        }

        /// <summary>
        /// SetControlから
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDragDrop(object sender, DragEventArgs e) {
            //
            Board_OnDragDrop.Invoke(sender, e);
        }

        /// <summary>
        /// SetControlのみのEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDragEnter(object sender, DragEventArgs e) {
            //
            Board_OnDragEnter.Invoke(sender, e);
        }

        /// <summary>
        /// SetControlから
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDragOver(object sender, DragEventArgs e) {
            //
            Board_OnDragOver.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseClick(object sender, MouseEventArgs e) {
            //
            Board_OnMouseClick.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDoubleClick(object sender, MouseEventArgs e) {
            //
            Board_OnMouseDoubleClick.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDown(object sender, MouseEventArgs e) {
            /*
             * SetControl
             * SetLabel
             * CarLabel
             * StaffLabel
             */
            switch (sender) {
                // 画面スクロールの準備
                case SetControl:
                    if (e.Button == MouseButtons.Left) {
                        this._oldMousePoint = ((Control)sender).PointToScreen(new Point(e.X, e.Y));
                        this.Cursor = Cursors.Hand;
                    }
                    break;
                // Dragの準備
                case SetLabel:
                case CarLabel:
                case StaffLabel:
                    Board_OnMouseDown.Invoke(sender, e);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseEnter(object sender, EventArgs e) {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseLeave(object sender, EventArgs e) {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseMove(object sender, MouseEventArgs e) {
            /*
             * SetControl
             * SetLabel
             * CarLabel
             * StaffLabel
             */
            switch (sender) {
                case SetControl:
                    if (e.Button == MouseButtons.Left) {
                        Point _newMousePoint = ((Control)sender).PointToScreen(new Point(e.X, e.Y));
                        int x = this._oldAutoScrollPosition.X + (_newMousePoint.X - this._oldMousePoint.X);
                        int y = this._oldAutoScrollPosition.Y + (_newMousePoint.Y - this._oldMousePoint.Y);
                        this.AutoScrollPosition = new Point(-x, -y);
                    }
                    break;
                case SetLabel:
                case CarLabel:
                case StaffLabel:
                    Board_OnMouseDown.Invoke(sender, e);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseUp(object sender, MouseEventArgs e) {
            this._oldAutoScrollPosition = this.AutoScrollPosition;
            this.Cursor = Cursors.Default;
        }

        /*
         * プロパティー
         */
        /// <summary>
        /// Column数
        /// </summary>
        public int ColumnNumber => _columnNumber;
        /// <summary>
        /// Row数
        /// </summary>
        public int RowNumber => _rowNumber;
        /// <summary>
        /// ダミーを合わせた全Row数
        /// </summary>
        public int RowAllNumber => _rowAllNumber;
        /// <summary>
        /// Board Cell Width
        /// </summary>
        public float CellWidth => _cellWidth;
        /// <summary>
        /// Board Cell Height
        /// </summary>
        public float CellHeight => _cellHeight;

        /// <summary>
        /// 指定セルの上（ダミー行）に更新マークを表示する
        /// 既に同じセルに更新マークがある場合は置き換える
        /// </summary>
        public void SetUpdateMark(CcToolTip ccToolTip, int cellNumber, string updatePcName, DateTime updateYmdHms) {
            Point point = GetCellPoint(cellNumber); // SetControlが配置されているCellのPointを取得
            int col = point.X;
            int markRow = point.Y - 1; // 更新マークはSetControlの上のダミー行に表示

            if (markRow < 0 || col < 0 || col >= this.ColumnCount || markRow >= this.RowCount)
                return;

            // ★ 既存の更新マークを削除（Timer も停止）
            Control control = this.Controls.Cast<Control>().FirstOrDefault(c => {
                var pos = this.GetPositionFromControl(c);
                return pos.Column == col && pos.Row == markRow &&
                       c.Tag is UpdateMarkTag;
            });

            if (control is not null) {
                if (control.Tag is UpdateMarkTag tag) {
                    tag.Timer.Stop();
                    tag.Timer.Dispose();
                }
                control.Dispose();
            }

            // ★ マーク用パネルを作成
            Panel panel = new() {
                Width = (int)_updateMarkWidth,
                Height = (int)_updateMarkHeight,
                BackColor = Color.OrangeRed,
                Margin = new Padding(2),
                Anchor = AnchorStyles.Top
            };

            // ★ ラベル
            CcLabel ccLabel = new() {
                Text = "更新",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                ForeColor = Color.White,
                Font = new Font(this.Font.FontFamily, 9, FontStyle.Bold),
                BackColor = Color.Transparent
            };
            panel.Controls.Add(ccLabel);

            // ★ ToolTip
            ccToolTip.SetToolTip(ccLabel, $"更新PC名：{updatePcName}\r\n更新時刻：{updateYmdHms:yyyy/MM/dd HH:mm:ss}");

            // ★ フェード点滅用 Timer
            var fadeTimer = new System.Windows.Forms.Timer {
                Interval = 20 // 20msごと → なめらか
            };

            int alpha = 255;
            int delta = -10; // 透明度の増減

            fadeTimer.Tick += (_, _) => {
                alpha += delta;

                // 透明度の上下限で反転
                if (alpha <= 50 || alpha >= 255)
                    delta = -delta;

                panel.BackColor = Color.FromArgb(alpha, Color.OrangeRed);
            };

            // ★ Panel が破棄されたら Timer も止める
            panel.Disposed += (_, _) => {
                fadeTimer.Stop();
                fadeTimer.Dispose();
            };

            fadeTimer.Start();

            // ★ Panel に Timer を紐づける（Tag を専用クラスに）
            panel.Tag = new UpdateMarkTag(fadeTimer);

            // ★ TableLayoutPanel に追加
            this.Controls.Add(panel, col, markRow);
        }

        /// <summary>
        /// UpdateMark の Tag 用クラス（Timer を保持）
        /// </summary>
        private sealed class UpdateMarkTag {
            public System.Windows.Forms.Timer Timer { get; }

            public UpdateMarkTag(System.Windows.Forms.Timer timer) {
                Timer = timer;
            }
        }
    }
}
