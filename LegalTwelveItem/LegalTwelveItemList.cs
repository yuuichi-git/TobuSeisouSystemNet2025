/*
 * 2025-05-14
 */
using System.Drawing.Printing;

using Common;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace LegalTwelveItem {
    public partial class LegalTwelveItemList : Form {
        private readonly DateTime _defaultDatetime = new(1900, 01, 01);
        private readonly DateUtility _dateUtility = new();
        private readonly Screen _screen;
        private readonly ScreenForm _screenForm = new();
        private PrintDocument _printDocument = new();
        /*
         * Dao
         */
        private readonly LegalTwelveItemDao _legalTwelveItemDao;
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;
        /*
         * Columns
         */
        private const int _colBelongsName = 0;
        private const int _colJobFormName = 1;
        private const int _colOccupation = 2;
        private const int _colName = 3;
        private const int _colEmploymentDate = 4;
        private const int _colStudentsFlag01 = 5;
        private const int _colStudentsFlag02 = 6;
        private const int _colStudentsFlag03 = 7;
        private const int _colStudentsFlag04 = 8;
        private const int _colStudentsFlag05 = 9;
        private const int _colStudentsFlag06 = 10;
        private const int _colStudentsFlag07 = 11;
        private const int _colStudentsFlag08 = 12;
        private const int _colStudentsFlag09 = 13;
        private const int _colStudentsFlag10 = 14;
        private const int _colStudentsFlag11 = 15;
        private const int _colStudentsFlag12 = 16;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public LegalTwelveItemList(ConnectionVo connectionVo, Screen screen) {
            _screen = screen;
            /*
             * Dao
             */
            _legalTwelveItemDao = new(connectionVo);
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
                "ToolStripMenuItemPrint",
                "ToolStripMenuItemPrintA4",
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);
            // 対象年度
            this.NumericUpDownExFiscalYear.Value = _dateUtility.GetFiscalYear();
            /*
             * プリンターの一覧を取得後、通常使うプリンター名をセットする
             */
            foreach (string item in new PrintUtility().GetAllPrinterName()) {
                this.ComboBoxExPrinterName.Items.Add(item);
            }
            this.ComboBoxExPrinterName.Text = _printDocument.PrinterSettings.PrinterName;
            /*
             * InitializeSpread
             */
            this.InitializeSheetViewList(this.SheetViewList);
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
            this.PutSheetViewList(_legalTwelveItemDao.SelectLegalTwelveItemListVo(_dateUtility.GetFiscalYearStartDate((int)NumericUpDownExFiscalYear.Value), _dateUtility.GetFiscalYearEndDate((int)NumericUpDownExFiscalYear.Value)));
        }

        int spreadListTopRow = 0;
        /// <summary>
        /// PutSheetViewList
        /// </summary>
        /// <param name="listLegalTwelveItemVo"></param>
        private void PutSheetViewList(List<LegalTwelveItemListVo> listLegalTwelveItemVo) {
            /*
             * SheetViewListの準備
             */
            this.SpreadList.SuspendLayout();                                                                                    // Spread 非活性化
            this.spreadListTopRow = SpreadList.GetViewportTopRow(0);                                                            // 先頭行（列）インデックスを取得
            if (this.SheetViewList.Rows.Count > 0)                                                                              // Rowを削除する
                this.SheetViewList.RemoveRows(0, this.SheetViewList.Rows.Count);
            /*
             * SheetViewListへ表示
             */
            int i = 0;
            foreach (LegalTwelveItemListVo legalTwelveItemListVo in listLegalTwelveItemVo) {
                this.SheetViewList.Rows.Add(i, 1);
                this.SheetViewList.RowHeader.Columns[0].Label = (i + 1).ToString();                                             // Rowヘッダ
                this.SheetViewList.Rows[i].ForeColor = legalTwelveItemListVo.JobForm == 11 ? Color.Blue : Color.Black;              // 手帳のレコードのForeColorをセット
                this.SheetViewList.Rows[i].Tag = legalTwelveItemListVo;                                                             // H_LegalTwelveItemVoを退避
                this.SheetViewList.Cells[i, _colBelongsName].Text = legalTwelveItemListVo.BelongsName;
                this.SheetViewList.Cells[i, _colJobFormName].Text = legalTwelveItemListVo.JobFormName;
                this.SheetViewList.Cells[i, _colOccupation].Text = legalTwelveItemListVo.OccupationName;
                this.SheetViewList.Cells[i, _colName].Text = legalTwelveItemListVo.StaffName;
                this.SheetViewList.Cells[i, _colEmploymentDate].Text = legalTwelveItemListVo.EmploymentDate != _defaultDatetime ? legalTwelveItemListVo.EmploymentDate.ToString("yyyy/MM/dd") : string.Empty;
                this.SheetViewList.Cells[i, _colStudentsFlag01].Text = legalTwelveItemListVo.Students01Flag ? "〇" : string.Empty;
                this.SheetViewList.Cells[i, _colStudentsFlag02].Text = legalTwelveItemListVo.Students02Flag ? "〇" : string.Empty;
                this.SheetViewList.Cells[i, _colStudentsFlag03].Text = legalTwelveItemListVo.Students03Flag ? "〇" : string.Empty;
                this.SheetViewList.Cells[i, _colStudentsFlag04].Text = legalTwelveItemListVo.Students04Flag ? "〇" : string.Empty;
                this.SheetViewList.Cells[i, _colStudentsFlag05].Text = legalTwelveItemListVo.Students05Flag ? "〇" : string.Empty;
                this.SheetViewList.Cells[i, _colStudentsFlag06].Text = legalTwelveItemListVo.Students06Flag ? "〇" : string.Empty;
                this.SheetViewList.Cells[i, _colStudentsFlag07].Text = legalTwelveItemListVo.Students07Flag ? "〇" : string.Empty;
                this.SheetViewList.Cells[i, _colStudentsFlag08].Text = legalTwelveItemListVo.Students08Flag ? "〇" : string.Empty;
                this.SheetViewList.Cells[i, _colStudentsFlag09].Text = legalTwelveItemListVo.Students09Flag ? "〇" : string.Empty;
                this.SheetViewList.Cells[i, _colStudentsFlag10].Text = legalTwelveItemListVo.Students10Flag ? "〇" : string.Empty;
                this.SheetViewList.Cells[i, _colStudentsFlag11].Text = legalTwelveItemListVo.Students11Flag ? "〇" : string.Empty;
                this.SheetViewList.Cells[i, _colStudentsFlag12].Text = legalTwelveItemListVo.Students12Flag ? "〇" : string.Empty;
                i++;
            }
            this.SpreadList.SetViewportTopRow(0, spreadListTopRow);                                                             // 先頭行（列）インデックスをセット
            this.SpreadList.ResumeLayout();                                                                                     // Spread 活性化
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(" ", i, " 件");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpreadList_CellDoubleClick(object sender, CellClickEventArgs e) {
            /*
             * ヘッダーのDoubleClickを回避
             */
            if (e.ColumnHeader)
                return;
            /*
             * Detailウインドウを表示
             */
            LegalTwelveItemDetail legalTwelveItemDetail = new(_connectionVo, _screen, (int)this.NumericUpDownExFiscalYear.Value, ((LegalTwelveItemListVo)SheetViewList.Rows[e.Row].Tag).StaffCode);
            _screenForm.SetPosition(_screen, legalTwelveItemDetail);
            legalTwelveItemDetail.KeyPreview = true;
            legalTwelveItemDetail.Show(this);
            return;
        }

        /// <summary>
        /// InitializeSheetViewList
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns></returns>
        private SheetView InitializeSheetViewList(SheetView sheetView) {
            this.SpreadList.AllowDragDrop = false; // DrugDropを禁止する
            this.SpreadList.PaintSelectionHeader = false; // ヘッダの選択状態をしない
            this.SpreadList.TabStrip.DefaultSheetTab.Font = new Font("Yu Gothic UI", 9);
            this.SpreadList.TabStripPolicy = TabStripPolicy.Never; // シートタブを非表示
            sheetView.AlternatingRows.Count = 2; // 行スタイルを２行単位とします
            sheetView.AlternatingRows[0].BackColor = Color.WhiteSmoke; // 1行目の背景色を設定します
            sheetView.AlternatingRows[1].BackColor = Color.White; // 2行目の背景色を設定します
            sheetView.ColumnHeader.Rows[0].Height = 22; // Columnヘッダの高さ
            sheetView.GrayAreaBackColor = Color.White;
            sheetView.HorizontalGridLine = new GridLine(GridLineType.None);
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9); // 行ヘッダのFont
            sheetView.RowHeader.Columns[0].Width = 48; // 行ヘッダの幅を変更します
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
                /*
                 * A4で印刷する
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
        private void EmploymentAgreementList_FormClosing(object sender, FormClosingEventArgs e) {
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
