/*
 * 2023-11-16
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class StaffMasterDao {
        private readonly DefaultValue _defaultValue = new();
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
             * Vo
             */
            _connectionVo = connectionVo;
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
        public List<StaffMasterVo> SelectAllStaffMaster(int[]? sqlBelongs, int[]? sqlJobForm, int[]? sqlOccupation, bool? sqlRetirementFlag) {
            List<StaffMasterVo> listStaffMasterVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
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
        /// SelectOneStaffMaster
        /// Picture有
        /// DeleteFlag = False
        /// </summary>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public StaffMasterVo SelectOneStaffMaster(int staffCode) {
            StaffMasterVo staffMasterVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
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
                }
            }
            return staffMasterVo;
        }

        /// <summary>
        /// SQL Belongsを作成する
        /// </summary>
        /// <param name="sqlBelongs"></param>
        /// <returns></returns>
        private string CreateSqlBelongs(int[]? sqlBelongs) {
            string sql = string.Empty;
            if (sqlBelongs is not null) {
                string codes = string.Empty;
                int i = 0;
                foreach (int code in sqlBelongs) {
                    codes += string.Concat(code.ToString(), i < sqlBelongs.Length - 1 ? "," : "");
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
        private string CreateSqlJobForm(int[]? sqlJobForm) {
            string sql = string.Empty;
            if (sqlJobForm is not null) {
                string codes = string.Empty;
                int i = 0;
                foreach (int code in sqlJobForm) {
                    codes += string.Concat(code.ToString(), i < sqlJobForm.Length - 1 ? "," : "");
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
        private string CreateSqlOccupation(int[]? sqlOccupation) {
            string sql = string.Empty;
            if (sqlOccupation is not null) {
                string codes = string.Empty;
                int i = 0;
                foreach (int code in sqlOccupation) {
                    codes += string.Concat(code.ToString(), i < sqlOccupation.Length - 1 ? "," : "");
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
