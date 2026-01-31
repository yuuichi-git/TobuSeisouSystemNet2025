/*
 * 2025-06-12
 */
using ControlEx;

using Dao;

using Vo;

namespace Waste {
    public partial class WasteDetail : Form {
        private int _targetId = 0;
        private ErrorProvider _errorProvider = new();                                               // ErrorProviderのインスタンスを生成
        /*
         * Dao
         */
        private WasteCustomerDao _wasteCustomerDao;
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクター(New)
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public WasteDetail(ConnectionVo connectionVo, Screen screen) {
            // アイコンを点滅なしに設定する
            _errorProvider.BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;
            /*
             * Dao
             */
            _wasteCustomerDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
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
            this.InitializeControls();
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;
            /*
             * Eventを登録する
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// コンストラクター(Update)
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        /// <param name="targetId"></param>
        public WasteDetail(ConnectionVo connectionVo, Screen screen, int targetId) {
            _targetId = targetId;
            // アイコンを点滅なしに設定する
            _errorProvider.BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;
            /*
             * Dao
             */
            _wasteCustomerDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
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
            this.InitializeControls();
            this.SetControls(targetId);
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
            switch (((CcButton)sender).Name) {
                case "ButtonExUpdate":
                    _errorProvider.Clear();                                                         // ErrorProviderをクリアします。
                    /*
                     * バリデーション
                     */
                    if (String.IsNullOrEmpty(TextBoxExEmissionCompanyKana.Text)) {                  // 排出事業者(フリガナ)
                        _errorProvider.SetError(TextBoxExEmissionCompanyKana, "必須項目です");
                        return;
                    }
                    if (String.IsNullOrEmpty(ComboBoxExEmissionCompanyName.Text)) {                 // 排出事業者
                        _errorProvider.SetError(ComboBoxExEmissionCompanyName, "必須項目です");
                        return;
                    }
                    if (String.IsNullOrEmpty(TextBoxExEmissionPlaceName.Text)) {                    // 排出事業場所名称
                        _errorProvider.SetError(TextBoxExEmissionPlaceName, "必須項目です");
                        return;
                    }
                    if (String.IsNullOrEmpty(TextBoxExEmissionPlaceAddress.Text)) {                 // 排出事業場所
                        _errorProvider.SetError(TextBoxExEmissionPlaceAddress, "必須項目です");
                        return;
                    }
                    if (_wasteCustomerDao.ExistenceWasteCustomerVo(_targetId)) {
                        WasteCustomerVo wasteCustomerVo = new();
                        wasteCustomerVo.Id = _targetId;
                        _wasteCustomerDao.UpdateOneWasteCustomerVo(this.SetVo(wasteCustomerVo));
                    } else {
                        WasteCustomerVo wasteCustomerVo = new();
                        wasteCustomerVo.Id = _wasteCustomerDao.GetId();
                        _wasteCustomerDao.InsertOneWasteCustomerVo(this.SetVo(wasteCustomerVo));
                    }
                    this.Close();
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
                case "ToolStripMenuItemExit": // アプリケーションを終了する
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// Voにセットする
        /// ここでは”ID”主キーには代入しない。INSERTとUPDATEでは採番処理が違うため
        /// </summary>
        /// <param name="wasteCustomerVo"></param>
        /// <returns></returns>
        private WasteCustomerVo SetVo(WasteCustomerVo wasteCustomerVo) {
            wasteCustomerVo.ConcludedDate = DateTimePickerExConcludedDate.GetDate();                                                                // 契約締結日
            wasteCustomerVo.ConcludedDetail = ComboBoxExConcludedDetail.Text;                                                                       // 契約内容
            wasteCustomerVo.TransportationCompany = ComboBoxExTransportationCompany.Text;                                                           // 運搬会社
            wasteCustomerVo.EmissionCompanyKana = TextBoxExEmissionCompanyKana.Text;                                                                // 排出事業者フリガナ
            wasteCustomerVo.EmissionCompanyName = ComboBoxExEmissionCompanyName.Text;                                                               // 排出事業者名称
            wasteCustomerVo.PostNumber = MaskedTextBoxExPostNumber.Text;                                                                            // 排出事業者郵便番号
            wasteCustomerVo.Address = TextBoxExAddress.Text;                                                                                        // 排出事業者住所
            wasteCustomerVo.TelephoneNumber = MaskedTextBoxExTelephoneNumber.Text;                                                                  // 排出事業者電話番号
            wasteCustomerVo.FaxNumber = MaskedTextBoxExFaxNumber.Text;                                                                              // 排出事業者FAX番号
            wasteCustomerVo.EmissionPlaceName = TextBoxExEmissionPlaceName.Text;                                                                    // 排出事業所名称
            wasteCustomerVo.EmissionPlaceAddress = TextBoxExEmissionPlaceAddress.Text;                                                              // 排出事業所住所
            wasteCustomerVo.UnitPriceFlammable = NumericUpDownExUnitPriceFlammable.Value;                                                           // 単価　可燃
            wasteCustomerVo.UnitPriceCollection = NumericUpDownExUnitPriceCollection.Value;                                                         // 単価　不燃収集
            wasteCustomerVo.UnitPriceDisposal = NumericUpDownExUnitPriceDisposal.Value;                                                             // 単価　不燃処分
            wasteCustomerVo.UnitPriceResources = NumericUpDownExUnitPriceResources.Value;                                                           // 単価　資源
            wasteCustomerVo.UnitPriceTransportationCosts = NumericUpDownExUnitPriceTransportationCosts.Value;                                       // 単価　運搬費
            wasteCustomerVo.UnitPriceManifestCosts = NumericUpDownExUnitPriceManifestCosts.Value;                                                   // 単価　マニフェスト費用
            wasteCustomerVo.UnitPriceOtherCosts = NumericUpDownExUnitPriceOtherCosts.Value;                                                         // 単価　その他費用
            wasteCustomerVo.UnitPriceBulkyTransportationCosts = NumericUpDownExUnitPriceBulkyTransportationCosts.Value;                             // 単価　粗大運搬費
            wasteCustomerVo.UnitPriceBulkyDisposal = NumericUpDownExUnitPriceBulkyDisposal.Value;                                                   // 単価　粗大処分費
            wasteCustomerVo.Remarks = TextBoxExRemarks.Text;                                                                                        // 備考
            wasteCustomerVo.InsertPcName = Environment.MachineName;
            wasteCustomerVo.InsertYmdHms = DateTime.Now;
            wasteCustomerVo.UpdatePcName = Environment.MachineName;
            wasteCustomerVo.UpdateYmdHms = DateTime.Now;
            wasteCustomerVo.DeletePcName = Environment.MachineName;
            wasteCustomerVo.DeleteYmdHms = DateTime.Now;
            wasteCustomerVo.DeleteFlag = false;
            return wasteCustomerVo;
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeControls() {
            this.DateTimePickerExConcludedDate.SetToday();                                                                                          // 契約締結日
            this.ComboBoxExConcludedDetail.Text = string.Empty;                                                                                     // 契約内容
            this.ComboBoxExTransportationCompany.Text = string.Empty;                                                                               // 運搬会社
            this.TextBoxExEmissionCompanyKana.Text = string.Empty;                                                                                  // 排出事業者フリガナ
            this.ComboBoxExEmissionCompanyName.Text = string.Empty;                                                                                 // 排出事業者名称
            this.MaskedTextBoxExPostNumber.Text = string.Empty;                                                                                     // 排出事業者郵便番号
            this.TextBoxExAddress.Text = string.Empty;                                                                                              // 排出事業者住所
            this.MaskedTextBoxExTelephoneNumber.Text = string.Empty;                                                                                // 排出事業者電話番号
            this.MaskedTextBoxExFaxNumber.Text = string.Empty;                                                                                      // 排出事業者FAX番号
            this.TextBoxExEmissionPlaceName.Text = string.Empty;                                                                                    // 排出事業所名称
            this.TextBoxExEmissionPlaceAddress.Text = string.Empty;                                                                                 // 排出事業所住所
            this.NumericUpDownExUnitPriceFlammable.Value = 0;                                                                                       // 単価　可燃
            this.NumericUpDownExUnitPriceCollection.Value = 0;                                                                                      // 単価　不燃収集
            this.NumericUpDownExUnitPriceDisposal.Value = 0;                                                                                        // 単価　不燃処分
            this.NumericUpDownExUnitPriceResources.Value = 0;                                                                                       // 単価　資源
            this.NumericUpDownExUnitPriceTransportationCosts.Value = 0;                                                                             // 単価　運搬費
            this.NumericUpDownExUnitPriceManifestCosts.Value = 0;                                                                                   // 単価　マニフェスト費用
            this.NumericUpDownExUnitPriceOtherCosts.Value = 0;                                                                                      // 単価　その他費用
            this.NumericUpDownExUnitPriceBulkyTransportationCosts.Value = 0;                                                                        // 単価　粗大運搬費
            this.NumericUpDownExUnitPriceBulkyDisposal.Value = 0;                                                                                   // 単価　粗大処分費
            this.TextBoxExRemarks.Text = string.Empty;                                                                                              // 備考
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetId"></param>
        private void SetControls(int targetId) {
            WasteCustomerVo wasteCustomerVo = _wasteCustomerDao.SelectOneWasteCustomerVo(targetId);
            this.DateTimePickerExConcludedDate.SetValue(wasteCustomerVo.ConcludedDate);
            this.ComboBoxExConcludedDetail.Text = wasteCustomerVo.ConcludedDetail;
            this.ComboBoxExTransportationCompany.Text = wasteCustomerVo.TransportationCompany;
            this.TextBoxExEmissionCompanyKana.Text = wasteCustomerVo.EmissionCompanyKana;
            this.ComboBoxExEmissionCompanyName.Text = wasteCustomerVo.EmissionCompanyName;
            this.MaskedTextBoxExPostNumber.Text = wasteCustomerVo.PostNumber;
            this.TextBoxExAddress.Text = wasteCustomerVo.Address;
            this.MaskedTextBoxExTelephoneNumber.Text = wasteCustomerVo.TelephoneNumber;
            this.MaskedTextBoxExFaxNumber.Text = wasteCustomerVo.FaxNumber;
            this.TextBoxExEmissionPlaceName.Text = wasteCustomerVo.EmissionPlaceName;
            this.TextBoxExEmissionPlaceAddress.Text = wasteCustomerVo.EmissionPlaceAddress;
            this.NumericUpDownExUnitPriceFlammable.Value = wasteCustomerVo.UnitPriceFlammable;
            this.NumericUpDownExUnitPriceCollection.Value = wasteCustomerVo.UnitPriceCollection;
            this.NumericUpDownExUnitPriceDisposal.Value = wasteCustomerVo.UnitPriceDisposal;
            this.NumericUpDownExUnitPriceResources.Value = wasteCustomerVo.UnitPriceResources;
            this.NumericUpDownExUnitPriceTransportationCosts.Value = wasteCustomerVo.UnitPriceTransportationCosts;
            this.NumericUpDownExUnitPriceManifestCosts.Value = wasteCustomerVo.UnitPriceManifestCosts;
            this.NumericUpDownExUnitPriceOtherCosts.Value = wasteCustomerVo.UnitPriceOtherCosts;
            this.NumericUpDownExUnitPriceBulkyTransportationCosts.Value = wasteCustomerVo.UnitPriceBulkyTransportationCosts;
            this.NumericUpDownExUnitPriceBulkyDisposal.Value = wasteCustomerVo.UnitPriceBulkyDisposal;
            this.TextBoxExRemarks.Text = wasteCustomerVo.Remarks;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WasteDetail_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
