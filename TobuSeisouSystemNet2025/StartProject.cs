/*
 * 2024-09-23
 */
using System.Data;

using Accident;

using Accounting;

using Car;

using Certification;

using Collection;

using Common;

using ControlEx;

using EGov;

using EmploymentAgreement;

using LegalTwelveItem;

using License;

using RollCall;

using Seisou;

using Staff;

using StatusOfResidence;

using Toukanpo;

using VehicleDispatch;

using Vo;

using Waste;

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
             * TreeViewEx
             */
            this.TreeViewEx1.Enabled = false;
            /*
             * システム管理
             */
            this.GroupBoxEx1.Enabled = false;
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
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;
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
                case "ButtonExConnectSqlServer":
                    try {
                        _connectionVo.ConnectSqlServer(this.MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag);
                        this.ButtonExConnectSqlServer.Enabled = false;
                        this.ButtonExDisConnectSqlServer.Enabled = true;
                        this.LabelExServerNameSqlServer.Text = string.Concat("接続先サーバー：" + _connectionVo.SqlServerConnection.DataSource);
                        this.LabelExDataBaseNameSqlServer.Text = string.Concat("接続先データベース：" + _connectionVo.SqlServerConnection.Database);
                        this.LabelExStatusSqlServer.Text = string.Concat("状態：" + _connectionVo.SqlServerConnection.State);

                        this.TreeViewEx1.Enabled = true;
                        this.GroupBoxEx1.Enabled = true;
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "ButtonExDisConnectSqlServer":
                    if (_connectionVo.OracleConnection.State == ConnectionState.Open) {
                        MessageBox.Show("OracleをCloseして下さい", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    } else {
                        try {
                            _connectionVo.DisConnectSqlServer();
                            this.ButtonExConnectSqlServer.Enabled = true;
                            this.ButtonExDisConnectSqlServer.Enabled = false;
                            this.LabelExServerNameSqlServer.Text = "接続先サーバー：";
                            this.LabelExDataBaseNameSqlServer.Text = "接続先データベース：";
                            this.LabelExStatusSqlServer.Text = "状態：";

                            this.TreeViewEx1.Enabled = false;
                            this.GroupBoxEx1.Enabled = false;
                        } catch (Exception exception) {
                            MessageBox.Show(exception.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    break;
                case "ButtonExConnectOracle":
                    try {
                        _connectionVo.ConnectOracle();
                        this.ButtonExConnectOracle.Enabled = false;
                        this.ButtonExDisConnectOracle.Enabled = true;
                        this.LabelExServerNameOracle.Text = string.Concat("接続先サーバー：" + _connectionVo.OracleConnection.DataSource);
                        this.LabelExServerVersion.Text = string.Concat("ServerVersion：" + _connectionVo.OracleConnection.ServerVersion);
                        this.LabelExDataBaseNameOracle.Text = string.Concat("接続先データベース：" + _connectionVo.OracleConnection.DatabaseName);
                        this.LabelExStatusOracle.Text = string.Concat("状態：" + _connectionVo.OracleConnection.State);
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "ButtonExDisConnectOracle":
                    try {
                        _connectionVo.DisConnectOracle();
                        this.ButtonExConnectOracle.Enabled = true;
                        this.ButtonExDisConnectOracle.Enabled = false;
                        this.LabelExServerNameOracle.Text = "接続先サーバー：";
                        this.LabelExServerVersion.Text = "ServerVersion：";
                        this.LabelExDataBaseNameOracle.Text = "接続先データベース：";
                        this.LabelExStatusOracle.Text = "状態：";

                        this.TreeViewEx1.Enabled = false;
                    } catch (Exception exception) {
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
        /// 接続先がSQLServerの場合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>a
        private void Label_SqlServer_Click(object sender, EventArgs e) {
            switch (_connectionVo.SqlServerConnection.State) {
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
                        case "StaffList":                                                                                                       // 従事者台帳
                            StaffList staffList = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);
                            _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, staffList);
                            staffList.Show();
                            break;
                        case "CarList":                                                                                                         // 車両台帳
                            CarList carList = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);
                            _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, carList);
                            carList.Show();
                            break;
                        case "CarWorkingDays":                                                                                                  // 車両稼働一覧
                            CarWorkingDays carWorkingDays = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);
                            _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, carWorkingDays);
                            carWorkingDays.Show();
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
                            CollectionStaffsChiyoda collectionWeightChiyoda = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);
                            _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, collectionWeightChiyoda);
                            collectionWeightChiyoda.ShowDialog();
                            break;
                        case "CollectionStaffsTaitou":                                                                                          // 台東古紙配車人数集計表
                            CollectionStaffsTaitou collectionStaffsTaitou = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);
                            _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, collectionStaffsTaitou);
                            collectionStaffsTaitou.ShowDialog();
                            break;
                        case "CollectionWeightTaitouList":                                                                                      // 台東古紙収集量集計表
                            CollectionWeightTaitouList collectionWeightTaitouList = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);
                            _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, collectionWeightTaitouList);
                            collectionWeightTaitouList.ShowDialog();
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
                        case "ToukanpoSpeedSurvey":                                                                                             // 東環保速度超過表
                            ToukanpoSpeedSurvey toukanpoSpeedSurvey = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);
                            _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, toukanpoSpeedSurvey);
                            toukanpoSpeedSurvey.Show();
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
                        case "WasteList":
                            WasteList wasteList = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);                                  // 廃棄物
                            _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, wasteList);
                            wasteList.Show();
                            break;
                    }
                    break;
                case ConnectionState.Connecting:                                                                                                //接続オブジェクトがデータ ソースに接続しています。
                    break;
                case ConnectionState.Closed:                                                                                                    //接続が閉じています。
                    MessageBox.Show("SQLServerへ接続して下さい。", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// 接続先がOracleの場合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_Oracle_Click(object sender, EventArgs e) {
            switch (_connectionVo.OracleConnection.State) {
                case ConnectionState.Open:                                                                                                      //接続が開いています。
                    switch ((string)((Label)sender).Tag) {
                        /*
                         * システム管理
                         */
                        case "OracleAllTable":
                            OracleAllTable oracleAllTable = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);                        // Oracleテーブル一覧表示
                            _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, oracleAllTable);
                            oracleAllTable.Show();
                            break;
                    }
                    break;
                case ConnectionState.Connecting:                                                                                                //接続オブジェクトがデータ ソースに接続しています。
                    break;
                case ConnectionState.Closed:                                                                                                    //接続が閉じています。
                    MessageBox.Show("Oracleへ接続して下さい。", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (_connectionVo.SqlServerConnection.State == ConnectionState.Open) {
                MessageBox.Show("アプリケーションを終了する前に、データベースを切断して下さい。", "ACID特性の確保", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            } else {
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            Files files = new();
            if (_connectionVo.SqlServerConnection.State == ConnectionState.Open) {
                switch (e.Node.Name) {
                    /*
                     * 陸運局監査
                     */
                    case "NodeRik00": // 00　巡回指導資料
                        files.OpenFolder(@"\\192.168.1.20\iso14001\陸運局監査\00　巡回指導資料");
                        break;
                    case "NodeRik01": // 01　運転者台帳
                        /*
                         * Formを表示する
                         */
                        StaffList staffList = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);
                        _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, staffList);
                        staffList.Show();
                        break;
                    case "NodeRik02": // 02　運行管理規定
                        files.OpenFolder(@"\\192.168.1.20\iso14001\陸運局監査\02　運行管理規定");
                        break;
                    case "NodeRik03": // 03　点呼記録簿・点呼執行要領
                        files.OpenFolder(@"\\192.168.1.20\iso14001\陸運局監査\03　点呼記録簿・点呼執行要領");
                        /*
                         * Formを表示する
                         */
                        FirstRollColl firstRollColl = new(_connectionVo);
                        _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, firstRollColl);
                        firstRollColl.Show();
                        break;
                    case "NodeRik04": // 04　乗務記録(運転日報)
                        break;
                    case "NodeRik05": // 05　運行計画及び勤務割当表
                        VehicleDispatchBoard vehicleDispatchBoard = new(_connectionVo);
                        _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, vehicleDispatchBoard);
                        vehicleDispatchBoard.Show();
                        break;
                    case "NodeRik06": // 06　乗務実績一覧表(拘束時間管理表)
                        break;
                    case "NodeRik07": // 07　運行記録計による記録
                        break;
                    case "NodeRik08": // 08　運行指示書
                        break;
                    case "NodeRik09": // 09　受注伝票
                        break;
                    case "NodeRik10": // 10　運行管理者・整備管理者選任届出書
                        break;
                    case "NodeRik11": // 11　運行管理者資格者証
                        break;
                    case "NodeRik12": // 12　運行管理者研修手帳
                        break;
                    case "NodeRik13": // 13　整備管理者研修手帳
                        break;
                    case "NodeRik14": // 14　教育実施計画
                        break;
                    case "NodeRik15": // 15　運転記録証明書又は無事故無違反証明書
                        break;
                    case "NodeRik16": // 16　乗務員指導記録簿
                        break;
                    case "NodeRik17": // 17　適性診断受診結果票
                        break;
                    case "NodeRik18": // 18　適性診断受診計画表
                        break;
                    case "NodeRik19": // 19　事故記録簿
                        break;
                    case "NodeRik20": // 20　自動車事故報告書
                        break;
                    case "NodeRik21": // 21　事業報告書・事業実績報告書
                        break;
                    case "NodeRik22": // 22　役員変更届出書
                        break;
                    case "NodeRik23": // 23　車両台帳・自動車検査証の写し
                        break;
                    case "NodeRik24": // 24　整備管理規定等の規定類
                        break;
                    case "NodeRik25": // 25　点検整備記録簿
                        break;
                    case "NodeRik26": // 26　日常点検基準
                        break;
                    case "NodeRik27": // 27　日常点検表
                        break;
                    case "NodeRik28": // 28　定期点検基準
                        break;
                    case "NodeRik29": // 29　定期点検整備実施計画表
                        break;
                    case "NodeRik30": // 30　賃金台帳
                        break;
                    case "NodeRik31": // 31　健康診断書・健康診断記録簿
                        break;
                    case "NodeRik32": // 32　就業規則
                        break;
                    case "NodeRik33": // 33　３６協定
                        break;
                    case "NodeRik34": // 34　出勤簿
                        break;
                    case "NodeRik35": // 35　運輸安全マネジメント関係
                        break;
                    case "NodeRik36": // 36　労災保険加入台帳
                        break;
                    case "NodeRik37": // 37　雇用保険加入台帳
                        break;
                    case "NodeRik38": // 38　健康保険加入台帳・厚生年金加入台帳
                        break;
                    /*
                     * ISO14001
                     */
                    case "NodeISO0000": // 環境マネジメントマニュアル
                        files.OpenFolder(@"\\192.168.1.20\iso14001\ISO事務局\① ISO\⓪環境マネジメントマニュアル");
                        break;
                    case "NodeISO0100": // 適用規格
                        files.OpenFolder(@"\\192.168.1.20\iso14001\ISO事務局\① ISO\①適用規格");
                        break;
                    case "NodeISO0200": // 引用規格
                        files.OpenFolder(@"\\192.168.1.20\iso14001\ISO事務局\① ISO\②引用規格");
                        break;
                    case "NodeISO0300": // 用語及び定義
                        files.OpenFolder(@"\\192.168.1.20\iso14001\ISO事務局\① ISO\③用語及び定義");
                        break;
                    case "NodeISO0400": // 組織の状況
                        break;
                    case "NodeISO0410": // 組織及びその状況の理解
                        break;
                    case "NodeISO0420": // 利害関係者のニーズ及び期待の理解
                        break;
                    case "NodeISO0430": // 環境マネジメントシステムの適用範囲の決定
                        break;
                    case "NodeISO0440": // 環境マネジメントシステム
                        break;
                    case "NodeISO0500": // リーダーシップ
                        break;
                    case "NodeISO0510": // リーダーシップ及びコミットメント
                        break;
                    case "NodeISO0520": // 環境方針
                        break;
                    case "NodeISO0530": // 組織の役割、責任及び権限
                        break;
                    case "NodeISO0600": // 計画
                        break;
                    case "NodeISO0610": // リスク及び機会への取組み
                        break;
                    case "NodeISO0611": // 一般
                        break;
                    case "NodeISO0612": // 環境側面
                        break;
                    case "NodeISO0613": // 順守義務(法的及びその他の要求事項)
                        /*
                         * Formを表示する
                         */
                        EGovList eGovList = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);
                        _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, eGovList);
                        eGovList.Show(this);
                        break;
                    case "NodeISO0614": // 取組みの計画策定
                        break;
                    case "NodeISO0620": // 環境目標及びそれを達成するための計画策定(環境マネジメントプログラム) 
                        files.OpenFolder(@"\\192.168.1.20\iso14001\ISO事務局\① ISO\⑥計画\⑥-2 環境目標及びそれを達成するための計画策定(環境マネジメントプログラム)");
                        break;
                    case "NodeISO0621": // 環境目標
                        break;
                    case "NodeISO0622": // 環境目標を達成するための取組みの計画策定
                        break;
                    case "NodeISO0700": // 支援
                        break;
                    case "NodeISO0710": // 資源
                        break;
                    case "NodeISO0720": // 力量
                        /*
                         * Formを表示する
                         */
                        CertificationList certificationList = new(_connectionVo, (Screen)ComboBoxExMonitor.SelectedValue);
                        _screenForm.SetPosition((Screen)ComboBoxExMonitor.SelectedValue, certificationList);
                        certificationList.Show(this);
                        break;
                    case "NodeISO0730": // 認識 
                        break;
                    case "NodeISO0740": // コミュニケーション
                        break;
                    case "NodeISO0750": // 文書管理
                        break;
                    case "NodeISO0751": // 一般
                        break;
                    case "NodeISO0752": // 作成及び更新
                        break;
                    case "NodeISO0753": // 文書化した情報の管理
                        break;
                    case "NodeISO0800": // 運用
                        break;
                    case "NodeISO0810": // 運用の計画及び管理
                        break;
                    case "NodeISO0820": // 緊急事態への準備及び対応
                        break;
                    case "NodeISO0900": // パフォーマンス評価
                        break;
                    case "NodeISO0910": // 監視、測定、分析及び評価
                        break;
                    case "NodeISO0911": // 一般
                        files.OpenFolder(@"\\192.168.1.20\iso14001\ISO事務局\① ISO\⑨パフォーマンス評価\⑨-1 監視、測定、分析及び評価\⑨-1-1 一般");
                        break;
                    case "NodeISO0912": // 順守評価
                        files.OpenFolder(@"\\192.168.1.20\iso14001\ISO事務局\① ISO\⑨パフォーマンス評価\⑨-1 監視、測定、分析及び評価\⑨-1-2 順守評価");
                        break;
                    case "NodeISO0920": // 内部監査
                        files.OpenFolder(@"\\192.168.1.20\iso14001\ISO事務局\① ISO\⑨パフォーマンス評価\⑨-2 内部監査");
                        break;
                    case "NodeISO0930": // マネジメントレビュー
                        files.OpenFolder(@"\\192.168.1.20\iso14001\ISO事務局\① ISO\⑨パフォーマンス評価\⑨-3 マネジメントレビュー");
                        break;
                    case "NodeISO1000": // 改善
                        break;
                    case "NodeISO1010": // 一般
                        break;
                    case "NodeISO1020": // 不適合及び是正措置
                        break;
                    case "NodeISO1030": // 継続的改善
                        break;
                    case "NodeTreatmentPlant": // 中間処理場
                        break;
                    case "NodeAccident": // 事故受付
                        files.OpenFolder(@"\\192.168.1.20\iso14001\ISO事務局\② 事故受付");
                        break;
                }
            } else {
            }
        }
    }
}
