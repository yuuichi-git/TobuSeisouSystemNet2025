/*
 * 2024-10-01
 */
using Vo;

namespace ControlEx {
    public partial class ComboBoxEx : ComboBox {
        /// <summary>
        /// コンストラクター
        /// </summary>
        public ComboBoxEx() {
            /*
             * InitializeControl
             */
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Back:                                                                                             // BackSpaceキー
                    if (this.Text.Length > 0)
                        this.Text = this.Text.Substring(0, this.Text.Length);
                    break;
                case Keys.Delete:                                                                                           // Deleteキー
                    this.DisplayClear();
                    break;
                case Keys.Enter:                                                                                            // Enterキー
                    SendKeys.Send("{TAB}");
                    break;
                default:
                    base.OnKeyDown(e);                                                                                      // その他のキーは通常の動作を行う
                    break;
            }
        }

        /// <summary>
        /// 画面表示をクリア
        /// </summary>
        public void DisplayClear() {
            this.SelectedIndex = -1;
            this.Text = string.Empty;
        }
    }
}
