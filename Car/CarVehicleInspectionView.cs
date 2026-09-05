/*
 * 2025-02-18
 */
using Dao;

using Vo;

namespace Car {
    public partial class CarVehicleInspectionView : Form {
        /*
         * Dao
         */
        private CarMasterDao _carMasterDao;

        /// <summary>
        /// コンストラクター(VehicleDispatchBoardからの呼び出し)
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="carCode"></param>
        public CarVehicleInspectionView(ConnectionVo connectionVo, int carCode) {
            /*
             * Dao
             */
            _carMasterDao = new(connectionVo);
            /*
             * InitializeControl
             */
            InitializeComponent();
            /*
             * MenuStrip
             */
            List<string> listString = new() {"ToolStripMenuItemFile",
                                             "ToolStripMenuItemExit",
                                             "ToolStripMenuItemHelp"};
            this.MenuStripEx1.ChangeEnable(listString);
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;

            byte[] recordDetailsPicture = _carMasterDao.SelectOneRecordDetailsPicture(carCode);
            this.CcPdfView1.SetPdfBytes(recordDetailsPicture);

            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch(((ToolStripMenuItem)sender).Name) {
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
        private void CarVehicleInspectionView_FormClosing(object sender, FormClosingEventArgs e) {
            this.Dispose();
        }
    }
}
