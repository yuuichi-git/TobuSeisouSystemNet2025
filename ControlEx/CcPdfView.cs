/*
 * 2026-05-18
 * PdfiumViewer.Core                                // PDF を表示するためのライブラリ PdfiumViewer のコアライブラリー .NET 6.0 以降で動作
 * HiraokaHyperTools.PdfiumViewer.Native.Windows    // PdfiumViewer のネイティブライブラリー Windows 用
 * PdfSharpCore                                     // PDF を作る／編集するためのライブラリ
 */
using System.ComponentModel;

using PdfiumViewer;

namespace CcControl {
    public partial class CcPdfView : PdfViewer {
        private PdfDocument? _pdfDocument = null;
        private MemoryStream _memoryStream;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public CcPdfView() {
            this.ShowToolbar = true;
            this.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// ※PDFの作成
        /// PdfSharpCoreを使用
        /// Bitmap を PDF に埋め込み、PDF の byte[] を返す
        /// </summary>
        public byte[] ConvertImageToPdfBytes(Bitmap bitmap) {
            using(MemoryStream pdfStream = new()) {

                PdfSharpCore.Pdf.PdfDocument pdfDocument = new();
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
        /// 表示中の PDF を Image(Bitmap) として返す
        /// </summary>
        /// <param name="page">ページ番号（0から）</param>
        /// <param name="dpi">出力 DPI（印刷用途なら 200～300）</param>
        /// <returns>Bitmap (Image)</returns>
        public Bitmap? GetPageImage(int page = 0, int dpi = 200) {
            if(this.PdfDocument == null)
                return null;

            if(page < 0 || page >= this.PdfDocument.PageCount)
                return null;

            try {
                // ★ ページサイズを取得
                var size = this.PdfDocument.PageSizes[page];

                // ★ 実寸に合わせたピクセル数を計算
                int width = (int)(size.Width * dpi / 72.0f);
                int height = (int)(size.Height * dpi / 72.0f);

                // ★ PdfiumViewer の Render を使用して Bitmap を生成
                Bitmap bitmap = (Bitmap)this.PdfDocument.Render(page, width, height, dpi, dpi, PdfRenderFlags.Annotations);

                return bitmap;
            } catch(Exception ex) {
                MessageBox.Show($"PDFページの描画に失敗しました: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// ※PDFの表示
        /// PDF を MemoryStream から読み込む
        /// </summary>
        public void SetPdfStream(MemoryStream stream) {
            if(stream == null)
                return;

            this.MemoryStream = stream;
            this.MemoryStream.Position = 0;

            this.PdfDocument = PdfDocument.Load(this.MemoryStream);
            this.Document = this.PdfDocument;
        }

        /// <summary>
        /// ※PDFの表示
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

            this.MemoryStream = new MemoryStream(bytes, false);
            this.MemoryStream.Position = 0;                                                                                     // 次に読み込むときのために、必ず Position を 0 に戻す

            this.PdfDocument = PdfDocument.Load(this.MemoryStream);
            this.Document = this.PdfDocument;
            this.ZoomMode = PdfViewerZoomMode.FitWidth;                                                                         // 横幅に合わせる
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
        /// ※改良の余地あり　画面をクリアする方法を探して！
        /// </summary>
        public void Clear() {
            this.MemoryStream = null;
            this.PdfDocument = null;
            this.Document = null;

            // 再描画して確実に消す
            this.Invalidate();
            this.Refresh();
        }

        /*
         * ----------------------------------------------------------------
         * Getter / Setter
         * ----------------------------------------------------------------
         */
        [Category("RisSoft")]
        [Browsable(false)]
        [Description("")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PdfDocument? PdfDocument {
            get {
                return _pdfDocument;
            }
            set {
                _pdfDocument = value;
            }
        }

        [Category("RisSoft")]
        [Browsable(false)]
        [Description("")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MemoryStream? MemoryStream {
            get {
                return _memoryStream;
            }
            set {
                _memoryStream = value;
            }
        }
    }
}
