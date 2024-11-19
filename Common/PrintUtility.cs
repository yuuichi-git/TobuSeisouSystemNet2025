/*
 * 2024-11-19
 */
using System.Drawing.Printing;

using ControlEx;

namespace Common {
    public class PrintUtility {

        public PrintUtility() {

        }

        /// <summary>
        /// インストールされている全てのプリンターを取得する
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllPrinterName() {
            List<string> listPrinterName = new();
            foreach (string printerName in PrinterSettings.InstalledPrinters)
                listPrinterName.Add(printerName);
            return listPrinterName;
        }

        /// <summary>
        /// デフォルトプリンタを取得する
        /// </summary>
        /// <returns></returns>
        public string GetDefaultPrinter() {
            PrintDocument printDocument = new();
            return printDocument.DefaultPageSettings.PrinterSettings.PrinterName;
        }

        /// <summary>
        /// ComboBoxExにインストールされているプリンターをセットする
        /// </summary>
        /// <param name="comboBoxEx"></param>
        /// <returns></returns>
        public ComboBoxEx SetAllPrinterForComboBoxEx(ComboBoxEx comboBoxEx) {
            foreach (string printerName in PrinterSettings.InstalledPrinters)
                comboBoxEx.Items.Add(printerName);
            comboBoxEx.Text = new PrintDocument().DefaultPageSettings.PrinterSettings.PrinterName;
            return comboBoxEx;
        }
    }
}
