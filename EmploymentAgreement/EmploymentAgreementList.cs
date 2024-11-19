/*
 * 2024-11-03
 */
using System.Data;

using Common;

using ControlEx;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace EmploymentAgreement {
    public partial class EmploymentAgreementList : Form {
        private readonly DateTime _defaultDateTime = new DateTime(1900, 01, 01);
        /// <summary>
        /// 所属
        /// </summary>
        private const int _colBelongs = 0;
        /// <summary>
        /// 職種
        /// </summary>
        private const int _colOccupation = 1;
        /// <summary>
        /// 雇用形態
        /// </summary>
        private const int _colJobForm = 2;
        /// <summary>
        /// 組合№
        /// </summary>
        private const int _colUnionCode = 3;
        /// <summary>
        /// 氏名
        /// </summary>
        private const int _colDisplayName = 4;
        /// <summary>
        /// カナ
        /// </summary>
        private const int _colNameKana = 5;
        /// <summary>
        /// 生年月日
        /// </summary>
        private const int _colBirthDate = 6;
        /// <summary>
        /// 年齢
        /// </summary>
        private const int _colAge = 7;
        /// <summary>
        /// 入社年月日
        /// </summary>
        private const int _colEmplomentDate = 8;
        /// <summary>
        /// 契約期間
        /// </summary>
        private const int _colContractExpirationPeriod = 9;
        /// <summary>
        /// 体験入社契約日
        /// </summary>
        private const int _colExperienceStartDate = 10;
        /// <summary>
        /// 体験入社終了日
        /// </summary>
        private const int _colExperienceEndDate = 11;
        /// <summary>
        /// 継続アルバイト契約開始日
        /// </summary>
        private const int _colContractExpirationPartTimeJobStartDate = 12;
        /// <summary>
        /// 継続アルバイト契約終了日
        /// </summary>
        private const int _colContractExpirationPartTimeJobEndDate = 13;
        /// <summary>
        /// 労協押印待ち
        /// true:労協に提出済
        /// false:労協に未提出
        /// </summary>
        private const int _colCheckFlag = 14;
        /// <summary>
        /// 長期契約開始日
        /// </summary>
        private const int _colContractExpirationLongJobStartDate = 15;
        /// <summary>
        /// 長期契約終了日
        /// </summary>
        private const int _colContractExpirationLongJobEndDate = 16;
        /// <summary>
        /// 短期契約開始日
        /// </summary>
        private const int _colContractExpirationShortJobStartDate = 17;
        /// <summary>
        /// 短期契約終了日
        /// </summary>
        private const int _colContractExpirationShortJobEndDate = 18;
        /// <summary>
        /// 誓約書
        /// </summary>
        private const int _colContractExpirationWrittenPledge = 19;
        /// <summary>
        /// 失墜行為
        /// </summary>
        private const int _colContractExpirationLossWrittenPledge = 20;
        /// <summary>
        /// 契約満了通知
        /// </summary>
        private const int _colContractExpirationNotice = 21;

        /*
         * インスタンス作成
         */
        private readonly DateUtility _date = new();
        private readonly Screen _screen;
        private readonly ScreenForm _screenForm = new();
        /*
         * Dao
         */
        private BelongsMasterDao _belongsMasterDao;
        private ContractExpirationDao _contractExpirationDao;
        private EmploymentAgreementDao _employmentAgreementDao;
        private JobFormMasterDao _jobFormMasterDao;
        private OccupationMasterDao _occupationMasterDao;
        private StaffMasterDao _staffMasterDao;
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;
        private List<StaffMasterVo> _listStaffMasterVo;
        private EmploymentAgreementVo _employmentAgreementVo;
        private List<EmploymentAgreementVo> _listEmploymentAgreementVo;
        private List<ContractExpirationVo> _listContractExpirationVo;
        /*
         * Dictionary
         */
        private readonly Dictionary<int, string> _dictionaryBelongs = new();
        private readonly Dictionary<int, string> _dictionaryOccupation = new();
        private readonly Dictionary<int, string> _dictionaryJobDescription = new();
        private readonly Dictionary<int, string> _dictionaryJobForm = new();

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public EmploymentAgreementList(ConnectionVo connectionVo, Screen screen) {
            /*
             * インスタンス作成
             */
            _screen = screen;
            /*
             * Dao
             */
            _belongsMasterDao = new(connectionVo);
            _contractExpirationDao = new(connectionVo);
            _employmentAgreementDao = new(connectionVo);
            _jobFormMasterDao = new(connectionVo);
            _occupationMasterDao = new(connectionVo);
            _staffMasterDao = new(connectionVo);
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
            MenuStripEx1.ChangeEnable(listString);
            this.CheckBoxExRetirementFlag.Checked = false;
            this.InitializeSheetView(this.SheetViewList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEx_Click(object sender, EventArgs e) {
            switch (((ButtonEx)sender).Name) {
                case "ButtonExUpdate":
                    try {
                        _listEmploymentAgreementVo = _employmentAgreementDao.SelectAllEmploymentAgreement();
                        _listContractExpirationVo = _contractExpirationDao.SelectAllContractExpiration();
                        this.AddSheetViewList(_staffMasterDao.SelectAllStaffMaster(new int[] { 12, 14, 15, 20, 21 },
                                                                                   new int[] { 10, 99 },
                                                                                   new int[] { 10, 11, 20, 99 },
                                                                                   this.CheckBoxExRetirementFlag.Checked));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
            }
        }

        int spreadListTopRow = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listStaffMasterVo"></param>
        private void AddSheetViewList(List<StaffMasterVo> listStaffMasterVo) {
            /*
             * Sort
             */
            var orderListStaffMasterVo = listStaffMasterVo.OrderBy(x => x.Belongs).ThenBy(x => x.UnionCode);

            int rowCount = 0;
            // Spread 非活性化
            SpreadList.SuspendLayout();
            // 先頭行（列）インデックスを取得
            spreadListTopRow = SpreadList.GetViewportTopRow(0);
            // Rowを削除する
            if (SheetViewList.Rows.Count > 0)
                SheetViewList.RemoveRows(0, SheetViewList.Rows.Count);
            foreach (StaffMasterVo staffMasterVo in orderListStaffMasterVo) {
                SheetViewList.Rows.Add(rowCount, 1);
                SheetViewList.RowHeader.Columns[0].Label = (rowCount + 1).ToString(); // Rowヘッダ
                SheetViewList.Rows[rowCount].ForeColor = Color.Black;
                SheetViewList.Rows[rowCount].Height = 20; // Rowの高さ
                SheetViewList.Rows[rowCount].Resizable = false; // RowのResizableを禁止

                SheetViewList.Cells[rowCount, _colBelongs].Text = _dictionaryBelongs[staffMasterVo.Belongs];
                SheetViewList.Cells[rowCount, _colOccupation].Text = _dictionaryOccupation[staffMasterVo.Occupation];
                SheetViewList.Cells[rowCount, _colJobForm].Text = _dictionaryJobForm[staffMasterVo.JobForm];
                SheetViewList.Cells[rowCount, _colUnionCode].Text = staffMasterVo.UnionCode > 0 ? staffMasterVo.UnionCode.ToString() : "";
                SheetViewList.Cells[rowCount, _colDisplayName].Text = staffMasterVo.DisplayName;
                SheetViewList.Cells[rowCount, _colNameKana].Text = staffMasterVo.NameKana;
                SheetViewList.Cells[rowCount, _colBirthDate].Value = staffMasterVo.BirthDate;
                SheetViewList.Cells[rowCount, _colAge].Value = _date.GetAge(staffMasterVo.BirthDate);
                SheetViewList.Cells[rowCount, _colEmplomentDate].Value = staffMasterVo.EmploymentDate;
                SheetViewList.Rows[rowCount].BackColor = Color.White;
                /*
                 * EmploymentAgreementVo
                 */
                EmploymentAgreementVo? employmentAgreementVo = _listEmploymentAgreementVo.Find(x => x.StaffCode == staffMasterVo.StaffCode);
                if (employmentAgreementVo is not null) {
                    SheetViewList.Cells[rowCount, _colContractExpirationPeriod].Value = employmentAgreementVo.ContractExpirationPeriod;
                    SheetViewList.Cells[rowCount, _colCheckFlag].Text = employmentAgreementVo.CheckFlag ? "✓" : "";
                } else {
                    SheetViewList.Rows[rowCount].BackColor = Color.LightSlateGray;
                }
                /*
                 * ContractExpirationVo
                 */
                List<ContractExpirationVo>? listContractExpirationVo = _listContractExpirationVo.FindAll(x => x.StaffCode == staffMasterVo.StaffCode);
                if (listContractExpirationVo is not null) {
                    /*
                     * 体験入社契約
                     */
                    if (listContractExpirationVo.Find(x => x.Code == 21) is not null) {
                        SheetViewList.Cells[rowCount, _colExperienceStartDate].Value = listContractExpirationVo.Find(x => x.Code == 21).StartDate;
                        SheetViewList.Cells[rowCount, _colExperienceEndDate].Value = listContractExpirationVo.Find(x => x.Code == 21).EndDate;
                        this.SetCellColor(listContractExpirationVo.Find(x => x.Code == 21).EndDate, rowCount, _colExperienceEndDate);
                    }
                    /*
                     * 継続アルバイト契約
                     */
                    if (listContractExpirationVo.Find(x => x.Code == 20) is not null) {
                        SheetViewList.Cells[rowCount, _colContractExpirationPartTimeJobStartDate].Value = listContractExpirationVo.Find(x => x.Code == 20).StartDate;
                        SheetViewList.Cells[rowCount, _colContractExpirationPartTimeJobEndDate].Value = listContractExpirationVo.Find(x => x.Code == 20).EndDate;
                        this.SetCellColor(listContractExpirationVo.Find(x => x.Code == 20).EndDate, rowCount, _colContractExpirationPartTimeJobEndDate);
                    }
                    /*
                     * 長期対象者契約
                     */
                    if (listContractExpirationVo.Find(x => x.Code == 10) is not null) {
                        SheetViewList.Cells[rowCount, _colContractExpirationLongJobStartDate].Value = listContractExpirationVo.Find(x => x.Code == 10).StartDate;
                        SheetViewList.Cells[rowCount, _colContractExpirationLongJobEndDate].Value = listContractExpirationVo.Find(x => x.Code == 10).EndDate;
                        this.SetCellColor(listContractExpirationVo.Find(x => x.Code == 10).EndDate, rowCount, _colContractExpirationLongJobEndDate);
                    }
                    /*
                     * 短期対象者契約
                     */
                    if (listContractExpirationVo.Find(x => x.Code == 11) is not null) {
                        SheetViewList.Cells[rowCount, _colContractExpirationShortJobStartDate].Value = listContractExpirationVo.Find(x => x.Code == 11).StartDate;
                        SheetViewList.Cells[rowCount, _colContractExpirationShortJobEndDate].Value = listContractExpirationVo.Find(x => x.Code == 11).EndDate;
                        this.SetCellColor(listContractExpirationVo.Find(x => x.Code == 11).EndDate, rowCount, _colContractExpirationShortJobEndDate);
                    }
                    /*
                     * 誓約書
                     */
                    if (listContractExpirationVo.Find(x => x.Code == 30) is not null) {
                        SheetViewList.Cells[rowCount, _colContractExpirationWrittenPledge].Value = listContractExpirationVo.Find(x => x.Code == 30).EndDate;
                        this.SetCellColor(listContractExpirationVo.Find(x => x.Code == 30).EndDate, rowCount, _colContractExpirationWrittenPledge);
                    }
                    /*
                     * 失墜行為
                     */
                    if (listContractExpirationVo.Find(x => x.Code == 40) is not null) {
                        SheetViewList.Cells[rowCount, _colContractExpirationLossWrittenPledge].Value = listContractExpirationVo.Find(x => x.Code == 40).EndDate;
                        this.SetCellColor(listContractExpirationVo.Find(x => x.Code == 40).EndDate, rowCount, _colContractExpirationLossWrittenPledge);
                    }
                    /*
                     * 契約満了
                     */
                    if (listContractExpirationVo.Find(x => x.Code == 50) is not null) {
                        SheetViewList.Cells[rowCount, _colContractExpirationNotice].Value = listContractExpirationVo.Find(x => x.Code == 50).EndDate;
                        this.SetCellColor(listContractExpirationVo.Find(x => x.Code == 50).EndDate, rowCount, _colContractExpirationNotice);
                    }
                }
                SheetViewList.Rows[rowCount].Tag = staffMasterVo;
                rowCount++;
            }
            // 先頭行（列）インデックスをセット
            SpreadList.SetViewportTopRow(0, spreadListTopRow);
            // Spread 活性化
            SpreadList.ResumeLayout();
            StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(" ", rowCount, " 件");
        }

        /// <summary>
        /// セルの色を設定
        /// </summary>
        /// <param name="endDate"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void SetCellColor(DateTime endDate, int row, int col) {
            switch ((endDate.Date - DateTime.Now.Date).Days) {
                case int n when (n <= 1):
                    SheetViewList.Cells[row, col].BackColor = Color.Red;
                    break;
                case int n when (n <= 3):
                    SheetViewList.Cells[row, col].BackColor = Color.Orange;
                    break;
            }
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
            SpreadList.TabStripPolicy = TabStripPolicy.Never; // シートタブを非表示

            sheetView.ColumnHeader.Rows[0].Height = 26; // Columnヘッダの高さ
            sheetView.GrayAreaBackColor = Color.White;
            sheetView.HorizontalGridLine = new GridLine(GridLineType.Flat);
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9); // 行ヘッダのFont
            sheetView.RowHeader.Columns[0].Width = 28; // 行ヘッダの幅を変更します
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
            // ヘッダーのDoubleClickを回避
            if (e.ColumnHeader)
                return;
            StaffMasterVo staffMasterVo = (StaffMasterVo)SheetViewList.Rows[e.Row].Tag;
            _employmentAgreementVo = _listEmploymentAgreementVo.Find(x => x.StaffCode == staffMasterVo.StaffCode);
            EmploymentAgreementDetail employmentAgreementDetail = new(_connectionVo, staffMasterVo, _employmentAgreementVo);
            _screenForm.SetPosition(_screen, employmentAgreementDetail);
            employmentAgreementDetail.ShowDialog(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            EmploymentAgreementPaper employmentAgreementPaper;
            StaffMasterVo staffMasterVo = (StaffMasterVo)SheetViewList.Rows[SheetViewList.ActiveRowIndex].Tag;

            switch (((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemExpiration": // 体験アルバイト契約 20
                    employmentAgreementPaper = new(_connectionVo, 21, staffMasterVo, _listEmploymentAgreementVo.Find(x => x.StaffCode == staffMasterVo.StaffCode));
                    _screenForm.SetPosition(_screen, employmentAgreementPaper);
                    employmentAgreementPaper.Show(this);
                    break;
                case "ToolStripMenuItemContractExpirationPartTimeJob": // 継続アルバイト契約 21
                    employmentAgreementPaper = new(_connectionVo, 20, staffMasterVo, _listEmploymentAgreementVo.Find(x => x.StaffCode == staffMasterVo.StaffCode));
                    _screenForm.SetPosition(_screen, employmentAgreementPaper);
                    employmentAgreementPaper.Show(this);
                    break;
                case "ToolStripMenuItemContractExpirationPartTimeEmployee": // 嘱託雇用契約社員 22
                    employmentAgreementPaper = new(_connectionVo, 22, staffMasterVo, _listEmploymentAgreementVo.Find(x => x.StaffCode == staffMasterVo.StaffCode));
                    _screenForm.SetPosition(_screen, employmentAgreementPaper);
                    employmentAgreementPaper.Show(this);
                    break;
                case "ToolStripMenuItemContractExpirationPartTimer": // パートタイマー 23
                    employmentAgreementPaper = new(_connectionVo, 23, staffMasterVo, _listEmploymentAgreementVo.Find(x => x.StaffCode == staffMasterVo.StaffCode));
                    _screenForm.SetPosition(_screen, employmentAgreementPaper);
                    employmentAgreementPaper.Show(this);
                    break;
                case "ToolStripMenuItemContractExpirationLongJob新産別": // 長期雇用契約（新産別)
                    employmentAgreementPaper = new(_connectionVo, 10, staffMasterVo, _listEmploymentAgreementVo.Find(x => x.StaffCode == staffMasterVo.StaffCode));
                    _screenForm.SetPosition(_screen, employmentAgreementPaper);
                    employmentAgreementPaper.Show(this);
                    break;
                case "ToolStripMenuItemContractExpirationLongJob自運労運転士": // 長期雇用契約（自運労運転士)
                    employmentAgreementPaper = new(_connectionVo, 11, staffMasterVo, _listEmploymentAgreementVo.Find(x => x.StaffCode == staffMasterVo.StaffCode));
                    _screenForm.SetPosition(_screen, employmentAgreementPaper);
                    employmentAgreementPaper.Show(this);
                    break;
                case "ToolStripMenuItemContractExpirationLongJob自運労作業員": // 長期雇用契約（自運労作業員)
                    employmentAgreementPaper = new(_connectionVo, 12, staffMasterVo, _listEmploymentAgreementVo.Find(x => x.StaffCode == staffMasterVo.StaffCode));
                    _screenForm.SetPosition(_screen, employmentAgreementPaper);
                    employmentAgreementPaper.Show(this);
                    break;
                case "ToolStripMenuItemContractExpirationShortJob": // 短期雇用契約

                    break;
                case "ToolStripMenuItemContractExpirationWrittenPledge": // 誓約書

                    break;
                case "ToolStripMenuItemContractExpirationLossWrittenPledge": // 失墜行為確認書

                    break;
                case "ToolStripMenuItemContractExpirationNotice": // 契約満了通知

                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmploymentAgreementList_FormClosing(object sender, FormClosingEventArgs e) {
            DialogResult dialogResult = MessageBox.Show("アプリケーションを終了します。よろしいですか？", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
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
