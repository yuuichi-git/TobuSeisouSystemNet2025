/*
 * 2025-05-23
 */
using Common;

using ControlEx;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace Accident {
    public partial class AccidentList : Form {
        private readonly ScreenForm _screenForm = new();
        /*
         * Columnの番号
         */
        /// <summary>
        /// 発生年月日
        /// </summary>
        private const int _colOccurrenceDate = 0;
        /// <summary>
        /// 発生場所
        /// </summary>
        private const int _colOccurrenceAddress = 1;
        /// <summary>
        /// 事故処理区分
        /// </summary>
        private const int _colTotallingFlag = 2;
        /// <summary>
        /// 受付の種類
        /// </summary>
        private const int _colAccident_Kind = 3;
        /// <summary>
        /// 氏名
        /// </summary>
        private const int _colDisplayName = 4;
        /// <summary>
        /// 職種
        /// </summary>
        private const int _colWorkKind = 5;
        /// <summary>
        /// 車両登録番号
        /// </summary>
        private const int _colCarRegistrationNumber = 6;
        /// <summary>
        /// 概要
        /// </summary>
        private const int _colAccidentSummary = 7;
        /// <summary>
        /// 詳細
        /// </summary>
        private const int _colAccidentDetail = 8;
        /// <summary>
        /// 指導
        /// </summary>
        private const int _colGuide = 9;

        /*
         * Dao
         */
        private readonly CarAccidentMasterDao _carAccidentMasterDao;
        private readonly CarMasterDao _carMasterDao;
        private readonly StaffMasterDao _staffMasterDao;
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public AccidentList(ConnectionVo connectionVo, Screen screen) {
            /*
             * Dao 
             */
            _carAccidentMasterDao = new(connectionVo);
            _carMasterDao = new(connectionVo);
            _staffMasterDao = new(connectionVo);
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
            List<string> listStringSheetViewList = new() {
                        "ToolStripMenuItemFile",
                        "ToolStripMenuItemExit",
                        "ToolStripMenuItemEdit",
                        "ToolStripMenuItemInsertNewRecord",
                        "ToolStripMenuItemHelp"
                     };
            this.MenuStripEx1.ChangeEnable(listStringSheetViewList);
            // 日付を初期化
            this.DateTimePickerExOperationDate1.SetValueJp(DateTime.Now.AddMonths(-3));
            this.DateTimePickerExOperationDate2.SetValueJp(DateTime.Now.AddDays(1));
            this.InitializeSheetView(SheetViewList);
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat("総レコード数：0件");
            /*
             * Eventを登録する
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExUpdate_Click(object sender, EventArgs e) {
            try {
                this.PutSheetViewList(_carAccidentMasterDao.SelectAllCarAccidentMaster(this.DateTimePickerExOperationDate1.GetValue(), this.DateTimePickerExOperationDate2.GetValue()));
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
        }

        int spreadListTopRow = 0;
        /// <summary>
        /// PutSheetViewList
        /// </summary>
        /// <param name="listCarAccidentMasterVo"></param>
        private void PutSheetViewList(List<CarAccidentMasterVo> listCarAccidentMasterVo) {
            // Spread 非活性化
            this.SpreadList.SuspendLayout();
            // 先頭行（列）インデックスを取得
            spreadListTopRow = SpreadList.GetViewportTopRow(0);
            // Rowを削除する
            if (SheetViewList.Rows.Count > 0)
                SheetViewList.RemoveRows(0, SheetViewList.Rows.Count);

            int i = 0;
            foreach (CarAccidentMasterVo carAccidentMasterVo in listCarAccidentMasterVo.OrderBy(x => x.OccurrenceYmdHms)) {
                SheetViewList.Rows.Add(i, 1);
                SheetViewList.RowHeader.Columns[0].Label = (i + 1).ToString(); // Rowヘッダ
                SheetViewList.Rows[i].Height = 22; // Rowの高さ
                SheetViewList.Rows[i].Resizable = false; // RowのResizableを禁止

                SheetViewList.Rows[i].Tag = carAccidentMasterVo; //carAccidentLedgerVoを退避する
                SheetViewList.Cells[i, _colOccurrenceDate].Value = carAccidentMasterVo.OccurrenceYmdHms;
                SheetViewList.Cells[i, _colOccurrenceAddress].Text = carAccidentMasterVo.OccurrenceAddress;
                SheetViewList.Cells[i, _colTotallingFlag].Text = carAccidentMasterVo.TotallingFlag ? "事故として扱う" : "";
                SheetViewList.Cells[i, _colAccident_Kind].Text = carAccidentMasterVo.AccidentKind;
                SheetViewList.Cells[i, _colDisplayName].Text = carAccidentMasterVo.DisplayName;
                SheetViewList.Cells[i, _colWorkKind].Text = carAccidentMasterVo.WorkKind;
                SheetViewList.Cells[i, _colCarRegistrationNumber].Text = carAccidentMasterVo.CarRegistrationNumber;
                SheetViewList.Cells[i, _colAccidentSummary].Text = carAccidentMasterVo.AccidentSummary;
                SheetViewList.Cells[i, _colAccidentDetail].Text = carAccidentMasterVo.AccidentDetail;
                SheetViewList.Cells[i, _colGuide].Text = carAccidentMasterVo.Guide;
                i++;
            }
            // 先頭行（列）インデックスをセット
            this.SpreadList.SetViewportTopRow(0, spreadListTopRow);
            // Spread 活性化
            this.SpreadList.ResumeLayout(true);
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat("総レコード数：", listCarAccidentMasterVo.Count, "件");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemInsertNewRecord":                                        // 新規レコード
                    AccidentDetail accidentDetail = new(_connectionVo, null);
                    _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), accidentDetail);
                    accidentDetail.Show(this);
                    break;
                case "ToolStripMenuItemExit":                                                   // アプリケーションを終了する
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpreadList_CellDoubleClick(object sender, CellClickEventArgs e) {
            // ヘッダーのDoubleClickを回避
            if (e.ColumnHeader)
                return;
            AccidentDetail accidentDetail = new(_connectionVo, (CarAccidentMasterVo)SheetViewList.Rows[e.Row].Tag);
            _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), accidentDetail);
            accidentDetail.Show(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns></returns>
        private SheetView InitializeSheetView(SheetView sheetView) {
            this.SpreadList.AllowDragDrop = false;                                              // DrugDropを禁止する
            this.SpreadList.PaintSelectionHeader = false;                                       // ヘッダの選択状態をしない
            this.SpreadList.TabStrip.DefaultSheetTab.Font = new Font("Yu Gothic UI", 9);
            sheetView.AlternatingRows.Count = 2;                                                // 行スタイルを２行単位とします
            sheetView.AlternatingRows[0].BackColor = Color.WhiteSmoke;                          // 1行目の背景色を設定します
            sheetView.AlternatingRows[1].BackColor = Color.White;                               // 2行目の背景色を設定します
            sheetView.ColumnHeader.Rows[0].Height = 26;                                         // Columnヘッダの高さ
            sheetView.GrayAreaBackColor = Color.White;
            sheetView.HorizontalGridLine = new GridLine(GridLineType.None);
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9);                  // 行ヘッダのFont
            sheetView.RowHeader.Columns[0].Width = 48;                                          // 行ヘッダの幅を変更します
            sheetView.VerticalGridLine = new GridLine(GridLineType.Flat, Color.LightGray);
            sheetView.RemoveRows(0, sheetView.Rows.Count);
            return sheetView;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimePickerExOperationDate1_ValueChanged(object sender, EventArgs e) {
            if (((DateTimePickerEx)sender).Value > DateTimePickerExOperationDate2.Value) {
                DateTimePickerExOperationDate2.Value = DateTimePickerExOperationDate1.Value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimePickerExOperationDate2_ValueChanged(object sender, EventArgs e) {
            if (((DateTimePickerEx)sender).Value < DateTimePickerExOperationDate1.Value) {
                DateTimePickerExOperationDate1.Value = DateTimePickerExOperationDate2.Value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccidentList_FormClosing(object sender, FormClosingEventArgs e) {
            DialogResult dialogResult = MessageBox.Show("アプリケーションを終了します。よろしいですか？", "メッセージ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            switch (dialogResult) {
                case DialogResult.OK:
                    e.Cancel = false;
                    Dispose();
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }
    }
}
