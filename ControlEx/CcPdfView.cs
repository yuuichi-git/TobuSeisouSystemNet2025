/*
 * 2026-05-18
 * PdfiumViewer.Core                                // PdfiumViewer のコアライブラリー .NET 6.0 以降で動作
 * HiraokaHyperTools.PdfiumViewer.Native.Windows    // PdfiumViewer のネイティブライブラリー Windows 用
 */
using PdfiumViewer;

namespace CcControl {
    public partial class CcPdfView : PdfViewer {
        private PdfDocument  _pdfDocument;
        private MemoryStream _memoryStream;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public CcPdfView() {
            this.ShowToolbar = true;
            this.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// PDF を MemoryStream から読み込む
        /// </summary>
        public void SetPdfStream(MemoryStream stream) {
            if(stream == null)
                return;

            this.Clear();                         // ★安全に破棄

            this.MemoryStream = stream;
            this.MemoryStream.Position = 0;           // ★必須

            this.PdfDocument = PdfDocument.Load(this.MemoryStream);
            this.Document = this.PdfDocument;
        }

        /// <summary>
        /// PDF または画像 byte[] を読み込む
        /// PDF ならそのまま、画像なら PDF に変換して読み込む
        /// </summary>
        public void SetPdfBytes(byte[] bytes) {
            if(bytes == null || bytes.Length == 0)
                return;

            // PDF 判定
            if(!IsPdf(bytes)) {
                try {
                    using(MemoryStream stream = new MemoryStream(bytes))
                    using(Bitmap bitmap = new Bitmap(stream)) {
                        bytes = ConvertImageToPdfBytes(bitmap);
                    }
                } catch {
                    this.Clear();
                    return;
                }
            }

            this.Clear();

            this.MemoryStream = new MemoryStream(bytes, false);
            this.MemoryStream.Position = 0;                                                                     // 次に読み込むときのために、必ず Position を 0 に戻す

            this.PdfDocument = PdfDocument.Load(this.MemoryStream);
            this.Document = this.PdfDocument;
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
        /// Bitmap を PDF に埋め込み、PDF の byte[] を返す
        /// </summary>
        public byte[] ConvertImageToPdfBytes(Bitmap bitmap) {
            using(MemoryStream pdfStream = new MemoryStream()) {

                PdfSharpCore.Pdf.PdfDocument pdfDocument = new PdfSharpCore.Pdf.PdfDocument();
                PdfSharpCore.Pdf.PdfPage     pdfPage     = pdfDocument.AddPage();

                pdfPage.Width = bitmap.Width;
                pdfPage.Height = bitmap.Height;

                PdfSharpCore.Drawing.XGraphics xGraphics = PdfSharpCore.Drawing.XGraphics.FromPdfPage(pdfPage);

                using(MemoryStream imgStream = new MemoryStream()) {
                    bitmap.Save(imgStream, System.Drawing.Imaging.ImageFormat.Png);
                    imgStream.Position = 0;

                    PdfSharpCore.Drawing.XImage xImage = PdfSharpCore.Drawing.XImage.FromStream(() => new MemoryStream(imgStream.ToArray()));
                    xGraphics.DrawImage(xImage, 0, 0, bitmap.Width, bitmap.Height);
                }
                pdfDocument.Save(pdfStream, false);
                return pdfStream.ToArray();
            }
        }

        /// <summary>
        /// 表示中の PDF を破棄する
        /// ※改良の余地あり　画面をクリアする方法を探して！
        /// </summary>
        public void Clear() {
            this.PdfDocument = null;
            this.MemoryStream = null;
        }

        /*
         * ----------------------------------------------------------------
         * Getter / Setter
         * ----------------------------------------------------------------
         */
        /// <summary>
        /// PdfDocument
        /// </summary>
        public PdfDocument PdfDocument {
            get {
                return this._pdfDocument;
            }
            set {
                this._pdfDocument = value;
            }
        }
        /// <summary>
        /// MemoryStream
        /// </summary>
        public MemoryStream MemoryStream {
            get {
                return this._memoryStream;
            }
            set {
                this._memoryStream = value;
            }
        }
    }
}
