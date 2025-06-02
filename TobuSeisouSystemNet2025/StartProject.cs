/*
 * 2024-09-23
 */
using System.Data;

using Accident;

using Accounting;

using Car;

using Certification;

using CollectionWeight;

using Common;

using ControlEx;

using EmploymentAgreement;

using LegalTwelveItem;

using License;

using RollCall;

using Staff;

using StatusOfResidence;

using Toukanpo;

using VehicleDispatch;

using Vo;

namespace TobuSeisouSystemNet2025 {
    public partial class StartProject : Form {
        /*
         * インスタンス作成
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
             * Initialize
             */
            InitializeComponent();
            /*
             * Form
             */
            _screenForm.SetPosition(Screen.PrimaryScreen, this);
            /*
             * MenuStrip
             */
            List<string> listString = new() {
                "ToolStripMenuItemFile",
                "ToolStripMenuItemExit",
                "ToolStripMenuItemDataBase",
                "ToolStripMenuItemDataBaseLocal",
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);

            this.LabelExPcName.Text = string.Concat("〇 PC-Name：", Environment.MachineName);
            this.LabelExIpAddress.Text = string.Concat("〇 IP-Address：", new Network().GetIpAddress());
            this.LabelExLocation.Text = string.Concat("〇 NW-Location：", new Network().GetConnectLocation(), "からの接続");
            /*
             * ComboBoxExMonitor
             */
            this.ComboBoxExMonitor.Items.Clear();
            List<ComboBoxItem> listComboBoxItem = new();
            int i = 0;
            int primaryScreenNumber = 0;
            foreach (Screen screen in _screenForm.GetAllScreen()) {
                listComboBoxItem.Add(new ComboBoxItem(string.Concat(screen.DeviceName, "　{ ", screen.Bounds.Width, "×", screen.Bounds.Height, " }"), screen));
                if (screen.Primary)
                    // PrimaryScreenを退避
                    primaryScreenNumber = i;
                i++;
            }
            // ComboBoxにデータをバインド
            this.ComboBoxExMonitor.DataSource = listComboBoxItem;
            this.ComboBoxExMonitor.DisplayMember = "DisplayName"; // 表示名を設定
            this.ComboBoxExMonitor.ValueMember = "Screen"; // 値を設定
            // PrimaryScreenをセット
            this.ComboBoxExMonitor.SelectedIndex = primaryScreenNumber;
            /*
             * TabControlExConnect
             */
            switch (new Network().GetConnectLocation()) {
                case "システム管理":                                        // TabPage[0]
                    break;
                case "事務":                                              // TabPage[1]
                    break;
                case "本社":                                              // TabPage[2]
                    break;
                case "三郷":                                              // TabPage[3]
                    break;
                case "２丁目事務所":                                       // TabPage[]
                    break;
                case "中間処理場":                                         // TabPage[]
                    break;
                default:
                    break;
            }
            /*
             * Event
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEx_Click(object sender, EventArgs e) {
            switch (((ButtonEx)sender).Name) {
                case "ButtonExConnect":
                    try {
                        _connectionVo.Connect(MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag);
                        this.ButtonExConnect.Enabled = false;
                        this.ButtonExDisConnect.Enabled = true;
                        this.LabelExServerName.Text = string.Concat("接続先サーバー：" + _connectionVo.Connection.DataSource);
                        this.LabelExDataBaseName.Text = string.Concat("接続先データベース：" + _connectionVo.Connection.Database);
                        this.LabelExStatus.Text = string.Concat("状態：" + _connectionVo.Connection.State);
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "ButtonExDisConnect":
                    _connectionVo.Connection.Close();
                    this.ButtonExConnect.Enabled = true;
                    this.ButtonExDisConnect.Enabled = false;
                    this.LabelExServerName.Text = "接続先サーバー：";
                    this.LabelExDataBaseName.Text = "接続先データベース：";
                    this.LabelExStatus.Text = "状態：";
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemExit":
                    this.Close();
                    break;
                case "ToolStripMenuItemDataBaseLocal":
                    this.MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = ((ToolStripMenuItem)sender).Checked;
                    break;
                default:
                    MessageBox.Show("ToolStripMenuItemが登録されていません", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        /// <summary>
        /// Label_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_Click(object sender, EventArgs e) {
            switch (_connectionVo.Connection.State) {
                case ConnectionState.Open:                                                                                                      //接続が開いています。
                    switch ((string)((Label)sender).Tag) {
                        case "VehicleDispatchBoard":                                                                                            // 配車パネル
                            VehicleDispatchBoard vehicleDispatchBoard = new(_connectionVo);
                            _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, vehicleDispatchBoard);
                            vehicleDispatchBoard.Show();
                            break;
                        case "FirstRollColl":                                                                                                   // 配車表
                            FirstRollColl firstRollColl = new(_connectionVo);
                            _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, firstRollColl);
                            firstRollColl.Show();
                            break;
                        case "StaffList":                                                                                                       // 従事者リスト
                            StaffList staffList = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);
                            _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, staffList);
                            staffList.Show();
                            break;
                        case "CarList":                                                                                                         // 車両台帳
                            CarList carList = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);
                            _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, carList);
                            carList.Show();
                            break;
                        case "EmploymentAgreementList":                                                                                         // 契約書・誓約書等
                            EmploymentAgreementList employmentAgreementList = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);
                            _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, employmentAgreementList);
                            employmentAgreementList.Show();
                            break;
                        case "StaffDestination":
                            StaffDestination staffDestination = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);
                            _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, staffDestination);
                            staffDestination.Show();
                            break;
                        case "CollectionWeightChiyoda":                                                                                         // 千代田配車集計表
                            CollectionWeightChiyoda collectionWeightChiyoda = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);
                            _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, collectionWeightChiyoda);
                            collectionWeightChiyoda.Show();
                            break;
                        case "StaffWorkingHours":                                                                                               // 個別労働時間集計表
                            StaffWorkingHours staffWorkingHours = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);
                            _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, staffWorkingHours);
                            staffWorkingHours.Show();
                            break;
                        case "StaffWorkingDays":                                                                                                // 個別労働時間集計表
                            StaffWorkingDays staffWorkingDays = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);
                            _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, staffWorkingDays);
                            staffWorkingDays.Show();
                            break;
                        case "LicenseList":                                                                                                     // 免許証台帳
                            LicenseList licenseList = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);
                            _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, licenseList);
                            licenseList.Show();
                            break;
                        case "ToukanpoList":                                                                                                    // 東環保カード
                            ToukanpoList toukanpoList = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);
                            _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, toukanpoList);
                            toukanpoList.Show();
                            break;
                        case "AccountingParttimeList":
                            AccountingParttimeList accountingParttimeList = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);        // アルバイト出勤状況
                            _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, accountingParttimeList);
                            accountingParttimeList.Show();
                            break;
                        case "AccountingFulltime":
                            AccountingFulltimeList accountingFulltimeList = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);        // 全従事者出勤状況
                            _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, accountingFulltimeList);
                            accountingFulltimeList.Show();
                            break;
                        case "StatusOfResidenceList":
                            StatusOfResidenceList statusOfResidenceList = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);          // 在留カード
                            _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, statusOfResidenceList);
                            statusOfResidenceList.Show();
                            break;
                        case "RollCallRecordSheet":
                            RollCallRecordSheet rollCallRecordSheet = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);             // 点呼記録簿
                            _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, rollCallRecordSheet);
                            rollCallRecordSheet.Show();
                            break;
                        case "LegalTwelveItemList":
                            LegalTwelveItemList legalTwelveItemList = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);             // 法定１２項目の講習
                            _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, legalTwelveItemList);
                            legalTwelveItemList.Show();
                            break;
                        case "CertificationList":
                            CertificationList certificationList = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);                  // 有資格者証一覧
                            _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, certificationList);
                            certificationList.Show();
                            break;
                        case "AccidentList":
                            AccidentList accidentList = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);                            // 事故記録簿
                            _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, accidentList);
                            accidentList.Show();
                            break;
                    }
                    break;
                case ConnectionState.Connecting:                                                                                                // 接続オブジェクトがデータ ソースに接続しています。
                    break;
                case ConnectionState.Closed:                                                                                                    //接続が閉じています。
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
        /// Label_MouseEnter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_MouseEnter(object sender, EventArgs e) {
            ((Label)sender).ForeColor = Color.Red;
        }

        /// <summary>
        /// Label_MouseLeave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_MouseLeave(object sender, EventArgs e) {
            ((Label)sender).ForeColor = Color.Black;
        }

        /// <summary>
        /// 内部クラス
        /// </summary>
        private class ComboBoxItem {
            string _displayName = string.Empty;
            Screen _screen = null;
            /// <summary>
            /// コンストラクター
            /// </summary>
            /// <param name="displayName"></param>
            /// <param name="screen"></param>
            public ComboBoxItem(string displayName, Screen screen) {
                _displayName = displayName;
                _screen = screen;
            }
            /// <summary>
            /// DisplayName
            /// </summary>
            public string DisplayName {
                get => this._displayName;
                set => this._displayName = value;
            }
            /// <summary>
            /// Screen
            /// </summary>
            public Screen Screen {
                get => this._screen;
                set => this._screen = value;
            }
        }

        /// <summary>
        /// StartForm_FormClosing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartForm_FormClosing(object sender, FormClosingEventArgs e) {
            DialogResult dialogResult = MessageBox.Show("アプリケーションを終了します。よろしいですか？", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            switch (dialogResult) {
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
