/*
 * 2024-03-21
 */
namespace Vo {
    public class CollectionWeightGroupChiyodaVo {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);

        private DateTime _operationDate;
        private int _staffCode;
        private string _staffDisplayName;
        private string _occupation;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public CollectionWeightGroupChiyodaVo() {
            _operationDate = _defaultDateTime;
            _staffCode = 0;
            _staffDisplayName = string.Empty;
            _occupation = string.Empty;
        }

        public DateTime OperationDate {
            get => _operationDate;
            set => _operationDate = value;
        }
        public int StaffCode {
            get => _staffCode;
            set => _staffCode = value;
        }
        public string StaffDisplayName {
            get => _staffDisplayName;
            set => _staffDisplayName = value;
        }
        public string Occupation {
            get => _occupation;
            set => _occupation = value;
        }
    }
}
