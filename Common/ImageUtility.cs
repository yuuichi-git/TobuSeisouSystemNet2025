/*
 * 2024-10-12
 */
namespace Common {
    public class ImageUtility {
        /// <summary>
        /// バイト配列をImageオブジェクトに変換
        /// </summary>
        /// <param name="arrayByte"></param>
        /// <returns></returns>
        public static Image ByteArrayToImage(byte[] arrayByte) {
            ImageConverter imgconv = new ImageConverter();
            object? obj = imgconv.ConvertFrom(arrayByte);
            if (obj is Image img) {
                return img;
            }
            throw new InvalidOperationException("バイト配列から画像への変換に失敗しました。");
        }

        /// <summary>
        /// Imageオブジェクトをバイト配列に変換
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static byte[] ImageToByteArray(Image image) {
            ImageConverter imgconv = new ImageConverter();
            object? obj = imgconv.ConvertTo(image, typeof(byte[]));
            if (obj is byte[] b) {
                return b;
            }
            throw new InvalidOperationException("画像からバイト配列への変換に失敗しました。");
        }
    }
}
