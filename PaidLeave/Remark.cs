/*
 * 2026-06-02
 */
using CcControl;

using Dao;

using Vo;

namespace PaidLeave {
    public partial class Remark : Form {
        /*
         * Dao
         */
        private TimeOffMasterDao _timeOffMasterDao;
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;
        /*
         * その他パラメータ
         */
        private DateTime _date;
        private int _code;
        private int _staffCode;

        public Remark(ConnectionVo connectionVo, DateTime date, int code, int staffCode) {
            /*
             * Dao
             */
            _timeOffMasterDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            /*
             * その他パラメータ
             */
            _date = date;
            _code = code;
            _staffCode = staffCode;
            /*
             * InitializeControl
             */
            InitializeComponent();
            InitializeControl();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CcButton_Click(object sender, EventArgs e) {
            switch (((CcButton)sender).Name) {
                case "CcButtonTimeStamp":
                    if (this.CcTextBoxRemark.Text.Length > 0) {
                        this.CcTextBoxRemark.Text += DateTime.Now.ToString(Environment.NewLine + "yyyy年MM月dd日 HH時mm分ss秒：");
                    } else {
                        this.CcTextBoxRemark.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH時mm分ss秒：");
                    }
                    break;
                case "CcButtonUpdate":
                    _timeOffMasterDao.UpdateOneRemarks(_date, _code, _staffCode, this.CcTextBoxRemark.Text);
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// 休暇マスタの備考を表示する
        /// </summary>
        private void InitializeControl() {
            TimeOffMasterVo? timeOffMasterVo = _timeOffMasterDao.SelectOneTimeOffMaster(_date, _code, _staffCode);
            if (timeOffMasterVo != null) {
                this.CcTextBoxRemark.Text = timeOffMasterVo.Remarks;
            } else {
                this.CcTextBoxRemark.Text = string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Remark_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
