/*
 * 2025-02-19
 */
using System.Drawing.Printing;

using ControlEx;

using Dao;

using Vo;

namespace License {
    public partial class LicenseDetail : Form {
        /*
         * Dao
         */
        private readonly LicenseMasterDao _licenseMasterDao;
        private readonly StaffMasterDao _staffMasterDao;
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクター(INSERT)
        /// </summary>
        /// <param name="connectionVo"></param>
        public LicenseDetail(ConnectionVo connectionVo) {
            /*
             * Dao
             */
            _licenseMasterDao = new(connectionVo);
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
            this.ComboBoxExSelectName.Enabled = true;
            this.InitializeControl();
            /*
             * MenuStrip
             */
            List<string> listString = new() {
                "ToolStripMenuItemFile",
                "ToolStripMenuItemExit",
                "ToolStripMenuItemPrint",
                "ToolStripMenuItemPrintB5",
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);

            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "Initialize Success";
            /*
             * Eventを登録する
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// コンストラクター(UPDATE)
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="staffCode"></param>
        public LicenseDetail(ConnectionVo connectionVo, int staffCode) {
            /*
             * Dao
             */
            _licenseMasterDao = new(connectionVo);
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
            this.InitializeControl();
            this.ComboBoxExSelectName.Enabled = false;
            this.SetControl(_licenseMasterDao.SelectOneLicenseMaster(staffCode));
            /*
             * MenuStrip
             */
            List<string> listString = new() {
                "ToolStripMenuItemFile",
                "ToolStripMenuItemExit",
                "ToolStripMenuItemPrint",
                "ToolStripMenuItemPrintB5",
                "ToolStripMenuItemHelp"
            };
            MenuStripEx1.ChangeEnable(listString);

            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "Initialize Success";
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
        private void ButtonExUpdate_Click(object sender, EventArgs e) {
            DialogResult dialogResult = MessageBox.Show("データを更新します。よろしいですか？", "メッセージ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            switch (dialogResult) {
                case DialogResult.OK:
                    try {
                        LicenseMasterVo licenseMasterVo = SetLicenseMasterVo();
                        if (_licenseMasterDao.ExistenceLicenseMaster(licenseMasterVo.StaffCode)) {
                            _licenseMasterDao.UpdateOneLicenseLedger(licenseMasterVo);
                        } else {
                            _licenseMasterDao.InsertOneLicenseMaster(licenseMasterVo);
                        }
                        this.Close();
                        break;
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case DialogResult.Cancel:
                    break;
            }
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
                    ((CcPictureBox)_sourceControl).Image = (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap);
                    break;
                case "ToolStripMenuItemDelete":                                                                         // Picture Delete
                    ((CcPictureBox)_sourceControl).Image = null;
                    break;
                case "ToolStripMenuItemPrintB5":
                    PrintDocument printDocument = new();
                    printDocument.PrintPage += PrintPage;
                    printDocument.Print();
                    break;
                case "ToolStripMenuItemExit":                                                                           // アプリケーションを終了する
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// InitializeControl
        /// </summary>
        private void InitializeControl() {
            this.ComboBoxExSelectName.SelectedIndex = -1;
            this.TextBoxExStaffCode.SetEmpty();
            this.TextBoxExNameKana.SetEmpty();
            this.TextBoxExName.SetEmpty();
            this.DateTimePickerExBirthDate.SetEmpty();
            this.TextBoxExCurrentAddress.SetEmpty();
            this.DateTimePickerExDeliveryDate.SetEmpty();
            this.DateTimePickerExExpirationDate.SetEmpty();
            this.InitializeComboBoxExLicenseCondition();
            this.TextBoxExLicenseNumber.SetEmpty();
            this.DateTimePickerExGetDate1.SetEmpty();
            this.DateTimePickerExGetDate2.SetEmpty();
            this.DateTimePickerExGetDate3.SetEmpty();
            this.CheckBoxExLarge.Checked = false;
            this.CheckBoxExMedium.Checked = false;
            this.CheckBoxExQuasiMedium.Checked = false;
            this.CheckBoxExOrdinary.Checked = false;
            this.CheckBoxExBigSpecial.Checked = false;
            this.CheckBoxExBigAutoBike.Checked = false;
            this.CheckBoxExOrdinaryAutoBike.Checked = false;
            this.CheckBoxExSmallSpecial.Checked = false;
            this.CheckBoxExWithARaw.Checked = false;
            this.CheckBoxExBigTwo.Checked = false;
            this.CheckBoxExMediumTwo.Checked = false;
            this.CheckBoxExOrdinaryTwo.Checked = false;
            this.CheckBoxExBigSpecialTwo.Checked = false;
            this.CheckBoxExTraction.Checked = false;
            this.PictureBoxEx1.SetEmpty();
            this.PictureBoxEx2.SetEmpty();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="licenseMasterVo"></param>
        private void SetControl(LicenseMasterVo licenseMasterVo) {
            this.TextBoxExStaffCode.Text = licenseMasterVo.StaffCode.ToString("#####");                                 // 社員コード
            this.TextBoxExNameKana.Text = licenseMasterVo.NameKana;                                                     // フリガナ
            this.TextBoxExName.Text = licenseMasterVo.Name;                                                             // 氏名
            this.DateTimePickerExBirthDate.SetValueJp(licenseMasterVo.BirthDate.Date);                                  // 生年月日
            this.TextBoxExCurrentAddress.Text = licenseMasterVo.CurrentAddress;                                         // 住所
            this.DateTimePickerExDeliveryDate.SetValueJp(licenseMasterVo.DeliveryDate.Date);                            // 交付
            this.DateTimePickerExExpirationDate.SetValueJp(licenseMasterVo.ExpirationDate.Date);                        // 有効期限
            this.ComboBoxExLicenseCondition.Text = licenseMasterVo.LicenseCondition;                                    // 条件等
            this.TextBoxExLicenseNumber.Text = licenseMasterVo.LicenseNumber;                                           // 番号
            this.DateTimePickerExGetDate1.SetValueJp(licenseMasterVo.GetDate1.Date);                                    // 二・小・原
            this.DateTimePickerExGetDate2.SetValueJp(licenseMasterVo.GetDate2.Date);                                    // 他
            this.DateTimePickerExGetDate3.SetValueJp(licenseMasterVo.GetDate3.Date);                                    // 二種
            this.CheckBoxExLarge.Checked = licenseMasterVo.Large; //
            this.CheckBoxExMedium.Checked = licenseMasterVo.Medium; //
            this.CheckBoxExQuasiMedium.Checked = licenseMasterVo.QuasiMedium; //
            this.CheckBoxExOrdinary.Checked = licenseMasterVo.Ordinary; //
            this.CheckBoxExBigSpecial.Checked = licenseMasterVo.BigSpecial; //
            this.CheckBoxExBigAutoBike.Checked = licenseMasterVo.BigAutoBike; //
            this.CheckBoxExOrdinaryAutoBike.Checked = licenseMasterVo.OrdinaryAutoBike; //
            this.CheckBoxExSmallSpecial.Checked = licenseMasterVo.SmallSpecial; //
            this.CheckBoxExWithARaw.Checked = licenseMasterVo.WithARaw; //
            this.CheckBoxExBigTwo.Checked = licenseMasterVo.BigTwo; //
            this.CheckBoxExMediumTwo.Checked = licenseMasterVo.MediumTwo; //
            this.CheckBoxExOrdinaryTwo.Checked = licenseMasterVo.OrdinaryTwo; //
            this.CheckBoxExBigSpecialTwo.Checked = licenseMasterVo.BigSpecialTwo; //
            this.CheckBoxExTraction.Checked = licenseMasterVo.Traction; //
            if (licenseMasterVo.PictureHead.Length > 0) {
                ImageConverter imageConv = new();
                this.PictureBoxEx1.Image = (Image)imageConv.ConvertFrom(licenseMasterVo.PictureHead);                   //写真表
            }
            if (licenseMasterVo.PictureTail.Length > 0) {
                ImageConverter imageConv = new();
                this.PictureBoxEx2.Image = (Image)imageConv.ConvertFrom(licenseMasterVo.PictureTail);                   //写真裏
            }
        }

        private LicenseMasterVo SetLicenseMasterVo() {
            LicenseMasterVo licenseMasterVo = new();
            licenseMasterVo.StaffCode = int.Parse(this.TextBoxExStaffCode.Text);                                        // 社員コード
            licenseMasterVo.NameKana = this.TextBoxExNameKana.Text;                                                     // フリガナ
            licenseMasterVo.Name = this.TextBoxExName.Text;                                                             // 氏名
            licenseMasterVo.BirthDate = this.DateTimePickerExBirthDate.GetValue();                                      // 生年月日
            licenseMasterVo.CurrentAddress = this.TextBoxExCurrentAddress.Text;                                         // 住所
            licenseMasterVo.DeliveryDate = this.DateTimePickerExDeliveryDate.GetValue();                                // 交付
            licenseMasterVo.ExpirationDate = this.DateTimePickerExExpirationDate.GetValue();                            // 有効期限
            licenseMasterVo.LicenseCondition = this.ComboBoxExLicenseCondition.Text;                                    // 条件等
            licenseMasterVo.LicenseNumber = this.TextBoxExLicenseNumber.Text;                                           // 番号
            licenseMasterVo.GetDate1 = this.DateTimePickerExGetDate1.GetValue();                                        // 二・小・原
            licenseMasterVo.GetDate2 = this.DateTimePickerExGetDate2.GetValue();                                        // 他
            licenseMasterVo.GetDate3 = this.DateTimePickerExGetDate3.GetValue();                                        // 二種
            licenseMasterVo.Large = this.CheckBoxExLarge.Checked;                                                       //
            licenseMasterVo.Medium = this.CheckBoxExMedium.Checked;                                                     //
            licenseMasterVo.QuasiMedium = this.CheckBoxExQuasiMedium.Checked;                                           //
            licenseMasterVo.Ordinary = this.CheckBoxExOrdinary.Checked;                                                 //
            licenseMasterVo.BigSpecial = this.CheckBoxExBigSpecial.Checked;                                             //
            licenseMasterVo.BigAutoBike = this.CheckBoxExBigAutoBike.Checked;                                           //
            licenseMasterVo.OrdinaryAutoBike = this.CheckBoxExOrdinaryAutoBike.Checked;                                 //
            licenseMasterVo.SmallSpecial = this.CheckBoxExSmallSpecial.Checked;                                         //
            licenseMasterVo.WithARaw = this.CheckBoxExWithARaw.Checked;                                                 //
            licenseMasterVo.BigTwo = this.CheckBoxExBigTwo.Checked;                                                     //
            licenseMasterVo.MediumTwo = this.CheckBoxExMediumTwo.Checked;                                               //
            licenseMasterVo.OrdinaryTwo = this.CheckBoxExOrdinaryTwo.Checked;                                           //
            licenseMasterVo.BigSpecialTwo = this.CheckBoxExBigSpecialTwo.Checked;                                       //
            licenseMasterVo.Traction = this.CheckBoxExTraction.Checked;                                                 //
            licenseMasterVo.PictureHead = (byte[])new ImageConverter().ConvertTo(this.PictureBoxEx1.Image, typeof(byte[])); //
            licenseMasterVo.PictureTail = (byte[])new ImageConverter().ConvertTo(this.PictureBoxEx2.Image, typeof(byte[])); //
            return licenseMasterVo;
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxExSelectName_SelectedIndexChanged(object sender, EventArgs e) {
            StaffMasterVo hStaffMasterVo = ((ComboBoxExSelectNameVo)((ComboBoxEx)sender).SelectedItem).StaffMasterVo;
            /*
             * StaffLedgerVoの値をControlにセットする
             */
            this.TextBoxExStaffCode.Text = hStaffMasterVo.StaffCode.ToString();
            this.TextBoxExNameKana.Text = hStaffMasterVo.NameKana;
            this.TextBoxExName.Text = hStaffMasterVo.Name;
            this.DateTimePickerExBirthDate.SetValueJp(hStaffMasterVo.BirthDate);
            this.TextBoxExCurrentAddress.Text = hStaffMasterVo.CurrentAddress;
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

        private void InitializeComboBoxExLicenseCondition() {
            this.ComboBoxExLicenseCondition.Items.Clear();
            foreach (string licenseCondition in _licenseMasterDao.SelectGroupLicenseCondition())
                this.ComboBoxExLicenseCondition.Items.Add(licenseCondition);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintPage(object sender, PrintPageEventArgs e) {
            Bitmap bitmapHead = new(this.PictureBoxEx1.Width, this.PictureBoxEx1.Height);                                // 表のBitmapを作成
            Bitmap bitmapTail = new(this.PictureBoxEx2.Width, this.PictureBoxEx2.Height);                                // 裏のBitmapを作成
            this.PictureBoxEx1.DrawToBitmap(bitmapHead, new Rectangle(0, 0, bitmapHead.Width, bitmapHead.Height));
            this.PictureBoxEx2.DrawToBitmap(bitmapTail, new Rectangle(0, 0, bitmapTail.Width, bitmapTail.Height));

            e.Graphics.DrawImage(bitmapHead, 0, 0, 342, 222);                                                       // 表を描画
            e.Graphics.DrawImage(bitmapTail, 0, 250, 342, 222);                                                     // 裏を描画

            e.HasMorePages = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LicenseDetail_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
