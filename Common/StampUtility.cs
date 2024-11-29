/*
 * 2024-11-29
 */
using System.Drawing.Drawing2D;

namespace Common {
    public class StampUtility {

        public StampUtility() {

        }

        public Bitmap CreateStamp(byte[] picture) {
            int stampWidth = 46;
            int stampHeight = 46;

            Bitmap bitmap = new(stampWidth, stampHeight); // 描画先とするImageオブジェクトを作成する
            Graphics graphics = Graphics.FromImage(bitmap); // ImageオブジェクトのGraphicsオブジェクトを作成する
            Image? image = picture.Length != 0 ? (Image?)new ImageConverter().ConvertFrom(picture) : null;
            if (image is not null) {
                graphics.DrawString("㊞", new("ＭＳ 明朝", 14), Brushes.Black, image.Width / 2 - 7, image.Height / 2 - 7);
                graphics.DrawImage(image, 0, 0, image.Width, image.Height);

                //補間方法として高品質双三次補間を指定する
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                //画像を縮小して描画する
                graphics.DrawImage(image, 0, 0, stampWidth, stampHeight);
            }
            //リソースを解放する
            graphics.Dispose();
            return bitmap;
        }
    }
}
