/*
 * 2025-05-10
 */
using ControlEx;

using Dao;

using Vo;

namespace StatusOfResidence {
    public partial class StatusOfResidenceDetail : Form {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        /*
         * Dao
         */
        private readonly StaffMasterDao _staffMasterDao;
        private readonly StatusOfResidenceMasterDao _statusOfResidenceMasterDao;
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        public StatusOfResidenceDetail(ConnectionVo connectionVo) {
            /*
             * Dao
             */
            _staffMasterDao = new(connectionVo);
            _statusOfResidenceMasterDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            /*
             * InitializeControl
             */
            InitializeComponent();
            this.InitializeControl();
            /*
             * MenuStrip
             */
            List<string> listString = new() {
                "ToolStripMenuItemFile",
                "ToolStripMenuItemExit",
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;

            this.ComboBoxExSelectName.Enabled = true;

            this.InitializeComboBoxExSelectName();

            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;
        }

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="staffCode"></param>
        public StatusOfResidenceDetail(ConnectionVo connectionVo, int staffCode) {
            /*
             * Dao
             */
            _staffMasterDao = new(connectionVo);
            _statusOfResidenceMasterDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            /*
             * InitializeControl
             */
            InitializeComponent();
            this.InitializeControl();
            /*
             * MenuStrip
             */
            List<string> listString = new() {
                "ToolStripMenuItemFile",
                "ToolStripMenuItemExit",
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;

            this.ComboBoxExSelectName.Enabled = false;

            this.PutControl(staffCode);

            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;
        }

        /// <summary>
        /// ToolStripMenuItemがクリックされた時のSourceControlを保持
        /// </summary>
        Control _sourceControl = null;
        private void ContextMenuStripEx1_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
            _sourceControl = ((ContextMenuStrip)sender).SourceControl;                                                  // ContextMenuStripを表示しているコントロールを取得する
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemClip":                                                                           // Picture Clip
                    ((PictureBoxEx)_sourceControl).Image = (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap);
                    break;
                case "ToolStripMenuItemDelete":                                                                         // Picture Delete
                    ((PictureBoxEx)_sourceControl).Image = null;
                    break;
                case "ToolStripMenuItemPrintB5":
                    //PrintDocument printDocument = new();
                    //printDocument.PrintPage += PrintPage;
                    //printDocument.Print();
                    break;
                case "ToolStripMenuItemExit":                                                                           // アプリケーションを終了する
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEx_Click(object sender, EventArgs e) {
            switch (((ButtonEx)sender).Name) {
                case "ButtonExUpdate":
                    DialogResult dialogResult = MessageBox.Show("データを更新します。よろしいですか？", "メッセージ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    switch (dialogResult) {
                        case DialogResult.OK:
                            try {
                                int.TryParse(this.LabelExStaffCode.Text, out int staffCode);
                                if (_statusOfResidenceMasterDao.ExistenceHStatusOfResidenceMaster(staffCode)) {
                                    try {
                                        _statusOfResidenceMasterDao.UpdateOneStatusOfResidenceMaster(this.SetVo());
                                    } catch (Exception exception) {
                                        MessageBox.Show(exception.Message);
                                    }
                                    this.Close();
                                } else {
                                    try {
                                        _statusOfResidenceMasterDao.InsertOneStatusOfResidenceMaster(this.SetVo());
                                    } catch (Exception exception) {
                                        MessageBox.Show(exception.Message);
                                    }
                                    this.Close();
                                }
                            } catch (Exception exception) {
                                MessageBox.Show(exception.Message);
                            }
                            break;
                        case DialogResult.Cancel:
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// InitializeControl
        /// </summary>
        private void InitializeControl() {
            ComboBoxExSelectName.Enabled = false;
            ComboBoxExSelectName.Text = string.Empty;
            LabelExStaffCode.Text = string.Empty;
            LabelExNameKana.Text = string.Empty;
            TextBoxExNameKana.Text = string.Empty;
            TextBoxExName.Text = string.Empty;
            DateTimePickerExBirthDate.SetEmpty();
            ComboBoxExGender.SelectedIndex = -1;
            ComboBoxExCompany.SelectedIndex = -1;
            TextBoxExAddress.Text = string.Empty;
            ComboBoxExStatusOfResidence.Text = string.Empty;
            ComboBoxExWorkLimit.Text = string.Empty;
            DateTimePickerExPeriodDate.SetEmpty();
            DateTimePickerExDeadlineDate.SetEmpty();
            PictureBoxEx1.Image = null;
            PictureBoxEx2.Image = null;
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;
        }

        /// <summary>
        /// PutControl
        /// </summary>
        /// <param name="staffCode"></param>
        private void PutControl(int staffCode) {
            StaffMasterVo staffMasterVo = _staffMasterDao.SelectOneStaffMaster(staffCode);
            StatusOfResidenceMasterVo statusOfResidenceMasterVo = _statusOfResidenceMasterDao.SelectOneStatusOfResidenceMasterP(staffCode);
            ComboBoxExSelectName.Text = string.Empty;
            LabelExStaffCode.Text = staffMasterVo.StaffCode.ToString("#####");
            LabelExNameKana.Text = staffMasterVo.DisplayName;
            TextBoxExNameKana.Text = statusOfResidenceMasterVo.StaffNameKana;
            TextBoxExName.Text = statusOfResidenceMasterVo.StaffName;
            DateTimePickerExBirthDate.SetValue(statusOfResidenceMasterVo.BirthDate);
            ComboBoxExGender.Text = statusOfResidenceMasterVo.Gender;
            ComboBoxExCompany.Text = statusOfResidenceMasterVo.Nationality;
            TextBoxExAddress.Text = statusOfResidenceMasterVo.Address;
            ComboBoxExStatusOfResidence.Text = statusOfResidenceMasterVo.StatusOfResidence;
            ComboBoxExWorkLimit.Text = statusOfResidenceMasterVo.WorkLimit;
            DateTimePickerExPeriodDate.SetValue(statusOfResidenceMasterVo.PeriodDate);
            DateTimePickerExDeadlineDate.SetValue(statusOfResidenceMasterVo.DeadlineDate);
            if (statusOfResidenceMasterVo.PictureHead.Length != 0) {
                ImageConverter imageConverter = new();
                PictureBoxEx1.Image = (Image)imageConverter.ConvertFrom(statusOfResidenceMasterVo.PictureHead); // 写真
            }
            if (statusOfResidenceMasterVo.PictureTail.Length != 0) {
                ImageConverter imageConverter = new();
                PictureBoxEx2.Image = (Image)imageConverter.ConvertFrom(statusOfResidenceMasterVo.PictureTail); // 写真
            }
        }

        /// <summary>
        /// SetVo
        /// </summary>
        /// <returns></returns>
        private StatusOfResidenceMasterVo SetVo() {
            StatusOfResidenceMasterVo statusOfResidenceMasterVo = new();
            statusOfResidenceMasterVo.StaffCode = int.Parse(LabelExStaffCode.Text);
            statusOfResidenceMasterVo.StaffNameKana = TextBoxExNameKana.Text;
            statusOfResidenceMasterVo.StaffName = TextBoxExName.Text;
            statusOfResidenceMasterVo.BirthDate = DateTimePickerExBirthDate.GetValue();
            statusOfResidenceMasterVo.Gender = ComboBoxExGender.Text;
            statusOfResidenceMasterVo.Nationality = ComboBoxExCompany.Text;
            statusOfResidenceMasterVo.Address = TextBoxExAddress.Text;
            statusOfResidenceMasterVo.StatusOfResidence = ComboBoxExStatusOfResidence.Text;
            statusOfResidenceMasterVo.WorkLimit = ComboBoxExWorkLimit.Text;
            statusOfResidenceMasterVo.PeriodDate = DateTimePickerExPeriodDate.GetValue();
            statusOfResidenceMasterVo.DeadlineDate = DateTimePickerExDeadlineDate.GetValue();
            statusOfResidenceMasterVo.PictureHead = (byte[])new ImageConverter().ConvertTo(PictureBoxEx1.Image, typeof(byte[])); // 写真
            statusOfResidenceMasterVo.PictureTail = (byte[])new ImageConverter().ConvertTo(PictureBoxEx2.Image, typeof(byte[])); // 写真
            statusOfResidenceMasterVo.InsertPcName = string.Empty;
            statusOfResidenceMasterVo.InsertYmdHms = _defaultDateTime;
            statusOfResidenceMasterVo.UpdatePcName = string.Empty;
            statusOfResidenceMasterVo.UpdateYmdHms = _defaultDateTime;
            statusOfResidenceMasterVo.DeletePcName = string.Empty;
            statusOfResidenceMasterVo.DeleteYmdHms = _defaultDateTime;
            statusOfResidenceMasterVo.DeleteFlag = false;
            return statusOfResidenceMasterVo;
        }

        private void InitializeComboBoxExSelectName() {
            this.ComboBoxExSelectName.Items.Clear();
            List<ComboBoxExSelectNameVo> listComboBoxSelectNameVo = new();
            foreach (StaffMasterVo staffMasterVo in _staffMasterDao.SelectAllStaffMaster(new List<int> { 10, 11, 12, 14, 15, 22, 99 },      // 役員・社員・アルバイト・嘱託雇用契約社員・パートタイマー・労供・指定なし
                                                                                         new List<int> { 20, 21, 22, 23, 99 },              // 労供長期・労供短期・指定なし
                                                                                         new List<int> { 10, 11, 12, 13, 20, 99 },          // 運転手・作業員・自転車駐輪場・リサイクルセンター・事務員・指定なし
                                                                                         false).FindAll(x => x.RetirementFlag == false).OrderBy(x => x.NameKana)) {
                this.ComboBoxExSelectName.Items.Add(new ComboBoxExSelectNameVo(staffMasterVo.Name, staffMasterVo));
            }
            this.ComboBoxExSelectName.DisplayMember = "Name";
            // ここでイベント追加しないと初期化で発火しちゃうよ
            this.ComboBoxExSelectName.SelectedIndexChanged += new EventHandler(ComboBoxExSelectName_SelectedIndexChanged);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxExSelectName_SelectedIndexChanged(object sender, EventArgs e) {
            StaffMasterVo staffMasterVo = ((ComboBoxExSelectNameVo)((ComboBoxEx)sender).SelectedItem).StaffMasterVo;
            /*
             * StaffLedgerVoの値をControlにセットする
             */
            this.LabelExStaffCode.Text = staffMasterVo.StaffCode.ToString();
            this.LabelExNameKana.Text = staffMasterVo.NameKana;
            this.TextBoxExNameKana.Text = staffMasterVo.NameKana;
            this.TextBoxExName.Text = staffMasterVo.Name;
            this.DateTimePickerExBirthDate.SetValueJp(staffMasterVo.BirthDate);
            this.ComboBoxExGender.Text = staffMasterVo.Gender;
            this.TextBoxExAddress.Text = staffMasterVo.CurrentAddress;
        }

        /// <summary>
        /// インナークラス
        /// </summary>
        private class ComboBoxExSelectNameVo {
            private string _name;
            private StaffMasterVo _staffMasterVo;

            // プロパティをコンストラクタでセット
            public ComboBoxExSelectNameVo(string name, StaffMasterVo staffMasterVo) {
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusOfResidenceDetail_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
