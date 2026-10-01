/*
 * 2026-09-19
 */
using System.Data;

using Car;

using CcControl;

using Common;

using Dao;

using License;

using RollCall;

using Staff;

using VehicleDispatch;

using Vo;

namespace TobuSeisouSystem2027 {
    public partial class StartProject : Form {
        /*
         * インスタンス
         */
        private readonly ScreenForm _screenForm = new();
        /*
         * Dao
         */
        private LoginDao _loginDao = new();
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
                        switch(ConnectionVo.ConnectSqlServer(this.CcMenuStrip1.ToolStripMenuItemDataBaseLocalFlag)) {
                            case ConnectionState.Open:
                                /*
                                 * ログイン情報をVoへセット
                                 * ※接続確立後に処理
                                 */
                                ConnectionVo.LoginVo.Id = Guid.NewGuid().ToString("N"); // "N"ハイフンなし（32桁）
                                ConnectionVo.LoginVo.Status = "Connect";
                                ConnectionVo.LoginVo.LoginPcName = NetworkUtility.GetPcName();
                                ConnectionVo.LoginVo.LoginIpAddress = NetworkUtility.GetIpAddress();
                                ConnectionVo.LoginVo.LoginDateTime = DateTime.Now;
                                _loginDao.ConnectionVo = this.ConnectionVo;
                                _loginDao.InsertLogin(ConnectionVo.LoginVo);
                                /*
                                 * Client
                                 */
                                this.CcLabelClientName.Text = NetworkUtility.GetPcName();
                                this.CcLabelClientIpAddress.Text = NetworkUtility.GetIpAddress();
                                this.CcLabelClientDefaultGateway.Text = NetworkUtility.GetDefaultGatewayAddress();
                                /*
                                 * Server
                                 */
                                this.CcLabelServerName.Text = NetworkUtility.GetPcNameFromIp(ConnectionVo.ServerName);
                                this.CcLabelServerIpAddress.Text = ConnectionVo.ServerName;
                                this.CcLabelDbName.Text = ConnectionVo.SqlServerConnection?.Database;
                                this.CcLabelDbStatus.Text = ConnectionVo.SqlServerConnection?.State.ToString();

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
                    /*
                     * ログオフ情報をVoへセット
                     * ※接続切断後に処理
                     */
                    ConnectionVo.LoginVo.Id = Guid.NewGuid().ToString("N"); // "N"ハイフンなし（32桁）
                    ConnectionVo.LoginVo.Status = "DisConnect";
                    ConnectionVo.LoginVo.LoginPcName = NetworkUtility.GetPcName();
                    ConnectionVo.LoginVo.LoginIpAddress = NetworkUtility.GetIpAddress();
                    ConnectionVo.LoginVo.LoginDateTime = DateTime.Now;
                    _loginDao.ConnectionVo = this.ConnectionVo;
                    _loginDao.InsertLogin(ConnectionVo.LoginVo);

                    try {
                        switch(ConnectionVo.DisConnectSqlServer()) {
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

        private VehicleDispatchBoard? _vehicleDispatchBoardAdachi;
        private FirstRollCall? _firstRollCall;
        private CarList? _carList;
        private StaffList? _staffList;
        private LicenseList? _licenseList;
        private StaffDestination? _staffDestination;
        private CarWorkingDays? _carWorkingDays;
        private VehicleDispatchBoard? _vehicleDispatchBoardMisato;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CcLabel_Click(object sender, EventArgs e) {
            switch(ConnectionVo.SqlServerConnection?.State) {
                case ConnectionState.Open:
                    switch(((CcLabel)sender).Name) {
                        /*
                         * 本社営業所
                         */
                        case "CcLabelVehicleDispatchBoardAdachi":
                            if(_vehicleDispatchBoardAdachi == null || _vehicleDispatchBoardAdachi.IsDisposed) {
                                ConnectionVo.ConnectionLocation = "本社営業所";
                                _vehicleDispatchBoardAdachi = new VehicleDispatchBoard(ConnectionVo);
                                _screenForm.SetPosition((Screen?)CcComboBoxMonitors1.SelectedValue, _vehicleDispatchBoardAdachi);
                                _vehicleDispatchBoardAdachi.Show();
                            }
                            break;
                        /*
                         * 点呼
                         */
                        case "CcLabelFirstRollCall":
                            if(_firstRollCall == null || _firstRollCall.IsDisposed) {
                                _firstRollCall = new FirstRollCall(ConnectionVo);
                                _screenForm.SetPosition((Screen?)CcComboBoxMonitors1.SelectedValue, _firstRollCall);
                                _firstRollCall.Show();
                            }
                            break;
                        /*
                         * 車両台帳
                         */
                        case "CcLabelCarList":
                            if(_carList == null || _carList.IsDisposed) {
                                _carList = new CarList(ConnectionVo, (Screen?)CcComboBoxMonitors1.SelectedValue);
                                _screenForm.SetPosition((Screen?)CcComboBoxMonitors1.SelectedValue, _carList);
                                _carList.Show();
                            }
                            break;
                        /*
                         * 従事者台帳
                         */
                        case "CcLabelStaffList":
                            if(_staffList == null || _staffList.IsDisposed) {
                                _staffList = new StaffList(ConnectionVo, (Screen?)CcComboBoxMonitors1.SelectedValue);
                                _screenForm.SetPosition((Screen?)CcComboBoxMonitors1.SelectedValue, _staffList);
                                _staffList.Show();
                            }
                            break;
                        /*
                         * 免許証台帳
                         */
                        case "CcLabelLicenseList":
                            if(_licenseList == null || _licenseList.IsDisposed) {
                                _licenseList = new LicenseList(ConnectionVo, (Screen?)CcComboBoxMonitors1.SelectedValue);
                                _screenForm.SetPosition((Screen?)CcComboBoxMonitors1.SelectedValue, _licenseList);
                                _licenseList.Show();
                            }
                            break;
                        /*
                         * 従事者勤務詳細
                         */
                        case "CcLabelStaffDestination":
                            if(_staffDestination == null || _staffDestination.IsDisposed) {
                                _staffDestination = new StaffDestination(ConnectionVo, (Screen?)CcComboBoxMonitors1.SelectedValue);
                                _screenForm.SetPosition((Screen?)CcComboBoxMonitors1.SelectedValue, _staffDestination);
                                _staffDestination.Show();
                            }
                            break;
                        /*
                         * 車両稼働表
                         */
                        case "CcLabelCarWorkingDays":
                            if(_carWorkingDays == null || _carWorkingDays.IsDisposed) {
                                _carWorkingDays = new CarWorkingDays(ConnectionVo, (Screen?)CcComboBoxMonitors1.SelectedValue);
                                _screenForm.SetPosition((Screen?)CcComboBoxMonitors1.SelectedValue, _carWorkingDays);
                                _carWorkingDays.Show();
                            }
                            break;
                        /*
                         * 三郷車庫
                         */
                        case "CcLabelVehicleDispatchBoardMisato":
                            if(_vehicleDispatchBoardMisato == null || _vehicleDispatchBoardMisato.IsDisposed) {
                                ConnectionVo.ConnectionLocation = "三郷車庫";
                                _vehicleDispatchBoardMisato = new VehicleDispatchBoard(ConnectionVo);
                                _screenForm.SetPosition((Screen?)CcComboBoxMonitors1.SelectedValue, _vehicleDispatchBoardMisato);
                                _vehicleDispatchBoardMisato.Show();
                            }
                            break;

                        default:
                            break;
                    }
                    break;

                case ConnectionState.Closed:
                    MessageBox.Show("データベースに接続して下さい。", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if(ConnectionVo.SqlServerConnection?.State == ConnectionState.Open) {
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

        /*
         * 
         * プロパティ
         * 
         */
        public ConnectionVo ConnectionVo {
            get => _connectionVo;
            set => _connectionVo = value;
        }
    }
}
