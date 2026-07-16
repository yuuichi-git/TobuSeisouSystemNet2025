/*
 * 2026-07-01
 */
using CcControl;

using Common;

using Dao;

using FarPoint.Excel;
using FarPoint.Win.Spread;

using Vo;

namespace PaidLeave {
    public partial class PaidLeavePrint : Form {
        private DateUtility _dateUtility = new ();
        /*
         * Dao
         */
        private StaffMasterDao _staffMasterDao;
        private PaidLeaveEntitlementDao _paidLeaveEntitlementDao;
        private TimeOffMasterDao _timeOffMasterDao;
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;
        private List<StaffMasterVo> _listStaffMasterVo;
        private List<PaidLeaveEntitlementV0> _listPaidLeaveEntitlementV0;
        private List<PaidLeaveBalanceVo> _listPaidLeaveBalanceVo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public PaidLeavePrint(ConnectionVo connectionVo, Screen screen) {
            /*
             * Dao
             */
            StaffMasterDao = new(connectionVo);
            PaidLeaveEntitlementDao = new(connectionVo);
            TimeOffMasterDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            /*
             * InitializeControl
             */
            InitializeComponent();
            /*
             * MenuStrip
             */
            List<string> listString = ["ToolStripMenuItemFile",
                                       "ToolStripMenuItemExit",
                                       "ToolStripMenuItemExport",
                                       "ToolStripMenuItemExportExcel",
                                       "ToolStripMenuItemPrint",
                                       "ToolStripMenuItemPrintA4",
                                       "ToolStripMenuItemHelp"];
            this.CcMenuStrip1.ChangeEnable(listString);
            /*
             * 日付の初期化は、基本的に前月の処理が対象なため、前月の初日と前月の末日を設定する
             */
            this.CcDateTimeOperationDate1.SetValue(_dateUtility.GetBeginOfMonth(DateTime.Today.AddMonths(-1)));
            this.CcDateTimeOperationDate2.SetValue(_dateUtility.GetEndOfMonth(DateTime.Today.AddMonths(-1)));
            this.CcLabel3.Text = "集計期間内の使用日数合計：0日";
            this.InitializeSheetView(this.SheetViewList);
            this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = string.Empty;
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

            this.SetSheetViewList(SheetViewList);
        }

        /// <summary>
        /// 現在のSpreadListの表示位置を保持するための変数
        /// </summary>
        int _spreadListTopRow = 0;

        /// <summary>
        /// Spread全体の描画
        /// </summary>
        private void SetSheetViewList(SheetView sheetView) {
            int rowCount = 0;
            // 集計期間内の有給使用日数
            int paidLeaveUsageDuringThePeriod = 0;
            // 集計期間内の有給使用日数合計
            int sumPaidLeaveUsageDuringThePeriod = 0;

            this.SpreadList.SuspendLayout();
            _spreadListTopRow = this.SpreadList.GetViewportTopRow(0);

            if(sheetView.Rows.Count > 0)
                sheetView.RemoveRows(0, sheetView.Rows.Count);

            foreach(StaffMasterVo staffMasterVo in ListStaffMasterVo.Where(x => x.PaidLeaveFlag).OrderBy(x => x.UnionCode)) {
                sheetView.Rows.Add(rowCount, 1);
                sheetView.Rows[rowCount].Height = 24;
                sheetView.HorizontalGridLine = new GridLine(GridLineType.Flat);
                sheetView.Rows[rowCount].Tag = staffMasterVo;

                sheetView.Cells[rowCount, 0].Text = staffMasterVo.UnionCode.ToString("###0");
                sheetView.Cells[rowCount, 1].Text = staffMasterVo.DisplayName;
                sheetView.Cells[rowCount, 2].Text = string.Concat(this.CcDateTimeOperationDate1.GetDate().ToString("yyyy/MM/dd"), " ～ ", this.CcDateTimeOperationDate2.GetDate().ToString("yyyy/MM/dd"));

                int 有給付与日数合計 = 0;
                int 有給使用日数合計 = 0;
                int 有給残日数合計 = 0;
                try {
                    foreach(PaidLeaveBalanceVo paidLeaveBalanceVo in PaidLeaveEntitlementDao.SelectRemainingDays(this.CcDateTimeOperationDate2.GetDate(), staffMasterVo.StaffCode)) {
                        有給付与日数合計 += paidLeaveBalanceVo.GrantedDays;
                        有給使用日数合計 += paidLeaveBalanceVo.UsedDays;
                        有給残日数合計 += paidLeaveBalanceVo.RemainingDays;
                    }

                    // 有給付与日数合計
                    sheetView.Cells[rowCount, 3].Text = string.Concat(有給付与日数合計, "日");
                    // 有給使用日数合計
                    sheetView.Cells[rowCount, 4].Text = string.Concat(有給使用日数合計, "日");
                    // 集計期間内の有給使用日数を取得する
                    paidLeaveUsageDuringThePeriod = TimeOffMasterDao.SelectAllTimeOffMaster(CcDateTimeOperationDate1.GetDate(), CcDateTimeOperationDate2.GetDate(), 1, staffMasterVo.StaffCode).ToList().Count;
                    sheetView.Cells[rowCount, 5].Text = string.Concat(paidLeaveUsageDuringThePeriod, "日");
                    // 有給残日数合計
                    sheetView.Cells[rowCount, 6].Text = string.Concat(有給残日数合計, "日");
                } catch(Exception ex) {
                    MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // 集計期間内の有給使用日数合計を更新する
                sumPaidLeaveUsageDuringThePeriod += paidLeaveUsageDuringThePeriod;

                rowCount++;
            }

            this.CcLabel3.Text = string.Concat("集計期間内の使用日数合計：", sumPaidLeaveUsageDuringThePeriod, "日");
            this.SpreadList.SetViewportTopRow(0, _spreadListTopRow);
            this.SpreadList.ResumeLayout();
            this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = string.Concat(" ", rowCount, " 件を処理しました");
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            switch(menuItem.Name) {
                case "ToolStripMenuItemExportExcel":
                    //xlsx形式ファイルをエクスポートします
                    string fileName = string.Concat("有給休暇使用表", DateTime.Now.ToString("MM月dd日"), "作成");
                    this.SpreadList.SaveExcel(new DirectryUtility().GetExcelDesktopPassXlsx(fileName), ExcelSaveFlags.UseOOXMLFormat);
                    MessageBox.Show("デスクトップへエクスポートしました", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "ToolStripMenuItemPrintA4":
                    this.SpreadList.PrintSheet(SheetViewList);
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
        private void CcDateTime_ValueChanged(object sender, EventArgs e) {
            switch(((CcDateTime)sender).Name) {
                case "CcDateTimeOperationDate1":
                    if(((CcDateTime)sender).Value > CcDateTimeOperationDate2.GetValue()) {
                        CcDateTimeOperationDate2.SetValue(((CcDateTime)sender).Value);
                    }
                    break;
                case "CcDateTimeOperationDate2":
                    if(((CcDateTime)sender).Value < CcDateTimeOperationDate1.GetValue()) {
                        CcDateTimeOperationDate1.SetValue(((CcDateTime)sender).Value);
                    }
                    break;
            }
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
            sheetView.ColumnHeader.Rows[0].Height = 30;                                                 // 列ヘッダーの高さを設定します。
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
        private void PaidLeavePrint_FormClosing(object sender, FormClosingEventArgs e) {
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

        private StaffMasterDao StaffMasterDao {
            get {
                return _staffMasterDao;
            }

            set {
                _staffMasterDao = value;
            }
        }

        private PaidLeaveEntitlementDao PaidLeaveEntitlementDao {
            get {
                return _paidLeaveEntitlementDao;
            }

            set {
                _paidLeaveEntitlementDao = value;
            }
        }

        public TimeOffMasterDao TimeOffMasterDao {
            get {
                return _timeOffMasterDao;
            }

            set {
                _timeOffMasterDao = value;
            }
        }

        private List<StaffMasterVo> ListStaffMasterVo {
            get {
                return _listStaffMasterVo;
            }

            set {
                _listStaffMasterVo = value;
            }
        }

        private List<PaidLeaveEntitlementV0> ListPaidLeaveEntitlementV0 {
            get {
                return _listPaidLeaveEntitlementV0;
            }

            set {
                _listPaidLeaveEntitlementV0 = value;
            }
        }

        public List<PaidLeaveBalanceVo> ListPaidLeaveBalanceVo {
            get {
                return _listPaidLeaveBalanceVo;
            }

            set {
                _listPaidLeaveBalanceVo = value;
            }
        }
    }
}
