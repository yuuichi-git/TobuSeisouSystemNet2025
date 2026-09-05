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

        private string _connectionLocation = string.Empty;

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
        /// 
        /// </summary>
        /// <param name="localDbConnectionFlag"></param>
        /// <returns></returns>
        public bool ConnectSqlServer(bool localDbConnectionFlag) {
            try {
                switch(Environment.MachineName) {
                    case "TSUJINOTE":
                    case "YUUICHIZBOOK":
                        if(localDbConnectionFlag) {
                            _serverName = @"localhost";     // ローカル接続
                        } else {
                            _pingReply = _ping.Send("192.168.1.20");
                            _serverName = (_pingReply.Status == IPStatus.Success)
                                            ? @"192.168.1.20"
                                            : @"localhost"; // フォールバック
                        }
                        break;

                    default:
                        _serverName = @"192.168.1.20";       // 他PCは強制ネットワーク
                        break;
                }
            } catch(Exception exception) {
                MessageBox.Show(exception.Message);
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
        /// SqlServer 接続を保持
        /// </summary>
        public SqlConnection SqlServerConnection { get => this._sqlConnection; set => this._sqlConnection = value; }
        /// <summary>
        /// Oracle 接続を保持
        /// </summary>
        public OracleConnection OracleConnection { get => this._oracleConnection; set => this._oracleConnection = value; }
        /// <summary>
        /// 接続地区を保持
        /// </summary>
        public string ConnectionLocation { get => this._connectionLocation; set => this._connectionLocation = value; }
    }
}
