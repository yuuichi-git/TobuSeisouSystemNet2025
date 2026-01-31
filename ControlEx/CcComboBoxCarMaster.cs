/*
 * 2025-11-10
 */
using System.Text.RegularExpressions;

using Vo;

namespace ControlEx {
    public partial class CcComboBoxCarMaster : ComboBox {
        /*
         * Vo
         */
        private readonly List<ComboBoxExCarMasterVo> _listComboBoxExCarMasterVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public CcComboBoxCarMaster() {
            /*
             * Vo初期化
             */
            _listComboBoxExCarMasterVo = new();
            /*
             * InitializeControl
             */
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listCarMasterVo"></param>
        /// <param name="doorFlag">true:Door番号を表示する false:Door番号を表示しない</param>
        public void SetItems(List<CarMasterVo> listCarMasterVo, bool doorFlag) {
            this.AutoCompleteMode = AutoCompleteMode.SuggestAppend;                                                                 // オートコンプリートモードを設定
            this.AutoCompleteSource = AutoCompleteSource.ListItems;                                                                 // オートコンプリートソースを設定
            this.DisplayMember = "RegistrationNumber";                                                                              // 表示するプロパティ名
            this.ValueMember = "CarMasterVo";                                                                                       // 値となるプロパティ名

            foreach (CarMasterVo carMasterVo in listCarMasterVo) {
                ComboBoxExCarMasterVo comboBoxExCarMasterVo = null;
                switch (doorFlag) {
                    case true:
                        comboBoxExCarMasterVo = new(string.Concat(carMasterVo.RegistrationNumber, " 【", carMasterVo.DoorNumber, "】"), string.Concat(carMasterVo.RegistrationNumber4, carMasterVo.DoorNumber), carMasterVo);
                        break;
                    case false:
                        comboBoxExCarMasterVo = new(carMasterVo.RegistrationNumber, carMasterVo.RegistrationNumber4, carMasterVo);
                        break;
                }
                _listComboBoxExCarMasterVo.Add(comboBoxExCarMasterVo);
            }
            this.DataSource = _listComboBoxExCarMasterVo;                                                                           // フィルタリング（必要に応じて条件を変更）
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
        protected override void OnDropDown(EventArgs e) {
            if (this.Text.Length > 0) {
                if (Regex.IsMatch(this.Text.Trim(), @"^[0-9]+$")) {                                                                 // 数字のみ入力されている場合
                    this.DataSource = _listComboBoxExCarMasterVo.Where(x => x.SerchKey.Contains(this.Text.Trim())).ToList();        // フィルタリング（必要に応じて条件を変更）
                } else {

                }
            } else {
                this.DataSource = _listComboBoxExCarMasterVo;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e) {
            switch (e.KeyCode) {
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
        public class ComboBoxExCarMasterVo {
            private string _registrationNumber;
            private string _serchKey;
            private CarMasterVo _carMasterVo;
            /// <summary>
            /// コンストラクター
            /// </summary>
            /// <param name="registrationNumber"></param>
            /// <param name="registrationNumber4"></param>
            /// <param name="carMasterVo"></param>
            public ComboBoxExCarMasterVo(string registrationNumber, string registrationNumber4, CarMasterVo carMasterVo) {
                _registrationNumber = registrationNumber;
                _serchKey = registrationNumber4;
                _carMasterVo = carMasterVo;
            }
            /// <summary>
            /// 車両登録番号
            /// </summary>
            public string RegistrationNumber {
                get => _registrationNumber;
                set => _registrationNumber = value;
            }
            /// <summary>
            /// 検索キー
            /// </summary>
            public string SerchKey {
                get => this._serchKey;
                set => this._serchKey = value;
            }
            /// <summary>
            /// Voを退避
            /// </summary>
            public CarMasterVo CarMasterVo {
                get => _carMasterVo;
                set => _carMasterVo = value;
            }
        }
    }
}
