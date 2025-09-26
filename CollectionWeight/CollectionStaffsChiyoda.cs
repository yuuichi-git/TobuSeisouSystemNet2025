/*
 * 2025-1-22
 */
using Dao;

using FarPoint.Win.Spread;

using Common;

using Vo;

namespace Collection {
    public partial class CollectionStaffsChiyoda : Form {
        /*
         * Dao
         */
        private readonly CollectionWeightChiyodaDao _collectionWeightChiyodaDao;
        /*
         * インターネットから祝日のデータを取得
         */
        private Dictionary<DateTime, string> _dictionaryHoliday = new HolidayUtility().GetHoliday();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public CollectionStaffsChiyoda(ConnectionVo connectionVo, Screen screen) {
            /*
             * Dao
             */
            _collectionWeightChiyodaDao = new(connectionVo);
            /*
             * InitializeControl
             */
            InitializeComponent();
            /*
             * MenuStrip
             */
            List<string> listString = new() {
                "ToolStripMenuItemFile",
                "ToolStripMenuItemExit",
                "ToolStripMenuItemPrint",
                "ToolStripMenuItemPrintA4",
                "ToolStripMenuItemHelp"
            };
            MenuStripEx1.ChangeEnable(listString);

            this.DateTimePickerEx1.SetValueJp(DateTime.Now);
            this.DateTimePickerEx2.SetValueJp(DateTime.Now);
            this.InitializeSheetView(SpreadList, SheetViewList);
            this.InitializeSheetView(SpreadAggregate, SheetViewAggregate);
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "Initialize Success";
            /*
             * Eventを登録する
             */
            MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExUpdate_Click(object sender, EventArgs e) {
            this.SetSheetViewList(SheetViewList);
            this.PutSheetViewAggregate(SheetViewAggregate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemPrintA4":
                    this.SpreadAggregate.PrintSheet(SheetViewAggregate);
                    break;
                case "ToolStripMenuItemExit":
                    this.Close();
                    break;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        private void SetSheetViewList(SheetView sheetView) {
            int sheetViewListTopRow;
            // Spread 非活性化
            SpreadList.SuspendLayout();
            // Rowを削除する
            if (sheetView.Rows.Count > 0)
                sheetView.RemoveRows(0, sheetView.Rows.Count);
            // 先頭行（列）インデックスを取得
            sheetViewListTopRow = SpreadList.GetViewportTopRow(0);

            int i = 0;
            foreach (CollectionWeightChiyodaVo collectionWeightChiyodaVo in _collectionWeightChiyodaDao.SelectVehicleDispatchDetail(this.DateTimePickerEx1.GetValue(), this.DateTimePickerEx2.GetValue())) {
                sheetView.AddRows(i, 1);
                if (_dictionaryHoliday.ContainsKey(collectionWeightChiyodaVo.OperationDate)) {
                    sheetView.Cells[i, 0].ForeColor = Color.Red;
                    sheetView.Cells[i, 0].Text = string.Concat(collectionWeightChiyodaVo.OperationDate.ToString("yyyy年MM月dd日"), "(", _dictionaryHoliday[collectionWeightChiyodaVo.OperationDate], ")");
                    sheetView.Cells[i, 1].ForeColor = Color.Red;
                    sheetView.Cells[i, 1].Text = collectionWeightChiyodaVo.StaffDisplayName1;
                    sheetView.Cells[i, 2].ForeColor = Color.Red;
                    sheetView.Cells[i, 2].Text = collectionWeightChiyodaVo.StaffDisplayName2;
                    sheetView.Cells[i, 3].ForeColor = Color.Gray;
                    sheetView.Cells[i, 3].Text = collectionWeightChiyodaVo.StaffDisplayName3;
                } else {
                    sheetView.Cells[i, 0].ForeColor = Color.Black;
                    sheetView.Cells[i, 0].Text = collectionWeightChiyodaVo.OperationDate.ToString("yyyy年MM月dd日(dddd)");
                    sheetView.Cells[i, 1].ForeColor = Color.Black;
                    sheetView.Cells[i, 1].Text = collectionWeightChiyodaVo.StaffDisplayName1;
                    sheetView.Cells[i, 2].ForeColor = Color.Black;
                    sheetView.Cells[i, 2].Text = collectionWeightChiyodaVo.StaffDisplayName2;
                    sheetView.Cells[i, 3].ForeColor = Color.Gray;
                    sheetView.Cells[i, 3].Text = collectionWeightChiyodaVo.StaffDisplayName3;
                }
                i++;
            }
            // 先頭行（列）インデックスをセット
            SpreadList.SetViewportTopRow(0, sheetViewListTopRow);
            // Spread 活性化
            SpreadList.ResumeLayout();
        }

        private void PutSheetViewAggregate(SheetView sheetView) {
            int sheetViewAggregateTopRow;
            // Spread 非活性化
            SpreadAggregate.SuspendLayout();
            // Rowを削除する
            if (sheetView.Rows.Count > 0)
                sheetView.RemoveRows(0, sheetView.Rows.Count);
            // 先頭行（列）インデックスを取得
            sheetViewAggregateTopRow = SpreadAggregate.GetViewportTopRow(0);
            foreach (CollectionWeightGroupChiyodaVo collectionWeightGroupChiyodaVo in _collectionWeightChiyodaDao.SelectGroupByVehicleDispatchDetail(this.DateTimePickerEx1.GetValue(), this.DateTimePickerEx2.GetValue())) {
                bool newRowFlag = true;
                for (int rowNumber = 0; rowNumber < sheetView.RowCount; rowNumber++) {
                    string key1 = sheetView.Cells[rowNumber, 0].Text;
                    string key2 = sheetView.Cells[rowNumber, 1].Text;
                    int key3 = (int)sheetView.Cells[rowNumber, 2].Value;
                    int key4 = (int)sheetView.Cells[rowNumber, 3].Value;
                    if (collectionWeightGroupChiyodaVo.StaffDisplayName == key1 && collectionWeightGroupChiyodaVo.Occupation == key2) {
                        /*
                         * 祝日かどうかを判定する(日曜日は入っていないので注意してね)
                         */
                        if (_dictionaryHoliday.ContainsKey(collectionWeightGroupChiyodaVo.OperationDate)) {
                            /*
                             * 祝日の場合
                             */
                            key4++;
                            sheetView.Cells[rowNumber, 3].Value = key4;
                            newRowFlag = false;
                        } else {
                            /*
                             * 平日の場合
                             */
                            key3++;
                            sheetView.Cells[rowNumber, 2].Value = key3;
                            newRowFlag = false;
                        }
                    };
                }
                /*
                 * 新規Rowを挿入する
                 * 祝日かどうかを判定する(日曜日は入っていないので注意してね)
                 */
                if (newRowFlag) {
                    if (_dictionaryHoliday.ContainsKey(collectionWeightGroupChiyodaVo.OperationDate)) {
                        /*
                         * 祝日の場合
                         */
                        sheetView.AddRows(0, 1);
                        sheetView.Cells[0, 0].Tag = collectionWeightGroupChiyodaVo.StaffCode;
                        sheetView.Cells[0, 0].Text = collectionWeightGroupChiyodaVo.StaffDisplayName;
                        sheetView.Cells[0, 1].Text = collectionWeightGroupChiyodaVo.Occupation;
                        sheetView.Cells[0, 2].Value = 0; // 平日の出勤日数を初期化
                        sheetView.Cells[0, 3].Value = 1; // 休日の出勤日数を初期化
                        sheetView.Cells[0, 4].Value = 0;
                    } else {
                        /*
                         * 平日の場合
                         */
                        sheetView.AddRows(0, 1);
                        sheetView.Cells[0, 0].Tag = collectionWeightGroupChiyodaVo.StaffCode;
                        sheetView.Cells[0, 0].Text = collectionWeightGroupChiyodaVo.StaffDisplayName;
                        sheetView.Cells[0, 1].Text = collectionWeightGroupChiyodaVo.Occupation;
                        sheetView.Cells[0, 2].Value = 1; // 平日の出勤日数を初期化
                        sheetView.Cells[0, 3].Value = 0; // 休日の出勤日数を初期化
                        sheetView.Cells[0, 4].Value = 0;
                    }
                }
            }
            /*
             * Footer集計
             */
            int H_GKI = 0;
            int K_GKI = 0;
            for (int i = 0; i < sheetView.RowCount; i++) {
                H_GKI += (int)sheetView.Cells[i, 2].Value;
                K_GKI += (int)sheetView.Cells[i, 3].Value;
            }
            sheetView.ColumnFooter.Cells[0, 2].Text = H_GKI.ToString();
            sheetView.ColumnFooter.Cells[0, 3].Text = K_GKI.ToString();
            /*
             * 全ての配車先での出勤日数計算
             */
            for (int i = 0; i < sheetView.RowCount; i++) {
                sheetView.Cells[i, 4].Value = _collectionWeightChiyodaDao.GetWorkingDaysForStaff(this.DateTimePickerEx1.GetValue().Date, this.DateTimePickerEx2.GetValue().Date, (int)sheetView.Cells[i, 0].Tag);
            }
            // 先頭行（列）インデックスをセット
            SpreadAggregate.SetViewportTopRow(0, sheetViewAggregateTopRow);
            // Spread 活性化
            SpreadAggregate.ResumeLayout();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fpSpread"></param>
        /// <param name="sheetView"></param>
        /// <returns></returns>
        private SheetView InitializeSheetView(FpSpread fpSpread, SheetView sheetView) {
            fpSpread.AllowDragDrop = false;                                                         // DrugDropを禁止する
            fpSpread.PaintSelectionHeader = false;                                                  // ヘッダの選択状態をしない
            fpSpread.TabStripPolicy = TabStripPolicy.Never;                                         // シートタブを非表示
            sheetView.AlternatingRows.Count = 2;                                                    // 行スタイルを２行単位とします
            sheetView.AlternatingRows[0].BackColor = Color.WhiteSmoke;                              // 1行目の背景色を設定します
            sheetView.AlternatingRows[1].BackColor = Color.White;                                   // 2行目の背景色を設定します
            sheetView.ColumnHeader.Rows[0].Height = 28;                                             // Columnヘッダの高さ
            sheetView.GrayAreaBackColor = Color.White;
            sheetView.HorizontalGridLine = new GridLine(GridLineType.None);
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9);                      // 行ヘッダのFont
            sheetView.RowHeader.Columns[0].Width = 50;                                              // 行ヘッダの幅を変更します
            sheetView.VerticalGridLine = new GridLine(GridLineType.Flat, Color.LightGray);
            if (sheetView.Rows.Count > 0)                                                           // Rowを削除する
                sheetView.RemoveRows(0, sheetView.Rows.Count);
            return sheetView;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimePickerEx1_ValueChanged(object sender, EventArgs e) {
            if (((DateTimePicker)sender).Value > DateTimePickerEx2.GetValue()) {
                DateTimePickerEx2.SetValueJp(((DateTimePicker)sender).Value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimePickerEx2_ValueChanged(object sender, EventArgs e) {
            if (((DateTimePicker)sender).Value < DateTimePickerEx1.GetValue()) {
                DateTimePickerEx1.SetValueJp(((DateTimePicker)sender).Value);
            }
            if (((DateTimePicker)sender).Value > DateTime.Now.Date) {
                ((DateTimePicker)sender).Value = DateTime.Now.Date;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CollectionWeightChiyoda_FormClosing(object sender, FormClosingEventArgs e) {
            DialogResult dialogResult = MessageBox.Show("アプリケーションを終了します。よろしいですか？", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
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
