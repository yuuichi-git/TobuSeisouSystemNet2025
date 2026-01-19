/*
 * 2025-1-9
 */
using System.Drawing.Printing;

using Dao;

using Vo;

namespace License {
    public partial class LicenseCard : Form {
        /*
         * Dao
         */
        private readonly LicenseMasterDao _licenseMasterDao;

        public LicenseCard(ConnectionVo connectionVo, int staffCode) {
            /*
             * Dao
             */
            _licenseMasterDao = new(connectionVo);
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
                "ToolStripMenuItemPrintB5",
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);

            this.PutPictureHead(_licenseMasterDao.SelectOnePictureHead(staffCode));
            this.PutPictureTail(_licenseMasterDao.SelectOnePictureTail(staffCode));

            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "Initialize Success";
            /*
             * Eventを登録する
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemPrintB5":
                    PrintDocument printDocument = new();
                    printDocument.PrintPage += PrintPage;
                    printDocument.Print();
                    break;
                case "ToolStripMenuItemExit":
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// PutPictureHead
        /// </summary>
        /// <param name="pictureHead"></param>
        private void PutPictureHead(byte[] pictureHead) {
            if (pictureHead.Length > 0) {
                ImageConverter imageConv = new();
                PictureBoxExHead.Image = (Image)imageConv.ConvertFrom(pictureHead); //写真表
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pictureTail"></param>
        private void PutPictureTail(byte[] pictureTail) {
            if (pictureTail.Length > 0) {
                ImageConverter imageConv = new();
                PictureBoxExTail.Image = (Image)imageConv.ConvertFrom(pictureTail); //写真裏
            }
        }

        private void PrintPage(object sender, PrintPageEventArgs e) {
            Bitmap bitmapHead = new(PictureBoxExHead.Width, PictureBoxExHead.Height);                                // 表のBitmapを作成
            Bitmap bitmapTail = new(PictureBoxExTail.Width, PictureBoxExTail.Height);                                // 裏のBitmapを作成
            this.PictureBoxExHead.DrawToBitmap(bitmapHead, new Rectangle(0, 0, bitmapHead.Width, bitmapHead.Height));
            this.PictureBoxExTail.DrawToBitmap(bitmapTail, new Rectangle(0, 0, bitmapTail.Width, bitmapTail.Height));

            e.Graphics.DrawImage(bitmapHead, 0, 0, 342, 222);                                                       // 表を描画
            e.Graphics.DrawImage(bitmapTail, 0, 250, 342, 222);                                                     // 裏を描画

            e.HasMorePages = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LicenseCard_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
