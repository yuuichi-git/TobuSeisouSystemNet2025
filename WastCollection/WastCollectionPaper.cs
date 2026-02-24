/*
 * 2026-02-11
 */
using System.Drawing.Printing;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace WastCollection {
    public partial class WastCollectionPaper : Form {
        private DateTime _defaultDateTime = new(1900, 1, 1);
        /*
         * Dao
         */
        private WasteCollectionHeadDao _wasteCollectionHeadDao;
        private WasteCollectionBodyDao _wasteCollectionBodyDao;
        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="id"></param>
        public WastCollectionPaper(ConnectionVo connectionVo, int id) {
            /*
             * Dao
             */
            _wasteCollectionHeadDao = new(connectionVo);
            _wasteCollectionBodyDao = new(connectionVo);
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
             * SheetView初期化
             */
            this.InitializeSheetView(SheetViewList);
            this.SetSheetView(this.SheetViewList, _wasteCollectionHeadDao.SelectOneWasteCollectionHead(id), _wasteCollectionBodyDao.SelectAllWasteCollectionBody(id));
            /*
             * StatusStrip
             */
            this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = string.Empty;
            /*
             * Eventを登録する
             */
            this.CcMenuStrip1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// SheetView初期化
        /// </summary>
        /// <param name="sheetView"></param>
        private void InitializeSheetView(SheetView sheetView) {
            const int DETAIL_START_ROW = 10;
            const int DETAIL_START_COL = 1;
            const int DETAIL_ROW_COUNT = 14;
            const int DETAIL_COL_COUNT = 7;

            sheetView.Cells[1, 3].Text = DateTime.Today.ToString("yyyy年MM月dd日 (dddd)");                                               // 見積日
            sheetView.Cells[1, 7].Text = "";                                                                                            // 依頼区
            sheetView.Cells[2, 3].Text = "";                                                                                            // 会社名
            sheetView.Cells[2, 7].Text = "";                                                                                            // 担当者
            sheetView.Cells[3, 3].Text = "";                                                                                            // 住所
            sheetView.Cells[4, 3].Text = "";                                                                                            // 連絡先
            sheetView.Cells[4, 7].Text = "";                                                                                            // 携帯
            sheetView.Cells[5, 3].Text = "";                                                                                            // 回収場所
            sheetView.Cells[6, 3].Text = "";                                                                                            // 住所
            sheetView.Cells[7, 3].Text = "";                                                                                            // 回収日
            sheetView.ClearRange(DETAIL_START_ROW, DETAIL_START_COL, DETAIL_ROW_COUNT, DETAIL_COL_COUNT, true);
            sheetView.Cells[24, 1].Text = "";                                                                                           // その他
            //sheetView.Cells[24, 7].Value = 0;                                                                                           // 小計
            //sheetView.Cells[25, 7].Value = 0;                                                                                           // 消費税
            //sheetView.Cells[26, 7].Value = 0;                                                                                           // 合計
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        /// <param name="wasteCollectionHeadVo"></param>
        /// <param name="listWasteCollectionBodyVo"></param>
        private void SetSheetView(SheetView sheetView, WasteCollectionHeadVo wasteCollectionHeadVo, List<WasteCollectionBodyVo> listWasteCollectionBodyVo) {
            sheetView.Cells[1, 3].Text = wasteCollectionHeadVo.OfficeQuotationDate.ToString("yyyy年MM月dd日 (dddd)");                    // 見積日
            sheetView.Cells[1, 7].Text = wasteCollectionHeadVo.OfficeRequestWordName;                                                   // 依頼区
            sheetView.Cells[2, 3].Text = wasteCollectionHeadVo.OfficeCompanyName;                                                       // 会社名
            sheetView.Cells[2, 7].Text = wasteCollectionHeadVo.OfficeContactPerson;                                                     // 担当者
            sheetView.Cells[3, 3].Text = wasteCollectionHeadVo.OfficeAddress;                                                           // 住所
            sheetView.Cells[4, 3].Text = wasteCollectionHeadVo.OfficeTelephoneNumber;                                                   // 連絡先
            sheetView.Cells[4, 7].Text = wasteCollectionHeadVo.OfficeCellphoneNumber;                                                   // 携帯
            sheetView.Cells[5, 3].Text = wasteCollectionHeadVo.WorkSiteLocation;                                                        // 回収場所
            sheetView.Cells[6, 3].Text = wasteCollectionHeadVo.WorkSiteAddress;                                                         // 住所
            if (wasteCollectionHeadVo.PickupDate.Date != _defaultDateTime.Date) {
                sheetView.Cells[7, 3].Text = wasteCollectionHeadVo.PickupDate.ToString("yyyy年MM月dd日 (dddd)");                         // 回収日
            } else {
                sheetView.Cells[7, 3].Text = "";
            }
            foreach (WasteCollectionBodyVo wasteCollectionBodyVo in listWasteCollectionBodyVo) {
                if (listWasteCollectionBodyVo.Count > 14) {
                    MessageBox.Show("明細行数が14行を超えています。辻へ報告してください。", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int rowIndex = wasteCollectionBodyVo.NumberOfRow + 9;
                sheetView.Cells[rowIndex, 1].Text = wasteCollectionBodyVo.ItemName;                                                     // 品名
                sheetView.Cells[rowIndex, 3].Text = wasteCollectionBodyVo.ItemSize;                                                     // 規格・サイズ
                sheetView.Cells[rowIndex, 4].Value = wasteCollectionBodyVo.NumberOfUnits;                                               // 数量
                sheetView.Cells[rowIndex, 5].Value = wasteCollectionBodyVo.UnitPrice;                                                   // 単価
                sheetView.Cells[rowIndex, 6].Value = wasteCollectionBodyVo.NumberOfUnits * wasteCollectionBodyVo.UnitPrice;             // 金額
                sheetView.Cells[rowIndex, 7].Text = wasteCollectionBodyVo.Remarks;                                                      // その他
            }
            sheetView.Cells[24, 1].Text = wasteCollectionHeadVo.Remarks;                                                                // その他
            //sheetView.Cells[24, 7].Value = 0;                                                                                           // 小計
            //sheetView.Cells[25, 7].Value = 0;                                                                                           // 消費税
            //sheetView.Cells[26, 7].Value = 0;                                                                                           // 合計
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemExit":
                    this.Close();
                    break;

                case "ToolStripMenuItemPrintA4":
                    PrintDocument _printDocument = new PrintDocument();
                    _printDocument.PrintPage += this.PrintDocument_PrintPage;                                                           // 印刷処理のイベントハンドラを登録

                    PrinterSettings printerSettings = _printDocument.PrinterSettings;
                    printerSettings.Copies = 1;                                                                                         // 印刷部数
                    printerSettings.Duplex = Duplex.Simplex;                                                                            // 両面印刷の設定（片面印刷）

                    _printDocument.DefaultPageSettings.Color = true;                                                                    // カラー印刷
                    _printDocument.Print();                                                                                             // 印刷実行
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e) {
            Rectangle rectangle = new Rectangle(e.PageBounds.X, e.PageBounds.Y, e.PageBounds.Width, e.PageBounds.Height);               // 印刷領域
            rectangle.Inflate(-20, -40);                                                                                                // 印刷領域の余白を設定
            this.SpreadList.OwnerPrintDraw(e.Graphics, rectangle, 0, 1);
            e.HasMorePages = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WastCollectionPaper_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
