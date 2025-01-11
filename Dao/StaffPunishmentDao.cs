/*
 * 2024-02-03
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class StaffPunishmentDao {
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
        public StaffPunishmentDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// SelectOneHStaffPunishmentMaster
        /// </summary>
        /// <returns></returns>
        public List<StaffPunishmentVo> SelectOneStaffPunishmentMaster(int staffCode) {
            List<StaffPunishmentVo> listStaffPunishmentVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffCode," +
                                            "PunishmentDate," +
                                            "PunishmentNote," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_StaffPunishmentMaster " +
                                     "WHERE StaffCode = " + staffCode + "";
            using (var sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    StaffPunishmentVo staffPunishmentVo = new();
                    staffPunishmentVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    staffPunishmentVo.PunishmentDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["PunishmentDate"]);
                    staffPunishmentVo.PunishmentNote = _defaultValue.GetDefaultValue<string>(sqlDataReader["PunishmentNote"]);
                    staffPunishmentVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    staffPunishmentVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    staffPunishmentVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    staffPunishmentVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    staffPunishmentVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    staffPunishmentVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    staffPunishmentVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listStaffPunishmentVo.Add(staffPunishmentVo);
                }
            }
            return listStaffPunishmentVo;
        }

        /// <summary>
        /// InsertOneHStaffPunishmentMasters
        /// </summary>
        /// <param name="staffPunishmentVo"></param>
        public void InsertOneStaffPunishmentMasters(StaffPunishmentVo staffPunishmentVo) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_StaffPunishmentMaster(StaffCode," +
                                                                         "PunishmentDate," +
                                                                         "PunishmentNote," +
                                                                         "InsertPcName," +
                                                                         "InsertYmdHms," +
                                                                         "UpdatePcName," +
                                                                         "UpdateYmdHms," +
                                                                         "DeletePcName," +
                                                                         "DeleteYmdHms," +
                                                                         "DeleteFlag) " +
                                     "VALUES (" + _defaultValue.GetDefaultValue<int>(staffPunishmentVo.StaffCode) + "," +
                                            "'" + _defaultValue.GetDefaultValue<DateTime>(staffPunishmentVo.PunishmentDate) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<string>(staffPunishmentVo.PunishmentNote) + "'," +
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
