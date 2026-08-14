/*
 * 2026-02-05
 */
using System.Diagnostics;

using Windows.Data.Pdf;
using Windows.Storage.Streams;

namespace Common {
    public class PdfUtility {
        /// <summary>
        /// 単一の PDF ページを Bitmap に変換する（Windows.Data.Pdf 使用）
        /// </summary>
        /// <param name="contextMenuStrip"></param>
        /// <returns></returns>
        public async Task<Bitmap> ConvertPdfToImage(ContextMenuStrip contextMenuStrip) {
            contextMenuStrip.Hide();

            using OpenFileDialog openFileDialog = new();
            openFileDialog.Title = "ファイルを選択してください";
            openFileDialog.Filter = "PDF ファイル (*.pdf)|*.pdf";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            DialogResult result = openFileDialog.ShowDialog();
            if(result != DialogResult.OK)
                return null;

            try {
                using FileStream fileStream = File.OpenRead(openFileDialog.FileName);
                IRandomAccessStream iRandomAccessStream = fileStream.AsRandomAccessStream();
                PdfDocument pdfDocument = await PdfDocument.LoadFromStreamAsync(iRandomAccessStream);

                using PdfPage pdfPage = pdfDocument.GetPage(0);

                PdfPageRenderOptions pdfPageRenderOptions = new();
                pdfPageRenderOptions.DestinationWidth = 2480;
                pdfPageRenderOptions.DestinationHeight = 3508;

                using InMemoryRandomAccessStream renderStream = new();
                await pdfPage.RenderToStreamAsync(renderStream, pdfPageRenderOptions);
                renderStream.Seek(0);

                using MemoryStream memoryStream = new();
                renderStream.AsStream().CopyTo(memoryStream);
                memoryStream.Position = 0;

                Bitmap bitmap = new(memoryStream);
                return bitmap;
            } catch(Exception ex) {
                Debug.WriteLine("ConvertPdfToImage:" + ex);
                return null;
            }
        }

        /// <summary>
        /// OpenFileDialogで選択された PDF ファイルを byte[] として返す
        /// </summary>
        /// <param name="ownerForm"></param>
        /// <returns></returns>
        public byte[] ConvertPdfToBytes(Form ownerForm) {
            using OpenFileDialog openFileDialog = new();
            openFileDialog.Title = "PDFファイルを選択してください";
            openFileDialog.Filter = "PDF ファイル (*.pdf)|*.pdf";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            DialogResult result = openFileDialog.ShowDialog(ownerForm);

            if(result != DialogResult.OK)
                return null;

            try {
                return File.ReadAllBytes(openFileDialog.FileName);
            } catch(Exception ex) {
                Debug.WriteLine("ConvertPdfToByte:" + ex);
                return null;
            }
        }

        /// <summary>
        /// OpenFileDialogで選択された PDF ファイルを byte[] として返す
        /// </summary>
        /// <param name="contextMenuStrip"></param>
        /// <returns></returns>
        public byte[] ConvertPdfToBytes(ContextMenuStrip contextMenuStrip) {
            contextMenuStrip.Hide();

            using OpenFileDialog openFileDialog = new();
            openFileDialog.Title = "PDFファイルを選択してください";
            openFileDialog.Filter = "PDF ファイル (*.pdf)|*.pdf";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            IWin32Window owner = contextMenuStrip.SourceControl?.FindForm() ?? null;
            DialogResult result = owner != null ? openFileDialog.ShowDialog(owner) : openFileDialog.ShowDialog();

            if(result != DialogResult.OK)
                return null;

            try {
                return File.ReadAllBytes(openFileDialog.FileName);
            } catch(Exception ex) {
                Debug.WriteLine("ConvertPdfToByte:" + ex);
                return null;
            }
        }


        /// <summary>
        /// Bitmap を PDF に埋め込み、PDF の byte[] を返す。
        /// 画像の DPI を考慮して PDF ページサイズを「物理サイズ（ポイント）」に変換し、
        /// PdfiumViewer で正しく表示できる PDF を生成する。
        /// </summary>
        /// <param name="bitmap">PDF に埋め込む Bitmap</param>
        /// <returns>PDF データの byte[]</returns>
        public byte[] ConvertImageToPdfBytes(Bitmap bitmap) {
            // PDF を書き込むためのメモリストリーム
            using(MemoryStream pdfStream = new()) {
                /*
                 * Bitmap の DPI（解像度）を取得する。
                 * PDF の座標系は「ポイント（pt）」で、1pt = 1/72 inch。
                 * 画像のピクセル数をそのまま PDF に使うと巨大ページになり、
                 * PdfiumViewer が正しく表示できない場合があるため、
                 * DPI を使って「物理サイズ（インチ）」→「ポイント」に変換する。
                 */
                float dpiX = bitmap.HorizontalResolution;
                float dpiY = bitmap.VerticalResolution;

                // ピクセル → ポイント（pt）へ変換
                double widthPt  = bitmap.Width  / dpiX * 72.0;
                double heightPt = bitmap.Height / dpiY * 72.0;

                /*
                 * PdfSharpCore の PDF ドキュメントを作成し、
                 * ページサイズを画像の物理サイズに合わせて設定する。
                 */
                PdfSharpCore.Pdf.PdfDocument pdfDocument = new();
                PdfSharpCore.Pdf.PdfPage pdfPage = pdfDocument.AddPage();
                pdfPage.Width = widthPt;
                pdfPage.Height = heightPt;

                // PDF 描画用の XGraphics を取得
                PdfSharpCore.Drawing.XGraphics xGraphics = PdfSharpCore.Drawing.XGraphics.FromPdfPage(pdfPage);

                /*
                 * PdfSharpCore は System.Drawing.Bitmap を直接扱えないため、
                 * 一度 PNG として MemoryStream に保存し、それを XImage として読み込む。
                 */
                using(MemoryStream imgStream = new()) {
                    // Bitmap → PNG 形式で MemoryStream に保存
                    bitmap.Save(imgStream, System.Drawing.Imaging.ImageFormat.Png);
                    imgStream.Position = 0; // 読み込み位置を先頭に戻す

                    /*
                     * XImage.FromStream は「ストリームを返すデリゲート」を要求するため、
                     * imgStream の内容を新しい MemoryStream にコピーして渡す。
                     * PdfSharpCore が内部でストリームをクローズする可能性があるため、
                     * 元の imgStream を直接渡すのは避ける。
                     */
                    PdfSharpCore.Drawing.XImage xImage = PdfSharpCore.Drawing.XImage.FromStream(() => new MemoryStream(imgStream.ToArray()));
                    /*
                     * PDF ページに画像を描画する。
                     * PDF の座標系はポイントなので、描画サイズもポイントで指定する。
                     */
                    xGraphics.DrawImage(xImage, 0, 0, widthPt, heightPt);
                }
                /*
                 * PDF を MemoryStream に保存。
                 * 第二引数 false は「ストリームを閉じない」設定。
                 * （pdfStream は using により呼び出し元で破棄されるため）
                 */
                pdfDocument.Save(pdfStream, false);

                // PDF の byte[] を返す
                return pdfStream.ToArray();
            }
        }
    }
}
