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
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT TOP 1 StaffCode " +
                                     "FROM H_ToukanpoTrainingCardMaster " +
                                     "WHERE StaffCode = " + staffCode + "";
            return sqlCommand.ExecuteScalar() is not null ? true : false;
        }

        public ToukanpoTrainingCardVo SelectOneToukanpoTrainingCardMaster(int staffCode) {
            ToukanpoTrainingCardVo toukanpoTrainingCardVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
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
                    toukanpoTrainingCardVo.DisplayName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DisplayName"]);
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

        public int InsertOneToukanpoTrainingCardMaster(ToukanpoTrainingCardVo toukanpoTrainingCardVo) {
            var sqlCommand = _connectionVo.Connection.CreateCommand();
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
                                            "'" + toukanpoTrainingCardVo.DisplayName + "'," +
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

        public int UpdateOneToukanpoTrainingCardMaster(ToukanpoTrainingCardVo toukanpoTrainingCardVo) {
            var sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_ToukanpoTrainingCardMaster " +
                                     "SET CompanyName = '" + toukanpoTrainingCardVo.CompanyName + "'," +
                                         "DisplayName = '" + toukanpoTrainingCardVo.DisplayName + "'," +
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
