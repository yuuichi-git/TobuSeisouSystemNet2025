/*
 * 2025-11-01
 */
using System.Drawing.Printing;

using Common;

using ControlEx;

using Dao;

using FarPoint.Excel;
using FarPoint.Win.Spread;

using Vo;

namespace Car {
    public partial class CarWorkingDays : Form {
        private PrintDocument _printDocument = new();
        /*
         * SPREADのColumnの番号
         */
        private const int colOperationDate = 0;             // 稼働日
        private const int colDoorNumber = 1;                // ドアNo
        private const int colRegistrationNumber = 2;        // 車両登録番号
        private const int colClassificationName = 3;        // 分類名
        private const int colDisguiseKind2 = 4;             // 車種2
        private const int colSetName = 5;                   // 配車先名
        private const int colStaffName = 6;                 // 運転者名
        private const int colRemarks = 7;                   // 備考
        /*
         * Dao
         */
        private readonly CarWorkingDaysDao _carWorkingDaysDao;
        private readonly CarMasterDao _carMasterDao;
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;
        private Screen _screen;
        private List<CarMasterVo> _listCarMasterVo;
        /*
         * インスタンス作成
         */
        private readonly DateUtility _dateUtility = new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public CarWorkingDays(ConnectionVo connectionVo, Screen screen) {
            /*
             * Dao
             */
            _carWorkingDaysDao = new CarWorkingDaysDao(connectionVo);
            _carMasterDao = new CarMasterDao(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            _screen = screen;
            _listCarMasterVo = _carMasterDao.SelectAllCarMaster().Where(x => x.DeleteFlag == false).OrderBy(x => x.DoorNumber).ThenBy(x => x.RegistrationNumber4).ToList();
            /*
             * InitializeControl
             */
            InitializeComponent();
            this.InitializeComboBoxExCarMaster();                                                                   // ComboBoxExCarMasterの初期化
            this.ComboBoxExCarMaster1.DisplayClear();                                                               // ComboBoxExCarMasterの表示クリア
            /*
             * MenuStrip
             */
            List<string> listString = new() {
                "ToolStripMenuItemFile",
                "ToolStripMenuItemExit",
                "ToolStripMenuItemExport",
                "ToolStripMenuItemExportExcel",
                "ToolStripMenuItemPrint",
                "ToolStripMenuItemPrintA4",
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);
            /*
             * 配車日を設定
             */
            this.DateTimePickerExOperationDate1.SetValue(_dateUtility.GetBeginOfMonth(DateTime.Now));
            this.DateTimePickerExOperationDate2.SetValue(_dateUtility.GetEndOfMonth(DateTime.Now));

            this.InitializeSheetView(this.SheetViewList);
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "Initialize Success";
            /*
             * Eventを登録する
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        int _spreadListTopRow = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExUpdate_Click(object sender, EventArgs e) {
            if (this.ComboBoxExCarMaster1.SelectedIndex != -1) {
                this.SetSheetViewList(this.SheetViewList);
            } else {
                MessageBox.Show("値が選択されていません", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        private void SetSheetViewList(SheetView sheetView) {
            List<CarWorkingDaysVo> _listCarWorkingDaysVo = new();
            _listCarWorkingDaysVo = _carWorkingDaysDao.SelectCarWorkingDaysVo(DateTimePickerExOperationDate1.Value,
                                                                              DateTimePickerExOperationDate2.Value,
                                                                              ((CarMasterVo)ComboBoxExCarMaster1.SelectedValue).CarCode);
            this.SpreadList.SuspendLayout();                                                                                                                                            // 非活性化
            _spreadListTopRow = this.SpreadList.GetViewportTopRow(0);                                                                                                                   // 先頭行（列）インデックスを取得
            if (sheetView.Rows.Count > 0)                                                                                                                                               // Rowを削除する
                sheetView.RemoveRows(0, sheetView.Rows.Count);

            int i = 0;
            foreach (CarWorkingDaysVo carWorkingDaysVo in _listCarWorkingDaysVo.OrderBy(x => x.OperationDate)) {
                sheetView.Rows.Add(i, 1);
                sheetView.RowHeader.Columns[0].Label = (i + 1).ToString();                                                                                                              // Rowヘッダ
                sheetView.Rows[i].Height = 22;                                                                                                                                          // Rowの高さ
                sheetView.Rows[i].Resizable = false;                                                                                                                                    // RowのResizableを禁止

                sheetView.Cells[i, colOperationDate].Value = carWorkingDaysVo.OperationDate.ToString("yyyy/MM/dd(ddd)");
                sheetView.Cells[i, colDoorNumber].Text = carWorkingDaysVo.DoorNumber.ToString();
                sheetView.Cells[i, colRegistrationNumber].Text = carWorkingDaysVo.RegistrationNumber.ToString();
                sheetView.Cells[i, colClassificationName].Text = carWorkingDaysVo.ClassificationName.ToString();
                sheetView.Cells[i, colDisguiseKind2].Text = carWorkingDaysVo.DisguiseKind2.ToString();
                sheetView.Cells[i, colSetName].Text = carWorkingDaysVo.SetName.ToString();
                sheetView.Cells[i, colStaffName].Text = carWorkingDaysVo.StaffName.ToString();
                sheetView.Cells[i, colRemarks].Text = carWorkingDaysVo.Remarks.Length > 0 ? carWorkingDaysVo.Remarks.ToString() : "-";
                sheetView.Rows[i].Tag = carWorkingDaysVo;
                i++;
            }
            this.SpreadList.SetViewportTopRow(0, _spreadListTopRow);                                                                                                                    // 先頭行（列）インデックスをセット

            this.SpreadList.ResumeLayout();                                                                                                                                             // 活性化
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(" ", i, " 件");
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeComboBoxExCarMaster() {
            this.ComboBoxExCarMaster1.SetItems(_listCarMasterVo, true);
        }

        /// <summary>
        /// InitializeSheetView
        /// </summary>
        /// <param name="sheetView"></param>
        private SheetView InitializeSheetView(SheetView sheetView) {
            this.SpreadList.AllowDragDrop = false;                                                      // ドラッグ＆ドロップを無効にする
            this.SpreadList.PaintSelectionHeader = false;                                               // 選択ヘッダの描画を無効にする
            this.SpreadList.TabStrip.DefaultSheetTab.Font = new Font("Yu Gothic UI", 9);
            this.SpreadList.TabStripPolicy = TabStripPolicy.Never;                                      // タブを非表示にする
            sheetView.AlternatingRows.Count = 2;                                                        // 交互に色をつける行数を設定します
            sheetView.AlternatingRows[0].BackColor = Color.WhiteSmoke;                                  // 1行目の背景色を設定します
            sheetView.AlternatingRows[1].BackColor = Color.White;                                       // 2行目の背景色を設定します
            sheetView.ColumnHeader.Rows[0].Height = 26;                                                 // ヘッダの高さ
            sheetView.GrayAreaBackColor = Color.White;
            sheetView.HorizontalGridLine = new GridLine(GridLineType.None);
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9);                          // 行ヘッダのフォント
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
                 * Excel(xlsx)形式でエクスポートする
                 */
                case "ToolStripMenuItemExportExcel":
                    //xlsx形式ファイルをエクスポートします
                    string fileName = string.Concat("車両別　稼働実績表", DateTime.Now.ToString("MM月dd日"), "作成");
                    this.SpreadList.SaveExcel(new Directry().GetExcelDesktopPassXlsx(fileName), ExcelSaveFlags.UseOOXMLFormat);
                    MessageBox.Show("デスクトップへエクスポートしました", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                /*
                 * A4で印刷する
                 */
                case "ToolStripMenuItemPrintA4":
                    // Eventを登録
                    _printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);
                    //// 出力先プリンタを指定します。
                    //_printDocument.PrinterSettings.PrinterName = _printDocument.PrinterSettings.PrinterName;
                    // 用紙の向きを設定(横：true、縦：false)
                    _printDocument.DefaultPageSettings.Landscape = false;
                    /*
                     * プリンタがサポートしている用紙サイズを調べる
                     */
                    foreach (PaperSize paperSize in _printDocument.PrinterSettings.PaperSizes) {
                        // A4用紙に設定する
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
                 * 
                 */
                case "ToolStripMenuItemExit":
                    this.Close();
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
        private void DateTimePickerEx1_ValueChanged(object sender, EventArgs e) {
            if (((CcDateTime)sender).Value > this.DateTimePickerExOperationDate2.GetValue()) {
                this.DateTimePickerExOperationDate2.SetValueJp(_dateUtility.GetEndOfMonth(((CcDateTime)sender).GetValue()));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimePickerEx2_ValueChanged(object sender, EventArgs e) {
            if (((CcDateTime)sender).Value < this.DateTimePickerExOperationDate1.GetValue()) {
                this.DateTimePickerExOperationDate1.SetValueJp(_dateUtility.GetBeginOfMonth(((CcDateTime)sender).GetValue()));
            }
        }

        /// <summary>
        /// CarWorkingDays_FormClosing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarWorkingDays_FormClosing(object sender, FormClosingEventArgs e) {
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