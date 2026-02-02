/*
 * 2026-01\28
 */
using Common;

using ControlEx;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace WastCollection {
    public partial class WastCollectionDetail : Form {
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
        /*
         * インスタンス作成
         */
        private readonly ScreenForm _screenForm = new();
        /*
         * Dao
         */
        private WordMasterDao _wordMasterDao;
        private WasteCollectionHeadDao _wasteCollectionHeadDao;
        private WasteCollectionBodyDao _wasteCollectionBodyDao;
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;
        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
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
            this.CcTextBoxId.SetEmpty();
            this.InitializeControl();
            /*
             * FpSpread/Viewを初期化
             */
            this.InitializeSheetViewList(this.SheetViewList);
            /*
             * StatusStrip
             */
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "新規登録モードで開かれました。";
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
        public WastCollectionDetail(ConnectionVo connectionVo, int id) {
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
            this.CcTextBoxId.Text = id.ToString();                                                                                          // Idをセット
            this.InitializeControl();
            /*
             * FpSpread/Viewを初期化
             */
            this.InitializeSheetViewList(this.SheetViewList);
            /*
             * StatusStrip
             */
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "修正登録モードで開かれました。";
            /*
             * Eventを登録する
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
            /*
             * データを表示する
             */
            this.SetHeadControl(_wasteCollectionHeadDao.SelectOneWasteCollectionHeadVo(id));
            this.SetBodyControl(this.SheetViewList, _wasteCollectionBodyDao.SelectAllWasteCollectionBodyVo(id));
            this.InitializeMsiControls();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CcButton_Click(object sender, EventArgs e) {
            switch (((CcButton)sender).Name) {
                case "CcButtonUpdate":



                    break;
                case "CcButtonOk":
                    if (_rowUpdateFlag) {                                                                                                   // 行の更新の場合
                        this.AddUpdateRow(this.SheetViewList, _doubleClickRowIndex, this.GetWasteCollectionBodyVo());
                        this.InitializeMsiControls();
                        _rowUpdateFlag = false;                                                                                             // Updateモードを解除
                    } else {                                                                                                                // 行の追加の場合
                        this.CcTextBoxNumber.Text = (this.SheetViewList.RowCount + 1).ToString();                                           // 新規行番号をセット
                        this.AddNewRow(this.SheetViewList, this.SheetViewList.RowCount, this.GetWasteCollectionBodyVo());
                        this.SheetViewListMsiNoReset(this.SheetViewList);
                        this.InitializeMsiControls();
                    }
                    break;
                case "CcButtonDelete":                                                                                                      // 行の削除ってことはUpdateモードで行呼び出ししてるよね
                    this.SheetViewList.RemoveRows(_doubleClickRowIndex, 1);
                    this.SheetViewListMsiNoReset(this.SheetViewList);
                    this.InitializeMsiControls();
                    _rowUpdateFlag = false;                                                                                                 // Updateモードを解除
                    break;
            }
        }

        private int _doubleClickRowIndex = 0;
        private bool _rowUpdateFlag = false;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpreadList_CellDoubleClick(object sender, CellClickEventArgs e) {
            if (e.ColumnHeader)                                                                                                             // ヘッダーのDoubleClickを回避
                return;
            _doubleClickRowIndex = e.Row;                                                                                                   // DoubleClickしたRowIndexを保存
            _rowUpdateFlag = true;                                                                                                          // Updateモードに変更
            this.SetMsiControls(this.SheetViewList, e.Row);
            this.CcButtonDelete.Enabled = true;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listWasteCollectionBodyVo"></param>
        private void SetBodyControl(SheetView sheetView, List<WasteCollectionBodyVo> listWasteCollectionBodyVo) {
            int rowIndex = 0;
            if (sheetView.Rows.Count > 0)                                                                                               // Rowを削除する
                sheetView.RemoveRows(0, sheetView.Rows.Count);
            try {
                foreach (WasteCollectionBodyVo wasteCollectionBodyVo in listWasteCollectionBodyVo) {
                    this.AddNewRow(sheetView, rowIndex, wasteCollectionBodyVo);
                    rowIndex++;
                }
            } catch (Exception exception) {
                MessageBox.Show(string.Concat("List<WasteCollectionBodyVo>の取得に失敗しました。", Environment.NewLine, exception.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = " データの取得に失敗しました。";
                return;
            }
        }

        /// <summary>
        /// SheetViewListの指定行のデータを明細入力項目にセットする
        /// </summary>
        /// <param name="sheetView">シート</param>
        /// <param name="rowIndex">行番号</param>
        private void SetMsiControls(SheetView sheetView, int rowIndex) {
            this.CcTextBoxNumber.Text = sheetView.Cells[rowIndex, _colNumberOfRow].Value.ToString();
            this.CcComboBoxItemName.Text = sheetView.Cells[rowIndex, _colItemName].Text;
            this.CcTextBoxItemSize.Text = sheetView.Cells[rowIndex, _colItemSize].Text;
            this.CcNumericUpDownNumberOfUnits.Value = Convert.ToDecimal(sheetView.Cells[rowIndex, _colNumberOfUnits].Value);
            this.CcNumericUpDownUnitPrice.Value = Convert.ToDecimal(sheetView.Cells[rowIndex, _colUnitPrice].Value);
            this.CcNumericUpDownAmount.Value = Convert.ToDecimal(sheetView.Cells[rowIndex, _colNumberOfUnits].Value) * Convert.ToDecimal(sheetView.Cells[rowIndex, _colUnitPrice].Value);
            this.CcTextBoxOthers.Text = sheetView.Cells[rowIndex, _colOthers].Text;
            this.CcButtonDelete.Enabled = true;
        }

        /// <summary>
        /// Voのデータを明細入力項目にセットする
        /// </summary>
        /// <param name="wasteCollectionBodyVo">Vo</param>
        private void SetMsiControls(WasteCollectionBodyVo wasteCollectionBodyVo) {
            this.CcTextBoxNumber.Text = wasteCollectionBodyVo.NumberOfRow.ToString();
            this.CcComboBoxItemName.Text = wasteCollectionBodyVo.ItemName;
            this.CcTextBoxItemSize.Text = wasteCollectionBodyVo.ItemSize;
            this.CcNumericUpDownNumberOfUnits.Value = wasteCollectionBodyVo.NumberOfUnits;
            this.CcNumericUpDownUnitPrice.Value = wasteCollectionBodyVo.UnitPrice;
            this.CcNumericUpDownAmount.Value = wasteCollectionBodyVo.NumberOfUnits * wasteCollectionBodyVo.UnitPrice;
            this.CcTextBoxOthers.Text = wasteCollectionBodyVo.Others;
            this.CcButtonDelete.Enabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        /// <param name="rowIndex"></param>
        /// <param name="wasteCollectionBodyVo"></param>
        private void AddNewRow(SheetView sheetView, int rowIndex, WasteCollectionBodyVo wasteCollectionBodyVo) {
            sheetView.Rows.Add(rowIndex, 1);
            sheetView.RowHeader.Columns[0].Label = (rowIndex + 1).ToString();                                                           // Rowヘッダ
            sheetView.Rows[rowIndex].Height = 20;                                                                                       // Rowの高さ
            sheetView.Rows[rowIndex].Resizable = false;                                                                                 // RowのResizableを禁止
            sheetView.Rows[rowIndex].Tag = wasteCollectionBodyVo;

            sheetView.Cells[rowIndex, _colNumberOfRow].Value = wasteCollectionBodyVo.NumberOfRow;
            sheetView.Cells[rowIndex, _colItemName].Text = wasteCollectionBodyVo.ItemName;
            sheetView.Cells[rowIndex, _colItemSize].Text = wasteCollectionBodyVo.ItemSize;
            sheetView.Cells[rowIndex, _colNumberOfUnits].Value = wasteCollectionBodyVo.NumberOfUnits;
            sheetView.Cells[rowIndex, _colUnitPrice].Value = wasteCollectionBodyVo.UnitPrice;
            sheetView.Cells[rowIndex, _colOthers].Text = wasteCollectionBodyVo.Others;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        /// <param name="rowIndex"></param>
        /// <param name="wasteCollectionBodyVo"></param>
        private void AddUpdateRow(SheetView sheetView, int rowIndex, WasteCollectionBodyVo wasteCollectionBodyVo) {
            sheetView.Cells[rowIndex, _colNumberOfRow].Value = wasteCollectionBodyVo.NumberOfRow;
            sheetView.Cells[rowIndex, _colItemName].Text = wasteCollectionBodyVo.ItemName;
            sheetView.Cells[rowIndex, _colItemSize].Text = wasteCollectionBodyVo.ItemSize;
            sheetView.Cells[rowIndex, _colNumberOfUnits].Value = wasteCollectionBodyVo.NumberOfUnits;
            sheetView.Cells[rowIndex, _colUnitPrice].Value = wasteCollectionBodyVo.UnitPrice;
            sheetView.Cells[rowIndex, _colOthers].Text = wasteCollectionBodyVo.Others;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private WasteCollectionHeadVo GetWasteCollectionHeadVo() {
            WasteCollectionHeadVo wasteCollectionHeadVo = new();
            wasteCollectionHeadVo.Id = int.Parse(this.CcTextBoxId.Text);
            wasteCollectionHeadVo.OfficeQuotationDate = this.CcDateTimeOfficeQuotationDate.GetDate();
            wasteCollectionHeadVo.OfficeRequestWord = ((WasteCollectionHeadVo)this.CcComboBoxWordName.SelectedValue).OfficeRequestWord;
            wasteCollectionHeadVo.OfficeRequestWordName = ((WasteCollectionHeadVo)this.CcComboBoxWordName.SelectedValue).OfficeRequestWordName;
            wasteCollectionHeadVo.OfficeCompanyName = this.CcComboBoxOfficeCompanyName.Text;
            wasteCollectionHeadVo.OfficeContactPerson = this.CcTextBoxOfficeContactPerson.Text;
            wasteCollectionHeadVo.OfficeAddress = this.CcTextBoxOfficeAddress.Text;
            wasteCollectionHeadVo.OfficeTelephoneNumber = this.CcTextBoxOfficeTelephoneNumber.Text;
            wasteCollectionHeadVo.OfficeCellphoneNumber = this.CcTextBoxOfficeCellphoneNumber.Text;
            wasteCollectionHeadVo.WorkSiteLocation = this.CcComboBoxWorkSiteLocation.Text;
            wasteCollectionHeadVo.WorkSiteAddress = this.CcTextBoxWorkSiteAddress.Text;
            wasteCollectionHeadVo.PickupDate = this.CcDateTimePickupDate.GetDate();
            wasteCollectionHeadVo.Remarks = this.CcTextBoxRemarks.Text;
            wasteCollectionHeadVo.MainPicture = (byte[])new ImageConverter().ConvertTo(this.CcPictureBox1.Image, typeof(byte[]));
            wasteCollectionHeadVo.SubPicture = (byte[])new ImageConverter().ConvertTo(this.CcPictureBox2.Image, typeof(byte[]));
            //wasteCollectionHeadVo.InsertPcName = ;
            //wasteCollectionHeadVo.InsertYmdHms = ;
            //wasteCollectionHeadVo.UpdatePcName = ;
            //wasteCollectionHeadVo.UpdateYmdHms = ;
            //wasteCollectionHeadVo.DeletePcName = ;
            //wasteCollectionHeadVo.DeleteYmdHms = ;
            //wasteCollectionHeadVo.DeleteFlag = ;
            return wasteCollectionHeadVo;
        }

        /// <summary>
        /// 各明細入力値をVoにセットする
        /// </summary>
        /// <returns></returns>
        private WasteCollectionBodyVo GetWasteCollectionBodyVo() {
            WasteCollectionBodyVo wasteCollectionBodyVo = new();
            wasteCollectionBodyVo.Id = int.Parse(this.CcTextBoxId.Text);
            wasteCollectionBodyVo.NumberOfRow = int.Parse(this.CcTextBoxNumber.Text);
            wasteCollectionBodyVo.ItemName = this.CcComboBoxItemName.Text;
            wasteCollectionBodyVo.ItemSize = this.CcTextBoxItemSize.Text;
            wasteCollectionBodyVo.NumberOfUnits = Convert.ToInt32(this.CcNumericUpDownNumberOfUnits.Value);
            wasteCollectionBodyVo.UnitPrice = this.CcNumericUpDownUnitPrice.Value;
            wasteCollectionBodyVo.Others = this.CcTextBoxOthers.Text;
            //wasteCollectionBodyVo.InsertPcName = ;
            //wasteCollectionBodyVo.InsertYmdHms = ;
            //wasteCollectionBodyVo.UpdatePcName = ;
            //wasteCollectionBodyVo.UpdateYmdHms = ;
            //wasteCollectionBodyVo.DeletePcName = ;
            //wasteCollectionBodyVo.DeleteYmdHms = ;
            //wasteCollectionBodyVo.DeleteFlag = ;
            return wasteCollectionBodyVo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<WasteCollectionBodyVo> GetListWasteCollectionBodyVo() {

            return new();
        }

        /// <summary>
        /// 明細の項目№を付け変える
        /// </summary>
        /// <param name="sheetView"></param>
        private void SheetViewListMsiNoReset(SheetView sheetView) {
            for (int rowIndex = 0; rowIndex < sheetView.RowCount; rowIndex++)
                sheetView.Cells[rowIndex, _colNumberOfRow].Value = rowIndex + 1;
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

        /// <summary>
        /// ContextMenuStripEx_Openedの際に設定される
        /// </summary>
        private Control _contextMenuStripExOpendControl;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStripEx1_Opened(object sender, EventArgs e) {
            switch (((ContextMenuStripEx)sender).SourceControl.Name) {
                case "CcPictureBox1":
                    _contextMenuStripExOpendControl = this.CcPictureBox1;
                    break;
                case "CcPictureBox2":
                    _contextMenuStripExOpendControl = this.CcPictureBox2;
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStripEx1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            switch (e.ClickedItem.Name) {
                case "ToolStripMenuItemPaste":
                    ((CcPictureBox)_contextMenuStripExOpendControl).Image = (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap);
                    break;
                case "ToolStripMenuItemDetail":
                    ((CcPictureBox)_contextMenuStripExOpendControl).Image = null;
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
                case "ToolStripMenuItemExit":                                                                                               // アプリケーションを終了する
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// コントロールを初期化する
        /// </summary>
        private void InitializeControl() {
            this.CcDateTimeOfficeQuotationDate.SetToday();
            this.CcComboBoxWordName.SelectedIndex = 20;                                                                                     // 足立区
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
            this.CcButtonDelete.Enabled = false;
        }

        /// <summary>
        /// 明細入力項目を初期化する
        /// </summary>
        private void InitializeMsiControls() {
            this.CcTextBoxNumber.SetEmpty();
            this.CcComboBoxItemName.DisplayClear();
            this.CcTextBoxItemSize.SetEmpty();
            this.CcNumericUpDownNumberOfUnits.Value = 0;
            this.CcNumericUpDownUnitPrice.Value = 0;
            this.CcNumericUpDownAmount.Value = 0;
            this.CcTextBoxOthers.SetEmpty();
            this.CcButtonDelete.Enabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CcPictureBox_DoubleClick(object sender, EventArgs e) {
            WastCollectionPaper wastCollectionPaper = new(_connectionVo, ((CcPictureBox)sender).Image);
            _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), wastCollectionPaper);
            wastCollectionPaper.ShowDialog(this);
        }
    }
}
