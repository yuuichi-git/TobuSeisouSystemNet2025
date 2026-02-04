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
        /// <summary>
        /// DoubleClickしたRowIndexを保持
        /// </summary>
        private int _doubleClickRowIndex = 0;
        /// <summary>
        /// true:Updateモード false:Insertモード
        /// </summary>
        private bool _rowUpdateFlag = false;
        /// <summary>
        /// コンストラクター(修正登録)で使用するIdを保持
        /// </summary>
        private int _id = 0;
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
        /// コンストラクター(新規登録)
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
             * Idを取得
             */
            _id = _wasteCollectionHeadDao.GetNewId();
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
            this.CcTextBoxId.Text = _id.ToString();                                                                                          // Idをセット
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
        /// コンストラクター(修正登録)
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
             * Idを取得
             */
            _id = id;
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
            this.SetHeadControls(_wasteCollectionHeadDao.SelectOneWasteCollectionHead(id));
            this.SetBodyControls(this.SheetViewList, _wasteCollectionBodyDao.SelectAllWasteCollectionBody(id));
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
                    /*
                     * HEADの更新・追加
                     */
                    if (_wasteCollectionHeadDao.ExistenceWasteCollectionHead(_id)) {
                        try {
                            _wasteCollectionHeadDao.UpdateOneWasteCollectionHead(this.GetWasteCollectionHeadVo());
                        } catch (Exception exception) {
                            MessageBox.Show(string.Concat("WasteCollectionHeadのUPDATEに失敗しました。", Environment.NewLine, exception.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    } else {
                        try {
                            _wasteCollectionHeadDao.InsertOneWasteCollectionHead(this.GetWasteCollectionHeadVo());
                        } catch (Exception exception) {
                            MessageBox.Show(string.Concat("WasteCollectionHeadのINSERTに失敗しました。", Environment.NewLine, exception.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    this.Close();
                    break;

                case "CcButtonOk":
                    this.AddNewRow(this.SheetViewList, this.SheetViewList.RowCount, this.GetWasteCollectionBodyVo(_id, this.SheetViewList.RowCount + 1));
                    //this.SheetViewListMsiNoReset(this.SheetViewList);
                    switch (_rowUpdateFlag) {
                        case false:                                                                                                         // Insertモード
                            try {
                                _wasteCollectionBodyDao.InsertOneWasteCollectionBody(_id, this.SheetViewList.RowCount, (WasteCollectionBodyVo)this.SheetViewList.Rows[this.SheetViewList.RowCount - 1].Tag);
                            } catch (Exception exception) {
                                MessageBox.Show(string.Concat("WasteCollectionBodyのINSERTに失敗しました。", Environment.NewLine, exception.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            break;
                        case true:                                                                                                          // Updateモード
                            this.AddUpdateRow(this.SheetViewList, _doubleClickRowIndex, this.GetWasteCollectionBodyVo(_id, _doubleClickRowIndex));
                            //this.SheetViewListMsiNoReset(this.SheetViewList);
                            this.InitializeMsiControls();
                            try {
                                _wasteCollectionBodyDao.UpdateOneWasteCollectionBody(_id, _doubleClickRowIndex + 1, (WasteCollectionBodyVo)this.SheetViewList.Rows[_doubleClickRowIndex].Tag);
                            } catch (Exception exception) {
                                MessageBox.Show(string.Concat("WasteCollectionBodyのUPDATEに失敗しました。", Environment.NewLine, exception.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            break;
                    }
                    this.InitializeMsiControls();
                    _rowUpdateFlag = false;                                                                                                 // Updateモードを解除
                    break;

                case "CcButtonDelete":                                                                                                      // 行の削除ってことはUpdateモードで行呼び出ししてるよね
                    this.SheetViewList.RemoveRows(_doubleClickRowIndex, 1);
                    //this.SheetViewListMsiNoReset(this.SheetViewList);
                    try {
                        _wasteCollectionBodyDao.DeleteOneWasteCollectionBody(_id, _doubleClickRowIndex + 1);
                    } catch (Exception exception) {
                        MessageBox.Show(string.Concat("WasteCollectionBodyのDELETEに失敗しました。", Environment.NewLine, exception.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    this.InitializeMsiControls();
                    _rowUpdateFlag = false;                                                                                                 // Updateモードを解除
                    break;

                case "CcButtonMaps1":
                    new Maps().MapOpen(this.CcTextBoxOfficeAddress.Text);
                    break;

                case "CcButtonMaps2":
                    new Maps().MapOpen(this.CcTextBoxWorkSiteAddress.Text);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpreadList_CellDoubleClick(object sender, CellClickEventArgs e) {
            _doubleClickRowIndex = e.Row;                                                                                                   // DoubleClickしたRowIndexを保存
            _rowUpdateFlag = true;                                                                                                          // Updateモードに変更

            if (e.ColumnHeader)                                                                                                             // ヘッダーのDoubleClickを回避
                return;

            WasteCollectionBodyVo wasteCollectionBodyVo = (WasteCollectionBodyVo)this.SheetViewList.Rows[_doubleClickRowIndex].Tag;
            if (wasteCollectionBodyVo is null || wasteCollectionBodyVo.DeleteFlag == true) {
                this.CcButtonDelete.Enabled = false;                                                                                        // 削除済みのレコードの場合、削除ボタンを無効化
            } else {
                this.CcButtonDelete.Enabled = true;                                                                                         // 削除ボタンを有効化
            }
            this.SetMsiControls(this.SheetViewList, e.Row);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wasteCollectionHeadVo"></param>
        private void SetHeadControls(WasteCollectionHeadVo wasteCollectionHeadVo) {
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

            if (wasteCollectionHeadVo.MainPicture.Length != 0) {
                ImageConverter imageConverter = new();
                this.CcPictureBox1.Image = (Image)imageConverter.ConvertFrom(wasteCollectionHeadVo.MainPicture);
            }
            if (wasteCollectionHeadVo.SubPicture.Length != 0) {
                ImageConverter imageConverter = new();
                this.CcPictureBox2.Image = (Image)imageConverter.ConvertFrom(wasteCollectionHeadVo.SubPicture);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listWasteCollectionBodyVo"></param>
        private void SetBodyControls(SheetView sheetView, List<WasteCollectionBodyVo> listWasteCollectionBodyVo) {
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
            sheetView.Rows[rowIndex].ForeColor = wasteCollectionBodyVo.DeleteFlag ? Color.DarkGray : Color.Black;                       // 削除済のレコードのForeColorをセット
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
        /// <returns></returns>
        private WasteCollectionHeadVo GetWasteCollectionHeadVo() {
            WasteCollectionHeadVo wasteCollectionHeadVo = new();
            wasteCollectionHeadVo.Id = int.Parse(this.CcTextBoxId.Text);
            wasteCollectionHeadVo.OfficeQuotationDate = this.CcDateTimeOfficeQuotationDate.GetValue();
            wasteCollectionHeadVo.OfficeRequestWord = ((WordMasterVo)this.CcComboBoxWordName.SelectedValue).Code;
            wasteCollectionHeadVo.OfficeRequestWordName = ((WordMasterVo)this.CcComboBoxWordName.SelectedValue).Name;
            wasteCollectionHeadVo.OfficeCompanyName = this.CcComboBoxOfficeCompanyName.Text;
            wasteCollectionHeadVo.OfficeContactPerson = this.CcTextBoxOfficeContactPerson.Text;
            wasteCollectionHeadVo.OfficeAddress = this.CcTextBoxOfficeAddress.Text;
            wasteCollectionHeadVo.OfficeTelephoneNumber = this.CcTextBoxOfficeTelephoneNumber.Text;
            wasteCollectionHeadVo.OfficeCellphoneNumber = this.CcTextBoxOfficeCellphoneNumber.Text;
            wasteCollectionHeadVo.WorkSiteLocation = this.CcComboBoxWorkSiteLocation.Text;
            wasteCollectionHeadVo.WorkSiteAddress = this.CcTextBoxWorkSiteAddress.Text;
            wasteCollectionHeadVo.PickupDate = this.CcDateTimePickupDate.GetValue();
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
        private WasteCollectionBodyVo GetWasteCollectionBodyVo(int id, int numberOfRow) {
            WasteCollectionBodyVo wasteCollectionBodyVo = new();
            wasteCollectionBodyVo.Id = id;
            wasteCollectionBodyVo.NumberOfRow = numberOfRow;
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
                case "ToolStripMenuItemExit":                                                                                           // アプリケーションを終了する
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
            this.SpreadList.AllowDragDrop = false;                                                                                      // DrugDropを禁止する
            this.SpreadList.PaintSelectionHeader = false;                                                                               // ヘッダの選択状態をしない
            sheetView.ColumnHeader.Rows[0].Height = 30;                                                                                 // Columnヘッダの高さ
            sheetView.GrayAreaBackColor = Color.White;
            sheetView.HorizontalGridLine = new GridLine(GridLineType.Flat);
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9);                                                          // 行ヘッダのFont
            sheetView.RowHeader.Columns[0].Width = 50;                                                                                  // 行ヘッダの幅を変更します
            sheetView.VerticalGridLine = new GridLine(GridLineType.Flat, Color.LightGray);
            sheetView.RemoveRows(0, sheetView.Rows.Count);
            return sheetView;
        }

        /// <summary>
        /// コントロールを初期化する
        /// </summary>
        private void InitializeControl() {
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

            this.CcPictureBox1.Image = null;
            this.CcPictureBox2.Image = null;
            /*
             * 現場(回収場所)
             */
            this.CcComboBoxWorkSiteLocation.DisplayClear();
            this.CcTextBoxWorkSiteAddress.SetEmpty();

            this.CcDateTimePickupDate.SetEmpty();
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

            this.CcDateTimeOfficeQuotationDate.Focus();
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
        /// CcPictureBox_DoubleClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CcPictureBox_DoubleClick(object sender, EventArgs e) {
            WastCollectionPaper wastCollectionPaper = new(_connectionVo, ((CcPictureBox)sender).Image);
            _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), wastCollectionPaper);
            wastCollectionPaper.ShowDialog(this);
        }

        /// <summary>
        /// WastCollectionDetail_FormClosing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WastCollectionDetail_FormClosing(object sender, FormClosingEventArgs e) {
            this.Dispose();
        }
    }
}
