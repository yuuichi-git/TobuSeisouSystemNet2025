/*
 * 2025-1-17
 */
using ControlEx;

using Dao;

using Vo;

namespace Staff {
    public partial class StaffDetail : Form {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly ErrorProvider _errorProvider = new();
        /*
         * Dao
         */
        private readonly BelongsMasterDao _belongsMasterDao;
        private readonly JobFormMasterDao _jobFormMasterDao;
        private readonly OccupationMasterDao _occupationMasterDao;

        private readonly StaffMasterDao _staffMasterDao;
        private readonly StaffHistoryDao _staffHistoryDao;
        private readonly StaffExperienceDao _staffExperienceDao;
        private readonly StaffFamilyDao _staffFamilyDao;
        private readonly StaffMedicalExaminationDao _staffMedicalExaminationDao;
        private readonly StaffCarViolateDao _staffCarViolateDao;
        private readonly StaffEducateDao _staffEducateDao;
        private readonly StaffProperDao _staffProperDao;
        private readonly StaffPunishmentDao _staffPunishmentDao;
        /*
         * Dictionary
         */
        private readonly Dictionary<int, string> _dictionaryBelongsIS = new();          // コード基準
        private readonly Dictionary<int, string> _dictionaryOccupationIS = new();
        private readonly Dictionary<int, string> _dictionaryJobFormIS = new();
        private readonly Dictionary<string, int> _dictionaryBelongsSI = new();          // 名前基準
        private readonly Dictionary<string, int> _dictionaryOccupationSI = new();
        private readonly Dictionary<string, int> _dictionaryJobFormSI = new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="staffCode"></param>
        public StaffDetail(ConnectionVo connectionVo, int staffCode) {
            /*
             * Dao
             */
            _belongsMasterDao = new(connectionVo);
            _jobFormMasterDao = new(connectionVo);
            _occupationMasterDao = new(connectionVo);

            _staffMasterDao = new(connectionVo);
            _staffHistoryDao = new(connectionVo);
            _staffExperienceDao = new(connectionVo);
            _staffFamilyDao = new(connectionVo);
            _staffMedicalExaminationDao = new(connectionVo);
            _staffCarViolateDao = new(connectionVo);
            _staffEducateDao = new(connectionVo);
            _staffProperDao = new(connectionVo);
            _staffPunishmentDao = new(connectionVo);
            /*
             * Dictionary
             */
            foreach (BelongsMasterVo belongsMasterVo in _belongsMasterDao.SelectAllBelongsMaster()) {
                _dictionaryBelongsIS.Add(belongsMasterVo.Code, belongsMasterVo.Name);
                _dictionaryBelongsSI.Add(belongsMasterVo.Name, belongsMasterVo.Code);
            }
            foreach (OccupationMasterVo occupationMasterVo in _occupationMasterDao.SelectAllOccupationMaster()) {
                _dictionaryOccupationIS.Add(occupationMasterVo.Code, occupationMasterVo.Name);
                _dictionaryOccupationSI.Add(occupationMasterVo.Name, occupationMasterVo.Code);
            }
            foreach (JobFormMasterVo jobFormMasterVo in _jobFormMasterDao.SelectAllJobFormMaster()) {
                _dictionaryJobFormIS.Add(jobFormMasterVo.Code, jobFormMasterVo.Name);
                _dictionaryJobFormSI.Add(jobFormMasterVo.Name, jobFormMasterVo.Code);
            }
            // アイコンを常に点滅に設定する
            _errorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
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

            this.InitializeControls();
            try {
                this.SetControl(_staffMasterDao.SelectOneStaffMaster(staffCode));
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
            /*
             * Eventを登録する
             */
            MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// 
        /// </summary>
        public void InitializeControls() {
            CheckBoxExTargetFlag.Checked = false;                                       // 配車する対象者
            CheckBoxExLegalTwelveItemFlag.Checked = false;                              // 法定１２項目受講対象者
            CheckBoxExToukanpoFlag.Checked = false;                                     // 東環保研修受講対象者
            foreach (RadioButtonEx radioButtonEx in GroupBoxExBelongs.Controls)         // 所属
                radioButtonEx.Checked = false;
            foreach (RadioButtonEx radioButtonEx in GroupBoxExJobForm.Controls)         // 雇用形態
                radioButtonEx.Checked = false;
            foreach (RadioButtonEx radioButtonEx in GroupBoxExOccupation.Controls)      // 職種
                radioButtonEx.Checked = false;
            /*
             * 個人情報
             */
            LabelExStaffCode.Text = string.Empty;
            TextBoxExUnionCode.Text = string.Empty;
            TextBoxExNameKana.Text = string.Empty;
            TextBoxExOtherNameKana.Text = string.Empty;
            TextBoxExName.Text = string.Empty;
            TextBoxExOtherName.Text = string.Empty;
            TextBoxExDisplayName.Text = string.Empty;
            DateTimeExBirthDate.SetClear();
            ComboBoxExGender.SelectedIndex = -1;
            ComboBoxExBloodType.SelectedIndex = -1;
            DateTimeExEmploymentDate.SetClear();
            CheckBoxExContractFlag.Checked = false;
            DateTimePickerExContractDate.Enabled = false;
            DateTimePickerExContractDate.SetClear();
            TextBoxExCurrentAddress.Text = string.Empty;
            TextBoxExRemarks.Text = string.Empty;
            TextBoxExTelephoneNumber.Text = string.Empty;
            TextBoxExCellphoneNumber.Text = string.Empty;
            PictureBoxExStaff.Image = null;
            PictureBoxExStamp.Image = null;
            /*
             * GroupBoxExDrive
             * 運転に関する情報
             */
            DateTimeExSelectionDate.SetClear();
            DateTimeExNotSelectionDate.SetClear();
            TextBoxExNotSelectionReason.Text = string.Empty;
            TextBoxExLicenseNumber.Text = string.Empty;
            ComboBoxExLicenseCondition.Text = string.Empty;
            TextBoxExLicenseType.Text = string.Empty;
            DateTimeExLicenseTypeExpirationDate.SetClear();
            /*
             * GroupBoxExHistory
             * 職業履歴
             */
            DateTimeExHistoryDate.SetClear();
            TextBoxExCompanyName.Text = string.Empty;
            DateTimeExHistoryDate1.SetClear();
            TextBoxExCompanyName1.Text = string.Empty;
            DateTimeExHistoryDate2.SetClear();
            TextBoxExCompanyName2.Text = string.Empty;
            DateTimeExHistoryDate3.SetClear();
            TextBoxExCompanyName3.Text = string.Empty;
            /*
             * GroupBoxExExperience
             * 過去に運転経験のある自動車の種類・経験期間等
             */
            ComboBoxExExperienceKind.SelectedIndex = -1;
            TextBoxExExperienceLoad.Text = string.Empty;
            TextBoxExExperienceDuration.Text = string.Empty;
            TextBoxExExperienceNote.Text = string.Empty;
            ComboBoxExExperienceKind1.SelectedIndex = -1;
            TextBoxExExperienceLoad1.Text = string.Empty;
            TextBoxExExperienceDuration1.Text = string.Empty;
            TextBoxExExperienceNote1.Text = string.Empty;
            ComboBoxExExperienceKind2.SelectedIndex = -1;
            TextBoxExExperienceLoad2.Text = string.Empty;
            TextBoxExExperienceDuration2.Text = string.Empty;
            TextBoxExExperienceNote2.Text = string.Empty;
            ComboBoxExExperienceKind3.SelectedIndex = -1;
            TextBoxExExperienceLoad3.Text = string.Empty;
            TextBoxExExperienceDuration3.Text = string.Empty;
            TextBoxExExperienceNote3.Text = string.Empty;
            /*
             * GroupBoxExRetirement
             * 解雇・退職の日付と理由
             */
            CheckBoxExRetirementFlag.Checked = false;
            DateTimeExRetirementDate.SetClear();
            TextBoxExRetirementNote.Text = string.Empty;
            DateTimeExDeathDate.SetClear();
            TextBoxExDeathNote.Text = string.Empty;

            /*
             * GroupBoxExFamily
             * 家族構成
             */
            TextBoxExFamilyName.Text = string.Empty;
            DateTimeExFamilyBirthDate.SetClear();
            ComboBoxExFamilyRelationship.SelectedIndex = -1;
            TextBoxExFamilyName1.Text = string.Empty;
            DateTimeExFamilyBirthDate1.SetClear();
            ComboBoxExFamilyRelationship1.SelectedIndex = -1;
            TextBoxExFamilyName2.Text = string.Empty;
            DateTimeExFamilyBirthDate2.SetClear();
            ComboBoxExFamilyRelationship2.SelectedIndex = -1;
            TextBoxExFamilyName3.Text = string.Empty;
            DateTimeExFamilyBirthDate3.SetClear();
            ComboBoxExFamilyRelationship3.SelectedIndex = -1;
            TextBoxExFamilyName4.Text = string.Empty;
            DateTimeExFamilyBirthDate4.SetClear();
            ComboBoxExFamilyRelationship4.SelectedIndex = -1;
            TextBoxExUrgentTelephoneNumber.Text = string.Empty;
            TextBoxExUrgentTelephoneMethod.Text = string.Empty;
            /*
             * GroupBoxExInsurance
             * 保険関係
             */
            DateTimeExHealthInsuranceDate.SetClear();
            ComboBoxExHealthInsuranceNumber.SelectedIndex = -1;
            TextBoxExHealthInsuranceNote.Text = string.Empty;
            DateTimeExWelfarePensionDate.SetClear();
            ComboBoxExWelfarePensionNumber.SelectedIndex = -1;
            TextBoxExWelfarePensionNote.Text = string.Empty;
            DateTimeExEmploymentInsuranceDate.SetClear();
            ComboBoxExEmploymentInsuranceNumber.Text = string.Empty;
            TextBoxExEmploymentInsuranceNote.Text = string.Empty;
            DateTimeExWorkerAccidentInsuranceDate.SetClear();
            ComboBoxExWorkerAccidentInsuranceNumber.SelectedIndex = -1;
            TextBoxExWorkerAccidentInsuranceNote.Text = string.Empty;
            /*
             * GroupBoxExMedicalExamination
             * 健康状態(健康診断等の実施結果による特記すべき事項)　※運転の可否に十分に留意すること
             */
            DateTimeExMedicalExaminationDate.SetClear();
            ComboBoxExMedicalInstitutionName.SelectedIndex = -1;
            TextBoxExMedicalExaminationNote.Text = string.Empty;
            DateTimeExMedicalExaminationDate1.SetClear();
            ComboBoxExMedicalInstitutionName1.SelectedIndex = -1;
            TextBoxExMedicalExaminationNote1.Text = string.Empty;
            DateTimeExMedicalExaminationDate2.SetClear();
            ComboBoxExMedicalInstitutionName2.SelectedIndex = -1;
            TextBoxExMedicalExaminationNote2.Text = string.Empty;
            DateTimeExMedicalExaminationDate3.SetClear();
            ComboBoxExMedicalInstitutionName3.SelectedIndex = -1;
            TextBoxExMedicalExaminationNote3.Text = string.Empty;
            /*
             * GroupBoxExCarViolate
             * 業務上の交通違反歴
             */
            DateTimeExCarViolateDate.SetClear();
            ComboBoxExCarViolateContent.SelectedIndex = -1;
            TextBoxExCarViolatePlace.Text = string.Empty;
            DateTimeExCarViolateDate1.SetClear();
            ComboBoxExCarViolateContent1.SelectedIndex = -1;
            TextBoxExCarViolatePlace1.Text = string.Empty;
            DateTimeExCarViolateDate2.SetClear();
            ComboBoxExCarViolateContent2.SelectedIndex = -1;
            TextBoxExCarViolatePlace2.Text = string.Empty;
            DateTimeExCarViolateDate3.SetClear();
            ComboBoxExCarViolateContent3.SelectedIndex = -1;
            TextBoxExCarViolatePlace3.Text = string.Empty;
            /*
             * GroupBoxEducate
             * 社内教育の実施記録
             */
            DateTimeExEducateDate.SetClear();
            ComboBoxExEducateName.SelectedIndex = -1;
            DateTimeExEducateDate1.SetClear();
            ComboBoxExEducateName1.SelectedIndex = -1;
            DateTimeExEducateDate2.SetClear();
            ComboBoxExEducateName2.SelectedIndex = -1;
            DateTimeExEducateDate3.SetClear();
            ComboBoxExEducateName3.SelectedIndex = -1;
            /*
             * GroupBoxProper
             * 適正診断(NASVA他)
             */
            DateTimeExProperDate.SetClear();
            ComboBoxExProperKind.SelectedIndex = -1;
            TextBoxExProperNote.Text = string.Empty;
            DateTimeExProperDate1.SetClear();
            ComboBoxExProperKind1.SelectedIndex = -1;
            TextBoxExProperNote1.Text = string.Empty;
            DateTimeExProperDate2.SetClear();
            ComboBoxExProperKind2.SelectedIndex = -1;
            TextBoxExProperNote2.Text = string.Empty;
            DateTimeExProperDate3.SetClear();
            ComboBoxExProperKind3.SelectedIndex = -1;
            TextBoxExProperNote3.Text = string.Empty;
            /*
             * GroupBoxExPunishment
             * 賞罰・譴責
             */
            DateTimeExPunishmentDate.SetClear();
            ComboBoxExPunishmentNote.Text = string.Empty;
            DateTimeExPunishmentDate1.SetClear();
            ComboBoxExPunishmentNote1.Text = string.Empty;
            DateTimeExPunishmentDate2.SetClear();
            ComboBoxExPunishmentNote2.Text = string.Empty;
            DateTimeExPunishmentDate3.SetClear();
            ComboBoxExPunishmentNote3.Text = string.Empty;

            StatusStripEx1.ToolStripStatusLabelDetail.Text = "Initialize Success";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private StaffMasterVo SetVo() {
            StaffMasterVo staffMasterVo = new();
            staffMasterVo.VehicleDispatchTarget = CheckBoxExTargetFlag.Checked;                                                         // 配車する対象者
            staffMasterVo.LegalTwelveItemFlag = CheckBoxExLegalTwelveItemFlag.Checked;                                                  // 法定１２項目受講対象者
            staffMasterVo.ToukanpoFlag = CheckBoxExToukanpoFlag.Checked;                                                                // 東環保研修受講対象者
            foreach (RadioButtonEx radioButtonExBelongs in GroupBoxExBelongs.Controls) {                                                // 所属
                if (radioButtonExBelongs.Checked)
                    staffMasterVo.Belongs = _dictionaryBelongsSI[radioButtonExBelongs.Text];
            }
            foreach (RadioButtonEx radioButtonExJobForm in GroupBoxExJobForm.Controls) {                                                // 雇用形態
                if (radioButtonExJobForm.Checked)
                    staffMasterVo.JobForm = _dictionaryJobFormSI[radioButtonExJobForm.Text];
            }
            foreach (RadioButtonEx radioButtonExOccupation in GroupBoxExOccupation.Controls) {                                          // 職種
                if (radioButtonExOccupation.Checked)
                    staffMasterVo.Occupation = _dictionaryOccupationSI[radioButtonExOccupation.Text];
            }
            /*
             * GroupBoxExPersonalData
             * 個人情報
             */
            int.TryParse(LabelExStaffCode.Text, out int _staffCode);                                                                    // StaffCode
            staffMasterVo.StaffCode = _staffCode;
            int.TryParse(TextBoxExUnionCode.Text, out int _unionCode);                                                                  // 組合コード
            staffMasterVo.UnionCode = _unionCode;
            staffMasterVo.NameKana = TextBoxExNameKana.Text;                                                                            // カナ
            staffMasterVo.OtherNameKana = TextBoxExOtherNameKana.Text;                                                                  // カナ(健康診断用)
            staffMasterVo.Name = TextBoxExName.Text;                                                                                    // 氏名
            staffMasterVo.OtherName = TextBoxExOtherName.Text;                                                                          // 氏名(健康診断用)
            staffMasterVo.DisplayName = TextBoxExDisplayName.Text;                                                                      // 略称名
            staffMasterVo.BirthDate = DateTimeExBirthDate.GetValue();                                                                   // 生年月日
            staffMasterVo.Gender = ComboBoxExGender.Text;                                                                               // 性別
            staffMasterVo.BloodType = ComboBoxExBloodType.Text;                                                                         // 血液型
            staffMasterVo.EmploymentDate = DateTimeExEmploymentDate.GetValue();                                                         // 雇用年月日
            staffMasterVo.ContractFlag = CheckBoxExContractFlag.Checked;                                                                // 契約満了日チェック
            staffMasterVo.ContractDate = CheckBoxExContractFlag.Checked ? DateTimePickerExContractDate.GetValue() : _defaultDateTime;   // 契約満了日
            staffMasterVo.CurrentAddress = TextBoxExCurrentAddress.Text;                                                                // 現住所
            staffMasterVo.Remarks = TextBoxExRemarks.Text;                                                                              // 備考
            staffMasterVo.TelephoneNumber = TextBoxExTelephoneNumber.Text;                                                              // 電話番号
            staffMasterVo.CellphoneNumber = TextBoxExCellphoneNumber.Text;                                                              // 携帯電話番号
            staffMasterVo.Picture = (byte[])new ImageConverter().ConvertTo(PictureBoxExStaff.Image, typeof(byte[]));                    // 写真
            staffMasterVo.StampPicture = (byte[])new ImageConverter().ConvertTo(PictureBoxExStamp.Image, typeof(byte[]));               // 写真
            /*
             * GroupBoxExDrive
             * 運転に関する情報
             */
            staffMasterVo.SelectionDate = DateTimeExSelectionDate.GetValue();                                                           // 運転手として選任された日
            staffMasterVo.NotSelectionDate = DateTimeExNotSelectionDate.GetValue();                                                     // 運転手として選任されなくなった日
            staffMasterVo.NotSelectionReason = TextBoxExNotSelectionReason.Text;                                                        // 選任されなくなった理由
            /*
             * GroupBoxExRetirement
             * 解雇・退職の日付と理由
             */
            staffMasterVo.RetirementFlag = CheckBoxExRetirementFlag.Checked;                                                            // 退職フラグ
            staffMasterVo.RetirementDate = DateTimeExRetirementDate.GetValue();                                                         // 退職日
            staffMasterVo.RetirementNote = TextBoxExRetirementNote.Text;                                                                // 退職理由
            staffMasterVo.DeathDate = DateTimeExDeathDate.GetValue();                                                                   // 死亡日
            staffMasterVo.DeathNote = TextBoxExDeathNote.Text;                                                                          // 死亡理由
            /*
             * GroupBoxExFamily
             * 家族構成
             */
            staffMasterVo.UrgentTelephoneNumber = TextBoxExUrgentTelephoneNumber.Text;                                                  // 緊急連絡先
            staffMasterVo.UrgentTelephoneMethod = TextBoxExUrgentTelephoneMethod.Text; // 緊急連絡方法
            /*
             * GroupBoxExInsurance
             * 保険関係
             */
            staffMasterVo.HealthInsuranceDate = DateTimeExHealthInsuranceDate.GetValue();                                               // 健康保険加入日
            staffMasterVo.HealthInsuranceNumber = ComboBoxExHealthInsuranceNumber.Text;                                                 // 健康保険番号
            staffMasterVo.HealthInsuranceNote = TextBoxExHealthInsuranceNote.Text;                                                      // 健康保険備考
            staffMasterVo.WelfarePensionDate = DateTimeExWelfarePensionDate.GetValue();                                                 // 年金保険加入日
            staffMasterVo.WelfarePensionNumber = ComboBoxExWelfarePensionNumber.Text;                                                   // 年金保険番号
            staffMasterVo.WelfarePensionNote = TextBoxExWelfarePensionNote.Text;                                                        // 年金保険備考
            staffMasterVo.EmploymentInsuranceDate = DateTimeExEmploymentInsuranceDate.GetValue();                                       // 雇用保険加入日
            staffMasterVo.EmploymentInsuranceNumber = ComboBoxExEmploymentInsuranceNumber.Text;                                         // 雇用保険番号
            staffMasterVo.EmploymentInsuranceNote = TextBoxExEmploymentInsuranceNote.Text;                                              // 雇用保険備考
            staffMasterVo.WorkerAccidentInsuranceDate = DateTimeExWorkerAccidentInsuranceDate.GetValue();                               // 労災保険加入日
            staffMasterVo.WorkerAccidentInsuranceNumber = ComboBoxExWorkerAccidentInsuranceNumber.Text;                                 // 労災保険番号
            staffMasterVo.WorkerAccidentInsuranceNote = TextBoxExWorkerAccidentInsuranceNote.Text;                                      // 労災保険備考

            return staffMasterVo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffMasterVo"></param>
        private void SetControl(StaffMasterVo staffMasterVo) {
            /*
             * Nullチェック
             */
            if (staffMasterVo is null)
                return;
            CheckBoxExTargetFlag.Checked = staffMasterVo.VehicleDispatchTarget;                                                         // 配車する対象者
            CheckBoxExLegalTwelveItemFlag.Checked = staffMasterVo.LegalTwelveItemFlag;                                                  // 法定１２項目受講対象者
            CheckBoxExToukanpoFlag.Checked = staffMasterVo.ToukanpoFlag;                                                                // 東環保研修受講対象者
            /*
             * GroupBoxExBelongs
             */
            foreach (RadioButtonEx radioButtonExBelongs in GroupBoxExBelongs.Controls)                                                  // 所属
                if (radioButtonExBelongs.Text == _dictionaryBelongsIS[staffMasterVo.Belongs])
                    radioButtonExBelongs.Checked = true;
            /*
             * GroupBoxExJobForm
             */
            foreach (RadioButtonEx radioButtonExJobForm in GroupBoxExJobForm.Controls)                                                  // 雇用形態
                if (radioButtonExJobForm.Text == _dictionaryJobFormIS[staffMasterVo.JobForm])
                    radioButtonExJobForm.Checked = true;
            /*
             * GroupBoxExOccupation
             */
            foreach (RadioButtonEx radioButtonExOccupation in GroupBoxExOccupation.Controls)                                            // 職種
                if (radioButtonExOccupation.Text == _dictionaryOccupationIS[staffMasterVo.Occupation])
                    radioButtonExOccupation.Checked = true;
            /*
             * GroupBoxExPersonalData
             * 個人情報
             */
            LabelExStaffCode.Text = staffMasterVo.StaffCode.ToString();                                                                 // 従事者コード
            TextBoxExUnionCode.Text = staffMasterVo.UnionCode.ToString();                                                               // 組合コード
            TextBoxExNameKana.Text = staffMasterVo.NameKana;
            TextBoxExOtherNameKana.Text = staffMasterVo.OtherNameKana;
            TextBoxExName.Text = staffMasterVo.Name;
            TextBoxExOtherName.Text = staffMasterVo.OtherName;
            TextBoxExDisplayName.Text = staffMasterVo.DisplayName;
            DateTimeExBirthDate.SetValueJp(staffMasterVo.BirthDate);
            ComboBoxExGender.Text = staffMasterVo.Gender;
            ComboBoxExBloodType.Text = staffMasterVo.BloodType;
            DateTimeExEmploymentDate.SetValueJp(staffMasterVo.EmploymentDate);
            CheckBoxExContractFlag.Checked = staffMasterVo.ContractFlag;
            DateTimePickerExContractDate.SetValue(staffMasterVo.ContractDate);
            TextBoxExCurrentAddress.Text = staffMasterVo.CurrentAddress;
            TextBoxExRemarks.Text = staffMasterVo.Remarks;
            TextBoxExTelephoneNumber.Text = staffMasterVo.TelephoneNumber;
            TextBoxExCellphoneNumber.Text = staffMasterVo.CellphoneNumber;
            PictureBoxExStaff.Image = staffMasterVo.Picture.Length != 0 ? (Image?)new ImageConverter().ConvertFrom(staffMasterVo.Picture) : null;
            PictureBoxExStamp.Image = staffMasterVo.StampPicture.Length != 0 ? (Image?)new ImageConverter().ConvertFrom(staffMasterVo.StampPicture) : null;
            /*
             * HGroupBoxExDrive
             * 運転に関する情報
             */
            DateTimeExSelectionDate.SetValueJp(staffMasterVo.SelectionDate);
            DateTimeExNotSelectionDate.SetValueJp(staffMasterVo.NotSelectionDate);
            TextBoxExNotSelectionReason.Text = staffMasterVo.NotSelectionReason;
            TextBoxExLicenseNumber.Text = staffMasterVo.LicenseMasterVo.LicenseNumber;
            ComboBoxExLicenseCondition.Text = staffMasterVo.LicenseMasterVo.LicenseCondition;
            string type = string.Empty;
            if (staffMasterVo.LicenseMasterVo.Large)
                type += "(大型) ";
            if (staffMasterVo.LicenseMasterVo.Medium)
                type += "(中型) ";
            if (staffMasterVo.LicenseMasterVo.QuasiMedium)
                type += "(準中型) ";
            if (staffMasterVo.LicenseMasterVo.Ordinary)
                type += "(普通)";
            TextBoxExLicenseType.Text = type; // 免許証の種類１
            DateTimeExLicenseTypeExpirationDate.SetValueJp(staffMasterVo.LicenseMasterVo.ExpirationDate);
            /*
             * GroupBoxExHistory 
             * 職業履歴
             */
            this.ScreenOutputGroupBoxExHistory(staffMasterVo.ListHStaffHistoryVo);
            /*
             * GroupBoxExExperience
             * 過去に運転経験のある自動車の種類・経験期間等
             */
            this.ScreenOutputGroupBoxExExperience(staffMasterVo.ListHStaffExperienceVo);
            /*
             * GroupBoxExRetirement
             * 解雇・退職の日付と理由
             */
            CheckBoxExRetirementFlag.Checked = staffMasterVo.RetirementFlag;
            DateTimeExRetirementDate.SetValueJp(staffMasterVo.RetirementDate);
            TextBoxExRetirementNote.Text = staffMasterVo.RetirementNote;
            DateTimeExDeathDate.SetValueJp(staffMasterVo.DeathDate);
            TextBoxExDeathNote.Text = staffMasterVo.DeathNote;
            /*
             * GroupBoxExFamily
             * 家族構成
             */
            this.ScreenOutputGroupBoxExFamily(staffMasterVo.ListHStaffFamilyVo);
            TextBoxExUrgentTelephoneNumber.Text = staffMasterVo.UrgentTelephoneNumber;
            TextBoxExUrgentTelephoneMethod.Text = staffMasterVo.UrgentTelephoneMethod;
            /*
             * HGroupBoxExInsurance
             * 保険関係
             */
            DateTimeExHealthInsuranceDate.SetValueJp(staffMasterVo.HealthInsuranceDate);
            ComboBoxExHealthInsuranceNumber.Text = staffMasterVo.HealthInsuranceNumber;
            TextBoxExHealthInsuranceNote.Text = staffMasterVo.HealthInsuranceNote;
            DateTimeExWelfarePensionDate.SetValueJp(staffMasterVo.WelfarePensionDate);
            ComboBoxExWelfarePensionNumber.Text = staffMasterVo.WelfarePensionNumber;
            TextBoxExWelfarePensionNote.Text = staffMasterVo.WelfarePensionNote;
            DateTimeExEmploymentInsuranceDate.SetValueJp(staffMasterVo.EmploymentInsuranceDate);
            ComboBoxExEmploymentInsuranceNumber.Text = staffMasterVo.EmploymentInsuranceNumber;
            TextBoxExEmploymentInsuranceNote.Text = staffMasterVo.EmploymentInsuranceNote;
            DateTimeExWorkerAccidentInsuranceDate.SetValueJp(staffMasterVo.WorkerAccidentInsuranceDate);
            ComboBoxExWorkerAccidentInsuranceNumber.Text = staffMasterVo.WorkerAccidentInsuranceNumber;
            TextBoxExWorkerAccidentInsuranceNote.Text = staffMasterVo.WorkerAccidentInsuranceNote;
            /*
             * HGroupBoxExMedical
             * 健康状態(健康診断等の実施結果による特記すべき事項)　※運転の可否に十分に留意すること
             */
            this.ScreenOutputGroupBoxExMedical(staffMasterVo.ListHStaffMedicalExaminationVo);
            /*
             * HGroupBoxExCarViolate
             * 業務上の交通違反歴
             */
            this.ScreenOutputGroupBoxExCarViolate(staffMasterVo.ListHStaffCarViolateVo);
            /*
             * HGroupBoxEducate
             * 社内教育の実施記録
             */
            this.ScreenOutputGroupBoxEducate(staffMasterVo.ListHStaffEducateVo);
            /*
             * HGroupBoxProper
             * 適正診断(NASVA他)
             */
            this.ScreenOutputGroupBoxProper(staffMasterVo.ListHStaffProperVo);
            /*
             * HGroupBoxExPunishment
             * 賞罰・譴責
             */
            this.ScreenOutputGroupBoxExPunishment(staffMasterVo.ListHStaffPunishmentVo);
        }

        /// <summary>
        /// 職業履歴
        /// </summary>
        private void ScreenOutputGroupBoxExHistory(List<StaffHistoryVo> listStaffHistoryVo) {
            Dictionary<int, DateTimePickerEx> dictionaryHistoryDate = new() { { 0, DateTimeExHistoryDate1 }, { 1, DateTimeExHistoryDate2 }, { 2, DateTimeExHistoryDate3 } };
            Dictionary<int, TextBoxEx> dictionaryHistoryNote = new() { { 0, TextBoxExCompanyName1 }, { 1, TextBoxExCompanyName2 }, { 2, TextBoxExCompanyName3 } };
            DateTimeExHistoryDate.SetClear();
            TextBoxExCompanyName.Text = string.Empty;
            int countGroupBoxExHistory = 0;
            foreach (StaffHistoryVo staffHistoryVo in listStaffHistoryVo) {
                dictionaryHistoryDate[countGroupBoxExHistory].SetValueJp(staffHistoryVo.HistoryDate);
                dictionaryHistoryNote[countGroupBoxExHistory].Text = staffHistoryVo.CompanyName;
                countGroupBoxExHistory++;
                if (countGroupBoxExHistory > 2)
                    break;
            }
        }

        /// <summary>
        /// 過去に運転経験のある自動車の種類・経験期間等
        /// </summary>
        private void ScreenOutputGroupBoxExExperience(List<StaffExperienceVo> listStaffExperienceVo) {
            Dictionary<int, ComboBoxEx> dictionaryExperienceKind = new() { { 0, ComboBoxExExperienceKind1 }, { 1, ComboBoxExExperienceKind2 }, { 2, ComboBoxExExperienceKind3 } };
            Dictionary<int, TextBoxEx> dictionaryExperienceLoad = new() { { 0, TextBoxExExperienceLoad1 }, { 1, TextBoxExExperienceLoad2 }, { 2, TextBoxExExperienceLoad3 } };
            Dictionary<int, TextBoxEx> dictionaryExperienceDuration = new() { { 0, TextBoxExExperienceDuration1 }, { 1, TextBoxExExperienceDuration2 }, { 2, TextBoxExExperienceDuration3 } };
            Dictionary<int, TextBoxEx> dictionaryExperienceNote = new() { { 0, TextBoxExExperienceNote1 }, { 1, TextBoxExExperienceNote2 }, { 2, TextBoxExExperienceNote3 } };
            ComboBoxExExperienceKind.SelectedIndex = -1;
            TextBoxExExperienceLoad.Text = string.Empty;
            TextBoxExExperienceDuration.Text = string.Empty;
            TextBoxExExperienceNote.Text = string.Empty;
            int countGroupBoxExExperience = 0;
            foreach (StaffExperienceVo staffExperienceVo in listStaffExperienceVo) {
                dictionaryExperienceKind[countGroupBoxExExperience].Text = staffExperienceVo.ExperienceKind;
                dictionaryExperienceLoad[countGroupBoxExExperience].Text = staffExperienceVo.ExperienceLoad;
                dictionaryExperienceDuration[countGroupBoxExExperience].Text = staffExperienceVo.ExperienceDuration;
                dictionaryExperienceNote[countGroupBoxExExperience].Text += staffExperienceVo.ExperienceNote;
                countGroupBoxExExperience++;
                if (countGroupBoxExExperience > 2)
                    break;
            }
        }

        /// <summary>
        /// 家族構成
        /// </summary>
        private void ScreenOutputGroupBoxExFamily(List<StaffFamilyVo> listStaffFamilyVo) {
            Dictionary<int, TextBoxEx> dictionaryFamilyName = new() { { 0, TextBoxExFamilyName1 }, { 1, TextBoxExFamilyName2 }, { 2, TextBoxExFamilyName3 } };
            Dictionary<int, DateTimePickerEx> dictionaryFamilyBirthDate = new() { { 0, DateTimeExFamilyBirthDate1 }, { 1, DateTimeExFamilyBirthDate2 }, { 2, DateTimeExFamilyBirthDate3 } };
            Dictionary<int, ComboBoxEx> dictionaryFamilyRelationship = new() { { 0, ComboBoxExFamilyRelationship1 }, { 1, ComboBoxExFamilyRelationship2 }, { 2, ComboBoxExFamilyRelationship3 } };
            TextBoxExFamilyName.Text = string.Empty;
            DateTimeExFamilyBirthDate.SetClear();
            ComboBoxExFamilyRelationship.SelectedIndex = -1;
            int countGroupBoxExFamily = 0;
            foreach (StaffFamilyVo staffFamilyVo in listStaffFamilyVo) {
                dictionaryFamilyName[countGroupBoxExFamily].Text = staffFamilyVo.FamilyName;
                dictionaryFamilyBirthDate[countGroupBoxExFamily].SetValueJp(staffFamilyVo.FamilyBirthDay);
                dictionaryFamilyRelationship[countGroupBoxExFamily].Text = staffFamilyVo.FamilyRelationship;
                countGroupBoxExFamily++;
                if (countGroupBoxExFamily > 2)
                    break;
            }

        }

        /// <summary>
        /// 健康状態(健康診断等の実施結果による特記すべき事項)　※運転の可否に十分に留意すること
        /// </summary>
        private void ScreenOutputGroupBoxExMedical(List<StaffMedicalExaminationVo> listStaffMedicalExaminationVo) {
            Dictionary<int, DateTimePickerEx> dictionaryMedicalDate = new() { { 0, DateTimeExMedicalExaminationDate1 }, { 1, DateTimeExMedicalExaminationDate2 }, { 2, DateTimeExMedicalExaminationDate3 } };
            Dictionary<int, ComboBoxEx> dictionaryMedicalName = new() { { 0, ComboBoxExMedicalInstitutionName1 }, { 1, ComboBoxExMedicalInstitutionName2 }, { 2, ComboBoxExMedicalInstitutionName3 } };
            Dictionary<int, TextBoxEx> dictionaryMedicalNote = new() { { 0, TextBoxExMedicalExaminationNote1 }, { 1, TextBoxExMedicalExaminationNote2 }, { 2, TextBoxExMedicalExaminationNote3 } };
            DateTimeExMedicalExaminationDate.SetClear();
            ComboBoxExMedicalInstitutionName.SelectedIndex = -1;
            TextBoxExMedicalExaminationNote.Text = string.Empty;
            int countGroupBoxExMedical = 0;
            foreach (StaffMedicalExaminationVo staffMedicalExaminationVo in listStaffMedicalExaminationVo) {
                dictionaryMedicalDate[countGroupBoxExMedical].SetValueJp(staffMedicalExaminationVo.MedicalExaminationDate);
                dictionaryMedicalName[countGroupBoxExMedical].Text = staffMedicalExaminationVo.MedicalInstitutionName;
                dictionaryMedicalNote[countGroupBoxExMedical].Text += staffMedicalExaminationVo.MedicalExaminationNote;
                countGroupBoxExMedical++;
                if (countGroupBoxExMedical > 2)
                    break;
            }
        }

        /// <summary>
        /// 業務上の交通違反歴
        /// </summary>
        private void ScreenOutputGroupBoxExCarViolate(List<StaffCarViolateVo> listStaffCarViolateVo) {
            Dictionary<int, DateTimePickerEx> dictionaryCarViolateDate = new() { { 0, DateTimeExCarViolateDate1 }, { 1, DateTimeExCarViolateDate2 }, { 2, DateTimeExCarViolateDate3 } };
            Dictionary<int, ComboBoxEx> dictionaryCarViolateContent = new() { { 0, ComboBoxExCarViolateContent1 }, { 1, ComboBoxExCarViolateContent2 }, { 2, ComboBoxExCarViolateContent3 } };
            Dictionary<int, TextBoxEx> dictionaryCarViolatePlace = new() { { 0, TextBoxExCarViolatePlace1 }, { 1, TextBoxExCarViolatePlace2 }, { 2, TextBoxExCarViolatePlace3 } };
            DateTimeExCarViolateDate.SetClear();
            ComboBoxExCarViolateContent.SelectedIndex = -1;
            TextBoxExCarViolatePlace.Text = string.Empty;
            int countGroupBoxExCarViolate = 0;
            foreach (StaffCarViolateVo staffCarViolateVo in listStaffCarViolateVo) {
                dictionaryCarViolateDate[countGroupBoxExCarViolate].SetValueJp(staffCarViolateVo.CarViolateDate);
                dictionaryCarViolateContent[countGroupBoxExCarViolate].Text = staffCarViolateVo.CarViolateContent;
                dictionaryCarViolatePlace[countGroupBoxExCarViolate].Text += staffCarViolateVo.CarViolatePlace;
                countGroupBoxExCarViolate++;
                if (countGroupBoxExCarViolate > 2)
                    break;
            }
        }

        /// <summary>
        /// 社内教育の実施記録
        /// </summary>
        private void ScreenOutputGroupBoxEducate(List<StaffEducateVo> listStaffEducateVo) {
            Dictionary<int, DateTimePickerEx> dictionaryEducateDate = new() { { 0, DateTimeExEducateDate1 }, { 1, DateTimeExEducateDate2 }, { 2, DateTimeExEducateDate3 } };
            Dictionary<int, ComboBoxEx> dictionaryEducateName = new() { { 0, ComboBoxExEducateName1 }, { 1, ComboBoxExEducateName2 }, { 2, ComboBoxExEducateName3 } };
            DateTimeExEducateDate.SetClear();
            ComboBoxExEducateName.SelectedIndex = -1;
            int countGroupBoxEducate = 0;
            foreach (StaffEducateVo staffEducateVo in listStaffEducateVo) {
                dictionaryEducateDate[countGroupBoxEducate].SetValueJp(staffEducateVo.EducateDate);
                dictionaryEducateName[countGroupBoxEducate].Text = staffEducateVo.EducateName;
                countGroupBoxEducate++;
                if (countGroupBoxEducate > 2)
                    break;
            }
        }

        /// <summary>
        /// 適正診断(NASVA他)
        /// </summary>
        private void ScreenOutputGroupBoxProper(List<StaffProperVo> listStaffProperVo) {
            Dictionary<int, ComboBoxEx> dictionaryProperKind = new() { { 0, ComboBoxExProperKind1 }, { 1, ComboBoxExProperKind2 }, { 2, ComboBoxExProperKind3 } };
            Dictionary<int, DateTimePickerEx> dictionaryProperDate = new() { { 0, DateTimeExProperDate1 }, { 1, DateTimeExProperDate2 }, { 2, DateTimeExProperDate3 } };
            Dictionary<int, TextBoxEx> dictionaryProperNote = new() { { 0, TextBoxExProperNote1 }, { 1, TextBoxExProperNote2 }, { 2, TextBoxExProperNote3 } };
            ComboBoxExProperKind.SelectedIndex = -1;
            DateTimeExProperDate.SetClear();
            TextBoxExProperNote.Text = string.Empty;
            int countGroupBoxProper = 0;
            foreach (StaffProperVo staffProperVo in listStaffProperVo.OrderByDescending(x => x.ProperDate)) {
                dictionaryProperKind[countGroupBoxProper].Text = staffProperVo.ProperKind;
                dictionaryProperDate[countGroupBoxProper].SetValueJp(staffProperVo.ProperDate);
                dictionaryProperNote[countGroupBoxProper].Text = staffProperVo.ProperNote;
                countGroupBoxProper++;
                if (countGroupBoxProper > 2)
                    break;
            }
        }

        /// <summary>
        /// 賞罰・譴責
        /// </summary>
        private void ScreenOutputGroupBoxExPunishment(List<StaffPunishmentVo> listStaffPunishmentVo) {
            Dictionary<int, DateTimePickerEx> dictionaryPunishmentDate = new() { { 0, DateTimeExPunishmentDate1 }, { 1, DateTimeExPunishmentDate2 }, { 2, DateTimeExPunishmentDate3 } };
            Dictionary<int, ComboBoxEx> dictionaryPunishmentNote = new() { { 0, ComboBoxExPunishmentNote1 }, { 1, ComboBoxExPunishmentNote2 }, { 2, ComboBoxExPunishmentNote3 } };
            DateTimeExPunishmentDate.SetClear();
            ComboBoxExPunishmentNote.Text = string.Empty;
            int countGroupBoxExPunishment = 0;
            foreach (StaffPunishmentVo staffPunishmentVo in listStaffPunishmentVo) {
                dictionaryPunishmentDate[countGroupBoxExPunishment].SetValueJp(staffPunishmentVo.PunishmentDate);
                dictionaryPunishmentNote[countGroupBoxExPunishment].Text = staffPunishmentVo.PunishmentNote;
                countGroupBoxExPunishment++;
                if (countGroupBoxExPunishment > 2)
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEx_Click(object sender, EventArgs e) {
            switch (((Button)sender).Name) {
                case "ButtonExUpdate":
                    try {
                        int.TryParse(LabelExStaffCode.Text, out int staffCode);
                        if (_staffMasterDao.ExistenceStaffMaster(staffCode)) {
                            _staffMasterDao.UpdateOneStaffMaster(SetVo());          // UPDATE
                            StatusStripEx1.ToolStripStatusLabelDetail.Text = "Update Success";
                            this.Close();
                        } else {
                            _staffMasterDao.InsertOneStaffMaster(SetVo());          // INSERT
                            StatusStripEx1.ToolStripStatusLabelDetail.Text = "Insert Success";
                            this.Close();
                        }
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "AddGroupBoxExHistory": // 職業履歴
                    try {
                        // 更新
                        StaffHistoryVo staffHistoryVo = new();
                        int.TryParse(LabelExStaffCode.Text, out int _staffCode); // StaffCode
                        staffHistoryVo.StaffCode = _staffCode;
                        staffHistoryVo.HistoryDate = DateTimeExHistoryDate.GetValue();
                        staffHistoryVo.CompanyName = TextBoxExCompanyName.Text;
                        /*
                         * Validation
                         */
                        if (DateTimeExHistoryDate.GetValue().Date == _defaultDateTime.Date) {
                            _errorProvider.SetError(DateTimeExHistoryDate, "入社日");
                            break;
                        } else if (TextBoxExCompanyName.Text.Length == 0) {
                            _errorProvider.SetError(TextBoxExCompanyName, "在籍記録");
                            break;
                        }
                        _staffHistoryDao.InsertOneStaffHistoryMaster(staffHistoryVo);
                        StatusStripEx1.ToolStripStatusLabelDetail.Text = "GroupBoxExHistoryを更新しました。";
                        /*
                         * 更新後の初期化
                         */
                        _errorProvider.Clear();
                        DateTimeExHistoryDate.SetClear();
                        TextBoxExCompanyName.Text = string.Empty;
                        // 再表示
                        this.ScreenOutputGroupBoxExHistory(_staffHistoryDao.SelectOneStaffHistoryMaster(_staffCode));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "AddGroupBoxExExperience": // 過去に運転経験のある自動車の種類・経験期間等
                    try {
                        // 更新
                        StaffExperienceVo staffExperienceVo = new();
                        int.TryParse(LabelExStaffCode.Text, out int _staffCode); // StaffCode
                        staffExperienceVo.StaffCode = _staffCode;
                        staffExperienceVo.ExperienceKind = ComboBoxExExperienceKind.Text;
                        staffExperienceVo.ExperienceLoad = TextBoxExExperienceLoad.Text;
                        staffExperienceVo.ExperienceDuration = TextBoxExExperienceDuration.Text;
                        staffExperienceVo.ExperienceNote = TextBoxExExperienceNote.Text;
                        /*
                         * Validation
                         */
                        if (ComboBoxExExperienceKind.Text.Length == 0) {
                            _errorProvider.SetError(ComboBoxExExperienceKind, "過去に運転経験のある自動車の種類");
                            break;
                        } else if (TextBoxExExperienceLoad.Text.Length == 0) {
                            _errorProvider.SetError(TextBoxExExperienceLoad, "過去に運転経験のある自動車の積載量");
                            break;
                        } else if (TextBoxExExperienceDuration.Text.Length == 0) {
                            _errorProvider.SetError(TextBoxExExperienceDuration, "過去に運転経験のある自動車の経験期間");
                            break;
                        }
                        _staffExperienceDao.InsertOneStaffExperienceMaster(staffExperienceVo);
                        StatusStripEx1.ToolStripStatusLabelDetail.Text = "GroupBoxExExperienceを更新しました。";
                        /*
                         * 更新後の初期化
                         */
                        _errorProvider.Clear();
                        ComboBoxExExperienceKind.Text = string.Empty;
                        TextBoxExExperienceLoad.Text = string.Empty;
                        TextBoxExExperienceDuration.Text = string.Empty;
                        TextBoxExExperienceNote.Text = string.Empty;
                        // 再表示
                        this.ScreenOutputGroupBoxExExperience(_staffExperienceDao.SelectOneStaffExperienceMaster(_staffCode));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "AddGroupBoxExFamily": // 家族構成
                    try {
                        // 更新
                        StaffFamilyVo staffFamilyVo = new();
                        int.TryParse(LabelExStaffCode.Text, out int _staffCode); // StaffCode
                        staffFamilyVo.StaffCode = _staffCode;
                        staffFamilyVo.FamilyName = TextBoxExFamilyName.Text;
                        staffFamilyVo.FamilyBirthDay = DateTimeExFamilyBirthDate.GetValue();
                        staffFamilyVo.FamilyRelationship = ComboBoxExFamilyRelationship.Text;
                        /*
                         * Validation
                         */
                        if (TextBoxExFamilyName.Text.Length == 0) {
                            _errorProvider.SetError(TextBoxExFamilyName, "家族氏名");
                            break;
                        } else if (DateTimeExFamilyBirthDate.GetValue().Date == _defaultDateTime.Date) {
                            _errorProvider.SetError(DateTimeExFamilyBirthDate, "生年月日");
                            break;
                        } else if (ComboBoxExFamilyRelationship.Text.Length == 0) {
                            _errorProvider.SetError(ComboBoxExFamilyRelationship, "従業員との関係");
                            break;
                        }
                        _staffFamilyDao.InsertOneStaffFamilyMaster(staffFamilyVo);
                        StatusStripEx1.ToolStripStatusLabelDetail.Text = "AddHGroupBoxExFamilyを更新しました。";
                        /*
                         * 更新後の初期化
                         */
                        _errorProvider.Clear();
                        TextBoxExFamilyName.Text = string.Empty;
                        DateTimeExFamilyBirthDate.SetClear();
                        ComboBoxExFamilyRelationship.Text = string.Empty;
                        // 再表示
                        this.ScreenOutputGroupBoxExFamily(_staffFamilyDao.SelectOneStaffFamilyMaster(_staffCode));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "AddGroupBoxExMedical": // 健康状態(健康診断等の実施結果による特記すべき事項)　※運転の可否に十分に留意すること
                    try {
                        // 更新
                        StaffMedicalExaminationVo staffMedicalExaminationVo = new();
                        int.TryParse(LabelExStaffCode.Text, out int _staffCode); // StaffCode
                        staffMedicalExaminationVo.StaffCode = _staffCode;
                        staffMedicalExaminationVo.MedicalExaminationDate = DateTimeExMedicalExaminationDate.GetValue();
                        staffMedicalExaminationVo.MedicalInstitutionName = ComboBoxExMedicalInstitutionName.Text;
                        staffMedicalExaminationVo.MedicalExaminationNote = TextBoxExMedicalExaminationNote.Text;
                        /*
                         * Validation
                         */
                        if (DateTimeExMedicalExaminationDate.GetValue().Date == _defaultDateTime.Date) {
                            _errorProvider.SetError(DateTimeExMedicalExaminationDate, "健診実施日");
                            break;
                        } else if (ComboBoxExMedicalInstitutionName.Text.Length == 0) {
                            _errorProvider.SetError(ComboBoxExMedicalInstitutionName, "受診機関名");
                            break;
                        }
                        _staffMedicalExaminationDao.InsertOneStaffMedicalExaminationMaster(staffMedicalExaminationVo);
                        StatusStripEx1.ToolStripStatusLabelDetail.Text = "GroupBoxExMedicalを更新しました。";
                        /*
                         * 更新後の初期化
                         */
                        _errorProvider.Clear();
                        DateTimeExMedicalExaminationDate.SetClear();
                        ComboBoxExMedicalInstitutionName.Text = string.Empty;
                        TextBoxExMedicalExaminationNote.Text = string.Empty;
                        // 再表示
                        this.ScreenOutputGroupBoxExMedical(_staffMedicalExaminationDao.SelectOneStaffMedicalExaminationMaster(_staffCode));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "AddGroupBoxExCarViolate": // 業務上の交通違反歴
                    try {
                        // 更新
                        StaffCarViolateVo staffCarViolateVo = new();
                        int.TryParse(LabelExStaffCode.Text, out int _staffCode); // StaffCode
                        staffCarViolateVo.StaffCode = _staffCode;
                        staffCarViolateVo.CarViolateDate = DateTimeExCarViolateDate.GetValue();
                        staffCarViolateVo.CarViolateContent = ComboBoxExCarViolateContent.Text;
                        staffCarViolateVo.CarViolatePlace = TextBoxExCarViolatePlace.Text;
                        /*
                         * Validation
                         */
                        if (DateTimeExCarViolateDate.GetValue().Date == _defaultDateTime.Date) {
                            _errorProvider.SetError(DateTimeExCarViolateDate, "違反年月日");
                            break;
                        } else if (ComboBoxExCarViolateContent.Text.Length == 0) {
                            _errorProvider.SetError(ComboBoxExCarViolateContent, "違反名");
                            break;
                        }
                        _staffCarViolateDao.InsertOneStaffCarViolateMaster(staffCarViolateVo);
                        StatusStripEx1.ToolStripStatusLabelDetail.Text = "GroupBoxExCarViolateを更新しました。";
                        /*
                         * 更新後の初期化
                         */
                        _errorProvider.Clear();
                        DateTimeExCarViolateDate.SetClear();
                        ComboBoxExCarViolateContent.Text = string.Empty;
                        TextBoxExCarViolatePlace.Text = string.Empty;
                        // 再表示
                        this.ScreenOutputGroupBoxExCarViolate(_staffCarViolateDao.SelectOneStaffCarViolateMaster(_staffCode));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "AddGroupBoxEducate": // 社内教育の実施記録
                    try {
                        // 更新
                        StaffEducateVo staffEducateVo = new();
                        int.TryParse(LabelExStaffCode.Text, out int _staffCode); // StaffCode
                        staffEducateVo.StaffCode = _staffCode;
                        staffEducateVo.EducateDate = DateTimeExEducateDate.GetValue();
                        staffEducateVo.EducateName = ComboBoxExEducateName.Text;
                        /*
                         * Validation
                         */
                        if (DateTimeExEducateDate.GetValue().Date == _defaultDateTime.Date) {
                            _errorProvider.SetError(DateTimeExEducateDate, "教育を受けた年月日");
                            break;
                        } else if (ComboBoxExEducateName.Text.Length == 0) {
                            _errorProvider.SetError(ComboBoxExEducateName, "教育名称");
                            break;
                        }
                        _staffEducateDao.InsertOneStaffEducateMaster(staffEducateVo);
                        StatusStripEx1.ToolStripStatusLabelDetail.Text = "AddGroupBoxEducateを更新しました。";
                        /*
                         * 更新後の初期化
                         */
                        _errorProvider.Clear();
                        DateTimeExEducateDate.SetClear();
                        ComboBoxExEducateName.Text = string.Empty;
                        // 再表示
                        this.ScreenOutputGroupBoxEducate(_staffEducateDao.SelectOneStaffEducateMaster(_staffCode));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "AddGroupBoxProper": // 適正診断(NASVA他)
                    try {
                        // 更新
                        StaffProperVo staffProperVo = new();
                        int.TryParse(LabelExStaffCode.Text, out int _staffCode); // StaffCode
                        staffProperVo.StaffCode = _staffCode;
                        staffProperVo.ProperKind = ComboBoxExProperKind.Text;
                        staffProperVo.ProperDate = DateTimeExProperDate.GetValue();
                        staffProperVo.ProperNote = TextBoxExProperNote.Text;
                        /*
                        * Validation
                        */
                        if (ComboBoxExProperKind.Text.Length == 0) {
                            _errorProvider.SetError(ComboBoxExProperKind, "診断の種類");
                            break;
                        } else if (DateTimeExProperDate.GetValue().Date == _defaultDateTime.Date) {
                            _errorProvider.SetError(DateTimeExProperDate, "診断年月日");
                            break;
                        } else if (TextBoxExProperNote.Text.Length == 0) {
                            _errorProvider.SetError(TextBoxExProperNote, "");
                            break;
                        }
                        _staffProperDao.InsertOneStaffProperMaster(staffProperVo);
                        StatusStripEx1.ToolStripStatusLabelDetail.Text = "AddGroupBoxProperを更新しました。";
                        /*
                         * 更新後の初期化
                         */
                        _errorProvider.Clear();
                        ComboBoxExProperKind.Text = string.Empty;
                        DateTimeExProperDate.SetClear();
                        TextBoxExProperNote.Text = string.Empty;
                        // 再表示
                        this.ScreenOutputGroupBoxProper(_staffProperDao.SelectOneStaffProperMaster(_staffCode));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "AddGroupBoxExPunishment": // 賞罰・譴責
                    try {
                        // 更新
                        StaffPunishmentVo staffPunishmentVo = new();
                        int.TryParse(LabelExStaffCode.Text, out int _staffCode); // StaffCode
                        staffPunishmentVo.StaffCode = _staffCode;
                        staffPunishmentVo.PunishmentDate = DateTimeExPunishmentDate.GetValue();
                        staffPunishmentVo.PunishmentNote = ComboBoxExPunishmentNote.Text;
                        /*
                         * Validation
                         */
                        if (DateTimeExPunishmentDate.GetValue().Date == _defaultDateTime.Date) {
                            _errorProvider.SetError(DateTimeExPunishmentDate, "年月日");
                            break;
                        } else if (ComboBoxExPunishmentNote.Text.Length == 0) {
                            _errorProvider.SetError(ComboBoxExPunishmentNote, "備考");
                            break;
                        }
                        _staffPunishmentDao.InsertOneStaffPunishmentMasters(staffPunishmentVo);
                        StatusStripEx1.ToolStripStatusLabelDetail.Text = "AddGroupBoxExPunishmentを更新しました。";
                        /*
                         * 更新後の初期化
                         */
                        _errorProvider.Clear();
                        DateTimeExPunishmentDate.SetClear();
                        ComboBoxExPunishmentNote.Text = string.Empty;
                        // 再表示
                        this.ScreenOutputGroupBoxExPunishment(_staffPunishmentDao.SelectOneStaffPunishmentMaster(_staffCode));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
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
                /*
                 * Picture クリップボード
                 */
                case "ToolStripMenuItemPictureClip":
                    PictureBoxExStaff.Image = (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap);
                    break;
                /*
                 * Picture 削除
                 */
                case "ToolStripMenuItemPictureDelete":
                    PictureBoxExStaff.Image = null;
                    break;
                /*
                 * 印影　クリップ
                 */
                case "ToolStripMenuItemStampClip":
                    PictureBoxExStamp.Image = (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap);
                    break;
                /*
                 * 印影　削除
                 */
                case "ToolStripMenuItemStampDelete":
                    PictureBoxExStamp.Image = null;
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButtonEx_CheckedChanged(object sender, EventArgs e) {
            switch (((RadioButtonEx)sender).Text) {
                case "役員":
                    RadioButtonExLongTimeS.Enabled = false;
                    RadioButtonExShortTimeS.Enabled = false;
                    RadioButtonExLongTimeJ.Enabled = false;
                    RadioButtonExShortTimeJ.Enabled = false;
                    RadioButtonExJobFormNothing.Enabled = true;
                    RadioButtonExJobFormNothing.Checked = true;

                    RadioButtonExOfficeWorker.Enabled = false;
                    RadioButtonExDrivers.Enabled = false;
                    RadioButtonExWorkers.Enabled = false;
                    radioButtonEx15.Enabled = false;
                    radioButtonEx17.Enabled = false;
                    radioButtonEx18.Enabled = true;
                    radioButtonEx18.Checked = true;
                    break;
                case "社員":
                    RadioButtonExLongTimeS.Enabled = false;
                    RadioButtonExShortTimeS.Enabled = false;
                    RadioButtonExLongTimeJ.Enabled = false;
                    RadioButtonExShortTimeJ.Enabled = false;
                    RadioButtonExJobFormNothing.Enabled = true;
                    RadioButtonExJobFormNothing.Checked = true;

                    RadioButtonExOfficeWorker.Enabled = true;
                    RadioButtonExDrivers.Enabled = true;
                    RadioButtonExWorkers.Enabled = true;
                    radioButtonEx15.Enabled = true;
                    radioButtonEx17.Enabled = true;
                    radioButtonEx18.Enabled = true;
                    break;
                case "アルバイト":
                    RadioButtonExLongTimeS.Enabled = false;
                    RadioButtonExShortTimeS.Enabled = false;
                    RadioButtonExLongTimeJ.Enabled = false;
                    RadioButtonExShortTimeJ.Enabled = false;
                    RadioButtonExJobFormNothing.Enabled = true;
                    RadioButtonExJobFormNothing.Checked = true;

                    RadioButtonExOfficeWorker.Enabled = true;
                    RadioButtonExDrivers.Enabled = true;
                    RadioButtonExWorkers.Enabled = true;
                    radioButtonEx15.Enabled = true;
                    radioButtonEx17.Enabled = true;
                    radioButtonEx18.Enabled = true;
                    break;
                case "派遣":
                    RadioButtonExLongTimeS.Enabled = false;
                    RadioButtonExShortTimeS.Enabled = false;
                    RadioButtonExLongTimeJ.Enabled = false;
                    RadioButtonExShortTimeJ.Enabled = false;
                    RadioButtonExJobFormNothing.Enabled = true;
                    RadioButtonExJobFormNothing.Checked = true;

                    RadioButtonExOfficeWorker.Enabled = false;
                    RadioButtonExDrivers.Enabled = true;
                    RadioButtonExWorkers.Enabled = true;
                    radioButtonEx15.Enabled = false;
                    radioButtonEx17.Enabled = false;
                    radioButtonEx18.Enabled = false;
                    break;
                case "嘱託雇用契約社員":
                    RadioButtonExLongTimeS.Enabled = false;
                    RadioButtonExShortTimeS.Enabled = false;
                    RadioButtonExLongTimeJ.Enabled = false;
                    RadioButtonExShortTimeJ.Enabled = false;
                    RadioButtonExJobFormNothing.Enabled = true;
                    RadioButtonExJobFormNothing.Checked = true;

                    RadioButtonExOfficeWorker.Enabled = true;
                    RadioButtonExDrivers.Enabled = false;
                    RadioButtonExWorkers.Enabled = false;
                    radioButtonEx15.Enabled = false;
                    radioButtonEx17.Enabled = false;
                    radioButtonEx18.Enabled = false;
                    break;
                case "パートタイマー":
                    RadioButtonExLongTimeS.Enabled = false;
                    RadioButtonExShortTimeS.Enabled = false;
                    RadioButtonExLongTimeJ.Enabled = false;
                    RadioButtonExShortTimeJ.Enabled = false;
                    RadioButtonExJobFormNothing.Enabled = true;
                    RadioButtonExJobFormNothing.Checked = true;

                    RadioButtonExOfficeWorker.Enabled = true;
                    RadioButtonExDrivers.Enabled = false;
                    RadioButtonExWorkers.Enabled = false;
                    radioButtonEx15.Enabled = false;
                    radioButtonEx17.Enabled = false;
                    radioButtonEx18.Enabled = false;
                    break;
                case "労供":
                    RadioButtonExLongTimeS.Enabled = true;
                    RadioButtonExShortTimeS.Enabled = true;
                    RadioButtonExLongTimeJ.Enabled = true;
                    RadioButtonExShortTimeJ.Enabled = true;
                    RadioButtonExJobFormNothing.Enabled = false;

                    RadioButtonExOfficeWorker.Enabled = false;
                    RadioButtonExDrivers.Enabled = true;
                    RadioButtonExWorkers.Enabled = true;
                    radioButtonEx15.Enabled = false;
                    radioButtonEx17.Enabled = false;
                    radioButtonEx18.Enabled = false;
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StaffDetail_FormClosing(object sender, FormClosingEventArgs e) {
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
