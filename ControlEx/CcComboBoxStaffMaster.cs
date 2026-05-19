/*
 * 2026-05-06
 */
using Vo;

namespace CcControl {
    public partial class CcComboBoxStaffMaster: ComboBox {
        /*
         * Vo
         */
        private readonly List<CcComboBoxStaffMasterVo> _listCcComboBoxStaffMasterVo;

        public CcComboBoxStaffMaster() {
            /*
             * Vo
             */
            _listCcComboBoxStaffMasterVo = new();
            /*
             * Initialize
             */
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listStaffMasterVo"></param>
        public void SetItems(List<StaffMasterVo> listStaffMasterVo) {
            this.AutoCompleteMode = AutoCompleteMode.SuggestAppend;                                                                 // オートコンプリートモードを設定
            this.AutoCompleteSource = AutoCompleteSource.ListItems;                                                                 // オートコンプリートソースを設定
            this.DisplayMember = "DisplayName";                                                                                     // 表示するプロパティ名
            this.ValueMember = "StaffMasterVo";                                                                                     // 値となるプロパティ名

            foreach(StaffMasterVo staffMasterVo in listStaffMasterVo) {
                CcComboBoxStaffMasterVo ccComboBoxStaffMasterVo = null;
                ccComboBoxStaffMasterVo = new(staffMasterVo.DisplayName, staffMasterVo);
                _listCcComboBoxStaffMasterVo.Add(ccComboBoxStaffMasterVo);
            }
            this.DataSource = _listCcComboBoxStaffMasterVo;                                                                           // フィルタリング（必要に応じて条件を変更）
        }

        /// <summary>
        /// 画面表示をクリア
        /// </summary>
        public void DisplayClear() {
            this.SelectedIndex = -1;
            this.Text = string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e) {
            switch(e.KeyCode) {
                case Keys.Enter:                                                                                                    // Enterキー
                    SendKeys.Send("{TAB}");
                    break;
                default:
                    break;
            }
        }

        /*
         * インナークラス
         */
        public class CcComboBoxStaffMasterVo {
            private string _displayName;
            private StaffMasterVo _staffMasterVo;

            /// <summary>
            /// コンストラクター
            /// </summary>
            /// <param name="displayName"></param>
            /// <param name="staffMasterVo"></param>
            public CcComboBoxStaffMasterVo(string displayName, StaffMasterVo staffMasterVo) {
                _displayName = displayName;
                _staffMasterVo = staffMasterVo;
            }
            /// <summary>
            /// 氏名
            /// </summary>
            public string DisplayName {
                get => _displayName;
                set => _displayName = value;
            }
            /// <summary>
            /// Voを退避
            /// </summary>
            public StaffMasterVo StaffMasterVo {
                get => _staffMasterVo;
                set => _staffMasterVo = value;
            }
        }

    }
}
