/*
 * 2026-01\28
 */
using ControlEx;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace WastCollection {
    public partial class WastCollectionDetail : Form {
        /*
         * Dao
         */
        private WordMasterDao _wordMasterDao;
        private WasteCollectionHeadDao _wasteCollectionHeadDao;
        private WasteCollectionBodyDao _wasteCollectionBodyDao;
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;
        private WasteCollectionHeadVo _wasteCollectionHeadVo;
        private List<WasteCollectionBodyVo> _listWasteCollectionBodyVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="id"></param>
        public WastCollectionDetail(ConnectionVo connectionVo) {
            /*
             * Dao
             */
            _wordMasterDao = new(connectionVo);
            _wasteCollectionHeadDao = new(connectionVo);
            _wasteCollectionBodyDao = new(connectionVo);
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
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);
            /*
             * Control
             */
            this.CcComboBoxWordName.SetItems(_wordMasterDao.SelectAllWordMaster());
            this.InitializeControl();
            /*
             * FpSpread/Viewを初期化
             */
            this.InitializeSheetViewList(this.SheetViewList);
            /*
             * StatusStrip
             */
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;
            /*
             * Eventを登録する
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="id"></param>
        public WastCollectionDetail(ConnectionVo connectionVo, string id) {
            /*
             * Dao
             */
            _wordMasterDao = new(connectionVo);
            _wasteCollectionHeadDao = new(connectionVo);
            _wasteCollectionBodyDao = new(connectionVo);
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
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);
            /*
             * Control
             */
            this.CcComboBoxWordName.SetItems(_wordMasterDao.SelectAllWordMaster());
            this.InitializeControl();
            /*
             * FpSpread/Viewを初期化
             */
            this.InitializeSheetViewList(this.SheetViewList);
            /*
             * StatusStrip
             */
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;
            /*
             * Eventを登録する
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
            /*
             * データを表示する
             */
            this.SetHeadControl(_wasteCollectionHeadDao.SelectOneWasteCollectionHeadVo(id));
            this.SetBodyControl(this.SheetViewList, _wasteCollectionBodyDao.SelectAllWasteCollectionBodyVo(id));
            this.SetMsiControl();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CcButton_Click(object sender, EventArgs e) {
            switch (((CcButton)sender).Name) {
                case "CcButtonUpdate":
                    this.Close();
                    break;
                case "CcButtonOk":

                    break;
                case "CcButtonDelete":

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
                case "ToolStripMenuItemExit":                                                                                           // アプリケーションを終了する
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpreadList_CellDoubleClick(object sender, CellClickEventArgs e) {
            if (e.ColumnHeader)                                                                                                         // ヘッダーのDoubleClickを回避
                return;



        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeControl() {
            this.CcButtonUpdate.Enabled = false;
            this.CcDateTimeOfficeQuotationDate.SetToday();
            this.CcComboBoxWordName.SelectedIndex = 20;                                                                                 // 足立区
            /*
             * 本社(依頼主)
             */
            this.CcComboBoxOfficeCompanyName.DisplayClear();
            this.CcTextBoxOfficeContactPerson.SetEmpty();
            this.CcTextBoxOfficeAddress.SetEmpty();
            this.CcTextBoxOfficeTelephoneNumber.SetEmpty();
            this.CcTextBoxOfficeCellphoneNumber.SetEmpty();
            /*
             * 現場(回収場所)
             */
            this.CcComboBoxWorkSiteLocation.DisplayClear();
            this.CcTextBoxWorkSiteAddress.SetEmpty();

            this.CcDateTimePickupDate.SetToday();
            this.CcTextBoxRemarks.SetEmpty();
            /*
             * 入力項目
             */
            this.CcTextBoxNumber.SetEmpty();
            this.CcComboBoxItemName.DisplayClear();
            this.CcTextBoxItemSize.SetEmpty();
            this.CcNumericUpDownNumberOfUnits.Value = 0;
            this.CcNumericUpDownUnitPrice.Value = 0;
            this.CcNumericUpDownAmount.Value = 0;
            this.CcTextBoxOthers.SetEmpty();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wasteCollectionHeadVo"></param>
        private void SetHeadControl(WasteCollectionHeadVo wasteCollectionHeadVo) {
            this.CcDateTimeOfficeQuotationDate.Value = wasteCollectionHeadVo.OfficeQuotationDate;
            this.CcComboBoxWordName.Text = wasteCollectionHeadVo.OfficeRequestWordName;
            /*
             * 本社(依頼主)
             */
            this.CcComboBoxOfficeCompanyName.Text = wasteCollectionHeadVo.OfficeCompanyName;
            this.CcTextBoxOfficeContactPerson.Text = wasteCollectionHeadVo.OfficeContactPerson;
            this.CcTextBoxOfficeAddress.Text = wasteCollectionHeadVo.OfficeAddress;
            this.CcTextBoxOfficeTelephoneNumber.Text = wasteCollectionHeadVo.OfficeTelephoneNumber;
            this.CcTextBoxOfficeCellphoneNumber.Text = wasteCollectionHeadVo.OfficeCellphoneNumber;
            /*
             * 現場(回収場所)
             */
            this.CcComboBoxWorkSiteLocation.Text = wasteCollectionHeadVo.WorkSiteLocation;
            this.CcTextBoxWorkSiteAddress.Text = wasteCollectionHeadVo.WorkSiteAddress;

            this.CcDateTimePickupDate.Value = wasteCollectionHeadVo.PickupDate;
            this.CcTextBoxRemarks.Text = wasteCollectionHeadVo.Remarks;
        }

        /*
         * Column Index
         */
        private const int _colNumberOfRow = 0;
        private const int _colItemName = 1;
        private const int _colItemSize = 2;
        private const int _colNumberOfUnits = 3;
        private const int _colUnitPrice = 4;
        private const int _colTotalPrice = 5;
        private const int _colOthers = 6;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="listWasteCollectionBodyVo"></param>
        private void SetBodyControl(SheetView sheetView, List<WasteCollectionBodyVo> listWasteCollectionBodyVo) {
            int rowCount = 0;
            if (sheetView.Rows.Count > 0)                                                                                               // Rowを削除する
                sheetView.RemoveRows(0, sheetView.Rows.Count);
            try {
                foreach (WasteCollectionBodyVo wasteCollectionBodyVo in listWasteCollectionBodyVo) {
                    sheetView.Rows.Add(rowCount, 1);
                    sheetView.RowHeader.Columns[0].Label = (rowCount + 1).ToString();                                                   // Rowヘッダ
                    sheetView.Rows[rowCount].Height = 20;                                                                               // Rowの高さ
                    sheetView.Rows[rowCount].Resizable = false;                                                                         // RowのResizableを禁止
                    sheetView.Rows[rowCount].Tag = wasteCollectionBodyVo;

                    sheetView.Cells[rowCount, _colNumberOfRow].Value = wasteCollectionBodyVo.NumberOfRow;
                    sheetView.Cells[rowCount, _colItemName].Text = wasteCollectionBodyVo.ItemName;
                    sheetView.Cells[rowCount, _colItemSize].Text = wasteCollectionBodyVo.ItemSize;
                    sheetView.Cells[rowCount, _colNumberOfUnits].Value = wasteCollectionBodyVo.NumberOfUnits;
                    sheetView.Cells[rowCount, _colUnitPrice].Value = wasteCollectionBodyVo.UnitPrice;
                    sheetView.Cells[rowCount, _colOthers].Text = wasteCollectionBodyVo.Others;

                    rowCount++;
                }
            } catch (Exception ex) {
                MessageBox.Show(string.Concat("List<WasteCollectionBodyVo>の取得に失敗しました。", Environment.NewLine, ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = " データの取得に失敗しました。";
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetMsiControl() {
            this.CcTextBoxNumber.SetEmpty();
            this.CcComboBoxItemName.DisplayClear();
            this.CcTextBoxItemSize.SetEmpty();
            this.CcNumericUpDownNumberOfUnits.Value = 0;
            this.CcNumericUpDownUnitPrice.Value = 0;
            this.CcNumericUpDownAmount.Value = 0;
            this.CcTextBoxOthers.SetEmpty();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns></returns>
        private SheetView InitializeSheetViewList(SheetView sheetView) {
            this.SpreadList.AllowDragDrop = false;                                                                                      // DrugDropを禁止する
            this.SpreadList.PaintSelectionHeader = false;                                                                               // ヘッダの選択状態をしない
            sheetView.ColumnHeader.Rows[0].Height = 30;                                                                                 // Columnヘッダの高さ
            sheetView.GrayAreaBackColor = Color.White;
            sheetView.HorizontalGridLine = new GridLine(GridLineType.None);
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9);                                                          // 行ヘッダのFont
            sheetView.RowHeader.Columns[0].Width = 50;                                                                                  // 行ヘッダの幅を変更します
            sheetView.VerticalGridLine = new GridLine(GridLineType.Flat, Color.LightGray);
            sheetView.RemoveRows(0, sheetView.Rows.Count);
            return sheetView;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WastCollectionDetail_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
