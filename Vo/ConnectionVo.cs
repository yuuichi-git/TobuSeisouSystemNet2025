/*
 * 2024-09-24
 */
using System.Data.SqlClient;
using System.Net.NetworkInformation;

using Oracle.ManagedDataAccess.Client;

using Vo.Properties;

namespace Vo {
    public class ConnectionVo {
        private SqlConnection _sqlConnection;
        private OracleConnection _oracleConnection;
        private readonly Ping _ping;
        private PingReply? _pingReply;
        private string _serverName = string.Empty;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public ConnectionVo() {
            _sqlConnection = new();
            _oracleConnection = new();
            _ping = new();
            _pingReply = null;
        }

        /// <summary> 
        /// ConnectSqlServer
        /// </summary>
        /// <param name="localDbConnectionFlag">true:Localに接続 false:Networkに接続</param>
        /// <returns>true:成功 false:失敗</returns>
        public bool ConnectSqlServer(bool localDbConnectionFlag) {
            try {
                switch (Environment.MachineName) {
                    case "TSUJINOTE":                                                                           // 自分のPCの場合
                        if (!localDbConnectionFlag) {
                            _pingReply = _ping.Send("192.168.1.20");
                            if (_pingReply.Status == IPStatus.Success)
                                _serverName = @"192.168.1.20";
                        } else {
                            _serverName = @"(Local)";
                        }
                            break;
                    default:                                                                                    // TSUJINOTE以外のPCは強制的にNetwork接続
                        _serverName = @"192.168.1.20";
                        break;
                }
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);                                                             // Pingでエラーが発生した場合
            }

            string connectionString = "Data Source = " + _serverName + ";"
                                    + "Initial Catalog = " + Resources.DataBaseName + ";"
                                    + "User ID = " + Resources.UserName + ";"
                                    + "Password = " + Resources.UserPassword + ";"
                                    + "MultipleActiveResultSets = True";
            this.SqlServerConnection = new(connectionString);
            try {
                this.SqlServerConnection.Open();
                return true;
            } catch {
                return false;
                throw;
            }
        }

        /// <summary>
        /// DisConnectSqlServer
        /// </summary>
        /// <returns></returns>
        public bool DisConnectSqlServer() {
            try {
                this.SqlServerConnection.Close();
                this.SqlServerConnection.Dispose();
                return true;
            } catch {
                return false;
                throw;
            }
        }

        /// <summary>
        /// ConnectOracle
        /// </summary>
        /// <returns></returns>
        public bool ConnectOracle() {
            string OraIP = "192.168.1.20:1521";
            string OraSID = "SEISOU";
            string OraID = "SEISOU";
            string OraPass = "SEISOU";
            OracleConnection.ConnectionString = "Data Source = //" + OraIP + "/" + OraSID + ";" +
                                                "User ID = " + OraID + ";" +
                                                "Password = " + OraPass + ";";
            try {
                OracleConnection.Open();
                return true;
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
                return false;
            }
        }

        /// <summary>
        /// DisConnectOracle
        /// </summary>
        public bool DisConnectOracle() {
            try {
                OracleConnection.Close();
                return true;
            } catch {
                return false;
                throw;
            }
        }

        /*
         * 
         * プロパティ
         * 
         */
        /// <summary>
        /// 接続を保持
        /// </summary>
        public SqlConnection SqlServerConnection {
            get => this._sqlConnection;
            set => this._sqlConnection = value;
        }
        /// <summary>
        /// 接続を保持
        /// </summary>
        public OracleConnection OracleConnection {
            get => this._oracleConnection;
            set => this._oracleConnection = value;
        }
    }
}
