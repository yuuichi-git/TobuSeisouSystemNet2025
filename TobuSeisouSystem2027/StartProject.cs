/*
 * 2026-09-19
 */
using System.Data;

using CcControl;

using Common;

using VehicleDispatch;

using Vo;

namespace TobuSeisouSystem2027 {
    public partial class StartProject : Form {
        /*
         * インスタンス
         */
        private readonly ScreenForm _screenForm = new();
        /*
         * Vo
         */
        private ConnectionVo _connectionVo = new();

        /// <summary>
        /// コンストラクター
        /// </summary>
        public StartProject() {
            /*
             * InitializeControls
             */
            InitializeComponent();
            /*
             * MenuStrip
             */
            List<string> listString = new() {"ToolStripMenuItemFile",
                                             "ToolStripMenuItemExit",
                                             "ToolStripMenuItemDataBase",
                                             "ToolStripMenuItemDataBaseLocal",
                                             "ToolStripMenuItemHelp"};
            this.CcMenuStrip1.ChangeEnable(listString);
            this.CcMenuStrip1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CcButton_Click(object sender, EventArgs e) {
            switch(((CcButton)sender).Name) {
                case "CcButtonConnect":
                    try {
                        switch(_connectionVo.ConnectSqlServer(this.CcMenuStrip1.ToolStripMenuItemDataBaseLocalFlag)) {
                            case ConnectionState.Open:
                                /*
                                 * Client
                                 */
                                this.CcLabelClientName.Text = NetworkUtility.GetPcName();
                                this.CcLabelClientIpAddress.Text = NetworkUtility.GetIpAddress();
                                this.CcLabelClientDefaultGateway.Text = NetworkUtility.GetDefaultGatewayAddress();
                                /*
                                 * Server
                                 */
                                this.CcLabelServerName.Text = NetworkUtility.GetPcNameFromIp(_connectionVo.ServerName);
                                this.CcLabelServerIpAddress.Text = _connectionVo.ServerName;
                                this.CcLabelDbName.Text = _connectionVo.SqlServerConnection?.Database;
                                this.CcLabelDbStatus.Text = _connectionVo.SqlServerConnection?.State.ToString();

                                this.CcButtonConnect.Enabled = false;
                                this.CcButtonDisConnect.Enabled = true;
                                this.CcComboBoxMonitors1.Enabled = false;
                                this.CcComboBoxPrinters1.Enabled = false;
                                break;
                            default:
                                MessageBox.Show("SQL Serverへの接続に失敗しました。", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                        }

                    } catch(Exception exception) {
                        MessageBox.Show(exception.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                case "CcButtonDisConnect":
                    try {
                        switch(_connectionVo.DisConnectSqlServer()) {
                            case ConnectionState.Closed:
                                /*
                                 * Client
                                 */
                                this.CcLabelClientName.Text = string.Empty;
                                this.CcLabelClientIpAddress.Text = string.Empty;
                                this.CcLabelClientDefaultGateway.Text = string.Empty;
                                /*
                                 * Server
                                 */
                                this.CcLabelServerName.Text = string.Empty;
                                this.CcLabelServerIpAddress.Text = string.Empty;
                                this.CcLabelDbName.Text = string.Empty;
                                this.CcLabelDbStatus.Text = string.Empty;

                                this.CcButtonConnect.Enabled = true;
                                this.CcButtonDisConnect.Enabled = false;
                                this.CcComboBoxMonitors1.Enabled = true;
                                this.CcComboBoxPrinters1.Enabled = true;
                                break;

                            default:
                                MessageBox.Show("SQL Serverへの切断に失敗しました。", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;

                        }

                    } catch(Exception exception) {
                        MessageBox.Show(exception.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CcLabel_Click(object sender, EventArgs e) {
            switch(_connectionVo.SqlServerConnection?.State) {
                case ConnectionState.Open:                                                                                                      //接続が開いています。
                    switch(((CcLabel)sender).Name) {
                        /*
                         * 本社営業所
                         */
                        case "CcLabelVehicleDispatchBoardAdachi":
                            _connectionVo.ConnectionLocation = "本社営業所";
                            VehicleDispatchBoard vehicleDispatchBoard = new(_connectionVo);
                            _screenForm.SetPosition((Screen?)CcComboBoxMonitors1.SelectedValue, vehicleDispatchBoard);
                            vehicleDispatchBoard.Show();
                            break;
                        case "CcLabelFirstRollCall":

                            break;
                        /*
                         * 三郷車庫
                         */
                        case "CcLabelVehicleDispatchBoardMisato":
                            _connectionVo.ConnectionLocation = "三郷車庫";

                            break;
                        default:

                            break;
                    }
                    break;
                case ConnectionState.Connecting:                                                                                                //接続オブジェクトがデータ ソースに接続しています。
                    break;
                case ConnectionState.Closed:                                                                                                    //接続が閉じています。
                    MessageBox.Show("データベースに接続して下さい。", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case ConnectionState.Executing:                                                                                                 //接続オブジェクトがコマンドを実行しています。
                    break;
                case ConnectionState.Fetching:                                                                                                  //接続オブジェクトがデータを検索しています。
                    break;
                case ConnectionState.Broken:                                                                                                    //データ ソースへの接続が断絶しています。
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object? sender, EventArgs e) {
            if(sender is null)
                return;

            switch(((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemExit":
                    this.Close();
                    break;
                case "ToolStripMenuItemDataBaseLocal":
                    this.CcMenuStrip1.ToolStripMenuItemDataBaseLocalFlag = ((ToolStripMenuItem)sender).Checked;
                    break;
                default:
                    MessageBox.Show("ToolStripMenuItemが登録されていません", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        private void CcLabel_MouseEnter(object sender, EventArgs e) {
            ((CcLabel)sender).ForeColor = Color.Red;
        }

        private void CcLabel_MouseLeave(object sender, EventArgs e) {
            ((CcLabel)sender).ForeColor = SystemColors.ControlText;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartProject_Load(object sender, EventArgs e) {
            /*
             * Client
             */
            this.CcLabelClientName.Text = string.Empty;
            this.CcLabelClientIpAddress.Text = string.Empty;
            this.CcLabelClientDefaultGateway.Text = string.Empty;
            /*
             * Server
             */
            this.CcLabelServerName.Text = string.Empty;
            this.CcLabelServerIpAddress.Text = string.Empty;
            this.CcLabelDbName.Text = string.Empty;
            this.CcLabelDbStatus.Text = string.Empty;

            this.CcButtonConnect.Enabled = true;
            this.CcButtonDisConnect.Enabled = false;
            this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "初期化に成功しました";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartProject_FormClosing(object sender, FormClosingEventArgs e) {
            if(_connectionVo.SqlServerConnection?.State == ConnectionState.Open) {
                MessageBox.Show("アプリケーションを終了する前に、データベースを切断して下さい。", "ACID特性の確保", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            } else {
                DialogResult dialogResult = MessageBox.Show("アプリケーションを終了します。よろしいですか？", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                switch(dialogResult) {
                    case DialogResult.OK:
                        e.Cancel = false;
                        Dispose();
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }
    }
}
