/*
 * 2024-10-09
 */
using System.Windows.Forms;

using Vo;

namespace ControlEx {
    public partial class Board : TableLayoutPanel {
        /*
         * Cellのサイズ
         */
        private const float _cellWidth = 74;
        private const float _cellHeight = 120;
        /*
         * Cellの数
         */
        private const int _columnNumber = 50; // Column数
        private const int _rowNumber = 4; // SetControlを配置出来るRow数(0,2,4,6)
        private const int _rowAllNumber = 8; // ダミーを含めたRow数(0,2,4,6はダミー)
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
                    case 0 or 2 or 4 or 6: // DetailCell
                        this.RowStyles.Add(new RowStyle(SizeType.Absolute, _rowDummyHeight));
                        break;
                    case 1 or 3 or 5 or 7: // SetControlCell
                        this.RowStyles.Add(new RowStyle(SizeType.Absolute, _rowHeight));
                        break;
                }
            }
        }

        /// <summary>
        /// OnPaint
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellPaint(TableLayoutCellPaintEventArgs e) {

        }

        /// <summary>
        /// SetControl追加(１個分のSetControlを処理)
        /// </summary>
        /// <param name="cellNumber"></param>
        /// <param name="setMasterVo"></param>
        /// <param name="carMasterVo"></param>
        /// <param name="listStaffMasterVo"></param>
        public void AddSetControl(int cellNumber, VehicleDispatchDetailVo vehicleDispatchDetailVo, SetMasterVo setMasterVo, CarMasterVo carMasterVo, List<StaffMasterVo> listStaffMasterVo) {
            SetControl setControl = new(vehicleDispatchDetailVo);
            setControl.AddSetLabel(setMasterVo);
            setControl.AddCarLabel(carMasterVo);
            setControl.AddStaffLabels(listStaffMasterVo);
            /*
             * Event
             */
            setControl.Event_SetControl_OnMouseDown += Board_MouseDown;
            setControl.Event_SetControl_OnMouseEnter += Board_MouseEnter;
            setControl.Event_SetControl_OnMouseLeave += Board_MouseLeave;
            setControl.Event_SetControl_OnMouseMove += Board_MouseMove;
            setControl.Event_SetControl_OnMouseUp += Board_MouseUp;

            this.Controls.Add(setControl, GetCellPoint(cellNumber).X, GetCellPoint(cellNumber).Y);
            this.SetColumnSpan(setControl, vehicleDispatchDetailVo.PurposeFlag ? 2 : 1);
        }

        /// <summary>
        /// 
        /// </summary>
        public void RemoveControls() {
            /*
             * メソッドをClear呼び出してもコントロール ハンドルはメモリから削除されません。 メモリリークを回避するにはメソッドをDispose明示的に呼び出す必要があります。
             * ※後ろから解放している点が重要らしい。
             */
            for (int i = this.Controls.Count - 1; 0 <= i; i--)
                this.Controls[i].Dispose();
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Board_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                this._oldMousePoint = ((Control)sender).PointToScreen(new Point(e.X, e.Y));
                this.Cursor = Cursors.Hand;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Board_MouseEnter(object sender, EventArgs e) {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Board_MouseLeave(object sender, EventArgs e) {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Board_MouseMove(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                Point _newMousePoint = ((Control)sender).PointToScreen(new Point(e.X, e.Y));
                int x = this._oldAutoScrollPosition.X + (_newMousePoint.X - this._oldMousePoint.X);
                int y = this._oldAutoScrollPosition.Y + (_newMousePoint.Y - this._oldMousePoint.Y);
                this.AutoScrollPosition = new Point(-x, -y);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Board_MouseUp(object sender, MouseEventArgs e) {
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
    }
}
