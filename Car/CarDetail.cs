/*
 * 2025-02-12
 */
using System.Drawing.Printing;

using Common;

using ControlEx;

using Dao;

using Vo;

namespace Car {

    public partial class CarDetail : Form {
        /*
         * インスタンス作成
         */
        private readonly ScreenForm _screenForm = new();
        /*
         * Dao
         */
        private readonly CarMasterDao _carMasterDao;
        private readonly ClassificationMasterDao _classificationMasterDao;
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;
        /*
         * Dictionary
         */
        private readonly Dictionary<string, int> _dictionaryClassificationCode = new();
        private readonly Dictionary<int, string> _dictionaryClassificationName = new();

        private readonly Dictionary<string, int> _dictionaryGarageCode = new() { { "指定なし", 0 }, { "足立", 1 }, { "三郷", 2 } };
        private readonly Dictionary<int, string> _dictionaryGarageName = new() { { 0, "指定なし" }, { 1, "足立" }, { 2, "三郷" } };

        private readonly Dictionary<string, int> _dictionaryCarKindCode = new() { { "軽自動車", 10 }, { "小型", 11 }, { "普通", 12 } };
        private readonly Dictionary<int, string> _dictionaryCarKindName = new() { { 10, "軽自動車" }, { 11, "小型" }, { 12, "普通" } };

        private readonly Dictionary<string, int> _dictionaryOtherCode = new() { { "事業用", 10 }, { "自家用", 11 } };
        private readonly Dictionary<int, string> _dictionaryOtherName = new() { { 10, "事業用" }, { 11, "自家用" } };

        private readonly Dictionary<string, int> _dictionaryShapeCode = new() { { "キャブオーバー", 10 }, { "塵芥車", 11 }, { "ダンプ", 12 }, { "コンテナ専用", 13 }, { "脱着装置付コンテナ専用車", 14 }, { "粉粒体運搬車", 15 }, { "糞尿車", 16 }, { "清掃車", 17 } };
        private readonly Dictionary<int, string> _dictionaryShapeName = new() { { 10, "キャブオーバー" }, { 11, "塵芥車" }, { 12, "ダンプ" }, { 13, "コンテナ専用" }, { 14, "脱着装置付コンテナ専用車" }, { 15, "粉粒体運搬車" }, { 16, "糞尿車" }, { 17, "清掃車" } };

        private readonly Dictionary<string, int> _dictionaryManufacturerCode = new() { { "いすゞ", 10 }, { "日産", 11 }, { "ダイハツ", 12 }, { "日野", 13 }, { "スバル", 14 } };
        private readonly Dictionary<int, string> _dictionaryManufacturerName = new() { { 10, "いすゞ" }, { 11, "日産" }, { 12, "ダイハツ" }, { 13, "日野" }, { 14, "スバル" } };

        /// <summary>
        /// コンストラクター(INSERT)
        /// </summary>
        /// <param name="connectionVo"></param>
        public CarDetail(ConnectionVo connectionVo) {
            /*
             * Dao
             */
            _carMasterDao = new(connectionVo);
            _classificationMasterDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            /*
             * Dictionary
             */
            foreach (ClassificationMasterVo classificationMasterVo in _classificationMasterDao.SelectAllClassificationMasterVo()) {
                _dictionaryClassificationCode.Add(classificationMasterVo.Name, classificationMasterVo.Code);
                _dictionaryClassificationName.Add(classificationMasterVo.Code, classificationMasterVo.Name);
            }
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
            this.TextBoxExCarCode.Text = (_carMasterDao.GetCarCode() + 1).ToString("#####");                                        // 新規での車両コード採番
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "車両CDの採番が完了しました";
            /*
             * Eventを登録する
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// コンストラクター(UPDATE)
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public CarDetail(ConnectionVo connectionVo, int carCode) {
            /*
             * Dao
             */
            _carMasterDao = new(connectionVo);
            _classificationMasterDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            /*
             * Dictionary
             */
            foreach (ClassificationMasterVo classificationMasterVo in _classificationMasterDao.SelectAllClassificationMasterVo()) {
                _dictionaryClassificationCode.Add(classificationMasterVo.Name, classificationMasterVo.Code);
                _dictionaryClassificationName.Add(classificationMasterVo.Code, classificationMasterVo.Name);
            }
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
            this.MenuStripEx1.ChangeEnable(listString);

            this.InitializeControl();
            this.SetControl(_carMasterDao.SelectOneCarMasterP(carCode));
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "Select Success";
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
            DialogResult dialogResult = MessageBox.Show("データを更新します。よろしいですか？", "Messsage", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            CarMasterVo carMasterVo = SetVo();
            try {
                switch (dialogResult) {
                    case DialogResult.OK:
                        if (_carMasterDao.ExistenceHCarMaster(carMasterVo.CarCode)) {
                            _carMasterDao.UpdateOneCarMaster(SetVo());
                            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "Update Success";
                        } else {
                            _carMasterDao.InsertOneCarMaster(SetVo());
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
                case "ToolStripMenuItemMainPictureClip":                                                                            // MainPicture クリップボード
                    PictureBoxExMainPicture.Image = (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap);
                    break;
                case "ToolStripMenuItemMainPictureDelete":                                                                          // MainPicture 削除
                    PictureBoxExMainPicture.Image = null;
                    break;
                case "ToolStripMenuItemSubPictureClip":                                                                             // SubPicture クリップボード
                    PictureBoxExSubPicture.Image = (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap);
                    break;
                case "ToolStripMenuItemSubPictureDelete":                                                                           // SubPicture 削除
                    PictureBoxExSubPicture.Image = null;
                    break;
                case "ToolStripMenuItemExit":                                                                                       // アプロケーションを終了する
                    this.Close();
                    break;
                case "ToolStripMenuItemPrintA4":                                                                                    // アプロケーションを終了する
                    this.ToolStripMenuItemPrintA4_Click();
                    break;
            }
        }

        /// <summary>
        /// SetVo
        /// Voに値をセットする
        /// </summary>
        /// <returns></returns>
        private CarMasterVo SetVo() {
            CarMasterVo carMasterVo = new();
            /*
             * システム情報
             */
            carMasterVo.CarCode = int.Parse(this.TextBoxExCarCode.Text);                                                            // 車両コード
            carMasterVo.RegistrationNumber = this.TextBoxExRegistrationNumber.Text;                                                 // 車両ナンバー
            carMasterVo.DoorNumber = this.TextBoxExDoorNumber.Text.Length > 0 ? int.Parse(this.TextBoxExDoorNumber.Text) : 0;       // ドア番号
            carMasterVo.RegistrationNumber1 = this.ComboBoxExRegistrationNumber1.Text;                                              // 車両ナンバー１
            carMasterVo.RegistrationNumber2 = this.TextBoxExRegistrationNumber2.Text;                                               // 車両ナンバー２
            carMasterVo.RegistrationNumber3 = this.TextBoxExRegistrationNumber3.Text;                                               // 車両ナンバー３
            carMasterVo.RegistrationNumber4 = this.TextBoxExRegistrationNumber4.Text;                                               // 車両ナンバー４
            carMasterVo.ClassificationCode = _dictionaryClassificationCode[this.ComboBoxExClassificationCode.Text];                 // 使用区分
            carMasterVo.GarageCode = _dictionaryGarageCode[this.ComboBoxExGarageCode.Text];                                         // 車庫地
            carMasterVo.DisguiseKind1 = this.ComboBoxExDisguiseKind1.Text;                                                          // 仮装の名称(システム表示)
            carMasterVo.DisguiseKind2 = this.ComboBoxExDisguiseKind2.Text;                                                          // 仮装の名称(事故報告書)
            carMasterVo.DisguiseKind3 = this.ComboBoxExDisguiseKind3.Text;                                                          // 仮装の名称(整備工場等)
            /*
             * １．基本情報
             */
            carMasterVo.VehicleNumber = this.TextBoxExVehicleNumber.Text;                                                           // 車台番号
            carMasterVo.RegistrationDate = this.DateTimePickerExRegistrationDate.GetValue().Date;                                   // 登録年月日/交付年月日
            carMasterVo.FirstRegistrationDate = this.DateTimePickerExFirstRegistrationDate.GetValue().Date;                         // 初度登録年月
            carMasterVo.ExpirationDate = this.DateTimePickerExExpirationDate.GetValue().Date;                                       // 有効期限の満了する日
            /*
             * ２．所有者・使用者情報
             */
            carMasterVo.OwnerName = this.ComboBoxExOwnerName.Text;                                                                  // 所有者の氏名又は名称
            carMasterVo.OwnerAddress = this.ComboBoxExOwnerAddress.Text;                                                            // 所有者の住所
            carMasterVo.UserName = this.ComboBoxExUserName.Text;                                                                    // 使用者の氏名又は名称
            carMasterVo.UserAddress = this.ComboBoxExUserAddress.Text;                                                              // 使用者の住所   
            carMasterVo.BaseAddress = this.ComboBoxExBaseAddress.Text;                                                              // 使用の本拠の位置
            /*
             * ３．車両詳細情報
             */
            carMasterVo.ManufacturerCode = _dictionaryManufacturerCode[this.ComboBoxExManufacturerCode.Text];                       // 車名
            carMasterVo.Version = this.TextBoxExVersion.Text;                                                                       // 型式
            carMasterVo.MotorVersion = this.TextBoxExMotorVersion.Text;                                                             // 原動機の型式
            carMasterVo.CarKindCode = _dictionaryCarKindCode[this.ComboBoxExCarKindCode.Text];                                      // 自動車の種別
            carMasterVo.CarUse = this.ComboBoxExCarUse.Text;                                                                        // 用途
            carMasterVo.OtherCode = _dictionaryOtherCode[this.ComboBoxExOtherCode.Text];                                            // 自家用・事業用の別
            carMasterVo.ShapeCode = _dictionaryShapeCode[this.ComboBoxExShapeCode.Text];                                            // 車体の形状
            carMasterVo.Capacity = this.NumericUpDownExCapacity.Value;                                                              // 乗車定員
            carMasterVo.MaximumLoadCapacity = this.NumericUpDownExMaximumLoadCapacity.Value;                                        // 最大積載量
            carMasterVo.VehicleWeight = this.NumericUpDownExVehicleWeight.Value;                                                    // 車両重量
            carMasterVo.TotalVehicleWeight = this.NumericUpDownExTotalVehicleWeight.Value;                                          // 車両総重量
            carMasterVo.Length = this.NumericUpDownExLength.Value;                                                                  // 長さ
            carMasterVo.Width = this.NumericUpDownExWidth.Value;                                                                    // 幅
            carMasterVo.Height = this.NumericUpDownExHeight.Value;                                                                  // 高さ 
            carMasterVo.FfAxisWeight = this.NumericUpDownExFfAxisWeight.Value;                                                      // 前前軸重
            carMasterVo.FrAxisWeight = this.NumericUpDownExFrAxisWeight.Value;                                                      // 前後軸重
            carMasterVo.RfAxisWeight = this.NumericUpDownExRfAxisWeight.Value;                                                      // 後前軸重
            carMasterVo.RrAxisWeight = this.NumericUpDownExRrAxisWeight.Value;                                                      // 後後軸重
            carMasterVo.TotalDisplacement = this.NumericUpDownExTotalDisplacement.Value;                                            // 総排気量又は定格出力
            carMasterVo.TypesOfFuel = this.ComboBoxExTypesOfFuel.Text;                                                              // 燃料の種類
            carMasterVo.VersionDesignateNumber = this.TextBoxExVersionDesignateNumber.Text;                                         // 型式指定番号
            carMasterVo.CategoryDistinguishNumber = this.TextBoxExCategoryDistinguishNumber.Text;                                   // 類別区分番号
            carMasterVo.Remarks = this.TextBoxExRemarks.Text;                                                                       // 備考
            carMasterVo.MainPicture = (byte[]?)new ImageConverter().ConvertTo(this.PictureBoxExMainPicture.Image, typeof(byte[]));  // 
            carMasterVo.SubPicture = (byte[]?)new ImageConverter().ConvertTo(this.PictureBoxExSubPicture.Image, typeof(byte[]));    // 
            carMasterVo.EmergencyVehicleFlag = this.CheckBoxExEmergencyVehicleFlag.Checked;                                         // 緊急車両登録フラグ
            carMasterVo.EmergencyVehicleDate = this.DateTimePickerExEmergencyVehicleDate.GetValue();                                // 緊急車両登録期限
            return carMasterVo;
        }

        /// <summary>
        /// コントロール初期化
        /// </summary>
        private void InitializeControl() {
            /*
             * システム情報
             */
            this.TextBoxExCarCode.ClearEmpty();                                                                                     // 車両コード
            this.TextBoxExRegistrationNumber.ClearEmpty();                                                                          // 車両ナンバー
            this.TextBoxExDoorNumber.ClearEmpty();                                                                                  // ドア番号
            this.CheckBoxExEmergencyVehicleFlag.Checked = false;                                                                    // 緊急車両
            this.DateTimePickerExEmergencyVehicleDate.SetClear();                                                                   // 緊急車両登録期限
            this.ComboBoxExRegistrationNumber1.Clear();                                                                             // 車両ナンバー１
            this.TextBoxExRegistrationNumber2.ClearEmpty();                                                                         // 車両ナンバー２
            this.TextBoxExRegistrationNumber3.ClearEmpty();                                                                         // 車両ナンバー３
            this.TextBoxExRegistrationNumber4.ClearEmpty();                                                                         // 車両ナンバー４
            this.ComboBoxExClassificationCode.Clear();                                                                              // 使用区分
            this.ComboBoxExGarageCode.Clear();                                                                                      // 車庫地
            this.ComboBoxExDisguiseKind1.Clear();                                                                                   // 仮装の名称(システム表示)
            this.ComboBoxExDisguiseKind2.Clear();                                                                                   // 仮装の名称(事故報告書)
            this.ComboBoxExDisguiseKind3.Clear();                                                                                   // 仮装の名称(整備工場等)
            /*
             * １．基本情報
             */
            this.TextBoxExVehicleNumber.ClearEmpty();                                                                               // 車台番号
            this.DateTimePickerExRegistrationDate.SetClear();                                                                       // 登録年月日/交付年月日
            this.DateTimePickerExFirstRegistrationDate.SetClear();                                                                  // 初度登録年月
            this.DateTimePickerExExpirationDate.SetClear();                                                                         // 有効期限の満了する日
            /*
             * ２．所有者・使用者情報
             */
            this.ComboBoxExOwnerName.Clear();                                                                                       // 所有者の氏名又は名称
            this.ComboBoxExOwnerAddress.Clear();                                                                                    // 所有者の住所
            this.ComboBoxExUserName.Clear();                                                                                        // 使用者の氏名又は名称
            this.ComboBoxExUserAddress.Clear();                                                                                     // 使用者の住所
            this.ComboBoxExBaseAddress.Clear();                                                                                     // 使用の本拠の位置
            /*
             * ３．車両詳細情報
             */
            this.ComboBoxExManufacturerCode.Clear();                                                                                // 車名
            this.TextBoxExVersion.ClearEmpty();                                                                                     // 型式
            this.TextBoxExMotorVersion.ClearEmpty();                                                                                // 原動機の型式
            this.ComboBoxExCarKindCode.Clear();                                                                                     // 自動車の種別
            this.ComboBoxExCarUse.Clear();                                                                                          // 用途
            this.ComboBoxExOtherCode.Clear();                                                                                       // 自家用・事業用の別
            this.ComboBoxExShapeCode.Clear();                                                                                       // 車体の形状
            this.NumericUpDownExCapacity.Value = 0;                                                                                 // 乗車定員
            this.NumericUpDownExMaximumLoadCapacity.Value = 0;                                                                      // 最大積載量
            this.NumericUpDownExVehicleWeight.Value = 0;                                                                            // 車両重量
            this.NumericUpDownExTotalVehicleWeight.Value = 0;                                                                       // 車両総重量
            this.NumericUpDownExLength.Value = 0;                                                                                   // 長さ
            this.NumericUpDownExWidth.Value = 0;                                                                                    // 幅
            this.NumericUpDownExHeight.Value = 0;                                                                                   // 高さ 
            this.NumericUpDownExFfAxisWeight.Value = 0;                                                                             // 前前軸重
            this.NumericUpDownExFrAxisWeight.Value = 0;                                                                             // 前後軸重
            this.NumericUpDownExRfAxisWeight.Value = 0;                                                                             // 後前軸重
            this.NumericUpDownExRrAxisWeight.Value = 0;                                                                             // 後後軸重
            this.NumericUpDownExTotalDisplacement.Value = 0;                                                                        // 総排気量又は定格出力
            this.ComboBoxExTypesOfFuel.Clear();                                                                                     // 燃料の種類
            this.TextBoxExVersionDesignateNumber.ClearEmpty();                                                                      // 型式指定番号
            this.TextBoxExCategoryDistinguishNumber.ClearEmpty();                                                                   // 類別区分番号
            this.TextBoxExRemarks.ClearEmpty();                                                                                     // 備考
            this.PictureBoxExMainPicture.Clear();                                                                                   // 写真
            this.PictureBoxExSubPicture.Clear();                                                                                    // 写真

            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;
        }

        /// <summary>
        /// ControlにVoをセットする
        /// </summary>
        /// <param name="carMasterVo"></param>
        private void SetControl(CarMasterVo carMasterVo) {
            /*
             * システム情報
             */
            this.TextBoxExCarCode.Text = carMasterVo.CarCode.ToString("#####");                                                     // 車両コード
            this.TextBoxExRegistrationNumber.Text = carMasterVo.RegistrationNumber;                                                 // 車両ナンバー
            this.TextBoxExDoorNumber.Text = carMasterVo.DoorNumber.ToString("#####");                                               // ドア番号
            this.CheckBoxExEmergencyVehicleFlag.Checked = carMasterVo.EmergencyVehicleFlag;                                         // 緊急車両フラグ
            if (carMasterVo.EmergencyVehicleFlag) {                                                                                 // 緊急車両登録期限
                this.DateTimePickerExEmergencyVehicleDate.Value = carMasterVo.EmergencyVehicleDate;
            } else {
                this.DateTimePickerExEmergencyVehicleDate.SetEmpty();
            }
            this.ComboBoxExRegistrationNumber1.Text = carMasterVo.RegistrationNumber1;                                              // 車両ナンバー１
            this.TextBoxExRegistrationNumber2.Text = carMasterVo.RegistrationNumber2;                                               // 車両ナンバー２
            this.TextBoxExRegistrationNumber3.Text = carMasterVo.RegistrationNumber3;                                               // 車両ナンバー３
            this.TextBoxExRegistrationNumber4.Text = carMasterVo.RegistrationNumber4;                                               // 車両ナンバー４
            this.ComboBoxExClassificationCode.Text = _dictionaryClassificationName[carMasterVo.ClassificationCode];                 // 使用区分
            this.ComboBoxExGarageCode.Text = _dictionaryGarageName[carMasterVo.GarageCode];                                         // 車庫地
            this.ComboBoxExDisguiseKind1.Text = carMasterVo.DisguiseKind1;                                                          // 仮装の名称(システム表示)
            this.ComboBoxExDisguiseKind2.Text = carMasterVo.DisguiseKind2;                                                          // 仮装の名称(事故報告書)
            this.ComboBoxExDisguiseKind3.Text = carMasterVo.DisguiseKind3;                                                          // 仮装の名称(整備工場等)
            /*
             * １．基本情報
             */
            this.TextBoxExVehicleNumber.Text = carMasterVo.VehicleNumber;                                                           // 車台番号
            this.DateTimePickerExRegistrationDate.SetValueJp(carMasterVo.RegistrationDate.Date);                                    // 登録年月日/交付年月日
            this.DateTimePickerExFirstRegistrationDate.SetValueJp(carMasterVo.FirstRegistrationDate.Date);                          // 初度登録年月
            this.DateTimePickerExExpirationDate.SetValueJp(carMasterVo.ExpirationDate.Date);                                        // 有効期限の満了する日
            /*
             * ２．所有者・使用者情報
             */
            this.ComboBoxExOwnerName.Text = carMasterVo.OwnerName;                                                                  // 所有者の氏名又は名称
            this.ComboBoxExOwnerAddress.Text = carMasterVo.OwnerAddress;                                                            // 所有者の住所
            this.ComboBoxExUserName.Text = carMasterVo.UserName;                                                                    // 使用者の氏名又は名称
            this.ComboBoxExUserAddress.Text = carMasterVo.UserAddress;                                                              // 使用者の住所
            this.ComboBoxExBaseAddress.Text = carMasterVo.BaseAddress;                                                              // 使用の本拠の位置
            /*
             * ３．車両詳細情報
             */
            this.ComboBoxExManufacturerCode.Text = _dictionaryManufacturerName[carMasterVo.ManufacturerCode];                       // 車名
            this.TextBoxExVersion.Text = carMasterVo.Version;                                                                       // 型式
            this.TextBoxExMotorVersion.Text = carMasterVo.MotorVersion;                                                             // 原動機の型式
            this.ComboBoxExCarKindCode.Text = _dictionaryCarKindName[carMasterVo.CarKindCode];                                      // 自動車の種別
            this.ComboBoxExCarUse.Text = carMasterVo.CarUse;                                                                        // 用途
            this.ComboBoxExOtherCode.Text = _dictionaryOtherName[carMasterVo.OtherCode];                                            // 自家用・事業用の別
            this.ComboBoxExShapeCode.Text = _dictionaryShapeName[carMasterVo.ShapeCode];                                            // 車体の形状
            this.NumericUpDownExCapacity.Value = carMasterVo.Capacity;                                                              // 乗車定員
            this.NumericUpDownExMaximumLoadCapacity.Value = carMasterVo.MaximumLoadCapacity;                                        // 最大積載量
            this.NumericUpDownExVehicleWeight.Value = carMasterVo.VehicleWeight;                                                    // 車両重量
            this.NumericUpDownExTotalVehicleWeight.Value = carMasterVo.TotalVehicleWeight;                                          // 車両総重量
            this.NumericUpDownExLength.Value = carMasterVo.Length;                                                                  // 長さ
            this.NumericUpDownExWidth.Value = carMasterVo.Width;                                                                    // 幅
            this.NumericUpDownExHeight.Value = carMasterVo.Height;                                                                  // 高さ 
            this.NumericUpDownExFfAxisWeight.Value = carMasterVo.FfAxisWeight;                                                      // 前前軸重
            this.NumericUpDownExFrAxisWeight.Value = carMasterVo.FrAxisWeight;                                                      // 前後軸重
            this.NumericUpDownExRfAxisWeight.Value = carMasterVo.RfAxisWeight;                                                      // 後前軸重
            this.NumericUpDownExRrAxisWeight.Value = carMasterVo.RrAxisWeight;                                                      // 後後軸重
            this.NumericUpDownExTotalDisplacement.Value = carMasterVo.TotalDisplacement;                                            // 総排気量又は定格出力
            this.ComboBoxExTypesOfFuel.Text = carMasterVo.TypesOfFuel;                                                              // 燃料の種類
            this.TextBoxExVersionDesignateNumber.Text = carMasterVo.VersionDesignateNumber;                                         // 型式指定番号
            this.TextBoxExCategoryDistinguishNumber.Text = carMasterVo.CategoryDistinguishNumber;                                   // 類別区分番号
            this.TextBoxExRemarks.Text = carMasterVo.Remarks;                                                                       // 備考
            if (carMasterVo.MainPicture.Length != 0) {
                ImageConverter imageConverter = new();
                this.PictureBoxExMainPicture.Image = (Image?)imageConverter.ConvertFrom(carMasterVo.MainPicture);                   // 写真
            }
            if (carMasterVo.SubPicture.Length != 0) {
                ImageConverter imageConverter = new();
                this.PictureBoxExSubPicture.Image = (Image?)imageConverter.ConvertFrom(carMasterVo.SubPicture);                     // 写真
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBoxEx_DoubleClick(object sender, EventArgs e) {
            CarVehicleInspectionView carVehicleInspectionView = new(_connectionVo, ((PictureBoxEx)sender).Image);
            _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), carVehicleInspectionView);
            carVehicleInspectionView.ShowDialog(this);
        }

        /*
         * TextBoxExRegistrationNumberの変更
         */
        private void ComboBoxExRegistrationNumber1_SelectedIndexChanged(object sender, EventArgs e) {
            this.TextBoxExRegistrationNumber.Text = this.SetTextBoxExRegistrationNumber();
        }
        private void TextBoxExRegistrationNumber_TextChanged(object sender, EventArgs e) {
            this.TextBoxExRegistrationNumber.Text = this.SetTextBoxExRegistrationNumber();
        }
        private string SetTextBoxExRegistrationNumber() {
            return string.Concat(ComboBoxExRegistrationNumber1.Text, TextBoxExRegistrationNumber2.Text, TextBoxExRegistrationNumber3.Text, TextBoxExRegistrationNumber4.Text);
        }

        /// <summary>
        /// ToolStripMenuItemPrintA4_Click
        /// </summary>
        private void ToolStripMenuItemPrintA4_Click() {
            PrintDocument _printDocument = new();
            _printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);
            // 出力先プリンタを指定します。
            //printDocument.PrinterSettings.PrinterName = "(PrinterName)";
            // 印刷部数を指定します。
            _printDocument.PrinterSettings.Copies = 1;
            // 両面印刷に設定します。
            _printDocument.PrinterSettings.Duplex = Duplex.Vertical;
            // カラー印刷に設定します。
            _printDocument.PrinterSettings.DefaultPageSettings.Color = true;
            _printDocument.Print();
        }

        /// <summary>
        /// printDocument_PrintPage
        /// </summary>
        private int _curPageNumber = 0; // 現在のページ番号
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e) {
            try {
                if (_curPageNumber == 0) {
                    /*
                     * 新型車検証
                     */
                    if (this.PictureBoxExMainPicture.Image is not null) {
                        // 新型車検証のサイズ(１０５＊１７７.８)
                        Rectangle rectangle = new(0, 0, 177 * 4, 105 * 4);
                        e.Graphics.DrawImage(this.PictureBoxExMainPicture.Image, rectangle);
                    }
                    e.HasMorePages = true;
                } else {
                    /*
                     * 記録事項と旧型車検証
                     */
                    if (this.PictureBoxExSubPicture.Image is not null) {
                        Rectangle rectangle = new(e.PageBounds.X, e.PageBounds.Y, e.PageBounds.Width, e.PageBounds.Height);
                        e.Graphics.DrawImage(this.PictureBoxExSubPicture.Image, rectangle);
                    }
                    e.HasMorePages = false;
                }
                _curPageNumber++;
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarDetail_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
