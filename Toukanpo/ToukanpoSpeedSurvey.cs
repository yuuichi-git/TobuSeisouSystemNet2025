/*
 * 2025-07-29
 */
using Common;

using Dao;

using FarPoint.Excel;
using FarPoint.Win.Spread;

using Vo;

namespace Toukanpo {
    public partial class ToukanpoSpeedSurvey : Form {
        /*
         * Dao
         */
        private readonly VehicleDispatchDetailDao _vehicleDispatchDetailDao;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="connectionVo"></param>
        public ToukanpoSpeedSurvey(ConnectionVo connectionVo, Screen screen) {
            /*
             * Dao
             */
            _vehicleDispatchDetailDao = new(connectionVo);
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
                        "ToolStripMenuItemExport",
                        "ToolStripMenuItemExportExcel",
                        "ToolStripMenuItemHelp"
                        };
            this.MenuStripEx1.ChangeEnable(listString);
            this.InitializeSheetView(this.SheetViewList);                                                                               // InitializeSheetView
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
            this.NumericUpDownExYear.Enabled = false;
            this.NumericUpDownExMonth.Enabled = false;
            this.ButtonExUpdate.Enabled = false;                                                                                        // 最新化ボタンを不活性化
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "集計中・・・・";

            // 対象月の日数を調べる
            int days = DateTime.DaysInMonth((int)NumericUpDownExYear.Value, (int)NumericUpDownExMonth.Value);
            /*
             * 集計年月を表示
             */
            this.SheetViewList.Cells["A1"].Value = (int)NumericUpDownExYear.Value;                                                      // 年
            this.SheetViewList.Cells["C1"].Value = (int)NumericUpDownExMonth.Value;                                                     // 月
            this.SheetViewList.Cells["D1"].Value = string.Concat("速度超過実態調査表（", (int)NumericUpDownExYear.Value, "年", (int)NumericUpDownExMonth.Value, "月）");

            this.SheetViewList.Cells["A7"].Value = (int)NumericUpDownExMonth.Value;                                                     // 月
            for (int i = 0; i < days; i++) {
                
                DateTime operationDate = new DateTime((int)NumericUpDownExYear.Value, (int)NumericUpDownExMonth.Value, i + 1);          // 配車日を作成する

                this.SheetViewList.Cells[i + 6, 1].Value = i + 1;
                string week = operationDate.ToString("ddd");
                switch (week) {
                    case "土":
                        this.SheetViewList.Cells[i + 6, 2].ForeColor = Color.Blue;
                        break;
                    case "日":
                        this.SheetViewList.Cells[i + 6, 2].ForeColor = Color.Red;
                        break;
                    default:
                        this.SheetViewList.Cells[i + 6, 2].ForeColor = Color.Black;
                        break;
                }
                this.SheetViewList.Cells[i + 6, 2].Value = week;
                this.SheetViewList.Cells[i + 6, 3].Value = _vehicleDispatchDetailDao.GetEmploymentCount(operationDate);                 // 雇上契約数
                this.SheetViewList.Cells[i + 6, 4].Value = _vehicleDispatchDetailDao.GetWardCount(operationDate);                       // 区契約数
            }

            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "集計が完了しました";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        private void InitializeSheetView(SheetView sheetView) {
            this.SpreadList.AllowDragDrop = false;                                                                                      // DrugDropを禁止する
            this.SpreadList.PaintSelectionHeader = false;                                                                               // ヘッダの選択状態をしない
            this.SpreadList.TabStripPolicy = TabStripPolicy.Never;                                                                      // シートタブを非表示
            // 現在年月
            this.NumericUpDownExYear.Value = DateTime.Now.Year;
            this.NumericUpDownExMonth.Value = DateTime.Now.Month;
            // SheetViewList
            SheetViewList.Cells["D1"].Value = string.Empty;
            sheetView.Cells["A7"].Value = string.Empty; // 月
            for (int i = 0; i < 31; i++) {
                sheetView.Cells[i + 6, 1].Value = string.Empty;
                sheetView.Cells[i + 6, 2].Value = string.Empty;
                sheetView.Cells[i + 6, 3].Value = 0;
                sheetView.Cells[i + 6, 4].Value = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                // Excel(xlsx)形式でエクスポートする
                case "ToolStripMenuItemExportExcel":
                    //xlsx形式ファイルをエクスポートします
                    string fileName = string.Concat("速度調査", DateTime.Now.ToString("MM月dd日"), "作成");
                    SpreadList.SaveExcel(new DirectryUtility().GetExcelDesktopPassXlsx(fileName), ExcelSaveFlags.UseOOXMLFormat);
                    MessageBox.Show("デスクトップへエクスポートしました", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                // アプリケーションを終了する
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
        private void ToukanpoSpeedSurvey_FormClosing(object sender, FormClosingEventArgs e) {
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
