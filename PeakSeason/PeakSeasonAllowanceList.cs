/*
 * 2026-07-21
 */
using CcControl;

using Common;

using Dao;

using FarPoint.Excel;
using FarPoint.Win.Spread;

using Vo;

namespace PeakSeason {
    public partial class PeakSeasonAllowanceList : Form {
        private DateUtility _dateUtility = new ();
        /*
         * Dao
         */
        private VehicleDispatchDetailDao _vehicleDispatchDetailDao;
        private PeakSeasonAllowanceDao _peakSeasonAllowanceDao;
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;
        private List<PeakSeasonAllowanceVo> _listPeakSeasonAllowanceVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public PeakSeasonAllowanceList(ConnectionVo connectionVo, Screen screen) {
            /*
             * Dao
             */
            VehicleDispatchDetailDao = new(connectionVo);
            PeakSeasonAllowanceDao = new(connectionVo);
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
            this.CcLabelPeakSeasonAllowanceCount.Text = string.Concat("集計期間内の対象日数合計：0日");
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
            try {
                ListPeakSeasonAllowanceVo = PeakSeasonAllowanceDao.SelectListPeakSeasonAllowanceVo(this.CcDateTimeOperationDate1.GetDate(),
                                                                                                   this.CcDateTimeOperationDate2.GetDate());

            } catch(Exception exception) {
                MessageBox.Show(exception.Message);
                return;
            }

            this.SetSheetViewList(SheetViewList);
        }

        /// <summary>
        /// 現在のSpreadListの表示位置を保持するための変数
        /// </summary>
        int _spreadListTopRow = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        private void SetSheetViewList(SheetView sheetView) {
            int rowCount = 0;
            int peakSeasonAllowanceCount = 0;

            this.SpreadList.SuspendLayout();
            _spreadListTopRow = this.SpreadList.GetViewportTopRow(0);

            if(sheetView.Rows.Count > 0)
                sheetView.RemoveRows(0, sheetView.Rows.Count);

            foreach(PeakSeasonAllowanceVo peakSeasonAllowanceVo in ListPeakSeasonAllowanceVo) {
                sheetView.Rows.Add(rowCount, 1);
                sheetView.Rows[rowCount].Height = 24;
                sheetView.HorizontalGridLine = new GridLine(GridLineType.Flat);
                sheetView.Rows[rowCount].Tag = peakSeasonAllowanceVo;
                // 組合コード
                sheetView.Cells[rowCount, 0].Text = peakSeasonAllowanceVo.UnionCode.ToString("###0");
                // 所属名
                sheetView.Cells[rowCount, 1].Text = peakSeasonAllowanceVo.BelongsName;
                // 氏名
                sheetView.Cells[rowCount, 2].Text = peakSeasonAllowanceVo.DisplayName;
                // 期間
                sheetView.Cells[rowCount, 3].Text = string.Concat(this.CcDateTimeOperationDate1.GetDate().ToString("yyyy/MM/dd"), " ～ ", this.CcDateTimeOperationDate2.GetDate().ToString("yyyy/MM/dd"));
                // 対象日数
                sheetView.Cells[rowCount, 4].Text = string.Concat(peakSeasonAllowanceVo.CountDays, "日");
                peakSeasonAllowanceCount += peakSeasonAllowanceVo.CountDays;

                rowCount++;
            }

            this.CcLabelPeakSeasonAllowanceCount.Text = string.Concat("集計期間内の対象日数合計：", peakSeasonAllowanceCount, "日");
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
                    string fileName = string.Concat("繁忙期割増費集計表", DateTime.Now.ToString("MM月dd日"), "作成");
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
            sheetView.RowHeader.Columns[0].Width = 100;                                                 // 行ヘッダーの幅を設定します。
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

        /* 
         * ----------------------------------------
         * 
         * プロパティ
         * 
         * ----------------------------------------
         */
        public VehicleDispatchDetailDao VehicleDispatchDetailDao {
            get {
                return _vehicleDispatchDetailDao;
            }

            set {
                _vehicleDispatchDetailDao = value;
            }
        }

        public PeakSeasonAllowanceDao PeakSeasonAllowanceDao {
            get {
                return _peakSeasonAllowanceDao;
            }

            set {
                _peakSeasonAllowanceDao = value;
            }
        }

        public ConnectionVo ConnectionVo {
            get {
                return _connectionVo;
            }

            set {
                _connectionVo = value;
            }
        }

        public List<PeakSeasonAllowanceVo> ListPeakSeasonAllowanceVo {
            get {
                return _listPeakSeasonAllowanceVo;
            }

            set {
                _listPeakSeasonAllowanceVo = value;
            }
        }
    }
}
