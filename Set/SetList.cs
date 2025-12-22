/*
 * 2025-12-15
 */
using Common;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace Set {
    public partial class SetList : Form {
        /*
         * SPREADのColumnの番号
         */
        /// <summary>
        /// 組番号
        /// </summary>
        private const int _colSetCode = 0;
        /// <summary>
        /// 市区町村コード
        /// </summary>
        private const int _colWordCode = 1;
        /// <summary>
        /// 組名
        /// </summary>
        private const int _colSetName = 2;
        /// <summary>
        /// 組名略称１
        /// </summary>
        private const int _colSetName1 = 3;
        /// <summary>
        /// 組名略称２
        /// </summary>
        private const int _colSetName2 = 4;
        /// <summary>
        /// 運賃支払いコード
        /// </summary>
        private const int _colFareCode = 5;
        /// <summary>
        /// 管理地
        /// </summary>
        private const int _colManagedSpaceCode = 6;
        /// <summary>
        /// 分類コード
        /// </summary>
        private const int _colClassificationCode = 7;
        /// <summary>
        /// 代番連絡方法
        /// </summary>
        private const int _colContactMethod = 8;
        /// <summary>
        /// 配車基本人数
        /// </summary>
        private const int _colNumberOfPeople = 9;
        /// <summary>
        /// スペアを付けられる配車先かどうか
        /// </summary>
        private const int _colSpareOfPeople = 10;
        /// <summary>
        /// 稼働曜日
        /// </summary>
        private const int _colWorkingDays = 11;
        /// <summary>
        /// 第五週の稼働
        /// </summary>
        private const int _colFiveLap = 12;
        /// <summary>
        /// 移動できるかどうか
        /// </summary>
        private const int _colMoveFlag = 13;
        /// <summary>
        /// 備考
        /// </summary>
        private const int _colRemarks = 14;


        /*
         * インスタンス作成
         */
        private readonly ScreenForm _screenForm = new();
        /*
         * Dao
         */
        private readonly ClassificationMasterDao _classificationMasterDao;
        private readonly FareMasterDao _fareMasterDao;
        private readonly ManagedSpaceDao _managedSpaceDao;
        private readonly SetMasterDao _setMasterDao;
        private readonly WordMasterDao _wordMasterDao;
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;
        /*
         * Dictionary
         */
        private readonly Dictionary<int, string> _dictionaryClassificationCode = new();
        private readonly Dictionary<int, string> _dictionaryFareCode = new();
        private readonly Dictionary<int, string> _dictionaryManagedSpaceCode = new();
        private readonly Dictionary<int, string> _dictionaryWordCode = new();
        /// <summary>
        /// 0:該当なし 1:足立 2:三郷 3:産廃車庫
        /// </summary>
        private readonly Dictionary<int, string> _dictionaryContactMethod = new() { { 10, "TEL" }, { 11, "FAX" }, { 12, "" }, { 13, "TEL/FAX" } };

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public SetList(ConnectionVo connectionVo, Screen screen) {
            /*
             * Dao
             */
            _classificationMasterDao = new(connectionVo);
            _fareMasterDao = new(connectionVo);
            _managedSpaceDao = new(connectionVo);
            _setMasterDao = new(connectionVo);
            _wordMasterDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            /*
             * Dictionary
             */
            foreach (ClassificationMasterVo classificationMasterVo in _classificationMasterDao.SelectAllClassificationMaster())
                _dictionaryClassificationCode.Add(classificationMasterVo.Code, classificationMasterVo.Name);
            foreach (FareMasterVo fareMasterVo in _fareMasterDao.SelectAllFareMasterVo())
                _dictionaryFareCode.Add(fareMasterVo.Code, fareMasterVo.Name);
            foreach (ManagedSpaceMasterVo managedSpaceVo in _managedSpaceDao.SelectAllManagedSpace())
                _dictionaryManagedSpaceCode.Add(managedSpaceVo.Code, managedSpaceVo.Name);
            foreach (WordMasterVo wordMasterVo in _wordMasterDao.SelectAllWordMaster())
                _dictionaryWordCode.Add(wordMasterVo.Code, wordMasterVo.Name);
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
             * Spread
             */
            this.InitializeSheetViewList(this.SheetViewList);
            /*
             * StatusStrip
             */
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;
            /*
             * Eventを登録する
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += this.ToolStripMenuItem_Click;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExUpdate_Click(object sender, EventArgs e) {
            this.SetSheetViewList(this.SheetViewList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemInsertNewRecord":                                                                // 新規レコード作成
                    SetDetail setDetail = new(_connectionVo);
                    _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), setDetail);
                    setDetail.ShowDialog(this);
                    break;
                case "ToolStripMenuItemExit":                                                                           // アプリケーションを終了する
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
            if (e.ColumnHeader)                                                                                                                                                         // ヘッダーのDoubleClickを回避
                return;
            /*
             * CarDetailを表示する
             */
            SetDetail setDetail = new(_connectionVo, (int)SheetViewList.Cells[e.Row, _colSetCode].Value);
            _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), setDetail);
            setDetail.ShowDialog(this);
        }

        int _spreadListTopRow = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        private void SetSheetViewList(SheetView sheetView) {
            List<SetMasterVo> _listSetMasterVo = _setMasterDao.SelectAllSetMaster();
            this.SpreadList.SuspendLayout();                                                                                                                                            // 非活性化
            _spreadListTopRow = SpreadList.GetViewportTopRow(0);                                                                                                                        // 先頭行（列）インデックスを取得
            if (sheetView.Rows.Count > 0)                                                                                                                                               // Rowを削除する
                sheetView.RemoveRows(0, sheetView.Rows.Count);

            int i = 0;
            foreach (SetMasterVo setMasterVo in _listSetMasterVo.OrderBy(x => x.SetCode)) {
                sheetView.Rows.Add(i, 1);
                sheetView.RowHeader.Columns[0].Label = (i + 1).ToString();                                                                                                              // Rowヘッダ
                sheetView.Rows[i].Height = 22;                                                                                                                                          // Rowの高さ
                sheetView.Rows[i].Resizable = false;                                                                                                                                    // RowのResizableを禁止
                sheetView.Rows[i].ForeColor = !setMasterVo.DeleteFlag ? Color.Black : Color.Red;                                                                                        // 削除済レコードは赤色で表示する

                sheetView.Cells[i, _colSetCode].Value = setMasterVo.SetCode;
                sheetView.Cells[i, _colWordCode].Text = _dictionaryWordCode[setMasterVo.WordCode];
                sheetView.Cells[i, _colSetName].Text = setMasterVo.SetName;
                sheetView.Cells[i, _colSetName1].Text = setMasterVo.SetName1;
                sheetView.Cells[i, _colSetName2].Text = setMasterVo.SetName2;
                sheetView.Cells[i, _colFareCode].Text = _dictionaryFareCode[setMasterVo.FareCode];
                sheetView.Cells[i, _colManagedSpaceCode].Text = _dictionaryManagedSpaceCode[setMasterVo.ManagedSpaceCode];
                sheetView.Cells[i, _colClassificationCode].Text = _dictionaryClassificationCode[setMasterVo.ClassificationCode];
                sheetView.Cells[i, _colContactMethod].Text = _dictionaryContactMethod[setMasterVo.ContactMethod];
                sheetView.Cells[i, _colNumberOfPeople].Text = string.Concat(setMasterVo.NumberOfPeople, " 人");
                sheetView.Cells[i, _colSpareOfPeople].Text = setMasterVo.SpareOfPeople.ToString();
                sheetView.Cells[i, _colWorkingDays].Text = setMasterVo.WorkingDays;
                sheetView.Cells[i, _colFiveLap].Text = setMasterVo.FiveLap.ToString();
                sheetView.Cells[i, _colMoveFlag].Text = setMasterVo.MoveFlag.ToString();
                sheetView.Cells[i, _colRemarks].Text = setMasterVo.Remarks;
                sheetView.Rows[i].Tag = setMasterVo;
                i++;
            }
            this.SpreadList.SetViewportTopRow(0, _spreadListTopRow);                                                                                                                    // 先頭行（列）インデックスをセット

            this.SpreadList.ResumeLayout();                                                                                                                                             // 活性化
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(" ", i, " 件");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns></returns>
        private SheetView InitializeSheetViewList(SheetView sheetView) {
            this.SpreadList.AllowDragDrop = false;                                                      // DrugDropを禁止する
            this.SpreadList.PaintSelectionHeader = false;                                               // ヘッダの選択状態をしない
            this.SpreadList.TabStripPolicy = TabStripPolicy.Never;                                      // シートタブを表示しない
            sheetView.AlternatingRows.Count = 2;                                                        // 行スタイルを２行単位とします
            sheetView.AlternatingRows[0].BackColor = Color.WhiteSmoke;                                  // 1行目の背景色を設定します
            sheetView.AlternatingRows[1].BackColor = Color.White;                                       // 2行目の背景色を設定します
            sheetView.ColumnHeader.Rows[0].Height = 30;                                                 // Columnヘッダの高さ
            sheetView.GrayAreaBackColor = Color.White;
            sheetView.HorizontalGridLine = new GridLine(GridLineType.None);
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9);                          // 行ヘッダのFont
            sheetView.RowHeader.Columns[0].Width = 50;                                                  // 行ヘッダの幅を変更します
            sheetView.VerticalGridLine = new GridLine(GridLineType.Flat, Color.LightGray);
            sheetView.RemoveRows(0, sheetView.Rows.Count);
            return sheetView;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetList_FormClosing(object sender, FormClosingEventArgs e) {
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
