/*
 * 2025-1-11
 */
using Common;

using ControlEx;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace Staff {

    public partial class StaffList : Form {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly DateUtility _dateUtility = new();
        private readonly ScreenForm _screenForm = new();
        /*
         * SPREADのColumnの番号
         */
        /// <summary>
        /// 所属
        /// </summary>
        private const int colBelongs = 0;
        /// <summary>
        /// 形態
        /// </summary>
        private const int colJobForm = 1;
        /// <summary>
        /// 職種
        /// </summary>
        private const int colOccupation = 2;
        /// <summary>
        /// 配車の対象かどうか
        /// </summary>
        private const int colVehicleDispatchTarget = 3;
        /// <summary>
        /// <summary>
        /// 社員CD
        /// </summary>
        private const int colCode = 4;
        /// <summary>
        /// 氏名
        /// </summary>
        private const int colName = 5;
        /// <summary>
        /// カナ
        /// </summary>
        private const int colNameKana = 6;
        /// <summary>
        /// 生年月日
        /// </summary>
        private const int colBirthDate = 7;
        /// <summary>
        /// 年齢
        /// </summary>
        private const int colFullAge = 8;
        /// <summary>
        /// 雇用年月日
        /// </summary>
        private const int colEmploymentDate = 9;
        /// <summary>
        /// 勤続年数
        /// </summary>
        private const int colServiceDate = 10;
        /// <summary>
        /// 東環保カード有無
        /// </summary>
        private const int colToukanpoCard = 11;
        /// <summary>
        /// 法定１２項目の研修対象
        /// </summary>
        private const int colLegalTwelveItemFlag = 12;
        /// <summary>
        /// 東環保研修の対象
        /// </summary>
        private const int colToukanpoFlag = 13;
        /// <summary>
        /// 免許証期限
        /// </summary>
        private const int colLicensExpirationDate = 14;
        /// <summary>
        /// 初任
        /// </summary>
        private const int colFirstTerm = 15;
        /// <summary>
        /// 適齢
        /// </summary>
        private const int colSuitableAge = 16;
        /// <summary>
        /// 事故件数
        /// </summary>
        private const int colCarAccidentCount = 17;
        /// <summary>
        /// １年以内の健康診断
        /// </summary>
        private const int colMedicalExaminationDate = 18;
        /// <summary>
        /// 現住所
        /// </summary>
        private const int colCurrentAddress = 19;
        /// <summary>
        /// 健康保険
        /// </summary>
        private const int colHealthInsuranceNumber = 20;
        /// <summary>
        /// 厚生年金
        /// </summary>
        private const int colWelfarePensionNumber = 21;
        /// <summary>
        /// 雇用保険
        /// </summary>
        private const int colEmploymentInsuranceNumber = 22;
        /// <summary>
        /// 労災保険
        /// </summary>
        private const int colWorkerAccidentInsuranceNumber = 23;
        /*
         * 参照
         */
        private Screen _screen;
        /*
         * Dao
         */
        private BelongsMasterDao _belongsMasterDao;
        private JobFormMasterDao _jobFormMasterDao;
        private OccupationMasterDao _occupationMasterDao;
        private StaffMasterDao _staffMasterDao;
        private ToukanpoTrainingCardDao _toukanpoTrainingCardDao;
        private LicenseMasterDao _licenseMasterDao;
        private StaffProperDao _staffProperDao;
        private CarAccidentMasterDao _carAccidentMasterDao;
        private StaffMedicalExaminationDao _staffMedicalExaminationDao;
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;
        private List<StaffMasterVo>? _listStaffMasterVo;
        private List<StaffMasterVo>? _findListStaffMasterVo;
        /*
         * Dictionary
         */
        private readonly Dictionary<int, string> _dictionaryBelongs = new();
        private readonly Dictionary<int, string> _dictionaryOccupation = new();
        private readonly Dictionary<int, string> _dictionaryJobForm = new();

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        public StaffList(ConnectionVo connectionVo, Screen screen) {
            /*
             * 参照
             */
            _screen = screen;
            /*
             * Dao
             */
            _belongsMasterDao = new(connectionVo);
            _jobFormMasterDao = new(connectionVo);
            _occupationMasterDao = new(connectionVo);
            _staffMasterDao = new(connectionVo);
            _toukanpoTrainingCardDao = new(connectionVo);
            _licenseMasterDao = new(connectionVo);
            _staffProperDao = new(connectionVo);
            _carAccidentMasterDao = new(connectionVo);
            _staffMedicalExaminationDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            _listStaffMasterVo = null;
            _findListStaffMasterVo = null;
            /*
             * Dictionary
             */
            foreach (BelongsMasterVo belongsMasterVo in _belongsMasterDao.SelectAllBelongsMaster())
                _dictionaryBelongs.Add(belongsMasterVo.Code, belongsMasterVo.Name);
            foreach (OccupationMasterVo occupationMasterVo in _occupationMasterDao.SelectAllOccupationMaster())
                _dictionaryOccupation.Add(occupationMasterVo.Code, occupationMasterVo.Name);
            foreach (JobFormMasterVo jobFormMasterVo in _jobFormMasterDao.SelectAllJobFormMaster())
                _dictionaryJobForm.Add(jobFormMasterVo.Code, jobFormMasterVo.Name);
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
             * FpSpread/Viewを初期化
             */
            this.InitializeSheetView(SheetViewList);
            this.InitializeSheetView(SheetViewMedical);
            this.InitializeSheetView(SheetViewDriver);
            this.InitializeSheetView(SheetViewToukanpo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEx_Click(object sender, EventArgs e) {
            switch (((ButtonEx)sender).Name) {
                case "ButtonExUpdate":
                    _listStaffMasterVo = _staffMasterDao.SelectAllStaffMaster(CreateArray(GroupBoxExBelongs), CreateArray(GroupBoxExJobForm), CreateArray(GroupBoxExOccupation), this.CheckBoxExRetirementFlag.Checked);
                    switch (this.SpreadList.ActiveSheet.SheetName) {
                        case "従事者リスト":
                            this.PutSheetViewList(this.SheetViewList);
                            break;
                        case "健康診断用リスト":

                            break;
                        case "運転者リスト":
                            this.PutSheetViewDriver(this.SheetViewDriver);
                            break;
                        case "東環保研修対象者リスト":

                            break;
                    }
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
                case "":

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
        private void TabControlEx1_Click(object sender, EventArgs e) {
            if (_listStaffMasterVo is not null) {
                switch (this.SpreadList.ActiveSheet.SheetName) {
                    case "従事者リスト":
                        this.PutSheetViewList(this.SheetViewList);
                        break;
                    case "健康診断用リスト":

                        break;
                    case "運転者リスト":
                        this.PutSheetViewDriver(this.SheetViewDriver);
                        break;
                    case "東環保研修対象者リスト":

                        break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpreadList_CellDoubleClick(object sender, CellClickEventArgs e) {
            // ダブルクリックされたのが従事者リストで無ければReturnする
            if (((FpSpread)sender).ActiveSheet.SheetName != "従事者リスト")
                return;
            // ヘッダーのDoubleClickを回避
            if (e.ColumnHeader)
                return;
            // Shiftが押された場合
            if ((ModifierKeys & Keys.Shift) == Keys.Shift) {
                StaffPaper staffPaper = new(_connectionVo, ((StaffMasterVo)SheetViewList.Rows[e.Row].Tag).StaffCode);
                _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), staffPaper);
                staffPaper.Show(this);
                return;
            }
            // 修飾キーが無い場合
            StaffDetail staffDetail = new(_connectionVo, ((StaffMasterVo)SheetViewList.Rows[e.Row].Tag).StaffCode);
            _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), staffDetail);
            staffDetail.Show(this);
        }

        int _spreadListTopRow = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        private void PutSheetViewList(SheetView sheetView) {
            int rowCount = 0;
            this.SpreadList.SuspendLayout();// Spread 非活性化
            _spreadListTopRow = this.SpreadList.GetViewportTopRow(0);// 先頭行（列）インデックスを取得
            if (sheetView.Rows.Count > 0)// Rowを削除する
                sheetView.RemoveRows(0, sheetView.Rows.Count);
            _findListStaffMasterVo = TabControlEx1.SelectedTab.Text switch {
                "あ行" => _listStaffMasterVo?.FindAll(x => x.NameKana.StartsWith("ア") || x.NameKana.StartsWith("イ") || x.NameKana.StartsWith("ウ") || x.NameKana.StartsWith("エ") || x.NameKana.StartsWith("オ")),
                "か行" => _listStaffMasterVo?.FindAll(x => x.NameKana.StartsWith("カ") || x.NameKana.StartsWith("ガ") || x.NameKana.StartsWith("キ") || x.NameKana.StartsWith("ギ") || x.NameKana.StartsWith("ク") || x.NameKana.StartsWith("グ") || x.NameKana.StartsWith("ケ") || x.NameKana.StartsWith("ゲ") || x.NameKana.StartsWith("コ") || x.NameKana.StartsWith("ゴ")),
                "さ行" => _listStaffMasterVo?.FindAll(x => x.NameKana.StartsWith("サ") || x.NameKana.StartsWith("シ") || x.NameKana.StartsWith("ス") || x.NameKana.StartsWith("セ") || x.NameKana.StartsWith("ソ")),
                "た行" => _listStaffMasterVo?.FindAll(x => x.NameKana.StartsWith("タ") || x.NameKana.StartsWith("ダ") || x.NameKana.StartsWith("チ") || x.NameKana.StartsWith("ツ") || x.NameKana.StartsWith("テ") || x.NameKana.StartsWith("デ") || x.NameKana.StartsWith("ト") || x.NameKana.StartsWith("ド")),
                "な行" => _listStaffMasterVo?.FindAll(x => x.NameKana.StartsWith("ナ") || x.NameKana.StartsWith("ニ") || x.NameKana.StartsWith("ヌ") || x.NameKana.StartsWith("ネ") || x.NameKana.StartsWith("ノ")),
                "は行" => _listStaffMasterVo?.FindAll(x => x.NameKana.StartsWith("ハ") || x.NameKana.StartsWith("パ") || x.NameKana.StartsWith("バ") || x.NameKana.StartsWith("ヒ") || x.NameKana.StartsWith("ビ") || x.NameKana.StartsWith("フ") || x.NameKana.StartsWith("ブ") || x.NameKana.StartsWith("ヘ") || x.NameKana.StartsWith("ベ") || x.NameKana.StartsWith("ホ")),
                "ま行" => _listStaffMasterVo?.FindAll(x => x.NameKana.StartsWith("マ") || x.NameKana.StartsWith("ミ") || x.NameKana.StartsWith("ム") || x.NameKana.StartsWith("メ") || x.NameKana.StartsWith("モ")),
                "や行" => _listStaffMasterVo?.FindAll(x => x.NameKana.StartsWith("ヤ") || x.NameKana.StartsWith("ユ") || x.NameKana.StartsWith("ヨ")),
                "ら行" => _listStaffMasterVo?.FindAll(x => x.NameKana.StartsWith("ラ") || x.NameKana.StartsWith("リ") || x.NameKana.StartsWith("ル") || x.NameKana.StartsWith("レ") || x.NameKana.StartsWith("ロ")),
                "わ行" => _listStaffMasterVo?.FindAll(x => x.NameKana.StartsWith("ワ") || x.NameKana.StartsWith("ヲ") || x.NameKana.StartsWith("ン")),
                _ => _listStaffMasterVo,
            };

            if (_findListStaffMasterVo is not null) {
                foreach (StaffMasterVo staffMasterVo in _findListStaffMasterVo.OrderBy(x => x.NameKana)) {
                    sheetView.Rows.Add(rowCount, 1);
                    sheetView.RowHeader.Columns[0].Label = (rowCount + 1).ToString(); // Rowヘッダ
                    sheetView.Rows[rowCount].ForeColor = staffMasterVo.RetirementFlag ? Color.Red : Color.Black; // 退職済のレコードのForeColorをセット
                    sheetView.Rows[rowCount].Height = 20; // Rowの高さ
                    sheetView.Rows[rowCount].Resizable = false; // RowのResizableを禁止
                    sheetView.Rows[rowCount].Tag = staffMasterVo;
                    // 所属
                    sheetView.Cells[rowCount, colBelongs].Value = _dictionaryBelongs[staffMasterVo.Belongs];
                    // 雇用形態
                    sheetView.Cells[rowCount, colJobForm].Value = _dictionaryJobForm[staffMasterVo.JobForm];
                    // 職種
                    sheetView.Cells[rowCount, colOccupation].Value = _dictionaryOccupation[staffMasterVo.Occupation];
                    // 配車の対象かどうか
                    sheetView.Cells[rowCount, colVehicleDispatchTarget].Value = staffMasterVo.VehicleDispatchTarget;
                    // 組合コード
                    sheetView.Cells[rowCount, colCode].Value = staffMasterVo.UnionCode;
                    // 名前
                    sheetView.Cells[rowCount, colName].Text = staffMasterVo.Name;
                    // カナ
                    sheetView.Cells[rowCount, colNameKana].Text = staffMasterVo.NameKana;
                    // 生年月日
                    sheetView.Cells[rowCount, colBirthDate].Value = _dateUtility.GetBirthday(staffMasterVo.BirthDate);
                    // 年齢
                    sheetView.Cells[rowCount, colFullAge].Value = string.Concat(_dateUtility.GetAge(staffMasterVo.BirthDate.Date), "歳");
                    // 雇用年月日
                    sheetView.Cells[rowCount, colEmploymentDate].Value = _dateUtility.GetEmploymentDate(staffMasterVo.EmploymentDate.Date);
                    // 勤続年数
                    if (staffMasterVo.EmploymentDate.Date != _defaultDateTime.Date)
                        sheetView.Cells[rowCount, colServiceDate].Value = string.Concat(_dateUtility.GetEmploymenteYear(staffMasterVo.EmploymentDate.Date).ToString("#0年"), _dateUtility.GetEmploymenteMonth(staffMasterVo.EmploymentDate.Date).ToString("00月"));
                    // 東環保研修カード
                    sheetView.Cells[rowCount, colToukanpoCard].Value = _toukanpoTrainingCardDao.ExistenceToukanpoTrainingCardMaster(staffMasterVo.StaffCode);
                    // 法定１２項目
                    sheetView.Cells[rowCount, colLegalTwelveItemFlag].Value = staffMasterVo.LegalTwelveItemFlag;
                    // 東環保講習
                    sheetView.Cells[rowCount, colToukanpoFlag].Value = staffMasterVo.ToukanpoFlag;
                    // 免許証期限
                    sheetView.Cells[rowCount, colLicensExpirationDate].Value = _licenseMasterDao.GetExpirationDate(staffMasterVo.StaffCode);
                   　// 初任診断
                    if (_staffProperDao.GetSyoninProperDate(staffMasterVo.StaffCode) != _defaultDateTime) {
                        sheetView.Cells[rowCount, colFirstTerm].Value = _staffProperDao.GetSyoninProperDate(staffMasterVo.StaffCode);
                    } else {
                        sheetView.Cells[rowCount, colFirstTerm].Value = string.Empty;
                    }
                    // 適齢診断の残日数
                    sheetView.Cells[rowCount, colSuitableAge].Value = _staffProperDao.GetTekireiProperDate(staffMasterVo.StaffCode);
                    // 年度内事故回数
                    sheetView.Cells[rowCount, colCarAccidentCount].Value = _carAccidentMasterDao.GetCarAccidentMasterCount(staffMasterVo.StaffCode);
                    /*
                     * 1年以内の健康診断
                     */
                    DateTime medicalExaminationDate = _staffMedicalExaminationDao.GetMedicalExaminationDate(staffMasterVo.StaffCode);
                    if (medicalExaminationDate != _defaultDateTime) {
                        sheetView.Cells[rowCount, colMedicalExaminationDate].Value = string.Concat("受診日(", medicalExaminationDate.ToString("yyyy年MM月dd日"), ")");
                    } else {
                        sheetView.Cells[rowCount, colMedicalExaminationDate].Value = "健康診断の記録無し";
                    }
                    // 現住所
                    sheetView.Cells[rowCount, colCurrentAddress].Value = staffMasterVo.CurrentAddress;
                    // 健康保険加入
                    sheetView.Cells[rowCount, colHealthInsuranceNumber].Value = staffMasterVo.HealthInsuranceDate != _defaultDateTime ? true : false;
                    // 厚生年金加入
                    sheetView.Cells[rowCount, colWelfarePensionNumber].Value = staffMasterVo.WelfarePensionDate != _defaultDateTime ? true : false;
                    // 雇用保険加入
                    sheetView.Cells[rowCount, colEmploymentInsuranceNumber].Value = staffMasterVo.EmploymentInsuranceDate != _defaultDateTime ? true : false;
                    // 労災保険
                    sheetView.Cells[rowCount, colWorkerAccidentInsuranceNumber].Value = staffMasterVo.WorkerAccidentInsuranceDate != _defaultDateTime ? true : false;
                    rowCount++;
                }
            }
            // 先頭行（列）インデックスをセット
            this.SpreadList.SetViewportTopRow(0, _spreadListTopRow);
            // Spread 活性化
            this.SpreadList.ResumeLayout();
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(" ", rowCount, " 件を処理しました");
        }

        private void PutSheetViewDriver(SheetView sheetView) {
            int rowCount = 0;
            // Spread 非活性化
            SpreadList.SuspendLayout();
            // 先頭行（列）インデックスを取得
            _spreadListTopRow = SpreadList.GetViewportTopRow(0);
            // Rowを削除する
            if (sheetView.Rows.Count > 0)
                sheetView.RemoveRows(0, sheetView.Rows.Count);
            List<StaffMasterVo>? _findListStaffMasterVo = TabControlEx1.SelectedTab.Text switch {
                "あ行" => _listStaffMasterVo?.FindAll(x => x.NameKana.StartsWith("ア") || x.NameKana.StartsWith("イ") || x.NameKana.StartsWith("ウ") || x.NameKana.StartsWith("エ") || x.NameKana.StartsWith("オ")),
                "か行" => _listStaffMasterVo?.FindAll(x => x.NameKana.StartsWith("カ") || x.NameKana.StartsWith("ガ") || x.NameKana.StartsWith("キ") || x.NameKana.StartsWith("ギ") || x.NameKana.StartsWith("ク") || x.NameKana.StartsWith("グ") || x.NameKana.StartsWith("ケ") || x.NameKana.StartsWith("ゲ") || x.NameKana.StartsWith("コ") || x.NameKana.StartsWith("ゴ")),
                "さ行" => _listStaffMasterVo?.FindAll(x => x.NameKana.StartsWith("サ") || x.NameKana.StartsWith("シ") || x.NameKana.StartsWith("ス") || x.NameKana.StartsWith("セ") || x.NameKana.StartsWith("ソ")),
                "た行" => _listStaffMasterVo?.FindAll(x => x.NameKana.StartsWith("タ") || x.NameKana.StartsWith("ダ") || x.NameKana.StartsWith("チ") || x.NameKana.StartsWith("ツ") || x.NameKana.StartsWith("テ") || x.NameKana.StartsWith("デ") || x.NameKana.StartsWith("ト") || x.NameKana.StartsWith("ド")),
                "な行" => _listStaffMasterVo?.FindAll(x => x.NameKana.StartsWith("ナ") || x.NameKana.StartsWith("ニ") || x.NameKana.StartsWith("ヌ") || x.NameKana.StartsWith("ネ") || x.NameKana.StartsWith("ノ")),
                "は行" => _listStaffMasterVo?.FindAll(x => x.NameKana.StartsWith("ハ") || x.NameKana.StartsWith("パ") || x.NameKana.StartsWith("バ") || x.NameKana.StartsWith("ヒ") || x.NameKana.StartsWith("ビ") || x.NameKana.StartsWith("フ") || x.NameKana.StartsWith("ブ") || x.NameKana.StartsWith("ヘ") || x.NameKana.StartsWith("ベ") || x.NameKana.StartsWith("ホ")),
                "ま行" => _listStaffMasterVo?.FindAll(x => x.NameKana.StartsWith("マ") || x.NameKana.StartsWith("ミ") || x.NameKana.StartsWith("ム") || x.NameKana.StartsWith("メ") || x.NameKana.StartsWith("モ")),
                "や行" => _listStaffMasterVo?.FindAll(x => x.NameKana.StartsWith("ヤ") || x.NameKana.StartsWith("ユ") || x.NameKana.StartsWith("ヨ")),
                "ら行" => _listStaffMasterVo?.FindAll(x => x.NameKana.StartsWith("ラ") || x.NameKana.StartsWith("リ") || x.NameKana.StartsWith("ル") || x.NameKana.StartsWith("レ") || x.NameKana.StartsWith("ロ")),
                "わ行" => _listStaffMasterVo?.FindAll(x => x.NameKana.StartsWith("ワ") || x.NameKana.StartsWith("ヲ") || x.NameKana.StartsWith("ン")),
                _ => _listStaffMasterVo,
            };
            /*
             * 法定１２項目の講習対象者(つまり運転手)
             */
            _findListStaffMasterVo = _findListStaffMasterVo?.FindAll(x => x.LegalTwelveItemFlag == true);

            if (_findListStaffMasterVo is not null) {
                foreach (StaffMasterVo staffMasterVo in _findListStaffMasterVo.OrderBy(x => x.Belongs).ThenBy(x => x.NameKana)) {
                    sheetView.Rows.Add(rowCount, 1);
                    sheetView.RowHeader.Columns[0].Label = (rowCount + 1).ToString(); // Rowヘッダ
                    sheetView.Rows[rowCount].ForeColor = staffMasterVo.RetirementFlag ? Color.Red : Color.Black; // 退職済のレコードのForeColorをセット
                    sheetView.Rows[rowCount].Height = 20; // Rowの高さ
                    sheetView.Rows[rowCount].Resizable = false; // RowのResizableを禁止
                    sheetView.Rows[rowCount].Tag = staffMasterVo;
                    // 通しナンバー
                    sheetView.Cells[rowCount, 0].Value = rowCount + 1;
                    // 雇用形態１
                    sheetView.Cells[rowCount, 1].Text = _dictionaryBelongs[staffMasterVo.Belongs];
                    // 雇用形態２
                    sheetView.Cells[rowCount, 2].Text = _dictionaryJobForm[staffMasterVo.JobForm];
                    // カナ
                    sheetView.Cells[rowCount, 3].Text = staffMasterVo.Name;
                    // 氏名
                    sheetView.Cells[rowCount, 4].Text = staffMasterVo.NameKana;
                    // 年齢
                    sheetView.Cells[rowCount, 5].Text = string.Concat(_dateUtility.GetAge(staffMasterVo.BirthDate.Date), "歳");
                    rowCount++;
                }
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
        /// <param name="groupBoxEx"></param>
        /// <returns></returns>
        private List<int> CreateArray(GroupBoxEx groupBoxEx) {
            List<int> list = new();
            foreach (CheckBoxEx checkBoxEx in groupBoxEx.Controls) {
                if (checkBoxEx.Checked)
                    list.Add(Convert.ToInt32(checkBoxEx.Tag));
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns></returns>
        private SheetView InitializeSheetView(SheetView sheetView) {
            SpreadList.AllowDragDrop = false; // DrugDropを禁止する
            SpreadList.PaintSelectionHeader = false; // ヘッダの選択状態をしない
            SpreadList.TabStrip.DefaultSheetTab.Font = new Font("Yu Gothic UI", 9);
            sheetView.AlternatingRows.Count = 2; // 行スタイルを２行単位とします
            sheetView.AlternatingRows[0].BackColor = Color.WhiteSmoke; // 1行目の背景色を設定します
            sheetView.AlternatingRows[1].BackColor = Color.White; // 2行目の背景色を設定します
            sheetView.ColumnHeader.Rows[0].Height = 26; // Columnヘッダの高さ
            sheetView.GrayAreaBackColor = Color.White;
            sheetView.HorizontalGridLine = new GridLine(GridLineType.None);
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9); // 行ヘッダのFont
            sheetView.RowHeader.Columns[0].Width = 48; // 行ヘッダの幅を変更します
            sheetView.VerticalGridLine = new GridLine(GridLineType.Flat, Color.LightGray);
            sheetView.RemoveRows(0, sheetView.Rows.Count);
            return sheetView;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StaffList_FormClosing(object sender, FormClosingEventArgs e) {
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
