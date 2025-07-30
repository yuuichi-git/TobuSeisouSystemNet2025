/*
 * 2024-04-25
 */
namespace Vo {
    public class CollectionStaffsTaitouVo {
        private int _operationWeekDay;
        private int _staffCode3;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public CollectionStaffsTaitouVo() {
            _operationWeekDay = 0;
            _staffCode3 = 0;
        }
        /// <summary>
        /// 配車曜日コード
        /// </summary>
        public int OperationWeekDay {
            get => _operationWeekDay;
            set => _operationWeekDay = value;
        }
        /// <summary>
        /// ３人目
        /// </summary>
        public int StaffCode3 {
            get => _staffCode3;
            set => _staffCode3 = value;
        }
    }
}
