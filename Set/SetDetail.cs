/*
 * 2025-12-17
 */
using Dao;

using Vo;

namespace Set {
    public partial class SetDetail : Form {
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
        private readonly Dictionary<int, string> _dictionaryClassificationMaster = new();
        private readonly Dictionary<int, string> _dictionaryContactMethod = new() { { 10, "TEL" }, { 11, "FAX" }, { 12, "しない" }, { 13, "TEL/FAX" } };
        private readonly Dictionary<int, string> _dictionaryFareMaster = new();
        private readonly Dictionary<int, string> _dictionaryManagedSpaceMaster = new();
        private readonly Dictionary<int, string> _dictionaryWordMaster = new();


        /// <summary>
        /// コンストラクター(INSERT)
        /// </summary>
        /// <param name="connectionVo"></param>
        public SetDetail(ConnectionVo connectionVo) {
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
                _dictionaryClassificationMaster.Add(classificationMasterVo.Code, classificationMasterVo.Name);
            foreach (FareMasterVo fareMasterVo in _fareMasterDao.SelectAllFareMasterVo())
                _dictionaryFareMaster.Add(fareMasterVo.FareCode, fareMasterVo.FareName);
            foreach (ManagedSpaceVo managedSpaceVo in _managedSpaceDao.SelectAllManagedSpace())
                _dictionaryManagedSpaceMaster.Add(managedSpaceVo.Code, managedSpaceVo.Name);
            foreach (WordMasterVo wordMasterVo in _wordMasterDao.SelectAllWordMaster())
                _dictionaryWordMaster.Add(wordMasterVo.Code, wordMasterVo.Name);
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
        }

        /// <summary>
        /// コンストラクター(UPDATE)
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="carCode"></param>
        public SetDetail(ConnectionVo connectionVo, int setCode) {
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
                _dictionaryClassificationMaster.Add(classificationMasterVo.Code, classificationMasterVo.Name);
            foreach (FareMasterVo fareMasterVo in _fareMasterDao.SelectAllFareMasterVo())
                _dictionaryFareMaster.Add(fareMasterVo.FareCode, fareMasterVo.FareName);
            foreach (ManagedSpaceVo managedSpaceVo in _managedSpaceDao.SelectAllManagedSpace())
                _dictionaryManagedSpaceMaster.Add(managedSpaceVo.Code, managedSpaceVo.Name);
            foreach (WordMasterVo wordMasterVo in _wordMasterDao.SelectAllWordMaster())
                _dictionaryWordMaster.Add(wordMasterVo.Code, wordMasterVo.Name);
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExUpdate_Click(object sender, EventArgs e) {

        }




        /// <summary>
        /// 
        /// </summary>
        private void InitializeControl() {
            this.TextBoxExSetCode.SetEmpty();                                               // 配車先コード
            this.SetItems(_wordMasterDao.SelectAllWordMaster());                            // 配車先地区
            this.TextBoxExSetName.SetEmpty();                                               // 配車先名
            this.TextBoxExSetName1.SetEmpty();                                              // 配車先略称１
            this.TextBoxExSetName2.SetEmpty();                                              // 配車先略称２
            this.SetItems(_fareMasterDao.SelectAllFareMasterVo());                          // 運賃区分
            this.SetItems(_managedSpaceDao.SelectAllManagedSpace());                        // 車両管理地
            this.SetItems(_classificationMasterDao.SelectAllClassificationMaster());        // 分類名
            this.ComboBoxExContactMethod.DisplayClear();                                    // 代番連絡方法
            this.NumericUpDownExNumberOfPeople.Value = 0;                                   // 基本人数
            this.ComboBoxExSpareOfPeople.DisplayClear();                                    // スペアフラグ
            this.CheckBoxExMon.Checked = false;                                             // 月
            this.CheckBoxExTue.Checked = false;                                             // 火
            this.CheckBoxExWed.Checked = false;                                             // 水
            this.CheckBoxExThu.Checked = false;                                             // 木
            this.CheckBoxExFri.Checked = false;                                             // 金
            this.CheckBoxExSat.Checked = false;                                             // 土
            this.CheckBoxExSun.Checked = false;                                             // 日
            this.ComboBoxExFiveLap.DisplayClear();                                          // 第五週稼働フラグ
            this.ComboBoxExMoveFlag.DisplayClear();                                         // 移動可能フラグ
            this.TextBoxExRemarks.SetEmpty();                                               // 備考
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setMasterVo"></param>
        private void SetControl(SetMasterVo setMasterVo) {
            this.TextBoxExSetCode.Text = setMasterVo.SetCode.ToString();                                                // 配車先コード
            this.ComboBoxExWordCode.Text = _dictionaryWordMaster[setMasterVo.WordCode];                                 // 配車先地区
            this.TextBoxExSetName.Text = setMasterVo.SetName;                                                           // 配車先名
            this.TextBoxExSetName1.Text = setMasterVo.SetName1;                                                         // 配車先略称１
            this.TextBoxExSetName2.Text = setMasterVo.SetName2;                                                         // 配車先略称２
            this.ComboBoxExFareCode.Text = _dictionaryFareMaster[setMasterVo.FareCode];                                 // 運賃区分
            this.ComboBoxExManagedSpaceCode.Text = _dictionaryManagedSpaceMaster[setMasterVo.ManagedSpaceCode];         // 車両管理地
            this.ComboBoxExClassificationCode.Text = _dictionaryClassificationMaster[setMasterVo.ClassificationCode];   // 分類名
            this.ComboBoxExContactMethod.Text = _dictionaryContactMethod[setMasterVo.ContactMethod];                    // 代番連絡方法
            this.NumericUpDownExNumberOfPeople.Value = setMasterVo.NumberOfPeople;                                      // 基本人数
            this.ComboBoxExSpareOfPeople.Text = setMasterVo.SpareOfPeople.ToString();                                   // スペアフラグ
            this.CheckBoxExMon.Checked = setMasterVo.WorkingDays.Contains("月") ? true : false;                          // 月
            this.CheckBoxExTue.Checked = setMasterVo.WorkingDays.Contains("火") ? true : false;                          // 火
            this.CheckBoxExWed.Checked = setMasterVo.WorkingDays.Contains("水") ? true : false;                          // 水
            this.CheckBoxExThu.Checked = setMasterVo.WorkingDays.Contains("木") ? true : false;                          // 木
            this.CheckBoxExFri.Checked = setMasterVo.WorkingDays.Contains("金") ? true : false;                          // 金
            this.CheckBoxExSat.Checked = setMasterVo.WorkingDays.Contains("土") ? true : false;                          // 土
            this.CheckBoxExSun.Checked = setMasterVo.WorkingDays.Contains("日") ? true : false;                          // 日
            this.ComboBoxExFiveLap.Text = setMasterVo.SpareOfPeople ? "稼働する" : "稼働しない";                            // 第五週稼働フラグ
            this.ComboBoxExMoveFlag.Text = setMasterVo.SpareOfPeople ? "可能" : "不可能";                                  // 移動可能フラグ
            this.TextBoxExRemarks.Text = setMasterVo.Remarks;                                                           // 備考
        }

        private SetMasterVo SetVo() {
            SetMasterVo setMasterVo = new();



            return setMasterVo;
        }

        /// <summary>
        /// 分類コード
        /// </summary>
        /// <param name="listClassificationMasterVo"></param>
        public void SetItems(List<ClassificationMasterVo> listClassificationMasterVo) {
            this.ComboBoxExClassificationCode.DisplayMember = "Name";                                                           // 表示するプロパティ名
            this.ComboBoxExClassificationCode.ValueMember = "Code";                                                             // 値となるプロパティ名
            foreach (ClassificationMasterVo classificationMasterVo in listClassificationMasterVo) {
                this.ComboBoxExClassificationCode.Items.Add(new ComboBoxExClassificationVo(classificationMasterVo.Code, classificationMasterVo.Name));
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
                this.ComboBoxExFareCode.Items.Add(new ComboBoxExFareCodeVo(fareMasterVo.FareCode, fareMasterVo.FareName));
            }
        }

        /// <summary>
        /// 管理地コード
        /// </summary>
        /// <param name="listManagedSpaceVo"></param>
        public void SetItems(List<ManagedSpaceVo> listManagedSpaceVo) {
            this.ComboBoxExManagedSpaceCode.DisplayMember = "Name";                                                             // 表示するプロパティ名
            this.ComboBoxExManagedSpaceCode.ValueMember = "Code";                                                               // 値となるプロパティ名
            foreach (ManagedSpaceVo managedSpaceVo in listManagedSpaceVo) {
                this.ComboBoxExManagedSpaceCode.Items.Add(new ComboBoxExManagedSpaceVo(managedSpaceVo.Code, managedSpaceVo.Name));
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
                this.ComboBoxExWordCode.Items.Add(new ComboBoxExWordCodeVo(wordMasterVo.Code, wordMasterVo.Name));
            }
        }

        /// <summary>
        /// 分類用VO
        /// </summary>
        private class ComboBoxExClassificationVo {
            int _code;
            string _name;

            public ComboBoxExClassificationVo(int code, string name) {
                _code = code;
                _name = name;
            }

            public int Code {
                get => this._code;
                set => this._code = value;
            }
            public string Name {
                get => this._name;
                set => this._name = value;
            }
        }

        /// <summary>
        /// 運賃区分用VO
        /// </summary>
        private class ComboBoxExFareCodeVo {
            int _code;
            string _name;

            public ComboBoxExFareCodeVo(int code, string name) {
                _code = code;
                _name = name;
            }

            public int Code {
                get => this._code;
                set => this._code = value;
            }
            public string Name {
                get => this._name;
                set => this._name = value;
            }
        }

        /// <summary>
        /// 車庫地用VO
        /// </summary>
        private class ComboBoxExManagedSpaceVo {
            int _code;
            string _name;

            public ComboBoxExManagedSpaceVo(int code, string name) {
                _code = code;
                _name = name;
            }

            public int Code {
                get => this._code;
                set => this._code = value;
            }
            public string Name {
                get => this._name;
                set => this._name = value;
            }
        }

        /// <summary>
        /// 市区町村コード用VO
        /// </summary>
        private class ComboBoxExWordCodeVo {
            int _code;
            string _name;

            public ComboBoxExWordCodeVo(int code, string name) {
                _code = code;
                _name = name;
            }

            public int Code {
                get => this._code;
                set => this._code = value;
            }
            public string Name {
                get => this._name;
                set => this._name = value;
            }
        }
    }
}
