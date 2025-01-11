/*
 * 2024-02-03
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {

    public class StaffEducateDao {
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
        public StaffEducateDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// SelectOneHStaffEducateMaster
        /// </summary>
        /// <returns></returns>
        public List<StaffEducateVo> SelectOneStaffEducateMaster(int staffCode) {
            List<StaffEducateVo> listStaffEducateVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffCode," +
                                            "EducateDate," +
                                            "EducateName," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_StaffEducateMaster " +
                                     "WHERE StaffCode = " + staffCode + "";
            using (var sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    StaffEducateVo staffEducateVo = new();
                    staffEducateVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    staffEducateVo.EducateDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["EducateDate"]);
                    staffEducateVo.EducateName = _defaultValue.GetDefaultValue<string>(sqlDataReader["CarViolateContent"]);
                    staffEducateVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["EducateName"]);
                    staffEducateVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    staffEducateVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    staffEducateVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    staffEducateVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    staffEducateVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    staffEducateVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listStaffEducateVo.Add(staffEducateVo);
                }
            }
            return listStaffEducateVo;
        }

        /// <summary>
        /// InsertOneHStaffEducateMaster
        /// </summary>
        /// <param name="staffEducateVo"></param>
        public void InsertOneStaffEducateMaster(StaffEducateVo staffEducateVo) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_StaffEducateMaster(StaffCode," +
                                                                      "EducateDate," +
                                                                      "EducateName," +
                                                                      "InsertPcName," +
                                                                      "InsertYmdHms," +
                                                                      "UpdatePcName," +
                                                                      "UpdateYmdHms," +
                                                                      "DeletePcName," +
                                                                      "DeleteYmdHms," +
                                                                      "DeleteFlag) " +
                                     "VALUES (" + _defaultValue.GetDefaultValue<int>(staffEducateVo.StaffCode) + "," +
                                            "'" + _defaultValue.GetDefaultValue<DateTime>(staffEducateVo.EducateDate) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<string>(staffEducateVo.EducateName) + "'," +
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
