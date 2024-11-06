/*
 * 2024-11-05
 */
using ControlEx;

using Dao;

using Vo;

namespace EmploymentAgreement {
    public partial class EmploymentAgreementDetail : Form {
        private readonly DateTime _defaultDateTime = new DateTime(1900, 01, 01);
        private readonly Dictionary<int, string> _dictionaryBelongs = new() { { 10, "役員" }, { 11, "社員" }, { 12, "アルバイト" }, { 13, "派遣" }, { 20, "新運転" }, { 21, "自運労" }, { 99, "" } };
        private readonly Dictionary<int, string> _dictionaryOccupation = new() { { 10, "運転手" }, { 11, "作業員" }, { 20, "事務職" }, { 99, "" } };
        private readonly Dictionary<int, string> _dictionaryJobForm = new() { { 10, "長期雇用" }, { 11, "短期雇用" }, { 99, "" } };
        /*
         * Dao
         */
        private EmploymentAgreementDao _employmentAgreementDao;
        /*
         * Vo
         */
        private StaffMasterVo _staffMasterVo;
        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        public EmploymentAgreementDetail(ConnectionVo connectionVo, StaffMasterVo staffMasterVo) {
            /*
             * Dao
             */
            _employmentAgreementDao = new(connectionVo);
            /*
             * Vo
             */
            _staffMasterVo = staffMasterVo;
            /*
             * コントロール初期化
             */
            InitializeComponent();
            this.InitializeControl();
            this.PutControl(_employmentAgreementDao.SelectOneEmploymentAgreement(_staffMasterVo.StaffCode));
            if (_employmentAgreementDao.CheckRecord(_staffMasterVo.StaffCode)) {
                this.ButtonExUpdate.Text = "更　新";
                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "この従事者の基本台帳が存在します。";
            } else {
                this.ButtonExUpdate.Text = "新規登録";
                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "この従事者の基本台帳が存在しません。";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEx_Click(object sender, EventArgs e) {
            switch (((ButtonEx)sender).Name) {
                // UPDATE
                case "ButtonExUpdate":
                    try {
                        if (_employmentAgreementDao.CheckRecord(_staffMasterVo.StaffCode)) {
                            // UPDATE
                            _employmentAgreementDao.UpdateOneEmploymentAgreement(SetEmploymentAgreementVo());
                        } else {
                            // INSERT
                            _employmentAgreementDao.InsertOneEmploymentAgreement(SetEmploymentAgreementVo());
                        }
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    this.Close();
                    break;
                // アルバイト用リスト
                case "BTNExContractExpirationPartTimeJob":

                    break;
                // 長期雇用リスト
                case "BTNExContractExpirationLongJob":

                    break;
                // 短期雇用リスト
                case "BTNExContractExpirationShortJob":

                    break;
                // 誓約書
                case "BTNExWrittenPledge":

                    break;
                // 失墜行為
                case "BTNExLossWrittenPledge":

                    break;
                // 契約満了通知
                case "BTNExContractExpirationNotice":

                    break;
                // 体験入社期間 画像
                case "BTNExExpirationPicture":
                    if (this.BTNExExpirationPicture.Tag is not null) {
                        new ShowPicture((byte[])this.BTNExExpirationPicture.Tag).ShowDialog(this);
                    } else {
                        MessageBox.Show("体験入社期間の契約書が添付されていません。");
                    }
                    break;
                // 長期アルバイト更新期間　画像１
                case "BTNExContractExpirationPartTimeJobPicture1":
                    if (this.BTNExContractExpirationPartTimeJobPicture1.Tag is not null) {

                    } else {
                        MessageBox.Show("長期アルバイト更新期間の契約書が添付されていません。");
                    }
                    break;
                // 長期アルバイト更新期間　画像２
                case "BTNExContractExpirationPartTimeJobPicture2":
                    if (this.BTNExContractExpirationPartTimeJobPicture2.Tag is not null) {

                    } else {
                        MessageBox.Show("長期アルバイト更新期間の契約書が添付されていません。");
                    }
                    break;
                // 組合長期雇用期間　画像１
                case "BTNExContractExpirationLongJobPicture1":
                    if (this.BTNExContractExpirationLongJobPicture1.Tag is not null) {

                    } else {
                        MessageBox.Show("組合長期雇用期間の契約書が添付されていません。");
                    }
                    break;
                // 組合長期雇用期間　画像２
                case "BTNExContractExpirationLongJobPicture2":
                    if (this.BTNExContractExpirationLongJobPicture2.Tag is not null) {

                    } else {
                        MessageBox.Show("組合長期雇用期間の契約書が添付されていません。");
                    }
                    break;
                // 組合短期雇用期間　画像１
                case "BTNExContractExpirationShortJobPicture1":
                    if (this.BTNExContractExpirationShortJobPicture1.Tag is not null) {

                    } else {
                        MessageBox.Show("組合短期雇用期間の契約書が添付されていません。");
                    }
                    break;
                // 組合短期雇用期間　画像２
                case "BTNExContractExpirationShortJobPicture2":
                    if (this.BTNExContractExpirationShortJobPicture2.Tag is not null) {

                    } else {
                        MessageBox.Show("組合短期雇用期間の契約書が添付されていません。");
                    }
                    break;
                // 誓約書期間　画像１
                case "BTNExWrittenPledgePicture1":
                    if (this.BTNExWrittenPledgePicture1.Tag is not null) {

                    } else {
                        MessageBox.Show("誓約書が添付されていません。");
                    }
                    break;
                // 誓約書期間　画像２
                case "BTNExWrittenPledgePicture2":
                    if (this.BTNExWrittenPledgePicture2.Tag is not null) {

                    } else {
                        MessageBox.Show("誓約書が添付されていません。");
                    }
                    break;
                // 失墜行為書類期間　画像１
                case "BTNExLossWrittenPledgePicture1":
                    if (this.BTNExLossWrittenPledgePicture1.Tag is not null) {

                    } else {
                        MessageBox.Show("失墜行為確認書が添付されていません。");
                    }
                    break;
                // 失墜行為書類期間　画像２
                case "BTNExLossWrittenPledgePicture2":
                    if (this.BTNExLossWrittenPledgePicture2.Tag is not null) {

                    } else {
                        MessageBox.Show("失墜行為確認書が添付されていません。");
                    }
                    break;
                // 契約満了通知(事前通知書)　画像１
                case "BTNExContractExpirationNoticePicture1":
                    if (this.BTNExContractExpirationNoticePicture1.Tag is not null) {

                    } else {
                        MessageBox.Show("契約満了通知(事前通知書)が添付されていません。");
                    }
                    break;
                // 契約満了通知(事前通知書)　画像２
                case "BTNExContractExpirationNoticePicture2":
                    if (this.BTNExContractExpirationNoticePicture2.Tag is not null) {

                    } else {
                        MessageBox.Show("契約満了通知(事前通知書)が添付されていません。");
                    }
                    break;
            }
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
        /// Control初期化
        /// </summary>
        private void InitializeControl() {
            // 組合№
            this.labelEx33.Text = _staffMasterVo.UnionCode.ToString("###");
            // 氏名
            this.labelEx29.Text = _staffMasterVo.DisplayName;
            // 所属
            this.labelEx30.Text = _dictionaryBelongs[_staffMasterVo.Belongs];
            // 職種
            this.labelEx31.Text = _dictionaryOccupation[_staffMasterVo.Occupation];
            // 雇用形態
            this.labelEx32.Text = _dictionaryJobForm[_staffMasterVo.JobForm];
            // 契約期間
            this.NumericUpDownExContractExpirationPeriod.Value = 12;
            /*
             * 体験入社期間
             */
            this.DateTimePickerExExpirationStartDate.SetClear();
            this.DateTimePickerExExpirationEndDate.SetClear();
            this.TextBoxExExpirationMemo.Text = string.Empty;
            /*
             * 長期アルバイト更新期間
             */
            this.DTPExContractExpirationPartTimeJobStartDate.SetClear();
            this.DTPExContractExpirationPartTimeJobEndDate.SetClear();
            this.DTPExContractExpirationPartTimeJobMemo.Text = string.Empty;
            this.DTPExContractExpirationPartTimeJobStartDate1.SetClear();
            this.DTPExContractExContractExpirationPartTimeJobEndDate1.SetClear();
            this.DTPExContractExpirationPartTimeJobMemo1.Text = string.Empty;
            this.DTPExContractExpirationPartTimeJobStartDate2.SetClear();
            this.DTPExContractExpirationPartTimeJobEndDate2.SetClear();
            this.DTPExContractExpirationPartTimeJobMemo2.Text = string.Empty;
            /*
             * 組合長期雇用期間
             */
            this.DTPExContractExpirationLongJobStartDate.SetClear();
            this.DTPExContractExpirationLongJobEndDate.SetClear();
            this.DTPExContractExpirationLongJobMemo.Text = string.Empty;
            this.DTPExContractExpirationLongJobStartDate1.SetClear();
            this.DTPExContractExpirationLongJobEndDate1.SetClear();
            this.DTPExContractExpirationLongJobMemo1.Text = string.Empty;
            this.DTPExContractExpirationLongJobStartDate2.SetClear();
            this.DTPExContractExpirationLongJobEndDate2.SetClear();
            this.DTPExContractExpirationLongJobMemo2.Text = string.Empty;
            /*
             * 組合短期雇用期間
             */
            this.DTPExContractExpirationShortJobStartDate.SetClear();
            this.DTPExContractExpirationShortJobEndDate.SetClear();
            this.DTPExContractExpirationShortJobMemo.Text = string.Empty;
            this.DTPExContractExpirationShortJobStartDate1.SetClear();
            this.DTPExContractExpirationShortJobEndDate1.SetClear();
            this.DTPExContractExpirationShortJobMemo1.Text = string.Empty;
            this.DTPExContractExpirationShortJobStartDate2.SetClear();
            this.DTPExContractExpirationShortJobEndDate2.SetClear();
            this.DTPExContractExpirationShortJobMemo2.Text = string.Empty;
            /*
             * 誓約書期間
             */
            this.DTPExWrittenPledgeStartDate.SetClear();
            this.DTPExWrittenPledgeEndDate.SetClear();
            this.DTPExWrittenPledgeMemo.Text = string.Empty;
            this.DTPExWrittenPledgeStartDate1.SetClear();
            this.DTPExWrittenPledgeEndDate1.SetClear();
            this.DTPExWrittenPledgeMemo1.Text = string.Empty;
            this.DTPExWrittenPledgeStartDate2.SetClear();
            this.DTPExWrittenPledgeEndDate2.SetClear();
            this.DTPExWrittenPledgeMemo2.Text = string.Empty;
            /*
             * 失墜行為書類期間
             */
            this.DTPExLossWrittenPledgeStartDate.SetClear();
            this.DTPExLossWrittenPledgeEndDate.SetClear();
            this.DTPExLossWrittenPledgeMemo.Text = string.Empty;
            this.DTPExLossWrittenPledgeStartDate1.SetClear();
            this.DTPExLossWrittenPledgeEndDate1.SetClear();
            this.DTPExLossWrittenPledgeMemo1.Text = string.Empty;
            this.DTPExLossWrittenPledgeStartDate2.SetClear();
            this.DTPExLossWrittenPledgeEndDate2.SetClear();
            this.DTPExLossWrittenPledgeMemo2.Text = string.Empty;
            /*
             * 契約満了通知(事前通知書)
             */
            this.DTPExContractExpirationNoticeStartDate.SetClear();
            this.DTPExContractExpirationNoticeEndDate.SetClear();
            this.DTPExContractExpirationNoticeMemo.Text = string.Empty;
            this.DTPExContractExpirationNoticeStartDate1.SetClear();
            this.DTPExContractExpirationNoticeEndDate1.SetClear();
            this.DTPExContractExpirationNoticeMemo1.Text = string.Empty;
            this.DTPExContractExpirationNoticeStartDate2.SetClear();
            this.DTPExContractExpirationNoticeEndDate2.SetClear();
            this.DTPExContractExpirationNoticeMemo2.Text = string.Empty;
        }

        /// <summary>
        /// EmploymentAgreementVoに値を代入
        /// </summary>
        /// <returns></returns>
        private EmploymentAgreementVo SetEmploymentAgreementVo() {
            EmploymentAgreementVo employmentAgreementVo = new();
            employmentAgreementVo.StaffCode = _staffMasterVo.StaffCode;
            employmentAgreementVo.ContractExpirationPeriod = (int)NumericUpDownExContractExpirationPeriod.Value;
            employmentAgreementVo.ExperienceFlag = DateTimePickerExExpirationStartDate.CustomFormat != " " ? true : false;
            employmentAgreementVo.ExperienceStartDate = DateTimePickerExExpirationStartDate.GetValue();
            employmentAgreementVo.ExperienceEndDate = DateTimePickerExExpirationEndDate.GetValue();
            employmentAgreementVo.ExperienceMemo = TextBoxExExpirationMemo.Text;
            employmentAgreementVo.ExperiencePicture = (byte[])new ImageConverter().ConvertTo(PictureBoxEx1.Image, typeof(byte[]));
            employmentAgreementVo.ListContractExpirationPartTimeJobVo = null;
            employmentAgreementVo.ListContractExpirationLongJobVo = null;
            employmentAgreementVo.ListContractExpirationShortJobVo = null;
            employmentAgreementVo.ListWrittenPledgeVo = null;
            employmentAgreementVo.ListLossWrittenPledgeVo = null;
            employmentAgreementVo.ListContractExpirationNoticeVo = null;
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
        /// Controlに出力
        /// </summary>
        /// <param name="employmentAgreementVo"></param>
        private void PutControl(EmploymentAgreementVo employmentAgreementVo) {
            if (employmentAgreementVo is not null) {
                // 契約期間
                this.NumericUpDownExContractExpirationPeriod.Value = employmentAgreementVo.ContractExpirationPeriod;
                // 体験入社開始日
                this.DateTimePickerExExpirationStartDate.SetValue(employmentAgreementVo.ExperienceStartDate);
                // 体験入社終了日
                this.DateTimePickerExExpirationEndDate.SetValue(employmentAgreementVo.ExperienceEndDate);
                // 体験入社メモ
                this.TextBoxExExpirationMemo.Text = employmentAgreementVo.ExperienceMemo;
                // 画像を退避(後の処理を楽にするためにボタンのTagに退避させておく)
                this.BTNExExpirationPicture.Tag = employmentAgreementVo.ExperiencePicture;
                // 長期アルバイト更新期間
                if (employmentAgreementVo.ListContractExpirationPartTimeJobVo is not null)
                    this.PutContractExpirationPartTimeJob(employmentAgreementVo.ListContractExpirationPartTimeJobVo);
                // 組合長期雇用期間
                if (employmentAgreementVo.ListContractExpirationLongJobVo is not null)
                    this.PutContractExpirationLongJob(employmentAgreementVo.ListContractExpirationLongJobVo);
                // 組合短期雇用期間
                if (employmentAgreementVo.ListContractExpirationShortJobVo is not null)
                    this.PutContractExpirationShortJob(employmentAgreementVo.ListContractExpirationShortJobVo);
                // 誓約書期間
                if (employmentAgreementVo.ListWrittenPledgeVo is not null)
                    this.PutWrittenPledge(employmentAgreementVo.ListWrittenPledgeVo);
                // 失墜行為書類期間
                if (employmentAgreementVo.ListLossWrittenPledgeVo is not null)
                    this.PutLossWrittenPledge(employmentAgreementVo.ListLossWrittenPledgeVo);
                // 契約満了通知(事前通知書)
                if (employmentAgreementVo.ListContractExpirationNoticeVo is not null)
                    this.PutContractExpirationNotice(employmentAgreementVo.ListContractExpirationNoticeVo);
            }
        }

        /// <summary>
        /// 長期アルバイト更新期間
        /// </summary>
        /// <param name="listContractExpirationPartTimeJobVo"></param>
        private void PutContractExpirationPartTimeJob(List<ContractExpirationPartTimeJobVo> listContractExpirationPartTimeJobVo) {
            Dictionary<int, DateTimePickerEx> _dicStartDate = new() { { 0, DTPExContractExpirationPartTimeJobStartDate1 }, { 1, DTPExContractExpirationPartTimeJobStartDate2 } };
            Dictionary<int, DateTimePickerEx> _dicEndDate = new() { { 0, DTPExContractExContractExpirationPartTimeJobEndDate1 }, { 1, DTPExContractExpirationPartTimeJobEndDate2 } };
            Dictionary<int, TextBoxEx> _dicMemo = new() { { 0, DTPExContractExpirationPartTimeJobMemo1 }, { 1, DTPExContractExpirationPartTimeJobMemo2 } };
            int count = 0;
            foreach (ContractExpirationPartTimeJobVo contractExpirationPartTimeJobVo in listContractExpirationPartTimeJobVo) {
                _dicStartDate[count].SetValueJp(contractExpirationPartTimeJobVo.ContractExpirationStartDate);
                _dicEndDate[count].SetValueJp(contractExpirationPartTimeJobVo.ContractExpirationEndDate);
                _dicMemo[count].Text = contractExpirationPartTimeJobVo.Memo;
                count++;
                if (count > 1)
                    break;
            }
        }

        /// <summary>
        /// 組合長期雇用期間
        /// </summary>
        /// <param name="listContractExpirationLongJobVo"></param>
        private void PutContractExpirationLongJob(List<ContractExpirationLongJobVo> listContractExpirationLongJobVo) {
            Dictionary<int, DateTimePickerEx> _dicStartDate = new() { { 0, DTPExContractExpirationLongJobStartDate1 }, { 1, DTPExContractExpirationLongJobStartDate2 } };
            Dictionary<int, DateTimePickerEx> _dicEndDate = new() { { 0, DTPExContractExpirationLongJobEndDate1 }, { 1, DTPExContractExpirationLongJobEndDate2 } };
            Dictionary<int, TextBoxEx> _dicMemo = new() { { 0, DTPExContractExpirationLongJobMemo1 }, { 1, DTPExContractExpirationLongJobMemo2 } };
            int count = 0;
            foreach (ContractExpirationLongJobVo contractExpirationLongJobVo in listContractExpirationLongJobVo) {
                _dicStartDate[count].SetValueJp(contractExpirationLongJobVo.ContractExpirationStartDate);
                _dicEndDate[count].SetValueJp(contractExpirationLongJobVo.ContractExpirationEndDate);
                _dicMemo[count].Text = contractExpirationLongJobVo.Memo;
                count++;
                if (count > 1)
                    break;
            }
        }

        /// <summary>
        /// 組合短期雇用期間
        /// </summary>
        /// <param name="listContractExpirationShortJobVo"></param>
        private void PutContractExpirationShortJob(List<ContractExpirationShortJobVo> listContractExpirationShortJobVo) {
            Dictionary<int, DateTimePickerEx> _dicStartDate = new() { { 0, DTPExContractExpirationShortJobStartDate1 }, { 1, DTPExContractExpirationShortJobStartDate2 } };
            Dictionary<int, DateTimePickerEx> _dicEndDate = new() { { 0, DTPExContractExpirationShortJobEndDate1 }, { 1, DTPExContractExpirationShortJobEndDate2 } };
            Dictionary<int, TextBoxEx> _dicMemo = new() { { 0, DTPExContractExpirationLongJobMemo1 }, { 1, DTPExContractExpirationLongJobMemo2 } };
            int count = 0;
            foreach (ContractExpirationShortJobVo contractExpirationShortJobVo in listContractExpirationShortJobVo) {
                _dicStartDate[count].SetValueJp(contractExpirationShortJobVo.ContractExpirationStartDate);
                _dicEndDate[count].SetValueJp(contractExpirationShortJobVo.ContractExpirationEndDate);
                _dicMemo[count].Text = contractExpirationShortJobVo.Memo;
                count++;
                if (count > 1)
                    break;
            }
        }

        /// <summary>
        /// 誓約書期間
        /// </summary>
        /// <param name="listWrittenPledgeVo"></param>
        private void PutWrittenPledge(List<WrittenPledgeVo> listWrittenPledgeVo) {
            Dictionary<int, DateTimePickerEx> _dicStartDate = new() { { 0, DTPExWrittenPledgeStartDate1 }, { 1, DTPExWrittenPledgeStartDate2 } };
            Dictionary<int, DateTimePickerEx> _dicEndDate = new() { { 0, DTPExWrittenPledgeEndDate1 }, { 1, DTPExWrittenPledgeEndDate2 } };
            Dictionary<int, TextBoxEx> _dicMemo = new() { { 0, DTPExWrittenPledgeMemo1 }, { 1, DTPExWrittenPledgeMemo2 } };
            int count = 0;
            foreach (WrittenPledgeVo writtenPledgeVo in listWrittenPledgeVo) {
                _dicStartDate[count].SetValueJp(writtenPledgeVo.ContractExpirationStartDate);
                _dicEndDate[count].SetValueJp(writtenPledgeVo.ContractExpirationEndDate);
                _dicMemo[count].Text = writtenPledgeVo.Memo;
                count++;
                if (count > 1)
                    break;
            }
        }

        /// <summary>
        /// 失墜行為書類期間
        /// </summary>
        /// <param name="listLossWrittenPledgeVo"></param>
        private void PutLossWrittenPledge(List<LossWrittenPledgeVo> listLossWrittenPledgeVo) {
            Dictionary<int, DateTimePickerEx> _dicStartDate = new() { { 0, DTPExLossWrittenPledgeStartDate1 }, { 1, DTPExLossWrittenPledgeStartDate2 } };
            Dictionary<int, DateTimePickerEx> _dicEndDate = new() { { 0, DTPExLossWrittenPledgeEndDate1 }, { 1, DTPExLossWrittenPledgeEndDate2 } };
            Dictionary<int, TextBoxEx> _dicMemo = new() { { 0, DTPExLossWrittenPledgeMemo1 }, { 1, DTPExLossWrittenPledgeMemo2 } };
            int count = 0;
            foreach (LossWrittenPledgeVo lossWrittenPledgeVo in listLossWrittenPledgeVo) {
                _dicStartDate[count].SetValueJp(lossWrittenPledgeVo.ContractExpirationStartDate);
                _dicEndDate[count].SetValueJp(lossWrittenPledgeVo.ContractExpirationEndDate);
                _dicMemo[count].Text = lossWrittenPledgeVo.Memo;
                count++;
                if (count > 1)
                    break;
            }
        }

        /// <summary>
        /// 契約満了通知(事前通知書)
        /// </summary>
        /// <param name="listContractExpirationNoticeVo"></param>
        private void PutContractExpirationNotice(List<ContractExpirationNoticeVo> listContractExpirationNoticeVo) {
            Dictionary<int, DateTimePickerEx> _dicStartDate = new() { { 0, DTPExContractExpirationNoticeStartDate1 }, { 1, DTPExContractExpirationNoticeStartDate2 } };
            Dictionary<int, DateTimePickerEx> _dicEndDate = new() { { 0, DTPExContractExpirationNoticeEndDate1 }, { 1, DTPExContractExpirationNoticeEndDate2 } };
            Dictionary<int, TextBoxEx> _dicMemo = new() { { 0, DTPExContractExpirationNoticeMemo1 }, { 1, DTPExContractExpirationNoticeMemo2 } };
            int count = 0;
            foreach (ContractExpirationNoticeVo contractExpirationNoticeVo in listContractExpirationNoticeVo) {
                _dicStartDate[count].SetValueJp(contractExpirationNoticeVo.ContractExpirationStartDate);
                _dicEndDate[count].SetValueJp(contractExpirationNoticeVo.ContractExpirationEndDate);
                _dicMemo[count].Text = contractExpirationNoticeVo.Memo;
                count++;
                if (count > 1)
                    break;
            }
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
