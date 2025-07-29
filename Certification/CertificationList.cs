using System.Drawing.Printing;

using Common;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace Certification {

    public partial class CertificationList : Form {
        private PrintDocument _printDocument = new();
        private DateUtility _dateUtility = new();
        private readonly Screen _screen;
        private readonly ScreenForm _screenForm = new();
        /*
         * Dao
         */
        private readonly StaffMasterDao _staffMasterDao;
        private readonly BelongsMasterDao _belongsMasterDao;
        private readonly LicenseMasterDao _licenseMasterDao;
        private readonly CertificationMasterDao _certificationMasterDao;
        private readonly CertificationFileDao _certificationFileDao;
        private readonly ToukanpoTrainingCardDao _toukanpoTrainingCardDao;
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;
        /*
         * Dictionary
         */
        private readonly Dictionary<int, string> _dictionaryBelongs = new();                    // コード基準

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public CertificationList(ConnectionVo connectionVo, Screen screen) {
            _screen = screen;
            /*
             * Dao
             */
            _staffMasterDao = new(connectionVo);
            _belongsMasterDao = new(connectionVo);
            _licenseMasterDao = new(connectionVo);
            _certificationMasterDao = new(connectionVo);
            _certificationFileDao = new(connectionVo);
            _toukanpoTrainingCardDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            /*
             * Dictionary
             */
            foreach (BelongsMasterVo belongsMasterVo in _belongsMasterDao.SelectAllBelongsMaster())
                _dictionaryBelongs.Add(belongsMasterVo.Code, belongsMasterVo.Name);
            /*
             * 
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
            this.MenuStripEx1.ChangeEnable(listString);
            /*
             * プリンターの一覧を取得後、通常使うプリンター名をセットする
             */
            foreach (string item in new PrintUtility().GetAllPrinterName()) {
                this.ComboBoxExPrinterName.Items.Add(item);
            }
            this.ComboBoxExPrinterName.Text = _printDocument.PrinterSettings.PrinterName;
            this.InitializeSheetView(SheetViewList);
            this.InitializeSheetViewList(SheetViewList);

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
            this.PutSheetViewList(SheetViewList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpreadList_CellDoubleClick(object sender, CellClickEventArgs e) {
            /*
             * CertificationCodeが101/102/103/104/238の場合は別処理をする
             */
            if (e.Column > 6 && e.Row > 3 && ((CertificationMasterVo)SheetViewList.Cells[1, e.Column].Tag).CertificationCode != 238) {
                /*
                 * TagからVoを取得
                 */
                StaffMasterVo staffMasterVo = (StaffMasterVo)SheetViewList.Cells[e.Row, 1].Tag;
                CertificationMasterVo certificationMasterVo = (CertificationMasterVo)SheetViewList.Cells[1, e.Column].Tag;
                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;
                /*
                 * Formを表示する
                 */
                CertificationDetail certificationDetail = new(_connectionVo, staffMasterVo.StaffCode, certificationMasterVo.CertificationCode);
                _screenForm.SetPosition(_screen, certificationDetail);
                certificationDetail.KeyPreview = true;
                certificationDetail.Show(this);

            } else {
                // 範囲外なので操作を中断する
                e.Cancel = true;
                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "ダブルクリックしたセルは範囲外です。";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        private void InitializeSheetViewList(SheetView sheetView) {
            this.SpreadList.SuspendLayout(); // Spread 非活性化
            /*
             * 初期値
             */
            sheetView.ColumnCount = 3;
            sheetView.RowCount = 4;
            /*
             * Columnを追加する
             */
            int columnCount = sheetView.Columns.Count;
            foreach (CertificationMasterVo certificationMasterVo in _certificationMasterDao.SelectAllCertificationMaster()) {
                sheetView.Columns.Add(columnCount, 1);
                sheetView.Columns[columnCount].Width = 25;
                /*
                 * 資格コード
                 */
                sheetView.Cells[0, columnCount].Font = new Font("游ゴシック", 8);
                sheetView.Cells[0, columnCount].Text = certificationMasterVo.CertificationCode.ToString("###");
                /*
                 * 資格名
                 * カスタムセルを使用している
                 */
                sheetView.Cells[1, columnCount].Tag = certificationMasterVo;
                //TextCellType textCellType = new();
                //textCellType.TextOrientation = TextOrientation.TextVertical;
                sheetView.Cells[1, columnCount].CellType = new VerticalTextCell();
                sheetView.Cells[1, columnCount].Font = new Font("メイリオ", 9);
                //sheetView.Cells[1, columnCount].VerticalAlignment = CellVerticalAlignment.Top;
                sheetView.Cells[1, columnCount].Text = certificationMasterVo.CertificationDisplayName;
                /*
                 * 取得計画人数
                 */
                sheetView.Cells[2, columnCount].Font = new Font("游ゴシック", 8);
                sheetView.Cells[2, columnCount].Text = certificationMasterVo.NumberOfAppointments.ToString("###");

                columnCount++;
            }

            /*
             * Rowを追加する
             */
            int rowCount = sheetView.RowCount;
            foreach (StaffMasterVo staffMasterVo in _staffMasterDao.SelectAllStaffMaster(new List<int> { 10, 11, 12, 14, 15, 22 },
                                                                                         new List<int> { 20, 22, 99 },
                                                                                         new List<int> { 10, 11, 20, 99 },
                                                                                         false).OrderBy(x => x.Belongs).ThenBy(x => x.NameKana)) {
                sheetView.Rows.Add(rowCount, 1);
                sheetView.Rows[rowCount].Height = 20;
                /*
                 * 所属
                 */
                sheetView.Cells[rowCount, 0].Font = new Font("游ゴシック", 8);
                sheetView.Cells[rowCount, 0].Text = _dictionaryBelongs[staffMasterVo.Belongs];
                /*
                 * 従事者名
                 */
                sheetView.Cells[rowCount, 1].Tag = staffMasterVo;
                sheetView.Cells[rowCount, 1].Font = new Font("游ゴシック", 9);
                sheetView.Cells[rowCount, 1].Text = staffMasterVo.DisplayName;
                /*
                 * 年齢
                 */
                sheetView.Cells[rowCount, 2].Text = string.Concat(_dateUtility.GetAge(staffMasterVo.BirthDate.Date), "歳");

                rowCount++;
            }
            /*
             * 固定行列を設定する
             */
            SheetViewList.FrozenRowCount = 4;
            SheetViewList.FrozenColumnCount = 3;

            this.SpreadList.ResumeLayout();                                         // Spread 活性化
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        private void PutSheetViewList(SheetView sheetView) {
            List<LicenseMasterVo> listLicenseMasterVo = _licenseMasterDao.SelectAllLicenseMaster();
            List<CertificationFileVo> listCertificationFileVo = _certificationFileDao.SelectAllCertificationFile();
            /*
             * Rowを追加する
             */
            for (int rowCount = 4; rowCount < sheetView.RowCount; rowCount++) {
                /*
                 * 免許区分
                 */
                LicenseMasterVo licenseMasterVo = listLicenseMasterVo.Find(x => x.StaffCode == ((StaffMasterVo)sheetView.Cells[rowCount, 1].Tag).StaffCode);
                if (licenseMasterVo is not null) {
                    // 大型
                    sheetView.Cells[rowCount, 3].ForeColor = Color.Red;
                    sheetView.Cells[rowCount, 3].Font = new Font("Yu Gothic UI", 12);
                    sheetView.Cells[rowCount, 3].HorizontalAlignment = CellHorizontalAlignment.Center;
                    sheetView.Cells[rowCount, 3].Text = licenseMasterVo.Large ? "〇" : "";
                    sheetView.Cells[rowCount, 3].VerticalAlignment = CellVerticalAlignment.Center;
                    // 中型
                    sheetView.Cells[rowCount, 4].ForeColor = Color.Red;
                    sheetView.Cells[rowCount, 4].Font = new Font("Yu Gothic UI", 12);
                    sheetView.Cells[rowCount, 4].HorizontalAlignment = CellHorizontalAlignment.Center;
                    sheetView.Cells[rowCount, 4].Text = licenseMasterVo.Medium ? "〇" : "";
                    sheetView.Cells[rowCount, 4].VerticalAlignment = CellVerticalAlignment.Center;
                    // 準中型
                    sheetView.Cells[rowCount, 5].ForeColor = Color.Red;
                    sheetView.Cells[rowCount, 5].Font = new Font("Yu Gothic UI", 12);
                    sheetView.Cells[rowCount, 5].HorizontalAlignment = CellHorizontalAlignment.Center;
                    sheetView.Cells[rowCount, 5].Text = licenseMasterVo.QuasiMedium ? "〇" : "";
                    sheetView.Cells[rowCount, 5].VerticalAlignment = CellVerticalAlignment.Center;
                    // 普通
                    sheetView.Cells[rowCount, 6].ForeColor = Color.Red;
                    sheetView.Cells[rowCount, 6].Font = new Font("Yu Gothic UI", 12);
                    sheetView.Cells[rowCount, 6].HorizontalAlignment = CellHorizontalAlignment.Center;
                    sheetView.Cells[rowCount, 6].Text = licenseMasterVo.Ordinary ? "〇" : "";
                    sheetView.Cells[rowCount, 6].VerticalAlignment = CellVerticalAlignment.Center;
                }
                /*
                 * 資格区分
                 */
                for (int columnCount = 7; columnCount < sheetView.ColumnCount; columnCount++) {
                    /*
                     * 238:作業員業務は、東環保カードの有無で判断する
                     */
                    if (((CertificationMasterVo)sheetView.Cells[1, columnCount].Tag).CertificationCode == 238) {
                        /*
                         * 東環保処理
                         */
                        if (_toukanpoTrainingCardDao.ExistenceToukanpoTrainingCardMaster(((StaffMasterVo)sheetView.Cells[rowCount, 1].Tag).StaffCode)) {
                            sheetView.Cells[rowCount, columnCount].Font = new Font("Yu Gothic UI", 12);
                            sheetView.Cells[rowCount, columnCount].ForeColor = Color.Blue;
                            sheetView.Cells[rowCount, columnCount].HorizontalAlignment = CellHorizontalAlignment.Center;
                            sheetView.Cells[rowCount, columnCount].Text = "〇";
                            sheetView.Cells[rowCount, columnCount].VerticalAlignment = CellVerticalAlignment.Center;
                        } else {

                        }
                    } else {
                        /*
                         * その他処理
                         */
                        CertificationFileVo certificationFileVo = listCertificationFileVo.Find(x => x.StaffCode == ((StaffMasterVo)sheetView.Cells[rowCount, 1].Tag).StaffCode &&
                                                                                                    x.CertificationCode == ((CertificationMasterVo)sheetView.Cells[1, columnCount].Tag).CertificationCode);
                        if (certificationFileVo is not null) {
                            sheetView.Cells[rowCount, columnCount].Font = new Font("Yu Gothic UI", 12);
                            sheetView.Cells[rowCount, columnCount].ForeColor = Color.Blue;
                            sheetView.Cells[rowCount, columnCount].HorizontalAlignment = CellHorizontalAlignment.Center;
                            if (certificationFileVo.Picture1Flag || certificationFileVo.Picture2Flag) {
                                sheetView.Cells[rowCount, columnCount].Text = "〇";
                            } else {
                                sheetView.Cells[rowCount, columnCount].Text = "×";
                            }

                            sheetView.Cells[rowCount, columnCount].VerticalAlignment = CellVerticalAlignment.Center;
                        }
                    }
                }
                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(rowCount - 4, "名");
            }

            /*
             * 資格取得済人数を算出する
             */
            int checkCount;
            for (int col = 3; col < SheetViewList.ColumnCount; col++) {
                checkCount = 0;
                for (int row = 4; row < SheetViewList.RowCount; row++) {
                    if (sheetView.Cells[row, col].Text != string.Empty)
                        checkCount++;
                }
                sheetView.Cells[3, col].Font = new Font("Yu Gothic UI", 8);
                sheetView.Cells[3, col].Value = checkCount;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                /*
                 * B5で印刷する
                 */
                case "ToolStripMenuItemPrintA4":
                    // Eventを登録
                    _printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);
                    // 出力先プリンタを指定します。
                    _printDocument.PrinterSettings.PrinterName = this.ComboBoxExPrinterName.Text;
                    // 用紙の向きを設定(横：true、縦：false)
                    _printDocument.DefaultPageSettings.Landscape = false;
                    /*
                     * プリンタがサポートしている用紙サイズを調べる
                     */
                    foreach (PaperSize paperSize in _printDocument.PrinterSettings.PaperSizes) {
                        // B5用紙に設定する
                        if (paperSize.Kind == PaperKind.A4) {
                            _printDocument.DefaultPageSettings.PaperSize = paperSize;
                            break;
                        }
                    }
                    // 印刷部数を指定します。
                    _printDocument.PrinterSettings.Copies = 1;
                    // 片面印刷に設定します。
                    _printDocument.PrinterSettings.Duplex = Duplex.Default;
                    // カラー印刷に設定します。
                    _printDocument.PrinterSettings.DefaultPageSettings.Color = true;
                    // 印刷する
                    _printDocument.Print();
                    break;
                /*
                 * アプリケーションを終了する
                 */
                case "ToolStripMenuItemExit":
                    Close();
                    break;
            }
        }

        /// <summary>
        /// InitializeSheetView
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns>SheetView</returns>
        private SheetView InitializeSheetView(SheetView sheetView) {
            this.SpreadList.AllowDragDrop = false;                                              // DrugDropを禁止する
            this.SpreadList.TabStripPolicy = TabStripPolicy.Never;                              // シートタブを非表示にする
            SpreadList.PaintSelectionHeader = false;                                            // ヘッダの選択状態をしない
            SpreadList.TabStrip.DefaultSheetTab.Font = new Font("Yu Gothic UI", 9);
            sheetView.ColumnHeader.Rows[0].Height = 26;                                         // Columnヘッダの高さ
            sheetView.GrayAreaBackColor = Color.White;
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9);                  // 行ヘッダのFont
            sheetView.RowHeader.Columns[0].Width = 48;                                          // 行ヘッダの幅を変更します
            return sheetView;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e) {
            // 印刷ページ（1ページ目）の描画を行う
            Rectangle rectangle = new(e.PageBounds.X, e.PageBounds.Y, e.PageBounds.Width, e.PageBounds.Height);
            // e.Graphicsへ出力(page パラメータは、０からではなく１から始まります)
            this.SpreadList.OwnerPrintDraw(e.Graphics, rectangle, 0, 1);
            // 印刷終了を指定
            e.HasMorePages = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CertificationList_FormClosing(object sender, FormClosingEventArgs e) {

        }

        /// <summary>
        /// カスタムセルクラス
        /// </summary>
        private class VerticalTextCell : FarPoint.Win.Spread.CellType.TextCellType {
            public override void PaintCell(Graphics g, Rectangle r, FarPoint.Win.Spread.Appearance appearance, object value, bool isSelected, bool isLocked, float zoomFactor) {
                Brush backColorBrush = new SolidBrush(appearance.BackColor);
                Brush foreColorBrush = new SolidBrush(appearance.ForeColor);
                StringFormat S = new StringFormat();
                S.FormatFlags = StringFormatFlags.DirectionVertical;

                g.FillRectangle(backColorBrush, r);
                g.DrawString((string)value, appearance.Font, foreColorBrush, r, S);

                backColorBrush.Dispose();
                foreColorBrush.Dispose();
            }
        }
    }
}
