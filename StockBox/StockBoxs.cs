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
            /*
             * Form
             */
            this.Opacity = 0.9;
            /*
             * MenuStrip
             */
            List<string> listString = new() {
                "ToolStripMenuItemFile",
                "ToolStripMenuItemExit",
                "ToolStripMenuItemHelp"
            };
            MenuStripEx1.ChangeEnable(listString);

            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "InitializeSuccess";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEx_Click(object sender, EventArgs e) {
            switch (((ButtonEx)sender).Name) {
                case "":
                    try {

                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StockBoxs_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
