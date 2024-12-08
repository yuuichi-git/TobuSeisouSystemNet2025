/*
 * 2024-11-05
 */
using System.Data;

using Common;

using ControlEx;

using Dao;

using Vo;

namespace EmploymentAgreement {
    public partial class EmploymentAgreementDetail : Form {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly DateUtility _dateUtility = new();
        /*
         * Dao
         */
        private BelongsMasterDao _belongsMasterDao;
        private ContractExpirationDao _contractExpirationDao;
        private EmploymentAgreementDao _employmentAgreementDao;
        private JobDescriptionMasterDao _jobDescriptionMasterDao;
        private JobFormMasterDao _jobFormMasterDao;
        private OccupationMasterDao _occupationMasterDao;
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;
        private readonly StaffMasterVo _staffMasterVo;
        private EmploymentAgreementVo _employmentAgreementVo;
        private List<ContractExpirationVo> _listContractExpirationVo;
        /*
         * Dictionary
         */
        private readonly Dictionary<int, string> _dictionaryBelongs = new();
        private readonly Dictionary<int, string> _dictionaryOccupation = new();
        private readonly Dictionary<int, string> _dictionaryJobDescription = new();
        private readonly Dictionary<int, string> _dictionaryJobForm = new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="staffMasterVo"></param>
        /// <param name="employmentAgreementVo"></param>
        public EmploymentAgreementDetail(ConnectionVo connectionVo, StaffMasterVo staffMasterVo, EmploymentAgreementVo employmentAgreementVo) {
            /*
             * Dao
             */
            _belongsMasterDao = new(connectionVo);
            _contractExpirationDao = new(connectionVo);
            _employmentAgreementDao = new(connectionVo);
            _jobDescriptionMasterDao = new(connectionVo);
            _jobFormMasterDao = new(connectionVo);
            _occupationMasterDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            _staffMasterVo = staffMasterVo;
            _employmentAgreementVo = employmentAgreementVo;
            _listContractExpirationVo = _contractExpirationDao.SelectOneContractExpirationP(staffMasterVo.StaffCode);
            /*
             * Dictionary
             */
            foreach (BelongsMasterVo belongsMasterVo in _belongsMasterDao.SelectAllBelongsMaster())
                _dictionaryBelongs.Add(belongsMasterVo.Code, belongsMasterVo.Name);
            foreach (OccupationMasterVo occupationMasterVo in _occupationMasterDao.SelectAllOccupationMaster())
                _dictionaryOccupation.Add(occupationMasterVo.Code, occupationMasterVo.Name);
            foreach (JobDescriptionMasterVo jobDescriptionMasterVo in _jobDescriptionMasterDao.SelectAllJobDescriptionMaster())
                _dictionaryJobDescription.Add(jobDescriptionMasterVo.Code, jobDescriptionMasterVo.Name);
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
            this.InitializeControl();
            if (_employmentAgreementDao.ExistenceEmploymentAgreement(_staffMasterVo.StaffCode)) {
                this.ButtonExUpdate.Text = "更　新";
                this.PutControlHead();
                this.PutControlBody();
                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "この従事者の基本台帳が存在します。";
            } else {
                this.ButtonExUpdate.Text = "新規登録";
                this.PutControlHead();
                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "この従事者の基本台帳が存在しません。台帳を作成してください。";
            }
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
                        if (!_employmentAgreementDao.ExistenceEmploymentAgreement(_staffMasterVo.StaffCode)) {
                            _employmentAgreementDao.InsertOneEmploymentAgreement(this.SetEmploymentAgreementVo());
                            StatusStripEx1.ToolStripStatusLabelDetail.Text = "INSERTに成功しました。";
                            this.Close();
                        } else {
                            _employmentAgreementDao.UpdateOneEmploymentAgreement(this.SetEmploymentAgreementVo());
                            StatusStripEx1.ToolStripStatusLabelDetail.Text = "UPDATEに成功しました。";
                            this.Close();
                        }
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                // 体験入社期間
                case "BTNExExpiration":
                    try {
                        _contractExpirationDao.InsertOneContractExpiration(SetContractExpirationVo(21,
                                                                                                   DTPExExpirationStartDate.GetDate(),
                                                                                                   DTPExExpirationEndDate.GetDate(),
                                                                                                   TextBoxExExpirationMemo.Text,
                                                                                                   PictureBoxEx1.Image));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    this.PutExpiration(_contractExpirationDao.SelectOneContractExpirationP(_staffMasterVo.StaffCode));
                    break;
                // 継続アルバイト更新期間
                case "BTNExContractExpirationPartTimeJob":
                    try {
                        if (!_contractExpirationDao.ExistenceContractExpiration(20,
                                                                                _staffMasterVo.StaffCode,
                                                                                DTPExContractExpirationPartTimeJobStartDate.GetDate().Date,
                                                                                DTPExContractExpirationPartTimeJobEndDate.GetDate().Date)) {
                            _contractExpirationDao.InsertOneContractExpiration(SetContractExpirationVo(20,
                                                                                                       DTPExContractExpirationPartTimeJobStartDate.GetDate().Date,
                                                                                                       DTPExContractExpirationPartTimeJobEndDate.GetDate().Date,
                                                                                                       TextBoxExContractExpirationPartTimeJobMemo.Text,
                                                                                                       PictureBoxEx1.Image));
                        } else {
                            _contractExpirationDao.UpdateOneContractExpiration(SetContractExpirationVo(20,
                                                                                                       DTPExContractExpirationPartTimeJobStartDate.GetDate().Date,
                                                                                                       DTPExContractExpirationPartTimeJobEndDate.GetDate().Date,
                                                                                                       TextBoxExContractExpirationPartTimeJobMemo.Text,
                                                                                                       PictureBoxEx1.Image));
                        }
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    this.PutContractExpirationPartTimeJob(_contractExpirationDao.SelectOneContractExpirationP(_staffMasterVo.StaffCode));
                    break;
                // 組合長期雇用期間
                case "BTNExContractExpirationLongJob":
                    try {
                        if (!_contractExpirationDao.ExistenceContractExpiration(10,
                                                                                _staffMasterVo.StaffCode,
                                                                                DTPExContractExpirationLongJobStartDate.GetDate().Date,
                                                                                DTPExContractExpirationLongJobEndDate.GetDate().Date)) {
                            _contractExpirationDao.InsertOneContractExpiration(SetContractExpirationVo(10,
                                                                                                       DTPExContractExpirationLongJobStartDate.GetDate().Date,
                                                                                                       DTPExContractExpirationLongJobEndDate.GetDate().Date,
                                                                                                       TextBoxExContractExpirationLongJobMemo.Text,
                                                                                                       PictureBoxEx1.Image));
                        } else {
                            _contractExpirationDao.UpdateOneContractExpiration(SetContractExpirationVo(10,
                                                                                                       DTPExContractExpirationLongJobStartDate.GetDate().Date,
                                                                                                       DTPExContractExpirationLongJobEndDate.GetDate().Date,
                                                                                                       TextBoxExContractExpirationLongJobMemo.Text,
                                                                                                       PictureBoxEx1.Image));
                        }
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    this.PutContractExpirationLongJob(_contractExpirationDao.SelectOneContractExpirationP(_staffMasterVo.StaffCode));
                    break;
                // 組合短期雇用期間
                case "BTNExContractExpirationShortJob":
                    try {
                        if (!_contractExpirationDao.ExistenceContractExpiration(11,
                                                                                _staffMasterVo.StaffCode,
                                                                                DTPExContractExpirationShortJobStartDate.GetDate().Date,
                                                                                DTPExContractExpirationShortJobEndDate.GetDate().Date)) {
                            _contractExpirationDao.InsertOneContractExpiration(SetContractExpirationVo(11,
                                                                                                       DTPExContractExpirationShortJobStartDate.GetDate().Date,
                                                                                                       DTPExContractExpirationShortJobEndDate.GetDate().Date,
                                                                                                       TextBoxExContractExpirationShortJobMemo.Text,
                                                                                                       PictureBoxEx1.Image));
                        } else {
                            _contractExpirationDao.UpdateOneContractExpiration(SetContractExpirationVo(11,
                                                                                                       DTPExContractExpirationShortJobStartDate.GetDate().Date,
                                                                                                       DTPExContractExpirationShortJobEndDate.GetDate().Date,
                                                                                                       TextBoxExContractExpirationShortJobMemo.Text,
                                                                                                       PictureBoxEx1.Image));
                        }
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    this.PutContractExpirationShortJob(_contractExpirationDao.SelectOneContractExpirationP(_staffMasterVo.StaffCode));
                    break;
                // 誓約書
                case "BTNExContractExpirationWrittenPledge":
                    try {
                        if (!_contractExpirationDao.ExistenceContractExpiration(30,
                                                                                _staffMasterVo.StaffCode,
                                                                                DTPExContractExpirationWrittenPledgeStartDate.GetDate().Date,
                                                                                DTPExContractExpirationWrittenPledgeEndDate.GetDate().Date)) {
                            _contractExpirationDao.InsertOneContractExpiration(SetContractExpirationVo(30,
                                                                                                       DTPExContractExpirationWrittenPledgeStartDate.GetDate().Date,
                                                                                                       DTPExContractExpirationWrittenPledgeEndDate.GetDate().Date,
                                                                                                       TextBoxExContractExpirationWrittenPledgeMemo.Text,
                                                                                                       PictureBoxEx1.Image));
                        } else {
                            _contractExpirationDao.UpdateOneContractExpiration(SetContractExpirationVo(30,
                                                                                                       DTPExContractExpirationWrittenPledgeStartDate.GetDate().Date,
                                                                                                       DTPExContractExpirationWrittenPledgeEndDate.GetDate().Date,
                                                                                                       TextBoxExContractExpirationWrittenPledgeMemo.Text,
                                                                                                       PictureBoxEx1.Image));
                        }
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    this.PutContractExpirationWrittenPledge(_contractExpirationDao.SelectOneContractExpirationP(_staffMasterVo.StaffCode));
                    break;
                // 失墜行為
                case "BTNExContractExpirationLossWrittenPledge":
                    try {
                        if (!_contractExpirationDao.ExistenceContractExpiration(40,
                                                                                _staffMasterVo.StaffCode,
                                                                                DTPExContractExpirationLossWrittenPledgeStartDate.GetDate().Date,
                                                                                DTPExContractExpirationLossWrittenPledgeEndDate.GetDate().Date)) {
                            _contractExpirationDao.InsertOneContractExpiration(SetContractExpirationVo(40,
                                                                                                       DTPExContractExpirationLossWrittenPledgeStartDate.GetDate().Date,
                                                                                                       DTPExContractExpirationLossWrittenPledgeEndDate.GetDate().Date,
                                                                                                       TextBoxExContractExpirationLossWrittenPledgeMemo.Text,
                                                                                                       PictureBoxEx1.Image));
                        } else {
                            _contractExpirationDao.UpdateOneContractExpiration(SetContractExpirationVo(40,
                                                                                                       DTPExContractExpirationLossWrittenPledgeStartDate.GetDate().Date,
                                                                                                       DTPExContractExpirationLossWrittenPledgeEndDate.GetDate().Date,
                                                                                                       TextBoxExContractExpirationLossWrittenPledgeMemo.Text,
                                                                                                       PictureBoxEx1.Image));
                        }
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    this.PutContractExpirationLossWrittenPledge(_contractExpirationDao.SelectOneContractExpirationP(_staffMasterVo.StaffCode));
                    break;
                // 契約満了通知
                case "BTNExContractExpirationNotice":
                    try {
                        if (!_contractExpirationDao.ExistenceContractExpiration(50,
                                                                                _staffMasterVo.StaffCode,
                                                                                DTPExContractExpirationNoticeStartDate.GetDate().Date,
                                                                                DTPExContractExpirationNoticeEndDate.GetDate().Date)) {
                            _contractExpirationDao.InsertOneContractExpiration(SetContractExpirationVo(50,
                                                                                                       DTPExContractExpirationNoticeStartDate.GetDate().Date,
                                                                                                       DTPExContractExpirationNoticeEndDate.GetDate().Date,
                                                                                                       TextBoxExContractExpirationNoticeMemo.Text,
                                                                                                       PictureBoxEx1.Image));
                        } else {
                            _contractExpirationDao.UpdateOneContractExpiration(SetContractExpirationVo(50,
                                                                                                       DTPExContractExpirationNoticeStartDate.GetDate().Date,
                                                                                                       DTPExContractExpirationNoticeEndDate.GetDate().Date,
                                                                                                       TextBoxExContractExpirationNoticeMemo.Text,
                                                                                                       PictureBoxEx1.Image));
                        }
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    this.PutContractExpirationNotice(_contractExpirationDao.SelectOneContractExpirationP(_staffMasterVo.StaffCode));
                    break;

                // 体験入社期間 画像
                case "BTNExExpirationPicture":
                    if (this.BTNExExpirationPicture.Tag is not null) {
                        new ShowPicture((byte[])this.BTNExExpirationPicture.Tag).Show();
                    } else {
                        MessageBox.Show("体験入社期間の契約書が添付されていません。");
                    }
                    break;
                // 長期アルバイト更新期間　画像１
                case "BTNExContractExpirationPartTimeJobPicture1":
                    if (this.BTNExContractExpirationPartTimeJobPicture1.Tag is not null) {
                        new ShowPicture((byte[])this.BTNExContractExpirationPartTimeJobPicture1.Tag).Show();
                    } else {
                        MessageBox.Show("長期アルバイト更新期間の契約書が添付されていません。");
                    }
                    break;
                // 長期アルバイト更新期間　画像２
                case "BTNExContractExpirationPartTimeJobPicture2":
                    if (this.BTNExContractExpirationPartTimeJobPicture2.Tag is not null) {
                        new ShowPicture((byte[])this.BTNExContractExpirationPartTimeJobPicture2.Tag).Show();
                    } else {
                        MessageBox.Show("長期アルバイト更新期間の契約書が添付されていません。");
                    }
                    break;
                // 組合長期雇用期間　画像１
                case "BTNExContractExpirationLongJobPicture1":
                    if (this.BTNExContractExpirationLongJobPicture1.Tag is not null) {
                        new ShowPicture((byte[])this.BTNExContractExpirationLongJobPicture1.Tag).Show();
                    } else {
                        MessageBox.Show("組合長期雇用期間の契約書が添付されていません。");
                    }
                    break;
                // 組合長期雇用期間　画像２
                case "BTNExContractExpirationLongJobPicture2":
                    if (this.BTNExContractExpirationLongJobPicture2.Tag is not null) {
                        new ShowPicture((byte[])this.BTNExContractExpirationLongJobPicture2.Tag).Show();
                    } else {
                        MessageBox.Show("組合長期雇用期間の契約書が添付されていません。");
                    }
                    break;
                // 組合短期雇用期間　画像１
                case "BTNExContractExpirationShortJobPicture1":
                    if (this.BTNExContractExpirationShortJobPicture1.Tag is not null) {
                        new ShowPicture((byte[])this.BTNExContractExpirationShortJobPicture1.Tag).Show();
                    } else {
                        MessageBox.Show("組合短期雇用期間の契約書が添付されていません。");
                    }
                    break;
                // 組合短期雇用期間　画像２
                case "BTNExContractExpirationShortJobPicture2":
                    if (this.BTNExContractExpirationShortJobPicture2.Tag is not null) {
                        new ShowPicture((byte[])this.BTNExContractExpirationShortJobPicture2.Tag).Show();
                    } else {
                        MessageBox.Show("組合短期雇用期間の契約書が添付されていません。");
                    }
                    break;
                // 誓約書期間　画像１
                case "BTNExWrittenPledgePicture1":
                    if (this.BTNExContractExpirationWrittenPledgePicture1.Tag is not null) {
                        new ShowPicture((byte[])this.BTNExContractExpirationWrittenPledgePicture1.Tag).Show();
                    } else {
                        MessageBox.Show("誓約書が添付されていません。");
                    }
                    break;
                // 失墜行為書類期間　画像１
                case "BTNExLossWrittenPledgePicture1":
                    if (this.BTNExContractExpirationLossWrittenPledgePicture1.Tag is not null) {
                        new ShowPicture((byte[])this.BTNExContractExpirationLossWrittenPledgePicture1.Tag).Show();
                    } else {
                        MessageBox.Show("失墜行為確認書が添付されていません。");
                    }
                    break;
                // 契約満了通知(事前通知書)　画像１
                case "BTNExContractExpirationNoticePicture1":
                    if (this.BTNExContractExpirationNoticePicture1.Tag is not null) {
                        new ShowPicture((byte[])this.BTNExContractExpirationNoticePicture1.Tag).Show();
                    } else {
                        MessageBox.Show("契約満了通知(事前通知書)が添付されていません。");
                    }
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void PutControlHead() {
            this.LabelExUnionCode.Text = _staffMasterVo.UnionCode.ToString("###");
            this.LabelExDisplayName.Text = _staffMasterVo.DisplayName;
            this.LabelExBelongs.Text = _dictionaryBelongs[_staffMasterVo.Belongs];
            this.LabelExOccupation.Text = _dictionaryOccupation[_staffMasterVo.Occupation];
            this.LabelExJobForm.Text = _dictionaryJobForm[_staffMasterVo.JobForm];
        }

        /// <summary>
        /// 
        /// </summary>
        private void PutControlBody() {
            this.ComboBoxExBaseAddress.Text = _employmentAgreementVo.BaseLocation; // 勤務地
            this.ComboBoxExBelongs.SelectedValue = _staffMasterVo.Belongs; // 雇用形態
            this.NUDExContractExpirationPeriod.Value = _employmentAgreementVo.ContractExpirationPeriod; // 契約期間
            /*
             * 契約期間文字を計算
             */
            DateTime date = DateTime.Now;
            string time = string.Empty;
            if (_employmentAgreementVo.ContractExpirationPeriodString != string.Empty) {
                this.TextBoxExContractExpirationPeriod.Text = _employmentAgreementVo.ContractExpirationPeriodString;
            } else {
                switch (_employmentAgreementVo.ContractExpirationPeriod) {
                    case 0:
                        time = string.Concat(date.Date.ToString("yyyy年MM月dd日"), " ～ ", date.Date.AddDays(6).ToString("yyyy年MM月dd日"));
                        this.TextBoxExContractExpirationPeriod.Text = time;
                        break;
                    case 1:
                        time = string.Concat(date.Date.ToString("yyyy年MM月dd日"), " ～ ", _dateUtility.GetEndOfMonth(date).ToString("yyyy年MM月dd日"));
                        this.TextBoxExContractExpirationPeriod.Text = time;
                        break;
                    case 2:
                        time = string.Concat(date.Date.ToString("yyyy年MM月dd日"), " ～ ", _dateUtility.GetEndOfMonth(date.AddMonths(1)).ToString("yyyy年MM月dd日"));
                        this.TextBoxExContractExpirationPeriod.Text = time;
                        break;
                    case 3:
                        time = string.Concat(date.Date.ToString("yyyy年MM月dd日"), " ～ ", _dateUtility.GetEndOfMonth(date.AddMonths(2)).ToString("yyyy年MM月dd日"));
                        this.TextBoxExContractExpirationPeriod.Text = time;
                        break;
                    case 6:
                        time = string.Concat(date.Date.ToString("yyyy年MM月dd日"), " ～ ", _dateUtility.GetEndOfMonth(date.AddMonths(5)).ToString("yyyy年MM月dd日"));
                        this.TextBoxExContractExpirationPeriod.Text = time;
                        break;
                    case 12:
                        time = string.Concat(_dateUtility.GetFiscalYearStartDate(date).ToString("yyyy年MM月dd日"), " ～ ", _dateUtility.GetFiscalYearEndDate(date).AddDays(-1).ToString("yyyy年MM月dd日"));
                        this.TextBoxExContractExpirationPeriod.Text = time;
                        break;
                    default:
                        this.TextBoxExContractExpirationPeriod.Text = string.Empty;
                        break;
                }
            }
            this.CheckBoxExCheckFlag.Checked = _employmentAgreementVo.CheckFlag; // 各労共に提出中
            this.ComboBoxExPayDetail.Text = _employmentAgreementVo.PayDetail; // 給与区分
            this.NUDExPay.Value = _employmentAgreementVo.Pay; // 給与単価
            this.ComboBoxExTravelCostDetail.Text = _employmentAgreementVo.TravelCostDetail; // 交通費区分
            this.NUDExTravelCost.Value = _employmentAgreementVo.TravelCost; // 交通費
            this.ComboBoxExJobDescription.SelectedValue = _employmentAgreementVo.JobDescription; // 従事すべき業務内容
            this.ComboBoxExWorkTime.Text = _employmentAgreementVo.WorkTime; // 勤務時間
            this.ComboBoxExBreakTime.Text = _employmentAgreementVo.BreakTime; // 休憩時間

            this.PutExpiration(_listContractExpirationVo);
            this.PutContractExpirationPartTimeJob(_listContractExpirationVo);
            this.PutContractExpirationLongJob(_listContractExpirationVo);
            this.PutContractExpirationShortJob(_listContractExpirationVo);
            this.PutContractExpirationWrittenPledge(_listContractExpirationVo);
            this.PutContractExpirationLossWrittenPledge(_listContractExpirationVo);
            this.PutContractExpirationNotice(_listContractExpirationVo);
        }

        /// <summary>
        /// 体験入社期間
        /// </summary>
        /// <param name="listContractExpirationVo"></param>
        private void PutExpiration(List<ContractExpirationVo> listContractExpirationVo) {
            Dictionary<int, DateTimePickerEx> _dicStartDate = new() { { 0, DTPExExpirationStartDate } };
            Dictionary<int, DateTimePickerEx> _dicEndDate = new() { { 0, DTPExExpirationEndDate } };
            Dictionary<int, TextBoxEx> _dicMemo = new() { { 0, TextBoxExExpirationMemo } };
            Dictionary<int, ButtonEx> _dicPicture = new() { { 0, BTNExExpirationPicture } };
            int count = 0;
            foreach (ContractExpirationVo contractExpirationVo in listContractExpirationVo.FindAll(x => x.Code == 21).OrderByDescending(x => x.EndDate)) {
                _dicStartDate[count].SetValueJp(contractExpirationVo.StartDate);
                _dicEndDate[count].SetValueJp(contractExpirationVo.EndDate);
                _dicMemo[count].Text = contractExpirationVo.Memo;
                _dicPicture[count].Tag = contractExpirationVo.Picture;
                /*
                 * 新規INSERT　編集を禁止
                 */
                _dicStartDate[count].Enabled = false;
                _dicEndDate[count].Enabled = false;
                _dicMemo[count].Enabled = false;
                BTNExExpiration.Enabled = false;

                count++;
                if (count > 0)
                    break;
            }
        }

        /// <summary>
        /// 長期アルバイト更新期間
        /// </summary>
        /// <param name="listContractExpirationVo"></param>
        private void PutContractExpirationPartTimeJob(List<ContractExpirationVo> listContractExpirationVo) {
            Dictionary<int, DateTimePickerEx> _dicStartDate = new() { { 0, DTPExContractExpirationPartTimeJobStartDate1 }, { 1, DTPExContractExpirationPartTimeJobStartDate2 } };
            Dictionary<int, DateTimePickerEx> _dicEndDate = new() { { 0, DTPExContractExpirationPartTimeJobEndDate1 }, { 1, DTPExContractExpirationPartTimeJobEndDate2 } };
            Dictionary<int, TextBoxEx> _dicMemo = new() { { 0, TextBoxExContractExpirationPartTimeJobMemo1 }, { 1, TextBoxExContractExpirationPartTimeJobMemo2 } };
            Dictionary<int, ButtonEx> _dicPicture = new() { { 0, BTNExContractExpirationPartTimeJobPicture1 }, { 1, BTNExContractExpirationPartTimeJobPicture2 } };
            int count = 0;
            foreach (ContractExpirationVo contractExpirationVo in listContractExpirationVo.FindAll(x => x.Code == 20).OrderByDescending(x => x.StartDate).OrderByDescending(x => x.EndDate)) {
                _dicStartDate[count].SetValueJp(contractExpirationVo.StartDate);
                _dicEndDate[count].SetValueJp(contractExpirationVo.EndDate);
                _dicMemo[count].Text = contractExpirationVo.Memo;
                _dicPicture[count].Tag = contractExpirationVo.Picture;
                count++;
                if (count > 1)
                    break;
            }
        }

        /// <summary>
        /// 組合長期雇用期間
        /// </summary>
        /// <param name="listContractExpirationVo"></param>
        private void PutContractExpirationLongJob(List<ContractExpirationVo> listContractExpirationVo) {
            Dictionary<int, DateTimePickerEx> _dicStartDate = new() { { 0, DTPExContractExpirationLongJobStartDate1 }, { 1, DTPExContractExpirationLongJobStartDate2 } };
            Dictionary<int, DateTimePickerEx> _dicEndDate = new() { { 0, DTPExContractExpirationLongJobEndDate1 }, { 1, DTPExContractExpirationLongJobEndDate2 } };
            Dictionary<int, TextBoxEx> _dicMemo = new() { { 0, TextBoxExContractExpirationLongJobMemo1 }, { 1, TextBoxExContractExpirationLongJobMemo2 } };
            Dictionary<int, ButtonEx> _dicPicture = new() { { 0, BTNExContractExpirationLongJobPicture1 }, { 1, BTNExContractExpirationLongJobPicture2 } };
            int count = 0;
            foreach (ContractExpirationVo contractExpirationVo in listContractExpirationVo.FindAll(x => x.Code == 10).OrderByDescending(x => x.EndDate)) {
                _dicStartDate[count].SetValueJp(contractExpirationVo.StartDate);
                _dicEndDate[count].SetValueJp(contractExpirationVo.EndDate);
                _dicMemo[count].Text = contractExpirationVo.Memo;
                _dicPicture[count].Tag = contractExpirationVo.Picture;
                count++;
                if (count > 1)
                    break;
            }
        }

        /// <summary>
        /// 組合短期雇用期間
        /// </summary>
        /// <param name="listContractExpirationVo"></param>
        private void PutContractExpirationShortJob(List<ContractExpirationVo> listContractExpirationVo) {
            Dictionary<int, DateTimePickerEx> _dicStartDate = new() { { 0, DTPExContractExpirationShortJobStartDate1 }, { 1, DTPExContractExpirationShortJobStartDate2 } };
            Dictionary<int, DateTimePickerEx> _dicEndDate = new() { { 0, DTPExContractExpirationShortJobEndDate1 }, { 1, DTPExContractExpirationShortJobEndDate2 } };
            Dictionary<int, TextBoxEx> _dicMemo = new() { { 0, TextBoxExContractExpirationShortJobMemo1 }, { 1, TextBoxExContractExpirationShortJobMemo2 } };
            Dictionary<int, ButtonEx> _dicPicture = new() { { 0, BTNExContractExpirationShortJobPicture1 }, { 1, BTNExContractExpirationShortJobPicture2 } };
            int count = 0;
            foreach (ContractExpirationVo contractExpirationVo in listContractExpirationVo.FindAll(x => x.Code == 11).OrderByDescending(x => x.EndDate)) {
                _dicStartDate[count].SetValueJp(contractExpirationVo.StartDate);
                _dicEndDate[count].SetValueJp(contractExpirationVo.EndDate);
                _dicMemo[count].Text = contractExpirationVo.Memo;
                _dicPicture[count].Tag = contractExpirationVo.Picture;
                count++;
                if (count > 1)
                    break;
            }
        }

        /// <summary>
        /// 誓約書
        /// </summary>
        /// <param name="listContractExpirationVo"></param>
        private void PutContractExpirationWrittenPledge(List<ContractExpirationVo> listContractExpirationVo) {
            Dictionary<int, DateTimePickerEx> _dicStartDate = new() { { 0, DTPExContractExpirationWrittenPledgeStartDate1 } };
            Dictionary<int, DateTimePickerEx> _dicEndDate = new() { { 0, DTPExContractExpirationWrittenPledgeEndDate1 } };
            Dictionary<int, TextBoxEx> _dicMemo = new() { { 0, TextBoxExContractExpirationWrittenPledgeMemo1 } };
            Dictionary<int, ButtonEx> _dicPicture = new() { { 0, BTNExContractExpirationWrittenPledgePicture1 } };
            int count = 0;
            foreach (ContractExpirationVo contractExpirationVo in listContractExpirationVo.FindAll(x => x.Code == 30).OrderByDescending(x => x.EndDate)) {
                _dicStartDate[count].SetValueJp(contractExpirationVo.StartDate);
                _dicEndDate[count].SetValueJp(contractExpirationVo.EndDate);
                _dicMemo[count].Text = contractExpirationVo.Memo;
                _dicPicture[count].Tag = contractExpirationVo.Picture;
                count++;
                if (count > 0)
                    break;
            }
        }

        /// <summary>
        /// 失墜行為書類期間
        /// </summary>
        /// <param name="listContractExpirationVo"></param>
        private void PutContractExpirationLossWrittenPledge(List<ContractExpirationVo> listContractExpirationVo) {
            Dictionary<int, DateTimePickerEx> _dicStartDate = new() { { 0, DTPExContractExpirationLossWrittenPledgeStartDate1 } };
            Dictionary<int, DateTimePickerEx> _dicEndDate = new() { { 0, DTPExContractExpirationLossWrittenPledgeEndDate1 } };
            Dictionary<int, TextBoxEx> _dicMemo = new() { { 0, TextBoxExContractExpirationLossWrittenPledgeMemo1 } };
            Dictionary<int, ButtonEx> _dicPicture = new() { { 0, BTNExContractExpirationLossWrittenPledgePicture1 } };
            int count = 0;
            foreach (ContractExpirationVo contractExpirationVo in listContractExpirationVo.FindAll(x => x.Code == 40).OrderByDescending(x => x.EndDate)) {
                _dicStartDate[count].SetValueJp(contractExpirationVo.StartDate);
                _dicEndDate[count].SetValueJp(contractExpirationVo.EndDate);
                _dicMemo[count].Text = contractExpirationVo.Memo;
                _dicPicture[count].Tag = contractExpirationVo.Picture;
                count++;
                if (count > 0)
                    break;
            }
        }

        /// <summary>
        /// 契約満了通知
        /// </summary>
        /// <param name="listContractExpirationVo"></param>
        private void PutContractExpirationNotice(List<ContractExpirationVo> listContractExpirationVo) {
            Dictionary<int, DateTimePickerEx> _dicStartDate = new() { { 0, DTPExContractExpirationNoticeStartDate1 } };
            Dictionary<int, DateTimePickerEx> _dicEndDate = new() { { 0, DTPExContractExpirationNoticeEndDate1 } };
            Dictionary<int, TextBoxEx> _dicMemo = new() { { 0, TextBoxExContractExpirationNoticeMemo1 } };
            Dictionary<int, ButtonEx> _dicPicture = new() { { 0, BTNExContractExpirationNoticePicture1 } };
            int count = 0;
            foreach (ContractExpirationVo contractExpirationVo in listContractExpirationVo.FindAll(x => x.Code == 50).OrderByDescending(x => x.EndDate)) {
                _dicStartDate[count].SetValueJp(contractExpirationVo.StartDate);
                _dicEndDate[count].SetValueJp(contractExpirationVo.EndDate);
                _dicMemo[count].Text = contractExpirationVo.Memo;
                _dicPicture[count].Tag = contractExpirationVo.Picture;
                count++;
                if (count > 0)
                    break;
            }
        }

        /// <summary>
        /// Voに代入
        /// </summary>
        /// <returns></returns>
        private EmploymentAgreementVo SetEmploymentAgreementVo() {
            EmploymentAgreementVo employmentAgreementVo = new();
            employmentAgreementVo.StaffCode = _staffMasterVo.StaffCode;
            employmentAgreementVo.BaseLocation = ComboBoxExBaseAddress.Text;
            employmentAgreementVo.Occupation = int.Parse(this.ComboBoxExBelongs.SelectedValue.ToString()); // 元がObject型で入ってるからキャストが必要
            employmentAgreementVo.ContractExpirationPeriod = (int)NUDExContractExpirationPeriod.Value;
            employmentAgreementVo.ContractExpirationPeriodString = this.TextBoxExContractExpirationPeriod.Text;
            employmentAgreementVo.PayDetail = this.ComboBoxExPayDetail.Text;
            employmentAgreementVo.Pay = (int)this.NUDExPay.Value;
            employmentAgreementVo.TravelCostDetail = this.ComboBoxExTravelCostDetail.Text;
            employmentAgreementVo.TravelCost = (int)this.NUDExTravelCost.Value;
            employmentAgreementVo.JobDescription = int.Parse(this.ComboBoxExJobDescription.SelectedValue.ToString()); // 元がObject型で入ってるからキャストが必要
            employmentAgreementVo.WorkTime = this.ComboBoxExWorkTime.Text;
            employmentAgreementVo.BreakTime = this.ComboBoxExBreakTime.Text;
            employmentAgreementVo.CheckFlag = this.CheckBoxExCheckFlag.Checked;
            employmentAgreementVo.InsertPcName = Environment.MachineName;
            employmentAgreementVo.InsertYmdHms = DateTime.Now;
            employmentAgreementVo.UpdatePcName = Environment.MachineName;
            employmentAgreementVo.UpdateYmdHms = DateTime.Now;
            employmentAgreementVo.DeletePcName = Environment.MachineName;
            employmentAgreementVo.DeleteYmdHms = DateTime.Now;
            employmentAgreementVo.DeleteFlag = false;
            return employmentAgreementVo;
        }

        /// <summary>
        /// Voに代入
        /// </summary>
        /// <returns></returns>
        private ContractExpirationVo SetContractExpirationVo(int code, DateTime startDate, DateTime endDate, string memo, System.Drawing.Image picture) {
            ContractExpirationVo contractExpirationVo = new();
            contractExpirationVo.Code = code;
            contractExpirationVo.StaffCode = _staffMasterVo.StaffCode;
            contractExpirationVo.StartDate = startDate;
            contractExpirationVo.EndDate = endDate;
            contractExpirationVo.Memo = memo;
            contractExpirationVo.Picture = (byte[])new ImageConverter().ConvertTo(picture, typeof(byte[])); // 写真
            contractExpirationVo.InsertPcName = Environment.MachineName;
            contractExpirationVo.InsertYmdHms = DateTime.Now;
            contractExpirationVo.UpdatePcName = Environment.MachineName;
            contractExpirationVo.UpdateYmdHms = DateTime.Now;
            contractExpirationVo.DeletePcName = Environment.MachineName;
            contractExpirationVo.DeleteYmdHms = DateTime.Now;
            contractExpirationVo.DeleteFlag = false;
            return contractExpirationVo;
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeControl() {
            this.LabelExUnionCode.Text = string.Empty;
            this.LabelExDisplayName.Text = string.Empty;
            this.LabelExBelongs.Text = string.Empty;
            this.LabelExOccupation.Text = string.Empty;
            this.LabelExJobForm.Text = string.Empty;
            this.InitializeComboBoxExBelongs();
            this.NUDExContractExpirationPeriod.Value = 0;
            this.TextBoxExContractExpirationPeriod.Text = string.Empty;
            this.CheckBoxExCheckFlag.Checked = false;
            this.InitializeComboBoxExPayDetail();
            this.NUDExPay.Value = 0;
            this.ComboBoxExTravelCostDetail.SelectedIndex = 0;
            this.NUDExTravelCost.Value = 0;
            this.InitializeComboBoxExJobDescription();
            this.ComboBoxExWorkTime.Text = string.Empty;
            this.ComboBoxExBreakTime.Text = string.Empty;

            /*
             * 体験入社期間
             */
            this.DTPExExpirationStartDate.SetClear();
            this.DTPExExpirationEndDate.SetClear();
            this.TextBoxExExpirationMemo.Text = string.Empty;
            /*
             * 長期アルバイト更新期間
             */
            this.DTPExContractExpirationPartTimeJobStartDate.SetClear();
            this.DTPExContractExpirationPartTimeJobEndDate.SetClear();
            this.TextBoxExContractExpirationPartTimeJobMemo.Text = string.Empty;
            this.DTPExContractExpirationPartTimeJobStartDate1.SetClear();
            this.DTPExContractExpirationPartTimeJobEndDate1.SetClear();
            this.TextBoxExContractExpirationPartTimeJobMemo1.Text = string.Empty;
            this.DTPExContractExpirationPartTimeJobStartDate2.SetClear();
            this.DTPExContractExpirationPartTimeJobEndDate2.SetClear();
            this.TextBoxExContractExpirationPartTimeJobMemo2.Text = string.Empty;
            /*
             * 組合長期雇用期間
             */
            this.DTPExContractExpirationLongJobStartDate.SetClear();
            this.DTPExContractExpirationLongJobEndDate.SetClear();
            this.TextBoxExContractExpirationLongJobMemo.Text = string.Empty;
            this.DTPExContractExpirationLongJobStartDate1.SetClear();
            this.DTPExContractExpirationLongJobEndDate1.SetClear();
            this.TextBoxExContractExpirationLongJobMemo1.Text = string.Empty;
            this.DTPExContractExpirationLongJobStartDate2.SetClear();
            this.DTPExContractExpirationLongJobEndDate2.SetClear();
            this.TextBoxExContractExpirationLongJobMemo2.Text = string.Empty;
            /*
             * 組合短期雇用期間
             */
            this.DTPExContractExpirationShortJobStartDate.SetClear();
            this.DTPExContractExpirationShortJobEndDate.SetClear();
            this.TextBoxExContractExpirationShortJobMemo.Text = string.Empty;
            this.DTPExContractExpirationShortJobStartDate1.SetClear();
            this.DTPExContractExpirationShortJobEndDate1.SetClear();
            this.TextBoxExContractExpirationShortJobMemo1.Text = string.Empty;
            this.DTPExContractExpirationShortJobStartDate2.SetClear();
            this.DTPExContractExpirationShortJobEndDate2.SetClear();
            this.TextBoxExContractExpirationShortJobMemo2.Text = string.Empty;
            /*
             * 誓約書期間
             */
            this.DTPExContractExpirationWrittenPledgeStartDate.SetClear();
            this.DTPExContractExpirationWrittenPledgeEndDate.SetClear();
            this.TextBoxExContractExpirationWrittenPledgeMemo.Text = string.Empty;
            this.DTPExContractExpirationWrittenPledgeStartDate1.SetClear();
            this.DTPExContractExpirationWrittenPledgeEndDate1.SetClear();
            this.TextBoxExContractExpirationWrittenPledgeMemo1.Text = string.Empty;
            /*
             * 失墜行為書類期間
             */
            this.DTPExContractExpirationLossWrittenPledgeStartDate.SetClear();
            this.DTPExContractExpirationLossWrittenPledgeEndDate.SetClear();
            this.TextBoxExContractExpirationLossWrittenPledgeMemo.Text = string.Empty;
            this.DTPExContractExpirationLossWrittenPledgeStartDate1.SetClear();
            this.DTPExContractExpirationLossWrittenPledgeEndDate1.SetClear();
            this.TextBoxExContractExpirationLossWrittenPledgeMemo1.Text = string.Empty;
            /*
             * 契約満了通知(事前通知書)
             */
            this.DTPExContractExpirationNoticeStartDate.SetClear();
            this.DTPExContractExpirationNoticeEndDate.SetClear();
            this.TextBoxExContractExpirationNoticeMemo.Text = string.Empty;
            this.DTPExContractExpirationNoticeStartDate1.SetClear();
            this.DTPExContractExpirationNoticeEndDate1.SetClear();
            this.TextBoxExContractExpirationNoticeMemo1.Text = string.Empty;
        }

        /// <summary>
        /// ToolStripMenuItem_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                /*
                 * Picture クリップボード
                 */
                case "ToolStripMenuItemPictureClip":
                    PictureBoxEx1.Image = (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap);
                    break;
                /*
                 * Picture 削除
                 */
                case "ToolStripMenuItemPictureDelete":
                    PictureBoxEx1.Image = null;
                    break;
                /*
                 * アプリケーションを終了する
                 */
                case "ToolStripMenuItemExit":
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// InitializeComboBoxExBelongs
        /// /// ComboBoxにデータを入れる
        /// </summary>
        private void InitializeComboBoxExBelongs() {
            this.ComboBoxExBelongs.Items.Clear();
            DataTable dataTable = new();
            dataTable.Columns.Add("Code");
            dataTable.Columns.Add("Name");
            foreach (BelongsMasterVo belongsMasterVo in _belongsMasterDao.SelectAllBelongsMaster())
                dataTable.Rows.Add(belongsMasterVo.Code, belongsMasterVo.Name);
            this.ComboBoxExBelongs.DataSource = dataTable;
            this.ComboBoxExBelongs.ValueMember = "Code";
            this.ComboBoxExBelongs.DisplayMember = "Name";
        }

        /// <summary>
        /// InitializeComboBoxExJobDescription
        /// ComboBoxにデータを入れる
        /// </summary>
        private void InitializeComboBoxExJobDescription() {
            this.ComboBoxExJobDescription.Items.Clear();
            DataTable dataTable = new();
            dataTable.Columns.Add("Code");
            dataTable.Columns.Add("Name");
            foreach (JobDescriptionMasterVo jobDescriptionMasterVo in _jobDescriptionMasterDao.SelectAllJobDescriptionMaster())
                dataTable.Rows.Add(jobDescriptionMasterVo.Code, jobDescriptionMasterVo.Name);
            this.ComboBoxExJobDescription.DataSource = dataTable;
            this.ComboBoxExJobDescription.ValueMember = "Code";
            this.ComboBoxExJobDescription.DisplayMember = "Name";
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeComboBoxExPayDetail() {
            this.ComboBoxExPayDetail.Items.Clear();
            foreach (string payDetail in _employmentAgreementDao.SelectGroupPayDetail())
                this.ComboBoxExPayDetail.Items.Add(payDetail);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmploymentAgreementDetail_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
