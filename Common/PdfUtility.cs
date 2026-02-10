/*
 * 2026-02-05
 */
using Windows.Data.Pdf;
using Windows.Storage.Streams;

namespace Common {
    public class PdfUtility {
        /// <summary>
        /// 単一の PDF ページを Bitmap に変換する
        /// </summary>
        /// <param name="contextMenuStrip"></param>
        /// <returns></returns>
        public async Task<Bitmap?> ConvertPdfToImage(ContextMenuStrip contextMenuStrip) {
            contextMenuStrip.Hide();                                                                                    // コンテキストメニューを閉じる

            using OpenFileDialog openFileDialog = new();
            openFileDialog.Title = "ファイルを選択してください";
            openFileDialog.Filter = "PDF ファイル (*.pdf)|*.pdf";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            DialogResult result = openFileDialog.ShowDialog();
            if (result != DialogResult.OK || result == DialogResult.None) {
                return null;
            }

            try {
                using FileStream fileStream = File.OpenRead(openFileDialog.FileName);
                IRandomAccessStream iRandomAccessStream = fileStream.AsRandomAccessStream();
                PdfDocument pdfDocument = await PdfDocument.LoadFromStreamAsync(iRandomAccessStream);

                using PdfPage pdfPage = pdfDocument.GetPage(0);

                /*
                 * A4:210mm × 297mm 
                 * 300dpi → 300 / 25.4 = 11.811 px / mm
                 * 210mm × 11.811 = 2480px
                 * 297mm × 11.811 = 3508px
                 */
                PdfPageRenderOptions pdfPageRenderOptions = new();
                pdfPageRenderOptions.DestinationWidth = 2480;
                pdfPageRenderOptions.DestinationHeight = 3508;

                using InMemoryRandomAccessStream inMemoryRandomAccessStream = new();
                await pdfPage.RenderToStreamAsync(inMemoryRandomAccessStream, pdfPageRenderOptions);
                inMemoryRandomAccessStream.Seek(0);

                /*
                 * ストリームを完全コピーして安全な Bitmap を作る
                 */
                using MemoryStream memoryStream = new();
                inMemoryRandomAccessStream.AsStream().CopyTo(memoryStream);
                memoryStream.Position = 0;

                Bitmap bitmap = new(memoryStream);                                                                      // 呼び出し側で Dispose
                return bitmap;
            } catch (Exception exception) {
                Console.WriteLine(exception);                                                                           // ログなどに残す
                return null;
            }
        }
    }
}


