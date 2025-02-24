/*
 * 2025-02-22
 */
using ControlEx;

using Dao;

using Vo;

namespace Toukanpo {
    public partial class ToukanpoDetail : Form {
        /*
         * Dao
         */
        private readonly ToukanpoTrainingCardDao _toukanpoTrainingCardDao;
        private readonly StaffMasterDao _staffMasterDao;
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;

        public ToukanpoDetail(ConnectionVo connectionVo) {
            /*
             * Dao
             */
            _connectionVo = new();
            _toukanpoTrainingCardDao = new(connectionVo);
            _staffMasterDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            /*
             * InitializeControl
             */
            InitializeComponent();
            this.InitializeComboBoxExSelectName();

            /*
             * MenuStrip
             */
            List<string> listString = new() {
                "ToolStripMenuItemFile",
                "ToolStripMenuItemExit",
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);
            this.DateTimePickerExCertificationDate.SetEmpty();
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "Initialize Success";
            /*
             * Eventを登録する
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="staffCode"></param>
        public ToukanpoDetail(ConnectionVo connectionVo, int staffCode) {
            /*
             * Dao
             */
            _connectionVo = new();
            _toukanpoTrainingCardDao = new(connectionVo);
            _staffMasterDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            /*
             * InitializeControl
             */
            InitializeComponent();
            this.InitializeComboBoxExSelectName();
            /*
             * MenuStrip
             */
            List<string> listString = new() {
                "ToolStripMenuItemFile",
                "ToolStripMenuItemExit",
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);
            this.SetControl(_toukanpoTrainingCardDao.SelectOneToukanpoTrainingCardMaster(staffCode));
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "Initialize Success";
            /*
             * Eventを登録する
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExUpdate_Click(object sender, EventArgs e) {
            if (_selectedToukanpoTrainingCardVo == null) {
                MessageBox.Show("従業員台帳に登録されていません。リストから選択して下さい。", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } else {
                DialogResult dialogResult = MessageBox.Show("データを更新しますか？", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                switch (dialogResult) {
                    case DialogResult.OK:
                        if (_toukanpoTrainingCardDao.ExistenceToukanpoTrainingCardMaster(_selectedToukanpoTrainingCardVo.StaffCode)) {
                            try {
                                _toukanpoTrainingCardDao.UpdateOneToukanpoTrainingCardMaster(this.SetVo());
                                MessageBox.Show("修正登録を完了しました。", "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            } catch (Exception exception) {
                                MessageBox.Show(exception.Message);
                            }
                        } else {
                            try {
                                _toukanpoTrainingCardDao.InsertOneToukanpoTrainingCardMaster(this.SetVo());
                                MessageBox.Show("新規登録を完了しました。", "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            } catch (Exception exception) {
                                MessageBox.Show(exception.Message);
                            }
                        }
                        break;
                    case DialogResult.Cancel:
                        break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toukanpoTrainingCardVo"></param>
        private void SetControl(ToukanpoTrainingCardVo toukanpoTrainingCardVo) {
            if (toukanpoTrainingCardVo.StaffCode > 0) {
                this.LabelExCompany.Text = toukanpoTrainingCardVo.CompanyName;                                                  // 所属会社名
                this.ComboBoxExSelectName.Text = toukanpoTrainingCardVo.Name;                                                   // 氏名
                this.DateTimePickerExCertificationDate.SetValue(toukanpoTrainingCardVo.CertificationDate.Date);                 // 認定日
                if (toukanpoTrainingCardVo.Picture.Length != 0) {                                                               // 写真
                    this.PictureBoxEx1.Image = (Image)new ImageConverter().ConvertFrom(toukanpoTrainingCardVo.Picture);
                } else {
                    this.PictureBoxEx1.Image = null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ToukanpoTrainingCardVo SetVo() {
            ToukanpoTrainingCardVo toukanpoTrainingCardVo = new();
            toukanpoTrainingCardVo.StaffCode = _selectedStaffMasterVo.StaffCode;
            toukanpoTrainingCardVo.Name = _selectedStaffMasterVo.Name;
            toukanpoTrainingCardVo.CompanyName = this.LabelExCompany.Text;
            toukanpoTrainingCardVo.CardName = _selectedStaffMasterVo.Name;
            toukanpoTrainingCardVo.CertificationDate = this.DateTimePickerExCertificationDate.GetValue().Date;
            toukanpoTrainingCardVo.Picture = (byte[])new ImageConverter().ConvertTo(this.PictureBoxEx1.Image, typeof(byte[])); // 写真
            toukanpoTrainingCardVo.InsertPcName = Environment.MachineName;
            toukanpoTrainingCardVo.InsertYmdHms = DateTime.Now;
            toukanpoTrainingCardVo.UpdatePcName = Environment.MachineName;
            toukanpoTrainingCardVo.UpdateYmdHms = DateTime.Now;
            toukanpoTrainingCardVo.DeletePcName = Environment.MachineName;
            toukanpoTrainingCardVo.DeleteYmdHms = DateTime.Now;
            toukanpoTrainingCardVo.DeleteFlag = false;
            return toukanpoTrainingCardVo;
        }

        /// <summary>
        /// ToolStripMenuItemがクリックされた時のSourceControlを保持
        /// </summary>
        Control _sourceControl = null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStripEx1_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
            _sourceControl = ((ContextMenuStripEx)sender).SourceControl;                                                  // ContextMenuStripを表示しているコントロールを取得する
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
                /*
                 * アプリケーションを終了する
                 */
                case "ToolStripMenuItemExit":
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
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
        /// ComboBoxSelectNameで選択されたStaffLedgerVoを保持
        /// </summary>
        private StaffMasterVo _selectedStaffMasterVo = new();
        /// <summary>
        /// ComboBoxSelectNameで選択されたToukanpoTrainingCardVoを保持
        /// </summary>
        private ToukanpoTrainingCardVo _selectedToukanpoTrainingCardVo;
        /// <summary>
        /// ComboBoxSelectName_SelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxExSelectName_SelectedIndexChanged(object sender, EventArgs e) {
            _selectedStaffMasterVo = ((ComboBoxExSelectNameVo)((ComboBoxEx)sender).SelectedItem).StaffMasterVo;
            _selectedToukanpoTrainingCardVo = _toukanpoTrainingCardDao.SelectOneToukanpoTrainingCardMaster(_selectedStaffMasterVo.StaffCode);
            if (_selectedToukanpoTrainingCardVo.StaffCode > 0) {
                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "登録されています";
                // Controlに値をセット
                this.LabelExCompany.Text = _selectedToukanpoTrainingCardVo.CompanyName;
                this.DateTimePickerExCertificationDate.SetValue(_selectedToukanpoTrainingCardVo.CertificationDate.Date);
                if (_selectedToukanpoTrainingCardVo.Picture.Length != 0) {
                    this.PictureBoxEx1.Image = (Image)new ImageConverter().ConvertFrom(_selectedToukanpoTrainingCardVo.Picture);
                } else {
                    this.PictureBoxEx1.Image = null;
                }
            } else {
                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "登録されていません";
                // Controlに値をセット
                this.LabelExCompany.Text = string.Empty;
                this.DateTimePickerExCertificationDate.SetEmpty();
                this.PictureBoxEx1.Image = null;
            }
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
        private void ToukanpoDetail_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
