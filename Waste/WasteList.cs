/*
 * 2025-06-11
 */
using Common;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace Waste {
    public partial class WasteList : Form {
        private readonly ScreenForm _screenForm = new();
        private readonly Screen _screen;
        /*
         * Dao
         */
        private WasteCustomerDao _wasteCustomerDao;
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public WasteList(ConnectionVo connectionVo, Screen screen) {
            _screen = screen;
            /*
             * Dao
             */
            _wasteCustomerDao = new(connectionVo);
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
                "ToolStripMenuItemEdit",
                "ToolStripMenuItemInsertNewRecord",
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);
            /*
             * FpSpread/Viewを初期化
             */
            this.InitializeSheetViewList(this.SheetViewList);
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
            this.PutSheetViewList(this.SheetViewList);
        }

        /// <summary>
        /// SheetViewの現在のRow位置を保持する
        /// </summary>
        int _spreadListTopRow = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        private void PutSheetViewList(SheetView sheetView) {
            int rowCount = 0;
            this.SpreadList.SuspendLayout();                                                                            // Spread 非活性化
            _spreadListTopRow = this.SpreadList.GetViewportTopRow(0);                                                   // 先頭行（列）インデックスを取得
            if (sheetView.Rows.Count > 0)                                                                               // Rowを削除する
                sheetView.RemoveRows(0, sheetView.Rows.Count);
            foreach (WasteCustomerVo wasteCustomerVo in _wasteCustomerDao.SelectAllWasteCustomerVo().Where(x => x.EmissionPlaceName.Contains(this.TextBoxExEmissionPlaceNameSearch.Text))) {
                sheetView.Rows.Add(rowCount, 1);
                sheetView.RowHeader.Columns[0].Label = (rowCount + 1).ToString();                                       // Rowヘッダ
                sheetView.Rows[rowCount].Height = 20;                                                                   // Rowの高さ
                sheetView.Rows[rowCount].Resizable = false;                                                             // RowのResizableを禁止
                sheetView.Rows[rowCount].Tag = wasteCustomerVo;

                sheetView.Cells[rowCount, 0].Text = wasteCustomerVo.ConcludedDate.ToString("yyyy/MM/dd");
                sheetView.Cells[rowCount, 1].Text = wasteCustomerVo.ConcludedDetail;
                sheetView.Cells[rowCount, 2].Text = wasteCustomerVo.TransportationCompany;
                sheetView.Cells[rowCount, 3].Text = wasteCustomerVo.EmissionCompanyKana;
                sheetView.Cells[rowCount, 4].Text = wasteCustomerVo.EmissionCompanyName;
                sheetView.Cells[rowCount, 5].Text = wasteCustomerVo.PostNumber;
                sheetView.Cells[rowCount, 6].Text = wasteCustomerVo.Address;
                sheetView.Cells[rowCount, 7].Text = wasteCustomerVo.TelephoneNumber;
                sheetView.Cells[rowCount, 8].Text = wasteCustomerVo.FaxNumber;
                sheetView.Cells[rowCount, 9].Text = wasteCustomerVo.EmissionPlaceName;
                sheetView.Cells[rowCount, 10].Text = wasteCustomerVo.EmissionPlaceAddress;
                sheetView.Cells[rowCount, 11].Value = wasteCustomerVo.UnitPriceFlammable;
                sheetView.Cells[rowCount, 12].Value = wasteCustomerVo.UnitPriceCollection;
                sheetView.Cells[rowCount, 13].Value = wasteCustomerVo.UnitPriceDisposal;
                sheetView.Cells[rowCount, 14].Value = wasteCustomerVo.UnitPriceResources;
                sheetView.Cells[rowCount, 15].Value = wasteCustomerVo.UnitPriceTransportationCosts;
                sheetView.Cells[rowCount, 16].Value = wasteCustomerVo.UnitPriceManifestCosts;
                sheetView.Cells[rowCount, 17].Value = wasteCustomerVo.UnitPriceOtherCosts;
                sheetView.Cells[rowCount, 18].Value = wasteCustomerVo.UnitPriceBulkyTransportationCosts;
                sheetView.Cells[rowCount, 19].Value = wasteCustomerVo.UnitPriceBulkyDisposal;
                sheetView.Cells[rowCount, 20].Text = wasteCustomerVo.Remarks;

                rowCount++;
            }
            // 先頭行（列）インデックスをセット
            this.SpreadList.SetViewportTopRow(0, _spreadListTopRow);
            // Spread 活性化
            this.SpreadList.ResumeLayout();
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(" ", rowCount, " 件を処理しました");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpreadList_CellDoubleClick(object sender, CellClickEventArgs e) {
            // ColumnヘッダーのDoubleClickを回避
            if (e.ColumnHeader)
                return;
            WasteDetail wasteDetail = new(_connectionVo, _screen, ((WasteCustomerVo)SheetViewList.Rows[e.Row].Tag).Id);
            _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), wasteDetail);
            wasteDetail.Show(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemInsertNewRecord":
                    WasteDetail wasteDetail = new(_connectionVo, _screen);
                    _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), wasteDetail);
                    wasteDetail.Show(this);
                    break;
                case "ToolStripMenuItemExit": // アプリケーションを終了する
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns></returns>
        private SheetView InitializeSheetViewList(SheetView sheetView) {
            this.SpreadList.AllowDragDrop = false;                                                      // DrugDropを禁止する
            this.SpreadList.PaintSelectionHeader = false;                                               // ヘッダの選択状態をしない
            this.SpreadList.TabStripPolicy = TabStripPolicy.Never;                                      // シートタブを非表示
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WasteList_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
