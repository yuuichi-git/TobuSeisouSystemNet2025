/*
 * 2023-11-16
 */
using System.Data;
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class StaffMasterDao {
        private readonly DefaultValue _defaultValue = new();
        /*
         * Dao
         */
        private readonly StaffHistoryDao _staffHistoryDao;
        private readonly StaffExperienceDao _staffExperienceDao;
        private readonly StaffFamilyDao _staffFamilyDao;
        private readonly StaffMedicalExaminationDao _staffMedicalExaminationDao;
        private readonly StaffCarViolateDao _staffCarViolateDao;
        private readonly StaffEducateDao _staffEducateDao;
        private readonly StaffProperDao _staffProperDao;
        private readonly StaffPunishmentDao _staffPunishmentDao;
        private readonly LicenseMasterDao _licenseMasterDao;
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        public StaffMasterDao(ConnectionVo connectionVo) {
            /*
             * Dao
             */
            _staffHistoryDao = new(connectionVo);
            _staffExperienceDao = new(connectionVo);
            _staffFamilyDao = new(connectionVo);
            _staffMedicalExaminationDao = new(connectionVo);
            _staffCarViolateDao = new(connectionVo);
            _staffEducateDao = new(connectionVo);
            _staffProperDao = new(connectionVo);
            _staffPunishmentDao = new(connectionVo);
            _licenseMasterDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// ExistenceHStaffMasterRecord
        /// true:該当レコードあり false:該当レコードなし
        /// </summary>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public bool ExistenceStaffMaster(int staffCode) {
            /*
             * 旧コード(SQLインジェクション対策なし)
             */
            //int count;
            //SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            //sqlCommand.CommandText = "SELECT COUNT(StaffCode) " +
            //                         "FROM H_StaffMaster " +
            //                         "WHERE StaffCode = " + staffCode;
            //try {
            //    count = (int)sqlCommand.ExecuteScalar();
            //} catch {
            //    throw;
            //}
            //return count != 0 ? true : false;

            using SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT COUNT(StaffCode) " +
                                     "FROM H_StaffMaster " +
                                     "WHERE StaffCode = @StaffCode";
            sqlCommand.Parameters.AddWithValue("@StaffCode", staffCode);
            object result = sqlCommand.ExecuteScalar();
            return Convert.ToInt32(result) > 0;
        }

        /// <summary>
        /// 新規staff_codeを採番
        /// 引数(staffCode)より小さい番号の中で最大の番号を取得する
        /// </summary>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public int GetStaffCode(int staffCode) {
            var sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT MAX(StaffCode) " +
                                     "FROM H_StaffMaster " +
                                     "WHERE StaffCode < " + staffCode;
            try {
                return (int)sqlCommand.ExecuteScalar();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// SelectAllStaffMaster
        /// Picture無
        /// DeleteFlag = False
        /// </summary>
        /// <param name="sqlBelongs"></param>
        /// <param name="sqlJobForm"></param>
        /// <param name="sqlOccupation"></param>
        /// <param name="sqlRetirementFlag">true:退職 false:在職 null:全て</param>
        /// <returns></returns>
        public List<StaffMasterVo> SelectAllStaffMaster(List<int>? sqlBelongs, List<int>? sqlJobForm, List<int>? sqlOccupation, bool? sqlRetirementFlag) {
            List<StaffMasterVo> listStaffMasterVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffCode," +
                                            "UnionCode," +
                                            "Belongs," +
                                            "VehicleDispatchTarget," +
                                            "JobForm," +
                                            "Occupation," +
                                            "NameKana," +
                                            "Name," +
                                            "DisplayName," +
                                            "OtherNameKana," +
                                            "OtherName," +
                                            "Gender," +
                                            "BirthDate," +
                                            "EmploymentDate," +
                                            "CurrentAddress," +
                                            "BeforeChangeAddress," +
                                            "Remarks," +
                                            "TelephoneNumber," +
                                            "CellphoneNumber," +
                                            //"Picture," +
                                            //"StampPicture," +
                                            "BloodType," +
                                            "SelectionDate," +
                                            "NotSelectionDate," +
                                            "NotSelectionReason," +
                                            "ContractFlag," +
                                            "ContractDate," +
                                            "RetirementFlag," +
                                            "RetirementDate," +
                                            "RetirementNote," +
                                            "DeathDate," +
                                            "DeathNote," +
                                            "LegalTwelveItemFlag," +
                                            "ToukanpoFlag," +
                                            "UrgentTelephoneNumber," +
                                            "UrgentTelephoneMethod," +
                                            "HealthInsuranceDate," +
                                            "HealthInsuranceNumber," +
                                            "HealthInsuranceNote," +
                                            "WelfarePensionDate," +
                                            "WelfarePensionNumber," +
                                            "WelfarePensionNote," +
                                            "EmploymentInsuranceDate," +
                                            "EmploymentInsuranceNumber," +
                                            "EmploymentInsuranceNote," +
                                            "WorkerAccidentInsuranceDate," +
                                            "WorkerAccidentInsuranceNumber," +
                                            "WorkerAccidentInsuranceNote," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_StaffMaster " +
                                     "WHERE DeleteFlag = 'false'" +
                                     CreateSqlBelongs(sqlBelongs) +
                                     CreateSqlJobForm(sqlJobForm) +
                                     CreateSqlOccupation(sqlOccupation) +
                                     CreateSqlRetirementFlag(sqlRetirementFlag);
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    StaffMasterVo staffMasterVo = new();
                    staffMasterVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    staffMasterVo.UnionCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["UnionCode"]);
                    staffMasterVo.Belongs = _defaultValue.GetDefaultValue<int>(sqlDataReader["Belongs"]);
                    staffMasterVo.VehicleDispatchTarget = _defaultValue.GetDefaultValue<bool>(sqlDataReader["VehicleDispatchTarget"]);
                    staffMasterVo.JobForm = _defaultValue.GetDefaultValue<int>(sqlDataReader["JobForm"]);
                    staffMasterVo.Occupation = _defaultValue.GetDefaultValue<int>(sqlDataReader["Occupation"]);
                    staffMasterVo.NameKana = _defaultValue.GetDefaultValue<string>(sqlDataReader["NameKana"]);
                    staffMasterVo.Name = _defaultValue.GetDefaultValue<string>(sqlDataReader["Name"]);
                    staffMasterVo.DisplayName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DisplayName"]);
                    staffMasterVo.OtherNameKana = _defaultValue.GetDefaultValue<string>(sqlDataReader["OtherNameKana"]);
                    staffMasterVo.OtherName = _defaultValue.GetDefaultValue<string>(sqlDataReader["OtherName"]);
                    staffMasterVo.Gender = _defaultValue.GetDefaultValue<string>(sqlDataReader["Gender"]);
                    staffMasterVo.BirthDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["BirthDate"]);
                    staffMasterVo.EmploymentDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["EmploymentDate"]);
                    staffMasterVo.CurrentAddress = _defaultValue.GetDefaultValue<string>(sqlDataReader["CurrentAddress"]);
                    staffMasterVo.BeforeChangeAddress = _defaultValue.GetDefaultValue<string>(sqlDataReader["BeforeChangeAddress"]);
                    staffMasterVo.Remarks = _defaultValue.GetDefaultValue<string>(sqlDataReader["Remarks"]);
                    staffMasterVo.TelephoneNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["TelephoneNumber"]);
                    staffMasterVo.CellphoneNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["CellphoneNumber"]);
                    //hStaffMasterVo.Picture = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture"]);
                    //hStaffMasterVo.StampPicture = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["StampPicture"]);
                    staffMasterVo.BloodType = _defaultValue.GetDefaultValue<string>(sqlDataReader["BloodType"]);
                    staffMasterVo.SelectionDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["SelectionDate"]);
                    staffMasterVo.NotSelectionDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["NotSelectionDate"]);
                    staffMasterVo.NotSelectionReason = _defaultValue.GetDefaultValue<string>(sqlDataReader["NotSelectionReason"]);
                    staffMasterVo.ContractFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["ContractFlag"]);
                    staffMasterVo.ContractDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["ContractDate"]);
                    staffMasterVo.RetirementFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["RetirementFlag"]);
                    staffMasterVo.RetirementDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["RetirementDate"]);
                    staffMasterVo.RetirementNote = _defaultValue.GetDefaultValue<string>(sqlDataReader["RetirementNote"]);
                    staffMasterVo.DeathDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeathDate"]);
                    staffMasterVo.DeathNote = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeathNote"]);
                    staffMasterVo.LegalTwelveItemFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["LegalTwelveItemFlag"]);
                    staffMasterVo.ToukanpoFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["ToukanpoFlag"]);
                    staffMasterVo.UrgentTelephoneNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["UrgentTelephoneNumber"]);
                    staffMasterVo.UrgentTelephoneMethod = _defaultValue.GetDefaultValue<string>(sqlDataReader["UrgentTelephoneMethod"]);
                    staffMasterVo.HealthInsuranceDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["HealthInsuranceDate"]);
                    staffMasterVo.HealthInsuranceNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["HealthInsuranceNumber"]);
                    staffMasterVo.HealthInsuranceNote = _defaultValue.GetDefaultValue<string>(sqlDataReader["HealthInsuranceNote"]);
                    staffMasterVo.WelfarePensionDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["WelfarePensionDate"]);
                    staffMasterVo.WelfarePensionNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["WelfarePensionNumber"]);
                    staffMasterVo.WelfarePensionNote = _defaultValue.GetDefaultValue<string>(sqlDataReader["WelfarePensionNote"]);
                    staffMasterVo.EmploymentInsuranceDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["EmploymentInsuranceDate"]);
                    staffMasterVo.EmploymentInsuranceNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["EmploymentInsuranceNumber"]);
                    staffMasterVo.EmploymentInsuranceNote = _defaultValue.GetDefaultValue<string>(sqlDataReader["EmploymentInsuranceNote"]);
                    staffMasterVo.WorkerAccidentInsuranceDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["WorkerAccidentInsuranceDate"]);
                    staffMasterVo.WorkerAccidentInsuranceNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["WorkerAccidentInsuranceNumber"]);
                    staffMasterVo.WorkerAccidentInsuranceNote = _defaultValue.GetDefaultValue<string>(sqlDataReader["WorkerAccidentInsuranceNote"]);
                    staffMasterVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    staffMasterVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    staffMasterVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    staffMasterVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    staffMasterVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    staffMasterVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    staffMasterVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listStaffMasterVo.Add(staffMasterVo);
                }
            }
            return listStaffMasterVo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public StaffMasterVo SelectOneStaffMaster(int staffCode) {
            StaffMasterVo staffMasterVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffCode," +
                                            "UnionCode," +
                                            "Belongs," +
                                            "VehicleDispatchTarget," +
                                            "JobForm," +
                                            "Occupation," +
                                            "NameKana," +
                                            "Name," +
                                            "DisplayName," +
                                            "OtherNameKana," +
                                            "OtherName," +
                                            "Gender," +
                                            "BirthDate," +
                                            "EmploymentDate," +
                                            "CurrentAddress," +
                                            "BeforeChangeAddress," +
                                            "Remarks," +
                                            "TelephoneNumber," +
                                            "CellphoneNumber," +
                                            "Picture," +
                                            "StampPicture," +
                                            "BloodType," +
                                            "SelectionDate," +
                                            "NotSelectionDate," +
                                            "NotSelectionReason," +
                                            "ContractFlag," +
                                            "ContractDate," +
                                            "RetirementFlag," +
                                            "RetirementDate," +
                                            "RetirementNote," +
                                            "DeathDate," +
                                            "DeathNote," +
                                            "LegalTwelveItemFlag," +
                                            "ToukanpoFlag," +
                                            "UrgentTelephoneNumber," +
                                            "UrgentTelephoneMethod," +
                                            "HealthInsuranceDate," +
                                            "HealthInsuranceNumber," +
                                            "HealthInsuranceNote," +
                                            "WelfarePensionDate," +
                                            "WelfarePensionNumber," +
                                            "WelfarePensionNote," +
                                            "EmploymentInsuranceDate," +
                                            "EmploymentInsuranceNumber," +
                                            "EmploymentInsuranceNote," +
                                            "WorkerAccidentInsuranceDate," +
                                            "WorkerAccidentInsuranceNumber," +
                                            "WorkerAccidentInsuranceNote," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_StaffMaster " +
                                     "WHERE StaffCode = " + staffCode + "";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    staffMasterVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    staffMasterVo.UnionCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["UnionCode"]);
                    staffMasterVo.Belongs = _defaultValue.GetDefaultValue<int>(sqlDataReader["Belongs"]);
                    staffMasterVo.VehicleDispatchTarget = _defaultValue.GetDefaultValue<bool>(sqlDataReader["VehicleDispatchTarget"]);
                    staffMasterVo.JobForm = _defaultValue.GetDefaultValue<int>(sqlDataReader["JobForm"]);
                    staffMasterVo.Occupation = _defaultValue.GetDefaultValue<int>(sqlDataReader["Occupation"]);
                    staffMasterVo.NameKana = _defaultValue.GetDefaultValue<string>(sqlDataReader["NameKana"]);
                    staffMasterVo.Name = _defaultValue.GetDefaultValue<string>(sqlDataReader["Name"]);
                    staffMasterVo.DisplayName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DisplayName"]);
                    staffMasterVo.OtherNameKana = _defaultValue.GetDefaultValue<string>(sqlDataReader["OtherNameKana"]);
                    staffMasterVo.OtherName = _defaultValue.GetDefaultValue<string>(sqlDataReader["OtherName"]);
                    staffMasterVo.Gender = _defaultValue.GetDefaultValue<string>(sqlDataReader["Gender"]);
                    staffMasterVo.BirthDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["BirthDate"]);
                    staffMasterVo.EmploymentDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["EmploymentDate"]);
                    staffMasterVo.CurrentAddress = _defaultValue.GetDefaultValue<string>(sqlDataReader["CurrentAddress"]);
                    staffMasterVo.BeforeChangeAddress = _defaultValue.GetDefaultValue<string>(sqlDataReader["BeforeChangeAddress"]);
                    staffMasterVo.Remarks = _defaultValue.GetDefaultValue<string>(sqlDataReader["Remarks"]);
                    staffMasterVo.TelephoneNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["TelephoneNumber"]);
                    staffMasterVo.CellphoneNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["CellphoneNumber"]);
                    staffMasterVo.Picture = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture"]);
                    staffMasterVo.StampPicture = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["StampPicture"]);
                    staffMasterVo.BloodType = _defaultValue.GetDefaultValue<string>(sqlDataReader["BloodType"]);
                    staffMasterVo.SelectionDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["SelectionDate"]);
                    staffMasterVo.NotSelectionDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["NotSelectionDate"]);
                    staffMasterVo.NotSelectionReason = _defaultValue.GetDefaultValue<string>(sqlDataReader["NotSelectionReason"]);
                    staffMasterVo.LicenseMasterVo = _licenseMasterDao.SelectOneLicenseMaster(_defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]));
                    staffMasterVo.ListHStaffHistoryVo = _staffHistoryDao.SelectOneStaffHistoryMaster(_defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]));
                    staffMasterVo.ListHStaffExperienceVo = _staffExperienceDao.SelectOneStaffExperienceMaster(_defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]));
                    staffMasterVo.ContractFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["ContractFlag"]);
                    staffMasterVo.ContractDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["ContractDate"]);
                    staffMasterVo.RetirementFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["RetirementFlag"]);
                    staffMasterVo.RetirementDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["RetirementDate"]);
                    staffMasterVo.RetirementNote = _defaultValue.GetDefaultValue<string>(sqlDataReader["RetirementNote"]);
                    staffMasterVo.DeathDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeathDate"]);
                    staffMasterVo.DeathNote = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeathNote"]);
                    staffMasterVo.LegalTwelveItemFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["LegalTwelveItemFlag"]);
                    staffMasterVo.ToukanpoFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["ToukanpoFlag"]);
                    staffMasterVo.ListHStaffFamilyVo = _staffFamilyDao.SelectOneStaffFamilyMaster(_defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]));
                    staffMasterVo.UrgentTelephoneNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["UrgentTelephoneNumber"]);
                    staffMasterVo.UrgentTelephoneMethod = _defaultValue.GetDefaultValue<string>(sqlDataReader["UrgentTelephoneMethod"]);
                    staffMasterVo.HealthInsuranceDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["HealthInsuranceDate"]);
                    staffMasterVo.HealthInsuranceNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["HealthInsuranceNumber"]);
                    staffMasterVo.HealthInsuranceNote = _defaultValue.GetDefaultValue<string>(sqlDataReader["HealthInsuranceNote"]);
                    staffMasterVo.WelfarePensionDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["WelfarePensionDate"]);
                    staffMasterVo.WelfarePensionNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["WelfarePensionNumber"]);
                    staffMasterVo.WelfarePensionNote = _defaultValue.GetDefaultValue<string>(sqlDataReader["WelfarePensionNote"]);
                    staffMasterVo.EmploymentInsuranceDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["EmploymentInsuranceDate"]);
                    staffMasterVo.EmploymentInsuranceNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["EmploymentInsuranceNumber"]);
                    staffMasterVo.EmploymentInsuranceNote = _defaultValue.GetDefaultValue<string>(sqlDataReader["EmploymentInsuranceNote"]);
                    staffMasterVo.WorkerAccidentInsuranceDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["WorkerAccidentInsuranceDate"]);
                    staffMasterVo.WorkerAccidentInsuranceNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["WorkerAccidentInsuranceNumber"]);
                    staffMasterVo.WorkerAccidentInsuranceNote = _defaultValue.GetDefaultValue<string>(sqlDataReader["WorkerAccidentInsuranceNote"]);
                    staffMasterVo.ListHStaffMedicalExaminationVo = _staffMedicalExaminationDao.SelectOneStaffMedicalExaminationMaster(_defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]));
                    staffMasterVo.ListHStaffCarViolateVo = _staffCarViolateDao.SelectOneStaffCarViolateMaster(_defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]));
                    staffMasterVo.ListHStaffEducateVo = _staffEducateDao.SelectOneStaffEducateMaster(_defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]));
                    staffMasterVo.ListHStaffProperVo = _staffProperDao.SelectOneStaffProperMaster(_defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]));
                    staffMasterVo.ListHStaffPunishmentVo = _staffPunishmentDao.SelectOneStaffPunishmentMaster(_defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]));
                    staffMasterVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    staffMasterVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    staffMasterVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    staffMasterVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    staffMasterVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    staffMasterVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    staffMasterVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                }
            }
            return staffMasterVo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffMasterVo"></param>
        public void InsertOneStaffMaster(StaffMasterVo staffMasterVo) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_StaffMaster(StaffCode," +
                                                               "UnionCode," +
                                                               "Belongs," +
                                                               "VehicleDispatchTarget," +
                                                               "JobForm," +
                                                               "Occupation," +
                                                               "NameKana," +
                                                               "Name," +
                                                               "DisplayName," +
                                                               "OtherNameKana," +
                                                               "OtherName," +
                                                               "Gender," +
                                                               "BirthDate," +
                                                               "EmploymentDate," +
                                                               "CurrentAddress," +
                                                               "BeforeChangeAddress," +
                                                               "Remarks," +
                                                               "TelephoneNumber," +
                                                               "CellphoneNumber," +
                                                               "Picture," +
                                                               "StampPicture," +
                                                               "BloodType," +
                                                               "SelectionDate," +
                                                               "NotSelectionDate," +
                                                               "NotSelectionReason," +
                                                               "ContractFlag," +
                                                               "ContractDate," +
                                                               "RetirementFlag," +
                                                               "RetirementDate," +
                                                               "RetirementNote," +
                                                               "DeathDate," +
                                                               "DeathNote," +
                                                               "LegalTwelveItemFlag," +
                                                               "ToukanpoFlag," +
                                                               "UrgentTelephoneNumber," +
                                                               "UrgentTelephoneMethod," +
                                                               "HealthInsuranceDate," +
                                                               "HealthInsuranceNumber," +
                                                               "HealthInsuranceNote," +
                                                               "WelfarePensionDate," +
                                                               "WelfarePensionNumber," +
                                                               "WelfarePensionNote," +
                                                               "EmploymentInsuranceDate," +
                                                               "EmploymentInsuranceNumber," +
                                                               "EmploymentInsuranceNote," +
                                                               "WorkerAccidentInsuranceDate," +
                                                               "WorkerAccidentInsuranceNumber," +
                                                               "WorkerAccidentInsuranceNote," +
                                                               "InsertPcName," +
                                                               "InsertYmdHms," +
                                                               "UpdatePcName," +
                                                               "UpdateYmdHms," +
                                                               "DeletePcName," +
                                                               "DeleteYmdHms," +
                                                               "DeleteFlag) " +
                                     "VALUES (" + staffMasterVo.StaffCode + "," +
                                             "" + staffMasterVo.UnionCode + "," +
                                             "" + staffMasterVo.Belongs + "," +
                                            "'" + staffMasterVo.VehicleDispatchTarget + "'," +
                                             "" + staffMasterVo.JobForm + "," +
                                             "" + staffMasterVo.Occupation + "," +
                                            "'" + staffMasterVo.NameKana + "'," +
                                            "'" + staffMasterVo.Name + "'," +
                                            "'" + staffMasterVo.DisplayName + "'," +
                                            "'" + staffMasterVo.OtherNameKana + "'," +
                                            "'" + staffMasterVo.OtherName + "'," +
                                            "'" + staffMasterVo.Gender + "'," +
                                            "'" + staffMasterVo.BirthDate + "'," +
                                            "'" + staffMasterVo.EmploymentDate + "'," +
                                            "'" + staffMasterVo.CurrentAddress + "'," +
                                            "'" + staffMasterVo.BeforeChangeAddress + "'," +
                                            "'" + staffMasterVo.Remarks + "'," +
                                            "'" + staffMasterVo.TelephoneNumber + "'," +
                                            "'" + staffMasterVo.CellphoneNumber + "'," +
                                            "@member_picture," +
                                            "@member_stampPicture," +
                                            "'" + staffMasterVo.BloodType + "'," +
                                            "'" + staffMasterVo.SelectionDate + "'," +
                                            "'" + staffMasterVo.NotSelectionDate + "'," +
                                            "'" + staffMasterVo.NotSelectionReason + "'," +
                                            "'" + staffMasterVo.ContractFlag + "'," +
                                            "'" + staffMasterVo.ContractDate + "'," +
                                            "'" + staffMasterVo.RetirementFlag + "'," +
                                            "'" + staffMasterVo.RetirementDate + "'," +
                                            "'" + staffMasterVo.RetirementNote + "'," +
                                            "'" + staffMasterVo.DeathDate + "'," +
                                            "'" + staffMasterVo.DeathNote + "'," +
                                            "'" + staffMasterVo.LegalTwelveItemFlag + "'," +
                                            "'" + staffMasterVo.ToukanpoFlag + "'," +
                                            "'" + staffMasterVo.UrgentTelephoneNumber + "'," +
                                            "'" + staffMasterVo.UrgentTelephoneMethod + "'," +
                                            "'" + staffMasterVo.HealthInsuranceDate + "'," +
                                            "'" + staffMasterVo.HealthInsuranceNumber + "'," +
                                            "'" + staffMasterVo.HealthInsuranceNote + "'," +
                                            "'" + staffMasterVo.WelfarePensionDate + "'," +
                                            "'" + staffMasterVo.WelfarePensionNumber + "'," +
                                            "'" + staffMasterVo.WelfarePensionNote + "'," +
                                            "'" + staffMasterVo.EmploymentInsuranceDate + "'," +
                                            "'" + staffMasterVo.EmploymentInsuranceNumber + "'," +
                                            "'" + staffMasterVo.EmploymentInsuranceNote + "'," +
                                            "'" + staffMasterVo.WorkerAccidentInsuranceDate + "'," +
                                            "'" + staffMasterVo.WorkerAccidentInsuranceNumber + "'," +
                                            "'" + staffMasterVo.WorkerAccidentInsuranceNote + "'," +
                                            "'" + staffMasterVo.InsertPcName + "'," +
                                            "'" + staffMasterVo.InsertYmdHms + "'," +
                                            "'" + staffMasterVo.UpdatePcName + "'," +
                                            "'" + staffMasterVo.UpdateYmdHms + "'," +
                                            "'" + staffMasterVo.DeletePcName + "'," +
                                            "'" + staffMasterVo.DeleteYmdHms + "'," +
                                             "'false'" +
                                             ");";
            try {
                sqlCommand.Parameters.Add("@member_picture", SqlDbType.Image, staffMasterVo.Picture.Length).Value = staffMasterVo.Picture;
                sqlCommand.Parameters.Add("@member_stampPicture", SqlDbType.Image, staffMasterVo.StampPicture.Length).Value = staffMasterVo.StampPicture;
                sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffMasterVo"></param>
        public void UpdateOneStaffMaster(StaffMasterVo staffMasterVo) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_StaffMaster " +
                                     "SET StaffCode = " + staffMasterVo.StaffCode + "," +
                                         "UnionCode = " + staffMasterVo.UnionCode + "," +
                                         "Belongs = " + staffMasterVo.Belongs + "," +
                                         "VehicleDispatchTarget = '" + staffMasterVo.VehicleDispatchTarget + "'," +
                                         "JobForm = " + staffMasterVo.JobForm + "," +
                                         "Occupation = " + staffMasterVo.Occupation + "," +
                                         "NameKana = '" + staffMasterVo.NameKana + "'," +
                                         "Name = '" + staffMasterVo.Name + "'," +
                                         "DisplayName = '" + staffMasterVo.DisplayName + "'," +
                                         "OtherNameKana = '" + staffMasterVo.OtherNameKana + "'," +
                                         "OtherName = '" + staffMasterVo.OtherName + "'," +
                                         "Gender = '" + staffMasterVo.Gender + "'," +
                                         "BirthDate = '" + staffMasterVo.BirthDate + "'," +
                                         "EmploymentDate = '" + staffMasterVo.EmploymentDate + "'," +
                                         "CurrentAddress = '" + staffMasterVo.CurrentAddress + "'," +
                                         "BeforeChangeAddress = '" + staffMasterVo.BeforeChangeAddress + "'," +
                                         "Remarks = '" + staffMasterVo.Remarks + "'," +
                                         "TelephoneNumber = '" + staffMasterVo.TelephoneNumber + "'," +
                                         "CellphoneNumber = '" + staffMasterVo.CellphoneNumber + "'," +
                                         "Picture = @member_picture," +
                                         "StampPicture = @member_stampPicture," +
                                         "BloodType = '" + staffMasterVo.BloodType + "'," +
                                         "SelectionDate = '" + staffMasterVo.SelectionDate + "'," +
                                         "NotSelectionDate = '" + staffMasterVo.NotSelectionDate + "'," +
                                         "NotSelectionReason = '" + staffMasterVo.NotSelectionReason + "'," +
                                         "ContractFlag = '" + staffMasterVo.ContractFlag + "'," +
                                         "ContractDate = '" + staffMasterVo.ContractDate + "'," +
                                         "RetirementFlag = '" + staffMasterVo.RetirementFlag + "'," +
                                         "RetirementDate = '" + staffMasterVo.RetirementDate + "'," +
                                         "RetirementNote = '" + staffMasterVo.RetirementNote + "'," +
                                         "DeathDate = '" + staffMasterVo.DeathDate + "'," +
                                         "DeathNote = '" + staffMasterVo.DeathNote + "'," +
                                         "LegalTwelveItemFlag = '" + staffMasterVo.LegalTwelveItemFlag + "'," +
                                         "ToukanpoFlag = '" + staffMasterVo.ToukanpoFlag + "'," +
                                         "UrgentTelephoneNumber = '" + staffMasterVo.UrgentTelephoneNumber + "'," +
                                         "UrgentTelephoneMethod = '" + staffMasterVo.UrgentTelephoneMethod + "'," +
                                         "HealthInsuranceDate = '" + staffMasterVo.HealthInsuranceDate + "'," +
                                         "HealthInsuranceNumber = '" + staffMasterVo.HealthInsuranceNumber + "'," +
                                         "HealthInsuranceNote = '" + staffMasterVo.HealthInsuranceNote + "'," +
                                         "WelfarePensionDate = '" + staffMasterVo.WelfarePensionDate + "'," +
                                         "WelfarePensionNumber = '" + staffMasterVo.WelfarePensionNumber + "'," +
                                         "WelfarePensionNote = '" + staffMasterVo.WelfarePensionNote + "'," +
                                         "EmploymentInsuranceDate = '" + staffMasterVo.EmploymentInsuranceDate + "'," +
                                         "EmploymentInsuranceNumber = '" + staffMasterVo.EmploymentInsuranceNumber + "'," +
                                         "EmploymentInsuranceNote = '" + staffMasterVo.EmploymentInsuranceNote + "'," +
                                         "WorkerAccidentInsuranceDate = '" + staffMasterVo.WorkerAccidentInsuranceDate + "'," +
                                         "WorkerAccidentInsuranceNumber = '" + staffMasterVo.WorkerAccidentInsuranceNumber + "'," +
                                         "WorkerAccidentInsuranceNote = '" + staffMasterVo.WorkerAccidentInsuranceNote + "'," +
                                         "UpdatePcName = '" + staffMasterVo.UpdatePcName + "'," +
                                         "UpdateYmdHms = '" + staffMasterVo.UpdateYmdHms + "' " +
                                     "WHERE StaffCode = " + staffMasterVo.StaffCode;
            try {
                sqlCommand.Parameters.Add("@member_picture", SqlDbType.Image, staffMasterVo.Picture.Length).Value = staffMasterVo.Picture;
                sqlCommand.Parameters.Add("@member_stampPicture", SqlDbType.Image, staffMasterVo.StampPicture.Length).Value = staffMasterVo.StampPicture;
                sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// SQL Belongsを作成する
        /// </summary>
        /// <param name="sqlBelongs"></param>
        /// <returns></returns>
        private string CreateSqlBelongs(List<int>? sqlBelongs) {
            string sql = string.Empty;
            if (sqlBelongs is not null) {
                string codes = string.Empty;
                int i = 0;
                foreach (int code in sqlBelongs) {
                    codes += string.Concat(code.ToString(), i < sqlBelongs.Count - 1 ? "," : "");
                    i++;
                }
                sql = " AND Belongs IN (" + codes + ")";
                return sql;
            } else {
                return sql;
            }
        }

        /// <summary>
        /// SQL JobFormを作成する
        /// </summary>
        /// <param name="sqlJobForm"></param>
        /// <returns></returns>
        private string CreateSqlJobForm(List<int>? sqlJobForm) {
            string sql = string.Empty;
            if (sqlJobForm is not null) {
                string codes = string.Empty;
                int i = 0;
                foreach (int code in sqlJobForm) {
                    codes += string.Concat(code.ToString(), i < sqlJobForm.Count - 1 ? "," : "");
                    i++;
                }
                sql = " AND JobForm IN (" + codes + ")";
                return sql;
            } else {
                return sql;
            }
        }

        /// <summary>
        /// SQL Occupationを作成する
        /// </summary>
        /// <param name="sqlOccupation"></param>
        /// <returns></returns>
        private string CreateSqlOccupation(List<int>? sqlOccupation) {
            string sql = string.Empty;
            if (sqlOccupation is not null) {
                string codes = string.Empty;
                int i = 0;
                foreach (int code in sqlOccupation) {
                    codes += string.Concat(code.ToString(), i < sqlOccupation.Count - 1 ? "," : "");
                    i++;
                }
                sql = " AND Occupation IN (" + codes + ")";
                return sql;
            } else {
                return sql;
            }
        }

        /// <summary>
        /// SQL RetirementFlagを作成する
        /// </summary>
        /// <param name="sqlRetirementFlag"></param>
        /// <returns></returns>
        private string CreateSqlRetirementFlag(bool? sqlRetirementFlag) {
            string sql = string.Empty;
            if (sqlRetirementFlag is not null) {
                if (sqlRetirementFlag == false) {
                    return " AND RetirementFlag = 'false'";
                } else {
                    return sql;
                }
            } else {
                return sql;
            }
        }
    }
}
