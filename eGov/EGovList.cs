/*
 * 2025-08-28
 */
using Vo;

namespace EGov {
    public partial class EGovList : Form {
        private EGobApi eGobApi = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public EGovList(ConnectionVo connectionVo, Screen screen) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;

            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExUpdate_Click(object sender, EventArgs e) {
            try {
                this.eGobApi.GetLawData("昭和二十三年法律第百八十六号");
            } catch (HttpRequestException httpRequestException) {
                MessageBox.Show(httpRequestException.Message);
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EGovList_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
