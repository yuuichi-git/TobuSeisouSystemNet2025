/*
 * 2024-11-05
 */
namespace CcControl {
    public partial class CcGroupBox : GroupBox {
        public CcGroupBox() {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }

        /// <summary>
        /// GroupBox内のCheckBoxのTagに格納されている値を配列に格納する
        /// </summary>
        /// <param name="ccGroupBox"></param>
        /// <returns></returns>
        public List<int> CreateArray(CcGroupBox ccGroupBox) {
            List<int> list = new();
            foreach (CcCheckBox checkBoxEx in ccGroupBox.Controls) {
                if (checkBoxEx.Checked)
                    list.Add(Convert.ToInt32(checkBoxEx.Tag));
            }
            return list;
        }
    }
}
