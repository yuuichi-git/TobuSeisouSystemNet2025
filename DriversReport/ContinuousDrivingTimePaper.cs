/*
 * 2026-05-05
 */
using System.Drawing.Printing;

using CcControl;

using Common;

using Dao;

using FarPoint.Win.Spread;

using Vo;

using static CcControl.CcComboBoxStaffMaster;

namespace DriversReport {
    public partial class ContinuousDrivingTimePaper : Form {
        private const int _colOperationDate = 0;
        private const int _colStaffDisplayName = 1;
        private const int _colJobForm = 2;
        private const int _colSetName = 3;
        private const int _colCarRegistrationNumber = 4;
        private const int _colFirstLollCallHms = 5;
        private const int _colLastLollCallHms = 6;
        private const int _colContinuosDrivingTime = 7;
        private const int _colRemarks = 8;

        private readonly DateUtility _dateUtility = new();
        /*
         * Dao
         */
        private ContinuousDrivingTimePaperDao _continuousDrivingTimePaperDao;
        private StaffMasterDao _staffMasterDao;
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;
        private StaffMasterVo _staffMasterVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public ContinuousDrivingTimePaper(ConnectionVo connectionVo, Screen screen) {
            /*
             * Dao
             */
            _continuousDrivingTimePaperDao = new ContinuousDrivingTimePaperDao(connectionVo);
            _staffMasterDao = new StaffMasterDao(connectionVo);
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
            this.CcMenuStrip1.ChangeEnable(listString);
            /*
             * プリンターの一覧を取得後、通常使うプリンター名をセットする
             */
            foreach (string item in new PrintUtility().GetAllPrinterName())
                this.ComboBoxExPrinterName.Items.Add(item);
            this.ComboBoxExPrinterName.Text = _printDocument.PrinterSettings.PrinterName;

            CcDateTimePickerOperationStartDate.Value = _dateUtility.GetBeginOfMonth(DateTime.Now);
            CcDateTimePickerOperationEndDate.Value = _dateUtility.GetEndOfMonth(DateTime.Now);
            InitializeCcComboBoxStaffMaster();
            InitializeSheetViewList(SheetViewList);
            CcStatusStrip1.ToolStripStatusLabelDetail.Text = string.Empty;
            /*
             * Eventを登録する
             */
            this.CcMenuStrip1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        private void CcButtonUpdate_Click(object sender, EventArgs e) {
            if (CcComboBoxStaffMaster1.SelectedItem == null) {
                CcStatusStrip1.ToolStripStatusLabelDetail.Text = " 対象のドライバーを選択してください。";
                return;
            }
            SetControlsValue(this.SheetViewList, _continuousDrivingTimePaperDao.SelectContinuousDrivingTimePaperVo(CcDateTimePickerOperationStartDate.Value,
                                                                                                                   CcDateTimePickerOperationEndDate.Value,
                                                                                                                   ((CcComboBoxStaffMasterVo)CcComboBoxStaffMaster1.SelectedItem).StaffMasterVo.StaffCode));
        }

        private PrintDocument _printDocument = new();
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
                    _printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);                         // Eventを登録
                    _printDocument.PrinterSettings.PrinterName = this.ComboBoxExPrinterName.Text;                           // 出力先プリンタを指定します。
                    _printDocument.DefaultPageSettings.Landscape = false;                                                   // 用紙の向きを設定(横：true、縦：false)
                    /*
                     * プリンタがサポートしている用紙サイズを調べる
                     */
                    foreach (PaperSize paperSize in _printDocument.PrinterSettings.PaperSizes) {
                        if (paperSize.Kind == PaperKind.A4) {                                                               // A4用紙に設定する
                            _printDocument.DefaultPageSettings.PaperSize = paperSize;
                            break;
                        }
                    }
                    _printDocument.PrinterSettings.Copies = 1;                                                              // 印刷部数を指定します。
                    _printDocument.PrinterSettings.Duplex = Duplex.Default;                                                 // 片面印刷に設定します。
                    _printDocument.PrinterSettings.DefaultPageSettings.Color = true;                                        // カラー印刷に設定します。
                    _printDocument.Print();                                                                                 // 印刷する
                    break;
                case "ToolStripMenuItemExit":                                                                               // アプリケーションを終了する
                    this.Close();
                    break;
            }
        }

        int spreadListTopRow1 = 0;
        private void SetControlsValue(SheetView sheetView, List<ContinuousDrivingTimePaperVo> listContinuousDrivingTimePaperVo) {
            SpreadList.SuspendLayout();                                                                                                                                                 // 非活性化
            spreadListTopRow1 = SpreadList.GetViewportTopRow(0);                                                                                                                        // 先頭行（列）インデックスを取得
            if (sheetView.Rows.Count > 0)                                                                                                                                               // Rowを削除する
                sheetView.RemoveRows(0, sheetView.Rows.Count);

            int i = 0;
            foreach (ContinuousDrivingTimePaperVo continuousDrivingTimePaperVo in listContinuousDrivingTimePaperVo) {
                sheetView.Rows.Add(i, 1);
                sheetView.RowHeader.Columns[0].Label = (i + 1).ToString();                                                                                                              // Rowヘッダ
                sheetView.Rows[i].Height = 22;                                                                                                                                          // Rowの高さ
                sheetView.Rows[i].Resizable = false;                                                                                                                                    // RowのResizableを禁止

                sheetView.Cells[i, _colOperationDate].Value = continuousDrivingTimePaperVo.OperationDate.ToString("yyyy/MM/dd");
                sheetView.Cells[i, _colStaffDisplayName].Value = continuousDrivingTimePaperVo.StaffDisplayName.ToString();
                sheetView.Cells[i, _colJobForm].Value = continuousDrivingTimePaperVo.JobForm.ToString();
                sheetView.Cells[i, _colSetName].Value = continuousDrivingTimePaperVo.SetName.ToString();
                sheetView.Cells[i, _colCarRegistrationNumber].Value = continuousDrivingTimePaperVo.CarRegistrationNumber.ToString();
                sheetView.Cells[i, _colFirstLollCallHms].Value = continuousDrivingTimePaperVo.FirstLollCallHms.ToString();
                sheetView.Cells[i, _colLastLollCallHms].Value = continuousDrivingTimePaperVo.LastLollCallHms.ToString();
                sheetView.Cells[i, _colContinuosDrivingTime].Value = continuousDrivingTimePaperVo.ContinuosDrivingTime;
                sheetView.Cells[i, _colRemarks].Value = continuousDrivingTimePaperVo.Remarks;

                i++;
            }
            SpreadList.SetViewportTopRow(0, spreadListTopRow1);                                                                                                                         // 先頭行（列）インデックスをセット

            SpreadList.ResumeLayout();                                                                                                                                                  // 活性化
            CcStatusStrip1.ToolStripStatusLabelDetail.Text = string.Concat(" ", i, " 件");
        }

        /// <summary>
        /// InitializeSheetViewList
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns></returns>
        private SheetView InitializeSheetViewList(SheetView sheetView) {
            this.SpreadList.AllowDragDrop = false;                                                      // DrugDropを禁止する
            this.SpreadList.PaintSelectionHeader = false;                                               // ヘッダの選択状態をしない
            this.SpreadList.TabStripPolicy = TabStripPolicy.Never;                                      // シートタブを表示
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
        /// InitializeCcComboBoxStaffMaster
        /// </summary>
        private void InitializeCcComboBoxStaffMaster() {
            CcComboBoxStaffMaster1.Items.Clear();
            foreach (StaffMasterVo staffMasterVo in _staffMasterDao.SelectAllStaffMaster(null, null, null, false)) {
                CcComboBoxStaffMaster1.Items.Add(new CcComboBoxStaffMasterVo(staffMasterVo.DisplayName, staffMasterVo));
            }
            CcComboBoxStaffMaster1.DisplayMember = "DisplayName";
        }

        /// <summary>
        /// CcDateTimePicker_ValueChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CcDateTimePicker_ValueChanged(object sender, EventArgs e) {
            switch (((CcDateTime)sender).Name) {
                case "CcDateTimePickerOperationStartDate":
                    if (((CcDateTime)sender).Value > CcDateTimePickerOperationEndDate.Value) {
                        CcDateTimePickerOperationEndDate.Value = ((CcDateTime)sender).Value;
                    }
                    break;
                case "CcDateTimePickerOperationEndDate":
                    if (CcDateTimePickerOperationStartDate.Value > ((CcDateTime)sender).Value) {
                        CcDateTimePickerOperationStartDate.Value = ((CcDateTime)sender).Value;
                    }
                    break;
                default:
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
        /// ContinuousDrivingTimePaper_FormClosing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContinuousDrivingTimePaper_FormClosing(object sender, FormClosingEventArgs e) {
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
