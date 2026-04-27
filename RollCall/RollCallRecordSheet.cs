using Common;
using Dao;
using FarPoint.Excel;
using FarPoint.Win.Spread;
using System.Drawing.Printing;
using Vo;

namespace RollCall {
    public partial class RollCallRecordSheet : Form {

        // 印刷用ドキュメント
        private readonly PrintDocument _printDocument = new();

        // Dao
        private readonly SetMasterDao _setMasterDao;
        private readonly CarMasterDao _carMasterDao;
        private readonly StaffMasterDao _staffMasterDao;
        private readonly VehicleDispatchDetailDao _vehicleDispatchDetailDao;
        private readonly FirstRollCallDao _firstRollCallDao;
        private readonly LastRollCallDao _lastRollCallDao;

        // Vo
        private List<SetMasterVo> _listSetMasterVo = new();
        private List<CarMasterVo> _listCarMasterVo = new();
        private List<StaffMasterVo> _listStaffMasterVo = new();
        private List<VehicleDispatchDetailVo> _listVehicleDispatchDetailVo = new();
        private FirstRollCallVo _firstRollCallVo = new();
        private LastRollCallVo _lastRollCallVo = new();

        // Spread の開始行（マジックナンバー排除）
        private const int START_ROW = 4;

        /// <summary>
        /// Spread の列番号を enum 化（マジックナンバー排除）
        /// </summary>
        private enum Col {
            SetName = 1,
            CarNumber = 2,
            Driver = 3,
            RollCallMethodStart = 4,
            RollCallTimeStart = 5,
            License = 6,
            Health = 7,
            DailyInspection = 9,
            AlcoholStart = 10,
            DetectorStart = 11,
            Instruction = 12,
            RollCallNameStart = 13,

            LastPlantName = 14,
            LastPlantCount = 15,
            LastPlantTime = 16,
            ReturnTime = 17,
            RollCallMethodEnd = 18,
            AlcoholEnd = 19,
            DetectorEnd = 20,
            Memo = 21,
            RollCallNameEnd = 22
        }

        /// <summary>
        /// 点呼記録簿画面
        /// </summary>
        public RollCallRecordSheet(ConnectionVo connectionVo, Screen screen) {

            // Dao 初期化
            _setMasterDao = new(connectionVo);
            _carMasterDao = new(connectionVo);
            _staffMasterDao = new(connectionVo);
            _vehicleDispatchDetailDao = new(connectionVo);
            _firstRollCallDao = new(connectionVo);
            _lastRollCallDao = new(connectionVo);

            // マスター読込
            _listSetMasterVo = _setMasterDao.SelectAllSetMaster();
            _listCarMasterVo = _carMasterDao.SelectAllCarMaster();
            _listStaffMasterVo = _staffMasterDao.SelectAllStaffMaster(null, null, null, null);

            InitializeComponent();

            // メニューの有効化設定
            List<string> listString = new() {
                "ToolStripMenuItemFile",
                "ToolStripMenuItemExit",
                "ToolStripMenuItemExport",
                "ToolStripMenuItemExportExcel",
                "ToolStripMenuItemPrint",
                "ToolStripMenuItemPrintB4",
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);

            // 初期値設定
            this.DateTimePickerExOperationDate.SetValueJp(DateTime.Now.Date);
            this.ComboBoxExManagedSpace.Text = "本社営業所";

            // プリンタ一覧
            foreach (string item in new PrintUtility().GetAllPrinterName()) {
                this.ComboBoxExPrinterName.Items.Add(item);
            }
            this.ComboBoxExPrinterName.Text = _printDocument.PrinterSettings.PrinterName;

            // Spread 初期化
            InitializeSheetView(this.SheetViewList);

            // StatusStrip
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;

            // イベント登録
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
            this._printDocument.PrintPage += PrintDocument_PrintPage;
        }

        /// <summary>
        /// Spread の初期設定
        /// </summary>
        private SheetView InitializeSheetView(SheetView sheetView) {
            this.SpreadList.AllowDragDrop = false;
            this.SpreadList.PaintSelectionHeader = false;
            this.SpreadList.TabStrip.DefaultSheetTab.Font = new Font("Yu Gothic UI", 9);
            this.SpreadList.TabStripPolicy = TabStripPolicy.Never;
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9);
            return sheetView;
        }
        /// <summary>
        /// 点呼記録簿の更新処理
        /// </summary>
        private void ButtonExUpdate_Click(object sender, EventArgs e) {
            // Spread のクリア
            this.SheetViewList.ClearRange(START_ROW, 1, 70, 22, true);

            int managedSpaceCode = this.ComboBoxExManagedSpace.SelectedIndex + 1;

            // 点呼実施者（始業点呼）を取得
            _firstRollCallVo = _firstRollCallDao.SelectOneFirstRollCallVo(this.DateTimePickerExOperationDate.GetValue());
            if (_firstRollCallVo is null) {
                MessageBox.Show("選択日付の点呼実施者記録が存在しません。処理を終了します。",
                                "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 見出し行
            this.SheetViewList.Cells[1, 1].Text = $"{this.DateTimePickerExOperationDate.GetValueJp()}  天候：{_firstRollCallVo.Weather}  {this.ComboBoxExManagedSpace.Text}";

            // 配車データ取得
            _listVehicleDispatchDetailVo = _vehicleDispatchDetailDao.SelectAllVehicleDispatchDetail(this.DateTimePickerExOperationDate.GetValue());

            int row = 0;
            foreach (var detail in _listVehicleDispatchDetailVo
                .Where(x => x.OperationFlag && x.ManagedSpaceCode == managedSpaceCode)
                .OrderBy(x => x.StaffRollCallYmdHms1)) {
                // マスターキャッシュ
                var set = _listSetMasterVo.Find(x => x.SetCode == detail.SetCode);
                var car = _listCarMasterVo.Find(x => x.CarCode == detail.CarCode);
                var staff = _listStaffMasterVo.Find(x => x.StaffCode == detail.StaffCode1);

                /*
                 * マスターが欠けている場合はスキップ
                 * 2026/4/27追記：自家用もスキップする。
                 */
                if (set is null || (car is null || car.OtherCode == 11) || staff is null)
                    continue;

                // 第五週の休車判定
                if (detail.OperationDate.Day > 28 && set.FiveLap == false)
                    continue;

                // 車両未指定は除外
                if (detail.CarCode <= 0)
                    continue;

                int r = START_ROW + row;

                // 配車先
                this.SheetViewList.Cells[r, (int)Col.SetName].Text = $"{set.SetName1}{set.SetName2}";

                // 自動車登録番号
                this.SheetViewList.Cells[r, (int)Col.CarNumber].Text = car.RegistrationNumber;

                // 運転手
                this.SheetViewList.Cells[r, (int)Col.Driver].Text = staff.DisplayName;

                // 点呼方法
                this.SheetViewList.Cells[r, (int)Col.RollCallMethodStart].Text = "対面";

                // 点呼時刻
                this.SheetViewList.Cells[r, (int)Col.RollCallTimeStart].Text =
                    detail.StaffRollCallYmdHms1.ToString("H:mm");

                // チェック項目
                this.SheetViewList.Cells[r, (int)Col.License].Text = "✓";
                this.SheetViewList.Cells[r, (int)Col.Health].Text = "✓";
                this.SheetViewList.Cells[r, (int)Col.DailyInspection].Text = "✓";
                this.SheetViewList.Cells[r, (int)Col.AlcoholStart].Text = "✓";
                this.SheetViewList.Cells[r, (int)Col.DetectorStart].Text = "有";

                // 指示事項
                this.SheetViewList.Cells[r, (int)Col.Instruction].Text =
                    $"{_firstRollCallVo.Instruction1}\r\n\r\n{_firstRollCallVo.Instruction2}";

                // 点呼実施者（始業）
                this.SheetViewList.Cells[r, (int)Col.RollCallNameStart].Text =
                    GetRollCallName(managedSpaceCode, detail.StaffRollCallYmdHms1, false);

                /*
                 * 乗務後点呼
                 */
                if (detail.LastRollCallFlag) {
                    _lastRollCallVo = _lastRollCallDao.SelectOneLastRollCall(
                        detail.SetCode, detail.OperationDate, detail.LastRollCallYmdHms);

                    if (_lastRollCallVo != null) {
                        this.SheetViewList.Cells[r, (int)Col.LastPlantName].Text = _lastRollCallVo.LastPlantName;
                        this.SheetViewList.Cells[r, (int)Col.LastPlantCount].Text = _lastRollCallVo.LastPlantCount.ToString();
                        this.SheetViewList.Cells[r, (int)Col.LastPlantTime].Text = _lastRollCallVo.LastPlantYmdHms.ToString("HH:mm");
                        this.SheetViewList.Cells[r, (int)Col.ReturnTime].Text = _lastRollCallVo.LastRollCallYmdHms.ToString("HH:mm");

                        this.SheetViewList.Cells[r, (int)Col.RollCallMethodEnd].Text = "対面";
                        this.SheetViewList.Cells[r, (int)Col.AlcoholEnd].Text = "✓";
                        this.SheetViewList.Cells[r, (int)Col.DetectorEnd].Text = "有";

                        this.SheetViewList.Cells[r, (int)Col.Memo].Text = detail.SetMemo;

                        // 点呼実施者（終業）
                        this.SheetViewList.Cells[r, (int)Col.RollCallNameEnd].Text =
                            GetRollCallName(managedSpaceCode, detail.StaffRollCallYmdHms1, true);
                    }
                }

                row++;
            }
        }

        /// <summary>
        /// 点呼実施者名を取得する
        /// </summary>
        private string GetRollCallName(int managedSpaceCode, DateTime time, bool isLast) {
            // 三郷車庫は固定
            if (managedSpaceCode == 2)
                return _firstRollCallVo.RollCallName5;

            int sec = time.Second;

            // 始業点呼
            if (!isLast)
                return (sec % 2 == 0) ? _firstRollCallVo.RollCallName1 : _firstRollCallVo.RollCallName2;

            // 終業点呼（秒の下1桁で振り分け）
            return (sec % 10 <= 4) ? _firstRollCallVo.RollCallName3 : _firstRollCallVo.RollCallName4;
        }

        /// <summary>
        /// メニュークリックイベント
        /// </summary>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            if (sender is not ToolStripMenuItem menu) return;

            switch (menu.Name) {
                case "ToolStripMenuItemExportExcel":
                    ExportExcel();
                    break;

                case "ToolStripMenuItemPrintB4":
                    PrintB4();
                    break;

                case "ToolStripMenuItemExit":
                    Close();
                    break;
            }
        }

        /// <summary>
        /// Excel エクスポート
        /// </summary>
        private void ExportExcel() {
            string fileName = $"点呼記録簿{DateTimePickerExOperationDate.GetDate():MM月dd日}{ComboBoxExManagedSpace.Text}分";
            this.SpreadList.SaveExcel(new DirectryUtility().GetExcelDesktopPassXlsx(fileName), ExcelSaveFlags.UseOOXMLFormat);
            MessageBox.Show("デスクトップへエクスポートしました", "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// B4 印刷処理
        /// </summary>
        private void PrintB4() {
            try {
                // 出力先プリンタ
                _printDocument.PrinterSettings.PrinterName = this.ComboBoxExPrinterName.Text;

                // 縦向き
                _printDocument.DefaultPageSettings.Landscape = false;

                // B4 用紙を設定（存在しない場合は既定のまま）
                foreach (PaperSize ps in _printDocument.PrinterSettings.PaperSizes) {
                    if (ps.Kind == PaperKind.B4) {
                        _printDocument.DefaultPageSettings.PaperSize = ps;
                        break;
                    }
                }

                // 印刷設定
                _printDocument.PrinterSettings.Copies = 1;
                _printDocument.PrinterSettings.Duplex = Duplex.Default;
                _printDocument.DefaultPageSettings.Color = true;

                // 印刷開始
                _printDocument.Print();
            } catch (Exception ex) {
                MessageBox.Show($"印刷中にエラーが発生しました。\n{ex.Message}", "印刷エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 印刷ページ描画
        /// </summary>
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e) {
            Rectangle rect = new(e.PageBounds.X, e.PageBounds.Y, e.PageBounds.Width, e.PageBounds.Height);

            // Spread の描画（pageIndex=0, pageNumber=1）
            this.SpreadList.OwnerPrintDraw(e.Graphics, rect, 0, 1);

            e.HasMorePages = false;
        }

        /// <summary>
        /// 日付変更時：車庫地を初期化
        /// </summary>
        private void DateTimePickerExOperationDate_ValueChanged(object sender, EventArgs e) {
            ComboBoxExManagedSpace.SelectedIndex = 0;
        }

        /// <summary>
        /// フォーム終了時の確認
        /// </summary>
        private void RollCallRecordSheet_FormClosing(object sender, FormClosingEventArgs e) {
            DialogResult dr = MessageBox.Show("アプリケーションを終了します。よろしいですか？", "メッセージ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (dr == DialogResult.OK) {
                e.Cancel = false;
                Dispose();
            } else {
                e.Cancel = true;
            }
        }
    }
}