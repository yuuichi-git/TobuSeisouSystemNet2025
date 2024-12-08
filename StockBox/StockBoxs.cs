/*
 * 2024-11-24
 */
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


    }
}
