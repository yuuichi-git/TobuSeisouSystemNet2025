/*
 * 2025-02-18
 */
using System.Drawing.Printing;

using Dao;

using Vo;

namespace Car {
    public partial class CarVehicleInspectionView : Form {
        private Image _image;
        private string _name;
        /*
         * Dao
         */
        private CarMasterDao _carMasterDao;

        /// <summary>
        /// コンストラクター(CarDetailからの呼び出し)
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="image"></param>
        /// <param name="name">PictureBoxExMainPicture or PictureBoxExSubPicture</param>
        public CarVehicleInspectionView(ConnectionVo connectionVo, Image image, string name) {
            _image = image;
            _name = name;
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
            this.PictureBoxEx1.Image = image;
        }

        /// <summary>
        /// コンストラクター(VehicleDispatchBoardからの呼び出し)
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="carCode"></param>
        public CarVehicleInspectionView(ConnectionVo connectionVo, int carCode) {
            /*
             * Dao
             */
            _carMasterDao = new(connectionVo);
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
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;
            /*
             * Eventを登録する
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;

            byte[] subPicture = _carMasterDao.SelectOneSubPicture(carCode);
            if (subPicture.Length != 0) {
                ImageConverter imageConverter = new();
                this.PictureBoxEx1.Image = (Image)imageConverter.ConvertFrom(subPicture);                   // 写真
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemExit":
                    this.Close();
                    break;
                case "ToolStripMenuItemPrintA4":                                                                                                            // アプロケーションを終了する
                    this.ToolStripMenuItemPrintA4_Click();
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
            switch (_name) {
                case "PictureBoxExMainPicture":
                    if (this.PictureBoxEx1.Image is not null) {
                        // 新型車検証のサイズ(１０５＊１７７.８)
                        Rectangle rectangle = new(0, 0, 177 * 4, 105 * 4);
                        e.Graphics.DrawImage(this.PictureBoxEx1.Image, rectangle);
                    }
                    e.HasMorePages = false;
                    break;
                case "PictureBoxExSubPicture":
                    if (this.PictureBoxEx1.Image is not null) {
                        Rectangle rectangle = new(e.PageBounds.X, e.PageBounds.Y, e.PageBounds.Width, e.PageBounds.Height);
                        e.Graphics.DrawImage(this.PictureBoxEx1.Image, rectangle);
                    }
                    e.HasMorePages = false;
                    break;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarVehicleInspectionView_FormClosing(object sender, FormClosingEventArgs e) {
            this.Dispose();
        }
    }
}
