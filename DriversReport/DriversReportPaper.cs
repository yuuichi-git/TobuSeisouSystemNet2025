/*
 * 2025-1-5
 */
using System.Drawing.Printing;

using Common;

using ControlEx;

using FarPoint.Win.Spread;

using Vo;

namespace DriversReport {
    public partial class DriversReportPaper : Form {
        private PrintDocument _printDocument = new();

        /// <summary>
        /// コンストラクター(未記入の日報用)
        /// </summary>
        /// <param name="connectionVo"></param>
        public DriversReportPaper(ConnectionVo connectionVo) {
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
                "ToolStripMenuItemPrintB5",
                "ToolStripMenuItemPrintB5Dialog",
                "ToolStripMenuItemHelp"
            };
            MenuStripEx1.ChangeEnable(listString);
            /*
             * プリンターの一覧を取得後、通常使うプリンター名をセットする
             */
            foreach (string item in new PrintUtility().GetAllPrinterName())
                this.ComboBoxExPrinterName.Items.Add(item);
            this.ComboBoxExPrinterName.Text = _printDocument.PrinterSettings.PrinterName;

            this.InitializeSheetView();
            /*
             * Eventを登録する
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// コンストラクター(各種データ入力済の日報用)
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="setControl"></param>
        public DriversReportPaper(ConnectionVo connectionVo, SetControl setControl) {
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
                "ToolStripMenuItemPrintB5",
                "ToolStripMenuItemPrintB5Dialog",
                "ToolStripMenuItemHelp"
            };
            MenuStripEx1.ChangeEnable(listString);
            /*
             * プリンターの一覧を取得後、通常使うプリンター名をセットする
             */
            foreach (string item in new PrintUtility().GetAllPrinterName()) {
                this.ComboBoxExPrinterName.Items.Add(item);
            }
            this.ComboBoxExPrinterName.Text = _printDocument.PrinterSettings.PrinterName;

            this.InitializeSheetView();
            this.PutSheetView(SheetViewDriversReport, setControl);
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
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                /*
                 * B5で印刷する
                 */
                case "ToolStripMenuItemPrintB5":
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
                        if (paperSize.Kind == PaperKind.B5) {
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
                 * B5で印刷する
                 */
                case "ToolStripMenuItemPrintB5Dialog":
                    // Excelライクなプレビューダイアログを有効にします
                    SheetViewDriversReport.PrintInfo.EnhancePreview = true;
                    SheetViewDriversReport.PrintInfo.Preview = true;
                    SheetViewDriversReport.PrintInfo.ShowBorder = false;
                    SheetViewDriversReport.PrintInfo.ShowColor = true;
                    // 印刷を実行します
                    this.SpreadDriversReportPaper.PrintSheet(SheetViewDriversReport);
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
        /// 運転日報を印刷する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e) {
            // 印刷ページ（1ページ目）の描画を行う
            Rectangle rectangle = new(e.PageBounds.X, e.PageBounds.Y, e.PageBounds.Width, e.PageBounds.Height);
            // e.Graphicsへ出力(page パラメータは、０からではなく１から始まります)
            this.SpreadDriversReportPaper.OwnerPrintDraw(e.Graphics, rectangle, 0, 1);
            // 印刷終了を指定
            e.HasMorePages = false;
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeSheetView() {
            // タブストリップボタンを必要に応じて表示します。
            this.SpreadDriversReportPaper.TabStripPolicy = TabStripPolicy.Never;
            /*
             * セルを初期化する
             */
            SheetViewDriversReport.Cells[3, 17].Text = string.Empty; // 配車先
            SheetViewDriversReport.Cells[3, 23].Text = string.Empty; // 組名
            SheetViewDriversReport.Cells[5, 1].Text = string.Empty; // 運転者氏名
            SheetViewDriversReport.Cells[5, 9].Text = string.Empty; // 交代運転者氏名
            SheetViewDriversReport.Cells[8, 1].Text = string.Empty; // ドア番号
            SheetViewDriversReport.Cells[8, 6].Text = string.Empty; // 登録番号
            SheetViewDriversReport.Cells[10, 17].Text = string.Empty; // 休憩場所
            SheetViewDriversReport.Cells[12, 3].Text = string.Empty; // 運搬先名①
            SheetViewDriversReport.Cells[12, 8].Text = string.Empty; // 正味重量①
            SheetViewDriversReport.Cells[13, 3].Text = string.Empty; // 運搬先名②
            SheetViewDriversReport.Cells[13, 8].Text = string.Empty; // 正味重量②
            SheetViewDriversReport.Cells[14, 3].Text = string.Empty; // 運搬先名③
            SheetViewDriversReport.Cells[14, 8].Text = string.Empty; // 正味重量③
            SheetViewDriversReport.Cells[15, 3].Text = string.Empty; // 運搬先名④
            SheetViewDriversReport.Cells[15, 8].Text = string.Empty; // 正味重量④
            SheetViewDriversReport.Cells[12, 17].Text = string.Empty; // 運搬先名⑤
            SheetViewDriversReport.Cells[12, 22].Text = string.Empty; // 正味重量⑤
            SheetViewDriversReport.Cells[13, 17].Text = string.Empty; // 運搬先名⑥
            SheetViewDriversReport.Cells[13, 22].Text = string.Empty; // 正味重量⑥
            SheetViewDriversReport.Cells[14, 17].Text = string.Empty; // 運搬先名⑦
            SheetViewDriversReport.Cells[14, 22].Text = string.Empty; // 正味重量⑦
            SheetViewDriversReport.Cells[15, 17].Text = string.Empty; // 運搬先名⑧
            SheetViewDriversReport.Cells[15, 22].Text = string.Empty; // 正味重量⑧
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        /// <param name="setControl"></param>
        private void PutSheetView(SheetView sheetView, SetControl setControl) {
            // 各種値の取得
            SheetViewDriversReport.Cells[3, 17].Text = ((SetLabel)setControl.DeployedSetLabel).SetMasterVo.SetName1; // 配車先
            SheetViewDriversReport.Cells[3, 23].Text = ((SetLabel)setControl.DeployedSetLabel).SetMasterVo.SetName2; // 組名
            SheetViewDriversReport.Cells[5, 1].Text = (StaffLabel)setControl.DeployedStaffLabel1 is not null ? ((StaffLabel)setControl.DeployedStaffLabel1).StaffMasterVo.OtherName : string.Empty; // 運転者氏名
            SheetViewDriversReport.Cells[8, 1].Text = (CarLabel)setControl.DeployedCarLabel is not null ? ((CarLabel)setControl.DeployedCarLabel).CarMasterVo.DoorNumber.ToString() : string.Empty; // ドア番号
            SheetViewDriversReport.Cells[8, 6].Text = (CarLabel)setControl.DeployedCarLabel is not null ? ((CarLabel)setControl.DeployedCarLabel).CarMasterVo.RegistrationNumber : string.Empty; // 登録番号
            switch (setControl.SetCode) {
                case 1311706: // 北区粗大軽
                    SheetViewDriversReport.Cells[3, 23].Text = "粗大軽 / 資源"; // 組名
                    SheetViewDriversReport.Cells[10, 17].Text = "浮間清掃事業所 /"; // 休憩場所
                    break;
                case 1311707: // 北区粗大１
                case 1311708: // 北区粗大２
                case 1311709: // 北区粗大３
                case 1311710: // 北区粗大４
                case 1311711: // 北区粗大５
                case 1311715: // 北区粗大対策
                    SheetViewDriversReport.Cells[3, 23].Text = "粗大　　組"; // 組名
                    SheetViewDriversReport.Cells[10, 17].Text = "浮間清掃事業所 /"; // 休憩場所
                    SheetViewDriversReport.Cells[12, 3].Text = "浮間清掃事業所"; // 運搬先名①
                    SheetViewDriversReport.Cells[13, 3].Text = "浮間清掃事業所"; // 運搬先名②
                    SheetViewDriversReport.Cells[14, 3].Text = "浮間清掃事業所"; // 運搬先名③
                    break;
                case 1310602: // 台東資源１
                case 1310603: // 台東資源２
                case 1310607: // 台東資源４
                    SheetViewDriversReport.Cells[10, 17].Text = "清川車庫 /"; // 休憩場所
                    break;
                case 1310801: // 江東１（新大）
                case 1310802: // 江東４（新大）
                case 1310815: // 江東４（小プ）
                case 1310816: // 江東１１（新大）
                case 1310817: // 江東８（新大）
                    SheetViewDriversReport.Cells[10, 17].Text = "江東区清掃事務所 /"; // 休憩場所
                    break;
                case 1311902: // 板橋西軽１
                case 1311903: // 板橋西軽２
                case 1311904: // 板橋西軽３
                    SheetViewDriversReport.Cells[10, 17].Text = "西台中継所/板橋清掃工場 /"; // 休憩場所
                    break;
                case 1311910: // 板橋東プラ軽３
                    SheetViewDriversReport.Cells[10, 17].Text = "板橋清掃工場"; // 休憩場所
                    break;
                case 1312013: // 石神井不燃５
                    SheetViewDriversReport.Cells[10, 17].Text = "田中駐車場 /"; // 休憩場所
                    break;
                case 1312161: // 足立１６
                case 1312162: // 足立２２
                case 1312164: // 足立２３(２０２４)
                case 1312163: // 足立３７
                case 1312105: // 足立不燃４
                    SheetViewDriversReport.Cells[10, 17].Text = "足立清掃工場 /"; // 休憩場所
                    break;
                case 1312212: // 小岩６
                    SheetViewDriversReport.Cells[10, 17].Text = "小岩清掃事務所 駐車場 /"; // 休憩場所
                    SheetViewDriversReport.Cells[11, 8].Text = "搬　入　時　刻";
                    SheetViewDriversReport.Cells[11, 22].Text = "搬　入　時　刻";
                    SheetViewDriversReport.Cells[12, 8].Text = "："; // 正味重量①
                    SheetViewDriversReport.Cells[13, 8].Text = "："; // 正味重量②
                    SheetViewDriversReport.Cells[14, 8].Text = "："; // 正味重量③
                    SheetViewDriversReport.Cells[15, 8].Text = "："; // 正味重量④
                    SheetViewDriversReport.Cells[12, 22].Text = "："; // 正味重量⑤
                    SheetViewDriversReport.Cells[13, 22].Text = "："; // 正味重量⑥
                    SheetViewDriversReport.Cells[14, 22].Text = "："; // 正味重量⑦
                    SheetViewDriversReport.Cells[15, 22].Text = "："; // 正味重量⑧
                    break;
                case 1310417: // 新宿２－５１
                    SheetViewDriversReport.Cells[10, 17].Text = "新宿清掃事務所　駐車場 /"; // 休憩場所
                    break;
                case 1311507: // 方南２３３
                    SheetViewDriversReport.Cells[10, 17].Text = "杉並清掃工場 /"; // 休憩場所
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DriversReportPaper_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
