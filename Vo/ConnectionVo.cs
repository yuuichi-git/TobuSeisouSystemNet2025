/*
 * 2024-09-24
 */
using System.Data.SqlClient;
using System.Net.NetworkInformation;

using Vo.Properties;

namespace Vo {
    public class ConnectionVo {
        private SqlConnection _sqlConnection = new();
        private Ping _ping = new();
        private PingReply _pingReply = null;
        private string _serverName = "(Local)";

        /// <summary>
        /// コンストラクター
        /// </summary>
        public ConnectionVo() {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localDbFlag">true:Localに接続 false:Networkに接続</param>
        /// <returns>true:成功 false:失敗</returns>
        public bool Connect(bool localDbFlag) {
            switch (Environment.MachineName) {
                case "LAPTOP-5J3QGU8A":
                    if (!localDbFlag) {
                        _pingReply = _ping.Send("192.168.1.21");
                        if (_pingReply.Status == IPStatus.Success)
                            _serverName = @"TOBUSERVER\SQLEXPRESS";
                    }
                    break;
                default:
                    _serverName = @"TOBUSERVER\SQLEXPRESS";
                    break;
            }
            string connectionString = "Data Source = " + _serverName + ";"
                                    + "Initial Catalog = " + Resources.DataBaseName + ";"
                                    + "User ID = " + Resources.UserName + ";"
                                    + "Password = " + Resources.UserPassword + ";"
                                    + "MultipleActiveResultSets = True";
            _sqlConnection = new(connectionString);
            try {
                _sqlConnection.Open();
                Connection = _sqlConnection;
                return true;
            } catch {
                return false;
                throw;
            }
        }

        /// <summary>
        /// DisConnect
        /// </summary>
        /// <returns></returns>
        public void DisConnect() {
            try {
                Connection.Close();
                Connection.Dispose();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 接続を保持
        /// </summary>
        public SqlConnection Connection {
            get => this._sqlConnection;
            set => this._sqlConnection = value;
        }
    }
}
