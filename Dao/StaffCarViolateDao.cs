/*
 * 2024-02-03
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class StaffCarViolateDao {
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
        public StaffCarViolateDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// SelectOneHStaffCarViolateMaster
        /// </summary>
        /// <returns></returns>
        public List<StaffCarViolateVo> SelectOneStaffCarViolateMaster(int staffCode) {
            List<StaffCarViolateVo> listStaffCarViolateVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffCode," +
                                            "CarViolateDate," +
                                            "CarViolateContent," +
                                            "CarViolatePlace," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_StaffCarViolateMaster " +
                                     "WHERE StaffCode = " + staffCode + "";
            using (var sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    StaffCarViolateVo staffCarViolateVo = new();
                    staffCarViolateVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    staffCarViolateVo.CarViolateDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["CarViolateDate"]);
                    staffCarViolateVo.CarViolateContent = _defaultValue.GetDefaultValue<string>(sqlDataReader["CarViolateContent"]);
                    staffCarViolateVo.CarViolatePlace = _defaultValue.GetDefaultValue<string>(sqlDataReader["CarViolatePlace"]);
                    staffCarViolateVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    staffCarViolateVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    staffCarViolateVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    staffCarViolateVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    staffCarViolateVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    staffCarViolateVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    staffCarViolateVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listStaffCarViolateVo.Add(staffCarViolateVo);
                }
            }
            return listStaffCarViolateVo;
        }

        /// <summary>
        /// InsertOneHStaffCarViolateMaster
        /// </summary>
        /// <param name="staffCarViolateVo"></param>
        public void InsertOneStaffCarViolateMaster(StaffCarViolateVo staffCarViolateVo) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_StaffCarViolateMaster(StaffCode," +
                                                                         "CarViolateDate," +
                                                                         "CarViolateContent," +
                                                                         "CarViolatePlace," +
                                                                         "InsertPcName," +
                                                                         "InsertYmdHms," +
                                                                         "UpdatePcName," +
                                                                         "UpdateYmdHms," +
                                                                         "DeletePcName," +
                                                                         "DeleteYmdHms," +
                                                                         "DeleteFlag) " +
                                     "VALUES (" + _defaultValue.GetDefaultValue<int>(staffCarViolateVo.StaffCode) + "," +
                                            "'" + _defaultValue.GetDefaultValue<DateTime>(staffCarViolateVo.CarViolateDate) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<string>(staffCarViolateVo.CarViolateContent) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<string>(staffCarViolateVo.CarViolatePlace) + "'," +
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
