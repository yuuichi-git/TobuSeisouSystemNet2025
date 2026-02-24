/*
 * 2026-01-24
 */
using Common;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace WastCollection {
    public partial class WastCollectionList : Form {
        /*
         * SPREADのColumnの番号
         */
        /// <summary>
        /// 見積日
        /// </summary>
        private const int _colOfficeQuotationDate = 0;
        /// <summary>
        /// 依頼区
        /// </summary>
        private const int _colOfficeRequestWordName = 1;
        /// <summary>
        /// 本社　会社名
        /// </summary>
        private const int _colOfficeCompanyName = 2;
        /// <summary>
        /// 本社　担当者
        /// </summary>
        private const int _colOfficeContactPerson = 3;
        /// <summary>
        /// 本社　住所
        /// </summary>
        private const int _colOfficeAddress = 4;
        /// <summary>
        /// 本社　連絡先
        /// </summary>
        private const int _colOfficeTelephoneNumber = 5;
        /// <summary>
        /// 本社　携帯
        /// </summary>
        private const int _colOfficeCellphoneNumber = 6;
        /// <summary>
        /// 現場　回収場所
        /// </summary>
        private const int _colWorkSiteLocation = 7;
        /// <summary>
        /// 現場　住所
        /// </summary>
        private const int _colWorkSiteAddress = 8;
        /// <summary>
        /// 回収日
        /// </summary>
        private const int _colPickupDate = 9;
        /// <summary>
        /// 備考
        /// </summary>
        private const int _colRemarks = 10;
        /*
         * インスタンス作成
         */
        private readonly DateTime _defaultDateTime = new(1900, 1, 1);
        private readonly ScreenForm _screenForm = new();
        /*
         * Object
         */
        private Screen _screen;
        /*
         * Dao
         */
        private WasteCollectionHeadDao _wasteCollectionHeadDao;
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;

        public WastCollectionList(ConnectionVo connectionVo, Screen screen) {
            /*
             * Object
             */
            _screen = screen;
            /*
             * Dao
             */
            _wasteCollectionHeadDao = new(connectionVo);
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
            List<string> listString = new() {
                "ToolStripMenuItemFile",
                "ToolStripMenuItemExit",
                "ToolStripMenuItemEdit",
                "ToolStripMenuItemInsertNewRecord",
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);
            /*
             * FpSpread/Viewを初期化
             */
            this.InitializeSheetViewList(this.SheetViewList);
            /*
             * StatusStrip
             */
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;
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
            this.PutSheetViewList(this.SheetViewList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpreadList_CellDoubleClick(object sender, CellClickEventArgs e) {
            if (e.ColumnHeader)                                                                                                         // ヘッダーのDoubleClickを回避
                return;

            if ((ModifierKeys & Keys.Shift) == Keys.Shift) {                                                                            // Shiftキーが押された場合
                WastCollectionPaper wastCollectionPaper = new(_connectionVo, ((WasteCollectionHeadVo)SheetViewList.Rows[e.Row].Tag).Id);
                _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), wastCollectionPaper);
                wastCollectionPaper.ShowDialog();
                return;
            }
            /*
             * WastCollectionDetailを表示する
             */
            WastCollectionDetail wastCollectionDetail = new(_connectionVo, ((WasteCollectionHeadVo)SheetViewList.Rows[e.Row].Tag).Id);
            _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), wastCollectionDetail);
            wastCollectionDetail.ShowDialog();
        }

        /// <summary>
        /// SheetViewの現在のRow位置を保持する
        /// </summary>
        int _spreadListTopRow = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        private void PutSheetViewList(SheetView sheetView) {
            int rowCount = 0;
            this.SpreadList.SuspendLayout();                                                                                            // Spread 非活性化
            this._spreadListTopRow = this.SpreadList.GetViewportTopRow(0);                                                              // 先頭行（列）インデックスを取得
            if (sheetView.Rows.Count > 0)                                                                                               // Rowを削除する
                sheetView.RemoveRows(0, sheetView.Rows.Count);
            try {
                foreach (WasteCollectionHeadVo wasteCollectionHeadVo in _wasteCollectionHeadDao.SelectAllWasteCollectionHead()) {
                    sheetView.Rows.Add(rowCount, 1);
                    sheetView.RowHeader.Columns[0].Label = (rowCount + 1).ToString();                                                   // Rowヘッダ
                    sheetView.Rows[rowCount].Height = 20;                                                                               // Rowの高さ
                    sheetView.Rows[rowCount].Resizable = false;                                                                         // RowのResizableを禁止
                    sheetView.Rows[rowCount].Tag = wasteCollectionHeadVo;

                    sheetView.Cells[rowCount, _colOfficeQuotationDate].Value = wasteCollectionHeadVo.OfficeQuotationDate;
                    sheetView.Cells[rowCount, _colOfficeRequestWordName].Text = wasteCollectionHeadVo.OfficeRequestWordName;
                    sheetView.Cells[rowCount, _colOfficeCompanyName].Text = wasteCollectionHeadVo.OfficeCompanyName;
                    sheetView.Cells[rowCount, _colOfficeContactPerson].Text = wasteCollectionHeadVo.OfficeContactPerson;
                    sheetView.Cells[rowCount, _colOfficeAddress].Text = wasteCollectionHeadVo.OfficeAddress;
                    sheetView.Cells[rowCount, _colOfficeTelephoneNumber].Text = wasteCollectionHeadVo.OfficeTelephoneNumber;
                    sheetView.Cells[rowCount, _colOfficeCellphoneNumber].Text = wasteCollectionHeadVo.OfficeCellphoneNumber;
                    sheetView.Cells[rowCount, _colWorkSiteLocation].Text = wasteCollectionHeadVo.WorkSiteLocation;
                    sheetView.Cells[rowCount, _colWorkSiteAddress].Text = wasteCollectionHeadVo.WorkSiteAddress;
                    if (wasteCollectionHeadVo.PickupDate != _defaultDateTime) {
                        sheetView.Cells[rowCount, _colPickupDate].Value = wasteCollectionHeadVo.PickupDate;
                    } else {
                        sheetView.Cells[rowCount, _colPickupDate].Text = string.Empty;
                    }
                    sheetView.Cells[rowCount, _colRemarks].Text = wasteCollectionHeadVo.Remarks;

                    rowCount++;
                }
            } catch (Exception ex) {
                MessageBox.Show(string.Concat("データの取得に失敗しました。", Environment.NewLine, ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = " データの取得に失敗しました。";
                return;
            }
            // 先頭行（列）インデックスをセット
            this.SpreadList.SetViewportTopRow(0, _spreadListTopRow);
            // Spread 活性化
            this.SpreadList.ResumeLayout();
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(" ", rowCount, " 件を処理しました");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns></returns>
        private SheetView InitializeSheetViewList(SheetView sheetView) {
            this.SpreadList.AllowDragDrop = false;                                                                                      // DrugDropを禁止する
            this.SpreadList.PaintSelectionHeader = false;                                                                               // ヘッダの選択状態をしない
            this.SpreadList.TabStripPolicy = TabStripPolicy.Never;                                                                      // シートタブを非表示
            sheetView.AlternatingRows.Count = 2;                                                                                        // 行スタイルを２行単位とします
            sheetView.AlternatingRows[0].BackColor = Color.WhiteSmoke;                                                                  // 1行目の背景色を設定します
            sheetView.AlternatingRows[1].BackColor = Color.White;                                                                       // 2行目の背景色を設定します
            sheetView.ColumnHeader.Rows[0].Height = 30;                                                                                 // Columnヘッダの高さ
            sheetView.GrayAreaBackColor = Color.White;
            sheetView.HorizontalGridLine = new GridLine(GridLineType.None);
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9);                                                          // 行ヘッダのFont
            sheetView.RowHeader.Columns[0].Width = 50;                                                                                  // 行ヘッダの幅を変更します
            sheetView.VerticalGridLine = new GridLine(GridLineType.Flat, Color.LightGray);
            sheetView.RemoveRows(0, sheetView.Rows.Count);
            return sheetView;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemInsertNewRecord":                                                                                 // 新規登録画面を表示する
                    WastCollectionDetail wastCollectionDetail = new(_connectionVo);
                    _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), wastCollectionDetail);
                    wastCollectionDetail.ShowDialog();
                    break;
                case "ToolStripMenuItemExit":                                                                                           // アプリケーションを終了する
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WastCollectionList_FormClosing(object sender, FormClosingEventArgs e) {
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
