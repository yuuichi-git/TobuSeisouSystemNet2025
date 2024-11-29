/*
 * 2024-11-29
 */
namespace Common {
    public class StampUtility {

        public StampUtility() {

        }

        public Bitmap CreateStamp(int width, int height, string text) {
            Bitmap bitmap = new(width, height); // 描画先とするImageオブジェクトを作成する
            Graphics graphics = Graphics.FromImage(bitmap); // ImageオブジェクトのGraphicsオブジェクトを作成する

            graphics.DrawString("㊞", new("ＭＳ 明朝", 14), Brushes.Black, 24, 24);

            //リソースを解放する
            graphics.Dispose();



            return bitmap;
        }


    }
}
