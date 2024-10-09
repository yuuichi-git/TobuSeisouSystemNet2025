/*
 * 2024/10/03
 */
namespace VehicleDispatch {
    public partial class VehicleDispatchBoard : Form {
        /// <summary>
        /// コンストラクター
        /// </summary>
        public VehicleDispatchBoard() {
            /*
             * InitializeControl
             */
            InitializeComponent();
            DateTimePickerExOperationDate.Value = DateTime.Now;
        }


    }
}
