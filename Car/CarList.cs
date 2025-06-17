using Common;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace Car {
    public partial class CarList : Form {
        /*
         * SPREADのColumnの番号
         */
        /// <summary>
        /// 車両コード
        /// </summary>
        private const int _colCarCode = 0;
        /// <summary>
        /// 緊急車両登録フラグ
        /// </summary>
        private const int _colEmergencyVehicle = 1;
        /// <summary>
        /// 緊急車両登録有効期限
        /// </summary>
        private const int _colEmergencyVehicleDate = 2;
        /// <summary>
        /// 自動車登録番号1
        /// </summary>
        private const int _colRegistrationNumber1 = 3;
        /// <summary>
        /// 自動車登録番号2(4桁の数字部分)
        /// </summary>
        private const int _colRegistrationNumber2 = 4;
        /// <summary>
        /// Door番号
        /// </summary>
        private const int _colDoorNumber = 5;
        /// <summary>
        /// 区分(雇上・区契・一般)
        /// </summary>
        private const int _colClassificationName = 6;
        /// <summary>
        /// 車庫地
        /// </summary>
        private const int _colGarageName = 7;
        /// <summary>
        /// 名称(配車パネル)
        /// </summary>
        private const int _colDisguiseKind_1 = 8;
        /// <summary>
        /// 名称(事故報告書)
        /// </summary>
        private const int _colDisguiseKind_2 = 9;
        /// <summary>
        /// 名称(整備)
        /// </summary>
        private const int _colDisguiseKind_3 = 10;
        /// <summary>
        /// 登録年月日
        /// </summary>
        private const int _colRegistrationDate = 11;
        /// <summary>
        /// 初年度登録年月
        /// </summary>
        private const int _colFirstRegistrationDate = 12;
        /// <summary>
        /// 自動車の種類
        /// </summary>
        private const int _colCarKindName = 13;
        /// <summary>
        /// 用途
        /// </summary>
        private const int _colCarUse = 14;
        /// <summary>
        /// 自家用・事業用の別
        /// </summary>
        private const int _colOtherCode = 15;
        /// <summary>
        /// 車体の形状
        /// </summary>
        private const int _colShapeName = 16;
        /// <summary>
        /// 有効期限の満了する日
        /// </summary>
        private const int _colExpirationDate = 17;
        /// <summary>
        /// 備考
        /// </summary>
        private const int _colRemarks = 18;
        /*
         * インスタンス作成
         */
        private readonly ScreenForm _screenForm = new();
        /*
         * Dao
         */
        private readonly CarMasterDao _carMasterDao;
        private readonly ClassificationMasterDao _classificationMasterDao;
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;
        /*
         * Dictionary
         */
        private readonly Dictionary<int, string> _dictionaryClassification = new();
        /// <summary>
        /// 0:該当なし 1:足立 2:三郷 3:産廃車庫
        /// </summary>
        private readonly Dictionary<int, string> _dictionaryGarageName = new Dictionary<int, string> { { 0, "該当なし" }, { 1, "本社" }, { 2, "三郷" }, { 3, "産廃車庫" } };
        /// <summary>
        /// 10:軽自動車 11:小型 12:普通
        /// </summary>
        private readonly Dictionary<int, string> _dictionaryCarKindName = new Dictionary<int, string> { { 10, "軽自動車" }, { 11, "小型" }, { 12, "普通" } };
        /// <summary>
        /// 10:事業用 11:自家用
        /// </summary>
        private readonly Dictionary<int, string> _dictionaryOtherName = new Dictionary<int, string> { { 10, "事業用" }, { 11, "自家用" } };
        /// <summary>
        /// 10:キャブオーバー 11:塵芥車 12:ダンプ 13:コンテナ専用 14:脱着装置付コンテナ専用車 15:粉粒体運搬車 16:糞尿車 17:清掃車 18:番
        /// </summary>
        private readonly Dictionary<int, string> _dictionaryShapeName = new Dictionary<int, string> { { 10, "キャブオーバー" }, { 11, "塵芥車" }, { 12, "ダンプ" }, { 13, "コンテナ専用" }, { 14, "脱着装置付コンテナ専用車" }, { 15, "粉粒体運搬車" }, { 16, "糞尿車" }, { 17, "清掃車" }, { 18, "バン" } };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public CarList(ConnectionVo connectionVo, Screen screen) {
            /*
             * Dao
             */
            _carMasterDao = new(connectionVo);
            _classificationMasterDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            /*
             * Dictionary
             */
            foreach (ClassificationMasterVo classificationMasterVo in _classificationMasterDao.SelectAllClassificationMasterVo())
                _dictionaryClassification.Add(classificationMasterVo.Code, classificationMasterVo.Name);
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

            this.InitializeSheetViewList1(this.SheetViewList);
            this.InitializeSheetViewList2(this.SheetViewList東京都運輸事業者向け燃料費高騰緊急対策事業支援金);
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
            switch (this.SpreadList.ActiveSheet.SheetName) {
                case "車両台帳":
                    this.SetSheetViewList1(this.SheetViewList);
                    break;
                case "東京都運輸事業者向け燃料費高騰緊急対策事業支援金":
                    this.SetSheetViewList2(this.SheetViewList東京都運輸事業者向け燃料費高騰緊急対策事業支援金);
                    break;

            }
        }

        int spreadListTopRow1 = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        private void SetSheetViewList1(SheetView sheetView) {
            List<CarMasterVo> _listCarMasterVo = new();
            if (CheckBoxExDeleteFlag.Checked) {                                                                                                                                         // 削除済のレコードも表示
                _listCarMasterVo = _carMasterDao.SelectAllCarMaster();
            } else {
                _listCarMasterVo = _carMasterDao.SelectAllCarMaster().FindAll(x => x.DeleteFlag == false);
            }
            SpreadList.SuspendLayout();                                                                                                                                                 // 非活性化
            spreadListTopRow1 = SpreadList.GetViewportTopRow(0);                                                                                                                         // 先頭行（列）インデックスを取得
            if (sheetView.Rows.Count > 0)                                                                                                                                               // Rowを削除する
                sheetView.RemoveRows(0, sheetView.Rows.Count);

            int i = 0;
            foreach (CarMasterVo carMasterVo in _listCarMasterVo.OrderBy(x => x.RegistrationNumber4)) {
                sheetView.Rows.Add(i, 1);
                sheetView.RowHeader.Columns[0].Label = (i + 1).ToString();                                                                                                              // Rowヘッダ
                sheetView.Rows[i].Height = 22;                                                                                                                                          // Rowの高さ
                sheetView.Rows[i].Resizable = false;                                                                                                                                    // RowのResizableを禁止
                sheetView.Rows[i].ForeColor = !carMasterVo.DeleteFlag ? Color.Black : Color.Red;                                                                                        // 削除済レコードは赤色で表示する

                sheetView.Cells[i, _colCarCode].Value = carMasterVo.CarCode;
                sheetView.Cells[i, _colEmergencyVehicle].Value = carMasterVo.EmergencyVehicleFlag;
                if (carMasterVo.EmergencyVehicleFlag) {
                    sheetView.Cells[i, _colEmergencyVehicleDate].ForeColor = carMasterVo.EmergencyVehicleDate.Date < DateTime.Now.Date ? Color.Red : Color.Black;
                    sheetView.Cells[i, _colEmergencyVehicleDate].Value = carMasterVo.EmergencyVehicleDate.Date;
                }
                sheetView.Cells[i, _colRegistrationNumber1].Text = string.Concat(carMasterVo.RegistrationNumber1, carMasterVo.RegistrationNumber2, carMasterVo.RegistrationNumber3);
                sheetView.Cells[i, _colRegistrationNumber2].Text = carMasterVo.RegistrationNumber4.ToString();
                sheetView.Cells[i, _colDoorNumber].Text = carMasterVo.DoorNumber.ToString("###");
                sheetView.Cells[i, _colClassificationName].Text = _dictionaryClassification[carMasterVo.ClassificationCode];
                sheetView.Cells[i, _colGarageName].Text = _dictionaryGarageName[carMasterVo.GarageCode];
                sheetView.Cells[i, _colDisguiseKind_1].Text = carMasterVo.DisguiseKind1;
                sheetView.Cells[i, _colDisguiseKind_2].Text = carMasterVo.DisguiseKind2;
                sheetView.Cells[i, _colDisguiseKind_3].Text = carMasterVo.DisguiseKind3;
                sheetView.Cells[i, _colRegistrationDate].Value = carMasterVo.RegistrationDate.Date;
                sheetView.Cells[i, _colFirstRegistrationDate].Value = carMasterVo.FirstRegistrationDate.Date;
                sheetView.Cells[i, _colCarKindName].Text = _dictionaryCarKindName[carMasterVo.CarKindCode];
                sheetView.Cells[i, _colCarUse].Text = carMasterVo.CarUse;
                sheetView.Cells[i, _colOtherCode].Text = _dictionaryOtherName[carMasterVo.OtherCode];
                sheetView.Cells[i, _colShapeName].Text = _dictionaryShapeName[carMasterVo.ShapeCode];
                sheetView.Cells[i, _colExpirationDate].ForeColor = carMasterVo.ExpirationDate.Date < DateTime.Now.Date ? Color.Red : Color.Black;
                sheetView.Cells[i, _colExpirationDate].Value = carMasterVo.ExpirationDate.Date;
                sheetView.Cells[i, _colRemarks].Text = carMasterVo.Remarks;
                i++;
            }
            SpreadList.SetViewportTopRow(0, spreadListTopRow1);                                                                                                                          // 先頭行（列）インデックスをセット

            SpreadList.ResumeLayout();                                                                                                                                                  // 活性化
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(" ", i, " 件");
        }

        int spreadListTopRow2 = 0;
        /// <summary>
        /// PutSheetViewList
        /// </summary>
        private void SetSheetViewList2(SheetView sheetView) {
            List<CarMasterVo> _listCarMasterVo = new();
            if (CheckBoxExDeleteFlag.Checked) {                                                                                                                                         // 削除済のレコードも表示
                _listCarMasterVo = _carMasterDao.SelectAllCarMaster().FindAll(x => x.OtherCode == 10);
            } else {
                _listCarMasterVo = _carMasterDao.SelectAllCarMaster().FindAll(x => x.OtherCode == 10 && x.DeleteFlag == false);
            }
            SpreadList.SuspendLayout();                                                                                                                                                 // 非活性化
            spreadListTopRow2 = SpreadList.GetViewportTopRow(0);                                                                                                                        // 先頭行（列）インデックスを取得
            if (sheetView.Rows.Count > 0)                                                                                                                                               // Rowを削除する
                sheetView.RemoveRows(0, sheetView.Rows.Count);

            int i = 0;
            foreach (CarMasterVo carMasterVo in _listCarMasterVo.OrderBy(x => x.CarKindCode).ThenBy(x => x.RegistrationNumber4)) {
                sheetView.Rows.Add(i, 1);
                sheetView.RowHeader.Columns[0].Label = (i + 1).ToString();                                                                                                              // Rowヘッダ
                sheetView.Rows[i].Height = 22;                                                                                                                                          // Rowの高さ
                sheetView.Rows[i].Resizable = false;                                                                                                                                    // RowのResizableを禁止
                sheetView.Rows[i].ForeColor = !carMasterVo.DeleteFlag ? Color.Black : Color.Red;                                                                                        // 削除済レコードは赤色で表示する

                sheetView.Cells[i, _colCarCode].Value = carMasterVo.CarCode;
                sheetView.Cells[i, _colEmergencyVehicle].Value = carMasterVo.EmergencyVehicleFlag;
                if (carMasterVo.EmergencyVehicleFlag) {
                    sheetView.Cells[i, _colEmergencyVehicleDate].ForeColor = carMasterVo.EmergencyVehicleDate.Date < DateTime.Now.Date ? Color.Red : Color.Black;
                    sheetView.Cells[i, _colEmergencyVehicleDate].Value = carMasterVo.EmergencyVehicleDate.Date;
                }
                sheetView.Cells[i, 0].Text = carMasterVo.RegistrationNumber1.ToString();
                sheetView.Cells[i, 1].Text = carMasterVo.RegistrationNumber2.ToString();
                sheetView.Cells[i, 2].Text = carMasterVo.RegistrationNumber3.ToString();
                sheetView.Cells[i, 3].Text = carMasterVo.RegistrationNumber4.ToString().PadLeft(4, '・');
                switch (carMasterVo.CarKindCode) {
                    case 10:    // 軽自動車
                        sheetView.Cells[i, 4].Text = "軽自動車";
                        break;
                    case 11:    // 小型
                    case 12:    // 普通
                        sheetView.Cells[i, 4].Text = "普通・小型";
                        break;
                }
                sheetView.Cells[i, 5].Text = "所有";
                sheetView.Cells[i, 6].Text = carMasterVo.OtherCode == 10 ? "事業用" : "自家用"
                ;
                i++;
            }
            SpreadList.SetViewportTopRow(0, spreadListTopRow2);                                                                                                                          // 先頭行（列）インデックスをセット

            SpreadList.ResumeLayout();                                                                                                                                                  // 活性化
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(" ", i, " 件");
        }

        /// <summary>
        /// 車両台帳
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns></returns>
        private SheetView InitializeSheetViewList1(SheetView sheetView) {
            this.SpreadList.AllowDragDrop = false;                                                      // DrugDropを禁止する
            this.SpreadList.PaintSelectionHeader = false;                                               // ヘッダの選択状態をしない
            this.SpreadList.TabStripPolicy = TabStripPolicy.Always;                                     // シートタブを表示
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
        /// 東京都運輸事業者向け燃料費高騰緊急対策事業支援金
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns></returns>
        private SheetView InitializeSheetViewList2(SheetView sheetView) {
            this.SpreadList.AllowDragDrop = false;                                                      // DrugDropを禁止する
            this.SpreadList.PaintSelectionHeader = false;                                               // ヘッダの選択状態をしない
            this.SpreadList.TabStrip.DefaultSheetTab.Font = new Font("Yu Gothic UI", 9);
            this.SpreadList.TabStripPolicy = TabStripPolicy.Always;                                     // シートタブを表示
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
        private void SpreadList_CellDoubleClick(object sender, CellClickEventArgs e) {
            /*
             * ヘッダーのDoubleClickを回避
             */
            if (e.Row < 0)
                return;
            /*
             * ActiveSheetにより処理を変更
             */
            switch (this.SpreadList.ActiveSheet.SheetName) {
                case "車両台帳":
                    /*
                     * CarDetailを表示する
                     */
                    CarDetail carDetail = new(_connectionVo, (int)SheetViewList.Cells[e.Row, _colCarCode].Value);
                    _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), carDetail);
                    carDetail.ShowDialog(this);
                    break;
                case "東京都運輸事業者向け燃料費高騰緊急対策事業支援金":

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
                case "ToolStripMenuItemInsertNewRecord":
                    CarDetail carDetail = new(_connectionVo); // CarDetailを表示する
                    _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), carDetail);
                    carDetail.ShowDialog(this);
                    break;
                case "ToolStripMenuItemExit": // アプリケーションを終了する
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarList_FormClosing(object sender, FormClosingEventArgs e) {
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
