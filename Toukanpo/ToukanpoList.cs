/*
 * 2025-02-21
 */
using Common;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace Toukanpo {
    public partial class ToukanpoList : Form {
        /*
         * Columns
         */
        /// <summary>
        /// 従事者コード
        /// </summary>
        private const int _colStaffCode = 0;
        /// <summary>
        /// 組合コード
        /// </summary>
        private const int _colUnionCode = 1;
        /// <summary>
        /// 画面表示氏名
        /// </summary>
        private const int _colName = 2;
        /// <summary>
        /// 取得年月日
        /// </summary>
        private const int _colCertificationDate = 3;
        /// <summary>
        /// 2025-02-22
        /// メモ
        /// </summary>
        private const int _colMemo = 4;

        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly DateUtility _dateUtility = new();
        private readonly ScreenForm _screenForm = new();
        /*
         * Dao
         */
        private readonly ToukanpoTrainingCardDao _toukanpoTrainingCardDao;
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;
        private List<ToukanpoTrainingCardVo> _listToukanpoTrainingCardVo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public ToukanpoList(ConnectionVo connectionVo, Screen screen) {
            /*
             * Dao
             */
            _toukanpoTrainingCardDao = new(connectionVo);
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

            this.InitializeSheetView(this.SheetViewList);
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
        private void TabControlEx1_Click(object sender, EventArgs e) {
            if (_listToukanpoTrainingCardVo is not null)
                this.SetSheetViewList(SheetViewList, _toukanpoTrainingCardDao.SelectAllToukanpoTrainingCardMaster());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExUpdate_Click(object sender, EventArgs e) {
            this.SetSheetViewList(SheetViewList, _toukanpoTrainingCardDao.SelectAllToukanpoTrainingCardMaster());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpreadList_CellDoubleClick(object sender, CellClickEventArgs e) {
            if (e.ColumnHeader)                 // ヘッダーのDoubleClickを回避
                return;
            ToukanpoDetail toukanpoDetail = new(_connectionVo, ((ToukanpoTrainingCardVo)SheetViewList.Rows[e.Row].Tag).StaffCode);
            _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), toukanpoDetail);
            toukanpoDetail.Show(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemInsertNewRecord":
                    ToukanpoDetail toukanpoDetail = new(_connectionVo);
                    _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), toukanpoDetail);
                    toukanpoDetail.Show(this);
                    break;
                /*
                 * アプリケーションを終了する
                 */
                case "ToolStripMenuItemExit":
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns></returns>
        private SheetView InitializeSheetView(SheetView sheetView) {
            SpreadList.AllowDragDrop = false; // DrugDropを禁止する
            SpreadList.PaintSelectionHeader = false; // ヘッダの選択状態をしない
            SpreadList.TabStrip.DefaultSheetTab.Font = new Font("Yu Gothic UI", 9);
            sheetView.AlternatingRows.Count = 2; // 行スタイルを２行単位とします
            sheetView.AlternatingRows[0].BackColor = Color.WhiteSmoke; // 1行目の背景色を設定します
            sheetView.AlternatingRows[1].BackColor = Color.White; // 2行目の背景色を設定します
            sheetView.ColumnHeader.Rows[0].Height = 26; // Columnヘッダの高さ
            sheetView.GrayAreaBackColor = Color.White;
            sheetView.HorizontalGridLine = new GridLine(GridLineType.None);
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9); // 行ヘッダのFont
            sheetView.RowHeader.Columns[0].Width = 48; // 行ヘッダの幅を変更します
            sheetView.VerticalGridLine = new GridLine(GridLineType.Flat, Color.LightGray);
            sheetView.RemoveRows(0, sheetView.Rows.Count);
            return sheetView;
        }

        int spreadListTopRow = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        /// <param name="listToukanpoTrainingCardVo"></param>
        private void SetSheetViewList(SheetView sheetView, List<ToukanpoTrainingCardVo> listToukanpoTrainingCardVo) {
            int row = 0;
            // Spread 非活性化
            this.SpreadList.SuspendLayout();
            // 先頭行（列）インデックスを取得
            spreadListTopRow = this.SpreadList.GetViewportTopRow(0);
            /*
             * Rowを削除する
             */
            if (sheetView.Rows.Count > 0)
                sheetView.RemoveRows(0, sheetView.Rows.Count);
            _listToukanpoTrainingCardVo = this.TabControlEx1.SelectedTab.Text switch {
                "あ行" => listToukanpoTrainingCardVo.FindAll(x => x.NameKana.StartsWith("ア") || x.NameKana.StartsWith("イ") || x.NameKana.StartsWith("ウ") || x.NameKana.StartsWith("エ") || x.NameKana.StartsWith("オ")),
                "か行" => listToukanpoTrainingCardVo.FindAll(x => x.NameKana.StartsWith("カ") || x.NameKana.StartsWith("ガ") || x.NameKana.StartsWith("キ") || x.NameKana.StartsWith("ギ") || x.NameKana.StartsWith("ク") || x.NameKana.StartsWith("グ") || x.NameKana.StartsWith("ケ") || x.NameKana.StartsWith("ゲ") || x.NameKana.StartsWith("コ") || x.NameKana.StartsWith("ゴ")),
                "さ行" => listToukanpoTrainingCardVo.FindAll(x => x.NameKana.StartsWith("サ") || x.NameKana.StartsWith("シ") || x.NameKana.StartsWith("ス") || x.NameKana.StartsWith("セ") || x.NameKana.StartsWith("ソ")),
                "た行" => listToukanpoTrainingCardVo.FindAll(x => x.NameKana.StartsWith("タ") || x.NameKana.StartsWith("ダ") || x.NameKana.StartsWith("チ") || x.NameKana.StartsWith("ツ") || x.NameKana.StartsWith("テ") || x.NameKana.StartsWith("デ") || x.NameKana.StartsWith("ト") || x.NameKana.StartsWith("ド")),
                "な行" => listToukanpoTrainingCardVo.FindAll(x => x.NameKana.StartsWith("ナ") || x.NameKana.StartsWith("ニ") || x.NameKana.StartsWith("ヌ") || x.NameKana.StartsWith("ネ") || x.NameKana.StartsWith("ノ")),
                "は行" => listToukanpoTrainingCardVo.FindAll(x => x.NameKana.StartsWith("ハ") || x.NameKana.StartsWith("パ") || x.NameKana.StartsWith("ヒ") || x.NameKana.StartsWith("ビ") || x.NameKana.StartsWith("フ") || x.NameKana.StartsWith("ブ") || x.NameKana.StartsWith("ヘ") || x.NameKana.StartsWith("ベ") || x.NameKana.StartsWith("ホ")),
                "ま行" => listToukanpoTrainingCardVo.FindAll(x => x.NameKana.StartsWith("マ") || x.NameKana.StartsWith("ミ") || x.NameKana.StartsWith("ム") || x.NameKana.StartsWith("メ") || x.NameKana.StartsWith("モ")),
                "や行" => listToukanpoTrainingCardVo.FindAll(x => x.NameKana.StartsWith("ヤ") || x.NameKana.StartsWith("ユ") || x.NameKana.StartsWith("ヨ")),
                "ら行" => listToukanpoTrainingCardVo.FindAll(x => x.NameKana.StartsWith("ラ") || x.NameKana.StartsWith("リ") || x.NameKana.StartsWith("ル") || x.NameKana.StartsWith("レ") || x.NameKana.StartsWith("ロ")),
                "わ行" => listToukanpoTrainingCardVo.FindAll(x => x.NameKana.StartsWith("ワ") || x.NameKana.StartsWith("ヲ") || x.NameKana.StartsWith("ン")),
                _ => listToukanpoTrainingCardVo,
            };
            foreach (ToukanpoTrainingCardVo toukanpoTrainingCardVo in _listToukanpoTrainingCardVo.OrderBy(x => x.NameKana)) {
                sheetView.Rows.Add(row, 1);
                sheetView.RowHeader.Columns[0].Label = (row + 1).ToString();                                                            // Rowヘッダ
                sheetView.Rows[row].ForeColor = toukanpoTrainingCardVo.DeleteFlag ? Color.Red : Color.Black;                            // 退職済のレコードのForeColorをセット
                sheetView.Rows[row].Height = 20;                                                                                        // Rowの高さ
                sheetView.Rows[row].Resizable = false;                                                                                  // RowのResizableを禁止
                sheetView.Rows[row].Tag = toukanpoTrainingCardVo;
                sheetView.Cells[row, _colStaffCode].Text = toukanpoTrainingCardVo.StaffCode.ToString("#####");                          // 社員コード
                sheetView.Cells[row, _colUnionCode].Text = toukanpoTrainingCardVo.UnionCode.ToString("#####");                          // 組合コード
                sheetView.Cells[row, _colName].Text = toukanpoTrainingCardVo.Name;                                                      // 氏名
                sheetView.Cells[row, _colCertificationDate].Text = toukanpoTrainingCardVo.CertificationDate.ToString("yyyy年MM月dd日");
                sheetView.Cells[row, _colMemo].Text = toukanpoTrainingCardVo.Memo;                                                      // メモ
                row++;
            }
            this.SpreadList.SetViewportTopRow(0, spreadListTopRow);                                                                          // 先頭行（列）インデックスをセット
            this.SpreadList.ResumeLayout();                                                                                                  // Spread 活性化
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(" ", row, " 件");

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToukanpoList_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
