/*
 * 2024-02-07
 */
using System.Data;
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class ToukanpoTrainingCardDao {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        public ToukanpoTrainingCardDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// レコードの存在確認
        /// </summary>
        /// <param name="staffCode"></param>
        /// <returns>true:存在する false:存在しない</returns>
        public bool ExistenceToukanpoTrainingCardMaster(int staffCode) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT TOP 1 StaffCode " +
                                     "FROM H_ToukanpoTrainingCardMaster " +
                                     "WHERE StaffCode = " + staffCode + "";
            return sqlCommand.ExecuteScalar() is not null ? true : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public ToukanpoTrainingCardVo SelectOneToukanpoTrainingCardMaster(int staffCode) {
            ToukanpoTrainingCardVo toukanpoTrainingCardVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffCode," +
                                            "DisplayName," +
                                            "CompanyName," +
                                            "CardName," +
                                            "CertificationDate," +
                                            "Picture," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_ToukanpoTrainingCardMaster " +
                                     "WHERE StaffCode = " + staffCode + "";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    toukanpoTrainingCardVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    toukanpoTrainingCardVo.Name = _defaultValue.GetDefaultValue<string>(sqlDataReader["DisplayName"]);
                    toukanpoTrainingCardVo.CompanyName = _defaultValue.GetDefaultValue<string>(sqlDataReader["CompanyName"]);
                    toukanpoTrainingCardVo.CardName = _defaultValue.GetDefaultValue<string>(sqlDataReader["CardName"]);
                    toukanpoTrainingCardVo.CertificationDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["CertificationDate"]);
                    toukanpoTrainingCardVo.Picture = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture"]);
                    toukanpoTrainingCardVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    toukanpoTrainingCardVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    toukanpoTrainingCardVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    toukanpoTrainingCardVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    toukanpoTrainingCardVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    toukanpoTrainingCardVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    toukanpoTrainingCardVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                }
            }
            return toukanpoTrainingCardVo;
        }

        /// <summary>
        /// ToukanpoList内で使用している
        /// </summary>
        /// <returns></returns>
        public List<ToukanpoTrainingCardVo> SelectAllToukanpoTrainingCardMaster() {
            List<ToukanpoTrainingCardVo> listToukanpoTrainingCardVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT H_ToukanpoTrainingCardMaster.StaffCode," +
                                            "H_StaffMaster.UnionCode," +
                                            "H_StaffMaster.Name," +
                                            "H_StaffMaster.NameKana," +
                                            "H_ToukanpoTrainingCardMaster.CompanyName," +
                                            "H_ToukanpoTrainingCardMaster.CardName," +
                                            "H_ToukanpoTrainingCardMaster.CertificationDate," +
                                            //"Picture," +
                                            "H_ToukanpoTrainingCardMaster.Memo," +
                                            "H_ToukanpoTrainingCardMaster.InsertPcName," +
                                            "H_ToukanpoTrainingCardMaster.InsertYmdHms," +
                                            "H_ToukanpoTrainingCardMaster.UpdatePcName," +
                                            "H_ToukanpoTrainingCardMaster.UpdateYmdHms," +
                                            "H_ToukanpoTrainingCardMaster.DeletePcName," +
                                            "H_ToukanpoTrainingCardMaster.DeleteYmdHms," +
                                            "H_ToukanpoTrainingCardMaster.DeleteFlag " +
                                     "FROM H_ToukanpoTrainingCardMaster " +
                                     "LEFT OUTER JOIN H_StaffMaster ON H_ToukanpoTrainingCardMaster.StaffCode = H_StaffMaster.StaffCode " +
                                     "WHERE H_StaffMaster.RetirementFlag = 'false'";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    ToukanpoTrainingCardVo toukanpoTrainingCardVo = new();
                    toukanpoTrainingCardVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    toukanpoTrainingCardVo.UnionCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["UnionCode"]);
                    toukanpoTrainingCardVo.Name = _defaultValue.GetDefaultValue<string>(sqlDataReader["Name"]);
                    toukanpoTrainingCardVo.NameKana = _defaultValue.GetDefaultValue<string>(sqlDataReader["NameKana"]);
                    toukanpoTrainingCardVo.CompanyName = _defaultValue.GetDefaultValue<string>(sqlDataReader["CompanyName"]);
                    toukanpoTrainingCardVo.CardName = _defaultValue.GetDefaultValue<string>(sqlDataReader["CardName"]);
                    toukanpoTrainingCardVo.CertificationDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["CertificationDate"]);
                    //toukanpoTrainingCardVo.Picture = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture"]);
                    toukanpoTrainingCardVo.Memo = _defaultValue.GetDefaultValue<string>(sqlDataReader["Memo"]);
                    toukanpoTrainingCardVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    toukanpoTrainingCardVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    toukanpoTrainingCardVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    toukanpoTrainingCardVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    toukanpoTrainingCardVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    toukanpoTrainingCardVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    toukanpoTrainingCardVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listToukanpoTrainingCardVo.Add(toukanpoTrainingCardVo);
                }
            }
            return listToukanpoTrainingCardVo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toukanpoTrainingCardVo"></param>
        /// <returns></returns>
        public int InsertOneToukanpoTrainingCardMaster(ToukanpoTrainingCardVo toukanpoTrainingCardVo) {
            var sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_ToukanpoTrainingCardMaster(StaffCode," +
                                                                              "DisplayName," +
                                                                              "CompanyName," +
                                                                              "CardName," +
                                                                              "CertificationDate," +
                                                                              "Picture," +
                                                                              "InsertPcName," +
                                                                              "InsertYmdHms," +
                                                                              "UpdatePcName," +
                                                                              "UpdateYmdHms," +
                                                                              "DeletePcName," +
                                                                              "DeleteYmdHms," +
                                                                              "DeleteFlag) " +
                                     "VALUES (" + toukanpoTrainingCardVo.StaffCode + "," +
                                            "'" + toukanpoTrainingCardVo.Name + "'," +
                                            "'" + toukanpoTrainingCardVo.CompanyName + "'," +
                                            "'" + toukanpoTrainingCardVo.CardName + "'," +
                                            "'" + toukanpoTrainingCardVo.CertificationDate + "'," +
                                            "@pictureCard," +
                                            "'" + Environment.MachineName + "'," +
                                            "'" + DateTime.Now + "'," +
                                            "''," +
                                            "'" + _defaultDateTime + "'," +
                                            "''," +
                                            "'" + _defaultDateTime + "'," +
                                            "'false'" +
                                            ");";
            try {
                sqlCommand.Parameters.Add("@pictureCard", SqlDbType.Image, toukanpoTrainingCardVo.Picture.Length).Value = toukanpoTrainingCardVo.Picture;
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toukanpoTrainingCardVo"></param>
        /// <returns></returns>
        public int UpdateOneToukanpoTrainingCardMaster(ToukanpoTrainingCardVo toukanpoTrainingCardVo) {
            var sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_ToukanpoTrainingCardMaster " +
                                     "SET CompanyName = '" + toukanpoTrainingCardVo.CompanyName + "'," +
                                         "DisplayName = '" + toukanpoTrainingCardVo.Name + "'," +
                                         "CardName = '" + toukanpoTrainingCardVo.CardName + "'," +
                                         "CertificationDate = '" + toukanpoTrainingCardVo.CertificationDate + "'," +
                                         "Picture = @pictureCard," +
                                         "UpdatePcName = '" + Environment.MachineName + "'," +
                                         "UpdateYmdHms = '" + DateTime.Now + "' " +
                                     "WHERE StaffCode = " + toukanpoTrainingCardVo.StaffCode;
            try {
                sqlCommand.Parameters.Add("@pictureCard", SqlDbType.Image, toukanpoTrainingCardVo.Picture.Length).Value = toukanpoTrainingCardVo.Picture;
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }
    }
}
