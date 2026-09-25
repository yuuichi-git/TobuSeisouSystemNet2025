/*
 * 2026-09-22
 */
namespace CcControl {
    public partial class CcComboBoxMonitors : ComboBox {
        /// <summary>
        /// コンストラクター
        /// </summary>
        public CcComboBoxMonitors() {
            /*
             * CcComboBoxMonitors
             */
            this.Items.Clear();
            List<ComboBoxItems> listComboBoxItems = new();
            foreach(Screen screen in GetAllScreen()) {
                listComboBoxItems.Add(new ComboBoxItems(string.Concat(screen.DeviceName, "　{ ", screen.Bounds.Width, "×", screen.Bounds.Height, " }"), screen));
            }
            // ComboBoxにデータをバインド
            this.DataSource = listComboBoxItems;
            this.DisplayMember = "DisplayName";                                           // 表示名を設定
            this.ValueMember = "Screen";                                                  // 値を設定
        }

        public List<Screen> GetAllScreen() {
            List<Screen> listScreen = new();
            foreach(Screen screen in Screen.AllScreens)
                listScreen.Add(screen);
            return listScreen;
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }

        /// <summary>
        /// 内部クラス
        /// </summary>
        private class ComboBoxItems {
            string _displayName;
            Screen _screen;
            public ComboBoxItems(string displayName, Screen screen) {
                _displayName = displayName;
                _screen = screen;
            }
            public string DisplayName {
                get => this._displayName;
                set => this._displayName = value;
            }
            public Screen Screen {
                get => this._screen;
                set => this._screen = value;
            }
        }
    }
}
