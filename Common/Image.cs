/*
 * 2024-10-12
 */
namespace Common {
    public class Image {
        /// <summary>
        /// バイト配列をImageオブジェクトに変換
        /// </summary>
        /// <param name="arrayByte"></param>
        /// <returns></returns>
        public static Image ByteArrayToImage(byte[] arrayByte) {
            ImageConverter imgconv = new ImageConverter();
            Image img = (Image)imgconv.ConvertFrom(arrayByte);
            return img;
        }

        /// <summary>
        /// Imageオブジェクトをバイト配列に変換
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static byte[] ImageToByteArray(Image image) {
            ImageConverter imgconv = new ImageConverter();
            byte[] b = (byte[])imgconv.ConvertTo(image, typeof(byte[]));
            return b;
        }
    }
}
