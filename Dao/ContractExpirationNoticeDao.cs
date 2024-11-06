/*
 * 2024-11-06
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class ContractExpirationNoticeDao {
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        public ContractExpirationNoticeDao(ConnectionVo connectionVo) {
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
        public List<ContractExpirationNoticeVo> SelectOneContractExpirationNotice(int staffCode) {
            List<ContractExpirationNoticeVo> listContractExpirationNoticeVo = new();
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
                                     "FROM H_LossWrittenPledge " +
                                     "WHERE StaffCode = '" + staffCode + "'";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    ContractExpirationNoticeVo contractExpirationNoticeVo = new();
                    contractExpirationNoticeVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    contractExpirationNoticeVo.ContractExpirationStartDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["ContractExpirationStartDate"]);
                    contractExpirationNoticeVo.ContractExpirationEndDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["ContractExpirationEndDate"]);
                    contractExpirationNoticeVo.Memo = _defaultValue.GetDefaultValue<string>(sqlDataReader["Memo"]);
                    contractExpirationNoticeVo.Picture = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture"]);
                    contractExpirationNoticeVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    contractExpirationNoticeVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    contractExpirationNoticeVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    contractExpirationNoticeVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    contractExpirationNoticeVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    contractExpirationNoticeVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    contractExpirationNoticeVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listContractExpirationNoticeVo.Add(contractExpirationNoticeVo);
                }
            }
            return listContractExpirationNoticeVo;
        }
    }
}
