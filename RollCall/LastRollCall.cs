/*
 * 2024-12-11
 */
using Common;

using ControlEx;

using Dao;

using Vo;

namespace RollCall {
    public partial class LastRollCall : Form {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly DateUtility _dateUtility = new();
        private readonly SetControl _setControl;
        /*
         * Dao
         */
        private readonly VehicleDispatchDetailDao _vehicleDispatchDetailDao;
        private readonly LastRollCallDao _lastRollCallDao;
        /*
         * Vo
         */
        private LastRollCallVo _beforeLastRollCallVo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="setControl"></param>
        public LastRollCall(ConnectionVo connectionVo, SetControl setControl) {
            /*
             * インスタンス
             */
            _setControl = setControl;
            /*
             * Dao
             */
            _vehicleDispatchDetailDao = new(connectionVo);
            _lastRollCallDao = new(connectionVo);
            /*
             * Vo
             */
            _beforeLastRollCallVo = null;
            /*
             * InitializeControl
             */
            InitializeComponent();
            /*
             * SQL
             */
            try {
                if (_lastRollCallDao.ExistenceLastRollCall(_setControl.SetCode, _setControl.OperationDate, _setControl.LastRollCallYmdHms)) {
                    _beforeLastRollCallVo = _lastRollCallDao.SelectOneLastRollCall(_setControl.SetCode, _setControl.OperationDate, _setControl.LastRollCallYmdHms);
                    this.SetControl(_beforeLastRollCallVo);
                } else {
                    this.InitializeControl();
                }
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// コントロールを初期化する
        /// </summary>
        private void InitializeControl() {
            this.LabelExSetName.Text = string.Concat(((SetLabel)_setControl.DeployedSetLabel).SetMasterVo.SetName, " の帰庫点呼入力");
            this.DateTimePickerExOperationDate.SetValue(_setControl.OperationDate);
            this.CcTimeFirstRollCallTime.Text = _vehicleDispatchDetailDao.GetStaffRollCallYmdHms1(_setControl.CellNumber, _setControl.OperationDate).ToString("HH:mm");
            this.NumericUpDownExLastPlantCount.Value = 0;
            this.ComboBoxExLastPlantName.SelectedIndex = -1;
            this.NumericUpDownExFirstOdoMeter.Value = 0;
            this.NumericUpDownExLastOdoMeter.Value = 0;
            this.NumericUpDownExOilAmount.Value = 0;
            this.CheckBoxExDelete.Checked = false;
        }

        /// <summary>
        /// ControlにVoの値をセットする
        /// </summary>
        /// <param name="lastRollCallVo"></param>
        private void SetControl(LastRollCallVo lastRollCallVo) {
            this.LabelExSetName.Text = string.Concat(((SetLabel)_setControl.DeployedSetLabel).SetMasterVo.SetName, " の帰庫点呼入力");
            this.DateTimePickerExOperationDate.SetValueJp(lastRollCallVo.OperationDate.Date);
            this.CcTimeFirstRollCallTime.Text = lastRollCallVo.FirstRollCallYmdHms.ToString("HH:mm");
            this.NumericUpDownExLastPlantCount.Value = lastRollCallVo.LastPlantCount;
            this.ComboBoxExLastPlantName.Text = lastRollCallVo.LastPlantName;
            this.CcTimeLastPlantTime.Text = lastRollCallVo.LastPlantYmdHms.ToString("HH:mm");
            this.CcTimeLastRollCallTime.Text = lastRollCallVo.LastRollCallYmdHms.ToString("HH:mm");
            this.NumericUpDownExFirstOdoMeter.Value = lastRollCallVo.FirstOdoMeter;
            this.NumericUpDownExLastOdoMeter.Value = lastRollCallVo.LastOdoMeter;
            this.NumericUpDownExOilAmount.Value = lastRollCallVo.OilAmount;
        }

        /// <summary>
        /// VoにControlの値をセットする
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private LastRollCallVo SetVo(DateTime dateTime) {
            LastRollCallVo newLastRollCallVo = new();
            newLastRollCallVo.SetCode = _setControl.SetCode;
            newLastRollCallVo.OperationDate = this.DateTimePickerExOperationDate.GetValue().Date;
            newLastRollCallVo.FirstRollCallYmdHms = _dateUtility.GetStringTimeToDateTime(this.DateTimePickerExOperationDate.GetValue(), this.CcTimeFirstRollCallTime.Text);
            newLastRollCallVo.LastPlantCount = (int)this.NumericUpDownExLastPlantCount.Value;
            newLastRollCallVo.LastPlantName = this.ComboBoxExLastPlantName.Text;
            newLastRollCallVo.LastPlantYmdHms = _dateUtility.GetStringTimeToDateTime(this.DateTimePickerExOperationDate.GetValue(), this.CcTimeLastPlantTime.Text);
            newLastRollCallVo.LastRollCallYmdHms = dateTime;
            newLastRollCallVo.FirstOdoMeter = this.NumericUpDownExFirstOdoMeter.Value;
            newLastRollCallVo.LastOdoMeter = this.NumericUpDownExLastOdoMeter.Value;
            newLastRollCallVo.OilAmount = this.NumericUpDownExOilAmount.Value;
            newLastRollCallVo.InsertPcName = Environment.MachineName;
            newLastRollCallVo.InsertYmdHms = DateTime.Now;
            newLastRollCallVo.UpdatePcName = Environment.MachineName;
            newLastRollCallVo.UpdateYmdHms = DateTime.Now;
            newLastRollCallVo.DeletePcName = Environment.MachineName;
            newLastRollCallVo.DeleteYmdHms = DateTime.Now;
            newLastRollCallVo.DeleteFlag = false;
            return newLastRollCallVo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExUpdate_Click(object sender, EventArgs e) {
            SetControl setControl = _setControl;
            SetLabel setLabel = (SetLabel)_setControl.DeployedSetLabel;
            DateTime oldLastRollCallTime = setLabel.LastRollCallYmdHms;
            DateTime newLastRollCallTime = _dateUtility.GetStringTimeToDateTime(this.DateTimePickerExOperationDate.GetValue(), this.CcTimeLastRollCallTime.Text);
            if (!this.CheckBoxExDelete.Checked) {
                if (_lastRollCallDao.ExistenceLastRollCall(setControl.SetCode, setControl.OperationDate, oldLastRollCallTime)) {
                    try {
                        setLabel.LastRollCallFlag = true;
                        setLabel.LastRollCallYmdHms = newLastRollCallTime;
                        /*
                         * VehicleDispatchDetailVoを書き換える
                         */
                        _vehicleDispatchDetailDao.UpdateOneLastRollCall(true, setControl.CellNumber, this.SetVo(newLastRollCallTime));
                        _lastRollCallDao.UpdateOneLastRollCall(((SetLabel)_setControl.DeployedSetLabel).SetMasterVo.SetCode, setControl.OperationDate, oldLastRollCallTime, this.SetVo(newLastRollCallTime));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                } else {
                    try {
                        setLabel.LastRollCallFlag = true;
                        setLabel.LastRollCallYmdHms = newLastRollCallTime;
                        /*
                         * VehicleDispatchDetailVoを書き換える
                         */
                        _vehicleDispatchDetailDao.UpdateOneLastRollCall(true, setControl.CellNumber, this.SetVo(newLastRollCallTime));
                        _lastRollCallDao.InsertOneLastRollCall(this.SetVo(newLastRollCallTime));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                }
            } else {
                try {
                    setLabel.LastRollCallFlag = false;
                    setLabel.LastRollCallYmdHms = _defaultDateTime;
                    /*
                     * VehicleDispatchDetailVoを書き換える
                     */
                    _vehicleDispatchDetailDao.DeleteOneLastRollCall(setControl.CellNumber, this.SetVo(oldLastRollCallTime));
                    _lastRollCallDao.DeleteOneLastRollCall(setControl.SetCode, setControl.OperationDate, oldLastRollCallTime);
                } catch (Exception exception) {
                    MessageBox.Show(exception.Message);
                }
            }
            this.Close();
        }

        /// <summary>
        /// TabでFocusを移動する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastRollCall_KeyDown(object sender, KeyEventArgs e) {
            bool forward;
            if (e.KeyCode == Keys.Enter) {
                forward = e.Modifiers != Keys.Shift;                                                                // Shiftキーが押されているかの判定
                this.SelectNextControl(this.ActiveControl, forward, true, true, true);                              // タブオーダー順で次のコントロールにフォーカスを移動
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastRollCall_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
