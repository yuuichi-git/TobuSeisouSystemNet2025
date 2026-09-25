/*
 * 2026-09-22
 */
using System.Drawing.Printing;

namespace CcControl {
    public partial class CcComboBoxPrinters : ComboBox {
        /// <summary>
        /// コンストラクター
        /// </summary>
        public CcComboBoxPrinters() {
            int i = 0;
            this.Items.Clear();
            List<ComboBoxItems> listComboBoxItems = new();
            foreach(string printer in GetAllPrinterName()) {
                listComboBoxItems.Add(new ComboBoxItems(i, printer));
                i++;
            }

            this.DataSource = listComboBoxItems;
            this.ValueMember = "Key";
            this.DisplayMember = "DisplayName";

            /*
             * バインド完了を保証
             * バインド直後は内部処理が完了していないため強制的にバインドを完了させる
             */
            this.BindingContext = new BindingContext();
        }

        /// <summary>
        /// インストールされている全てのプリンター名を取得
        /// </summary>
        public List<string> GetAllPrinterName() {
            List<string> listPrinterName = new();
            foreach(string printerName in PrinterSettings.InstalledPrinters)
                listPrinterName.Add(printerName);
            return listPrinterName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);

            // ★ 既定プリンターを選択する
            int i = 0;
            string defaultPrinter = new PrinterSettings().PrinterName;
            foreach(ComboBoxItems item in (List<ComboBoxItems>)this.DataSource) {
                if(item.DisplayName == "FUJIFILM Apeos C5571") {
                    this.SelectedIndex = i;
                    break;
                }
                i++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }

        /// <summary>
        /// 内部クラス
        /// </summary>
        private class ComboBoxItems {
            int _key;
            string _displayName;
            public ComboBoxItems(int key, string displayName) {
                _key = key;
                _displayName = displayName;
            }
            /// <summary>
            /// 0から始まるListの順番
            /// </summary>
            public int Key {
                get => _key;
                set => _key = value;
            }
            /// <summary>
            /// プリンター名
            /// </summary>
            public string DisplayName {
                get => this._displayName;
                set => this._displayName = value;
            }
        }
    }
}
