/*
 * 2026-02-05
 */
using System.Diagnostics;

using PdfSharpCore.Drawing;

using Windows.Data.Pdf;
using Windows.Storage.Streams;

namespace Common {
    public class PdfUtility {

        /// <summary>
        /// 単一の PDF ページを Bitmap に変換する（Windows.Data.Pdf 使用）
        /// </summary>
        public async Task<Bitmap?> ConvertPdfToImage(ContextMenuStrip contextMenuStrip) {
            contextMenuStrip.Hide();

            using OpenFileDialog openFileDialog = new();
            openFileDialog.Title = "ファイルを選択してください";
            openFileDialog.Filter = "PDF ファイル (*.pdf)|*.pdf";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            DialogResult result = openFileDialog.ShowDialog();
            if (result != DialogResult.OK)
                return null;

            try {
                using FileStream fileStream = File.OpenRead(openFileDialog.FileName);
                IRandomAccessStream iRandomAccessStream = fileStream.AsRandomAccessStream();
                PdfDocument pdfDocument = await PdfDocument.LoadFromStreamAsync(iRandomAccessStream);

                using PdfPage pdfPage = pdfDocument.GetPage(0);

                PdfPageRenderOptions options = new();
                options.DestinationWidth = 2480;
                options.DestinationHeight = 3508;

                using InMemoryRandomAccessStream renderStream = new();
                await pdfPage.RenderToStreamAsync(renderStream, options);
                renderStream.Seek(0);

                using MemoryStream ms = new();
                renderStream.AsStream().CopyTo(ms);
                ms.Position = 0;

                Bitmap bitmap = new(ms);
                return bitmap;
            } catch (Exception ex) {
                Debug.WriteLine("ConvertPdfToImage:" + ex);
                return null;
            }
        }

        /// <summary>
        /// PDF を byte[] に変換する（File.ReadAllBytes）
        /// </summary>
        public byte[]? ConvertPdfToByte(ContextMenuStrip contextMenuStrip) {
            contextMenuStrip.Hide();

            using OpenFileDialog openFileDialog = new();
            openFileDialog.Title = "PDFファイルを選択してください";
            openFileDialog.Filter = "PDF ファイル (*.pdf)|*.pdf";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            IWin32Window owner = contextMenuStrip.SourceControl?.FindForm() ?? null;
            DialogResult result = owner != null ? openFileDialog.ShowDialog(owner) : openFileDialog.ShowDialog();

            if (result != DialogResult.OK)
                return null;

            try {
                return File.ReadAllBytes(openFileDialog.FileName);
            } catch (Exception ex) {
                Debug.WriteLine("ConvertPdfToByte:" + ex);
                return null;
            }
        }

        /// <summary>
        /// Bitmap を PDF に変換して byte[] として返す（PdfSharpCore 使用）
        /// </summary>
        public byte[] ConvertImageToPdfBytes(Bitmap bitmap) {
            using (MemoryStream pdfStream = new()) {

                PdfSharpCore.Pdf.PdfDocument pdfDocument = new();
                PdfSharpCore.Pdf.PdfPage page = pdfDocument.AddPage();
                page.Width = bitmap.Width;
                page.Height = bitmap.Height;

                PdfSharpCore.Drawing.XGraphics xGraphics = PdfSharpCore.Drawing.XGraphics.FromPdfPage(page);

                using (MemoryStream imgStream = new()) {
                    bitmap.Save(imgStream, System.Drawing.Imaging.ImageFormat.Png);
                    imgStream.Position = 0;

                    PdfSharpCore.Drawing.XImage xImage = PdfSharpCore.Drawing.XImage.FromStream(() => new MemoryStream(imgStream.ToArray()));
                    xGraphics.DrawImage(xImage, 0, 0, bitmap.Width, bitmap.Height);
                }

                pdfDocument.Save(pdfStream, false);
                return pdfStream.ToArray();
            }
        }
    }
}
