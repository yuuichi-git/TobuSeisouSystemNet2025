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
            UnionCode = 0,
            Name = 1,
            ReferenceDate = 2,
            YearsOfService = 3,
            StartDate = 4,
            WorkDays = 5,
            GrantedDays = 6,
            TimeOffStart = 7
        }

        private const int BlockRowCount = 3;
        private const int MaxTimeOffColumns = 25;

        private readonly DateUtility _dateUtility = new();
        private readonly Dictionary<int, DateTime> _dictionaryStartDate = [];

        /*
         * Dao
         */
        private readonly StaffMasterDao _staffMasterDao;
        private readonly PaidLeaveEntitlementDao _paidLeaveEntitlementDao;
        private readonly TimeOffMasterDao _timeOffMasterDao;
        private readonly VehicleDispatchDetailDao _vehicleDispatchDetailDao;
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;
        private List<StaffMasterVo> _listStaffMasterVo;
        private List<PaidLeaveEntitlementV0> _listPaidLeaveEntitlementV0;

        private int _spreadListTopRow = 0;
        private StaffMasterVo _pushStaffMasterVo = null;
        private CellRange _cellRange = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public PaidLeaveList(ConnectionVo connectionVo, Screen screen) {
            /*
             * Dao
             */
            this._staffMasterDao = new StaffMasterDao(connectionVo);
            this._paidLeaveEntitlementDao = new PaidLeaveEntitlementDao(connectionVo);
            this._timeOffMasterDao = new TimeOffMasterDao(connectionVo);
            this._vehicleDispatchDetailDao = new VehicleDispatchDetailDao(connectionVo);
            /*
             * Vo
             */
            this._connectionVo = connectionVo;

            this.InitializeComponent();
            this.InitializeSheetView(this.SheetViewList);
            /*
             * MenuStrip
             */
            List<string> listString = ["ToolStripMenuItemFile",
                                       "ToolStripMenuItemExit",
                                       "ToolStripMenuItemHelp"];
            this.CcMenuStrip1.ChangeEnable(listString);
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
            _listStaffMasterVo = _staffMasterDao.SelectAllStaffMaster(this.GroupBoxExBelongs.CreateArray(this.GroupBoxExBelongs),
                                                                      this.GroupBoxExJobForm.CreateArray(this.GroupBoxExJobForm),
                                                                      this.GroupBoxExOccupation.CreateArray(this.GroupBoxExOccupation),
                                                                      false);
            _listPaidLeaveEntitlementV0 = _paidLeaveEntitlementDao.SelectAllPaidLeaveEntitlementV0();
            this.SetSheetViewList(SheetViewList, _listStaffMasterVo, _listPaidLeaveEntitlementV0);
        }

        /// <summary>
        /// Spread全体の描画
        /// </summary>
        private void SetSheetViewList(SheetView sheetView, List<StaffMasterVo> listStaffMasterVo, List<PaidLeaveEntitlementV0> listPaidLeaveEntitlementV0) {

            sheetView.RowHeader.Visible = false;

            int rowCount = 0;
            this.SpreadList.SuspendLayout();
            _spreadListTopRow = this.SpreadList.GetViewportTopRow(0);

            if(sheetView.Rows.Count > 0) {
                sheetView.RemoveRows(0, sheetView.Rows.Count);
            }

            foreach(StaffMasterVo staffMasterVo in listStaffMasterVo.Where(x => x.PaidLeaveFlag).OrderBy(x => x.UnionCode)) {
                int baseRow = rowCount * BlockRowCount;
                this.SetOneBlock(sheetView, baseRow, staffMasterVo, listPaidLeaveEntitlementV0);
                rowCount++;
            }

            this.SpreadList.SetViewportTopRow(0, this._spreadListTopRow);
            this.SpreadList.ResumeLayout();
            this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = string.Concat(" ", rowCount, " 件を処理しました");
        }

        /// <summary>
        /// 1従事者分(3行)の描画
        /// </summary>
        private void SetOneBlock(SheetView sheetView,
                                 int baseRow,
                                 StaffMasterVo staffMasterVo,
                                 List<PaidLeaveEntitlementV0> listPaidLeaveEntitlementV0) {

            sheetView.Rows.Add(baseRow, BlockRowCount);
            sheetView.Rows[baseRow].Height = 24;
            sheetView.HorizontalGridLine = new GridLine(GridLineType.Flat);

            this.SetBlockHeader(sheetView, baseRow, staffMasterVo);
            this.InitializeStartDateDictionary(staffMasterVo);

            List<PaidLeaveEntitlementV0> listEntitlement = listPaidLeaveEntitlementV0.Where(x => x.StaffCode == staffMasterVo.StaffCode && x.StartDate >= _dictionaryStartDate[2])
                                                                                     .OrderBy(x => x.StartDate)
                                                                                     .Take(3)
                                                                                     .ToList();
            foreach(PaidLeaveEntitlementV0 entitlement in listEntitlement) {
                this.WriteEntitlementForBlock(sheetView, baseRow, staffMasterVo, entitlement);
            }
        }

        /// <summary>
        /// ヘッダ部(氏名・コード・基準日・起算日)の描画
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
        /// 直近起算日と過去2年分をDictionaryに格納
        /// </summary>
        /// <param name="staffMasterVo"></param>
        private void InitializeStartDateDictionary(StaffMasterVo staffMasterVo) {
            DateTime recentCommencementDate = this._dateUtility.GetPaidLeaveCommencementDate(staffMasterVo.PaidLeaveCommencementDate);
            _dictionaryStartDate.Clear();
            _dictionaryStartDate.Add(0, recentCommencementDate.AddYears(0).Date);   // 直近の起算日
            _dictionaryStartDate.Add(1, recentCommencementDate.AddYears(-1).Date);  // １年前の起算日
            _dictionaryStartDate.Add(2, recentCommencementDate.AddYears(-2).Date);  // ２年前の起算日
        }

        /// <summary>
        /// 3行分のうち該当する行に付与情報を書き込む
        /// </summary>
        /// <param name="sheetView"></param>
        /// <param name="baseRow"></param>
        /// <param name="staffMasterVo"></param>
        /// <param name="entitlement"></param>
        private void WriteEntitlementForBlock(SheetView sheetView, int baseRow, StaffMasterVo staffMasterVo, PaidLeaveEntitlementV0 entitlement) {
            for(int index = 0; index <= 2; index++) {
                if(_dictionaryStartDate[index].Date == entitlement.StartDate.Date) {
                    int targetRow = baseRow + (2 - index); // 2年前→0行目, 1年前→1行目, 0年前→2行目
                    WriteEntitlementRow(sheetView, targetRow, staffMasterVo, entitlement, _dictionaryStartDate[index], index);
                }
            }
        }

        /// <summary>
        /// 1行分の付与情報を書き込む
        /// </summary>
        /// <param name="sheetView"></param>
        /// <param name="row"></param>
        /// <param name="staffMasterVo"></param>
        /// <param name="entitlement"></param>
        /// <param name="startDate"></param>
        /// <param name="index"></param>
        private void WriteEntitlementRow(SheetView sheetView, int row, StaffMasterVo staffMasterVo, PaidLeaveEntitlementV0 entitlement, DateTime startDate, int index) {
            int workDays;
            if(CcCheckBoxWorkDaysFlag.Checked) {
                workDays = this._vehicleDispatchDetailDao.GetCountVehicleDispatchDetail(startDate.AddYears(-1), startDate.AddDays(-1), staffMasterVo.StaffCode);
            } else {
                workDays = 0;
            }

            sheetView.Cells[row, (int)Col.YearsOfService].Text = string.Concat(entitlement.YearsOfService, "年6か月");
            sheetView.Cells[row, (int)Col.StartDate].HorizontalAlignment = CellHorizontalAlignment.Center;
            sheetView.Cells[row, (int)Col.StartDate].Text = entitlement.StartDate.ToString("yyyy/MM/dd");
            sheetView.Cells[row, (int)Col.WorkDays].Text = workDays.ToString();
            sheetView.Cells[row, (int)Col.GrantedDays].Text = entitlement.GrantedDays.ToString();

            if(entitlement.GrantedDays > 0) {
                int startCol = (int)Col.TimeOffStart;
                int endCol = (int)Col.TimeOffStart + entitlement.GrantedDays - 1;

                sheetView.Cells[row, startCol, row, endCol].BackColor = Color.LightYellow;

                // 2年前の起算日が直近起算日から2年前の場合は破棄表示
                if(index == 2 && entitlement.StartDate.Date == this._dictionaryStartDate[2].Date) {
                    sheetView.Cells[row, (int)Col.YearsOfService, row, endCol].ForeColor = Color.White;
                    sheetView.Cells[row, (int)Col.YearsOfService, row, endCol].BackColor = Color.DarkGray;
                }
            }

            this.SetOneBody(row, staffMasterVo, index);
        }

        /// <summary>
        /// TimeOffMasterを書き出し
        /// </summary>
        /// <param name="rowNumber"></param>
        /// <param name="staffMasterVo"></param>
        /// <param name="number"></param>
        private void SetOneBody(int rowNumber, StaffMasterVo staffMasterVo, int number) {
            List<TimeOffMasterVo> listTimeOffMasterVo =
                this._timeOffMasterDao.SelectAllTimeOffMaster(staffMasterVo.StaffCode)
                                      .Where(x => x.BaseDate.Date == this._dictionaryStartDate[number].Date)
                                      .OrderBy(x => x.Date)
                                      .ToList();
            int colOffset = 0;
            foreach(TimeOffMasterVo timeOffMasterVo in listTimeOffMasterVo) {
                int colIndex = (int)Col.TimeOffStart + colOffset;
                if(colIndex >= (int)Col.TimeOffStart + MaxTimeOffColumns) {
                    break;
                }

                if(this._dictionaryStartDate[0].Date <= timeOffMasterVo.Date.Date) {
                    this.SheetViewList.Cells[rowNumber, colIndex].ForeColor = Color.Red;
                }

                this.SheetViewList.Cells[rowNumber, colIndex].Text = timeOffMasterVo.Date.ToString("yyyy/MM/dd");
                colOffset++;
            }
        }

        /// <summary>
        /// CcContextMenuStripがOpenされたStaffMasterVoを取得する
        /// </summary>
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
            this._cellRange = this.SpreadList.GetRootWorkbook().GetCellFromPixel(e.X, e.Y);
            this.SheetViewList.ClearSelection();
            int startRow = this._cellRange.Row / BlockRowCount * BlockRowCount;
            this.SheetViewList.AddSelection(startRow, 0, BlockRowCount, MaxTimeOffColumns);
        }

        /// <summary>
        /// InitializeSheetView
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns></returns>
        private void InitializeSheetView(SheetView sheetView) {
            this.SpreadList.AllowDragDrop = false;
            this.SpreadList.PaintSelectionHeader = false;
            this.SpreadList.ClipboardOptions = ClipboardOptions.AllHeaders;
            sheetView.ColumnHeader.Rows[0].Height = 26;
            sheetView.GrayAreaBackColor = Color.White;
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9);
            sheetView.RowHeader.Columns[0].Width = 48;
            sheetView.VerticalGridLine = new GridLine(GridLineType.Flat, Color.LightGray);
            sheetView.RemoveRows(0, sheetView.Rows.Count);
        }

        /// <summary>
        /// 
        /// </summary>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            switch(menuItem.Name) {
                // 新規起算日を追加する
                case "ToolStripMenuItemAdd":
                    PaidLeaveDetail paidLeaveDetail = new (_connectionVo, _pushStaffMasterVo);
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
    }
}
