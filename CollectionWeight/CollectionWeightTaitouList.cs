/*
 * 2025-08-05
 */
using System.Drawing.Printing;

using Common;

using ControlEx;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace Collection {
    public partial class CollectionWeightTaitouList : Form {
        private readonly DateUtility _dateUtility = new();
        private PrintDocument _printDocument = new();
        /*
         * Dao
         */
        private readonly CollectionTaitouDao _collectionTaitouDao;
        /*
         * Vo
         */
        private List<CollectionWeightTaitouVo> _listCollectionWeightTaitouVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public CollectionWeightTaitouList(ConnectionVo connectionVo, Screen screen) {
            /*
             * Dao
             */
            _collectionTaitouDao = new(connectionVo);
            /*
             * Vo
             */
            _listCollectionWeightTaitouVo = new();

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
            this.DateTimePickerEx1.Value = _dateUtility.GetBeginOfMonth(DateTime.Now);
            this.DateTimePickerEx2.Value = _dateUtility.GetEndOfMonth(DateTime.Now);
            this.InitializeSheetView(this.SheetViewList);
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "Initialize Success";
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
            try {
                _listCollectionWeightTaitouVo = _collectionTaitouDao.SelectCollectionWeightTaitou(this.DateTimePickerEx1.GetValue(), this.DateTimePickerEx2.GetValue());
                this.PutSheetViewList(_listCollectionWeightTaitouVo);
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listCollectionWeightTaitouVo"></param>
        private void PutSheetViewList(List<CollectionWeightTaitouVo> listCollectionWeightTaitouVo) {
            // Spread 非活性化
            this.SpreadList.SuspendLayout();
            // Rowを削除する
            SheetViewList.ClearRange(1, 0, 31, 9, true);

            int rowNumber = 1;
            for (int day = 1; day <= new DateUtility().GetDaysInMonth(DateTimePickerEx2.Value); day++) {
                /*
                 * レコードを抽出する
                 */
                CollectionWeightTaitouVo collectionWeightTaitouVo = listCollectionWeightTaitouVo.Find(x => x.OperationDate == new DateTime(this.DateTimePickerEx1.Value.Year, this.DateTimePickerEx1.Value.Month, day));
                /*
                 * 出力する
                 */
                DateTime date = new(this.DateTimePickerEx1.Value.Year, this.DateTimePickerEx1.Value.Month, day);
                this.SheetViewList.Cells[rowNumber, 0].Value = date;
                this.SheetViewList.Cells[rowNumber, 1].Text = date.ToString("ddd");
                switch (date.ToString("ddd")) {
                    case "月":
                    case "火":
                    case "水":
                    case "木":
                    case "金":
                        this.SheetViewList.Cells[rowNumber, 1].ForeColor = Color.Black;
                        break;
                    case "土":
                        this.SheetViewList.Cells[rowNumber, 1].ForeColor = Color.Blue;
                        break;
                    case "日":
                        this.SheetViewList.Cells[rowNumber, 1].ForeColor = Color.Red;
                        break;
                }
                if (collectionWeightTaitouVo is not null) {
                    SheetViewList.Cells[rowNumber, 2].Value = collectionWeightTaitouVo.Weight1Total;
                    SheetViewList.Cells[rowNumber, 3].Value = collectionWeightTaitouVo.Weight2Total;
                    SheetViewList.Cells[rowNumber, 4].Value = collectionWeightTaitouVo.Weight3Total;
                    SheetViewList.Cells[rowNumber, 5].Value = collectionWeightTaitouVo.Weight4Total;
                    SheetViewList.Cells[rowNumber, 6].Value = collectionWeightTaitouVo.Weight5Total;
                    SheetViewList.Cells[rowNumber, 7].Value = collectionWeightTaitouVo.Weight6Total;
                    SheetViewList.Cells[rowNumber, 8].Value = collectionWeightTaitouVo.Weight7Total;
                }
                rowNumber++;
            }
            // Spread 活性化
            this.SpreadList.ResumeLayout();
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
        /// InitializeSheetViewList
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns></returns>
        private void InitializeSheetView(SheetView sheetView) {
            SpreadList.AllowDragDrop = false; // DrugDropを禁止する
            SpreadList.PaintSelectionHeader = false; // ヘッダの選択状態をしない
            SheetViewList.ClearRange(1, 0, 31, 9, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimePickerEx1_ValueChanged(object sender, EventArgs e) {
            if (((DateTimePickerEx)sender).Value > this.DateTimePickerEx2.GetValue()) {
                this.DateTimePickerEx2.SetValueJp(_dateUtility.GetEndOfMonth(((DateTimePickerEx)sender).GetValue()));
            }
        }
        private void DateTimePickerEx2_ValueChanged(object sender, EventArgs e) {
            if (((DateTimePickerEx)sender).Value < this.DateTimePickerEx1.GetValue()) {
                this.DateTimePickerEx1.SetValueJp(_dateUtility.GetBeginOfMonth(((DateTimePickerEx)sender).GetValue()));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CollectionWeightTaitouList_FormClosing(object sender, FormClosingEventArgs e) {
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
