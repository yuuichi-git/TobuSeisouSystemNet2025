/*
 * 2026-02-02
 */
using System.Drawing.Printing;

using Vo;

namespace WastCollection {
    public partial class WastCollectionView : Form {
        public WastCollectionView(ConnectionVo connectionVo, Image image) {
            /*
             * InitializeControl
             */
            InitializeComponent();
            /*
             * MenuStrip
             */
            List<string> listString = new() {
                "ToolStripMenuItemFile",
                "ToolStripMenuItemExit",
                "ToolStripMenuItemPrint",
                "ToolStripMenuItemPrintA4",
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;
            /*
             * Eventを登録する
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
            this.CcPictureBox1.Image = image;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemPrintA4":                                                                                                            // アプロケーションを終了する
                    this.ToolStripMenuItemPrintA4_Click();
                    break;
                case "ToolStripMenuItemExit":
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// ToolStripMenuItemPrintA4_Click
        /// </summary>
        private void ToolStripMenuItemPrintA4_Click() {
            PrintDocument _printDocument = new();
            _printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);
            // 出力先プリンタを指定します。
            //printDocument.PrinterSettings.PrinterName = "(PrinterName)";
            // 印刷部数を指定します。
            _printDocument.PrinterSettings.Copies = 1;
            // 片面印刷に設定します。
            _printDocument.PrinterSettings.Duplex = Duplex.Simplex;
            // カラー印刷に設定します。
            _printDocument.PrinterSettings.DefaultPageSettings.Color = true;
            _printDocument.Print();
        }

        /// <summary>
        /// printDocument_PrintPage
        /// </summary>
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e) {
            if (this.CcPictureBox1.Image is not null) {
                Rectangle rectangle = new(e.PageBounds.X, e.PageBounds.Y, e.PageBounds.Width, e.PageBounds.Height);
                e.Graphics.DrawImage(this.CcPictureBox1.Image, rectangle);
            }
            e.HasMorePages = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WastCollectionPaper_FormClosing(object sender, FormClosingEventArgs e) {
            this.Dispose();
        }
    }
}
