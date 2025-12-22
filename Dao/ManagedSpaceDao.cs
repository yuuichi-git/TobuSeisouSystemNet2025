/*
 * 2025-12-16
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class ManagedSpaceDao {
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        public ManagedSpaceDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ManagedSpaceMasterVo> SelectAllManagedSpace() {
            List<ManagedSpaceMasterVo> listManagedSpaceVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT Code," +
                                            "Name," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_ManagedSpaceMaster";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    ManagedSpaceMasterVo managedSpaceVo = new();
                    managedSpaceVo.Code = _defaultValue.GetDefaultValue<int>(sqlDataReader["Code"]);
                    managedSpaceVo.Name = _defaultValue.GetDefaultValue<string>(sqlDataReader["Name"]);
                    managedSpaceVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    managedSpaceVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    managedSpaceVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    managedSpaceVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    managedSpaceVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    managedSpaceVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    managedSpaceVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listManagedSpaceVo.Add(managedSpaceVo);
                }
            }
            return listManagedSpaceVo;
        }
    }
}
