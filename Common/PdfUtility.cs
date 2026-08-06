/*
 * 2026-02-05
 */
using System.Diagnostics;
using System.Text;

using Windows.Data.Pdf;
using Windows.Storage.Streams;

namespace Common {
    public class PdfUtility {

        /// <summary>
        /// byte[] が PDF 形式かどうか判定する
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns>true:PDF形式 false:それ以外</returns>
        public bool IsPdfFormat(byte[] bytes) {
            /*
             * PDF フォーマットかどうかを判定 "%PDF-"
             */
            if(bytes.Length < 5)
                return false;

            // PDF は必ず "%PDF-" で始まる
            string header = Encoding.ASCII.GetString(bytes, 0, 5);
            return header.StartsWith("%PDF-");
        }

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
            if(result != DialogResult.OK)
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
        public byte[]? ConvertPdfToByte(Form ownerForm) {
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
        public byte[]? ConvertPdfToByte(ContextMenuStrip contextMenuStrip) {
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
        /// Bitmap を PDF に埋め込み、PDF の byte[] を返す
        /// （1枚の画像をそのまま PDF ページとして保存する）
        /// </summary>
        /// <param name="bitmap">PDF に埋め込む Bitmap</param>
        /// <returns>PDF データの byte[]</returns>
        public byte[] ConvertImageToPdfBytes(Bitmap bitmap) {

            // PDF を書き込むためのメモリストリーム
            using(MemoryStream pdfStream = new()) {

                /*
                 * PdfSharpCore の PDF ドキュメントを作成
                 * AddPage() でページを追加し、画像サイズに合わせてページサイズを設定する。
                 * 画像を「そのままのピクセルサイズ」で PDF に貼り付けたい場合は、
                 * Width / Height を bitmap のサイズに合わせる必要がある。
                 */
                PdfSharpCore.Pdf.PdfDocument pdfDocument = new();
                PdfSharpCore.Pdf.PdfPage pdfPage = pdfDocument.AddPage();
                pdfPage.Width = bitmap.Width;
                pdfPage.Height = bitmap.Height;

                /*
                 * PDF 描画用の XGraphics を取得。
                 * XGraphics は PDF ページに対する描画コンテキスト。
                 */
                PdfSharpCore.Drawing.XGraphics xGraphics =
            PdfSharpCore.Drawing.XGraphics.FromPdfPage(pdfPage);

                /*
                 * Bitmap を PNG として一度 MemoryStream に保存し、
                 * その PNG データを XImage として読み込む。
                 *
                 * PdfSharpCore は System.Drawing.Bitmap を直接扱えないため、
                 * 一度 PNG などの画像形式に変換する必要がある。
                 */
                using(MemoryStream imgStream = new()) {

                    // Bitmap → PNG 形式で MemoryStream に保存
                    bitmap.Save(imgStream, System.Drawing.Imaging.ImageFormat.Png);
                    imgStream.Position = 0; // 読み込み位置を先頭に戻す

                    /*
                     * XImage.FromStream は「ストリームを返すデリゲート」を要求するため、
                     * imgStream の内容を新しい MemoryStream にコピーして渡す。
                     * （PdfSharpCore の仕様で、ストリームはクローズされる可能性があるため）
                     */
                    PdfSharpCore.Drawing.XImage xImage =
                PdfSharpCore.Drawing.XImage.FromStream(
                    () => new MemoryStream(imgStream.ToArray())
                );

                    // PDF ページに画像を描画（左上 0,0 に原寸で貼り付け）
                    xGraphics.DrawImage(xImage, 0, 0, bitmap.Width, bitmap.Height);
                }

                /*
                 * PDF を MemoryStream に保存。
                 * 第二引数の "false" は「閉じるときにストリームをクローズしない」設定。
                 * （pdfStream を using で管理しているため、ここでは閉じない）
                 */
                pdfDocument.Save(pdfStream, false);

                // PDF の byte[] を返す
                return pdfStream.ToArray();
            }
        }
    }
}
