/*
 * 2024-09-24
 */
using System.Data;
using System.Net.NetworkInformation;

using Microsoft.Data.SqlClient;                                                             // 2026-09-07 System.Data.SqlClient から Microsoft.Data.SqlClient に変更

using Oracle.ManagedDataAccess.Client;

using Vo.Properties;

namespace Vo {
    public class ConnectionVo {
        private SqlConnection? _sqlConnection = new();
        private OracleConnection _oracleConnection = new();
        private readonly Ping _ping = new();
        private string _serverName = string.Empty;
        private string _connectionLocation = string.Empty;

        private LoginVo _loginVo = new();

        /// <summary>
        /// SQL Server 接続
        /// </summary>
        /// <param name="localDbConnectionFlag">true:ローカルDB false:ネットワークDB</param>
        /// <returns></returns>
        public ConnectionState ConnectSqlServer(bool localDbConnectionFlag = false) {
            try {
                switch(Environment.MachineName) {
                    case "YUUICHIZBOOK":
                        if(localDbConnectionFlag) {
                            ServerName = @"localhost";                                                              // ローカル接続
                        } else {
                            PingReply? _pingReply = _ping.Send(Resources.DataBaseServer);
                            ServerName = (_pingReply.Status == IPStatus.Success) ? @"192.168.1.20" : @"localhost";  // フォールバック
                        }
                        break;

                    default:
                        ServerName = Resources.DataBaseServer;                                                      // 他PCは強制ネットワーク
                        break;
                }
            } catch(Exception exception) {
                MessageBox.Show(exception.Message);
            }

            /*
             * Microsoft.Data.SqlClient 接続文字列
             */
            string connectionString = "Data Source=" + ServerName + ";" +
                                      "Initial Catalog=" + Resources.DataBaseName + ";" +
                                      "User ID=" + Resources.UserName + ";" +
                                      "Password=" + Resources.UserPassword + ";" +
                                      "MultipleActiveResultSets=True;" +
                                      "Encrypt=False;";
            this.SqlServerConnection = new(connectionString);

            try {
                this.SqlServerConnection.Open();
                return this.SqlServerConnection.State;
            } catch {
                return this.SqlServerConnection.State;
            }
        }

        /// <summary>
        /// SQL Server 切断
        /// </summary>
        /// <returns></returns>
        public ConnectionState? DisConnectSqlServer() {
            try {
                // 開いている場合のみ Close（Dispose 内でも呼ばれるが安全のため）
                if(SqlServerConnection?.State == ConnectionState.Open)
                    SqlServerConnection.Close();
                return SqlServerConnection?.State;

            } catch {
                return SqlServerConnection?.State;
            }
        }

        /// <summary>
        /// ConnectOracle
        /// </summary>
        /// <returns></returns>
        public bool ConnectOracle() {
            string OraIP        = "192.168.1.20:1521";
            string OraSID       = "SEISOU";
            string OraID        = "SEISOU";
            string OraPass      = "SEISOU";
            OracleConnection.ConnectionString = "Data Source    = //" + OraIP + "/" + OraSID + ";" +
                                                "User ID        = " + OraID + ";" +
                                                "Password       = " + OraPass + ";";
            try {
                OracleConnection.Open();
                return true;
            } catch(Exception exception) {
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
        public SqlConnection? SqlServerConnection {
            get => this._sqlConnection;
            set => this._sqlConnection = value;
        }
        /// <summary>
        /// Oracle 接続を保持
        /// </summary>
        public OracleConnection OracleConnection {
            get => this._oracleConnection;
            set => this._oracleConnection = value;
        }
        /// <summary>
        /// 接続地区を保持
        /// </summary>
        public string ConnectionLocation {
            get => this._connectionLocation;
            set => this._connectionLocation = value;
        }
        /// <summary>
        /// 接続先サーバー名を保持
        /// </summary>
        public string ServerName {
            get => _serverName;
            set => _serverName = value;
        }
        /// <summary>
        /// ログイン情報を保持
        /// </summary>
        public LoginVo LoginVo {
            get => _loginVo;
            set => _loginVo = value;
        }
    }

    /// <summary>
    /// ログイン管理クラス
    /// </summary>
    public class LoginVo {
        private string _id = string.Empty;
        private string _status = string.Empty;
        private string _loginPcName = string.Empty;
        private string _loginIpAddress = string.Empty;
        private DateTime _loginDateTime = new DateTime(1900,01,01);

        /// <summary>
        /// 一意のId
        /// </summary>
        public string Id {
            get => _id;
            set => _id = value;
        }
        /// <summary>
        /// ログインステータス
        /// Connect　DisConnect
        /// </summary>
        public string Status {
            get => _status;
            set => _status = value;
        }
        /// <summary>
        /// ログインPC名
        /// </summary>
        public string LoginPcName {
            get => _loginPcName;
            set => _loginPcName = value;
        }
        /// <summary>
        /// ログインIPアドレス
        /// </summary>
        public string LoginIpAddress {
            get => _loginIpAddress;
            set => _loginIpAddress = value;
        }
        /// <summary>
        /// ログイン日時
        /// </summary>
        public DateTime LoginDateTime {
            get => _loginDateTime;
            set => _loginDateTime = value;
        }
    }
}
