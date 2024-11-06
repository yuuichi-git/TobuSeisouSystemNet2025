/*
 * 2024-11-06
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class LossWrittenPledgeDao {
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        public LossWrittenPledgeDao(ConnectionVo connectionVo) {
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
        public List<LossWrittenPledgeVo> SelectOneLossWrittenPledge(int staffCode) {
            List<LossWrittenPledgeVo> listLossWrittenPledgeVo = new();
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
                    LossWrittenPledgeVo lossWrittenPledgeVo = new();
                    lossWrittenPledgeVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    lossWrittenPledgeVo.ContractExpirationStartDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["ContractExpirationStartDate"]);
                    lossWrittenPledgeVo.ContractExpirationEndDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["ContractExpirationEndDate"]);
                    lossWrittenPledgeVo.Memo = _defaultValue.GetDefaultValue<string>(sqlDataReader["Memo"]);
                    lossWrittenPledgeVo.Picture = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture"]);
                    lossWrittenPledgeVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    lossWrittenPledgeVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    lossWrittenPledgeVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    lossWrittenPledgeVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    lossWrittenPledgeVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    lossWrittenPledgeVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    lossWrittenPledgeVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listLossWrittenPledgeVo.Add(lossWrittenPledgeVo);
                }
            }
            return listLossWrittenPledgeVo;
        }
    }
}
