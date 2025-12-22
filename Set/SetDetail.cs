/*
 * 2025-12-17
 */
using Dao;

using Vo;

namespace Set {
    public partial class SetDetail : Form {
        private ErrorProvider _errorProvider;
        private int _setCode;
        private string _constructorFlag = string.Empty;
        /*
         * Dao
         */
        private readonly ClassificationMasterDao _classificationMasterDao;
        private readonly FareMasterDao _fareMasterDao;
        private readonly ManagedSpaceDao _managedSpaceDao;
        private readonly SetMasterDao _setMasterDao;
        private readonly WordMasterDao _wordMasterDao;
        /*
         * Dictionary
         */
        private readonly Dictionary<int, ClassificationMasterVo> _dictionaryClassificationMaster = new();
        private readonly Dictionary<int, string> _dictionaryContactMethod = new() { { 10, "TEL" }, { 11, "FAX" }, { 12, "しない" }, { 13, "TEL/FAX" } };
        private readonly Dictionary<int, FareMasterVo> _dictionaryFareMaster = new();
        private readonly Dictionary<int, ManagedSpaceMasterVo> _dictionaryManagedSpaceMaster = new();
        private readonly Dictionary<int, WordMasterVo> _dictionaryWordMaster = new();


        /// <summary>
        /// コンストラクター(INSERT)
        /// </summary>
        /// <param name="connectionVo"></param>
        public SetDetail(ConnectionVo connectionVo) {
            _errorProvider = new();
            _constructorFlag = "INSERT";
            /*
             * Dao
             */
            _classificationMasterDao = new(connectionVo);
            _fareMasterDao = new(connectionVo);
            _managedSpaceDao = new(connectionVo);
            _setMasterDao = new(connectionVo);
            _wordMasterDao = new(connectionVo);
            /*
             * Dictionary
             */
            foreach (ClassificationMasterVo classificationMasterVo in _classificationMasterDao.SelectAllClassificationMaster())
                _dictionaryClassificationMaster.Add(classificationMasterVo.Code, classificationMasterVo);
            foreach (FareMasterVo fareMasterVo in _fareMasterDao.SelectAllFareMasterVo())
                _dictionaryFareMaster.Add(fareMasterVo.Code, fareMasterVo);
            foreach (ManagedSpaceMasterVo managedSpaceVo in _managedSpaceDao.SelectAllManagedSpace())
                _dictionaryManagedSpaceMaster.Add(managedSpaceVo.Code, managedSpaceVo);
            foreach (WordMasterVo wordMasterVo in _wordMasterDao.SelectAllWordMaster())
                _dictionaryWordMaster.Add(wordMasterVo.Code, wordMasterVo);
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
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);

            this.InitializeControl();
            /*
             * StatusStrip
             */
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;
            /*
             * Eventを登録する
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += this.ToolStripMenuItem_Click;
        }

        /// <summary>
        /// コンストラクター(UPDATE)
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="carCode"></param>
        public SetDetail(ConnectionVo connectionVo, int setCode) {
            _errorProvider = new();
            _constructorFlag = "UPDATE";
            _setCode = setCode;
            /*
             * Dao
             */
            _classificationMasterDao = new(connectionVo);
            _fareMasterDao = new(connectionVo);
            _managedSpaceDao = new(connectionVo);
            _setMasterDao = new(connectionVo);
            _wordMasterDao = new(connectionVo);
            /*
             * Dictionary
             */
            foreach (ClassificationMasterVo classificationMasterVo in _classificationMasterDao.SelectAllClassificationMaster())
                _dictionaryClassificationMaster.Add(classificationMasterVo.Code, classificationMasterVo);
            foreach (FareMasterVo fareMasterVo in _fareMasterDao.SelectAllFareMasterVo())
                _dictionaryFareMaster.Add(fareMasterVo.Code, fareMasterVo);
            foreach (ManagedSpaceMasterVo managedSpaceVo in _managedSpaceDao.SelectAllManagedSpace())
                _dictionaryManagedSpaceMaster.Add(managedSpaceVo.Code, managedSpaceVo);
            foreach (WordMasterVo wordMasterVo in _wordMasterDao.SelectAllWordMaster())
                _dictionaryWordMaster.Add(wordMasterVo.Code, wordMasterVo);
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
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);

            this.InitializeControl();

            this.SetControl(_setMasterDao.SelectOneSetMaster(setCode));
            /*
             * StatusStrip
             */
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;
            /*
             * Eventを登録する
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += this.ToolStripMenuItem_Click;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExUpdate_Click(object sender, EventArgs e) {
            /*
             * Controlの値をVoにセットするけど、バリデートに失敗したら処理を中止。
             * 成功：VO　失敗：null
             */
            SetMasterVo setMasterVo = SetVo();
            if (setMasterVo is null)
                return;

            DialogResult dialogResult = MessageBox.Show("データを更新します。よろしいですか？", "Messsage", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            try {
                switch (dialogResult) {
                    case DialogResult.OK:
                        if (_setMasterDao.ExistenceHSetMaster(setMasterVo.SetCode)) {
                            _setMasterDao.UpdateOneSetMasterVo(SetVo());
                            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "Update Success";
                        } else {
                            _setMasterDao.InsertOneSetMasterVo(SetVo());
                            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "Insert Success";
                        }
                        Close();
                        break;
                    case DialogResult.Cancel:
                        this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "処理を中止しました。";
                        break;
                }
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemExit":                                                                                                                                           // アプリケーションを終了する
                    this.Close();
                    break;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void InitializeControl() {
            this.TextBoxExSetCode.SetEmpty();                                                                                                                                           // 配車先コード
            this.SetItems(_wordMasterDao.SelectAllWordMaster());                                                                                                                        // 配車先地区
            this.TextBoxExSetName.SetEmpty();                                                                                                                                           // 配車先名
            this.TextBoxExSetName1.SetEmpty();                                                                                                                                          // 配車先略称１
            this.TextBoxExSetName2.SetEmpty();                                                                                                                                          // 配車先略称２
            this.SetItems(_fareMasterDao.SelectAllFareMasterVo());                                                                                                                      // 運賃区分
            this.SetItems(_managedSpaceDao.SelectAllManagedSpace());                                                                                                                    // 車両管理地
            this.SetItems(_classificationMasterDao.SelectAllClassificationMaster());                                                                                                    // 分類名
            this.ComboBoxExContactMethod.DisplayClear();                                                                                                                                // 代番連絡方法
            this.NumericUpDownExNumberOfPeople.Value = 0;                                                                                                                               // 基本人数
            this.ComboBoxExSpareOfPeople.DisplayClear();                                                                                                                                // スペアフラグ
            this.CheckBoxExMon.Checked = false;                                                                                                                                         // 月
            this.CheckBoxExTue.Checked = false;                                                                                                                                         // 火
            this.CheckBoxExWed.Checked = false;                                                                                                                                         // 水
            this.CheckBoxExThu.Checked = false;                                                                                                                                         // 木
            this.CheckBoxExFri.Checked = false;                                                                                                                                         // 金
            this.CheckBoxExSat.Checked = false;                                                                                                                                         // 土
            this.CheckBoxExSun.Checked = false;                                                                                                                                         // 日
            this.ComboBoxExFiveLap.DisplayClear();                                                                                                                                      // 第五週稼働フラグ
            this.ComboBoxExMoveFlag.DisplayClear();                                                                                                                                     // 移動可能フラグ
            this.TextBoxExRemarks.SetEmpty();                                                                                                                                           // 備考
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setMasterVo"></param>
        private void SetControl(SetMasterVo setMasterVo) {
            this.TextBoxExSetCode.Text = setMasterVo.SetCode.ToString();                                                                                                                // 配車先コード
            this.ComboBoxExWordCode.SelectedIndex = this.ComboBoxExWordCode.FindStringExact(_dictionaryWordMaster[setMasterVo.WordCode].Name);                                          // 配車先地区
            this.TextBoxExSetName.Text = setMasterVo.SetName;                                                                                                                           // 配車先名
            this.TextBoxExSetName1.Text = setMasterVo.SetName1;                                                                                                                         // 配車先略称１
            this.TextBoxExSetName2.Text = setMasterVo.SetName2;                                                                                                                         // 配車先略称２
            this.ComboBoxExFareCode.SelectedIndex = this.ComboBoxExFareCode.FindStringExact(_dictionaryFareMaster[setMasterVo.FareCode].Name);                                          // 運賃区分
            this.ComboBoxExManagedSpaceCode.SelectedIndex = this.ComboBoxExManagedSpaceCode.FindStringExact(_dictionaryManagedSpaceMaster[setMasterVo.ManagedSpaceCode].Name);          // 車両管理地
            this.ComboBoxExClassificationCode.SelectedIndex = this.ComboBoxExClassificationCode.FindStringExact(_dictionaryClassificationMaster[setMasterVo.ClassificationCode].Name);  // 分類名
            this.ComboBoxExContactMethod.Text = _dictionaryContactMethod[setMasterVo.ContactMethod];                                                                                    // 代番連絡方法
            this.NumericUpDownExNumberOfPeople.Value = setMasterVo.NumberOfPeople;                                                                                                      // 基本人数
            this.ComboBoxExSpareOfPeople.Text = setMasterVo.SpareOfPeople.ToString();                                                                                                   // スペアフラグ
            this.CheckBoxExMon.Checked = setMasterVo.WorkingDays.Contains("月") ? true : false;                          // 月
            this.CheckBoxExTue.Checked = setMasterVo.WorkingDays.Contains("火") ? true : false;                          // 火
            this.CheckBoxExWed.Checked = setMasterVo.WorkingDays.Contains("水") ? true : false;                          // 水
            this.CheckBoxExThu.Checked = setMasterVo.WorkingDays.Contains("木") ? true : false;                          // 木
            this.CheckBoxExFri.Checked = setMasterVo.WorkingDays.Contains("金") ? true : false;                          // 金
            this.CheckBoxExSat.Checked = setMasterVo.WorkingDays.Contains("土") ? true : false;                          // 土
            this.CheckBoxExSun.Checked = setMasterVo.WorkingDays.Contains("日") ? true : false;                          // 日
            this.ComboBoxExFiveLap.Text = setMasterVo.FiveLap ? "稼働する" : "稼働しない";                                                                                                  // 第五週稼働フラグ
            this.ComboBoxExMoveFlag.Text = setMasterVo.MoveFlag ? "可能" : "不可能";                                                                                                      // 移動可能フラグ
            this.TextBoxExRemarks.Text = setMasterVo.Remarks;                                                                                                                           // 備考
        }

        /// <summary>
        /// Voに値をセット
        /// </summary>
        /// <returns></returns>
        private SetMasterVo SetVo() {
            /*
             * バリデート
             */
            bool errorFlag = false;
            //if (this.TextBoxExSetCode.Text.Length < 7) {
            //    this._errorProvider.SetError(this.TextBoxExSetCode, "配車先コードが正しくありません。");
            //    errorFlag = true;
            //}
            if (this.ComboBoxExWordCode.SelectedIndex < 0) {
                this._errorProvider.SetError(this.ComboBoxExWordCode, "地区を選択して下さい。");
                errorFlag = true;
            }
            if (this.ComboBoxExFareCode.SelectedIndex < 0) {
                this._errorProvider.SetError(this.ComboBoxExFareCode, "運賃区分を選択して下さい。");
                errorFlag = true;
            }
            if (this.ComboBoxExManagedSpaceCode.SelectedIndex < 0) {
                this._errorProvider.SetError(this.ComboBoxExManagedSpaceCode, "車両管理地を選択して下さい。");
                errorFlag = true;
            }
            if (this.ComboBoxExClassificationCode.SelectedIndex < 0) {
                this._errorProvider.SetError(this.ComboBoxExClassificationCode, "分類を選択して下さい。");
                errorFlag = true;
            }
            if (this.ComboBoxExContactMethod.SelectedIndex < 0) {
                this._errorProvider.SetError(this.ComboBoxExContactMethod, "代番連絡方法を選択して下さい。");
                errorFlag = true;
            }
            if (this.ComboBoxExSpareOfPeople.SelectedIndex < 0) {
                this._errorProvider.SetError(this.ComboBoxExSpareOfPeople, "スペアフラグを選択して下さい。");
                errorFlag = true;
            }
            if (this.ComboBoxExFiveLap.SelectedIndex < 0) {
                this._errorProvider.SetError(this.ComboBoxExFiveLap, "第五週稼働フラグを選択して下さい。");
                errorFlag = true;
            }
            if (this.ComboBoxExMoveFlag.SelectedIndex < 0) {
                this._errorProvider.SetError(this.ComboBoxExMoveFlag, "移動可能フラグを選択して下さい。");
                errorFlag = true;
            }
            if (errorFlag)
                return null;

            /*
             * Voに値をセット
             */
            SetMasterVo setMasterVo = new();
            switch (_constructorFlag) {
                case "INSERT":
                    setMasterVo.SetCode = _setMasterDao.GetSetCode(((ComboBoxExWordCodeVo)this.ComboBoxExWordCode.SelectedItem).WordMasterVo.Code) + 1;                                 // 配車先コード自動採番
                    break;
                case "UPDATE":
                    setMasterVo.SetCode = _setCode;                                                                                                                                     // 配車先コード
                    break;
            }
            //setMasterVo.SetCode = int.Parse(this.TextBoxExSetCode.Text);                                                                                                                // 配車先コード
            setMasterVo.WordCode = ((ComboBoxExWordCodeVo)this.ComboBoxExWordCode.SelectedItem).Code;                                                                                   // 配車先地区
            setMasterVo.SetName = this.TextBoxExSetName.Text;                                                                                                                           // 配車先名
            setMasterVo.SetName1 = this.TextBoxExSetName1.Text;                                                                                                                         // 配車先略称１
            setMasterVo.SetName2 = this.TextBoxExSetName2.Text;                                                                                                                         // 配車先略称２
            setMasterVo.FareCode = ((ComboBoxExFareCodeVo)this.ComboBoxExFareCode.SelectedItem).Code;                                                                                   // 運賃区分
            setMasterVo.ManagedSpaceCode = ((ComboBoxExManagedSpaceVo)this.ComboBoxExManagedSpaceCode.SelectedItem).Code;                                                               // 車両管理地
            setMasterVo.ClassificationCode = ((ComboBoxExClassificationVo)this.ComboBoxExClassificationCode.SelectedItem).Code;                                                         // 分類名
            switch (this.ComboBoxExContactMethod.Text) {
                case "TEL":
                    setMasterVo.ContactMethod = 10;
                    break;
                case "FAX":
                    setMasterVo.ContactMethod = 11;
                    break;
                case "しない":
                    setMasterVo.ContactMethod = 12;
                    break;
                case "TEL/FAX":
                    setMasterVo.ContactMethod = 13;
                    break;
            }
            setMasterVo.NumberOfPeople = (int)this.NumericUpDownExNumberOfPeople.Value;                                                                                                 // 基本人数
            switch (this.ComboBoxExSpareOfPeople.Text) {                                                                                                                                // スペアフラグ
                case "True":
                    setMasterVo.SpareOfPeople = true;
                    break;
                case "False":
                    setMasterVo.SpareOfPeople = false;
                    break;
            }
            string _workingDays = string.Empty;                                                                                                                                         // 稼働曜日
            if (this.CheckBoxExMon.Checked)
                _workingDays += "月";
            if (this.CheckBoxExTue.Checked)
                _workingDays += "火";
            if (this.CheckBoxExWed.Checked)
                _workingDays += "水";
            if (this.CheckBoxExThu.Checked)
                _workingDays += "木";
            if (this.CheckBoxExFri.Checked)
                _workingDays += "金";
            if (this.CheckBoxExSat.Checked)
                _workingDays += "土";
            if (this.CheckBoxExSun.Checked)
                _workingDays += "日";
            setMasterVo.WorkingDays = _workingDays;
            setMasterVo.FiveLap = this.ComboBoxExFiveLap.Text == "稼働する" ? true : false;                                                                                             // 第五週稼働フラグ
            setMasterVo.MoveFlag = this.ComboBoxExMoveFlag.Text == "可能" ? true : false;                                                                                          // 移動可能フラグ
            setMasterVo.Remarks = this.TextBoxExRemarks.Text;                                                                                                                          // 備考
            return setMasterVo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetDetail_FormClosing(object sender, FormClosingEventArgs e) {

        }

        /// <summary>
        /// 分類コード
        /// </summary>
        /// <param name="listClassificationMasterVo"></param>
        public void SetItems(List<ClassificationMasterVo> listClassificationMasterVo) {
            this.ComboBoxExClassificationCode.DisplayMember = "Name";                                                           // 表示するプロパティ名
            this.ComboBoxExClassificationCode.ValueMember = "Code";                                                             // 値となるプロパティ名
            foreach (ClassificationMasterVo classificationMasterVo in listClassificationMasterVo) {
                this.ComboBoxExClassificationCode.Items.Add(new ComboBoxExClassificationVo(classificationMasterVo.Code, classificationMasterVo.Name, classificationMasterVo));
            }
        }

        /// <summary>
        /// 運賃支払いコード
        /// </summary>
        /// <param name="listFareMasterVo"></param>
        public void SetItems(List<FareMasterVo> listFareMasterVo) {
            this.ComboBoxExFareCode.DisplayMember = "Name";                                                                     // 表示するプロパティ名
            this.ComboBoxExFareCode.ValueMember = "Code";                                                                       // 値となるプロパティ名
            foreach (FareMasterVo fareMasterVo in listFareMasterVo) {
                this.ComboBoxExFareCode.Items.Add(new ComboBoxExFareCodeVo(fareMasterVo.Code, fareMasterVo.Name, fareMasterVo));
            }
        }

        /// <summary>
        /// 管理地コード
        /// </summary>
        /// <param name="listManagedSpaceVo"></param>
        public void SetItems(List<ManagedSpaceMasterVo> listManagedSpaceVo) {
            this.ComboBoxExManagedSpaceCode.DisplayMember = "Name";                                                             // 表示するプロパティ名
            this.ComboBoxExManagedSpaceCode.ValueMember = "Code";                                                               // 値となるプロパティ名
            foreach (ManagedSpaceMasterVo managedSpaceVo in listManagedSpaceVo) {
                this.ComboBoxExManagedSpaceCode.Items.Add(new ComboBoxExManagedSpaceVo(managedSpaceVo.Code, managedSpaceVo.Name, managedSpaceVo));
            }
        }

        /// <summary>
        /// 市区町村コード
        /// </summary>
        /// <param name="listWordMasterVo"></param>
        public void SetItems(List<WordMasterVo> listWordMasterVo) {
            this.ComboBoxExWordCode.DisplayMember = "Name";                                                                     // 表示するプロパティ名
            this.ComboBoxExWordCode.ValueMember = "Code";                                                                       // 値となるプロパティ名
            foreach (WordMasterVo wordMasterVo in listWordMasterVo) {
                this.ComboBoxExWordCode.Items.Add(new ComboBoxExWordCodeVo(wordMasterVo.Code, wordMasterVo.Name, wordMasterVo));
            }
        }

        /// <summary>
        /// 分類用VO
        /// </summary>
        private class ComboBoxExClassificationVo {
            int _code;
            string _name;
            ClassificationMasterVo _classificationMasterVo;
            public ComboBoxExClassificationVo(int code, string name, ClassificationMasterVo classificationMasterVo) {
                _code = code;
                _name = name;
                _classificationMasterVo = classificationMasterVo;
            }
            public int Code {
                get => this._code;
                set => this._code = value;
            }
            public string Name {
                get => this._name;
                set => this._name = value;
            }
            public ClassificationMasterVo ClassificationMasterVo {
                get => this._classificationMasterVo;
                set => this._classificationMasterVo = value;
            }
        }

        /// <summary>
        /// 運賃区分用VO
        /// </summary>
        private class ComboBoxExFareCodeVo {
            int _code;
            string _name;
            FareMasterVo _fareMasterVo;
            public ComboBoxExFareCodeVo(int code, string name, FareMasterVo fareMasterVo) {
                _code = code;
                _name = name;
                _fareMasterVo = fareMasterVo;
            }
            public int Code {
                get => this._code;
                set => this._code = value;
            }
            public string Name {
                get => this._name;
                set => this._name = value;
            }
            public FareMasterVo FareMasterVo {
                get => this._fareMasterVo;
                set => this._fareMasterVo = value;
            }
        }

        /// <summary>
        /// 車庫地用VO
        /// </summary>
        private class ComboBoxExManagedSpaceVo {
            int _code;
            string _name;
            ManagedSpaceMasterVo _managedSpaceVo;
            public ComboBoxExManagedSpaceVo(int code, string name, ManagedSpaceMasterVo managedSpaceVo) {
                _code = code;
                _name = name;
                _managedSpaceVo = managedSpaceVo;
            }
            public int Code {
                get => this._code;
                set => this._code = value;
            }
            public string Name {
                get => this._name;
                set => this._name = value;
            }
            public ManagedSpaceMasterVo ManagedSpaceVo {
                get => this._managedSpaceVo;
                set => this._managedSpaceVo = value;
            }
        }

        /// <summary>
        /// 市区町村コード用VO
        /// </summary>
        private class ComboBoxExWordCodeVo {
            int _code;
            string _name;
            WordMasterVo _wordMasterVo;
            public ComboBoxExWordCodeVo(int code, string name, WordMasterVo wordMasterVo) {
                _code = code;
                _name = name;
                _wordMasterVo = wordMasterVo;
            }
            public int Code {
                get => this._code;
                set => this._code = value;
            }
            public string Name {
                get => this._name;
                set => this._name = value;
            }
            public WordMasterVo WordMasterVo {
                get => this._wordMasterVo;
                set => this._wordMasterVo = value;
            }
        }
    }
}
