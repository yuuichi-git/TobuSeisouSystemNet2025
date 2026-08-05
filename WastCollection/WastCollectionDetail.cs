/*
 * 2026-01-28
 */
using System.Text;

using CcControl;

using Common;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace WastCollection {
    public partial class WastCollectionDetail : Form {
        private DateTime _defaultDateTime = new(1900, 1, 1);
        /*
         * Column Index
         */
        /// <summary>
        /// 明細№
        /// </summary>
        private const int _colNumberOfRow = 0;
        /// <summary>
        /// 品名
        /// </summary>
        private const int _colItemName = 1;
        /// <summary>
        /// サイズ
        /// </summary>
        private const int _colItemSize = 2;
        /// <summary>
        /// 数量
        /// </summary>
        private const int _colNumberOfUnits = 3;
        /// <summary>
        /// 単価
        /// </summary>
        private const int _colUnitPrice = 4;
        /// <summary>
        /// 金額
        /// </summary>
        private const int _colTotalPrice = 5;
        /// <summary>
        /// 備考
        /// </summary>
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
        private readonly PdfUtility _pdfUtility = new();
        private CcPdfView[] _ccPdfViews = new CcPdfView[4];             // 4つの PdfViewer（メモ１～４）
        private MemoryStream[] _memoryStream = new MemoryStream[4];     // PdfViewer ごとに MemoryStream を保持する
        /*
         * Dao
         */
        private WordMasterDao _wordMasterDao;
        private WasteCollectionHeadDao _wasteCollectionHeadDao;
        private WasteCollectionBodyDao _wasteCollectionBodyDao;

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
             * Idを取得
             */
            try {
                _id = _wasteCollectionHeadDao.GetNewId();
            } catch(Exception ex) {
                MessageBox.Show(string.Concat("データの取得に失敗しました。", Environment.NewLine, ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            /*
             * InitializeControl
             */
            InitializeComponent();
            this.CcTextBoxId.Text = _id.ToString();                                                                                         // Idをセット
            this.CcComboBoxWordName.SetItems(_wordMasterDao.SelectAllWordMaster());
            this.InitializeControl();
            /*
             * FpSpread/Viewを初期化
             */
            this.InitializeSheetViewList(this.SheetViewList);
            /*
             * StatusStrip
             */
            this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "新規登録モードで開かれました。";
            /*
             * 入力用Controlsを初期化
             */
            this.InitializeMsiControls();
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
             * Idを取得
             */
            _id = id;
            /*
             * InitializeControl
             */
            InitializeComponent();
            this.CcTextBoxId.Text = id.ToString();                                                                                          // Idをセット
            this.CcComboBoxWordName.SetItems(_wordMasterDao.SelectAllWordMaster());
            this.InitializeControl();
            /*
             * FpSpread/Viewを初期化
             */
            this.InitializeSheetViewList(this.SheetViewList);
            /*
             * StatusStrip
             */
            this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "修正登録モードで開かれました。";
            /*
             * データを表示する
             */
            this.SetHeadControls(_wasteCollectionHeadDao.SelectOneWasteCollectionHead(id));
            this.SetBodyControls(this.SheetViewList, _wasteCollectionBodyDao.SelectAllWasteCollectionBody(id));
            /*
             *入力用Controlsを初期化
             */
            this.InitializeMsiControls();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CcButton_Click(object sender, EventArgs e) {
            switch(((CcButton)sender).Name) {
                case "CcButtonUpdate":
                    /*
                     * HEADの更新・追加
                     */
                    if(_wasteCollectionHeadDao.ExistenceWasteCollectionHead(_id)) {
                        try {
                            _wasteCollectionHeadDao.UpdateOneWasteCollectionHead(this.GetWasteCollectionHeadVo());
                        } catch(Exception exception) {
                            MessageBox.Show(string.Concat("WasteCollectionHeadのUPDATEに失敗しました。", Environment.NewLine, exception.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    } else {
                        try {
                            _wasteCollectionHeadDao.InsertOneWasteCollectionHead(this.GetWasteCollectionHeadVo());
                        } catch(Exception exception) {
                            MessageBox.Show(string.Concat("WasteCollectionHeadのINSERTに失敗しました。", Environment.NewLine, exception.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    this.Close();
                    break;

                case "CcButtonOk":
                    switch(_rowUpdateFlag) {
                        case false:                                                                                                         // Insertモード
                            this.AddNewRow(this.SheetViewList, this.SheetViewList.RowCount, this.GetWasteCollectionBodyVo(_id, this.SheetViewList.RowCount + 1));
                            //this.SheetViewListMsiNoReset(this.SheetViewList);
                            try {
                                _wasteCollectionBodyDao.InsertOneWasteCollectionBody(_id, this.SheetViewList.RowCount, (WasteCollectionBodyVo)this.SheetViewList.Rows[this.SheetViewList.RowCount - 1].Tag);
                            } catch(Exception exception) {
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
                            } catch(Exception exception) {
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
                    } catch(Exception exception) {
                        MessageBox.Show(string.Concat("WasteCollectionBodyのDELETEに失敗しました。", Environment.NewLine, exception.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    this.InitializeMsiControls();
                    _rowUpdateFlag = false;                                                                                                 // Updateモードを解除
                    break;

                case "CcButtonMaps1":
                    new MapUtility().MapOpen(this.CcTextBoxOfficeAddress.Text);
                    break;

                case "CcButtonMaps2":
                    new MapUtility().MapOpen(this.CcTextBoxWorkSiteAddress.Text);
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

            if(e.ColumnHeader)                                                                                                              // ヘッダーのDoubleClickを回避
                return;

            WasteCollectionBodyVo wasteCollectionBodyVo = (WasteCollectionBodyVo)this.SheetViewList.Rows[_doubleClickRowIndex].Tag;
            if(wasteCollectionBodyVo is null || wasteCollectionBodyVo.DeleteFlag == true) {
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
            /*
             * 回収日
             */
            if(wasteCollectionHeadVo.PickupDate.Date != _defaultDateTime.Date) {
                this.CcDateTimePickupDate.Value = wasteCollectionHeadVo.PickupDate;
            } else {
                this.CcDateTimePickupDate.SetEmpty();
            }
            this.CcTextBoxRemarks.Text = wasteCollectionHeadVo.Remarks;

            /*
             * PDF 表示（Image1〜4）
             */
            ShowPdfIfExists(_ccPdfViews[0], wasteCollectionHeadVo.MainPicture, 0);
            ShowPdfIfExists(_ccPdfViews[1], wasteCollectionHeadVo.SubPicture, 1);
            ShowPdfIfExists(_ccPdfViews[2], wasteCollectionHeadVo.AdditionalPicture1, 2);
            ShowPdfIfExists(_ccPdfViews[3], wasteCollectionHeadVo.AdditionalPicture2, 3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listWasteCollectionBodyVo"></param>
        private void SetBodyControls(SheetView sheetView, List<WasteCollectionBodyVo> listWasteCollectionBodyVo) {
            int rowIndex = 0;
            if(sheetView.Rows.Count > 0)                                                                                               // Rowを削除する
                sheetView.RemoveRows(0, sheetView.Rows.Count);
            try {
                foreach(WasteCollectionBodyVo wasteCollectionBodyVo in listWasteCollectionBodyVo) {
                    this.AddNewRow(sheetView, rowIndex, wasteCollectionBodyVo);
                    rowIndex++;
                }
            } catch(Exception exception) {
                MessageBox.Show(string.Concat("List<WasteCollectionBodyVo>の取得に失敗しました。", Environment.NewLine, exception.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = " データの取得に失敗しました。";
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
            sheetView.Cells[rowIndex, _colUnitPrice].ForeColor = Color.Blue;
            sheetView.Cells[rowIndex, _colUnitPrice].Value = wasteCollectionBodyVo.UnitPrice;
            sheetView.Cells[rowIndex, _colTotalPrice].ForeColor = Color.Red;
            sheetView.Cells[rowIndex, _colTotalPrice].Value = wasteCollectionBodyVo.NumberOfUnits * wasteCollectionBodyVo.UnitPrice;
            sheetView.Cells[rowIndex, _colOthers].Text = wasteCollectionBodyVo.Remarks;
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
            sheetView.Cells[rowIndex, _colUnitPrice].ForeColor = Color.Blue;
            sheetView.Cells[rowIndex, _colUnitPrice].Value = wasteCollectionBodyVo.UnitPrice;
            sheetView.Cells[rowIndex, _colTotalPrice].ForeColor = Color.Red;
            sheetView.Cells[rowIndex, _colTotalPrice].Value = wasteCollectionBodyVo.NumberOfUnits * wasteCollectionBodyVo.UnitPrice;
            sheetView.Cells[rowIndex, _colOthers].Text = wasteCollectionBodyVo.Remarks;
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
            wasteCollectionHeadVo.MainPicture = _memoryStream[0]?.ToArray() ?? Array.Empty<byte>();
            wasteCollectionHeadVo.SubPicture = _memoryStream[1]?.ToArray() ?? Array.Empty<byte>();
            wasteCollectionHeadVo.AdditionalPicture1 = _memoryStream[2]?.ToArray() ?? Array.Empty<byte>();
            wasteCollectionHeadVo.AdditionalPicture2 = _memoryStream[3]?.ToArray() ?? Array.Empty<byte>();
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
            wasteCollectionBodyVo.Remarks = this.CcTextBoxOthers.Text;
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ContextMenuStripEx1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            if(sender is not ContextMenuStrip menu)
                return;

            if(menu.SourceControl is not CcPdfView ccPdfView)
                return;

            int imageNo = GetImageNoFromViewer(ccPdfView);
            if(imageNo == 0)
                return;

            switch(e.ClickedItem.Name) {
                case "ToolStripMenuItemOpen":
                    byte[] bytes = _pdfUtility.ConvertPdfToByte(menu);
                    if(bytes is null)
                        return;

                    this.ShowPdfToViewer(ccPdfView, bytes);
                    this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "PDF を表示しました。";
                    break;

                case "ToolStripMenuItemPaste": {
                    IDataObject data = Clipboard.GetDataObject();
                    if(data == null) {
                        MessageBox.Show("クリップボードが空です。");
                        break;
                    }

                    // ★ クリップボードに画像があるか？
                    if(data.GetDataPresent(DataFormats.Bitmap)) {
                        Bitmap bmp = (Bitmap)data.GetData(DataFormats.Bitmap);
                        if(bmp == null) {
                            MessageBox.Show("画像の取得に失敗しました。");
                            break;
                        }

                        // ★ Bitmap → PDF(byte[]) に変換（PdfUtility 使用）
                        byte[] pdfBytes = _pdfUtility.ConvertImageToPdfBytes(bmp);
                        if(pdfBytes == null || pdfBytes.Length == 0) {
                            MessageBox.Show("画像を PDF に変換できませんでした。");
                            break;
                        }

                        // ★ PdfiumViewer に表示（CcPdfView）
                        this.ShowPdfToViewer(ccPdfView, pdfBytes);

                        // ★ DB 保存用に MemoryStream を保持
                        int imageNo1 = GetImageNoFromViewer(ccPdfView);
                        int index = imageNo1 - 1;

                        //_memoryStream[index]?.Dispose();
                        _memoryStream[index] = new MemoryStream(pdfBytes);

                        this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "画像を PDF として貼り付けました。";
                        break;
                    }

                    MessageBox.Show("クリップボードに画像がありません。");
                    break;
                }

                case "ToolStripMenuItemDelete":
                    this.ClearPdfViewer(ccPdfView);
                    this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "PDF を削除しました。";
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch(((ToolStripMenuItem)sender).Name) {
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
        /// PDF が存在すれば表示する（画像 byte[] にも対応）
        /// </summary>
        /// <param name="ccPdfView">表示先の PDF ビューア</param>
        /// <param name="bytes">DB から取得した byte[]（PDF または画像）</param>
        /// <param name="index">MemoryStream のインデックス（0〜3）</param>
        private void ShowPdfIfExists(CcPdfView ccPdfView, byte[] bytes, int index) {
            // ★ viewer が null の場合は何もできない
            if(ccPdfView is null)
                return;

            // ★ byte[] が null または空なら PDF をクリア
            if(bytes is null || bytes.Length == 0) {
                ClearPdfViewer(ccPdfView);
                return;
            }

            // ★ _memoryStream 配列が未初期化なら初期化
            if(_memoryStream is null)
                _memoryStream = new MemoryStream[4];

            // ★ index が範囲外なら return（安全対策）
            if(index < 0 || index >= _memoryStream.Length)
                return;

            // ★ PDF 形式かどうか判定（画像 byte[] の場合は PDF に変換）
            byte[] pdfBytes;
            if(IsPdfFormat(bytes)) {
                // そのまま PDF として扱う
                pdfBytes = bytes;
            } else {
                // ★ 画像 byte[] → Bitmap → PDF に変換
                try {
                    using(var ms = new MemoryStream(bytes))
                    using(var bmp = new Bitmap(ms)) {
                        pdfBytes = _pdfUtility.ConvertImageToPdfBytes(bmp);
                    }
                } catch {
                    MessageBox.Show("画像データが不正です。PDF に変換できませんでした。");
                    ClearPdfViewer(ccPdfView);
                    return;
                }
            }

            // ★ MemoryStream を再生成（Dispose → 新規作成）
            _memoryStream[index]?.Dispose();
            _memoryStream[index] = new MemoryStream(pdfBytes, false); // 読み取り専用

            // ★ PDF をビューアに表示
            try {
                ccPdfView.SetPdfStream(_memoryStream[index]);
            } catch(Exception ex) {
                MessageBox.Show("PDF の読み込みに失敗しました。" + Environment.NewLine + ex.Message);
                ClearPdfViewer(ccPdfView);
            }
        }

        /// <summary>
        /// byte[] が PDF 形式かどうか判定する
        /// </summary>
        private bool IsPdfFormat(byte[] bytes) {
            if(bytes.Length < 5)
                return false;

            // PDF は必ず "%PDF-" で始まる
            string header = Encoding.ASCII.GetString(bytes, 0, 5);
            return header.StartsWith("%PDF-");
        }

        /// <summary>
        /// PdfViewer がどの ImageNo に対応しているかを返す
        /// </summary>
        private int GetImageNoFromViewer(CcPdfView viewer) {
            for(int i = 0; i < _ccPdfViews.Length; i++) {
                if(_ccPdfViews[i] == viewer) {
                    return i + 1;
                }
            }
            return 0;
        }

        /// <summary>
        /// 指定された PdfViewer に PDF（byte[]）を表示する
        /// </summary>
        private void ShowPdfToViewer(CcPdfView ccPdfView, byte[] pdfBytes) {
            int imageNo = GetImageNoFromViewer(ccPdfView);
            if(imageNo == 0)
                return;

            int index = imageNo - 1;

            _memoryStream[index]?.Dispose();
            _memoryStream[index] = new MemoryStream(pdfBytes);

            ccPdfView.Unload();
            ccPdfView.SetPdfStream(_memoryStream[index]);
        }

        /// <summary>
        /// PDF ビューアをクリアする（Null 安全化）
        /// </summary>
        private void ClearPdfViewer(CcPdfView ccPdfView) {
            if(ccPdfView is null)
                return;

            int imageNo = GetImageNoFromViewer(ccPdfView);
            if(imageNo == 0)
                return;

            int index = imageNo - 1;

            // MemoryStream を破棄
            _memoryStream[index]?.Dispose();
            _memoryStream[index] = null;

            // viewer をアンロード
            try {
                ccPdfView.Unload();
            } catch {
                // Unload が失敗してもアプリは落とさない
            }
        }

        /// <summary>
        /// WastCollectionDetail_FormClosing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WastCollectionDetail_FormClosing(object sender, FormClosingEventArgs e) {
            DialogResult dialogResult = MessageBox.Show("アプリケーションを終了します。よろしいですか？", "メッセージ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            switch(dialogResult) {
                case DialogResult.OK:
                    e.Cancel = false;
                    Dispose();
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }

        /// <summary>
        /// コントロールを初期化する
        /// </summary>
        private void InitializeControl() {
            /*
             * MenuStrip
             */
            List<string> listString = new() {
                "ToolStripMenuItemFile",
                "ToolStripMenuItemExit",
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;

            /*
             * PDF 表示エリア
             */
            TabPage[] tabPages = new TabPage[4];
            tabPages[0] = this.TabPage1;
            tabPages[1] = this.TabPage2;
            tabPages[2] = this.TabPage3;
            tabPages[3] = this.TabPage4;
            /*
             * 4つの CcPdfView を生成して TabPage に配置
             */
            for(int i = 0; i < 4; i++) {
                _ccPdfViews[i] = new();
                tabPages[i].Controls.Add(_ccPdfViews[i]);
                _ccPdfViews[i].ContextMenuStrip = this.CcContextMenuStrip1;                                                                 // 共通の ContextMenuStrip を設定
            }

            this.CcDateTimeOfficeQuotationDate.SetToday();
            this.CcComboBoxWordName.SelectedIndex = 20;                                                                                     // 足立区
            /*
             * 本社(依頼主)
             */
            this.CcComboBoxOfficeCompanyName.DisplayEmpty();
            this.CcTextBoxOfficeContactPerson.SetEmpty();
            this.CcTextBoxOfficeAddress.SetEmpty();
            this.CcTextBoxOfficeTelephoneNumber.SetEmpty();
            this.CcTextBoxOfficeCellphoneNumber.SetEmpty();
            /*
             * PDF クリア
             */
            for(int i = 0; i < 4; i++) {
                ClearPdfViewer(_ccPdfViews[i]);
            }
            /*
             * 現場(回収場所)
             */
            this.CcComboBoxWorkSiteLocation.DisplayEmpty();
            this.CcTextBoxWorkSiteAddress.SetEmpty();

            this.CcDateTimePickupDate.SetEmpty();
            this.CcTextBoxRemarks.SetEmpty();
            /*
             * 入力項目
             */
            this.CcTextBoxNumber.SetEmpty();
            //this.CcComboBoxItemName.DisplayClear();
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
            this.InitializeCcComboBoxItemName();                                                                                            // 明細入力の品名ComboBoxを初期化
            this.CcTextBoxItemSize.SetEmpty();
            this.CcNumericUpDownNumberOfUnits.Value = 0;
            this.CcNumericUpDownUnitPrice.Value = 0;
            this.CcNumericUpDownAmount.Value = 0;
            this.CcTextBoxOthers.SetEmpty();
            this.CcButtonDelete.Enabled = false;
        }

        /// <summary>
        /// CcComboBoxItemNameを初期化する
        /// </summary>
        private void InitializeCcComboBoxItemName() {
            this.CcComboBoxItemName.Items.Clear();
            foreach(string data in _wasteCollectionBodyDao.SelectGroupItemName())
                this.CcComboBoxItemName.Items.Add(data);
            this.CcComboBoxItemName.DisplayEmpty();
        }
    }
}
