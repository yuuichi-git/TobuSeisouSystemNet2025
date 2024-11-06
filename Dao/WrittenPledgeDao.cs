/*
 * 2024-11-06
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class WrittenPledgeDao {
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        public WrittenPledgeDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public List<WrittenPledgeVo> SelectOneWrittenPledge(int staffCode) {
            List<WrittenPledgeVo> listWrittenPledgeVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffCode," +
                                            "ContractExpirationStartDate," +
                                            "ContractExpirationEndDate," +
                                            "Memo," +
                                            "Picture," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_WrittenPledge " +
                                     "WHERE StaffCode = '" + staffCode + "'";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    WrittenPledgeVo writtenPledgeVo = new();
                    writtenPledgeVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    writtenPledgeVo.ContractExpirationStartDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["ContractExpirationStartDate"]);
                    writtenPledgeVo.ContractExpirationEndDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["ContractExpirationEndDate"]);
                    writtenPledgeVo.Memo = _defaultValue.GetDefaultValue<string>(sqlDataReader["Memo"]);
                    writtenPledgeVo.Picture = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture"]);
                    writtenPledgeVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    writtenPledgeVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    writtenPledgeVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    writtenPledgeVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    writtenPledgeVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    writtenPledgeVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    writtenPledgeVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listWrittenPledgeVo.Add(writtenPledgeVo);
                }
            }
            return listWrittenPledgeVo;
        }
    }
}
