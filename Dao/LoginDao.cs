/*
 * 2026-09-26
 */
using Microsoft.Data.SqlClient;                                         // 2026-09-07 SYstem.Data.SqlClient から Microsoft.Data.SqlClient に変更

using Common;

using Vo;

namespace Dao {
    public class LoginDao {
        private readonly DefaultValue _defaultValue = new();
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        /*
         * Vo
         */
        private ConnectionVo? _connectionVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        public LoginDao() {
            /*
             * Vo
             */
            _connectionVo = null;
        }

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        public LoginDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginVo"></param>
        /// <returns></returns>
        public int InsertLogin(LoginVo loginVo) {
            if(ConnectionVo?.SqlServerConnection is null)
                return 0;

            SqlCommand sqlCommand = ConnectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_Login(Id," +
                                                         "Status," +
                                                         "PcName," +
                                                         "IpAddress," +
                                                         "LoginDateTime," +
                                                         "InsertPcName," +
                                                         "InsertYmdHms," +
                                                         "UpdatePcName," +
                                                         "UpdateYmdHms," +
                                                         "DeletePcName," +
                                                         "DeleteYmdHms," +
                                                         "DeleteFlag) " +
                                     "VALUES ('" + loginVo.Id + "'," +
                                             "'" + loginVo.Status + "'," +
                                             "'" + loginVo.LoginPcName + "'," +
                                             "'" + loginVo.LoginIpAddress + "'," +
                                             "'" + loginVo.LoginDateTime + "'," +
                                             "'" + Environment.MachineName + "'," +
                                             "'" + DateTime.Now + "'," +
                                             "'" + string.Empty + "'," +
                                             "'" + _defaultDateTime + "'," +
                                             "'" + string.Empty + "'," +
                                             "'" + _defaultDateTime + "'," +
                                             "'false'" +
                                             ");";
            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /*
         * プロパティ
         */
        public ConnectionVo? ConnectionVo {
            get => _connectionVo;
            set => _connectionVo = value;
        }
    }
}
