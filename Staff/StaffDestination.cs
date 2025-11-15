/*
 * 2024-12-19
 */
using System.Data;

using Common;

using ControlEx;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace Staff {
    public partial class StaffDestination : Form {
        /*
         * インスタンス作成
         */
        private readonly DateUtility _dateUtility = new();
        /*
         * SPREADのColumnの番号
         */
        /// <summary>
        /// 配車日
        /// </summary>
        private const int colOperationDate = 0;
        /// <summary>
        /// 出庫点呼時刻
        /// </summary>
        private const int colStaffRollCallYmdHms = 1;
        /// <summary>
        /// 役職又は所属
        /// </summary>
        private const int colBelongs = 2;
        /// <summary>
        /// 雇用形態
        /// </summary>
        private const int colJobForm = 3;
        /// <summary>
        /// 職種
        /// </summary>
        private const int colStaffOccupation = 4;
        /// <summary>
        /// 氏名
        /// </summary>
        private const int colDisplayName = 5;
        /// <summary>
        /// 配車先
        /// </summary>
        private const int colSetName = 6;
        /// <summary>
        /// メモ
        /// </summary>
        private const int colStaffMemo = 7;
        /*
         * Dao
         */
        private readonly StaffDestinationDao _staffDestinationDao;
        private BelongsMasterDao _belongsMasterDao;
        private JobFormMasterDao _jobFormMasterDao;
        private OccupationMasterDao _occupationMasterDao;
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;
        /*
         * Dictionary
         */
        private readonly Dictionary<int, string> _dictionaryBelongs = new();
        private readonly Dictionary<int, string> _dictionaryOccupation = new();
        private readonly Dictionary<int, string> _dictionaryJobForm = new();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public StaffDestination(ConnectionVo connectionVo, Screen screen) {
            /*
             * Dao
             */
            _staffDestinationDao = new(connectionVo);
            _belongsMasterDao = new(connectionVo);
            _jobFormMasterDao = new(connectionVo);
            _occupationMasterDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            /*
             * Dictionary
             */
            foreach (BelongsMasterVo belongsMasterVo in _belongsMasterDao.SelectAllBelongsMaster())
                _dictionaryBelongs.Add(belongsMasterVo.Code, belongsMasterVo.Name);
            foreach (JobFormMasterVo jobFormMasterVo in _jobFormMasterDao.SelectAllJobFormMaster())
                _dictionaryJobForm.Add(jobFormMasterVo.Code, jobFormMasterVo.Name);
            foreach (OccupationMasterVo occupationMasterVo in _occupationMasterDao.SelectAllOccupationMaster())
                _dictionaryOccupation.Add(occupationMasterVo.Code, occupationMasterVo.Name);
            /*
             * InitializeControl
             */
            InitializeComponent();
            /*
             * MenuStrip
             */
            List<string> listString = new() {
                "ToolStripMenuItemFile",
                "ToolStripMenuItemExit",
                "ToolStripMenuItemPrint",
                "ToolStripMenuItemPrintA4",
                "ToolStripMenuItemHelp"
            };
            MenuStripEx1.ChangeEnable(listString);
            /*
             * 配車日を設定
             */
            this.DateTimePickerExOperationDate1.SetValue(_dateUtility.GetBeginOfMonth(DateTime.Now));
            this.DateTimePickerExOperationDate2.SetValue(_dateUtility.GetEndOfMonth(DateTime.Now));
            this.InitializeComboBoxExStaffDisplayName();
            this.InitializeSheetView(this.SheetViewList);
            /*
             * Eventを登録する
             */
            MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEx_Click(object sender, EventArgs e) {
            switch (((ButtonEx)sender).Name) {
                case "ButtonExUpdate":
                    try {
                        this.SetSheetView();
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
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
                case "ToolStripMenuItemPrintA4":
                    SpreadList.PrintSheet(SheetViewList);
                    break;
                default:
                    MessageBox.Show("ToolStripMenuItem_Click");
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetSheetView() {
            List<SheetViewVo> _listSheetViewVo = new();
            /*
             * ComboBoxExStaffNameが未選択ならCheckBoxでSQLを発行
             */
            switch (ComboBoxExStaffName.SelectedIndex) {
                case -1: // 複数名検索
                    /*
                     * SQL条件を作成する
                     * SQLを作成する際に、全てのチェック項目のチェックが外れていないかを確認する
                     */
                    bool check;
                    check = false;
                    foreach (CheckBox checkBox in GroupBoxExBelongs.Controls) {
                        if (checkBox.Checked)
                            check = true;
                    }
                    if (!check) {
                        MessageBox.Show("役職又は所属(第一条件)の全てのチェックを外す事は出来ません", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    check = false;
                    foreach (CheckBox checkBox in GroupBoxExJobForm.Controls) {
                        if (checkBox.Checked)
                            check = true;
                    }
                    if (!check) {
                        MessageBox.Show("雇用形態(第二条件)の全てのチェックを外す事は出来ません", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    check = false;
                    foreach (CheckBox checkBox in GroupBoxExOccupation.Controls) {
                        if (checkBox.Checked)
                            check = true;
                    }
                    if (!check) {
                        MessageBox.Show("職種(第三条件)の全てのチェックを外す事は出来ません", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    /*
                     * SQL発行
                     */
                    _listSheetViewVo = GetListSheetViewVo(_staffDestinationDao.SelectAllStaffDestinationVo(this.DateTimePickerExOperationDate1.GetDate(), this.DateTimePickerExOperationDate2.GetDate()));
                    _listSheetViewVo = _listSheetViewVo.Where(x => CreateList(this.GroupBoxExBelongs).Contains(x.StaffBelongs) &&
                                                                   CreateList(this.GroupBoxExJobForm).Contains(x.StaffJobForm) &&
                                                                   CreateList(this.GroupBoxExOccupation).Contains(x.StaffOccupation)).ToList();
                    break;
                default: // 1名検索
                    /*
                     * SQL発行
                     */
                    _listSheetViewVo = GetListSheetViewVo(_staffDestinationDao.SelectAllStaffDestinationVo(this.DateTimePickerExOperationDate1.GetDate(), this.DateTimePickerExOperationDate2.GetDate()));
                    _listSheetViewVo = _listSheetViewVo.FindAll(x => x.StaffCode == ((HComboBoxExSelectNameVo)ComboBoxExStaffName.SelectedItem).StaffMasterVo.StaffCode);
                    break;
            }
            /*
             * 表示
             */
            int rowCount = 0;
            this.SpreadList.SuspendLayout(); // Spread 非活性化
            // Rowを削除する
            if (SheetViewList.Rows.Count > 0)
                SheetViewList.RemoveRows(0, SheetViewList.Rows.Count);
            if (_listSheetViewVo is not null) {
                /*
                 * 朝電・無断のみを抽出
                 */
                if (CheckBoxExAbsence.Checked)
                    _listSheetViewVo = _listSheetViewVo.FindAll(x => x.SetCode == 1312140 || x.SetCode == 1312141);
                /*
                 * 変数定義
                 */
                foreach (SheetViewVo sheetViewVo in _listSheetViewVo.OrderBy(x => x.OperationDate)) {
                    SheetViewList.Rows.Add(rowCount, 1);
                    SheetViewList.RowHeader.Columns[0].Label = (rowCount + 1).ToString(); // Rowヘッダ
                    SheetViewList.Rows[rowCount].Height = 20; // Rowの高さ
                    SheetViewList.Rows[rowCount].Resizable = false; // RowのResizableを禁止
                    // RowのForeColor
                    SheetViewList.Rows[rowCount].ForeColor = sheetViewVo.SetCode == 1312140 ? Color.Red : Color.Black;
                    // 配車日
                    SheetViewList.Cells[rowCount, colOperationDate].Value = sheetViewVo.OperationDate.Date;
                    // 点呼時刻
                    SheetViewList.Cells[rowCount, colStaffRollCallYmdHms].Text = sheetViewVo.StaffRollCallYmdHms.ToString("HH:mm");
                    // 役職又は所属
                    SheetViewList.Cells[rowCount, colBelongs].Value = _dictionaryBelongs[sheetViewVo.StaffBelongs];
                    // 雇用形態
                    SheetViewList.Cells[rowCount, colJobForm].Value = _dictionaryJobForm[sheetViewVo.StaffJobForm];
                    // 職種
                    SheetViewList.Cells[rowCount, colStaffOccupation].Value = _dictionaryOccupation[sheetViewVo.StaffOccupation];
                    // 氏名
                    SheetViewList.Cells[rowCount, colDisplayName].Text = sheetViewVo.StaffName;
                    // 配車先
                    SheetViewList.Cells[rowCount, colSetName].Value = sheetViewVo.SetName;
                    // メモ
                    SheetViewList.Cells[rowCount, colStaffMemo].Text = sheetViewVo.Memo;
                    rowCount++;
                }
            }
            this.SpreadList.ResumeLayout(); // Spread 活性化
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(" ", rowCount, " 件");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listStaffDestinationVo"></param>
        /// <returns></returns>
        private List<SheetViewVo> GetListSheetViewVo(List<StaffDestinationVo> listStaffDestinationVo) {
            List<SheetViewVo> listSheetViewVo = new();
            foreach (StaffDestinationVo staffDestinationVo in listStaffDestinationVo) {
                SheetViewVo sheetViewVo1 = new();
                sheetViewVo1.OperationDate = staffDestinationVo.OperationDate;
                sheetViewVo1.StaffRollCallYmdHms = staffDestinationVo.StaffRollCallYmdHms1;
                sheetViewVo1.StaffBelongs = staffDestinationVo.StaffBelongs1;
                sheetViewVo1.StaffJobForm = staffDestinationVo.StaffJobForm1;
                sheetViewVo1.StaffOccupation = staffDestinationVo.StaffOccupation1;
                sheetViewVo1.StaffCode = staffDestinationVo.StaffCode1;
                sheetViewVo1.StaffName = staffDestinationVo.StaffName1;
                sheetViewVo1.SetCode = staffDestinationVo.SetCode;
                sheetViewVo1.SetName = staffDestinationVo.SetName;
                sheetViewVo1.Memo = staffDestinationVo.StaffMemo1;
                listSheetViewVo.Add(sheetViewVo1);

                SheetViewVo sheetViewVo2 = new();
                sheetViewVo2.OperationDate = staffDestinationVo.OperationDate;
                sheetViewVo2.StaffRollCallYmdHms = staffDestinationVo.StaffRollCallYmdHms2;
                sheetViewVo2.StaffBelongs = staffDestinationVo.StaffBelongs2;
                sheetViewVo2.StaffJobForm = staffDestinationVo.StaffJobForm2;
                sheetViewVo2.StaffOccupation = staffDestinationVo.StaffOccupation2;
                sheetViewVo2.StaffCode = staffDestinationVo.StaffCode2;
                sheetViewVo2.StaffName = staffDestinationVo.StaffName2;
                sheetViewVo2.SetCode = staffDestinationVo.SetCode;
                sheetViewVo2.SetName = staffDestinationVo.SetName;
                sheetViewVo2.Memo = staffDestinationVo.StaffMemo2;
                listSheetViewVo.Add(sheetViewVo2);

                SheetViewVo sheetViewVo3 = new();
                sheetViewVo3.OperationDate = staffDestinationVo.OperationDate;
                sheetViewVo3.StaffRollCallYmdHms = staffDestinationVo.StaffRollCallYmdHms3;
                sheetViewVo3.StaffBelongs = staffDestinationVo.StaffBelongs3;
                sheetViewVo3.StaffJobForm = staffDestinationVo.StaffJobForm3;
                sheetViewVo3.StaffOccupation = staffDestinationVo.StaffOccupation3;
                sheetViewVo3.StaffCode = staffDestinationVo.StaffCode3;
                sheetViewVo3.StaffName = staffDestinationVo.StaffName3;
                sheetViewVo3.SetCode = staffDestinationVo.SetCode;
                sheetViewVo3.SetName = staffDestinationVo.SetName;
                sheetViewVo3.Memo = staffDestinationVo.StaffMemo3;
                listSheetViewVo.Add(sheetViewVo3);

                SheetViewVo sheetViewVo4 = new();
                sheetViewVo4.OperationDate = staffDestinationVo.OperationDate;
                sheetViewVo4.StaffRollCallYmdHms = staffDestinationVo.StaffRollCallYmdHms4;
                sheetViewVo4.StaffBelongs = staffDestinationVo.StaffBelongs4;
                sheetViewVo4.StaffJobForm = staffDestinationVo.StaffJobForm4;
                sheetViewVo4.StaffOccupation = staffDestinationVo.StaffOccupation4;
                sheetViewVo4.StaffCode = staffDestinationVo.StaffCode4;
                sheetViewVo4.StaffName = staffDestinationVo.StaffName4;
                sheetViewVo4.SetCode = staffDestinationVo.SetCode;
                sheetViewVo4.SetName = staffDestinationVo.SetName;
                sheetViewVo4.Memo = staffDestinationVo.StaffMemo4;
                listSheetViewVo.Add(sheetViewVo4);
            }
            return listSheetViewVo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupBoxEx"></param>
        /// <returns></returns>
        private List<int> CreateList(GroupBoxEx groupBoxEx) {
            List<int> list = new();
            foreach (CheckBoxEx checkBoxEx in groupBoxEx.Controls) {
                if (checkBoxEx.Checked)
                    list.Add(Convert.ToInt32(checkBoxEx.Tag));
            }
            return list;
        }

        /// <summary>
        /// InitializeSheetView
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns>SheetView</returns>
        private SheetView InitializeSheetView(SheetView sheetView) {
            this.SpreadList.AllowDragDrop = false; // DrugDropを禁止する
            this.SpreadList.PaintSelectionHeader = false; // ヘッダの選択状態をしない
            this.SpreadList.TabStrip.DefaultSheetTab.Font = new Font("Yu Gothic UI", 9);
            this.SpreadList.TabStripPolicy = TabStripPolicy.Never; // Tab非表示
            sheetView.AlternatingRows.Count = 2; // 行スタイルを２行単位とします
            sheetView.AlternatingRows[0].BackColor = Color.WhiteSmoke; // 1行目の背景色を設定します
            sheetView.AlternatingRows[1].BackColor = Color.White; // 2行目の背景色を設定します
            sheetView.ColumnHeader.Rows[0].Height = 26; // Columnヘッダの高さ
            sheetView.GrayAreaBackColor = Color.White;
            sheetView.HorizontalGridLine = new GridLine(GridLineType.None);
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9); // 行ヘッダのFont
            sheetView.RowHeader.Columns[0].Width = 48; // 行ヘッダの幅を変更します
            sheetView.VerticalGridLine = new GridLine(GridLineType.Flat, Color.LightGray);
            sheetView.RemoveRows(0, sheetView.Rows.Count);
            return sheetView;
        }

        /// <summary>
        /// ComboBoxExStaffDisplayNameを初期化
        /// </summary>
        private void InitializeComboBoxExStaffDisplayName() {
            ComboBoxExStaffName.Items.Clear();
            foreach (StaffMasterVo staffMasterVo in _staffDestinationDao.SelectAllStaffMasterVo().OrderBy(x => x.NameKana))
                ComboBoxExStaffName.Items.Add(new HComboBoxExSelectNameVo(staffMasterVo.Name, staffMasterVo));
            ComboBoxExStaffName.DisplayMember = "Name";
        }

        /// <summary>
        /// インナークラス
        /// </summary>
        private class HComboBoxExSelectNameVo {
            private string _name;
            private StaffMasterVo _staffMasterVo;

            // プロパティをコンストラクタでセット
            public HComboBoxExSelectNameVo(string name, StaffMasterVo staffMasterVo) {
                _name = name;
                _staffMasterVo = staffMasterVo;
            }
            public string Name {
                get => _name;
                set => _name = value;
            }
            public StaffMasterVo StaffMasterVo {
                get => _staffMasterVo;
                set => _staffMasterVo = value;
            }
        }

        /// <summary>
        /// インナークラス
        /// </summary>
        private class SheetViewVo {
            private readonly DateTime _defaultDateTime = new DateTime(1900, 01, 01);
            private DateTime _operationDate;
            private DateTime _staffRollCallYmdHms;
            private int _staffBelongs;
            private int _staffJobForm;
            private int _staffOccupation;
            private int _staffCode;
            private string _staffName;
            private int _setCode;
            private string _setName;
            private string _memo;

            public SheetViewVo() {
                _operationDate = _defaultDateTime;
                _staffRollCallYmdHms = _defaultDateTime;
                _staffBelongs = 99;
                _staffJobForm = 99;
                _staffOccupation = 99;
                _staffCode = 0;
                _staffName = string.Empty;
                _setCode = 0;
                _setName = string.Empty;
                _memo = string.Empty;
            }

            public DateTime OperationDate {
                get => this._operationDate;
                set => this._operationDate = value;
            }
            public DateTime StaffRollCallYmdHms {
                get => this._staffRollCallYmdHms;
                set => this._staffRollCallYmdHms = value;
            }
            public int StaffBelongs {
                get => this._staffBelongs;
                set => this._staffBelongs = value;
            }
            public int StaffJobForm {
                get => this._staffJobForm;
                set => this._staffJobForm = value;
            }
            public int StaffOccupation {
                get => this._staffOccupation;
                set => this._staffOccupation = value;
            }
            public int StaffCode {
                get => this._staffCode;
                set => this._staffCode = value;
            }
            public string StaffName {
                get => this._staffName;
                set => this._staffName = value;
            }
            public int SetCode {
                get => this._setCode;
                set => this._setCode = value;
            }
            public string SetName {
                get => this._setName;
                set => this._setName = value;
            }
            public string Memo {
                get => this._memo;
                set => this._memo = value;
            }
        }

        /// <summary>
        /// DateTimePickerExOperationDate1_ValueChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimePickerExOperationDate1_ValueChanged(object sender, EventArgs e) {
            if (((DateTimePickerEx)sender).Value > this.DateTimePickerExOperationDate2.GetValue()) {
                this.DateTimePickerExOperationDate2.SetValueJp(_dateUtility.GetEndOfMonth(((DateTimePickerEx)sender).GetValue()));
            }
        }

        /// <summary>
        /// DateTimePickerExOperationDate2_ValueChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimePickerExOperationDate2_ValueChanged(object sender, EventArgs e) {
            if (((DateTimePickerEx)sender).Value < this.DateTimePickerExOperationDate1.GetValue()) {
                this.DateTimePickerExOperationDate1.SetValueJp(_dateUtility.GetBeginOfMonth(((DateTimePickerEx)sender).GetValue()));
            }
        }

        /// <summary>
        /// StaffDestination_FormClosing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StaffDestination_FormClosing(object sender, FormClosingEventArgs e) {
            DialogResult dialogResult = MessageBox.Show("アプリケーションを終了します。よろしいですか？", "メッセージ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
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
