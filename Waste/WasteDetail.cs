/*
 * 2025-06-12
 */
using ControlEx;

using Dao;

using Vo;

namespace Waste {
    public partial class WasteDetail : Form {
        private int _targetId = 0;
        private ErrorProvider _errorProvider = new();                                               // ErrorProvider�̃C���X�^���X�𐶐�
        /*
         * Dao
         */
        private WasteCustomerDao _wasteCustomerDao;
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;

        /// <summary>
        /// �R���X�g���N�^�[(New)
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public WasteDetail(ConnectionVo connectionVo, Screen screen) {
            // �A�C�R����_�łȂ��ɐݒ肷��
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
             * Event��o�^����
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// �R���X�g���N�^�[(Update)
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        /// <param name="targetId"></param>
        public WasteDetail(ConnectionVo connectionVo, Screen screen, int targetId) {
            _targetId = targetId;
            // �A�C�R����_�łȂ��ɐݒ肷��
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
             * Event��o�^����
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExUpdate_Click(object sender, EventArgs e) {
            switch (((ButtonEx)sender).Name) {
                case "ButtonExUpdate":
                    _errorProvider.Clear();                                                         // ErrorProvider���N���A���܂��B
                    /*
                     * �o���f�[�V����
                     */
                    if (String.IsNullOrEmpty(TextBoxExEmissionCompanyKana.Text)) {                  // �r�o���Ǝ�(�t���K�i)
                        _errorProvider.SetError(TextBoxExEmissionCompanyKana, "�K�{���ڂł�");
                        return;
                    }
                    if (String.IsNullOrEmpty(ComboBoxExEmissionCompanyName.Text)) {                 // �r�o���Ǝ�
                        _errorProvider.SetError(ComboBoxExEmissionCompanyName, "�K�{���ڂł�");
                        return;
                    }
                    if (String.IsNullOrEmpty(TextBoxExEmissionPlaceName.Text)) {                    // �r�o���Əꏊ����
                        _errorProvider.SetError(TextBoxExEmissionPlaceName, "�K�{���ڂł�");
                        return;
                    }
                    if (String.IsNullOrEmpty(TextBoxExEmissionPlaceAddress.Text)) {                 // �r�o���Əꏊ
                        _errorProvider.SetError(TextBoxExEmissionPlaceAddress, "�K�{���ڂł�");
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
                case "ToolStripMenuItemExit": // �A�v���P�[�V�������I������
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// Vo�ɃZ�b�g����
        /// �����ł́hID�h��L�[�ɂ͑�����Ȃ��BINSERT��UPDATE�ł͍̔ԏ������Ⴄ����
        /// </summary>
        /// <param name="wasteCustomerVo"></param>
        /// <returns></returns>
        private WasteCustomerVo SetVo(WasteCustomerVo wasteCustomerVo) {
            wasteCustomerVo.ConcludedDate = DateTimePickerExConcludedDate.GetDate();                                                                // �_�������
            wasteCustomerVo.ConcludedDetail = ComboBoxExConcludedDetail.Text;                                                                       // �_����e
            wasteCustomerVo.TransportationCompany = ComboBoxExTransportationCompany.Text;                                                           // �^�����
            wasteCustomerVo.EmissionCompanyKana = TextBoxExEmissionCompanyKana.Text;                                                                // �r�o���Ǝ҃t���K�i
            wasteCustomerVo.EmissionCompanyName = ComboBoxExEmissionCompanyName.Text;                                                               // �r�o���ƎҖ���
            wasteCustomerVo.PostNumber = MaskedTextBoxExPostNumber.Text;                                                                            // �r�o���ƎҗX�֔ԍ�
            wasteCustomerVo.Address = TextBoxExAddress.Text;                                                                                        // �r�o���ƎҏZ��
            wasteCustomerVo.TelephoneNumber = MaskedTextBoxExTelephoneNumber.Text;                                                                  // �r�o���Ǝғd�b�ԍ�
            wasteCustomerVo.FaxNumber = MaskedTextBoxExFaxNumber.Text;                                                                              // �r�o���Ǝ�FAX�ԍ�
            wasteCustomerVo.EmissionPlaceName = TextBoxExEmissionPlaceName.Text;                                                                    // �r�o���Ə�����
            wasteCustomerVo.EmissionPlaceAddress = TextBoxExEmissionPlaceAddress.Text;                                                              // �r�o���Ə��Z��
            wasteCustomerVo.UnitPriceFlammable = NumericUpDownExUnitPriceFlammable.Value;                                                           // �P���@�R
            wasteCustomerVo.UnitPriceCollection = NumericUpDownExUnitPriceCollection.Value;                                                         // �P���@�s�R���W
            wasteCustomerVo.UnitPriceDisposal = NumericUpDownExUnitPriceDisposal.Value;                                                             // �P���@�s�R����
            wasteCustomerVo.UnitPriceResources = NumericUpDownExUnitPriceResources.Value;                                                           // �P���@����
            wasteCustomerVo.UnitPriceTransportationCosts = NumericUpDownExUnitPriceTransportationCosts.Value;                                       // �P���@�^����
            wasteCustomerVo.UnitPriceManifestCosts = NumericUpDownExUnitPriceManifestCosts.Value;                                                   // �P���@�}�j�t�F�X�g��p
            wasteCustomerVo.UnitPriceOtherCosts = NumericUpDownExUnitPriceOtherCosts.Value;                                                         // �P���@���̑���p
            wasteCustomerVo.UnitPriceBulkyTransportationCosts = NumericUpDownExUnitPriceBulkyTransportationCosts.Value;                             // �P���@�e��^����
            wasteCustomerVo.UnitPriceBulkyDisposal = NumericUpDownExUnitPriceBulkyDisposal.Value;                                                   // �P���@�e�又����
            wasteCustomerVo.Remarks = TextBoxExRemarks.Text;                                                                                        // ���l
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
            this.DateTimePickerExConcludedDate.SetToday();                                                                                          // �_�������
            this.ComboBoxExConcludedDetail.Text = string.Empty;                                                                                     // �_����e
            this.ComboBoxExTransportationCompany.Text = string.Empty;                                                                               // �^�����
            this.TextBoxExEmissionCompanyKana.Text = string.Empty;                                                                                  // �r�o���Ǝ҃t���K�i
            this.ComboBoxExEmissionCompanyName.Text = string.Empty;                                                                                 // �r�o���ƎҖ���
            this.MaskedTextBoxExPostNumber.Text = string.Empty;                                                                                     // �r�o���ƎҗX�֔ԍ�
            this.TextBoxExAddress.Text = string.Empty;                                                                                              // �r�o���ƎҏZ��
            this.MaskedTextBoxExTelephoneNumber.Text = string.Empty;                                                                                // �r�o���Ǝғd�b�ԍ�
            this.MaskedTextBoxExFaxNumber.Text = string.Empty;                                                                                      // �r�o���Ǝ�FAX�ԍ�
            this.TextBoxExEmissionPlaceName.Text = string.Empty;                                                                                    // �r�o���Ə�����
            this.TextBoxExEmissionPlaceAddress.Text = string.Empty;                                                                                 // �r�o���Ə��Z��
            this.NumericUpDownExUnitPriceFlammable.Value = 0;                                                                                       // �P���@�R
            this.NumericUpDownExUnitPriceCollection.Value = 0;                                                                                      // �P���@�s�R���W
            this.NumericUpDownExUnitPriceDisposal.Value = 0;                                                                                        // �P���@�s�R����
            this.NumericUpDownExUnitPriceResources.Value = 0;                                                                                       // �P���@����
            this.NumericUpDownExUnitPriceTransportationCosts.Value = 0;                                                                             // �P���@�^����
            this.NumericUpDownExUnitPriceManifestCosts.Value = 0;                                                                                   // �P���@�}�j�t�F�X�g��p
            this.NumericUpDownExUnitPriceOtherCosts.Value = 0;                                                                                      // �P���@���̑���p
            this.NumericUpDownExUnitPriceBulkyTransportationCosts.Value = 0;                                                                        // �P���@�e��^����
            this.NumericUpDownExUnitPriceBulkyDisposal.Value = 0;                                                                                   // �P���@�e�又����
            this.TextBoxExRemarks.Text = string.Empty;                                                                                              // ���l
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
