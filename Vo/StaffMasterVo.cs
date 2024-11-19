/*
 * 2023-10-31
 */
namespace Vo {
    /*
     * DeepCopyで使用
     */
    [Serializable] // ←DeepCopyする場合には必要
    public class StaffMasterVo {
        private readonly DateTime _defaultDateTime = new DateTime(1900, 01, 01);

        private int _staffCode;
        private int _unionCode;
        private int _belongs;
        private bool _vehicleDispatchTarget;
        private int _jobForm;
        private int _occupation;
        private string _nameKana;
        private string _name;
        private string _displayName;
        private string _otherNameKana;
        private string _otherName;
        private string _gender;
        private DateTime _birthDate;
        private DateTime _employmentDate;
        private string _currentAddress;
        private string _beforeChangeAddress;
        private string _remarks;
        private string _telephoneNumber;
        private string _cellphoneNumber;
        private byte[] _picture;
        private string _bloodType;
        private DateTime _selectionDate;
        private DateTime _notSelectionDate;
        private string _notSelectionReason;
        private LicenseMasterVo _licenseMasterVo;
        private List<StaffHistoryVo> _listStaffHistoryVo;
        private List<StaffExperienceVo> _listStaffExperienceVo;
        private bool _contractFlag;
        private DateTime _contractDate;
        private bool _retirementFlag;
        private DateTime _retirementDate;
        private string _retirementNote;
        private DateTime _deathDate;
        private string _deathNote;
        private bool _legalTwelveItemFlag;
        private bool _toukanpoFlag;
        private List<StaffFamilyVo> _listStaffFamilyVo;
        private string _urgentTelephoneNumber;
        private string _urgentTelephoneMethod;
        private DateTime _healthInsuranceDate;
        private string _healthInsuranceNumber;
        private string _healthInsuranceNote;
        private DateTime _welfarePensionDate;
        private string _welfarePensionNumber;
        private string _welfarePensionNote;
        private DateTime _employmentInsuranceDate;
        private string _employmentInsuranceNumber;
        private string _employmentInsuranceNote;
        private DateTime _workerAccidentInsuranceDate;
        private string _workerAccidentInsuranceNumber;
        private string _workerAccidentInsuranceNote;
        private List<StaffMedicalExaminationVo> _listStaffMedicalExaminationVo;
        private List<StaffCarViolateVo> _listStaffCarViolateVo;
        private List<StaffEducateVo> _listStaffEducateVo;
        private List<StaffProperVo> _listStaffProperVo;
        private List<StaffPunishmentVo> _listStaffPunishmentVo;
        private string _insertPcName;
        private DateTime _insertYmdHms;
        private string _updatePcName;
        private DateTime _updateYmdHms;
        private string _deletePcName;
        private DateTime _deleteYmdHms;
        private bool _deleteFlag;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public StaffMasterVo() {
            _staffCode = 0;
            _unionCode = 0;
            _belongs = 0;
            _vehicleDispatchTarget = false;
            _jobForm = 0;
            _occupation = 0;
            _nameKana = string.Empty;
            _name = string.Empty;
            _otherNameKana = string.Empty;
            _otherName = string.Empty;
            _displayName = string.Empty;
            _gender = string.Empty;
            _birthDate = _defaultDateTime;
            _employmentDate = _defaultDateTime;
            _currentAddress = string.Empty;
            _beforeChangeAddress = string.Empty;
            _remarks = string.Empty;
            _telephoneNumber = string.Empty;
            _cellphoneNumber = string.Empty;
            _picture = Array.Empty<byte>();
            _bloodType = string.Empty;
            _selectionDate = _defaultDateTime;
            _notSelectionDate = _defaultDateTime;
            _notSelectionReason = string.Empty;
            _licenseMasterVo = new LicenseMasterVo();
            _listStaffHistoryVo = new List<StaffHistoryVo>();
            _listStaffExperienceVo = new List<StaffExperienceVo>();
            _contractFlag = false;
            _contractDate = _defaultDateTime;
            _retirementFlag = false;
            _retirementDate = _defaultDateTime;
            _retirementNote = string.Empty;
            _deathDate = _defaultDateTime;
            _deathNote = string.Empty;
            _legalTwelveItemFlag = false;
            _toukanpoFlag = false;
            _listStaffFamilyVo = new List<StaffFamilyVo>();
            _urgentTelephoneNumber = string.Empty;
            _urgentTelephoneMethod = string.Empty;
            _healthInsuranceDate = _defaultDateTime;
            _healthInsuranceNumber = string.Empty;
            _healthInsuranceNote = string.Empty;
            _welfarePensionDate = _defaultDateTime;
            _welfarePensionNumber = string.Empty;
            _welfarePensionNote = string.Empty;
            _employmentInsuranceDate = _defaultDateTime;
            _employmentInsuranceNumber = string.Empty;
            _employmentInsuranceNote = string.Empty;
            _workerAccidentInsuranceDate = _defaultDateTime;
            _workerAccidentInsuranceNumber = string.Empty;
            _workerAccidentInsuranceNote = string.Empty;
            _listStaffMedicalExaminationVo = new List<StaffMedicalExaminationVo>();
            _listStaffCarViolateVo = new List<StaffCarViolateVo>();
            _listStaffEducateVo = new List<StaffEducateVo>();
            _listStaffProperVo = new List<StaffProperVo>();
            _listStaffPunishmentVo = new List<StaffPunishmentVo>();
            _insertPcName = string.Empty;
            _insertYmdHms = _defaultDateTime;
            _updatePcName = string.Empty;
            _updateYmdHms = _defaultDateTime;
            _deletePcName = string.Empty;
            _deleteYmdHms = _defaultDateTime;
            _deleteFlag = false;
        }

        /// <summary>
        /// 従業員コード
        /// </summary>
        public int StaffCode {
            get => _staffCode;
            set => _staffCode = value;
        }
        /// <summary>
        /// 組合コード
        /// </summary>
        public int UnionCode {
            get => _unionCode;
            set => _unionCode = value;
        }
        /// <summary>
        /// 所属
        /// 10:役員 11:社員 12:アルバイト 13:派遣 14:嘱託雇用契約社員 15:パートタイマー 20:新運転 21:自運労
        /// </summary>
        public int Belongs {
            get => _belongs;
            set => _belongs = value;
        }
        /// <summary>
        /// 配車の対象かどうか
        /// true:対象 false:非対象
        /// </summary>
        public bool VehicleDispatchTarget {
            get => _vehicleDispatchTarget;
            set => _vehicleDispatchTarget = value;
        }
        /// <summary>
        /// 雇用形態
        /// 10:長期雇用 11:手帳 99:指定なし
        /// </summary>
        public int JobForm {
            get => _jobForm;
            set => _jobForm = value;
        }
        /// <summary>
        /// 職種
        /// 10:運転手 11:作業員 20:事務職 99:指定なし
        /// </summary>
        public int Occupation {
            get => _occupation;
            set => _occupation = value;
        }
        /// <summary>
        /// 氏名カナ
        /// </summary>
        public string NameKana {
            get => _nameKana;
            set => _nameKana = value;
        }
        /// <summary>
        /// 氏名
        /// </summary>
        public string Name {
            get => _name;
            set => _name = value;
        }
        /// <summary>
        /// 健康診断用の表記
        /// </summary>
        public string OtherNameKana {
            get => _otherNameKana;
            set => _otherNameKana = value;
        }
        /// <summary>
        /// 健康診断用の表記
        /// </summary>
        public string OtherName {
            get => _otherName;
            set => _otherName = value;
        }
        /// <summary>
        /// 画面表示・配車表用氏名
        /// 全角６文字以内
        /// </summary>
        public string DisplayName {
            get => _displayName;
            set => _displayName = value;
        }
        /// <summary>
        /// 性別
        /// </summary>
        public string Gender {
            get => _gender;
            set => _gender = value;
        }
        /// <summary>
        /// 生年月日
        /// </summary>
        public DateTime BirthDate {
            get => _birthDate;
            set => _birthDate = value;
        }
        /// <summary>
        /// 雇用年月日
        /// アルバイト開始日又は長期雇用開始日
        /// </summary>
        public DateTime EmploymentDate {
            get => _employmentDate;
            set => _employmentDate = value;
        }
        /// <summary>
        /// 現住所
        /// </summary>
        public string CurrentAddress {
            get => _currentAddress;
            set => _currentAddress = value;
        }
        /// <summary>
        /// 変更前住所
        /// </summary>
        public string BeforeChangeAddress {
            get => _beforeChangeAddress;
            set => _beforeChangeAddress = value;
        }
        /// <summary>
        /// その他備考
        /// </summary>
        public string Remarks {
            get => _remarks;
            set => _remarks = value;
        }
        /// <summary>
        /// 電話番号
        /// </summary>
        public string TelephoneNumber {
            get => _telephoneNumber;
            set => _telephoneNumber = value;
        }
        /// <summary>
        /// 携帯番号
        /// </summary>
        public string CellphoneNumber {
            get => _cellphoneNumber;
            set => _cellphoneNumber = value;
        }
        /// <summary>
        /// 写真
        /// </summary>
        public byte[] Picture {
            get => _picture;
            set => _picture = value;
        }
        /// <summary>
        /// 血液型
        /// </summary>
        public string BloodType {
            get => _bloodType;
            set => _bloodType = value;
        }
        /// <summary>
        /// 運転手として選任された日
        /// </summary>
        public DateTime SelectionDate {
            get => _selectionDate;
            set => _selectionDate = value;
        }
        /// <summary>
        /// 運転手として選任されなっくなった日
        /// </summary>
        public DateTime NotSelectionDate {
            get => _notSelectionDate;
            set => _notSelectionDate = value;
        }
        /// <summary>
        /// 選任されなくなった理由
        /// </summary>
        public string NotSelectionReason {
            get => _notSelectionReason;
            set => _notSelectionReason = value;
        }
        /// <summary>
        /// 免許証番号
        /// </summary>
        public LicenseMasterVo LicenseMasterVo {
            get => _licenseMasterVo;
            set => _licenseMasterVo = value;
        }
        /// <summary>
        /// 職業履歴
        /// </summary>
        public List<StaffHistoryVo> ListHStaffHistoryVo {
            get => _listStaffHistoryVo;
            set => _listStaffHistoryVo = value;
        }
        /// <summary>
        /// 過去に運転経験のある自動車
        /// </summary>
        public List<StaffExperienceVo> ListHStaffExperienceVo {
            get => _listStaffExperienceVo;
            set => _listStaffExperienceVo = value;
        }
        /// <summary>
        /// 契約フラグ
        /// true:契約期間が１年ではない場合 false:契約期間が１年(通常)
        /// </summary>
        public bool ContractFlag {
            get => _contractFlag;
            set => _contractFlag = value;
        }
        /// <summary>
        /// 契約満了日
        /// </summary>
        public DateTime ContractDate {
            get => _contractDate;
            set => _contractDate = value;
        }
        /// <summary>
        /// 退職フラグ
        /// </summary>
        public bool RetirementFlag {
            get => _retirementFlag;
            set => _retirementFlag = value;
        }
        /// <summary>
        /// 退職日
        /// </summary>
        public DateTime RetirementDate {
            get => _retirementDate;
            set => _retirementDate = value;
        }
        /// <summary>
        /// 退職理由
        /// </summary>
        public string RetirementNote {
            get => _retirementNote;
            set => _retirementNote = value;
        }
        /// <summary>
        /// 死亡日
        /// </summary>
        public DateTime DeathDate {
            get => _deathDate;
            set => _deathDate = value;
        }
        /// <summary>
        /// 死亡理由
        /// </summary>
        public string DeathNote {
            get => _deathNote;
            set => _deathNote = value;
        }
        /// <summary>
        /// 法定１２項目の講習受講対象者
        /// true:受講対象者 false:受講未対象者
        /// </summary>
        public bool LegalTwelveItemFlag {
            get => _legalTwelveItemFlag;
            set => _legalTwelveItemFlag = value;
        }
        /// <summary>
        /// 東環保更新研修受講対象者フラグ
        /// true:受講対象者 false:受講未対象者
        /// </summary>
        public bool ToukanpoFlag {
            get => _toukanpoFlag;
            set => _toukanpoFlag = value;
        }
        /// <summary>
        /// 家族構成
        /// </summary>
        public List<StaffFamilyVo> ListHStaffFamilyVo {
            get => _listStaffFamilyVo;
            set => _listStaffFamilyVo = value;
        }
        /// <summary>
        /// 緊急連絡先
        /// </summary>
        public string UrgentTelephoneNumber {
            get => _urgentTelephoneNumber;
            set => _urgentTelephoneNumber = value;
        }
        /// <summary>
        /// 緊急連絡方法
        /// </summary>
        public string UrgentTelephoneMethod {
            get => _urgentTelephoneMethod;
            set => _urgentTelephoneMethod = value;
        }
        /// <summary>
        /// 健康保険
        /// </summary>
        public DateTime HealthInsuranceDate {
            get => _healthInsuranceDate;
            set => _healthInsuranceDate = value;
        }
        public string HealthInsuranceNumber {
            get => _healthInsuranceNumber;
            set => _healthInsuranceNumber = value;
        }
        public string HealthInsuranceNote {
            get => _healthInsuranceNote;
            set => _healthInsuranceNote = value;
        }
        /// <summary>
        /// 年金保険
        /// </summary>
        public DateTime WelfarePensionDate {
            get => _welfarePensionDate;
            set => _welfarePensionDate = value;
        }
        public string WelfarePensionNumber {
            get => _welfarePensionNumber;
            set => _welfarePensionNumber = value;
        }
        public string WelfarePensionNote {
            get => _welfarePensionNote;
            set => _welfarePensionNote = value;
        }
        /// <summary>
        /// 雇用保険
        /// </summary>
        public DateTime EmploymentInsuranceDate {
            get => _employmentInsuranceDate;
            set => _employmentInsuranceDate = value;
        }
        public string EmploymentInsuranceNumber {
            get => _employmentInsuranceNumber;
            set => _employmentInsuranceNumber = value;
        }
        public string EmploymentInsuranceNote {
            get => _employmentInsuranceNote;
            set => _employmentInsuranceNote = value;
        }
        /// <summary>
        /// 労災保険
        /// </summary>
        public DateTime WorkerAccidentInsuranceDate {
            get => _workerAccidentInsuranceDate;
            set => _workerAccidentInsuranceDate = value;
        }
        public string WorkerAccidentInsuranceNumber {
            get => _workerAccidentInsuranceNumber;
            set => _workerAccidentInsuranceNumber = value;
        }
        public string WorkerAccidentInsuranceNote {
            get => _workerAccidentInsuranceNote;
            set => _workerAccidentInsuranceNote = value;
        }
        /// <summary>
        /// 健康診断記録
        /// </summary>
        public List<StaffMedicalExaminationVo> ListHStaffMedicalExaminationVo {
            get => _listStaffMedicalExaminationVo;
            set => _listStaffMedicalExaminationVo = value;
        }
        /// <summary>
        /// 免許証違反記録
        /// </summary>
        public List<StaffCarViolateVo> ListHStaffCarViolateVo {
            get => _listStaffCarViolateVo;
            set => _listStaffCarViolateVo = value;
        }
        /// <summary>
        /// 教育指導記録
        /// </summary>
        public List<StaffEducateVo> ListHStaffEducateVo {
            get => _listStaffEducateVo;
            set => _listStaffEducateVo = value;
        }
        /// <summary>
        /// 適正診断記録
        /// </summary>
        public List<StaffProperVo> ListHStaffProperVo {
            get => _listStaffProperVo;
            set => _listStaffProperVo = value;
        }
        /// <summary>
        /// 賞罰・譴責記録
        /// </summary>
        public List<StaffPunishmentVo> ListHStaffPunishmentVo {
            get => _listStaffPunishmentVo;
            set => _listStaffPunishmentVo = value;
        }
        public string InsertPcName {
            get => _insertPcName;
            set => _insertPcName = value;
        }
        public DateTime InsertYmdHms {
            get => _insertYmdHms;
            set => _insertYmdHms = value;
        }
        public string UpdatePcName {
            get => _updatePcName;
            set => _updatePcName = value;
        }
        public DateTime UpdateYmdHms {
            get => _updateYmdHms;
            set => _updateYmdHms = value;
        }
        public string DeletePcName {
            get => _deletePcName;
            set => _deletePcName = value;
        }
        public DateTime DeleteYmdHms {
            get => _deleteYmdHms;
            set => _deleteYmdHms = value;
        }
        public bool DeleteFlag {
            get => _deleteFlag;
            set => _deleteFlag = value;
        }
    }
}
