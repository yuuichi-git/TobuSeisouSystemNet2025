/*
 * 2024-11-24
 */
using ControlEx;

using Vo;

namespace StockBox {
    public partial class StockBoxs : Form {
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;
        public StockBoxs(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            /*
             * InitializeControl
             */
            InitializeComponent();

            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "InitializeSuccess";
        }

        private void ButtonEx_Click(object sender, EventArgs e) {
            switch (((ButtonEx)sender).Name) {
                case "ButtonExUpdate":
                    try {
                        AddControls(_vehicleDispatchDetailDao.SelectAllVehicleDispatchDetail(DateTimePickerExOperationDate.GetDate()));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
            }
        }


    }
}
