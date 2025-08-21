/*
 * 2024-02-03
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class StaffFamilyDao {
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
        public StaffFamilyDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// SelectOneHStaffFamilyMaster
        /// </summary>
        /// <returns></returns>
        public List<StaffFamilyVo> SelectOneStaffFamilyMaster(int staffCode) {
            List<StaffFamilyVo> listStaffFamilyVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffCode," +
                                            "FamilyName," +
                                            "FamilyBirthDay," +
                                            "FamilyRelationship," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_StaffFamilyMaster " +
                                     "WHERE StaffCode = " + staffCode + "";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    StaffFamilyVo staffFamilyVo = new();
                    staffFamilyVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    staffFamilyVo.FamilyName = _defaultValue.GetDefaultValue<string>(sqlDataReader["FamilyName"]);
                    staffFamilyVo.FamilyBirthDay = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["FamilyBirthDay"]);
                    staffFamilyVo.FamilyRelationship = _defaultValue.GetDefaultValue<string>(sqlDataReader["FamilyRelationship"]);
                    staffFamilyVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    staffFamilyVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    staffFamilyVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    staffFamilyVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    staffFamilyVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    staffFamilyVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listStaffFamilyVo.Add(staffFamilyVo);
                }
            }
            return listStaffFamilyVo;
        }

        /// <summary>
        /// InsertOneHStaffFamilyMaster
        /// </summary>
        /// <param name="staffFamilyVo"></param>
        public void InsertOneStaffFamilyMaster(StaffFamilyVo staffFamilyVo) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_StaffFamilyMaster(StaffCode," +
                                                                     "FamilyName," +
                                                                     "FamilyBirthDay," +
                                                                     "FamilyRelationship," +
                                                                     "InsertPcName," +
                                                                     "InsertYmdHms," +
                                                                     "UpdatePcName," +
                                                                     "UpdateYmdHms," +
                                                                     "DeletePcName," +
                                                                     "DeleteYmdHms," +
                                                                     "DeleteFlag) " +
                                     "VALUES (" + _defaultValue.GetDefaultValue<int>(staffFamilyVo.StaffCode) + "," +
                                            "'" + _defaultValue.GetDefaultValue<string>(staffFamilyVo.FamilyName) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<DateTime>(staffFamilyVo.FamilyBirthDay) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<string>(staffFamilyVo.FamilyRelationship) + "'," +
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
