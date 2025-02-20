/*
 * 2025-02-19
 */
using Vo;

namespace License {
    public partial class LicenseDetail : Form {
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクター(INSERT)
        /// </summary>
        /// <param name="connectionVo"></param>
        public LicenseDetail(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            /*
             * InitializeControl
             */
            InitializeComponent();
        }

        /// <summary>
        /// コンストラクター(UPDATE)
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="staffCode"></param>
        public LicenseDetail(ConnectionVo connectionVo, int staffCode) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            /*
             * InitializeControl
             */
            InitializeComponent();
        }
    }
}
