/*
 * 2026-01-28
 */
using System.Text.RegularExpressions;

using Vo;

namespace ControlEx {
    public partial class CcComboBoxWordMaster : ComboBox {
        /*
         * Vo
         */
        private readonly List<CcComboBoxWordMasterVo> _listCcComboBoxWordMasterVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public CcComboBoxWordMaster() {
            /*
             * Vo初期化
             */
            _listCcComboBoxWordMasterVo = new();
            /*
             * InitializeControl
             */
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listWordMasterVo"></param>
        public void SetItems(List<WordMasterVo> listWordMasterVo) {
            this.AutoCompleteMode = AutoCompleteMode.SuggestAppend;                                                                 // オートコンプリートモードを設定
            this.AutoCompleteSource = AutoCompleteSource.ListItems;                                                                 // オートコンプリートソースを設定
            this.DisplayMember = "WordName";                                                                                        // 表示するプロパティ名
            this.ValueMember = "WordMasterVo";                                                                                      // 値となるプロパティ名
            
            foreach (WordMasterVo wordMasterVo in listWordMasterVo) {
                CcComboBoxWordMasterVo ccComboBoxWordMasterVo = new(wordMasterVo.Code, wordMasterVo.Name, wordMasterVo);
                _listCcComboBoxWordMasterVo.Add(ccComboBoxWordMasterVo);
            }
            this.DataSource = _listCcComboBoxWordMasterVo;                                                                           // フィルタリング（必要に応じて条件を変更）
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
                    this.DataSource = _listCcComboBoxWordMasterVo.Where(x => x.WordName.Contains(this.Text.Trim())).ToList();       // フィルタリング（必要に応じて条件を変更）
                } else {

                }
            } else {
                this.DataSource = _listCcComboBoxWordMasterVo;
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
        public class CcComboBoxWordMasterVo {
            private int _wordCode;
            private string _wordName;
            private WordMasterVo _wordMasterVo;
            /// <summary>
            /// コンストラクター
            /// </summary>
            /// <param name="wordCode"></param>
            /// <param name="wordName"></param>
            /// <param name="wordMasterVo"></param>
            public CcComboBoxWordMasterVo(int wordCode, string wordName, WordMasterVo wordMasterVo) {
                _wordCode = wordCode;
                _wordName = wordName;
                _wordMasterVo = wordMasterVo;
            }
            /// <summary>
            /// 区コード
            /// </summary>
            public int WordCode {
                get => this._wordCode;
                set => this._wordCode = value;
            }
            /// <summary>
            /// 区名
            /// </summary>
            public string WordName {
                get => this._wordName;
                set => this._wordName = value;
            }
            /// <summary>
            /// WordMasterVo
            /// </summary>
            public WordMasterVo WordMasterVo {
                get => this._wordMasterVo;
                set => this._wordMasterVo = value;
            }
        }
    }
}
