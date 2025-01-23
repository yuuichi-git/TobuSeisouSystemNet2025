/*
 * 2024-03-21
 */
namespace Vo {
    public class CollectionWeightChiyodaVo {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);

        private DateTime _operationDate;
        private int _staffCode1;
        private string _staffDisplayName1;
        private int _staffDisplayName2;
        private string _staffName2;
        private int _staffDisplayName3;
        private string _staffName3;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public CollectionWeightChiyodaVo() {
            _operationDate = _defaultDateTime;
            _staffCode1 = 0;
            _staffDisplayName1 = string.Empty;
            _staffDisplayName2 = 0;
            _staffName2 = string.Empty;
            _staffDisplayName3 = 0;
            _staffName3 = string.Empty;
        }

        public DateTime OperationDate {
            get => _operationDate;
            set => _operationDate = value;
        }
        public int StaffCode1 {
            get => _staffCode1;
            set => _staffCode1 = value;
        }
        public string StaffDisplayName1 {
            get => _staffDisplayName1;
            set => _staffDisplayName1 = value;
        }
        public int StaffCode2 {
            get => _staffDisplayName2;
            set => _staffDisplayName2 = value;
        }
        public string StaffDisplayName2 {
            get => _staffName2;
            set => _staffName2 = value;
        }
        public int StaffCode3 {
            get => _staffDisplayName3;
            set => _staffDisplayName3 = value;
        }
        public string StaffDisplayName3 {
            get => _staffName3;
            set => _staffName3 = value;
        }
    }
}
