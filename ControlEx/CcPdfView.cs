/*
 * 2026-05-18
 * PdfiumViewer.Core                                // PdfiumViewer のコアライブラリー .NEt 6.0 以降で動作
 * HiraokaHyperTools.PdfiumViewer.Native.Windows    // PdfiumViewer のネイティブライブラリー Windows 用
 */
using PdfiumViewer;

namespace CcControl {
    public partial class CcPdfView : PdfViewer {
        private PdfDocument _pdfDocument;
        private MemoryStream _memoryStream;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public CcPdfView() {
            this.ShowToolbar = true;                                        // 標準のツールバーを表示する
            this.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// PDF を MemoryStream から読み込む
        /// </summary>
        public void SetPdfStream(MemoryStream stream) {
            if(stream is null)
                return;
            Unload();                                                       // 既存の PDF を破棄

            // 新しいストリームを保持
            _memoryStream = stream;

            // PdfiumViewer の PdfDocument を生成
            _pdfDocument = PdfDocument.Load(_memoryStream);

            // PdfViewer にセット
            this.Document = _pdfDocument;
        }

        /// <summary>
        /// PDF または画像 byte[] を読み込む
        /// PDF ならそのまま、画像なら PDF に変換して読み込む
        /// </summary>
        public void SetPdfBytes(byte[] bytes) {
            if(bytes is null || bytes.Length == 0)
                return;

            // ★ PDF 判定（%PDF- で始まるか）
            if(!IsPdf(bytes)) {
                // ★ PDF でない → 画像として扱い PDF に変換
                try {
                    using(MemoryStream ms = new MemoryStream(bytes))
                    using(Bitmap bitmap = new Bitmap(ms)) {
                        bytes = ConvertImageToPdfBytes(bitmap);
                    }
                } catch {
                    // ★ 画像としても不正 → 表示クリア
                    Unload();
                    return;
                }
            }

            // ★ 既存 PDF を破棄
            Unload();

            // ★ 新しい PDF を読み込む
            _memoryStream = new MemoryStream(bytes, false);   // 読み取り専用
            _pdfDocument = PdfDocument.Load(_memoryStream);

            this.Document = _pdfDocument;
        }

        /// <summary>
        /// PDF かどうか判定（%PDF-）
        /// </summary>
        private bool IsPdf(byte[] bytes) {
            if(bytes.Length < 5)
                return false;

            return bytes[0] == 0x25 &&   // %
                   bytes[1] == 0x50 &&   // P
                   bytes[2] == 0x44 &&   // D
                   bytes[3] == 0x46 &&   // F
                   bytes[4] == 0x2D;     // -
        }

        /// <summary>
        /// 表示中の PDF を破棄する
        /// </summary>
        public void Unload() {

            // PdfViewer の Document を解除
            this.Document = null;

            // PdfDocument を破棄
            if(_pdfDocument is not null) {
                _pdfDocument.Dispose();
                _pdfDocument = null;
            }

            // MemoryStream を破棄
            if(_memoryStream is not null) {
                _memoryStream.Dispose();
                _memoryStream = null;
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
