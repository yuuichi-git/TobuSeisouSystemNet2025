/*
 * 2024-1-5
 */
using System.Drawing.Printing;
using System.Globalization;

using Common;

using ControlEx;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace Substitute {
    public partial class SubstituteSheet : Form {
        PrintDocument _printDocument = new();  // 清掃事務所用
        DateUtility _dateUtility = new();

        /// <summary>
        /// 代番の電話番号
        /// </summary>
        string _cellphoneNumber = string.Empty;
        /// <summary>
        /// 配車先コードと車載電話番号の紐づけ
        /// </summary>
        readonly Dictionary<int, string> _dictionaryTelephoneNumber = new() { { 1310101, "090-6506-7967" },     // 千代田２
                                                                              { 1310102, "080-8868-7459" },     // 千代田６
                                                                              { 1310103, "080-8868-8023" },     // 千代田紙１
                                                                              { 1310201, "080-2202-7713" },     // 中央ペット７
                                                                              { 1310202, "080-3493-3729" },     // 中央ペット８
                                                                              { 1310207, "080-2567-3121" },     // 中央ペット１１

                                                                              //{ 1312167, "" },                // 足立１２(個人)
                                                                              //{ 1312168, "" },                // 足立１３(個人)
                                                                              { 1312162, "090-5560-0491" },     // 足立２２
                                                                              { 1312102, "090-5560-0677" },     // 足立２３
                                                                              { 1312163, "090-5560-0700" },     // 足立３７
                                                                              //{ 1312169, "" },                // 足立８(個人)

                                                                              { 1312214, "080-3493-3728" },     // 葛飾４４
                                                                              { 1312215, "080-2202-7269" },     // 葛飾５３
                                                                              //{ 1312203, "" },                // 小岩４(個人)
                                                                              { 1312216, "090-9817-8129" },     // 葛飾１３
                                                                              //{ 1312212, "" }                 // 小岩６(個人)
                                                                              };

        /*
         * 代番のセル位置の紐づけ(SheetView1用)
         */
        readonly Dictionary<int, string> _dictionarySheetView1SetName = new() { { 0, "B38" }, { 1, "B42" }, { 2, "B46" } };
        readonly Dictionary<int, string> _dictionarySheetView1Occupation = new() { { 0, "B40" }, { 1, "B44" }, { 2, "B48" } };
        readonly Dictionary<int, string> _dictionarySheetView1BeforeStaffDisplayName = new() { { 0, "D38" }, { 1, "D42" }, { 2, "D46" } };
        readonly Dictionary<int, string> _dictionarySheetView1AfterDisplayName = new() { { 0, "I38" }, { 1, "I42" }, { 2, "I46" } };
        readonly Dictionary<int, string> _dictionarySheetView1CellphoneNumber = new() { { 0, "I40" }, { 1, "I44" }, { 2, "I48" } };
        /*
         * 代番のセル位置の紐づけ(SheetView2用)
         */
        Dictionary<int, string> _dictionarySheetView2BeforeStaffDisplayName = new() { { 0, "D40" }, { 1, "D42" } };
        Dictionary<int, string> _dictionarySheetView2AfterDisplayName = new() { { 0, "H40" }, { 1, "H42" } };

        private string _cleanOfficeName;    // 清掃事務所名
        private string _officerStaffName;   // 配置担当者
        private string _cleanOfficeFaxText; // 清掃事務所FAXテキスト
        /*
         * Dao
         */
        private readonly CarMasterDao _carMasterDao;
        private readonly StaffMasterDao _staffMasterDao;
        private readonly VehicleDispatchBodyDao _vehicleDispatchBodyDao;
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;
        /*
         * 参照
         */
        private SetControl _setControl;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="setControl"></param>
        public SubstituteSheet(ConnectionVo connectionVo, SetControl setControl) {
            /*
             * Dao
             */
            _carMasterDao = new(connectionVo);
            _staffMasterDao = new(connectionVo);
            _vehicleDispatchBodyDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            /*
             * 参照
             */
            _setControl = setControl;

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
            MenuStripEx1.ChangeEnable(listString);
            /*
             * プリンターの一覧を取得後、通常使うプリンター名をセットする
             */
            foreach (string item in new PrintUtility().GetAllPrinterName()) {
                this.ComboBoxExPrinterName.Items.Add(item);
            }
            this.ComboBoxExPrinterName.Text = "SHARP BP-60C26 FAX"; // FAXを指定
            /*
             * 送信先FAX番号
             */
            this.LabelExFaxNumber.Text = string.Empty;
            /*
             * FpSpreadを初期化
             */
            this.InitializeSheetView();
            this.InitializeSheetViewKYOTUU();
            this.InitializeSheetViewSAKURADAI();
            this.InitializeSheetViewKATSUSHIKA();

            /*
             * FAXの宛先・FAX番号をセット
             */
            switch (_setControl.SetCode) {
                case 1310101: // 千代田２
                case 1310102: // 千代田６
                case 1310103: // 千代田紙１
                    _cleanOfficeName = "　日盛運輸　様";
                    _officerStaffName = "石原　由規";
                    _cleanOfficeFaxText = string.Concat("千代田区支部", "\r\n", "ＦＡＸ ０３－３６７８－２６８８");
                    Clipboard.SetText("0336782688");
                    OutputSheetViewKYOTUU(SheetView1, _setControl);
                    this.ButtonExPrint1.Enabled = true;     // PrintButton1
                    this.ButtonExPrint2.Enabled = false;    // PrintButton2
                    break;
                case 1310201: // 中央ペット７
                case 1310202: // 中央ペット８
                case 1310207: // 中央ペット１１
                    _cleanOfficeName = string.Concat("　東京都環境衛生事業協同組合", "\r\n", " 　中央区支部　様");
                    _officerStaffName = "石原　由規";
                    _cleanOfficeFaxText = string.Concat("中央区支部", "\r\n", " ＦＡＸ ０３－６２８０－５８４１");
                    Clipboard.SetText("0362805841");
                    OutputSheetViewKYOTUU(SheetView1, _setControl);
                    this.ButtonExPrint1.Enabled = true;     // PrintButton1
                    this.ButtonExPrint2.Enabled = false;    // PrintButton2
                    break;
                case 1312169: // 足立８
                case 1312167: // 足立１２
                case 1312168: // 足立１３
                case 1312161: // 足立１６
                case 1312134: // 足立１８
                case 1312162: // 足立２２
                case 1312102: // 足立２３(2025)
                case 1312164: // 足立２３(2024)
                case 1312103: // 足立２４
                case 1312163: // 足立３７
                case 1312104: // 足立３８
                case 1312105: // 足立不燃４
                    _cleanOfficeName = "　足立清掃事務所　御中";
                    _officerStaffName = "石原　由規";
                    _cleanOfficeFaxText = string.Concat("足立清掃事務所", "\r\n", " ＦＡＸ ０３－３８５７－５７４３");
                    Clipboard.SetText("0338575743");
                    OutputSheetViewKYOTUU(SheetView1, _setControl);
                    this.ButtonExPrint1.Enabled = true;     // PrintButton1
                    this.ButtonExPrint2.Enabled = false;    // PrintButton2
                    break;
                case 1312204: // 葛飾１１
                case 1312211: // 葛飾軽１２
                case 1312216: // 葛飾軽１３
                case 1312209: // 葛飾３２
                case 1312214: // 葛飾４４
                case 1312215: // 葛飾５３
                case 1312210: // 葛飾５４
                    _cleanOfficeName = "　葛飾区清掃事務所　御中";
                    _officerStaffName = "石原　由規";
                    _cleanOfficeFaxText = string.Concat("葛飾区清掃事務所", "\r\n", " ＦＡＸ ０３－３６９１－１７９７");
                    Clipboard.SetText("0336911797");
                    OutputSheetViewKATSUSHIKA(SheetView3, _setControl);
                    this.ButtonExPrint1.Enabled = true;     // PrintButton1
                    this.ButtonExPrint2.Enabled = false;    // PrintButton2
                    break;
                case 1312203: // 小岩４
                case 1312208: // 小岩５
                case 1312212: // 小岩６
                    _cleanOfficeName = "　小岩清掃事務所　御中";
                    _officerStaffName = "石原　由規";
                    _cleanOfficeFaxText = string.Concat("小岩清掃事務所", "\r\n", " ＦＡＸ ０３－３６７３－２５３５");
                    Clipboard.SetText("0336732535");
                    OutputSheetViewKYOTUU(SheetView1, _setControl);
                    this.ButtonExPrint1.Enabled = true;     // PrintButton1
                    this.ButtonExPrint2.Enabled = false;    // PrintButton2
                    break;
                case 1312011: // 桜台2-1
                case 1312012: // 桜台2-2
                case 1312006: // 桜台臨時
                    _cleanOfficeName = string.Empty;
                    _officerStaffName = "百瀨　友";
                    _cleanOfficeFaxText = string.Concat("東京都環境衛生事業協同組合 練馬区支部事務局", "\r\n", " ＦＡＸ ０３－５９４７－３４４１");
                    Clipboard.SetText("0359473441");
                    OutputSheetViewSAKURADAI(SheetView2, _setControl);
                    this.ButtonExPrint1.Enabled = true;     // PrintButton1
                    this.ButtonExPrint2.Enabled = false;    // PrintButton2
                    break;
                case 1310501: // 文京プラ軽３
                case 1310503: // 文京プラ６
                    _cleanOfficeName = string.Concat("　文京清掃事務所　御中");
                    _officerStaffName = "今村　修";
                    _cleanOfficeFaxText = string.Concat("文京清掃事務所", "\r\n", " ＦＡＸ ０３－３８１６－３９８１");
                    Clipboard.SetText("0338163981");
                    OutputSheetViewKYOTUU(SheetView1, _setControl);
                    this.ButtonExPrint1.Enabled = true;     // PrintButton1
                    this.ButtonExPrint2.Enabled = true;     // PrintButton2
                    break;
                default:
                    _cleanOfficeName = string.Empty;
                    _officerStaffName = string.Empty;
                    _cleanOfficeFaxText = string.Empty;
                    OutputSheetViewKYOTUU(SheetView1, _setControl);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        /// <param name="setControl"></param>
        private void OutputSheetViewKYOTUU(SheetView sheetView, SetControl setControl) {
            // シートを選択
            this.SpreadSubstitute.ActiveSheetIndex = 0;
            int staffPutNumber = 0;
            int arrayLoopCount = 0;
            /*
             * 各要素を取得
             */
            // 本番登録のデータ
            VehicleDispatchBodyVo vehicleDispatchBodyVo = _vehicleDispatchBodyDao.SelectOneVehicleDispatchBody(_setControl.SetCode, _setControl.OperationDate.Date, _dateUtility.GetFiscalYear(_setControl.OperationDate.Date));
            /*
             * VehicleDispatchBodyVoのCarCode・StaffCodeがゼロの場合、当該曜日の本番データは無い
             */
            if (vehicleDispatchBodyVo.CarCode == 0 && vehicleDispatchBodyVo.StaffCode1 == 0 && vehicleDispatchBodyVo.StaffCode2 == 0 && vehicleDispatchBodyVo.StaffCode3 == 0 && vehicleDispatchBodyVo.StaffCode4 == 0) {
                this.LabelExFaxNumber.BackColor = Color.Pink;
                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "当該曜日の本番データが存在しません。手打ちで入力してください。";
            } else {
                this.LabelExFaxNumber.BackColor = SystemColors.Control;
                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;
            }
            // 配車パネルのデータ
            SetMasterVo displaySetMasterVo = ((SetLabel)_setControl.DeployedSetLabel).SetMasterVo;
            CarMasterVo displayCarMasterVo = ((CarLabel)_setControl.DeployedCarLabel).CarMasterVo;

            // 日付
            CultureInfo cultureInfo = new("ja-JP", true);
            cultureInfo.DateTimeFormat.Calendar = new JapaneseCalendar();
            sheetView.Cells["G3"].Text = DateTime.Now.ToString("gg y年M月d日", cultureInfo);
            // 配置担当者
            sheetView.Cells["J13"].Text = _officerStaffName;
            // 宛先
            sheetView.Cells["B6"].Text = _cleanOfficeName;
            /*
             * 代車
             */
            if (vehicleDispatchBodyVo.CarCode != displayCarMasterVo.CarCode) { // 本番データと実際の配車データを比較
                // 本番 組数 車両ナンバー ドア番号
                sheetView.Cells["B29"].Text = displaySetMasterVo.SetName2;
                sheetView.Cells["C29"].Text = _carMasterDao.SelectOneCarMasterP(vehicleDispatchBodyVo.CarCode).RegistrationNumber;
                sheetView.Cells["F29"].Text = _carMasterDao.SelectOneCarMasterP(vehicleDispatchBodyVo.CarCode).DoorNumber.ToString();
                // 代車 車両ナンバー ドア番号
                sheetView.Cells["H29"].Text = _carMasterDao.SelectOneCarMasterP(displayCarMasterVo.CarCode).RegistrationNumber;
                sheetView.Cells["L29"].Text = _carMasterDao.SelectOneCarMasterP(displayCarMasterVo.CarCode).DoorNumber.ToString();
            }
            /*
             * 連絡先番号をセット
             */
            switch (displaySetMasterVo.SetCode) {
                case 1312167: // 足立１２(個人)
                case 1312168: // 足立１３(個人)
                case 1312169: // 足立８(個人)
                case 1312203: // 小岩４(個人)
                case 1312212: // 小岩６(個人)
                case 1310501: // 文京プラ軽３(個人)
                case 1310503: // 文京プラ６(個人)
                    // 個人携帯番号を登録
                    _cellphoneNumber = _staffMasterDao.SelectOneStaffMaster(((StaffLabel)_setControl.DeployedStaffLabel1).StaffMasterVo.StaffCode).CellphoneNumber.ToString();
                    break;
                default:
                    // 車載携帯番号を登録
                    _cellphoneNumber = _dictionaryTelephoneNumber[displaySetMasterVo.SetCode];
                    break;
            }
            /*
             * 運転手
             */
            if (_setControl.DeployedStaffLabel1 is not null && vehicleDispatchBodyVo.StaffCode1 != ((StaffLabel)_setControl.DeployedStaffLabel1).StaffMasterVo.StaffCode) {
                PutSheetView1(staffPutNumber, displaySetMasterVo.SetName, "運転手", _staffMasterDao.SelectOneStaffMaster(vehicleDispatchBodyVo.StaffCode1).DisplayName, ((StaffLabel)_setControl.DeployedStaffLabel1).StaffMasterVo.DisplayName, _cellphoneNumber);
                staffPutNumber++;  // 次の行にインクリメントする
            }

            List<int> _arrayHONBANStaffCodes = new();
            List<int> _arrayDAIBANStaffCodes = new();
            // 本番の作業員コードを格納
            if (vehicleDispatchBodyVo.StaffCode2 != 0)
                _arrayHONBANStaffCodes.Add(vehicleDispatchBodyVo.StaffCode2);
            if (vehicleDispatchBodyVo.StaffCode3 != 0)
                _arrayHONBANStaffCodes.Add(vehicleDispatchBodyVo.StaffCode3);
            if (vehicleDispatchBodyVo.StaffCode4 != 0)
                _arrayHONBANStaffCodes.Add(vehicleDispatchBodyVo.StaffCode4);
            // 代番の作業員コードを格納
            if ((StaffLabel)_setControl.DeployedStaffLabel2 is not null)
                _arrayDAIBANStaffCodes.Add(((StaffLabel)_setControl.DeployedStaffLabel2).StaffMasterVo.StaffCode);
            if ((StaffLabel)_setControl.DeployedStaffLabel3 is not null)
                _arrayDAIBANStaffCodes.Add(((StaffLabel)_setControl.DeployedStaffLabel3).StaffMasterVo.StaffCode);
            if ((StaffLabel)_setControl.DeployedStaffLabel4 is not null)
                _arrayDAIBANStaffCodes.Add(((StaffLabel)_setControl.DeployedStaffLabel4).StaffMasterVo.StaffCode);

            bool isEqual = _arrayHONBANStaffCodes.SequenceEqual(_arrayDAIBANStaffCodes);
            // Listを比較して同一で無ければ代番が存在する
            if (!isEqual) {
                List<int> staffCodes = new();
                staffCodes.AddRange(_arrayHONBANStaffCodes);
                staffCodes.AddRange(_arrayDAIBANStaffCodes);
                foreach (int staffCode in staffCodes) {
                    if (_arrayHONBANStaffCodes.Contains(staffCode) && _arrayDAIBANStaffCodes.Contains(staffCode)) {
                        _arrayHONBANStaffCodes.Remove(staffCode);
                        _arrayDAIBANStaffCodes.Remove(staffCode);
                    }
                }

                foreach (int staffCode in _arrayHONBANStaffCodes) {
                    PutSheetView1(staffPutNumber, displaySetMasterVo.SetName, "職員",
                                  _staffMasterDao.SelectOneStaffMaster(_arrayHONBANStaffCodes[arrayLoopCount]).DisplayName,
                                  _staffMasterDao.SelectOneStaffMaster(_arrayDAIBANStaffCodes[arrayLoopCount]).DisplayName,
                                  _staffMasterDao.SelectOneStaffMaster(_arrayDAIBANStaffCodes[arrayLoopCount]).CellphoneNumber);
                    staffPutNumber++; // 次の行にインクリメントする
                    arrayLoopCount++;
                }
            }
            // FAX番号他
            sheetView.Cells["H51"].Text = _cleanOfficeFaxText;
            /*
             * 送信先FAX番号
             */
            this.LabelExFaxNumber.Text = _cleanOfficeFaxText;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        /// <param name="setControl"></param>
        private void OutputSheetViewSAKURADAI(SheetView sheetView, SetControl setControl) {
            // シートを選択
            SpreadSubstitute.ActiveSheetIndex = 1;
            int staffPutNumber = 0;
            int arrayLoopCount = 0;
            /*
             * 各要素を取得
             */
            // 本番登録のデータ
            VehicleDispatchBodyVo vehicleDispatchBodyVo = _vehicleDispatchBodyDao.SelectOneVehicleDispatchBody(_setControl.SetCode, _setControl.OperationDate.Date, _dateUtility.GetFiscalYear(_setControl.OperationDate.Date));
            /*
             * VehicleDispatchBodyVoのCarCode・StaffCodeがゼロの場合、当該曜日の本番データは無い
             */
            if (vehicleDispatchBodyVo.CarCode == 0 && vehicleDispatchBodyVo.StaffCode1 == 0 && vehicleDispatchBodyVo.StaffCode2 == 0 && vehicleDispatchBodyVo.StaffCode3 == 0 && vehicleDispatchBodyVo.StaffCode4 == 0) {
                this.LabelExFaxNumber.BackColor = Color.Pink;
                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "当該曜日の本番データが存在しません。手打ちで入力してください。";
            } else {
                this.LabelExFaxNumber.BackColor = SystemColors.Control;
                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;
            }
            // 配車パネルのデータ
            SetMasterVo displaySetMasterVo = ((SetLabel)_setControl.DeployedSetLabel).SetMasterVo;
            /*
             * 作成日・組・曜日
             */
            CultureInfo cultureInfo = new("ja-JP", true);
            cultureInfo.DateTimeFormat.Calendar = new JapaneseCalendar();
            sheetView.Cells["J3"].Text = DateTime.Now.ToString("gg y年M月d日", cultureInfo);
            sheetView.Cells["H18"].Text = string.Concat(displaySetMasterVo.SetName2, " 組");
            sheetView.Cells["K18"].Text = string.Concat(_setControl.OperationDate.ToString("dddd"));
            /*
             * 運転手代番の処理
             */
            if (vehicleDispatchBodyVo.StaffCode1 != 0 && vehicleDispatchBodyVo.StaffCode1 != ((StaffLabel)_setControl.DeployedStaffLabel1).StaffMasterVo.StaffCode) {
                // 運転手名
                sheetView.Cells["D21"].Text = ((StaffLabel)_setControl.DeployedStaffLabel1).StaffMasterVo.DisplayName;
                // 携帯番号 とりあえず車載携帯がくるまでは個人携帯で登録
                sheetView.Cells["J21"].Text = ((StaffLabel)_setControl.DeployedStaffLabel1).StaffMasterVo.CellphoneNumber.Length > 0 ? ((StaffLabel)_setControl.DeployedStaffLabel1).StaffMasterVo.CellphoneNumber : "☆携帯電話未登録☆";
                // 代番
                sheetView.Cells["D25"].Text = _setControl.OperationDate.ToString("gg y年M月d日", cultureInfo);
                sheetView.Cells["I25"].Text = string.Concat(_setControl.OperationDate.ToString("gg y年M月d日", cultureInfo), " 迄");
            }
            /*
             * 作業員代番の処理
             */
            List<int> _arrayHONBANStaffCodes = new();
            List<int> _arrayDAIBANStaffCodes = new();
            // 本番の作業員コードを格納
            if (vehicleDispatchBodyVo.StaffCode2 != 0)
                _arrayHONBANStaffCodes.Add(vehicleDispatchBodyVo.StaffCode2);
            if (vehicleDispatchBodyVo.StaffCode3 != 0)
                _arrayHONBANStaffCodes.Add(vehicleDispatchBodyVo.StaffCode3);
            // 代番の作業員コードを格納
            if ((StaffLabel)_setControl.DeployedStaffLabel2 is not null)
                _arrayDAIBANStaffCodes.Add(((StaffLabel)_setControl.DeployedStaffLabel2).StaffMasterVo.StaffCode);
            if ((StaffLabel)_setControl.DeployedStaffLabel3 is not null)
                _arrayDAIBANStaffCodes.Add(((StaffLabel)_setControl.DeployedStaffLabel3).StaffMasterVo.StaffCode);

            // Listを比較して同一で無ければ代番が存在する
            if (!_arrayHONBANStaffCodes.SequenceEqual(_arrayDAIBANStaffCodes)) {
                // 代番日付
                sheetView.Cells["D37"].Text = _setControl.OperationDate.ToString("gg y年M月d日", cultureInfo);
                sheetView.Cells["I37"].Text = string.Concat(_setControl.OperationDate.ToString("gg y年M月d日", cultureInfo), " 迄");

                List<int> staffCodes = new List<int>();
                staffCodes.AddRange(_arrayHONBANStaffCodes);
                staffCodes.AddRange(_arrayDAIBANStaffCodes);
                foreach (int staffCode in staffCodes) {
                    if (_arrayHONBANStaffCodes.Contains(staffCode) && _arrayDAIBANStaffCodes.Contains(staffCode)) {
                        _arrayHONBANStaffCodes.Remove(staffCode);
                        _arrayDAIBANStaffCodes.Remove(staffCode);
                    }
                }
                foreach (int staffCode in _arrayHONBANStaffCodes) {
                    PutSheetView2(staffPutNumber,
                                  _staffMasterDao.SelectOneStaffMaster(_arrayHONBANStaffCodes[arrayLoopCount]).DisplayName,
                                  _staffMasterDao.SelectOneStaffMaster(_arrayDAIBANStaffCodes[arrayLoopCount]).DisplayName);
                    staffPutNumber++; // 次の行にインクリメントする
                    arrayLoopCount++;
                }
            }
            /*
             * 代車の処理
             */
            if (vehicleDispatchBodyVo.CarCode != 0 && vehicleDispatchBodyVo.CarCode != ((CarLabel)_setControl.DeployedCarLabel).CarMasterVo.CarCode) {
                sheetView.Cells["G45"].Text = string.Concat(((CarLabel)_setControl.DeployedCarLabel).CarMasterVo.RegistrationNumber, " (", ((CarLabel)_setControl.DeployedCarLabel).CarMasterVo.DoorNumber, ")");
                sheetView.Cells["D49"].Text = _setControl.OperationDate.ToString("gg y年M月d日", cultureInfo);
                sheetView.Cells["I49"].Text = string.Concat(_setControl.OperationDate.ToString("gg y年M月d日", cultureInfo), " 迄");
            }
            /*
             * 送信先FAX番号
             */
            this.LabelExFaxNumber.Text = _cleanOfficeFaxText;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        /// <param name="setControl"></param>
        private void OutputSheetViewKATSUSHIKA(SheetView sheetView, SetControl setControl) {
            // シートを選択
            SpreadSubstitute.ActiveSheetIndex = 2;
            int arrayLoopCount = 0;
            /*
             * 各要素を取得
             */
            // 本番登録のデータ
            VehicleDispatchBodyVo vehicleDispatchBodyVo = _vehicleDispatchBodyDao.SelectOneVehicleDispatchBody(_setControl.SetCode, _setControl.OperationDate.Date, _dateUtility.GetFiscalYear(_setControl.OperationDate.Date));
            /*
             * VehicleDispatchBodyVoのCarCode・StaffCodeがゼロの場合、当該曜日の本番データは無い
             */
            if (vehicleDispatchBodyVo.CarCode == 0 && vehicleDispatchBodyVo.StaffCode1 == 0 && vehicleDispatchBodyVo.StaffCode2 == 0 && vehicleDispatchBodyVo.StaffCode3 == 0 && vehicleDispatchBodyVo.StaffCode4 == 0) {
                this.LabelExFaxNumber.BackColor = Color.Pink;
                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "当該曜日の本番データが存在しません。手打ちで入力してください。";
            } else {
                this.LabelExFaxNumber.BackColor = SystemColors.Control;
                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;
            }
            // 配車パネルのデータ
            SetMasterVo displaySetMasterVo = ((SetLabel)_setControl.DeployedSetLabel).SetMasterVo;
            CarMasterVo displayCarMasterVo = ((CarLabel)_setControl.DeployedCarLabel).CarMasterVo;
            StaffMasterVo displayStaffMasterVo1 = ((StaffLabel)_setControl.DeployedStaffLabel1).StaffMasterVo;
            StaffMasterVo displayStaffMasterVo2 = ((StaffLabel)_setControl.DeployedStaffLabel2).StaffMasterVo;
            StaffMasterVo displayStaffMasterVo3 = ((StaffLabel)_setControl.DeployedStaffLabel3).StaffMasterVo;

            // 日付
            CultureInfo cultureInfo = new("ja-JP", true);
            cultureInfo.DateTimeFormat.Calendar = new JapaneseCalendar();
            sheetView.Cells["B2"].Text = DateTime.Now.ToString("gg　　y年　　M月　　d日　　(dddd)", cultureInfo);
            // 宛先
            sheetView.Cells["C3"].Text = _cleanOfficeName;
            /*
             * 代車
             */
            if (vehicleDispatchBodyVo.CarCode != displayCarMasterVo.CarCode) { // 本番データと実際の配車データを比較
                // 本番 組数 車両ナンバー ドア番号
                sheetView.Cells["B9"].Text = displaySetMasterVo.SetName2;
                sheetView.Cells["F9"].Text = string.Concat(_carMasterDao.SelectOneCarMasterP(vehicleDispatchBodyVo.CarCode).RegistrationNumber, " (", _carMasterDao.SelectOneCarMasterP(vehicleDispatchBodyVo.CarCode).DoorNumber.ToString(), ")");
                // 代車 車両ナンバー ドア番号
                sheetView.Cells["F11"].Text = string.Concat(_carMasterDao.SelectOneCarMasterP(displayCarMasterVo.CarCode).RegistrationNumber, " (", _carMasterDao.SelectOneCarMasterP(displayCarMasterVo.CarCode).DoorNumber.ToString(), ")");
            }
            /*
             * 運転手
             */
            if (_setControl.DeployedStaffLabel1 is not null && vehicleDispatchBodyVo.StaffCode1 != ((StaffLabel)_setControl.DeployedStaffLabel1).StaffMasterVo.StaffCode) {
                // 本番　運転手
                sheetView.Cells["J9"].Text = string.Concat(_staffMasterDao.SelectOneStaffMaster(vehicleDispatchBodyVo.StaffCode1).DisplayName);
                sheetView.Cells["J11"].Text = string.Concat(_staffMasterDao.SelectOneStaffMaster(displayStaffMasterVo1.StaffCode).DisplayName);
            }

            List<int> _arrayHONBANStaffCodes = new();
            List<int> _arrayDAIBANStaffCodes = new();
            // 本番の作業員コードを格納
            if (vehicleDispatchBodyVo.StaffCode2 != 0)
                _arrayHONBANStaffCodes.Add(vehicleDispatchBodyVo.StaffCode2);
            if (vehicleDispatchBodyVo.StaffCode3 != 0)
                _arrayHONBANStaffCodes.Add(vehicleDispatchBodyVo.StaffCode3);
            if (vehicleDispatchBodyVo.StaffCode4 != 0)
                _arrayHONBANStaffCodes.Add(vehicleDispatchBodyVo.StaffCode4);
            // 代番の作業員コードを格納
            if ((StaffLabel)_setControl.DeployedStaffLabel2 is not null)
                _arrayDAIBANStaffCodes.Add(((StaffLabel)_setControl.DeployedStaffLabel2).StaffMasterVo.StaffCode);
            if ((StaffLabel)_setControl.DeployedStaffLabel3 is not null)
                _arrayDAIBANStaffCodes.Add(((StaffLabel)_setControl.DeployedStaffLabel3).StaffMasterVo.StaffCode);
            if ((StaffLabel)_setControl.DeployedStaffLabel4 is not null)
                _arrayDAIBANStaffCodes.Add(((StaffLabel)_setControl.DeployedStaffLabel4).StaffMasterVo.StaffCode);

            bool isEqual = _arrayHONBANStaffCodes.SequenceEqual(_arrayDAIBANStaffCodes);
            // Listを比較して同一で無ければ代番が存在する
            if (!isEqual) {
                List<int> staffCodes = new();
                staffCodes.AddRange(_arrayHONBANStaffCodes);
                staffCodes.AddRange(_arrayDAIBANStaffCodes);
                foreach (int staffCode in staffCodes) {
                    if (_arrayHONBANStaffCodes.Contains(staffCode) && _arrayDAIBANStaffCodes.Contains(staffCode)) {
                        _arrayHONBANStaffCodes.Remove(staffCode);
                        _arrayDAIBANStaffCodes.Remove(staffCode);
                    }
                }

                foreach (int staffCode in _arrayHONBANStaffCodes) {
                    switch (arrayLoopCount) {
                        case 0: // 作業員１
                            sheetView.Cells["M9"].Text = string.Concat(_staffMasterDao.SelectOneStaffMaster(_arrayHONBANStaffCodes[arrayLoopCount]).DisplayName);
                            sheetView.Cells["M11"].Text = string.Concat(_staffMasterDao.SelectOneStaffMaster(_arrayDAIBANStaffCodes[arrayLoopCount]).DisplayName);
                            break;
                        case 1: // 作業員２
                            sheetView.Cells["Q9"].Text = string.Concat(_staffMasterDao.SelectOneStaffMaster(_arrayHONBANStaffCodes[arrayLoopCount]).DisplayName);
                            sheetView.Cells["Q11"].Text = string.Concat(_staffMasterDao.SelectOneStaffMaster(_arrayDAIBANStaffCodes[arrayLoopCount]).DisplayName);
                            break;
                        case 2: // 作業員３
                            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "作業員の人数が不正です。";
                            break;
                    }
                    arrayLoopCount++;
                }
            }
            /*
             * 送信先FAX番号
             */
            this.LabelExFaxNumber.Text = _cleanOfficeFaxText;
        }

        /// <summary>
        /// PutSheetView1
        /// 共通のシート
        /// </summary>
        /// <param name="rowNumber">挿入位置</param>
        /// <param name="setName">配車先名</param>
        /// <param name="occupation"></param>
        /// <param name="beforeDisplayName"></param>
        /// <param name="afterDisplayName"></param>
        /// <param name="cellphoneNumber"></param>
        private void PutSheetView1(int rowNumber, string setName, string occupation, string beforeDisplayName, string afterDisplayName, string cellphoneNumber) {
            SheetView1.Cells[_dictionarySheetView1SetName[rowNumber]].Text = string.Concat(setName, "組");
            SheetView1.Cells[_dictionarySheetView1Occupation[rowNumber]].Text = occupation;
            SheetView1.Cells[_dictionarySheetView1BeforeStaffDisplayName[rowNumber]].Text = beforeDisplayName;
            SheetView1.Cells[_dictionarySheetView1AfterDisplayName[rowNumber]].Text = afterDisplayName;
            SheetView1.Cells[_dictionarySheetView1CellphoneNumber[rowNumber]].Text = cellphoneNumber;
        }

        /// <summary>
        /// PutSheetView2
        /// 桜台用のシート
        /// </summary>
        /// <param name="rowNumber">挿入位置</param>
        /// <param name="beforeDisplayName">本番従事者名</param>
        /// <param name="afterDisplayName">代番従事者名</param>
        private void PutSheetView2(int rowNumber, string beforeDisplayName, string afterDisplayName) {
            SheetView2.Cells[_dictionarySheetView2BeforeStaffDisplayName[rowNumber]].Text = beforeDisplayName;
            SheetView2.Cells[_dictionarySheetView2AfterDisplayName[rowNumber]].Text = afterDisplayName;
        }

        /// <summary>
        /// InitializeSheetViewList1
        /// </summary>
        private void InitializeSheetViewKYOTUU() {
            // 作成日付
            SheetView1.Cells["G3"].ResetValue();
            // 送り先
            SheetView1.Cells["B6"].ResetValue();
            /*
             * 代車
             */
            // １行目
            SheetView1.Cells["B29"].ResetValue();
            SheetView1.Cells["C29"].ResetValue();
            SheetView1.Cells["F29"].ResetValue();
            SheetView1.Cells["H29"].ResetValue();
            SheetView1.Cells["L29"].ResetValue();
            // ２行目
            SheetView1.Cells["B31"].ResetValue();
            SheetView1.Cells["C31"].ResetValue();
            SheetView1.Cells["F31"].ResetValue();
            SheetView1.Cells["H31"].ResetValue();
            SheetView1.Cells["L31"].ResetValue();
            /*
             * 代番
             */
            // １行目
            SheetView1.Cells["B38"].ResetValue(); // 組
            SheetView1.Cells["B40"].ResetValue(); // 運転手・職員
            SheetView1.Cells["D38"].ResetValue(); // 本番氏名
            SheetView1.Cells["I38"].ResetValue(); // 代番氏名
            SheetView1.Cells["I40"].ResetValue(); // 代番携帯番号
            // ２行目
            SheetView1.Cells["B42"].ResetValue(); // 組
            SheetView1.Cells["B44"].ResetValue(); // 運転手・職員
            SheetView1.Cells["D42"].ResetValue(); // 本番氏名
            SheetView1.Cells["I42"].ResetValue(); // 代番氏名
            SheetView1.Cells["I44"].ResetValue(); // 代番携帯番号
            // ３行目
            SheetView1.Cells["B46"].ResetValue(); // 組
            SheetView1.Cells["B48"].ResetValue(); // 運転手・職員
            SheetView1.Cells["D46"].ResetValue(); // 本番氏名
            SheetView1.Cells["I46"].ResetValue(); // 代番氏名
            SheetView1.Cells["I48"].ResetValue(); // 代番携帯番号
            SheetView1.Cells["H51"].ResetValue(); // 送り先
        }

        /// <summary>
        /// InitializeSheetViewList2
        /// </summary>
        private void InitializeSheetViewSAKURADAI() {
            // 作成日付
            SheetView2.Cells["J3"].ResetValue();
            // １-組名・曜日
            SheetView2.Cells["H18"].ResetValue();
            SheetView2.Cells["K18"].ResetValue();
            // ２-運転手名・携帯番号・交代・代番
            SheetView2.Cells["D21"].ResetValue();
            SheetView2.Cells["J21"].ResetValue();
            SheetView2.Cells["D23"].ResetValue();
            SheetView2.Cells["D25"].ResetValue();
            SheetView2.Cells["I25"].ResetValue();
            // ３-作業員名・交代・代番
            SheetView2.Cells["D30"].ResetValue();
            SheetView2.Cells["D33"].ResetValue();
            SheetView2.Cells["H33"].ResetValue();
            SheetView2.Cells["D35"].ResetValue();
            SheetView2.Cells["H35"].ResetValue();
            SheetView2.Cells["D37"].ResetValue();
            SheetView2.Cells["I37"].ResetValue();
            SheetView2.Cells["D40"].ResetValue();
            SheetView2.Cells["H40"].ResetValue();
            SheetView2.Cells["D42"].ResetValue();
            SheetView2.Cells["H42"].ResetValue();
            // ４-収集車両・交代・代番
            SheetView2.Cells["G45"].ResetValue();
            SheetView2.Cells["D47"].ResetValue();
            SheetView2.Cells["D49"].ResetValue();
            SheetView2.Cells["I49"].ResetValue();
        }

        /// <summary>
        /// InitializeSheetViewList3
        /// </summary>
        private void InitializeSheetViewKATSUSHIKA() {
            // 日付
            SheetView3.Cells["B2"].ResetValue();
            // 担当
            SheetView3.Cells["B9"].ResetValue();
            // 車番
            SheetView3.Cells["F9"].ResetValue();
            SheetView3.Cells["F11"].ResetValue();
            // 運転手
            SheetView3.Cells["J9"].ResetValue();
            SheetView3.Cells["J11"].ResetValue();
            // 作業員１
            SheetView3.Cells["M9"].ResetValue();
            SheetView3.Cells["M11"].ResetValue();
            // 作業員２
            SheetView3.Cells["Q9"].ResetValue();
            SheetView3.Cells["Q11"].ResetValue();

        }

        private void InitializeSheetView() {
            SpreadSubstitute.AllowDragDrop = false; // DrugDropを禁止する
            SpreadSubstitute.PaintSelectionHeader = false; // ヘッダの選択状態をしない
            SpreadSubstitute.TabStrip.DefaultSheetTab.Font = new Font("Yu Gothic UI", 9);
            SpreadSubstitute.TabStripPolicy = TabStripPolicy.Never; // シートタブを非表示
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExPrint1_Click(object sender, EventArgs e) {
            // Eventを登録
            _printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);
            // 出力先プリンタを指定します。
            _printDocument.PrinterSettings.PrinterName = this.ComboBoxExPrinterName.Text;
            // 用紙の向きを設定(横：true、縦：false)
            switch (SpreadSubstitute.ActiveSheetIndex) {
                case 0:     // 共通
                case 1:     // 練馬
                    _printDocument.DefaultPageSettings.Landscape = false;
                    break;
                case 2:     // 葛飾
                    _printDocument.DefaultPageSettings.Landscape = true;
                    break;
            }
            /*
             * プリンタがサポートしている用紙サイズを調べる
             */
            foreach (PaperSize paperSize in _printDocument.PrinterSettings.PaperSizes) {
                // A4用紙に設定する
                if (paperSize.Kind == PaperKind.A4) {
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
        }

        /// <summary>
        /// 文京支部対応
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExPrint2_Click(object sender, EventArgs e) {
            // 文京支部の値をセットする
            _cleanOfficeName = string.Concat("　東京都環境衛生事業協同組合　文京支部　御中");
            _officerStaffName = "今村　修";
            _cleanOfficeFaxText = string.Concat("東京都環境衛生事業協同組合　文京支部", "\r\n", " ＦＡＸ ０３－３６０７－６０４０");
            Clipboard.SetText("0336076040");
            OutputSheetViewKYOTUU(SheetView1, _setControl);

            //メッセージキューに現在あるWindowsメッセージをすべて処理する
            System.Windows.Forms.Application.DoEvents();

            // Eventを登録
            _printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);
            // 出力先プリンタを指定します。
            _printDocument.PrinterSettings.PrinterName = this.ComboBoxExPrinterName.Text;
            // 用紙の向きを設定(横：true、縦：false)
            switch (SpreadSubstitute.ActiveSheetIndex) {
                case 0:     // 共通
                case 1:     // 練馬
                    _printDocument.DefaultPageSettings.Landscape = false;
                    break;
                case 2:     // 葛飾
                    _printDocument.DefaultPageSettings.Landscape = true;
                    break;
            }
            /*
             * プリンタがサポートしている用紙サイズを調べる
             */
            foreach (PaperSize paperSize in _printDocument.PrinterSettings.PaperSizes) {
                // A4用紙に設定する
                if (paperSize.Kind == PaperKind.A4) {
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
        }

        /// <summary>
        /// PrintDocument_PrintPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e) {
            int sheetNumber = SpreadSubstitute.ActiveSheetIndex;
            // 印刷ページ（1ページ目）の描画を行う
            Rectangle rectangle = new(e.PageBounds.X, e.PageBounds.Y, e.PageBounds.Width, e.PageBounds.Height);
            // e.Graphicsへ出力(page パラメータは、０からではなく１から始まります)
            SpreadSubstitute.OwnerPrintDraw(e.Graphics, rectangle, sheetNumber, 1);
            // 印刷終了を指定
            e.HasMorePages = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubstituteSheet_FormClosing(object sender, FormClosingEventArgs e) {
            _printDocument.Dispose();
            this.Dispose();
        }
    }
}
