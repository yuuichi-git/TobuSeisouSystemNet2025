/*
 * 2024-02-03
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class StaffExperienceDao {
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
        public StaffExperienceDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// SelectOneHStaffExperienceMaster
        /// </summary>
        /// <returns></returns>
        public List<StaffExperienceVo> SelectOneStaffExperienceMaster(int staffCode) {
            List<StaffExperienceVo> listStaffExperienceVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffCode," +
                                            "ExperienceKind," +
                                            "ExperienceLoad," +
                                            "ExperienceDuration," +
                                            "ExperienceNote," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_StaffExperienceMaster " +
                                     "WHERE StaffCode = " + staffCode + "";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    StaffExperienceVo staffExperienceVo = new();
                    staffExperienceVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    staffExperienceVo.ExperienceKind = _defaultValue.GetDefaultValue<string>(sqlDataReader["ExperienceKind"]);
                    staffExperienceVo.ExperienceLoad = _defaultValue.GetDefaultValue<string>(sqlDataReader["ExperienceLoad"]);
                    staffExperienceVo.ExperienceDuration = _defaultValue.GetDefaultValue<string>(sqlDataReader["ExperienceDuration"]);
                    staffExperienceVo.ExperienceNote = _defaultValue.GetDefaultValue<string>(sqlDataReader["ExperienceNote"]);
                    staffExperienceVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    staffExperienceVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    staffExperienceVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    staffExperienceVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    staffExperienceVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    staffExperienceVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listStaffExperienceVo.Add(staffExperienceVo);
                }
            }
            return listStaffExperienceVo;
        }

        /// <summary>
        /// InsertOneHStaffExperienceMaster
        /// </summary>
        /// <param name="staffExperienceVo"></param>
        public void InsertOneStaffExperienceMaster(StaffExperienceVo staffExperienceVo) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_StaffExperienceMaster(StaffCode," +
                                                                         "ExperienceKind," +
                                                                         "ExperienceLoad," +
                                                                         "ExperienceDuration," +
                                                                         "ExperienceNote," +
                                                                         "InsertPcName," +
                                                                         "InsertYmdHms," +
                                                                         "UpdatePcName," +
                                                                         "UpdateYmdHms," +
                                                                         "DeletePcName," +
                                                                         "DeleteYmdHms," +
                                                                         "DeleteFlag) " +
                                     "VALUES (" + _defaultValue.GetDefaultValue<int>(staffExperienceVo.StaffCode) + "," +
                                            "'" + _defaultValue.GetDefaultValue<string>(staffExperienceVo.ExperienceKind) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<string>(staffExperienceVo.ExperienceLoad) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<string>(staffExperienceVo.ExperienceDuration) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<string>(staffExperienceVo.ExperienceNote) + "'," +
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
