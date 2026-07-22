/*
 * 2026-06-08 (refactored)
 */
using Common;

using Dao;

using FarPoint.Win.Spread;
using FarPoint.Win.Spread.Model;

using Vo;

namespace PaidLeave {
    public partial class PaidLeaveList : Form {
        /*
         * Columns
         */
        private enum Col {
            /// <summary>
            /// 組合番号
            /// </summary>
            UnionCode = 0,
            /// <summary>
            /// 氏名
            /// </summary>
            Name = 1,
            /// <summary>
            /// 基準日・起算日
            /// </summary>
            ReferenceDate = 2,
            /// <summary>
            /// 勤務年数
            /// </summary>
            YearsOfService = 3,
            /// <summary>
            /// 起算日
            /// </summary>
            StartDate = 4,
            /// <summary>
            /// 勤務日数
            /// </summary>
            WorkDays = 5,
            /// <summary>
            /// 付与日数
            /// </summary>
            GrantedDays = 6,
            /// <summary>
            /// 1回目のスタートColumn番号
            /// </summary>
            TimeOffStart = 7
        }
        private readonly DateUtility _dateUtility;
        private readonly Dictionary<int, DateTime> _dictionaryStartDate = [];
        /*
         * プロパティ
         */
        private DateTime _recentCommencementDate;
        /*
         * Dao
         */
        private StaffMasterDao _staffMasterDao;
        private PaidLeaveEntitlementDao _paidLeaveEntitlementDao;
        private TimeOffMasterDao _timeOffMasterDao;
        private VehicleDispatchDetailDao _vehicleDispatchDetailDao;
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;
        private List<StaffMasterVo> _listStaffMasterVo;
        private List<PaidLeaveEntitlementV0> _listPaidLeaveEntitlementV0;

        /// <summary>
        /// １人分の使用行数
        /// </summary>
        private const int _blockRowCount = 3;
        private int _spreadListTopRow = 0;
        private StaffMasterVo _pushStaffMasterVo = null;
        private CellRange _cellRange = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public PaidLeaveList(ConnectionVo connectionVo, Screen screen) {
            _dateUtility = new();
            /*
             * Dao
             */
            StaffMasterDao = new StaffMasterDao(connectionVo);
            PaidLeaveEntitlementDao = new PaidLeaveEntitlementDao(connectionVo);
            TimeOffMasterDao = new TimeOffMasterDao(connectionVo);
            VehicleDispatchDetailDao = new VehicleDispatchDetailDao(connectionVo);
            /*
             * Vo
             */
            ConnectionVo = connectionVo;
            /*
             * InitializeControl
             */
            InitializeComponent();
            /*
             * MenuStrip
             */
            List<string> listString = ["ToolStripMenuItemFile",
                                       "ToolStripMenuItemExit",
                                       "ToolStripMenuItemHelp"];
            this.CcMenuStrip1.ChangeEnable(listString);

            this.InitializeSheetView(this.SheetViewList);
            /*
             * Eventを登録する
             */
            this.CcMenuStrip1.Event_MenuStripEx_ToolStripMenuItem_Click += this.ToolStripMenuItem_Click;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CcButtonUpdate_Click(object sender, EventArgs e) {
            ListStaffMasterVo = StaffMasterDao.SelectAllStaffMaster(this.GroupBoxExBelongs.CreateArray(this.GroupBoxExBelongs),
                                                                    this.GroupBoxExJobForm.CreateArray(this.GroupBoxExJobForm),
                                                                    this.GroupBoxExOccupation.CreateArray(this.GroupBoxExOccupation),
                                                                    false);
            ListPaidLeaveEntitlementV0 = PaidLeaveEntitlementDao.SelectAllPaidLeaveEntitlementV0();
            this.SetSheetViewList(SheetViewList, ListStaffMasterVo, ListPaidLeaveEntitlementV0);
        }

        /// <summary>
        /// Spread全体の描画
        /// </summary>
        private void SetSheetViewList(SheetView sheetView, List<StaffMasterVo> listStaffMasterVo, List<PaidLeaveEntitlementV0> listPaidLeaveEntitlementV0) {
            // ヘッダを非表示
            sheetView.RowHeader.Visible = false;

            int rowCount = 0;
            this.SpreadList.SuspendLayout();
            _spreadListTopRow = this.SpreadList.GetViewportTopRow(0);

            if(sheetView.Rows.Count > 0) {
                sheetView.RemoveRows(0, sheetView.Rows.Count);
            }

            foreach(StaffMasterVo staffMasterVo in listStaffMasterVo.Where(x => x.PaidLeaveFlag).OrderBy(x => x.UnionCode)) {
                int baseRow = rowCount * _blockRowCount;
                this.SetOneBlock(sheetView, baseRow, staffMasterVo, listPaidLeaveEntitlementV0);
                rowCount++;
            }

            this.SpreadList.SetViewportTopRow(0, _spreadListTopRow);
            this.SpreadList.ResumeLayout();
            this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = string.Concat(" ", rowCount, " 件を処理しました");
        }

        /// <summary>
        /// 1従事者分(3行)の描画
        /// </summary>
        /// <param name="sheetView">対象のSheetView</param>
        /// <param name="baseRow"></param>
        /// <param name="staffMasterVo"></param>
        /// <param name="listPaidLeaveEntitlementV0"></param>
        private void SetOneBlock(SheetView sheetView, int baseRow, StaffMasterVo staffMasterVo, List<PaidLeaveEntitlementV0> listPaidLeaveEntitlementV0) {
            sheetView.Rows.Add(baseRow, _blockRowCount);
            sheetView.Rows[baseRow].Height = 24;
            sheetView.HorizontalGridLine = new GridLine(GridLineType.Flat);

            this.SetBlockHeader(sheetView, baseRow, staffMasterVo);
            /*
             * 直近起算日と過去2年分をDictionaryに格納
             */
            RecentCommencementDate = _dateUtility.GetPaidLeaveCommencementDate(staffMasterVo.PaidLeaveCommencementDate);
            _dictionaryStartDate.Clear();
            _dictionaryStartDate.Add(0, RecentCommencementDate.AddYears(0).Date);   // 直近の起算日
            _dictionaryStartDate.Add(1, RecentCommencementDate.AddYears(-1).Date);  // １年前の起算日
            _dictionaryStartDate.Add(2, RecentCommencementDate.AddYears(-2).Date);  // ２年前の起算日

            foreach(PaidLeaveEntitlementV0 paidLeaveEntitlementV0 in listPaidLeaveEntitlementV0.Where(x => x.StaffCode == staffMasterVo.StaffCode && x.StartDate >= _dictionaryStartDate[2]).OrderBy(x => x.StartDate).Take(3).ToList())
                SetPaidLeaveEntitlement(sheetView, baseRow, staffMasterVo, paidLeaveEntitlementV0);
        }

        /// <summary>
        /// ヘッダ部(氏名・コード・基準日・起算日)の描画
        /// Cellを作成
        /// </summary>
        /// <param name="sheetView"></param>
        /// <param name="baseRow"></param>
        /// <param name="staffMasterVo"></param>
        private void SetBlockHeader(SheetView sheetView, int baseRow, StaffMasterVo staffMasterVo) {
            /*
             * 組合コード・従事者コード
             */
            sheetView.AddSpanCell(baseRow + 0, (int)Col.UnionCode, 2, 1);
            sheetView.Cells[baseRow + 0, (int)Col.UnionCode].Text = staffMasterVo.UnionCode.ToString();
            sheetView.Cells[baseRow + 2, (int)Col.UnionCode].Text = staffMasterVo.StaffCode.ToString();
            sheetView.Cells[baseRow + 2, (int)Col.UnionCode].Tag = staffMasterVo;
            /*
             * 氏名
             */
            sheetView.AddSpanCell(baseRow + 0, (int)Col.Name, 3, 1);
            // 2026-06-08 (refactored) 起算月が今月の場合は背景色を変更する
            if(staffMasterVo.PaidLeaveCommencementDate.Month == DateTime.Now.Month) {
                sheetView.Cells[baseRow + 0, (int)Col.Name].BackColor = Color.Yellow;
            } else {
                sheetView.Cells[baseRow + 0, (int)Col.Name].BackColor = Color.White;
            }
            sheetView.Cells[baseRow + 0, (int)Col.Name].Text = staffMasterVo.Name;
            /*
             * 基準日・起算日
             */
            sheetView.AddSpanCell(baseRow + 0, (int)Col.ReferenceDate, 2, 1);
            sheetView.Cells[baseRow + 0, (int)Col.ReferenceDate].HorizontalAlignment = CellHorizontalAlignment.Center;
            sheetView.Cells[baseRow + 0, (int)Col.ReferenceDate].Text = staffMasterVo.PaidLeaveReferenceDate.ToString("yyyy/MM/dd");

            sheetView.Cells[baseRow + 2, (int)Col.ReferenceDate].ForeColor = Color.Red;
            sheetView.Cells[baseRow + 2, (int)Col.ReferenceDate].HorizontalAlignment = CellHorizontalAlignment.Center;
            sheetView.Cells[baseRow + 2, (int)Col.ReferenceDate].Text = staffMasterVo.PaidLeaveCommencementDate.ToString("yyyy/MM/dd");
        }

        /// <summary>
        /// 3行分のうち該当する行に付与情報を書き込む
        /// </summary>
        /// <param name="sheetView">対象のSheetView</param>
        /// <param name="baseRow"></param>
        /// <param name="staffMasterVo"></param>
        /// <param name="paidLeaveEntitlementV0"></param>
        private void SetPaidLeaveEntitlement(SheetView sheetView, int baseRow, StaffMasterVo staffMasterVo, PaidLeaveEntitlementV0 paidLeaveEntitlementV0) {
            for(int index = 0; index <= 2; index++) {
                if(_dictionaryStartDate[index].Date == paidLeaveEntitlementV0.StartDate.Date) {
                    int targetRow = baseRow + (2 - index); // 2年前→0行目, 1年前→1行目, 0年前→2行目
                    DateTime startDate = _dictionaryStartDate[index].Date;

                    int workDays;
                    if(CcCheckBoxWorkDaysFlag.Checked) {
                        workDays = VehicleDispatchDetailDao.GetCountVehicleDispatchDetail(startDate.AddYears(-1), startDate.AddDays(-1), staffMasterVo.StaffCode);
                    } else {
                        workDays = 0;
                    }

                    sheetView.Cells[targetRow, (int)Col.YearsOfService].Text = string.Concat(paidLeaveEntitlementV0.YearsOfService, "年6か月");
                    sheetView.Cells[targetRow, (int)Col.StartDate].HorizontalAlignment = CellHorizontalAlignment.Center;
                    sheetView.Cells[targetRow, (int)Col.StartDate].Text = paidLeaveEntitlementV0.StartDate.ToString("yyyy/MM/dd");
                    sheetView.Cells[targetRow, (int)Col.WorkDays].Text = workDays.ToString();
                    sheetView.Cells[targetRow, (int)Col.GrantedDays].Text = paidLeaveEntitlementV0.GrantedDays.ToString();

                    if(paidLeaveEntitlementV0.GrantedDays > 0) {
                        int startCol = (int)Col.TimeOffStart;
                        int endCol   = (int)Col.TimeOffStart + paidLeaveEntitlementV0.GrantedDays - 1;
                        sheetView.Cells[targetRow, startCol, targetRow, endCol].BackColor = Color.LightYellow;

                        // 2年前の起算日が直近起算日から2年前の場合は破棄表示
                        if(index == 2 && paidLeaveEntitlementV0.StartDate.Date == this._dictionaryStartDate[2].Date) {
                            sheetView.Cells[targetRow, (int)Col.YearsOfService, targetRow, endCol].ForeColor = Color.White;
                            sheetView.Cells[targetRow, (int)Col.YearsOfService, targetRow, endCol].BackColor = Color.DarkGray;
                        }
                    }
                    this.SetTimeOffMaster(targetRow, staffMasterVo, index);
                }
            }
        }

        /// <summary>
        /// TimeOffMasterを書き出し
        /// </summary>
        /// <param name="rowNumber"></param>
        /// <param name="staffMasterVo"></param>
        /// <param name="number"></param>
        private void SetTimeOffMaster(int rowNumber, StaffMasterVo staffMasterVo, int number) {
            int colOffset = 0;
            foreach(TimeOffMasterVo timeOffMasterVo in TimeOffMasterDao.SelectAllTimeOffMaster(staffMasterVo.StaffCode)
                                                                       .Where(x => x.BaseDate.Date == _dictionaryStartDate[number].Date && x.Code == 1) // 1:有給休暇
                                                                       .OrderBy(x => x.Date).ToList()) {
                int colIndex = (int)Col.TimeOffStart + colOffset;
                /*
                 * 直近起算日以降の休暇日はForeColor = Color.Red
                 */
                if(_dictionaryStartDate[0].Date <= timeOffMasterVo.Date.Date)
                    this.SheetViewList.Cells[rowNumber, colIndex].ForeColor = Color.Red;

                this.SheetViewList.Cells[rowNumber, colIndex].Text = timeOffMasterVo.Date.ToString("yyyy/MM/dd");
                colOffset++;
            }
        }

        /// <summary>
        /// CcContextMenuStripがOpenされたStaffMasterVoを取得する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CcContextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
            // ★ 右クリック位置のセルを取得
            Point point = this.SpreadList.PointToClient(Cursor.Position);
            CellRange cellRange = this.SpreadList.GetRootWorkbook().GetCellFromPixel(point.X, point.Y);

            // ★ ヘッダ判定（Row < 0 または Column < 0）
            if(cellRange.Row < 0 || cellRange.Column < 0) {
                e.Cancel = true;   // ContextMenuStrip を開かせない
                return;
            }

            // ★ 通常セルの場合のみ処理
            CellRange[] ranges = this.SheetViewList.GetSelections();
            foreach(CellRange range in ranges) {
                _pushStaffMasterVo = this.SheetViewList.Cells[range.Row + 2, (int)Col.UnionCode].Tag as StaffMasterVo;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpreadList_MouseMove(object sender, MouseEventArgs e) {
            _cellRange = this.SpreadList.GetRootWorkbook().GetCellFromPixel(e.X, e.Y);
            this.SheetViewList.ClearSelection();
            int startRow = _cellRange.Row / _blockRowCount * _blockRowCount;
            this.SheetViewList.AddSelection(startRow, 0, _blockRowCount, this.SheetViewList.ColumnCount);
        }

        /// <summary>
        /// InitializeSheetView
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns></returns>
        private void InitializeSheetView(SheetView sheetView) {
            this.SpreadList.AllowDragDrop = false;                                                      // ドラッグアンドドロップを許可するかどうかを設定します。
            this.SpreadList.PaintSelectionHeader = false;                                               // 選択ヘッダーを描画するかどうかを設定します。
            this.SpreadList.ClipboardOptions = ClipboardOptions.AllHeaders;                             // クリップボードにコピーする際のオプションを設定します。
            sheetView.ColumnHeader.Rows[0].Height = 26;                                                 // 列ヘッダーの高さを設定します。
            sheetView.GrayAreaBackColor = Color.White;                                                  // グレーエリアの背景色を設定します。
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9);                          // 行ヘッダーのフォントを設定します。
            sheetView.RowHeader.Columns[0].Width = 48;                                                  // 行ヘッダーの幅を設定します。
            sheetView.VerticalGridLine = new GridLine(GridLineType.Flat, Color.LightGray);              // 垂直グリッド線のスタイルを設定します。
            sheetView.RemoveRows(0, sheetView.Rows.Count);                                              // すべての行を削除します。
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            switch(menuItem.Name) {
                // 新規起算日を追加する
                case "ToolStripMenuItemAdd":
                    PaidLeaveDetail paidLeaveDetail = new (ConnectionVo, _pushStaffMasterVo);
                    paidLeaveDetail.StartPosition = FormStartPosition.CenterScreen;
                    paidLeaveDetail.ShowDialog(this);
                    break;

                // アプリケーションを終了する
                case "ToolStripMenuItemExit":
                    Close();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PaidLeaveList_FormClosing(object sender, FormClosingEventArgs e) {
            DialogResult dialogResult = MessageBox.Show("アプリケーションを終了します。よろしいですか？", "メッセージ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            switch(dialogResult) {
                case DialogResult.OK:
                    e.Cancel = false;
                    this.Dispose();
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }

        /// <summary>
        /// StaffMasterDao
        /// </summary>
        public StaffMasterDao StaffMasterDao {
            get {
                return _staffMasterDao;
            }
            set {
                _staffMasterDao = value;
            }
        }
        /// <summary>
        /// 有給取得ファイル（Head）
        /// </summary>
        public PaidLeaveEntitlementDao PaidLeaveEntitlementDao {
            get {
                return _paidLeaveEntitlementDao;
            }
            set {
                _paidLeaveEntitlementDao = value;
            }
        }
        /// <summary>
        /// 休暇取得マスター（Body）
        /// </summary>
        public TimeOffMasterDao TimeOffMasterDao {
            get {
                return _timeOffMasterDao;
            }
            set {
                _timeOffMasterDao = value;
            }
        }
        /// <summary>
        /// 配車記録ファイル
        /// </summary>
        public VehicleDispatchDetailDao VehicleDispatchDetailDao {
            get {
                return _vehicleDispatchDetailDao;
            }
            set {
                _vehicleDispatchDetailDao = value;
            }
        }
        /// <summary>
        /// 直近の有給起算日
        /// </summary>
        private DateTime RecentCommencementDate {
            get {
                return _recentCommencementDate;
            }
            set {
                _recentCommencementDate = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public ConnectionVo ConnectionVo {
            get {
                return _connectionVo;
            }
            set {
                _connectionVo = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public List<StaffMasterVo> ListStaffMasterVo {
            get {
                return _listStaffMasterVo;
            }
            set {
                _listStaffMasterVo = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public List<PaidLeaveEntitlementV0> ListPaidLeaveEntitlementV0 {
            get {
                return _listPaidLeaveEntitlementV0;
            }
            set {
                _listPaidLeaveEntitlementV0 = value;
            }
        }
    }
}
