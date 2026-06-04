/*
 * 2026-05-26
 */
using System.Text;

using Common;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace Estra {
    public partial class EstraList : Form {
        private string _targetName;
        private List<MJyoumuinVo> _listMJyoumuinVo;
        private List<MSyaryoVo> _listMSharyoVo;

        /*
         * Dao
         */
        private LicenseMasterDao _licenseMasterDao;
        private CarMasterDao _carMasterDao;
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;

        public EstraList(ConnectionVo connectionVo, string targetName) {
            _targetName = targetName;
            /*
             * Dao
             */
            _licenseMasterDao = new(connectionVo);
            _carMasterDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            /*
             * InitializeControls
             */
            InitializeComponent();
            /*
             * MenuStrip
             */
            List<string> listStringSheetViewM_JYOUMUIN = new() {"ToolStripMenuItemFile",
                                                                "ToolStripMenuItemExit",
                                                                "ToolStripMenuItemExport",
                                                                "ToolStripMenuItemExportCSV",
                                                                "ToolStripMenuItemHelp"};
            this.CcMenuStrip1.ChangeEnable(listStringSheetViewM_JYOUMUIN);
            this.CcMenuStrip1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;                                         // Eventを登録する
            /*
             * SheetTabの調整
             */
            switch (_targetName) {
                case "M_JYOUMUIN":
                    SpreadList.ActiveSheet = SheetViewM_JYOUMUIN;                                                                           // SheetViewM_JYOUMUINをアクティブにする
                    InitializeSheetView(this.SheetViewM_JYOUMUIN);
                    break;
                case "M_SHARYO":
                    SpreadList.ActiveSheet = SheetViewM_SHARYO;                                                                             // SheetViewM_SHARYOをアクティブにする
                    InitializeSheetView(this.SheetViewM_SHARYO);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExUpdate_Click(object sender, EventArgs e) {
            switch (_targetName) {
                case "M_JYOUMUIN":                                                                                                          // SheetViewM_JYOUMUIN
                    this.SetSheetViewM_JYOUMUIN(SheetViewM_JYOUMUIN, _licenseMasterDao.SelectAllLicenseMaster());
                    break;
                case "M_SHARYO":                                                                                                            // SheetViewM_SHARYO"
                    SetSheetViewM_SHARYO(SheetViewM_SHARYO, _carMasterDao.SelectAllCarMaster());
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
                case "ToolStripMenuItemExportCSV":
                    string filePath = string.Empty;
                    switch (SpreadList.ActiveSheet.SheetName) {
                        case "M_JYOUMUIN":
                            /*
                             * csv形式ファイルをエクスポートします 矢崎エナジーシステム(M_JYOUMUIN)データ
                             * Encoding.Unicode ← UTF-16 LE（BOM付き）でエンコードしないとエラーになるよ
                             */
                            filePath = string.Concat("矢崎エナジーシステム(M_JYOUMUIN)データ", DateTime.Now.ToString("MM月dd日"), "作成");
                            using (StreamWriter writer = new(new DirectryUtility().GetExcelDesktopPassCsv(filePath), false, Encoding.Unicode)) {                                                // Encoding.Unicode ← UTF-16 LE（BOM付き）
                                for (int i = 0; i < _listMJyoumuinVo.Count; i++) {
                                    MJyoumuinVo mJyoumuinVo = _listMJyoumuinVo[i];

                                    // CSV 1行を作成
                                    string line = string.Join(",", mJyoumuinVo.JyoumuinCode,
                                                                   mJyoumuinVo.JyoumuinName.Length >= 8 ? mJyoumuinVo.JyoumuinName.Substring(0, 8) : mJyoumuinVo.JyoumuinName,                  // 乗務員名は全角８文字以内
                                                                   mJyoumuinVo.JyoumuinNameKana.Length >= 16 ? mJyoumuinVo.JyoumuinNameKana.Substring(0, 16) : mJyoumuinVo.JyoumuinNameKana,    // 乗務員名カナは全角１６文字文字以内
                                                                   mJyoumuinVo.JigyousyoCode,
                                                                   mJyoumuinVo.SyozokuCode,
                                                                   mJyoumuinVo.GetsujiSyukeiTaishou,
                                                                   mJyoumuinVo.RoumuKanriTaishou,
                                                                   mJyoumuinVo.SaiyouYmd,
                                                                   mJyoumuinVo.TaishokuYmd
                                    );
                                    writer.WriteLine(line);
                                }
                            }
                            MessageBox.Show("デスクトップへエクスポートしました", "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;

                        case "M_SHARYO":
                            /*
                             * csv形式ファイルをエクスポートします 矢崎エナジーシステム(M_SHARYO)データ
                             * Encoding.Unicode ← UTF-16 LE（BOM付き）でエンコードしないとエラーになるよ
                             */
                            filePath = string.Concat("矢崎エナジーシステム(M_SHARYO)データ", DateTime.Now.ToString("MM月dd日"), "作成");
                            using (StreamWriter writer = new(new DirectryUtility().GetExcelDesktopPassCsv(filePath), false, Encoding.Unicode)) {                                                // Encoding.Unicode ← UTF-16 LE（BOM付き）



                            }
                            MessageBox.Show("デスクトップへエクスポートしました", "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                    }

                    break;

                case "ToolStripMenuItemExit":                                                   // アプリケーションを終了する
                    this.Close();
                    break;
            }
        }

        int _spreadListTopRow = 0;
        /// <summary>
        /// M_JYOUMUIN　作成
        /// </summary>
        /// <param name="sheetView"></param>
        /// <param name="listLicenseMasterVo"></param>
        private void SetSheetViewM_JYOUMUIN(SheetView sheetView, List<LicenseMasterVo> listLicenseMasterVo) {
            _listMJyoumuinVo = new();

            // Spread 非活性化
            this.SpreadList.SuspendLayout();
            // 先頭行（列）インデックスを取得
            this._spreadListTopRow = SpreadList.GetViewportTopRow(0);
            if (sheetView.Rows.Count > 0)
                sheetView.RemoveRows(0, sheetView.Rows.Count);
            int row = 0;                                                                                                                    // Rowインデックス
            int lastId = listLicenseMasterVo.Max(x => x.UnionCode) + 1;                                                                     // 最終ID(245番を含ませる)
            foreach (LicenseMasterVo licenseMasterVo in listLicenseMasterVo.OrderBy(x => x.UnionCode)) {
                MJyoumuinVo mJyoumuinVo = new();
                switch (licenseMasterVo.UnionCode) {
                    case 0:
                        sheetView.Rows.Add(row, 1);
                        sheetView.RowHeader.Columns[0].Label = (row + 1).ToString();                                                        // Rowヘッダ
                        sheetView.Rows[row].Height = 22;                                                                                    // Rowの高さ
                        sheetView.Rows[row].Resizable = false;                                                                              // RowのResizableを禁止
                        sheetView.Cells[row, 0].Text = lastId.ToString("00000000");
                        sheetView.Cells[row, 1].Text = licenseMasterVo.Name;
                        sheetView.Cells[row, 2].Text = licenseMasterVo.NameKana;
                        sheetView.Cells[row, 3].Text = "0001";                                                  // 事業所
                        sheetView.Cells[row, 4].Text = "0001";                                                  // 所属
                        sheetView.Cells[row, 5].Text = "1";                                                     // 月次集計対象
                        sheetView.Cells[row, 6].Text = "1";                                                     // 労務管理対象
                        sheetView.Cells[row, 7].Text = "";                                                      // 採用年月日
                        sheetView.Cells[row, 8].Text = "";                                                      // 退職年月日
                        /*
                         * CSV用にList<MJyoumuinVo>に追加する
                         */
                        mJyoumuinVo.JyoumuinCode = lastId.ToString("00000000");
                        mJyoumuinVo.JyoumuinName = licenseMasterVo.Name;
                        mJyoumuinVo.JyoumuinNameKana = licenseMasterVo.NameKana;
                        mJyoumuinVo.JigyousyoCode = "0001";
                        mJyoumuinVo.SyozokuCode = "0001";
                        mJyoumuinVo.GetsujiSyukeiTaishou = "1";
                        mJyoumuinVo.RoumuKanriTaishou = "1";
                        mJyoumuinVo.SaiyouYmd = "";
                        mJyoumuinVo.TaishokuYmd = "";

                        row++;
                        lastId++;
                        break;
                    default:
                        sheetView.Rows.Add(row, 1);
                        sheetView.RowHeader.Columns[0].Label = (row + 1).ToString();                                                        // Rowヘッダ
                        sheetView.Rows[row].Height = 22;                                                                                    // Rowの高さ
                        sheetView.Rows[row].Resizable = false;                                                                              // RowのResizableを禁止
                        sheetView.Cells[row, 0].Text = licenseMasterVo.UnionCode.ToString("00000000");
                        sheetView.Cells[row, 1].Text = licenseMasterVo.Name;
                        sheetView.Cells[row, 2].Text = licenseMasterVo.NameKana;
                        sheetView.Cells[row, 3].Text = "0001";                                                  // 事業所
                        sheetView.Cells[row, 4].Text = "0001";                                                  // 所属
                        sheetView.Cells[row, 5].Text = "1";                                                     // 月次集計対象
                        sheetView.Cells[row, 6].Text = "1";                                                     // 労務管理対象
                        sheetView.Cells[row, 7].Text = "";                                                      // 採用年月日
                        sheetView.Cells[row, 8].Text = "";                                                      // 退職年月日
                        /*
                         * CSV用にList<MJyoumuinVo>に追加する
                         */
                        mJyoumuinVo.JyoumuinCode = licenseMasterVo.UnionCode.ToString("00000000");
                        mJyoumuinVo.JyoumuinName = licenseMasterVo.Name;
                        mJyoumuinVo.JyoumuinNameKana = licenseMasterVo.NameKana;
                        mJyoumuinVo.JigyousyoCode = "0001";
                        mJyoumuinVo.SyozokuCode = "0001";
                        mJyoumuinVo.GetsujiSyukeiTaishou = "1";
                        mJyoumuinVo.RoumuKanriTaishou = "1";
                        mJyoumuinVo.SaiyouYmd = "";
                        mJyoumuinVo.TaishokuYmd = "";

                        row++;
                        break;
                }
                _listMJyoumuinVo.Add(mJyoumuinVo);
            }

            /*
             * 99999999番の予備(運転者共通)を追加する
             */
            sheetView.Rows.Add(row, 1);
            sheetView.RowHeader.Columns[0].Label = (row + 1).ToString(); // Rowヘッダ
            sheetView.Rows[row].Height = 22; // Rowの高さ
            sheetView.Rows[row].Resizable = false; // RowのResizableを禁止
            sheetView.Cells[row, 0].Text = "99999999";                                                          // 乗務員コード
            sheetView.Cells[row, 1].Text = "運転者共通";                                                          // 乗務員名
            sheetView.Cells[row, 2].Text = "ウンテンシャキョウツウ";                                                // フリガナ
            sheetView.Cells[row, 3].Text = "0001";                                                              // 事業所
            sheetView.Cells[row, 4].Text = "0001";                                                              // 所属
            sheetView.Cells[row, 5].Text = "1";                                                                 // 月次集計対象
            sheetView.Cells[row, 6].Text = "1";                                                                 // 労務管理対象
            sheetView.Cells[row, 7].Text = "";                                                                  // 採用年月日
            sheetView.Cells[row, 8].Text = "";                                                                  // 退職年月日
            /*
             * CSV用にList<MJyoumuinVo>に追加する
             */
            MJyoumuinVo mJyoumuinVo99999999 = new();
            mJyoumuinVo99999999.JyoumuinCode = "99999999";
            mJyoumuinVo99999999.JyoumuinName = "運転者共通";
            mJyoumuinVo99999999.JyoumuinNameKana = "ウンテンシャキョウツウ";
            mJyoumuinVo99999999.JigyousyoCode = "0001";
            mJyoumuinVo99999999.SyozokuCode = "0001";
            mJyoumuinVo99999999.GetsujiSyukeiTaishou = "1";
            mJyoumuinVo99999999.RoumuKanriTaishou = "1";
            mJyoumuinVo99999999.SaiyouYmd = "";
            mJyoumuinVo99999999.TaishokuYmd = "";
            _listMJyoumuinVo.Add(mJyoumuinVo99999999);

            this.SpreadList.SetViewportTopRow(0, _spreadListTopRow);                                                                        // 先頭行（列）インデックスをセット
            this.SpreadList.ResumeLayout();                                                                                                 // Spread 活性化
            this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = string.Concat(" ", row, " 件");
        }

        /// <summary>
        /// M_SHARYO　作成
        /// </summary>
        /// <param name="sheetView"></param>
        /// <param name="listCarMasterVo"></param>
        private void SetSheetViewM_SHARYO(SheetView sheetView, List<CarMasterVo> listCarMasterVo) {
            _listMSharyoVo = new();

            // Spread 非活性化
            this.SpreadList.SuspendLayout();
            // 先頭行（列）インデックスを取得
            this._spreadListTopRow = SpreadList.GetViewportTopRow(0);
            if (sheetView.Rows.Count > 0)
                sheetView.RemoveRows(0, sheetView.Rows.Count);

            int row = 0;                                                                                                                    // Rowインデックス
            foreach (CarMasterVo carMasterVo in listCarMasterVo.Where(x => x.DeleteFlag == false).OrderBy(x => x.RegistrationNumber4)) {
                sheetView.Rows.Add(row, 1);
                sheetView.RowHeader.Columns[0].Label = (row + 1).ToString(); // Rowヘッダ
                sheetView.Rows[row].Height = 22; // Rowの高さ
                sheetView.Rows[row].Resizable = false; // RowのResizableを禁止
                sheetView.Cells[row, 0].Text = carMasterVo.RegistrationNumber4.PadLeft(8, '0');                                             // 車両コード
                sheetView.Cells[row, 1].Text = carMasterVo.RegistrationNumber;                                                              // 車両名
                // 設定グループ
                sheetView.Cells[row, 3].Text = "本社営業所";                                                                                 // 事業所
                sheetView.Cells[row, 4].Text = "本社";                                                                                      // 所属
                sheetView.Cells[row, 5].Text = "運転者共通";                                                                                 // デフォルト乗務員
                sheetView.Cells[row, 6].Text = carMasterVo.MaximumLoadCapacity.ToString();                                                  // 最大積載量
                sheetView.Cells[row, 7].Text = "標準設定";                                                                                   // 日報作成設定
                sheetView.Cells[row, 8].Value = true;
                sheetView.Cells[row, 9].Text = carMasterVo.RegistrationDate.ToString("yyyy/MM/dd");
                sheetView.Cells[row, 10].Text = "";
                row++;
            }

            this.SpreadList.SetViewportTopRow(0, _spreadListTopRow);                                                                        // 先頭行（列）インデックスをセット
            this.SpreadList.ResumeLayout();                                                                                                 // Spread 活性化
            this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = string.Concat(" ", row, " 件");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EstraList_FormClosing(object sender, FormClosingEventArgs e) {
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

        /// <summary>
        /// InitializeSheetView
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns>SheetView</returns>
        private SheetView InitializeSheetView(SheetView sheetView) {
            SpreadList.AllowDragDrop = false;                                                                                               // DrugDropを禁止する
            SpreadList.PaintSelectionHeader = false;                                                                                        // ヘッダの選択状態をしない
            SpreadList.TabStripPolicy = TabStripPolicy.Never;                                                                               // TabStripを表示しない
            SpreadList.TabStrip.DefaultSheetTab.Font = new Font("Yu Gothic UI", 9);
            sheetView.AlternatingRows.Count = 2;                                                                                            // 行スタイルを２行単位とします
            sheetView.AlternatingRows[0].BackColor = Color.WhiteSmoke;                                                                      // 1行目の背景色を設定します
            sheetView.AlternatingRows[1].BackColor = Color.White;                                                                           // 2行目の背景色を設定します
            sheetView.ColumnHeader.Rows[0].Height = 26;                                                                                     // Columnヘッダの高さ
            sheetView.GrayAreaBackColor = Color.White;
            sheetView.HorizontalGridLine = new GridLine(GridLineType.None);
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9);                                                              // 行ヘッダのFont
            sheetView.RowHeader.Columns[0].Width = 48;                                                                                      // 行ヘッダの幅を変更します
            sheetView.VerticalGridLine = new GridLine(GridLineType.Flat, Color.LightGray);
            sheetView.RemoveRows(0, sheetView.Rows.Count);
            return sheetView;
        }

        private class MJyoumuinVo {
            public string JyoumuinCode { get; set; }
            public string JyoumuinName { get; set; }
            public string JyoumuinNameKana { get; set; }
            public string JigyousyoCode { get; set; }
            public string SyozokuCode { get; set; }
            public string GetsujiSyukeiTaishou { get; set; }
            public string RoumuKanriTaishou { get; set; }
            public string SaiyouYmd { get; set; }
            public string TaishokuYmd { get; set; }
        }

        private class MSyaryoVo {
            public string SharyoCode { get; set; }                      // 車両コード
            public string SharyoName { get; set; }                      // 車両名
            public string SettingsGroup { get; set; }                   // 設定グループ
            public string JigyousyoCode { get; set; }                   // 事業所コード
            public string SyozokuCode { get; set; }                     // 所属コード
            public string DefaultStaffCode { get; set; }                // デフォルト担当者コード
            public string MaximumLoadCapacity { get; set; }             // 最大積載量
            public string Nippou { get; set; }                          // 日報作成設定
            public string GetsujiSyukeiTaishou { get; set; }            // 月次集計対象
            public string InsertDate { get; set; }                      // 登録年月日
            public string DeleteDate { get; set; }                      // 廃車年月日
        }
    }
}
