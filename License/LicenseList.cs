/*
 * 2025-02-18
 */
using Common;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace License {
    public partial class LicenseList : Form {
        /*
         * List用
         */
        /// <summary>
        /// 社員コード
        /// </summary>
        private const int _colStaffCode = 0;
        /// <summary>
        /// 氏名
        /// </summary>
        private const int _colName = 1;
        /// <summary>
        /// 交付年月日
        /// </summary>
        private const int _colDeliveryDate = 2;
        /// <summary>
        /// 有効期限
        /// </summary>
        private const int _colExpirationDate = 3;
        /// <summary>
        /// 条件等
        /// </summary>
        private const int _colLicenseCondition = 4;
        /// <summary>
        /// 免許証番号
        /// </summary>
        private const int _colLicenseNumber = 5;
        /// <summary>
        /// 大型
        /// </summary>
        private const int _colLarge = 6;
        /// <summary>
        /// 中型
        /// </summary>
        private const int _colMedium = 7;
        /// <summary>
        /// 準中型
        /// </summary>
        private const int _colQuasiMedium = 8;
        /// <summary>
        /// 普通
        /// </summary>
        private const int _colOrdinary = 9;
        /// <summary>
        /// 大特
        /// </summary>
        private const int _colBigSpecial = 10;
        /// <summary>
        /// 大自二
        /// </summary>
        private const int _colBigAutoBike = 11;
        /// <summary>
        /// 普自二
        /// </summary>
        private const int _colOrdinaryAutoBike = 12;
        /// <summary>
        /// 小特
        /// </summary>
        private const int _colSmallSpecial = 13;
        /// <summary>
        /// 原付
        /// </summary>
        private const int _colWithARaw = 14;

        /*
         * 東海電子用
         */
        /// <summary>
        /// ALC-RECでの通しID
        /// </summary>
        private const int _colTId = 0;
        /// <summary>
        /// 氏名
        /// </summary>
        private const int _colTName = 1;
        /// <summary>
        /// 免許証番号
        /// </summary>
        private const int _colTLicenseNumber = 2;
        /// <summary>
        /// 免許期限
        /// </summary>
        private const int _colTLicenseExpirationDate = 3;
        /// <summary>
        /// 交付日(四輪)
        /// </summary>
        private const int _colTIssuanceDate = 4;
        /// <summary>
        /// 免許種類
        /// </summary>
        private const int _colTLicenseType = 5;
        /// <summary>
        /// PIN登録
        /// </summary>
        private const int _colTPin = 6;
        /// <summary>
        /// 証明写真
        /// </summary>
        private const int _colTPicture = 7;
        /// <summary>
        /// フリガナ
        /// </summary>
        private const int _colTNameKana = 8;

        private readonly ScreenForm _screenForm = new();
        /*
         * Dao
         */
        private readonly LicenseMasterDao _licenseMasterDao;
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;
        private List<LicenseMasterVo> _listLicenseMasterVo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public LicenseList(ConnectionVo connectionVo, Screen screen) {
            /*
             * Dao
             */
            _licenseMasterDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            /*
             * InitializeControl
             */
            InitializeComponent();
            this.InitializeSheetView(this.SheetViewList);
            this.InitializeSheetView(this.SheetViewToukaidenshi);

            this.SpreadList.ActiveSheetIndex = 0;                                               // ActiveSheet
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
            switch (this.SpreadList.ActiveSheet.SheetName) {
                case "LicenseList":                                                                         // SheetViewList
                    this.SetSheetViewList(SheetViewList, _licenseMasterDao.SelectAllLicenseMaster());
                    break;
                case "ToukaiDenshiCSV":                                                                     // SheetViewTokaidenshi
                    this.SetSheetViewToukaidenshi(SheetViewToukaidenshi, _licenseMasterDao.SelectAllLicenseMaster());
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemInsertNewRecord":
                    LicenseDetail licenseDetail = new(_connectionVo);
                    _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), licenseDetail);
                    licenseDetail.Show(this);
                    break;
                case "ToolStripMenuItemExportCSV":
                    //csv形式ファイルをエクスポートします
                    string fileName = string.Concat("東海電子免許証データ", DateTime.Now.ToString("MM月dd日"), "作成");
                    //アクティブシート上の全データをcsv形式ファイルに保存します
                    SheetViewToukaidenshi.SaveTextFile(new Directry().GetExcelDesktopPassCsv(fileName),
                                                       TextFileFlags.None,
                                                       FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly,
                                                       Environment.NewLine,
                                                       ",",
                                                       "");
                    MessageBox.Show("デスクトップへエクスポートしました", "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            // ダブルクリックされたのが従事者リストで無ければReturnする
            if (((FpSpread)sender).ActiveSheet.SheetName != "LicenseList")
                return;
            // ヘッダーのDoubleClickを回避
            if (e.ColumnHeader)
                return;
            LicenseDetail licenseDetail = new(_connectionVo, ((LicenseMasterVo)SheetViewList.Rows[e.Row].Tag).StaffCode);
            _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), licenseDetail);
            licenseDetail.Show(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpreadList_ActiveSheetChanged(object sender, EventArgs e) {
            switch (((FpSpread)sender).ActiveSheet.SheetName) {
                case "LicenseList":
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
                    this.TabControlExKana.Enabled = true;                                       // TabControlを有効にする
                    break;
                case "ToukaiDenshiCSV":
                    /*
                     * MenuStrip
                     */
                    List<string> listStringSheetViewToukaidenshi = new() {
                        "ToolStripMenuItemFile",
                        "ToolStripMenuItemExit",
                        "ToolStripMenuItemExport",
                        "ToolStripMenuItemExportCSV",
                        "ToolStripMenuItemHelp"
                        };
                    this.MenuStripEx1.ChangeEnable(listStringSheetViewToukaidenshi);
                    this.TabControlExKana.Enabled = false;                                      // TabControlを無効にする
                    break;
            }
        }

        /// <summary>
        /// InitializeSheetView
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns>SheetView</returns>
        private SheetView InitializeSheetView(SheetView sheetView) {
            SpreadList.AllowDragDrop = false;                                                   // DrugDropを禁止する
            SpreadList.PaintSelectionHeader = false;                                            // ヘッダの選択状態をしない
            SpreadList.TabStrip.DefaultSheetTab.Font = new Font("Yu Gothic UI", 9);
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
        private void TabControlExKana_Click(object sender, EventArgs e) {
            if (_listLicenseMasterVo is not null)
                this.SetSheetViewList(SheetViewList, _licenseMasterDao.SelectAllLicenseMaster());
        }

        int spreadListTopRow = 0;
        /// <summary>
        /// SetSheetViewList
        /// </summary>
        /// <param name="sheetView"></param>
        /// <param name="listLicenseMasterVo"></param>
        private void SetSheetViewList(SheetView sheetView, List<LicenseMasterVo> listLicenseMasterVo) {
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
            _listLicenseMasterVo = TabControlExKana.SelectedTab.Text switch {
                "あ行" => listLicenseMasterVo.FindAll(x => x.NameKana.StartsWith("ア") || x.NameKana.StartsWith("イ") || x.NameKana.StartsWith("ウ") || x.NameKana.StartsWith("エ") || x.NameKana.StartsWith("オ")),
                "か行" => listLicenseMasterVo.FindAll(x => x.NameKana.StartsWith("カ") || x.NameKana.StartsWith("ガ") || x.NameKana.StartsWith("キ") || x.NameKana.StartsWith("ギ") || x.NameKana.StartsWith("ク") || x.NameKana.StartsWith("グ") || x.NameKana.StartsWith("ケ") || x.NameKana.StartsWith("ゲ") || x.NameKana.StartsWith("コ") || x.NameKana.StartsWith("ゴ")),
                "さ行" => listLicenseMasterVo.FindAll(x => x.NameKana.StartsWith("サ") || x.NameKana.StartsWith("シ") || x.NameKana.StartsWith("ス") || x.NameKana.StartsWith("セ") || x.NameKana.StartsWith("ソ")),
                "た行" => listLicenseMasterVo.FindAll(x => x.NameKana.StartsWith("タ") || x.NameKana.StartsWith("ダ") || x.NameKana.StartsWith("チ") || x.NameKana.StartsWith("ツ") || x.NameKana.StartsWith("テ") || x.NameKana.StartsWith("デ") || x.NameKana.StartsWith("ト") || x.NameKana.StartsWith("ド")),
                "な行" => listLicenseMasterVo.FindAll(x => x.NameKana.StartsWith("ナ") || x.NameKana.StartsWith("ニ") || x.NameKana.StartsWith("ヌ") || x.NameKana.StartsWith("ネ") || x.NameKana.StartsWith("ノ")),
                "は行" => listLicenseMasterVo.FindAll(x => x.NameKana.StartsWith("ハ") || x.NameKana.StartsWith("パ") || x.NameKana.StartsWith("ヒ") || x.NameKana.StartsWith("ビ") || x.NameKana.StartsWith("フ") || x.NameKana.StartsWith("ブ") || x.NameKana.StartsWith("ヘ") || x.NameKana.StartsWith("ベ") || x.NameKana.StartsWith("ホ")),
                "ま行" => listLicenseMasterVo.FindAll(x => x.NameKana.StartsWith("マ") || x.NameKana.StartsWith("ミ") || x.NameKana.StartsWith("ム") || x.NameKana.StartsWith("メ") || x.NameKana.StartsWith("モ")),
                "や行" => listLicenseMasterVo.FindAll(x => x.NameKana.StartsWith("ヤ") || x.NameKana.StartsWith("ユ") || x.NameKana.StartsWith("ヨ")),
                "ら行" => listLicenseMasterVo.FindAll(x => x.NameKana.StartsWith("ラ") || x.NameKana.StartsWith("リ") || x.NameKana.StartsWith("ル") || x.NameKana.StartsWith("レ") || x.NameKana.StartsWith("ロ")),
                "わ行" => listLicenseMasterVo.FindAll(x => x.NameKana.StartsWith("ワ") || x.NameKana.StartsWith("ヲ") || x.NameKana.StartsWith("ン")),
                _ => listLicenseMasterVo,
            };
            // 削除済のレコードも表示
            if (!CheckBoxExRetirementFlag.Checked)
                _listLicenseMasterVo = _listLicenseMasterVo.FindAll(x => x.DeleteFlag == false);
            foreach (LicenseMasterVo licenseMasterVo in _listLicenseMasterVo.OrderBy(x => x.NameKana)) {
                sheetView.Rows.Add(row, 1);
                sheetView.RowHeader.Columns[0].Label = (row + 1).ToString();                                                    // Rowヘッダ
                sheetView.Rows[row].ForeColor = licenseMasterVo.DeleteFlag ? Color.Red : Color.Black;                           // 退職済のレコードのForeColorをセット
                sheetView.Rows[row].Height = 20;                                                                                // Rowの高さ
                sheetView.Rows[row].Resizable = false;                                                                          // RowのResizableを禁止
                sheetView.Rows[row].Tag = licenseMasterVo;
                sheetView.Cells[row, _colStaffCode].Text = licenseMasterVo.StaffCode.ToString("#####");                         // 社員コード
                sheetView.Cells[row, _colName].Text = licenseMasterVo.Name;                                                     // 氏名
                sheetView.Cells[row, _colDeliveryDate].Value = licenseMasterVo.DeliveryDate.Date;                               // 交付年月日
                sheetView.Cells[row, _colExpirationDate].Value = licenseMasterVo.ExpirationDate.Date;                           // 有効期限
                sheetView.Cells[row, _colLicenseCondition].Text = licenseMasterVo.LicenseCondition;                             // 条件等
                sheetView.Cells[row, _colLicenseNumber].Text = licenseMasterVo.LicenseNumber;                                   // 免許証番号
                sheetView.Cells[row, _colLarge].Text = licenseMasterVo.Large ? "○" : string.Empty;                            // 大型
                sheetView.Cells[row, _colMedium].Text = licenseMasterVo.Medium ? "○" : string.Empty;                          // 中型
                sheetView.Cells[row, _colQuasiMedium].Text = licenseMasterVo.QuasiMedium ? "○" : string.Empty;                // 準中型
                sheetView.Cells[row, _colOrdinary].Text = licenseMasterVo.Ordinary ? "○" : string.Empty;                      // 普通
                sheetView.Cells[row, _colBigSpecial].Text = licenseMasterVo.BigSpecial ? "○" : string.Empty;                  // 大特
                sheetView.Cells[row, _colBigAutoBike].Text = licenseMasterVo.BigAutoBike ? "○" : string.Empty;                // 大自二
                sheetView.Cells[row, _colOrdinaryAutoBike].Text = licenseMasterVo.OrdinaryAutoBike ? "○" : string.Empty;      // 普自二
                sheetView.Cells[row, _colSmallSpecial].Text = licenseMasterVo.SmallSpecial ? "○" : string.Empty;              // 小特
                sheetView.Cells[row, _colWithARaw].Text = licenseMasterVo.WithARaw ? "○" : string.Empty;                      // 原付
                row++;
            }
            SpreadList.SetViewportTopRow(0, spreadListTopRow);                                                                  // 先頭行（列）インデックスをセット
            SpreadList.ResumeLayout();                                                                                          // Spread 活性化
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(" ", row, " 件");
        }

        /// <summary>
        /// SetSheetViewToukaidenshi
        /// </summary>
        /// <param name="sheetView"></param>
        /// <param name="listLicenseMasterVo"></param>
        private void SetSheetViewToukaidenshi(SheetView sheetView, List<LicenseMasterVo> listLicenseMasterVo) {
            // Spread 非活性化
            SpreadList.SuspendLayout();
            // 先頭行（列）インデックスを取得
            spreadListTopRow = SpreadList.GetViewportTopRow(0);
            if (sheetView.Rows.Count > 0)
                sheetView.RemoveRows(0, sheetView.Rows.Count);
            int row = 0;

            foreach (LicenseMasterVo licenseMasterVo in listLicenseMasterVo) {
                /*
                 * 245番は予備で使用するので空けておく処理
                 */
                if (row + 1 == 245) {
                    /*
                     * 245番の予備を追加する
                     */
                    sheetView.Rows.Add(row, 1);
                    sheetView.RowHeader.Columns[0].Label = (row + 1).ToString(); // Rowヘッダ
                    sheetView.Rows[row].Height = 22; // Rowの高さ
                    sheetView.Rows[row].Resizable = false; // RowのResizableを禁止
                    sheetView.Cells[row, _colTId].Text = "245";
                    sheetView.Cells[row, _colTName].Text = "予備";
                    sheetView.Cells[row, _colTLicenseNumber].Text = string.Empty;
                    sheetView.Cells[row, _colTLicenseExpirationDate].Text = DateTime.Now.AddYears(2).ToString("yyyy/MM/dd");
                    sheetView.Cells[row, _colTIssuanceDate].Text = DateTime.Now.AddYears(-1).ToString("yyyy/MM/dd");
                    sheetView.Cells[row, _colTLicenseType].Text = "";
                    sheetView.Cells[row, _colTPin].Text = "無";
                    sheetView.Cells[row, _colTPicture].Text = "無";
                    sheetView.Cells[row, _colTNameKana].Text = "ヨビ";

                    row++;
                    sheetView.Rows.Add(row, 1);
                    sheetView.RowHeader.Columns[0].Label = (row + 1).ToString(); // Rowヘッダ
                    sheetView.Rows[row].Height = 22; // Rowの高さ
                    sheetView.Rows[row].Resizable = false; // RowのResizableを禁止
                    sheetView.Cells[row, _colTId].Text = string.Concat(row + 1);
                    sheetView.Cells[row, _colTName].Text = licenseMasterVo.Name;
                    sheetView.Cells[row, _colTLicenseNumber].Text = licenseMasterVo.LicenseNumber;
                    sheetView.Cells[row, _colTLicenseExpirationDate].Text = licenseMasterVo.ExpirationDate.ToString("yyyy/MM/dd");
                    sheetView.Cells[row, _colTIssuanceDate].Text = licenseMasterVo.DeliveryDate.ToString("yyyy/MM/dd");
                    sheetView.Cells[row, _colTLicenseType].Text = string.Empty;
                    sheetView.Cells[row, _colTPin].Text = "無";
                    sheetView.Cells[row, _colTPicture].Text = "無";
                    sheetView.Cells[row, _colTNameKana].Text = licenseMasterVo.NameKana;
                } else {
                    sheetView.Rows.Add(row, 1);
                    sheetView.RowHeader.Columns[0].Label = (row + 1).ToString(); // Rowヘッダ
                    sheetView.Rows[row].Height = 22; // Rowの高さ
                    sheetView.Rows[row].Resizable = false; // RowのResizableを禁止
                    sheetView.Cells[row, _colTId].Text = string.Concat(row + 1);
                    sheetView.Cells[row, _colTName].Text = licenseMasterVo.Name;
                    sheetView.Cells[row, _colTLicenseNumber].Text = licenseMasterVo.LicenseNumber;
                    sheetView.Cells[row, _colTLicenseExpirationDate].Text = licenseMasterVo.ExpirationDate.ToString("yyyy/MM/dd");
                    sheetView.Cells[row, _colTIssuanceDate].Text = licenseMasterVo.DeliveryDate.ToString("yyyy/MM/dd");
                    sheetView.Cells[row, _colTLicenseType].Text = string.Empty;
                    sheetView.Cells[row, _colTPin].Text = "無";
                    sheetView.Cells[row, _colTPicture].Text = "無";
                    sheetView.Cells[row, _colTNameKana].Text = licenseMasterVo.NameKana;
                }
                row++;
            }
            /*
             * 9999番の予備(点検用)を追加する
             */
            sheetView.Rows.Add(row, 1);
            sheetView.RowHeader.Columns[0].Label = (row + 1).ToString(); // Rowヘッダ
            sheetView.Rows[row].Height = 22; // Rowの高さ
            sheetView.Rows[row].Resizable = false; // RowのResizableを禁止
            sheetView.Cells[row, _colTId].Text = "9999";
            sheetView.Cells[row, _colTName].Text = "点検用";
            sheetView.Cells[row, _colTLicenseNumber].Text = string.Empty;
            sheetView.Cells[row, _colTLicenseExpirationDate].Text = DateTime.Now.AddYears(2).ToString("yyyy/MM/dd");
            sheetView.Cells[row, _colTIssuanceDate].Text = DateTime.Now.AddYears(-1).ToString("yyyy/MM/dd");
            sheetView.Cells[row, _colTLicenseType].Text = "";
            sheetView.Cells[row, _colTPin].Text = "無";
            sheetView.Cells[row, _colTPicture].Text = "無";
            sheetView.Cells[row, _colTNameKana].Text = "テンケンヨウ";

            // 先頭行（列）インデックスをセット
            SpreadList.SetViewportTopRow(0, spreadListTopRow);
            // Spread 活性化
            SpreadList.ResumeLayout();
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(" ", row, " 件");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LicenseList_FormClosing(object sender, FormClosingEventArgs e) {
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
