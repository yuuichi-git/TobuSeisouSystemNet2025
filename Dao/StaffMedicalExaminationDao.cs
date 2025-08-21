/*
 * 2024-02-03
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class StaffMedicalExaminationDao {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        public StaffMedicalExaminationDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// レコードの存在確認
        /// </summary>
        /// <param name="staffCode"></param>
        /// <returns>存在する:DateTime型 存在しない:_defaultDateTime</returns>
        public DateTime GetMedicalExaminationDate(int staffCode) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT MAX(MedicalExaminationDate) AS AA FROM H_StaffMedicalExaminationMaster WHERE StaffCode = " + staffCode + "";
            var data = sqlCommand.ExecuteScalar();
            if (data is not null) {
                return _defaultValue.GetDefaultValue<DateTime>(sqlCommand.ExecuteScalar());
            } else {
                return _defaultDateTime;
            }
        }

        /// <summary>
        /// SelectOneHStaffMedicalExaminationMaster
        /// </summary>
        /// <returns></returns>
        public List<StaffMedicalExaminationVo> SelectOneStaffMedicalExaminationMaster(int staffCode) {
            List<StaffMedicalExaminationVo> listStaffMedicalExaminationVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffCode," +
                                            "MedicalExaminationDate," +
                                            "MedicalInstitutionName," +
                                            "MedicalExaminationNote," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_StaffMedicalExaminationMaster " +
                                     "WHERE StaffCode = " + staffCode + "";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    StaffMedicalExaminationVo staffMedicalExaminationVo = new();
                    staffMedicalExaminationVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    staffMedicalExaminationVo.MedicalExaminationDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["MedicalExaminationDate"]);
                    staffMedicalExaminationVo.MedicalInstitutionName = _defaultValue.GetDefaultValue<string>(sqlDataReader["MedicalInstitutionName"]);
                    staffMedicalExaminationVo.MedicalExaminationNote = _defaultValue.GetDefaultValue<string>(sqlDataReader["MedicalExaminationNote"]);
                    staffMedicalExaminationVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    staffMedicalExaminationVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    staffMedicalExaminationVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    staffMedicalExaminationVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    staffMedicalExaminationVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    staffMedicalExaminationVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listStaffMedicalExaminationVo.Add(staffMedicalExaminationVo);
                }
            }
            return listStaffMedicalExaminationVo;
        }

        /// <summary>
        /// InsertOneHStaffMedicalExaminationMaster
        /// </summary>
        /// <param name="staffMedicalExaminationVo"></param>
        public void InsertOneStaffMedicalExaminationMaster(StaffMedicalExaminationVo staffMedicalExaminationVo) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_StaffMedicalExaminationMaster(StaffCode," +
                                                                                 "MedicalExaminationDate," +
                                                                                 "MedicalInstitutionName," +
                                                                                 "MedicalExaminationNote," +
                                                                                 "InsertPcName," +
                                                                                 "InsertYmdHms," +
                                                                                 "UpdatePcName," +
                                                                                 "UpdateYmdHms," +
                                                                                 "DeletePcName," +
                                                                                 "DeleteYmdHms," +
                                                                                 "DeleteFlag) " +
                                     "VALUES (" + _defaultValue.GetDefaultValue<int>(staffMedicalExaminationVo.StaffCode) + "," +
                                            "'" + _defaultValue.GetDefaultValue<DateTime>(staffMedicalExaminationVo.MedicalExaminationDate) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<string>(staffMedicalExaminationVo.MedicalInstitutionName) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<string>(staffMedicalExaminationVo.MedicalExaminationNote) + "'," +
                                            "'" + Environment.MachineName + "'," +
                                            "'" + DateTime.Now + "'," +
                                            "'" + "" + "'," +
                                            "'" + _defaultDateTime + "'," +
                                            "'" + "" + "'," +
                                            "'" + _defaultDateTime + "'," +
                                            "'False'" +
                                            ");";
            try {
                sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }
    }
}
