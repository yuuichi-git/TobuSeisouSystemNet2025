/*
 * 2025-05-23
 */
using Common;

using ControlEx;

using Dao;

using Vo;

namespace Accident {
    public partial class AccidentDetail : Form {
        private ErrorProvider _errorProvider = new();
        private CcPictureBox[] _arrayPictureBoxEx = new CcPictureBox[5];
        /*
         * Dao
         */
        private readonly CarAccidentMasterDao _carAccidentMasterDao;
        private readonly CarMasterDao _carMasterDao;
        private readonly StaffMasterDao _staffMasterDao;
        /*
         * Vo
         */
        private CarAccidentMasterVo _carAccidentMasterVo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="carAccidentMasterVo">carAccidentMasterVo:修正登録　Null:新規登録</param>
        public AccidentDetail(ConnectionVo connectionVo, CarAccidentMasterVo carAccidentMasterVo) {
            _errorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            /*
             * Dao
             */
            _carAccidentMasterDao = new(connectionVo);
            _carMasterDao = new(connectionVo);
            _staffMasterDao = new(connectionVo);
            /*
             * Vo
             */
            _carAccidentMasterVo = carAccidentMasterVo;
            /*
             * InitializeControl
             */
            InitializeComponent();
            /*
             * MenuStrip
             */
            List<string> listStringSheetViewList = new() {
                        "ToolStripMenuItemFile",
                        "ToolStripMenuItemExit",
                        "ToolStripMenuItemHelp"
                     };
            this.MenuStripEx1.ChangeEnable(listStringSheetViewList);
            _arrayPictureBoxEx = [PictureBoxEx1, PictureBoxEx2, PictureBoxEx3, PictureBoxEx4, PictureBoxEx5];
            /*
             * 新規・修正の処理
             */
            if (carAccidentMasterVo is not null) {
                this.PutControl(_carAccidentMasterDao.SelectOneCarAccidentMaster(carAccidentMasterVo.StaffCode, carAccidentMasterVo.InsertYmdHms));
            } else {
                this.InitializeControl();
            }
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;
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
            _errorProvider.Clear();                                                                             // ErrorProviderをクリア
            /*
             * バリデート
             */
            // 事故発生日
            if (this.DateTimePickerExOccurrence.CustomFormat == " ") {
                _errorProvider.SetError(this.DateTimePickerExOccurrence, "事故発生日が不正です");
                return;
            }
            // 事故発生時刻
            if (this.MaskedTextBoxExTime.Text.Length < 4) {                                                     // 入力文字の桁数チェック
                _errorProvider.SetError(this.MaskedTextBoxExTime, "入力フォーマットが不正です");
                return;
            }
            if (Int32.Parse(this.MaskedTextBoxExTime.Text.Substring(0, 2)) < 24) {                              // HH部分のチェック
            } else {
                _errorProvider.SetError(this.MaskedTextBoxExTime, "時(HH)の入力値が不正です");
                return;
            }
            if (Int32.Parse(this.MaskedTextBoxExTime.Text.Substring(2, 2)) < 60) {                              // mm部分のチェック
            } else {
                _errorProvider.SetError(this.MaskedTextBoxExTime, "分(mm)の入力値が不正です");
                return;
            }
            // 従事者台帳に反映
            if (this.ComboBoxExTotallingFlag.SelectedIndex == -1) {
                _errorProvider.SetError(this.ComboBoxExTotallingFlag, "選択して下さい");
                return;
            }
            // 天候
            if (this.ComboBoxExWeather.SelectedIndex == -1) {
                _errorProvider.SetError(this.ComboBoxExWeather, "選択して下さい");
                return;
            }
            // 事故の種別
            if (this.ComboBoxExAccidentKind.SelectedIndex == -1) {
                _errorProvider.SetError(this.ComboBoxExAccidentKind, "選択して下さい");
                return;
            }
            // 事故の静動
            if (this.ComboBoxExCarStatic.SelectedIndex == -1) {
                _errorProvider.SetError(this.ComboBoxExCarStatic, "選択して下さい");
                return;
            }
            // 事故発生の原因
            if (this.ComboBoxExOccurrenceCause.SelectedIndex == -1) {
                _errorProvider.SetError(this.ComboBoxExOccurrenceCause, "選択して下さい");
                return;
            }
            // 過失の有無
            if (this.ComboBoxExNegligence.SelectedIndex == -1) {
                _errorProvider.SetError(this.ComboBoxExNegligence, "選択して下さい");
                return;
            }
            // 人身の詳細
            if (this.ComboBoxExPersonalInjury.SelectedIndex == -1) {
                _errorProvider.SetError(this.ComboBoxExPersonalInjury, "選択して下さい");
                return;
            }
            // 事故の状態
            if (this.ComboBoxExPropertyAccident1.SelectedIndex == -1) {
                _errorProvider.SetError(this.ComboBoxExPropertyAccident1, "選択して下さい");
                return;
            }
            // 事故の相手
            if (this.ComboBoxExPropertyAccident2.SelectedIndex == -1) {
                _errorProvider.SetError(this.ComboBoxExPropertyAccident2, "選択して下さい");
                return;
            }
            // 発生場所
            if (this.TextBoxExOccurrenceAddress.Text.Length < 1) {
                _errorProvider.SetError(this.TextBoxExOccurrenceAddress, "住所を入力して下さい");
                return;
            }
            // 運転手・作業員名
            if (this.ComboBoxExDisplayName.SelectedIndex == -1) {
                _errorProvider.SetError(this.ComboBoxExDisplayName, "選択して下さい");
                return;
            }
            // 職種
            if (this.ComboBoxExWorkKind.SelectedIndex == -1) {
                _errorProvider.SetError(this.ComboBoxExWorkKind, "選択して下さい");
                return;
            }
            // 車両登録番号
            // 事故の概要
            // 事故の詳細
            // 再発防止指導

            /*
             * INSERT / UPDATE
             */
            CarAccidentMasterVo carAccidentMasterVo = SetVo();
            if (_carAccidentMasterDao.ExistenceCarAccidentMaster(carAccidentMasterVo.StaffCode, carAccidentMasterVo.InsertYmdHms)) {
                _carAccidentMasterDao.UpdateOneCarAccidentMaster(carAccidentMasterVo);
                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "UPDATE SUCCESS";
            } else {
                _carAccidentMasterDao.InsertOneCarAccidentMaster(carAccidentMasterVo);
                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "INSERT SUCCESS";
            }
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private CarAccidentMasterVo SetVo() {
            StaffMasterVo staffMasterVo = ((ComboBoxExDisplayNameVo)ComboBoxExDisplayName.SelectedItem).StaffMasterVo;// ComboBox.SelectItemからVoを取得する
            CarAccidentMasterVo carAccidentMasterVo = new();
            switch (ComboBoxExTotallingFlag.Text) {// 集計
                case "反映する":
                    carAccidentMasterVo.TotallingFlag = true;
                    break;
                case "反映しない":
                    carAccidentMasterVo.TotallingFlag = false;
                    break;
            }
            carAccidentMasterVo.OccurrenceYmdHms = new DateTime(DateTimePickerExOccurrence.Value.Year,                              // 事故発生年月日
                                                                DateTimePickerExOccurrence.Value.Month,
                                                                DateTimePickerExOccurrence.Value.Day,
                                                                int.Parse(MaskedTextBoxExTime.Text.Substring(0, 2)),
                                                                int.Parse(MaskedTextBoxExTime.Text.Substring(2, 2)),
                                                                0);
            carAccidentMasterVo.Weather = this.ComboBoxExWeather.Text;                                                              // 天候
            carAccidentMasterVo.AccidentKind = this.ComboBoxExAccidentKind.Text;                                                    // 事故の種別 
            carAccidentMasterVo.CarStatic = this.ComboBoxExCarStatic.Text;                                                          // 車両の静動
            carAccidentMasterVo.OccurrenceCause = this.ComboBoxExOccurrenceCause.Text;                                              // 事故の発生原因　
            carAccidentMasterVo.Negligence = this.ComboBoxExNegligence.Text;                                                        // 過失の有無
            carAccidentMasterVo.PersonalInjury = this.ComboBoxExPersonalInjury.Text;                                                // 人身事故の詳細　
            carAccidentMasterVo.PropertyAccident1 = this.ComboBoxExPropertyAccident1.Text;                                          // 事故の状態
            carAccidentMasterVo.PropertyAccident2 = this.ComboBoxExPropertyAccident2.Text;                                          // 事故の相手
            carAccidentMasterVo.OccurrenceAddress = this.TextBoxExOccurrenceAddress.Text;                                           // 事故の発生場所
            if (staffMasterVo is not null) {                                                                                        // 運転手・作業員の氏名
                carAccidentMasterVo.StaffCode = staffMasterVo.StaffCode;
                carAccidentMasterVo.DisplayName = staffMasterVo.DisplayName;
                carAccidentMasterVo.LicenseNumber = staffMasterVo.LicenseMasterVo.LicenseNumber;                                    // 免許証番号
            }
            carAccidentMasterVo.WorkKind = this.ComboBoxExWorkKind.Text;                                                            // 運転手・作業員の別 
            carAccidentMasterVo.CarRegistrationNumber = this.ComboBoxExCarRegistrationNumber.Text;                                  // 車両登録番号
            carAccidentMasterVo.AccidentSummary = this.TextBoxExAccidentSummary.Text;                                               // 事故概要
            carAccidentMasterVo.AccidentDetail = this.TextBoxExAccidentDetail.Text;                                                 // 事故詳細
            carAccidentMasterVo.Guide = this.TextBoxExGuide.Text;                                                                   // 事故後の指導
            carAccidentMasterVo.Picture1 = (byte[])new ImageConverter().ConvertTo(PictureBoxEx1.Image, typeof(byte[]));             // PictureBoxPicture1
            carAccidentMasterVo.Picture2 = (byte[])new ImageConverter().ConvertTo(PictureBoxEx2.Image, typeof(byte[]));             // PictureBoxPicture2
            carAccidentMasterVo.Picture3 = (byte[])new ImageConverter().ConvertTo(PictureBoxEx3.Image, typeof(byte[]));             // PictureBoxPicture3
            carAccidentMasterVo.Picture4 = (byte[])new ImageConverter().ConvertTo(PictureBoxEx4.Image, typeof(byte[]));             // PictureBoxPicture4
            carAccidentMasterVo.Picture5 = (byte[])new ImageConverter().ConvertTo(PictureBoxEx5.Image, typeof(byte[]));             // PictureBoxPicture5
            carAccidentMasterVo.InsertYmdHms = _carAccidentMasterVo is not null ? _carAccidentMasterVo.InsertYmdHms : new DateTime(1900, 01, 01);// 登録日時
            return carAccidentMasterVo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="carAccidentMasterVo"></param>
        private void PutControl(CarAccidentMasterVo carAccidentMasterVo) {
            this.DateTimePickerExOccurrence.SetValue(carAccidentMasterVo.OccurrenceYmdHms);                                         // 事故発生日
            this.MaskedTextBoxExTime.Text = carAccidentMasterVo.OccurrenceYmdHms.ToString("HH:mm");                                 // 事故発生時刻
            this.ComboBoxExTotallingFlag.SelectedIndex = carAccidentMasterVo.TotallingFlag ? 0 : 1;                                 // 従事者台帳に反映
            this.ComboBoxExWeather.Text = carAccidentMasterVo.Weather;                                                              // 天候
            this.ComboBoxExAccidentKind.Text = carAccidentMasterVo.AccidentKind;                                                    // 事故の種別
            this.ComboBoxExCarStatic.Text = carAccidentMasterVo.CarStatic;                                                          // 事故の静動
            this.ComboBoxExOccurrenceCause.Text = carAccidentMasterVo.OccurrenceCause;                                              // 事故発生の原因
            this.ComboBoxExNegligence.Text = carAccidentMasterVo.Negligence;                                                        // 過失の有無
            this.ComboBoxExPersonalInjury.Text = carAccidentMasterVo.PersonalInjury;                                                // 人身の詳細
            this.ComboBoxExPropertyAccident1.Text = carAccidentMasterVo.PropertyAccident1;                                          // 事故の状態
            this.ComboBoxExPropertyAccident2.Text = carAccidentMasterVo.PropertyAccident2;                                          // 事故の相手
            this.TextBoxExOccurrenceAddress.Text = carAccidentMasterVo.OccurrenceAddress;                                           // 発生場所
            this.InitializeComboBoxExDisplayName();                                                                                 // 運転手・作業員名
            this.ComboBoxExDisplayName.Text = carAccidentMasterVo.DisplayName;
            this.ComboBoxExWorkKind.Text = carAccidentMasterVo.WorkKind;                                                            // 職種
            this.InitializeComboBoxExCarRegistrationNumber();                                                                       // 車両登録番号
            this.ComboBoxExCarRegistrationNumber.Text = carAccidentMasterVo.CarRegistrationNumber;
            this.TextBoxExAccidentSummary.Text = carAccidentMasterVo.AccidentSummary;                                               // 事故の概要
            this.TextBoxExAccidentDetail.Text = carAccidentMasterVo.AccidentDetail;                                                 // 事故の詳細
            this.TextBoxExGuide.Text = carAccidentMasterVo.Guide;                                                                   // 再発防止指導
            /*
             * 写真１～５
             */
            if (carAccidentMasterVo.Picture1 is not null && carAccidentMasterVo.Picture1.Length != 0) {
                ImageConverter imageConverter = new();
                this.PictureBoxEx1.Image = (Image)imageConverter.ConvertFrom(carAccidentMasterVo.Picture1);
            } else {
                this.PictureBoxEx1.Image = null;
            }
            if (carAccidentMasterVo.Picture2 is not null && carAccidentMasterVo.Picture2.Length != 0) {
                ImageConverter imageConverter = new();
                this.PictureBoxEx2.Image = (Image)imageConverter.ConvertFrom(carAccidentMasterVo.Picture2);
            } else {
                this.PictureBoxEx2.Image = null;
            }
            if (carAccidentMasterVo.Picture3 is not null && carAccidentMasterVo.Picture3.Length != 0) {
                ImageConverter imageConverter = new();
                this.PictureBoxEx3.Image = (Image)imageConverter.ConvertFrom(carAccidentMasterVo.Picture3);
            } else {
                this.PictureBoxEx3.Image = null;
            }
            if (carAccidentMasterVo.Picture4 is not null && carAccidentMasterVo.Picture4.Length != 0) {
                ImageConverter imageConverter = new();
                this.PictureBoxEx4.Image = (Image)imageConverter.ConvertFrom(carAccidentMasterVo.Picture4);
            } else {
                this.PictureBoxEx4.Image = null;
            }
            if (carAccidentMasterVo.Picture5 is not null && carAccidentMasterVo.Picture5.Length != 0) {
                ImageConverter imageConverter = new();
                this.PictureBoxEx5.Image = (Image)imageConverter.ConvertFrom(carAccidentMasterVo.Picture5);
            } else {
                this.PictureBoxEx5.Image = null;
            }
        }

        /// <summary>
        /// ToolStripMenuItemがクリックされた時のSourceControlを保持
        /// </summary>
        Control _sourceControl = null;
        /// <summary>
        /// ContextMenuStrip1_Opened
        /// コンテキストが開かれた親コントロールを取得する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStrip1_Opened(object sender, EventArgs e) {
            _sourceControl = ((ContextMenuStrip)sender).SourceControl;                          //ContextMenuStripを表示しているコントロールを取得する
        }

        /// <summary>
        /// ToolStripMenuItem_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                /*
                 * Picture Clip
                 */
                case "ToolStripMenuItemClip":
                    ((CcPictureBox)_sourceControl).Image = (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap);
                    break;
                /*
                 * Picture Delete
                 */
                case "ToolStripMenuItemDelete":
                    ((CcPictureBox)_sourceControl).Image = null;
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExMap_Click(object sender, EventArgs e) {
            // GoogleMaps表示
            new MapUtility().MapOpen(this.TextBoxExOccurrenceAddress.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeControl() {
            this.DateTimePickerExOccurrence.SetToday();                                         // 事故発生日
            this.MaskedTextBoxExTime.Text = DateTime.Now.ToString("HH:mm");                     // 事故発生時刻
            this.ComboBoxExTotallingFlag.SelectedIndex = 0;                                     // 従事者台帳に反映
            this.ComboBoxExWeather.SelectedIndex = -1;                                          // 天候
            this.ComboBoxExAccidentKind.SelectedIndex = -1;                                     // 事故の種別
            this.ComboBoxExCarStatic.SelectedIndex = -1;                                        // 事故の静動
            this.ComboBoxExOccurrenceCause.SelectedIndex = -1;                                  // 事故発生の原因
            this.ComboBoxExNegligence.SelectedIndex = -1;                                       // 過失の有無
            this.ComboBoxExPersonalInjury.SelectedIndex = -1;                                   // 人身の詳細
            this.ComboBoxExPropertyAccident1.SelectedIndex = -1;                                // 事故の状態
            this.ComboBoxExPropertyAccident2.SelectedIndex = -1;                                // 事故の相手
            this.TextBoxExOccurrenceAddress.Clear();                                            // 発生場所
            this.InitializeComboBoxExDisplayName();                                             // 運転手・作業員名
            this.ComboBoxExDisplayName.SelectedIndex = -1;
            this.ComboBoxExWorkKind.SelectedIndex = -1;                                         // 職種
            this.InitializeComboBoxExCarRegistrationNumber();                                   // 車両登録番号
            this.TextBoxExAccidentSummary.Clear();                                              // 事故の概要
            this.TextBoxExAccidentDetail.Clear();                                               // 事故の詳細
            this.TextBoxExGuide.Clear();                                                        // 再発防止指導
            for (int i = 0; i < 5; i++)                                                         // PictureBox
                _arrayPictureBoxEx[i].Image = null;
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeComboBoxExCarRegistrationNumber() {
            this.ComboBoxExCarRegistrationNumber.Items.Clear();
            foreach (CarMasterVo carMasterVo in _carMasterDao.SelectAllCarMaster().OrderBy(x => x.RegistrationNumber4)) {
                this.ComboBoxExCarRegistrationNumber.Items.Add(new ComboBoxExCarRegistrationNumberVo(carMasterVo.RegistrationNumber, carMasterVo));
            }
            this.ComboBoxExCarRegistrationNumber.DisplayMember = "RegistrationNumber";
            // ここでイベント追加しないと初期化で発火しちゃうよ
            this.ComboBoxExCarRegistrationNumber.SelectedIndexChanged += new EventHandler(ComboBoxExCarRegistrationNumber_SelectedIndexChanged);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxExCarRegistrationNumber_SelectedIndexChanged(object sender, EventArgs e) {

        }

        /// <summary>
        /// インナークラス
        /// </summary>
        private class ComboBoxExCarRegistrationNumberVo {
            private string _registrationNumber;
            private CarMasterVo _carMasterVo;

            public ComboBoxExCarRegistrationNumberVo(string registrationNumber, CarMasterVo carMasterVo) {
                _registrationNumber = registrationNumber;
                _carMasterVo = carMasterVo;
            }

            public string RegistrationNumber {
                get => this._registrationNumber;
                set => this._registrationNumber = value;
            }
            public CarMasterVo CarMasterVo {
                get => this._carMasterVo;
                set => this._carMasterVo = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeComboBoxExDisplayName() {
            this.ComboBoxExDisplayName.Items.Clear();
            foreach (StaffMasterVo staffMasterVo in _staffMasterDao.SelectAllStaffMaster(new List<int> { 10, 11, 12, 14, 15, 22, 99 },      // 役員・社員・アルバイト・嘱託雇用契約社員・パートタイマー・労供・指定なし
                                                                                         new List<int> { 20, 21, 22, 23, 99 },              // 労供長期・労供短期・指定なし
                                                                                         new List<int> { 10, 11, 12, 13, 20, 99 },          // 運転手・作業員・自転車駐輪場・リサイクルセンター・事務員・指定なし
                                                                                         false).FindAll(x => x.RetirementFlag == false).OrderBy(x => x.NameKana)) {
                this.ComboBoxExDisplayName.Items.Add(new ComboBoxExDisplayNameVo(staffMasterVo.DisplayName, staffMasterVo));
            }
            this.ComboBoxExDisplayName.DisplayMember = "Name";
            // ここでイベント追加しないと初期化で発火しちゃうよ
            this.ComboBoxExDisplayName.SelectedIndexChanged += new EventHandler(ComboBoxExDisplayName_SelectedIndexChanged);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxExDisplayName_SelectedIndexChanged(object sender, EventArgs e) {

        }

        /// <summary>
        /// インナークラス
        /// </summary>
        private class ComboBoxExDisplayNameVo {
            private string _name;
            private StaffMasterVo _staffMasterVo;

            public ComboBoxExDisplayNameVo(string name, StaffMasterVo staffMasterVo) {
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
        private void AccidentDetail_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
