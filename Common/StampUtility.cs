/*
 * 2024-11-29
 */
namespace Common {
    public class StampUtility {

        public StampUtility() {

        }

        public Bitmap CreateStamp(byte[] imageStamp) {
            Bitmap bitmap = new(46, 46); // 描画先とするImageオブジェクトを作成する
            Graphics graphics = Graphics.FromImage(bitmap); // ImageオブジェクトのGraphicsオブジェクトを作成する

            Image _image = imageStamp.Length != 0 ? (Image?)new ImageConverter().ConvertFrom(imageStamp) : null;

            graphics.DrawString("㊞", new("ＭＳ 明朝", 14), Brushes.Black, 24, 24);

            //リソースを解放する
            graphics.Dispose();



            return bitmap;
        }


    }
}
