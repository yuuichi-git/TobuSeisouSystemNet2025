/*
 * 2026-08-28
 */
using Vo;

namespace RiskAssessment {
    public partial class RiskAssessmentList : Form {
        /*
         * Columns
         */
        private const int _colBelongsName = 0;
        private const int _colJobFormName = 1;
        private const int _colOccupation = 2;
        private const int _colName = 3;
        private const int _colEmploymentDate = 4;
        private const int _colStudentsFlag01 = 5;
        private const int _colStudentsFlag02 = 6;
        private const int _colStudentsFlag03 = 7;
        /*
         * Dao
         */

        /*
         * Vo
         */
        private ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public RiskAssessmentList(ConnectionVo connectionVo, Screen screen) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            /*
             * Initialize
             */
            InitializeComponent();

        }
    }
}
