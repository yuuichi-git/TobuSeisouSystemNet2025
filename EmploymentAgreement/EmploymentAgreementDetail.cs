/*
 * 2024-11-05
 */
using System.Data;

using CcControl;

using Common;

using Dao;

using Vo;

namespace EmploymentAgreement {
    public partial class EmploymentAgreementDetail : Form {
        /*
         * インスタンス作成
         */
        private readonly DateUtility _dateUtility = new();
        private readonly PdfUtility _pdfUtility = new();
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
        /// コンストラクター
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
                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "基本台帳を修正します。";
            } else {
                this.ButtonExUpdate.Text = "新規登録";
                this.PutControlHead();
                this.PutControlInitializeBody();
                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "基本台帳が存在しません。新規登録します。";
            }
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
        private void ButtonEx_Click(object sender, EventArgs e) {
            switch (((CcButton)sender).Name) {
                case "CcButtonUpdate":
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
                case "CcButtonExpiration":
                    if (CcPictureBox1.Image == null) {
                        MessageBox.Show("画像を追加してください。", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    CcDateTimeExpirationStartDate.Enabled = false;
                    CcDateTimeExpirationEndDate.Enabled = false;
                    CcTextBoxExpirationMemo.Enabled = false;
                    CcButtonExpiration.Enabled = false;
                    try {
                        _contractExpirationDao.InsertOneContractExpiration(SetContractExpirationVo(21,
                                                                                                   CcDateTimeExpirationStartDate.GetDate(),
                                                                                                   CcDateTimeExpirationEndDate.GetDate(),
                                                                                                   CcTextBoxExpirationMemo.Text,
                                                                                                   CcPictureBox1.Image));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    this.PutExpiration(_contractExpirationDao.SelectOneContractExpirationP(_staffMasterVo.StaffCode));
                    break;
                // 継続アルバイト更新期間
                case "CcButtonContractExpirationPartTimeJob":
                    if (CcPictureBox1.Image == null) {
                        MessageBox.Show("画像を追加してください。", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    CcDateTimeContractExpirationPartTimeJobStartDate.Enabled = false;
                    CcDateTimeContractExpirationPartTimeJobEndDate.Enabled = false;
                    CcTextBoxContractExpirationPartTimeJobMemo.Enabled = false;
                    CcButtonContractExpirationPartTimeJob.Enabled = false;
                    try {
                        if (!_contractExpirationDao.ExistenceContractExpiration(20,
                                                                                _staffMasterVo.StaffCode,
                                                                                CcDateTimeContractExpirationPartTimeJobStartDate.GetDate().Date,
                                                                                CcDateTimeContractExpirationPartTimeJobEndDate.GetDate().Date)) {
                            _contractExpirationDao.InsertOneContractExpiration(SetContractExpirationVo(20,
                                                                                                       CcDateTimeContractExpirationPartTimeJobStartDate.GetDate().Date,
                                                                                                       CcDateTimeContractExpirationPartTimeJobEndDate.GetDate().Date,
                                                                                                       CcTextBoxContractExpirationPartTimeJobMemo.Text,
                                                                                                       CcPictureBox1.Image));
                        } else {
                            _contractExpirationDao.UpdateOneContractExpiration(SetContractExpirationVo(20,
                                                                                                       CcDateTimeContractExpirationPartTimeJobStartDate.GetDate().Date,
                                                                                                       CcDateTimeContractExpirationPartTimeJobEndDate.GetDate().Date,
                                                                                                       CcTextBoxContractExpirationPartTimeJobMemo.Text,
                                                                                                       CcPictureBox1.Image));
                        }
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    this.PutContractExpirationPartTimeJob(_contractExpirationDao.SelectOneContractExpirationP(_staffMasterVo.StaffCode));
                    break;
                // 組合長期雇用期間
                case "CcButtonContractExpirationLongJob":
                    if (CcPictureBox1.Image == null) {
                        MessageBox.Show("画像を追加してください。", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    CcDateTimeContractExpirationLongJobStartDate.Enabled = false;
                    CcDateTimeContractExpirationLongJobEndDate.Enabled = false;
                    CcTextBoxContractExpirationLongJobMemo.Enabled = false;
                    CcButtonContractExpirationLongJob.Enabled = false;
                    try {
                        if (!_contractExpirationDao.ExistenceContractExpiration(10,
                                                                                _staffMasterVo.StaffCode,
                                                                                CcDateTimeContractExpirationLongJobStartDate.GetDate().Date,
                                                                                CcDateTimeContractExpirationLongJobEndDate.GetDate().Date)) {
                            _contractExpirationDao.InsertOneContractExpiration(SetContractExpirationVo(10,
                                                                                                       CcDateTimeContractExpirationLongJobStartDate.GetDate().Date,
                                                                                                       CcDateTimeContractExpirationLongJobEndDate.GetDate().Date,
                                                                                                       CcTextBoxContractExpirationLongJobMemo.Text,
                                                                                                       CcPictureBox1.Image));
                        } else {
                            _contractExpirationDao.UpdateOneContractExpiration(SetContractExpirationVo(10,
                                                                                                       CcDateTimeContractExpirationLongJobStartDate.GetDate().Date,
                                                                                                       CcDateTimeContractExpirationLongJobEndDate.GetDate().Date,
                                                                                                       CcTextBoxContractExpirationLongJobMemo.Text,
                                                                                                       CcPictureBox1.Image));
                        }
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    this.PutContractExpirationLongJob(_contractExpirationDao.SelectOneContractExpirationP(_staffMasterVo.StaffCode));
                    break;
                // 組合短期雇用期間
                case "CcButtonContractExpirationShortJob":
                    if (CcPictureBox1.Image == null) {
                        MessageBox.Show("画像を追加してください。", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    CcDateTimeContractExpirationShortJobStartDate.Enabled = false;
                    CcDateTimeContractExpirationShortJobEndDate.Enabled = false;
                    CcTextBoxContractExpirationShortJobMemo.Enabled = false;
                    CcButtonContractExpirationShortJob.Enabled = false;
                    try {
                        if (!_contractExpirationDao.ExistenceContractExpiration(11,
                                                                                _staffMasterVo.StaffCode,
                                                                                CcDateTimeContractExpirationShortJobStartDate.GetDate().Date,
                                                                                CcDateTimeContractExpirationShortJobEndDate.GetDate().Date)) {
                            _contractExpirationDao.InsertOneContractExpiration(SetContractExpirationVo(11,
                                                                                                       CcDateTimeContractExpirationShortJobStartDate.GetDate().Date,
                                                                                                       CcDateTimeContractExpirationShortJobEndDate.GetDate().Date,
                                                                                                       CcTextBoxContractExpirationShortJobMemo.Text,
                                                                                                       CcPictureBox1.Image));
                        } else {
                            _contractExpirationDao.UpdateOneContractExpiration(SetContractExpirationVo(11,
                                                                                                       CcDateTimeContractExpirationShortJobStartDate.GetDate().Date,
                                                                                                       CcDateTimeContractExpirationShortJobEndDate.GetDate().Date,
                                                                                                       CcTextBoxContractExpirationShortJobMemo.Text,
                                                                                                       CcPictureBox1.Image));
                        }
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    this.PutContractExpirationShortJob(_contractExpirationDao.SelectOneContractExpirationP(_staffMasterVo.StaffCode));
                    break;
                // 誓約書
                case "CcButtonContractExpirationWrittenPledge":
                    if (CcPictureBox1.Image == null) {
                        MessageBox.Show("画像を追加してください。", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    CcDateTimeContractExpirationWrittenPledgeStartDate.Enabled = false;
                    CcDateTimeContractExpirationWrittenPledgeEndDate.Enabled = false;
                    CcTextBoxContractExpirationWrittenPledgeMemo.Enabled = false;
                    CcButtonContractExpirationWrittenPledge.Enabled = false;
                    try {
                        if (!_contractExpirationDao.ExistenceContractExpiration(30,
                                                                                _staffMasterVo.StaffCode,
                                                                                CcDateTimeContractExpirationWrittenPledgeStartDate.GetDate().Date,
                                                                                CcDateTimeContractExpirationWrittenPledgeEndDate.GetDate().Date)) {
                            _contractExpirationDao.InsertOneContractExpiration(SetContractExpirationVo(30,
                                                                                                       CcDateTimeContractExpirationWrittenPledgeStartDate.GetDate().Date,
                                                                                                       CcDateTimeContractExpirationWrittenPledgeEndDate.GetDate().Date,
                                                                                                       CcTextBoxContractExpirationWrittenPledgeMemo.Text,
                                                                                                       CcPictureBox1.Image));
                        } else {
                            _contractExpirationDao.UpdateOneContractExpiration(SetContractExpirationVo(30,
                                                                                                       CcDateTimeContractExpirationWrittenPledgeStartDate.GetDate().Date,
                                                                                                       CcDateTimeContractExpirationWrittenPledgeEndDate.GetDate().Date,
                                                                                                       CcTextBoxContractExpirationWrittenPledgeMemo.Text,
                                                                                                       CcPictureBox1.Image));
                        }
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    this.PutContractExpirationWrittenPledge(_contractExpirationDao.SelectOneContractExpirationP(_staffMasterVo.StaffCode));
                    break;
                // 失墜行為
                case "CcButtonContractExpirationLossWrittenPledge":
                    if (CcPictureBox1.Image == null) {
                        MessageBox.Show("画像を追加してください。", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    CcDateTimeContractExpirationLossWrittenPledgeStartDate.Enabled = false;
                    CcDateTimeContractExpirationLossWrittenPledgeEndDate.Enabled = false;
                    CcTextBoxContractExpirationLossWrittenPledgeMemo.Enabled = false;
                    CcButtonContractExpirationLossWrittenPledge.Enabled = false;
                    try {
                        if (!_contractExpirationDao.ExistenceContractExpiration(40,
                                                                                _staffMasterVo.StaffCode,
                                                                                CcDateTimeContractExpirationLossWrittenPledgeStartDate.GetDate().Date,
                                                                                CcDateTimeContractExpirationLossWrittenPledgeEndDate.GetDate().Date)) {
                            _contractExpirationDao.InsertOneContractExpiration(SetContractExpirationVo(40,
                                                                                                       CcDateTimeContractExpirationLossWrittenPledgeStartDate.GetDate().Date,
                                                                                                       CcDateTimeContractExpirationLossWrittenPledgeEndDate.GetDate().Date,
                                                                                                       CcTextBoxContractExpirationLossWrittenPledgeMemo.Text,
                                                                                                       CcPictureBox1.Image));
                        } else {
                            _contractExpirationDao.UpdateOneContractExpiration(SetContractExpirationVo(40,
                                                                                                       CcDateTimeContractExpirationLossWrittenPledgeStartDate.GetDate().Date,
                                                                                                       CcDateTimeContractExpirationLossWrittenPledgeEndDate.GetDate().Date,
                                                                                                       CcTextBoxContractExpirationLossWrittenPledgeMemo.Text,
                                                                                                       CcPictureBox1.Image));
                        }
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    this.PutContractExpirationLossWrittenPledge(_contractExpirationDao.SelectOneContractExpirationP(_staffMasterVo.StaffCode));
                    break;
                // 契約満了通知
                case "CcButtonContractExpirationNotice":
                    if (CcPictureBox1.Image == null) {
                        MessageBox.Show("画像を追加してください。", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    CcDateTimeContractExpirationNoticeStartDate.Enabled = false;
                    CcDateTimeContractExpirationNoticeEndDate.Enabled = false;
                    CcTextBoxContractExpirationNoticeMemo.Enabled = false;
                    CcButtonContractExpirationNotice.Enabled = false;
                    try {
                        if (!_contractExpirationDao.ExistenceContractExpiration(50,
                                                                                _staffMasterVo.StaffCode,
                                                                                CcDateTimeContractExpirationNoticeStartDate.GetDate().Date,
                                                                                CcDateTimeContractExpirationNoticeEndDate.GetDate().Date)) {
                            _contractExpirationDao.InsertOneContractExpiration(SetContractExpirationVo(50,
                                                                                                       CcDateTimeContractExpirationNoticeStartDate.GetDate().Date,
                                                                                                       CcDateTimeContractExpirationNoticeEndDate.GetDate().Date,
                                                                                                       CcTextBoxContractExpirationNoticeMemo.Text,
                                                                                                       CcPictureBox1.Image));
                        } else {
                            _contractExpirationDao.UpdateOneContractExpiration(SetContractExpirationVo(50,
                                                                                                       CcDateTimeContractExpirationNoticeStartDate.GetDate().Date,
                                                                                                       CcDateTimeContractExpirationNoticeEndDate.GetDate().Date,
                                                                                                       CcTextBoxContractExpirationNoticeMemo.Text,
                                                                                                       CcPictureBox1.Image));
                        }
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    this.PutContractExpirationNotice(_contractExpirationDao.SelectOneContractExpirationP(_staffMasterVo.StaffCode));
                    break;

                // 体験入社期間 画像
                case "CcButtonExpirationPicture":
                    if (this.CcButtonExpirationPicture.Tag is not null) {
                        new EmploymentAgreementView((byte[])this.CcButtonExpirationPicture.Tag).Show();
                    } else {
                        MessageBox.Show("体験入社期間の契約書が添付されていません。");
                    }
                    break;
                // 長期アルバイト更新期間　画像１
                case "CcButtonContractExpirationPartTimeJobPicture1":
                    if (this.CcButtonContractExpirationPartTimeJobPicture1.Tag is not null) {
                        new EmploymentAgreementView((byte[])this.CcButtonContractExpirationPartTimeJobPicture1.Tag).Show();
                    } else {
                        MessageBox.Show("長期アルバイト更新期間の契約書が添付されていません。");
                    }
                    break;
                // 長期アルバイト更新期間　画像２
                case "CcButtonContractExpirationPartTimeJobPicture2":
                    if (this.CcButtonContractExpirationPartTimeJobPicture2.Tag is not null) {
                        new EmploymentAgreementView((byte[])this.CcButtonContractExpirationPartTimeJobPicture2.Tag).Show();
                    } else {
                        MessageBox.Show("長期アルバイト更新期間の契約書が添付されていません。");
                    }
                    break;
                // 組合長期雇用期間　画像１
                case "CcButtonContractExpirationLongJobPicture1":
                    if (this.CcButtonContractExpirationLongJobPicture1.Tag is not null) {
                        new EmploymentAgreementView((byte[])this.CcButtonContractExpirationLongJobPicture1.Tag).Show();
                    } else {
                        MessageBox.Show("組合長期雇用期間の契約書が添付されていません。");
                    }
                    break;
                // 組合長期雇用期間　画像２
                case "CcButtonContractExpirationLongJobPicture2":
                    if (this.CcButtonContractExpirationLongJobPicture2.Tag is not null) {
                        new EmploymentAgreementView((byte[])this.CcButtonContractExpirationLongJobPicture2.Tag).Show();
                    } else {
                        MessageBox.Show("組合長期雇用期間の契約書が添付されていません。");
                    }
                    break;
                // 組合短期雇用期間　画像１
                case "CcButtonContractExpirationShortJobPicture1":
                    if (this.CcButtonContractExpirationShortJobPicture1.Tag is not null) {
                        new EmploymentAgreementView((byte[])this.CcButtonContractExpirationShortJobPicture1.Tag).Show();
                    } else {
                        MessageBox.Show("組合短期雇用期間の契約書が添付されていません。");
                    }
                    break;
                // 組合短期雇用期間　画像２
                case "CcButtonContractExpirationShortJobPicture2":
                    if (this.CcButtonContractExpirationShortJobPicture2.Tag is not null) {
                        new EmploymentAgreementView((byte[])this.CcButtonContractExpirationShortJobPicture2.Tag).Show();
                    } else {
                        MessageBox.Show("組合短期雇用期間の契約書が添付されていません。");
                    }
                    break;
                // 誓約書期間　画像１
                case "CcButtonContractExpirationWrittenPledgePicture1":
                    if (this.CcButtonContractExpirationWrittenPledgePicture1.Tag is not null) {
                        new EmploymentAgreementView((byte[])this.CcButtonContractExpirationWrittenPledgePicture1.Tag).Show();
                    } else {
                        MessageBox.Show("誓約書が添付されていません。");
                    }
                    break;
                // 失墜行為書類期間　画像１
                case "CcButtonContractExpirationLossWrittenPledgePicture1":
                    if (this.CcButtonContractExpirationLossWrittenPledgePicture1.Tag is not null) {
                        new EmploymentAgreementView((byte[])this.CcButtonContractExpirationLossWrittenPledgePicture1.Tag).Show();
                    } else {
                        MessageBox.Show("失墜行為確認書が添付されていません。");
                    }
                    break;
                // 契約満了通知(事前通知書)　画像１
                case "CcButtonContractExpirationNoticePicture1":
                    if (this.CcButtonContractExpirationNoticePicture1.Tag is not null) {
                        new EmploymentAgreementView((byte[])this.CcButtonContractExpirationNoticePicture1.Tag).Show();
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
            this.CcLabelUnionCode.Text = _staffMasterVo.UnionCode.ToString("###");
            this.CcLabelDisplayName.Text = _staffMasterVo.DisplayName;
            this.CcLabelBelongs.Text = _dictionaryBelongs[_staffMasterVo.Belongs];
            this.CcLabelOccupation.Text = _dictionaryOccupation[_staffMasterVo.Occupation];
            this.CcLabelJobForm.Text = _dictionaryJobForm[_staffMasterVo.JobForm];
        }

        /// <summary>
        /// 新規の場合の初期値をセットする
        /// </summary>
        private void PutControlInitializeBody() {

        }

        /// <summary>
        /// 修正の場合の値をセットする
        /// </summary>
        private void PutControlBody() {
            this.CcComboBoxBaseAddress.Text = _employmentAgreementVo.BaseLocation; // 勤務地
            this.CcComboBoxBelongs.SelectedValue = _staffMasterVo.Belongs; // 雇用形態
            this.CcNumericUpDownContractExpirationPeriod.Value = _employmentAgreementVo.ContractExpirationPeriod; // 契約期間
            /*
             * 契約期間文字を計算
             */
            DateTime date = DateTime.Now;
            string time = string.Empty;
            if (_employmentAgreementVo.ContractExpirationPeriodString != string.Empty) {
                this.CcTextBoxContractExpirationPeriod.Text = _employmentAgreementVo.ContractExpirationPeriodString;
            } else {
                switch (_employmentAgreementVo.ContractExpirationPeriod) {
                    case 0:
                        time = string.Concat(date.Date.ToString("yyyy年MM月dd日"), " ～ ", date.Date.AddDays(6).ToString("yyyy年MM月dd日"));
                        this.CcTextBoxContractExpirationPeriod.Text = time;
                        break;
                    case 1:         // １カ月更新の場合
                        time = string.Concat(date.Date.ToString("yyyy年MM月dd日"), " ～ ", _dateUtility.GetEndOfMonth(date).ToString("yyyy年MM月dd日"));
                        this.CcTextBoxContractExpirationPeriod.Text = time;
                        break;
                    case 2:         // ２カ月更新の場合
                        time = string.Concat(date.Date.ToString("yyyy年MM月dd日"), " ～ ", _dateUtility.GetEndOfMonth(date.AddMonths(1)).ToString("yyyy年MM月dd日"));
                        this.CcTextBoxContractExpirationPeriod.Text = time;
                        break;
                    case 3:         // ３カ月更新の場合
                        time = string.Concat(date.Date.ToString("yyyy年MM月dd日"), " ～ ", _dateUtility.GetEndOfMonth(date.AddMonths(2)).ToString("yyyy年MM月dd日"));
                        this.CcTextBoxContractExpirationPeriod.Text = time;
                        break;
                    case 6:         // ６カ月更新の場合
                        time = string.Concat(date.Date.ToString("yyyy年MM月dd日"), " ～ ", _dateUtility.GetEndOfMonth(date.AddMonths(5)).ToString("yyyy年MM月dd日"));
                        this.CcTextBoxContractExpirationPeriod.Text = time;
                        break;
                    case 12:        // １２カ月更新の場合
                        time = string.Concat(_dateUtility.GetFiscalYearStartDate(date).ToString("yyyy年MM月dd日"), " ～ ", _dateUtility.GetFiscalYearEndDate(date).AddDays(-1).ToString("yyyy年MM月dd日"));
                        this.CcTextBoxContractExpirationPeriod.Text = time;
                        break;
                    default:
                        this.CcTextBoxContractExpirationPeriod.Text = string.Empty;
                        break;
                }
            }
            this.CcCheckBoxCheckFlag.Checked = _employmentAgreementVo.CheckFlag;                        // 各労共に提出中
            this.CcComboBoxPayDetail.Text = _employmentAgreementVo.PayDetail;                           // 給与区分
            this.CcNumericUpDownPay.Value = _employmentAgreementVo.Pay;                                 // 給与単価
            this.CcComboBoxTravelCostDetail.Text = _employmentAgreementVo.TravelCostDetail;             // 交通費区分
            this.CcNumericUpDownTravelCost.Value = _employmentAgreementVo.TravelCost;                   // 交通費
            this.CcComboBoxJobDescription.SelectedValue = _employmentAgreementVo.JobDescription;        // 従事すべき業務内容
            this.CcComboBoxWorkTime.Text = _employmentAgreementVo.WorkTime;                             // 勤務時間
            this.CcComboBoxBreakTime.Text = _employmentAgreementVo.BreakTime;                           // 休憩時間
            this.CcCheckBoxKOYOU.Checked = _employmentAgreementVo.KoyouFlag;                            // 雇用保険の有無
            this.CcCheckBoxSYAKAI.Checked = _employmentAgreementVo.SyakaiFlag;                          // 社会保険の有無
            this.CcComboBoxSalaryRaise.Text = _employmentAgreementVo.SalaryRaise;                       // 昇給の有無
            this.CcComboBoxBonusSummerText.Text = _employmentAgreementVo.BonusSummerText;               // 夏賞与の有無
            this.CcNumericUpDownBonusSummerPay.Value = _employmentAgreementVo.BonusSummerPay;           // 夏賞与の金額
            this.CcComboBoxBonusWinterText.Text = _employmentAgreementVo.BonusWinterText;               // 冬賞与の有無
            this.CcNumericUpDownBonusWinterPay.Value = _employmentAgreementVo.BonusWinterPay;           // 冬賞与の金額
            this.CcComboBoxBonusDetailText.Text = _employmentAgreementVo.BonusDetailText;
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
            Dictionary<int, CcDateTime> _dicStartDate = new() { { 0, CcDateTimeExpirationStartDate } };
            Dictionary<int, CcDateTime> _dicEndDate = new() { { 0, CcDateTimeExpirationEndDate } };
            Dictionary<int, CcTextBox> _dicMemo = new() { { 0, CcTextBoxExpirationMemo } };
            Dictionary<int, CcButton> _dicPicture = new() { { 0, CcButtonExpirationPicture } };
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
                CcButtonExpiration.Enabled = false;

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
            Dictionary<int, CcDateTime> _dicStartDate = new() { { 0, CcDateTimeContractExpirationPartTimeJobStartDate1 }, { 1, CcDateTimeContractExpirationPartTimeJobStartDate2 } };
            Dictionary<int, CcDateTime> _dicEndDate = new() { { 0, CcDateTimeContractExpirationPartTimeJobEndDate1 }, { 1, CcDateTimeContractExpirationPartTimeJobEndDate2 } };
            Dictionary<int, CcTextBox> _dicMemo = new() { { 0, CcTextBoxContractExpirationPartTimeJobMemo1 }, { 1, CcTextBoxContractExpirationPartTimeJobMemo2 } };
            Dictionary<int, CcButton> _dicPicture = new() { { 0, CcButtonContractExpirationPartTimeJobPicture1 }, { 1, CcButtonContractExpirationPartTimeJobPicture2 } };
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
            Dictionary<int, CcDateTime> _dicStartDate = new() { { 0, CcDateTimeContractExpirationLongJobStartDate1 }, { 1, CcDateTimeContractExpirationLongJobStartDate2 } };
            Dictionary<int, CcDateTime> _dicEndDate = new() { { 0, CcDateTimeContractExpirationLongJobEndDate1 }, { 1, CcDateTimeContractExpirationLongJobEndDate2 } };
            Dictionary<int, CcTextBox> _dicMemo = new() { { 0, CcTextBoxContractExpirationLongJobMemo1 }, { 1, CcTextBoxContractExpirationLongJobMemo2 } };
            Dictionary<int, CcButton> _dicPicture = new() { { 0, CcButtonContractExpirationLongJobPicture1 }, { 1, CcButtonContractExpirationLongJobPicture2 } };
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
            Dictionary<int, CcDateTime> _dicStartDate = new() { { 0, CcDateTimeContractExpirationShortJobStartDate1 }, { 1, CcDateTimeContractExpirationShortJobStartDate2 } };
            Dictionary<int, CcDateTime> _dicEndDate = new() { { 0, CcDateTimeContractExpirationShortJobEndDate1 }, { 1, CcDateTimeContractExpirationShortJobEndDate2 } };
            Dictionary<int, CcTextBox> _dicMemo = new() { { 0, CcTextBoxContractExpirationShortJobMemo1 }, { 1, CcTextBoxContractExpirationShortJobMemo2 } };
            Dictionary<int, CcButton> _dicPicture = new() { { 0, CcButtonContractExpirationShortJobPicture1 }, { 1, CcButtonContractExpirationShortJobPicture2 } };
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
            Dictionary<int, CcDateTime> _dicStartDate = new() { { 0, CcDateTimeContractExpirationWrittenPledgeStartDate1 } };
            Dictionary<int, CcDateTime> _dicEndDate = new() { { 0, CcDateTimeContractExpirationWrittenPledgeEndDate1 } };
            Dictionary<int, CcTextBox> _dicMemo = new() { { 0, CcTextBoxContractExpirationWrittenPledgeMemo1 } };
            Dictionary<int, CcButton> _dicPicture = new() { { 0, CcButtonContractExpirationWrittenPledgePicture1 } };
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
            Dictionary<int, CcDateTime> _dicStartDate = new() { { 0, CcDateTimeContractExpirationLossWrittenPledgeStartDate1 } };
            Dictionary<int, CcDateTime> _dicEndDate = new() { { 0, CcDateTimeContractExpirationLossWrittenPledgeEndDate1 } };
            Dictionary<int, CcTextBox> _dicMemo = new() { { 0, CcTextBoxContractExpirationLossWrittenPledgeMemo1 } };
            Dictionary<int, CcButton> _dicPicture = new() { { 0, CcButtonContractExpirationLossWrittenPledgePicture1 } };
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
            Dictionary<int, CcDateTime> _dicStartDate = new() { { 0, CcDateTimeContractExpirationNoticeStartDate1 } };
            Dictionary<int, CcDateTime> _dicEndDate = new() { { 0, CcDateTimeContractExpirationNoticeEndDate1 } };
            Dictionary<int, CcTextBox> _dicMemo = new() { { 0, CcTextBoxContractExpirationNoticeMemo1 } };
            Dictionary<int, CcButton> _dicPicture = new() { { 0, CcButtonContractExpirationNoticePicture1 } };
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
            employmentAgreementVo.BaseLocation = CcComboBoxBaseAddress.Text;
            employmentAgreementVo.Occupation = int.Parse(this.CcComboBoxBelongs.SelectedValue.ToString()); // 元がObject型で入ってるからキャストが必要
            employmentAgreementVo.ContractExpirationPeriod = (int)CcNumericUpDownContractExpirationPeriod.Value;
            employmentAgreementVo.ContractExpirationPeriodString = this.CcTextBoxContractExpirationPeriod.Text;
            employmentAgreementVo.PayDetail = this.CcComboBoxPayDetail.Text;
            employmentAgreementVo.Pay = (int)this.CcNumericUpDownPay.Value;
            employmentAgreementVo.TravelCostDetail = this.CcComboBoxTravelCostDetail.Text;
            employmentAgreementVo.TravelCost = (int)this.CcNumericUpDownTravelCost.Value;
            employmentAgreementVo.JobDescription = int.Parse(this.CcComboBoxJobDescription.SelectedValue.ToString()); // 元がObject型で入ってるからキャストが必要
            employmentAgreementVo.WorkTime = this.CcComboBoxWorkTime.Text;
            employmentAgreementVo.BreakTime = this.CcComboBoxBreakTime.Text;
            employmentAgreementVo.CheckFlag = this.CcCheckBoxCheckFlag.Checked;
            employmentAgreementVo.KoyouFlag = this.CcCheckBoxKOYOU.Checked;
            employmentAgreementVo.SyakaiFlag = this.CcCheckBoxSYAKAI.Checked;
            employmentAgreementVo.SalaryRaise = this.CcComboBoxSalaryRaise.Text;
            employmentAgreementVo.BonusSummerText = this.CcComboBoxBonusSummerText.Text;
            employmentAgreementVo.BonusSummerPay = (int)this.CcNumericUpDownBonusSummerPay.Value;
            employmentAgreementVo.BonusWinterText = this.CcComboBoxBonusWinterText.Text;
            employmentAgreementVo.BonusWinterPay = (int)this.CcNumericUpDownBonusWinterPay.Value;
            employmentAgreementVo.BonusDetailText = this.CcComboBoxBonusDetailText.Text;
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
            this.CcLabelUnionCode.Text = string.Empty;
            this.CcLabelDisplayName.Text = string.Empty;
            this.CcLabelBelongs.Text = string.Empty;
            this.CcLabelOccupation.Text = string.Empty;
            this.CcLabelJobForm.Text = string.Empty;
            this.InitializeComboBoxExBelongs();
            this.CcNumericUpDownContractExpirationPeriod.Value = 0;
            this.CcTextBoxContractExpirationPeriod.Text = string.Empty;
            this.CcCheckBoxCheckFlag.Checked = false;
            this.InitializeComboBoxExPayDetail();
            this.CcNumericUpDownPay.Value = 0;
            this.CcComboBoxTravelCostDetail.SelectedIndex = 0;
            this.CcNumericUpDownTravelCost.Value = 0;
            this.InitializeComboBoxExJobDescription();
            this.CcComboBoxWorkTime.Text = string.Empty;
            this.CcComboBoxBreakTime.Text = string.Empty;

            /*
             * 体験入社期間
             */
            this.CcDateTimeExpirationStartDate.SetClear();
            this.CcDateTimeExpirationEndDate.SetClear();
            this.CcTextBoxExpirationMemo.Text = string.Empty;
            /*
             * 長期アルバイト更新期間
             */
            this.CcDateTimeContractExpirationPartTimeJobStartDate.SetClear();
            this.CcDateTimeContractExpirationPartTimeJobEndDate.SetClear();
            this.CcTextBoxContractExpirationPartTimeJobMemo.Text = string.Empty;
            this.CcDateTimeContractExpirationPartTimeJobStartDate1.SetClear();
            this.CcDateTimeContractExpirationPartTimeJobEndDate1.SetClear();
            this.CcTextBoxContractExpirationPartTimeJobMemo1.Text = string.Empty;
            this.CcDateTimeContractExpirationPartTimeJobStartDate2.SetClear();
            this.CcDateTimeContractExpirationPartTimeJobEndDate2.SetClear();
            this.CcTextBoxContractExpirationPartTimeJobMemo2.Text = string.Empty;
            /*
             * 組合長期雇用期間
             */
            this.CcDateTimeContractExpirationLongJobStartDate.SetClear();
            this.CcDateTimeContractExpirationLongJobEndDate.SetClear();
            this.CcTextBoxContractExpirationLongJobMemo.Text = string.Empty;
            this.CcDateTimeContractExpirationLongJobStartDate1.SetClear();
            this.CcDateTimeContractExpirationLongJobEndDate1.SetClear();
            this.CcTextBoxContractExpirationLongJobMemo1.Text = string.Empty;
            this.CcDateTimeContractExpirationLongJobStartDate2.SetClear();
            this.CcDateTimeContractExpirationLongJobEndDate2.SetClear();
            this.CcTextBoxContractExpirationLongJobMemo2.Text = string.Empty;
            /*
             * 組合短期雇用期間
             */
            this.CcDateTimeContractExpirationShortJobStartDate.SetClear();
            this.CcDateTimeContractExpirationShortJobEndDate.SetClear();
            this.CcTextBoxContractExpirationShortJobMemo.Text = string.Empty;
            this.CcDateTimeContractExpirationShortJobStartDate1.SetClear();
            this.CcDateTimeContractExpirationShortJobEndDate1.SetClear();
            this.CcTextBoxContractExpirationShortJobMemo1.Text = string.Empty;
            this.CcDateTimeContractExpirationShortJobStartDate2.SetClear();
            this.CcDateTimeContractExpirationShortJobEndDate2.SetClear();
            this.CcTextBoxContractExpirationShortJobMemo2.Text = string.Empty;
            /*
             * 誓約書期間
             */
            this.CcDateTimeContractExpirationWrittenPledgeStartDate.SetClear();
            this.CcDateTimeContractExpirationWrittenPledgeEndDate.SetClear();
            this.CcTextBoxContractExpirationWrittenPledgeMemo.Text = string.Empty;
            this.CcDateTimeContractExpirationWrittenPledgeStartDate1.SetClear();
            this.CcDateTimeContractExpirationWrittenPledgeEndDate1.SetClear();
            this.CcTextBoxContractExpirationWrittenPledgeMemo1.Text = string.Empty;
            /*
             * 失墜行為書類期間
             */
            this.CcDateTimeContractExpirationLossWrittenPledgeStartDate.SetClear();
            this.CcDateTimeContractExpirationLossWrittenPledgeEndDate.SetClear();
            this.CcTextBoxContractExpirationLossWrittenPledgeMemo.Text = string.Empty;
            this.CcDateTimeContractExpirationLossWrittenPledgeStartDate1.SetClear();
            this.CcDateTimeContractExpirationLossWrittenPledgeEndDate1.SetClear();
            this.CcTextBoxContractExpirationLossWrittenPledgeMemo1.Text = string.Empty;
            /*
             * 契約満了通知(事前通知書)
             */
            this.CcDateTimeContractExpirationNoticeStartDate.SetClear();
            this.CcDateTimeContractExpirationNoticeEndDate.SetClear();
            this.CcTextBoxContractExpirationNoticeMemo.Text = string.Empty;
            this.CcDateTimeContractExpirationNoticeStartDate1.SetClear();
            this.CcDateTimeContractExpirationNoticeEndDate1.SetClear();
            this.CcTextBoxContractExpirationNoticeMemo1.Text = string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ContextMenuStripEx_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            if (sender is not ContextMenuStrip contextMenuStrip)
                return;

            if (contextMenuStrip.SourceControl is not CcPictureBox ccPictureBox)
                return;

            switch (e.ClickedItem.Name) {
                case "ToolStripMenuItemOpen":
                    Bitmap bitmap = await _pdfUtility.ConvertPdfToImage(contextMenuStrip);
                    if (bitmap is not null) {
                        ccPictureBox.Image = bitmap;
                    }
                    break;

                case "ToolStripMenuItemPaste":
                    IDataObject data = Clipboard.GetDataObject();
                    if (data?.GetDataPresent(DataFormats.Bitmap) == true) {
                        ccPictureBox.Image = (Bitmap)data.GetData(DataFormats.Bitmap);
                    }
                    break;

                case "ToolStripMenuItemDelete":
                    ccPictureBox.Image = null;
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
        /// InitializeComboBoxExBelongs
        /// /// ComboBoxにデータを入れる
        /// </summary>
        private void InitializeComboBoxExBelongs() {
            this.CcComboBoxBelongs.Items.Clear();
            DataTable dataTable = new();
            dataTable.Columns.Add("Code");
            dataTable.Columns.Add("Name");
            foreach (BelongsMasterVo belongsMasterVo in _belongsMasterDao.SelectAllBelongsMaster())
                dataTable.Rows.Add(belongsMasterVo.Code, belongsMasterVo.Name);
            this.CcComboBoxBelongs.DataSource = dataTable;
            this.CcComboBoxBelongs.ValueMember = "Code";
            this.CcComboBoxBelongs.DisplayMember = "Name";
        }

        /// <summary>
        /// InitializeComboBoxExJobDescription
        /// ComboBoxにデータを入れる
        /// </summary>
        private void InitializeComboBoxExJobDescription() {
            this.CcComboBoxJobDescription.Items.Clear();
            DataTable dataTable = new();
            dataTable.Columns.Add("Code");
            dataTable.Columns.Add("Name");
            foreach (JobDescriptionMasterVo jobDescriptionMasterVo in _jobDescriptionMasterDao.SelectAllJobDescriptionMaster())
                dataTable.Rows.Add(jobDescriptionMasterVo.Code, jobDescriptionMasterVo.Name);
            this.CcComboBoxJobDescription.DataSource = dataTable;
            this.CcComboBoxJobDescription.ValueMember = "Code";
            this.CcComboBoxJobDescription.DisplayMember = "Name";
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeComboBoxExPayDetail() {
            this.CcComboBoxPayDetail.Items.Clear();
            foreach (string payDetail in _employmentAgreementDao.SelectGroupPayDetail())
                this.CcComboBoxPayDetail.Items.Add(payDetail);
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
