/*
 * 2024-1-7
 */
using System.Drawing.Printing;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace Staff {
    public partial class StaffPaper : Form {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly int _staffCode;
        /*
         * Dao
         */
        private readonly StaffMasterDao _staffMasterDao;
        private readonly StaffProperDao _staffProperDao;
        private readonly StaffMedicalExaminationDao _staffMedicalExaminationDao;
        private readonly LicenseMasterDao _licenseMasterDao;
        private readonly StaffHistoryDao _staffHistoryDao;
        private readonly StaffExperienceDao _staffExperienceDao;
        private readonly StaffFamilyDao _staffFamilyDao;
        private readonly CarAccidentMasterDao _carAccidentMasterDao;
        private readonly StaffCarViolateDao _staffCarViolateDao;
        private readonly StaffEducateDao _staffEducateDao;
        private readonly StaffPunishmentDao _staffPunishmentDao;
        private readonly BelongsMasterDao _belongsMasterDao;
        private readonly JobFormMasterDao _jobFormMasterDao;
        private readonly OccupationMasterDao _occupationMasterDao;
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;
        private StaffMasterVo _staffMasterVo;
        private LicenseMasterVo _licenseMasterVo;
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
        /// <param name="staffMasterVo"></param>
        public StaffPaper(ConnectionVo connectionVo, int staffCode) {
            _staffCode = staffCode;
            /*
             * Dao
             */
            _staffMasterDao = new(connectionVo);
            _staffProperDao = new(connectionVo);
            _staffMedicalExaminationDao = new(connectionVo);
            _licenseMasterDao = new(connectionVo);
            _staffHistoryDao = new(connectionVo);
            _staffExperienceDao = new(connectionVo);
            _staffFamilyDao = new(connectionVo);
            _carAccidentMasterDao = new(connectionVo);
            _staffCarViolateDao = new(connectionVo);
            _staffEducateDao = new(connectionVo);
            _staffPunishmentDao = new(connectionVo);
            _belongsMasterDao = new(connectionVo);
            _jobFormMasterDao = new(connectionVo);
            _occupationMasterDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            _staffMasterVo = _staffMasterDao.SelectOneStaffMaster(staffCode);
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
                "ToolStripMenuItemPrint",
                "ToolStripMenuItemPrintA4",
                "ToolStripMenuItemHelp"
            };
            MenuStripEx1.ChangeEnable(listString);

            this.InitializeSpreadStaffRegisterHead(this.SheetViewHead);
            this.InitializeSpreadStaffRegisterTail(this.SheetViewTail);
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "Initialize Success";

            this.SheetViewHeadOutPut(this.SheetViewHead);
            this.SheetViewTailOutPut(this.SheetViewTail);
            /*
             * Eventを登録する
             */
            MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// 表面
        /// </summary>
        /// <param name="sheetView"></param>
        private void SheetViewHeadOutPut(SheetView sheetView) {
            /*
             * 印刷日時
             */
            sheetView.Cells[0, 32].Text = string.Concat(DateTime.Now.ToString("yyyy年MM月dd日　印刷"));
            /*
             * 初任診断
             */
            DateTime syoninProperDate = _staffProperDao.GetSyoninProperDate(_staffMasterVo.StaffCode);
            if (syoninProperDate != _defaultDateTime) {
                sheetView.Cells[2, 10].ForeColor = Color.Black;
                sheetView.Cells[2, 10].Text = syoninProperDate.ToString("yyyy/MM/dd");
            } else {
                sheetView.Cells[2, 10].ForeColor = Color.Black;
                sheetView.Cells[2, 10].Text = "記録なし";
            }
            /*
             * 適齢診断
             */
            int age = new Common.DateUtility().GetAge(_staffMasterVo.BirthDate);
            // ”65歳以上”及び”運転手”ならForeColorを変える
            if (age >= 65 && _staffMasterVo.Occupation == 10) {
                string tekireiProperDate = _staffProperDao.GetTekireiProperDate(_staffMasterVo.StaffCode);
                if (tekireiProperDate != string.Empty) {
                    sheetView.Cells[3, 10].ForeColor = Color.Black;
                    sheetView.Cells[3, 10].Text = tekireiProperDate;
                } else {
                    sheetView.Cells[3, 10].ForeColor = Color.Black;
                    sheetView.Cells[3, 10].Text = "記録なし";
                }
            } else {
                sheetView.Cells[3, 10].ForeColor = Color.White;
                sheetView.Cells[3, 10].Text = string.Empty;
            }
            /*
             * 健康診断
             */
            DateTime medicalExaminationDate = _staffMedicalExaminationDao.GetMedicalExaminationDate(_staffMasterVo.StaffCode);
            if (medicalExaminationDate != _defaultDateTime) {
                if (medicalExaminationDate.AddYears(1) > DateTime.Now) {
                    sheetView.Cells[4, 10].ForeColor = Color.Black;
                    sheetView.Cells[4, 10].Text = medicalExaminationDate.ToString("yyyy/MM/dd");
                } else {
                    sheetView.Cells[4, 10].ForeColor = Color.Black;
                    sheetView.Cells[4, 10].Text = "１年以上経過";
                }
            } else {
                sheetView.Cells[4, 10].ForeColor = Color.Black;
                sheetView.Cells[4, 10].Text = "記録なし";
            }
            /*
             * 社員
             */
            if (_staffMasterVo.Belongs == 10 || _staffMasterVo.Belongs == 11) {
                sheetView.Cells[2, 1].ForeColor = Color.Red;
                sheetView.Cells[2, 2].ForeColor = Color.Red;
            }
            /*
             * アルバイト
             */
            if (_staffMasterVo.Belongs == 12) {
                sheetView.Cells[3, 1].ForeColor = Color.Red;
                sheetView.Cells[3, 2].ForeColor = Color.Red;
            }
            /*
             * 派遣
             */
            if (_staffMasterVo.Belongs == 13) {
                sheetView.Cells[4, 1].ForeColor = Color.Red;
                sheetView.Cells[4, 2].ForeColor = Color.Red;
            }
            /*
             * 労共(長期)
             */
            if (_staffMasterVo.Belongs == 22 && (_staffMasterVo.JobForm == 20 || _staffMasterVo.JobForm == 22)) {
                sheetView.Cells[5, 1].ForeColor = Color.Red;
                sheetView.Cells[5, 2].ForeColor = Color.Red;
            }
            /*
             * 労共(短期)
             */
            if (_staffMasterVo.Belongs == 22 && (_staffMasterVo.JobForm == 21 || _staffMasterVo.JobForm == 23)) {
                sheetView.Cells[6, 1].ForeColor = Color.Red;
                sheetView.Cells[6, 2].ForeColor = Color.Red;
            }
            /*
             * 運転手
             */
            if (_staffMasterVo.Occupation == 10) {
                sheetView.Cells[7, 1].ForeColor = Color.Red;
                sheetView.Cells[7, 2].ForeColor = Color.Red;
            }
            /*
             * 作業員
             */
            if (_staffMasterVo.Occupation == 11) {
                sheetView.Cells[8, 1].ForeColor = Color.Red;
                sheetView.Cells[8, 2].ForeColor = Color.Red;
            }
            sheetView.Cells[10, 5].Text = _staffMasterVo.NameKana; // フリガナ
            sheetView.Cells[11, 5].Text = string.Concat(_staffMasterVo.Name, " (", _dictionaryBelongs[_staffMasterVo.Belongs], ")"); // 氏名
            sheetView.Cells[11, 18].Text = _staffMasterVo.Gender; // 性別
            sheetView.Cells[11, 20].Value = _staffMasterVo.BirthDate.Date != _defaultDateTime.Date ? _staffMasterVo.BirthDate.Date : null; // 生年月日
            sheetView.Cells[11, 26].Value = _staffMasterVo.EmploymentDate.Date != _defaultDateTime.Date ? _staffMasterVo.EmploymentDate.Date : null; // 雇用年月日
            sheetView.Cells[13, 5].Text = _staffMasterVo.CurrentAddress; // 現住所
            sheetView.Cells[15, 5].Text = _staffMasterVo.Remarks; // 変更後住所
            sheetView.Cells[17, 7].Text = _staffMasterVo.TelephoneNumber; // 電話番号
            sheetView.Cells[17, 21].Text = _staffMasterVo.CellphoneNumber; // 携帯電話
            sheetView.Cells[10, 32].Value = _staffMasterVo.Picture.Length != 0 ? (Image?)new ImageConverter().ConvertFrom(_staffMasterVo.Picture) : null;
            sheetView.Cells[19, 35].Text = _staffMasterVo.BloodType;//血液型
            sheetView.Cells[21, 9].Value = _staffMasterVo.SelectionDate.Date != _defaultDateTime.Date ? _staffMasterVo.SelectionDate.Date : null;//運転者に選任された日
            sheetView.Cells[23, 9].Value = _staffMasterVo.NotSelectionDate.Date != _defaultDateTime.Date ? _staffMasterVo.NotSelectionDate.Date : null;//運転者でなくなった日
            sheetView.Cells[25, 4].Text = _staffMasterVo.NotSelectionReason;//運転者でなくなった日　理由
            /*
             * 免許証関連
             */
            try {
                _licenseMasterVo = _licenseMasterDao.SelectOneLicenseMaster(_staffMasterVo.StaffCode);
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
            sheetView.Cells[27, 7].Text = _licenseMasterVo.LicenseNumber;//免許証番号
            sheetView.Cells[27, 17].Text = _licenseMasterVo.LicenseCondition;//条件等
            string? kind = null;
            if (_licenseMasterVo.Large)
                kind += "(大型)";
            if (_licenseMasterVo.Medium)
                kind += "(中型)";
            if (_licenseMasterVo.QuasiMedium)
                kind += "(準中型)";
            if (_licenseMasterVo.Ordinary)
                kind += "(普通)";
            if (kind != null) {
                sheetView.Cells[31, 3].Text = string.Concat(kind, ":", _licenseMasterVo.DeliveryDate.ToString("yyyy年MM月dd日"));//免許証の種類/取得日1
                sheetView.Cells[31, 27].Value = _licenseMasterVo.ExpirationDate.Date;//有効期限1
            }
            /*
             * 社歴
             */
            Dictionary<int, Point> _pointHistoryDate = new() { { 0, new Point(41, 3) }, { 1, new Point(43, 3) }, { 2, new Point(45, 3) }, { 3, new Point(41, 21) }, { 4, new Point(43, 21) }, { 5, new Point(45, 21) } };
            Dictionary<int, Point> _pointHistoryNote = new() { { 0, new Point(41, 11) }, { 1, new Point(43, 11) }, { 2, new Point(45, 11) }, { 3, new Point(41, 29) }, { 4, new Point(43, 29) }, { 5, new Point(45, 29) } };
            List<StaffHistoryVo> listStaffHistoryVo = new();
            try {
                listStaffHistoryVo = _staffHistoryDao.SelectOneStaffHistoryMaster(_staffMasterVo.StaffCode);
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
            int countHStaffHistoryVo = 0;
            foreach (StaffHistoryVo staffHistoryVo in listStaffHistoryVo.OrderBy(x => x.HistoryDate)) {
                sheetView.Cells[_pointHistoryDate[countHStaffHistoryVo].X, _pointHistoryDate[countHStaffHistoryVo].Y].Value = staffHistoryVo.HistoryDate.Date != _defaultDateTime.Date ? staffHistoryVo.HistoryDate.Date : null;
                sheetView.Cells[_pointHistoryNote[countHStaffHistoryVo].X, _pointHistoryNote[countHStaffHistoryVo].Y].Text = staffHistoryVo.CompanyName;
                countHStaffHistoryVo++;
                if (countHStaffHistoryVo > 5)
                    break;
            }
            /*
             * 過去に運転経験のある車両
             */
            Dictionary<int, Point> _pointExperienceKind = new() { { 0, new Point(51, 1) }, { 1, new Point(53, 1) }, { 2, new Point(55, 1) }, { 3, new Point(57, 1) } };
            Dictionary<int, Point> _pointExperienceLoad = new() { { 0, new Point(51, 12) }, { 1, new Point(53, 12) }, { 2, new Point(55, 12) }, { 3, new Point(57, 12) } };
            Dictionary<int, Point> _pointExperienceDuration = new() { { 0, new Point(51, 20) }, { 1, new Point(53, 20) }, { 2, new Point(55, 20) }, { 3, new Point(57, 20) } };
            Dictionary<int, Point> _pointExperienceNote = new() { { 0, new Point(51, 31) }, { 1, new Point(53, 31) }, { 2, new Point(55, 31) }, { 3, new Point(57, 31) } };
            List<StaffExperienceVo> listStaffExperienceVo = new();
            try {
                listStaffExperienceVo = _staffExperienceDao.SelectOneStaffExperienceMaster(_staffMasterVo.StaffCode);
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
            int countStaffExperienceVo = 0;
            foreach (StaffExperienceVo staffExperienceVo in listStaffExperienceVo) {
                sheetView.Cells[_pointExperienceKind[countStaffExperienceVo].X, _pointExperienceKind[countStaffExperienceVo].Y].Text = staffExperienceVo.ExperienceKind;//種類
                sheetView.Cells[_pointExperienceLoad[countStaffExperienceVo].X, _pointExperienceLoad[countStaffExperienceVo].Y].Text = staffExperienceVo.ExperienceLoad;//積載量又は定員
                sheetView.Cells[_pointExperienceDuration[countStaffExperienceVo].X, _pointExperienceDuration[countStaffExperienceVo].Y].Text = staffExperienceVo.ExperienceDuration;//経験期間
                sheetView.Cells[_pointExperienceNote[countStaffExperienceVo].X, _pointExperienceNote[countStaffExperienceVo].Y].Text = staffExperienceVo.ExperienceNote;//備考
                countStaffExperienceVo++;
                if (countStaffExperienceVo > 3)
                    break;
            }
            /*
             * 解雇・死亡
             */
            sheetView.Cells[60, 10].Value = _staffMasterVo.RetirementDate.Date != _defaultDateTime.Date ? _staffMasterVo.RetirementDate.Date : null; // 解雇又は退職の年月日
            sheetView.Cells[60, 17].Text = _staffMasterVo.RetirementNote; // 解雇又は退職の理由
            sheetView.Cells[62, 15].Value = _staffMasterVo.DeathDate.Date != _defaultDateTime.Date ? _staffMasterVo.DeathDate.Date : null; // 死亡の場合の年月日
            sheetView.Cells[62, 22].Text = _staffMasterVo.DeathNote; // 死亡の場合の原因
        }

        /// <summary>
        /// 裏面
        /// </summary>
        /// <param name="sheetView"></param>
        private void SheetViewTailOutPut(SheetView sheetView) {
            /*
             * 家族状況
             */
            Dictionary<int, Point> _pointFamilyName = new() { { 0, new Point(3, 3) }, { 1, new Point(5, 3) }, { 2, new Point(7, 3) }, { 3, new Point(3, 21) }, { 4, new Point(5, 21) }, { 5, new Point(7, 21) } };
            Dictionary<int, Point> _pointFamilyBirthDay = new() { { 0, new Point(3, 12) }, { 1, new Point(5, 12) }, { 2, new Point(7, 12) }, { 3, new Point(3, 30) }, { 4, new Point(5, 30) }, { 5, new Point(7, 30) } };
            Dictionary<int, Point> _pointFamilyRelationship = new() { { 0, new Point(3, 18) }, { 1, new Point(5, 18) }, { 2, new Point(7, 18) }, { 3, new Point(3, 36) }, { 4, new Point(5, 36) }, { 5, new Point(7, 36) } };
            List<StaffFamilyVo> listStaffFamilyVo = new();
            try {
                listStaffFamilyVo = _staffFamilyDao.SelectOneStaffFamilyMaster(_staffCode);
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
            int countStaffFamilyVo = 0;
            foreach (StaffFamilyVo staffFamilyVo in listStaffFamilyVo.OrderBy(x => x.FamilyBirthDay)) {
                sheetView.Cells[_pointFamilyName[countStaffFamilyVo].X, _pointFamilyName[countStaffFamilyVo].Y].Text = staffFamilyVo.FamilyName;
                sheetView.Cells[_pointFamilyBirthDay[countStaffFamilyVo].X, _pointFamilyBirthDay[countStaffFamilyVo].Y].Value = staffFamilyVo.FamilyBirthDay.Date != _defaultDateTime.Date ? staffFamilyVo.FamilyBirthDay.Date : null;
                sheetView.Cells[_pointFamilyRelationship[countStaffFamilyVo].X, _pointFamilyRelationship[countStaffFamilyVo].Y].Text = staffFamilyVo.FamilyRelationship;
                countStaffFamilyVo++;
                if (countStaffFamilyVo > 5)
                    break;
            }
            sheetView.Cells[9, 9].Value = _staffMasterVo.UrgentTelephoneNumber;                                                                                                 // 緊急時連絡方法　電話
            sheetView.Cells[9, 17].Value = _staffMasterVo.UrgentTelephoneMethod;                                                                                                // 緊急時連絡方法　方法
            /*
             * 保険関係
             */
            sheetView.Cells[14, 10].Value = _staffMasterVo.HealthInsuranceDate.Date != _defaultDateTime.Date ? _staffMasterVo.HealthInsuranceDate.Date : null;                  // 健康保険加入年月日
            sheetView.Cells[14, 17].Value = _staffMasterVo.HealthInsuranceNumber;                                                                                               // 健康保険の記号・番号
            sheetView.Cells[14, 28].Value = _staffMasterVo.HealthInsuranceNote;                                                                                                 // 健康保険の備考
            sheetView.Cells[16, 10].Value = _staffMasterVo.WelfarePensionDate.Date != _defaultDateTime.Date ? _staffMasterVo.WelfarePensionDate.Date : null;                    // 厚生年金保険加入年月日
            sheetView.Cells[16, 17].Value = _staffMasterVo.WelfarePensionNumber;                                                                                                // 厚生年金保険の記号・番号
            sheetView.Cells[16, 28].Value = _staffMasterVo.WelfarePensionNote;                                                                                                  // 厚生年金保険の備考
            sheetView.Cells[18, 10].Value = _staffMasterVo.EmploymentInsuranceDate.Date != _defaultDateTime.Date ? _staffMasterVo.EmploymentInsuranceDate.Date : null;          // 雇用保険加入年月日
            sheetView.Cells[18, 17].Value = _staffMasterVo.EmploymentInsuranceNumber;                                                                                           // 雇用保険の記号・番号
            sheetView.Cells[18, 28].Value = _staffMasterVo.EmploymentInsuranceNote;                                                                                             // 雇用保険の備考
            sheetView.Cells[20, 10].Value = _staffMasterVo.WorkerAccidentInsuranceDate.Date != _defaultDateTime.Date ? _staffMasterVo.WorkerAccidentInsuranceDate.Date : null;  // 労災保険加入年月日
            sheetView.Cells[20, 17].Value = _staffMasterVo.WorkerAccidentInsuranceNumber;                                                                                       // 労災保険の記号・番号
            sheetView.Cells[20, 28].Value = _staffMasterVo.WorkerAccidentInsuranceNote;                                                                                         // 労災保険の備考
            /*
             * 健康診断
             */
            Dictionary<int, Point> _pointMedicalExaminationDate = new() { { 0, new Point(25, 1) }, { 1, new Point(27, 1) }, { 2, new Point(29, 1) }, { 3, new Point(31, 1) } };
            Dictionary<int, Point> _pointMedicalInstitutionName = new() { { 0, new Point(25, 10) }, { 1, new Point(27, 10) }, { 2, new Point(29, 10) }, { 3, new Point(31, 10) } };
            List<StaffMedicalExaminationVo> listStaffMedicalExaminationVo = new();
            try {
                listStaffMedicalExaminationVo = _staffMedicalExaminationDao.SelectOneStaffMedicalExaminationMaster(_staffCode);
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
            int countHStaffMedicalExaminationVo = 0;
            foreach (StaffMedicalExaminationVo staffMedicalExaminationVo in listStaffMedicalExaminationVo.OrderByDescending(x => x.MedicalExaminationDate)) {
                sheetView.Cells[_pointMedicalExaminationDate[countHStaffMedicalExaminationVo].X, _pointMedicalExaminationDate[countHStaffMedicalExaminationVo].Y].Value = staffMedicalExaminationVo.MedicalExaminationDate.Date != _defaultDateTime.Date ? staffMedicalExaminationVo.MedicalExaminationDate.Date : null;
                sheetView.Cells[_pointMedicalInstitutionName[countHStaffMedicalExaminationVo].X, _pointMedicalInstitutionName[countHStaffMedicalExaminationVo].Y].Text = staffMedicalExaminationVo.MedicalInstitutionName;
                countHStaffMedicalExaminationVo++;
                if (countHStaffMedicalExaminationVo > 3)
                    break;
            }
            sheetView.Cells[33, 10].Value = countHStaffMedicalExaminationVo != 0 ? "診断結果を参照" : ""; // 診断以外で気づいた点
            /*
             * 交通事故発生年月日・概要
             */
            Dictionary<int, Point> _pointOccurrenceYmdHms = new() { { 0, new Point(39, 1) }, { 1, new Point(41, 1) }, { 2, new Point(43, 1) }, { 3, new Point(39, 20) }, { 4, new Point(41, 20) }, { 5, new Point(43, 20) } };
            Dictionary<int, Point> _pointAccidentSummary = new() { { 0, new Point(39, 6) }, { 1, new Point(41, 6) }, { 2, new Point(43, 6) }, { 3, new Point(39, 25) }, { 4, new Point(41, 25) }, { 5, new Point(43, 25) } };
            List<CarAccidentMasterVo> listCarAccidentMasterVo = new();
            try {
                listCarAccidentMasterVo = _carAccidentMasterDao.SelectGroupCarAccidentMaster(_staffCode);
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
            int countHCarAccidentMasterVo = 0;
            foreach (CarAccidentMasterVo hCarAccidentMasterVo in listCarAccidentMasterVo.OrderByDescending(x => x.OccurrenceYmdHms)) {
                sheetView.Cells[_pointOccurrenceYmdHms[countHCarAccidentMasterVo].X, _pointOccurrenceYmdHms[countHCarAccidentMasterVo].Y].Value = hCarAccidentMasterVo.OccurrenceYmdHms.Date != _defaultDateTime.Date ? hCarAccidentMasterVo.OccurrenceYmdHms.Date : null;
                sheetView.Cells[_pointAccidentSummary[countHCarAccidentMasterVo].X, _pointAccidentSummary[countHCarAccidentMasterVo].Y].Text = hCarAccidentMasterVo.AccidentSummary;
                countHCarAccidentMasterVo++;
                if (countHCarAccidentMasterVo > 5)
                    break;
            }
            /*
             * 交通違反
             */
            Dictionary<int, Point> _pointCarViolateDate = new() { { 0, new Point(47, 1) }, { 1, new Point(49, 1) }, { 2, new Point(51, 1) }, { 3, new Point(47, 20) }, { 4, new Point(49, 20) }, { 5, new Point(51, 20) } };
            Dictionary<int, Point> _pointCarViolateContent = new() { { 0, new Point(47, 6) }, { 1, new Point(49, 6) }, { 2, new Point(51, 6) }, { 3, new Point(47, 25) }, { 4, new Point(49, 25) }, { 5, new Point(51, 25) } };
            Dictionary<int, Point> _pointCarViolatePlace = new() { { 0, new Point(47, 15) }, { 1, new Point(49, 15) }, { 2, new Point(51, 15) }, { 3, new Point(47, 34) }, { 4, new Point(49, 34) }, { 5, new Point(51, 34) } };
            List<StaffCarViolateVo> listStaffCarViolateVo = new();
            try {
                listStaffCarViolateVo = _staffCarViolateDao.SelectOneStaffCarViolateMaster(_staffCode);
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
            int countStaffCarViolateVo = 0;
            foreach (StaffCarViolateVo staffCarViolateVo in listStaffCarViolateVo.OrderByDescending(x => x.CarViolateDate)) {
                sheetView.Cells[_pointCarViolateDate[countStaffCarViolateVo].X, _pointCarViolateDate[countStaffCarViolateVo].Y].Value = staffCarViolateVo.CarViolateDate.Date != _defaultDateTime.Date ? staffCarViolateVo.CarViolateDate.Date : null;
                sheetView.Cells[_pointCarViolateContent[countStaffCarViolateVo].X, _pointCarViolateContent[countStaffCarViolateVo].Y].Text = staffCarViolateVo.CarViolateContent;
                sheetView.Cells[_pointCarViolatePlace[countStaffCarViolateVo].X, _pointCarViolatePlace[countStaffCarViolateVo].Y].Text = staffCarViolateVo.CarViolatePlace;
                countStaffCarViolateVo++;
                if (countStaffCarViolateVo > 5)
                    break;
            }
            /*
             * 教育
             */
            Dictionary<int, Point> _pointEducateDate = new() { { 0, new Point(57, 1) }, { 1, new Point(59, 1) }, { 2, new Point(61, 1) }, { 3, new Point(57, 20) }, { 4, new Point(59, 20) }, { 5, new Point(61, 20) } };
            Dictionary<int, Point> _pointEducateName = new() { { 0, new Point(57, 6) }, { 1, new Point(59, 6) }, { 2, new Point(61, 6) }, { 3, new Point(57, 25) }, { 4, new Point(59, 25) }, { 5, new Point(61, 25) } };
            List<StaffEducateVo> listStaffEducateVo = new();
            try {
                listStaffEducateVo = _staffEducateDao.SelectOneStaffEducateMaster(_staffCode);
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
            int countHStaffEducateVo = 0;
            foreach (StaffEducateVo staffEducateVo in listStaffEducateVo.OrderByDescending(x => x.EducateDate)) {
                sheetView.Cells[_pointEducateDate[countHStaffEducateVo].X, _pointEducateDate[countHStaffEducateVo].Y].Value = staffEducateVo.EducateDate.Date != _defaultDateTime.Date ? staffEducateVo.EducateDate.Date : null;
                sheetView.Cells[_pointEducateName[countHStaffEducateVo].X, _pointEducateName[countHStaffEducateVo].Y].Text = staffEducateVo.EducateName;
                countHStaffEducateVo++;
                if (countHStaffEducateVo > 5)
                    break;
            }
            /*
             * 適正診断
             */
            Dictionary<int, Point> _pointProperKind = new() { { 0, new Point(65, 3) }, { 1, new Point(67, 3) }, { 2, new Point(69, 3) } };
            Dictionary<int, Point> _pointProperDate = new() { { 0, new Point(65, 11) }, { 1, new Point(67, 11) }, { 2, new Point(69, 11) } };
            Dictionary<int, Point> _pointProperNote = new() { { 0, new Point(65, 16) }, { 1, new Point(67, 16) }, { 2, new Point(69, 16) } };
            List<StaffProperVo> listStaffProperVo = new();
            try {
                listStaffProperVo = _staffProperDao.SelectOneStaffProperMaster(_staffCode);
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
            int countHStaffProperVo = 0;
            foreach (StaffProperVo staffProperVo in listStaffProperVo.OrderByDescending(x => x.ProperDate)) {
                sheetView.Cells[_pointProperKind[countHStaffProperVo].X, _pointProperKind[countHStaffProperVo].Y].Text = staffProperVo.ProperKind;
                sheetView.Cells[_pointProperDate[countHStaffProperVo].X, _pointProperDate[countHStaffProperVo].Y].Value = staffProperVo.ProperDate.Date != _defaultDateTime.Date ? staffProperVo.ProperDate.Date : null;
                sheetView.Cells[_pointProperNote[countHStaffProperVo].X, _pointProperNote[countHStaffProperVo].Y].Text = staffProperVo.ProperNote;
                countHStaffProperVo++;
                if (countHStaffProperVo > 2)
                    break;
            }
            /*
             * 賞罰・譴責
             */
            Dictionary<int, Point> _pointPunishmentDate = new() { { 0, new Point(71, 3) }, { 1, new Point(71, 21) }, { 2, new Point(73, 3) }, { 3, new Point(73, 21) } };
            Dictionary<int, Point> _pointPunishmentNote = new() { { 0, new Point(71, 9) }, { 1, new Point(71, 27) }, { 2, new Point(73, 9) }, { 3, new Point(73, 27) } };
            List<StaffPunishmentVo> listStaffPunishmentVo = new();
            try {
                listStaffPunishmentVo = _staffPunishmentDao.SelectOneStaffPunishmentMaster(_staffCode);
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
            int countStaffPunishmentVo = 0;
            foreach (StaffPunishmentVo staffPunishmentVo in listStaffPunishmentVo.OrderByDescending(x => x.PunishmentDate)) {
                sheetView.Cells[_pointPunishmentDate[countStaffPunishmentVo].X, _pointPunishmentDate[countStaffPunishmentVo].Y].Value = staffPunishmentVo.PunishmentDate.Date != _defaultDateTime.Date ? staffPunishmentVo.PunishmentDate.Date : null;
                sheetView.Cells[_pointPunishmentNote[countStaffPunishmentVo].X, _pointPunishmentNote[countStaffPunishmentVo].Y].Text = staffPunishmentVo.PunishmentNote;
                countStaffPunishmentVo++;
                if (countStaffPunishmentVo > 3)
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        private void InitializeSpreadStaffRegisterHead(SheetView sheetView) {
            //表面
            this.SpreadStaffRegisterHead.SuspendLayout();
            sheetView.Cells[0, 32].Text = "";//印刷日時
            sheetView.Cells[2, 11].Text = "";//初任
            sheetView.Cells[3, 11].Text = "";//適齢
            sheetView.Cells[4, 11].Text = "";//健診
            sheetView.Cells[10, 5].Text = "";//ふりがな
            sheetView.Cells[11, 5].Text = "";//氏名
            sheetView.Cells[11, 18].Text = "";//性別
            sheetView.Cells[11, 20].Text = "";//生年月日
            sheetView.Cells[11, 26].Text = "";//雇用年月日
            sheetView.Cells[13, 5].Text = "";//現住所
            sheetView.Cells[15, 5].Text = "";//変更後住所
            sheetView.Cells[17, 7].Text = "";//電話番号
            sheetView.Cells[17, 21].Text = "";//携帯電話
            sheetView.Cells[10, 32].Value = null;//写真
            sheetView.Cells[19, 35].Text = "";//血液型
            sheetView.Cells[21, 9].Text = "";//運転者に選任された日
            sheetView.Cells[23, 9].Text = "";//運転者でなくなった日
            sheetView.Cells[25, 4].Text = "";//運転者でなくなった日　理由
            sheetView.Cells[27, 7].Text = "";//免許証番号
            sheetView.Cells[27, 17].Text = "";//条件等
            sheetView.Cells[31, 3].Text = "";//免許証の種類/取得日1
            sheetView.Cells[31, 27].Text = "";//有効期限1
            sheetView.Cells[33, 3].Text = "";//免許証の種類/取得日2
            sheetView.Cells[33, 27].Text = "";//有効期限2
            sheetView.Cells[35, 3].Text = "";//免許証の種類/取得日3
            sheetView.Cells[35, 27].Text = "";//有効期限3
            sheetView.Cells[37, 3].Text = "";//免許証の種類/取得日4
            sheetView.Cells[37, 27].Text = "";//有効期限4
            sheetView.Cells[39, 3].Text = "";//免許証の種類/取得日5
            sheetView.Cells[39, 27].Text = "";//有効期限5
            sheetView.Cells[41, 3].Text = "";//履歴日時1
            sheetView.Cells[41, 11].Text = "";//履歴内容1
            sheetView.Cells[43, 3].Text = "";//履歴日時2
            sheetView.Cells[43, 11].Text = "";//履歴内容2
            sheetView.Cells[45, 3].Text = "";//履歴日時3
            sheetView.Cells[45, 11].Text = "";//履歴内容3
            sheetView.Cells[41, 21].Text = "";//履歴日時4
            sheetView.Cells[41, 29].Text = "";//履歴内容4
            sheetView.Cells[43, 21].Text = "";//履歴日時5
            sheetView.Cells[43, 29].Text = "";//履歴内容5
            sheetView.Cells[45, 21].Text = "";//履歴日時6
            sheetView.Cells[45, 29].Text = "";//履歴内容6
            sheetView.Cells[51, 1].Text = "";//種類1
            sheetView.Cells[51, 12].Text = "";//積載量又は定員1
            sheetView.Cells[51, 20].Text = "";//経験期間1
            sheetView.Cells[51, 31].Text = "";//備考1
            sheetView.Cells[53, 1].Text = "";//種類2
            sheetView.Cells[53, 12].Text = "";//積載量又は定員2
            sheetView.Cells[53, 20].Text = "";//経験期間2
            sheetView.Cells[53, 31].Text = "";//備考2
            sheetView.Cells[55, 1].Text = "";//種類3
            sheetView.Cells[55, 12].Text = "";//積載量又は定員3
            sheetView.Cells[55, 20].Text = "";//経験期間3
            sheetView.Cells[55, 31].Text = "";//備考3
            sheetView.Cells[57, 1].Text = "";//種類4
            sheetView.Cells[57, 12].Text = "";//積載量又は定員4
            sheetView.Cells[57, 20].Text = "";//経験期間4
            sheetView.Cells[57, 31].Text = "";//備考4
            sheetView.Cells[60, 10].Text = "";//解雇又は退職の年月日
            sheetView.Cells[60, 17].Text = "";//解雇又は退職の理由
            sheetView.Cells[62, 15].Text = "";//死亡の場合の年月日
            sheetView.Cells[62, 22].Text = "";//死亡の場合の原因
            this.SpreadStaffRegisterHead.ResumeLayout(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        private void InitializeSpreadStaffRegisterTail(SheetView sheetView) {
            //裏面
            this.SpreadStaffRegisterTail.SuspendLayout();
            sheetView.Cells[3, 3].Text = "";//家族状況氏名1
            sheetView.Cells[3, 12].Text = "";//家族状況生年月日1
            sheetView.Cells[3, 18].Text = "";//家族状況続柄1
            sheetView.Cells[5, 3].Text = "";//家族状況氏名2
            sheetView.Cells[5, 12].Text = "";//家族状況生年月日2
            sheetView.Cells[5, 18].Text = "";//家族状況続柄2
            sheetView.Cells[7, 3].Text = "";//家族状況氏名3
            sheetView.Cells[7, 12].Text = "";//家族状況生年月日3
            sheetView.Cells[7, 18].Text = "";//家族状況続柄3
            sheetView.Cells[3, 21].Text = "";//家族状況氏名4
            sheetView.Cells[3, 30].Text = "";//家族状況生年月日4
            sheetView.Cells[3, 36].Text = "";//家族状況続柄4
            sheetView.Cells[5, 21].Text = "";//家族状況氏名5
            sheetView.Cells[5, 30].Text = "";//家族状況生年月日5
            sheetView.Cells[5, 36].Text = "";//家族状況続柄5
            sheetView.Cells[7, 21].Text = "";//家族状況氏名6
            sheetView.Cells[7, 30].Text = "";//家族状況生年月日6
            sheetView.Cells[7, 36].Text = "";//家族状況続柄6
            sheetView.Cells[9, 9].Text = "";//緊急時連絡方法　電話
            sheetView.Cells[9, 17].Text = "";//緊急時連絡方法　方法
            sheetView.Cells[14, 10].Text = "";//健康保険加入年月日
            sheetView.Cells[14, 17].Text = "";//健康保険の記号・番号
            sheetView.Cells[14, 28].Text = "";//健康保険の備考
            sheetView.Cells[16, 10].Text = "";//厚生年金保険加入年月日
            sheetView.Cells[16, 17].Text = "";//厚生年金保険の記号・番号
            sheetView.Cells[16, 28].Text = "";//厚生年金保険の備考
            sheetView.Cells[18, 10].Text = "";//雇用保険加入年月日
            sheetView.Cells[18, 17].Text = "";//雇用保険の記号・番号
            sheetView.Cells[18, 28].Text = "";//雇用保険の備考
            sheetView.Cells[20, 10].Text = "";//労災保険加入年月日
            sheetView.Cells[20, 17].Text = "";//労災保険の記号・番号
            sheetView.Cells[20, 28].Text = "";//労災保険の備考
            sheetView.Cells[25, 1].Text = "";//健康状態日付1
            sheetView.Cells[25, 10].Text = "";//健康状態備考1
            sheetView.Cells[27, 1].Text = "";//健康状態日付2
            sheetView.Cells[27, 10].Text = "";//健康状態備考2
            sheetView.Cells[29, 1].Text = "";//健康状態日付3
            sheetView.Cells[29, 10].Text = "";//健康状態備考3
            sheetView.Cells[31, 1].Text = "";//健康状態日付4
            sheetView.Cells[31, 10].Text = "";//健康状態備考4
            sheetView.Cells[33, 10].Text = "";//診断以外で気づいた点
            sheetView.Cells[39, 1].Text = "";//交通事故歴発生年月日1
            sheetView.Cells[39, 6].Text = "";//交通事故概要1
            sheetView.Cells[41, 1].Text = "";//交通事故歴発生年月日2
            sheetView.Cells[41, 6].Text = "";//交通事故概要2
            sheetView.Cells[43, 1].Text = "";//交通事故歴発生年月日3
            sheetView.Cells[43, 6].Text = "";//交通事故概要3
            sheetView.Cells[39, 20].Text = "";//交通事故歴発生年月日4
            sheetView.Cells[39, 25].Text = "";//交通事故概要4
            sheetView.Cells[41, 20].Text = "";//交通事故歴発生年月日5
            sheetView.Cells[41, 25].Text = "";//交通事故概要5
            sheetView.Cells[43, 20].Text = "";//交通事故歴発生年月日6
            sheetView.Cells[43, 25].Text = "";//交通事故概要6
            sheetView.Cells[47, 1].Text = "";//交通違反歴発生年月日1
            sheetView.Cells[47, 6].Text = "";//交通違反内容1
            sheetView.Cells[47, 15].Text = "";//交通違反場所1
            sheetView.Cells[49, 1].Text = "";//交通違反歴発生年月日2
            sheetView.Cells[49, 6].Text = "";//交通違反内容2
            sheetView.Cells[49, 15].Text = "";//交通違反場所2
            sheetView.Cells[51, 1].Text = "";//交通違反歴発生年月日3
            sheetView.Cells[51, 6].Text = "";//交通違反内容3
            sheetView.Cells[51, 15].Text = "";//交通違反場所3
            sheetView.Cells[47, 20].Text = "";//交通違反歴発生年月日4
            sheetView.Cells[47, 25].Text = "";//交通違反内容4
            sheetView.Cells[47, 34].Text = "";//交通違反場所4
            sheetView.Cells[49, 20].Text = "";//交通違反歴発生年月日5
            sheetView.Cells[49, 25].Text = "";//交通違反内容5
            sheetView.Cells[49, 34].Text = "";//交通違反場所5
            sheetView.Cells[51, 20].Text = "";//交通違反歴発生年月日6
            sheetView.Cells[51, 25].Text = "";//交通違反内容6
            sheetView.Cells[51, 34].Text = "";//交通違反場所6
            sheetView.Cells[57, 1].Text = "";//指導教育実施年月日1
            sheetView.Cells[57, 6].Text = "";//指導教育実施対象理由1
            sheetView.Cells[59, 1].Text = "";//指導教育実施年月日2
            sheetView.Cells[59, 6].Text = "";//指導教育実施対象理由2
            sheetView.Cells[61, 1].Text = "";//指導教育実施年月日3
            sheetView.Cells[61, 6].Text = "";//指導教育実施対象理由3
            sheetView.Cells[57, 20].Text = "";//指導教育実施年月日4
            sheetView.Cells[57, 25].Text = "";//指導教育実施対象理由4
            sheetView.Cells[59, 20].Text = "";//指導教育実施年月日5
            sheetView.Cells[59, 25].Text = "";//指導教育実施対象理由5
            sheetView.Cells[61, 20].Text = "";//指導教育実施年月日6
            sheetView.Cells[61, 25].Text = "";//指導教育実施対象理由6
            sheetView.Cells[65, 3].Text = "";//適性診断の種類1
            sheetView.Cells[65, 11].Text = "";//適性診断の実施年月日1
            sheetView.Cells[65, 16].Text = "";//適性診断の特記事項1
            sheetView.Cells[67, 3].Text = "";//適性診断の種類2
            sheetView.Cells[67, 11].Text = "";//適性診断の実施年月日2
            sheetView.Cells[67, 16].Text = "";//適性診断の特記事項2
            sheetView.Cells[69, 3].Text = "";//適性診断の種類3
            sheetView.Cells[69, 11].Text = "";//適性診断の実施年月日3
            sheetView.Cells[69, 16].Text = "";//適性診断の特記事項3
            sheetView.Cells[71, 3].Text = "";//賞罰実施年月日1
            sheetView.Cells[71, 9].Text = "";//賞罰内容1
            sheetView.Cells[73, 3].Text = "";//賞罰実施年月日2
            sheetView.Cells[73, 9].Text = "";//賞罰内容2
            sheetView.Cells[71, 21].Text = "";//賞罰実施年月日3
            sheetView.Cells[71, 27].Text = "";//賞罰内容3
            sheetView.Cells[73, 21].Text = "";//賞罰実施年月日4
            sheetView.Cells[73, 27].Text = "";//賞罰内容4
            this.SpreadStaffRegisterTail.ResumeLayout(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemPrintA4":
                    PrintDocument _printDocument;
                    _printDocument = new PrintDocument();
                    _printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);
                    // 出力先プリンタを指定します。
                    //printDocument.PrinterSettings.PrinterName = "(PrinterName)";
                    // 印刷部数を指定します。
                    _printDocument.PrinterSettings.Copies = 1;
                    // 両面印刷に設定します。
                    _printDocument.PrinterSettings.Duplex = Duplex.Vertical;
                    // カラー印刷に設定します。
                    _printDocument.PrinterSettings.DefaultPageSettings.Color = true;
                    _printDocument.Print();
                    break;
            }
        }

        /// <summary>
        /// printDocument_PrintPage
        /// </summary>
        private int curPageNumber = 0; // 現在のページ番号

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e) {
            if (curPageNumber == 0) {
                // 印刷ページ（1ページ目）の描画を行う
                Rectangle rectangle = new Rectangle(e.PageBounds.X, e.PageBounds.Y, e.PageBounds.Width, e.PageBounds.Height);
                // 使用するページ数を計算
                //int cnt = SpreadStaffRegisterHead.GetOwnerPrintPageCount(e.Graphics, rectangle, 0);
                // e.Graphicsへ出力
                SpreadStaffRegisterHead.OwnerPrintDraw(e.Graphics, rectangle, 0, 1);
                // 印刷継続を指定
                e.HasMorePages = true;
            } else {
                // 印刷ページ（2ページ目）の描画を行う
                Rectangle rectangle = new Rectangle(e.PageBounds.X, e.PageBounds.Y, e.PageBounds.Width, e.PageBounds.Height);
                // 使用するページ数を計算
                //int cnt = SpreadStaffRegisterHead.GetOwnerPrintPageCount(e.Graphics, rectangle, 0);
                // e.Graphicsへ出力
                SpreadStaffRegisterTail.OwnerPrintDraw(e.Graphics, rectangle, 0, 1);
                // 印刷終了を指定
                e.HasMorePages = false;
            }
            //ページ番号を繰り上げる
            curPageNumber++;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StaffPaper_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
