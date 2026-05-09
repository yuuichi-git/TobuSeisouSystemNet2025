/*
 * 2026-05-05
 */
using Dao;
using Vo;

namespace DriversReport {
    public partial class ContinuousDrivingTimePaper : Form {
        /*
         * Dao
         */
        private ContinuousDrivingTimePaperDao _continuousDrivingTimePaperDao;
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;
        private StaffMasterVo _staffMasterVo;

        public ContinuousDrivingTimePaper(ConnectionVo connectionVo, Screen screen) {
            /*
             * Dao
             */
            _continuousDrivingTimePaperDao = new ContinuousDrivingTimePaperDao(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            /*
             * Initialize
             */
            InitializeComponent();

        }

        private void CcButtonUpdate_Click(object sender, EventArgs e) {

        }


    }
}
