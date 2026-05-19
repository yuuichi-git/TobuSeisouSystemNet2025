/*
 * 2026-05-18
 */
using PdfiumViewer;

namespace CcControl {
    public partial class CcPdfView : PdfViewer {

        private PdfDocument _document;
        private MemoryStream _stream;

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
            if (stream is null)
                return;
            Unload();                                                  // 既存の PDF を破棄

            // 新しいストリームを保持
            _stream = stream;

            // PdfiumViewer の PdfDocument を生成
            _document = PdfDocument.Load(_stream);

            // PdfViewer にセット
            this.Document = _document;
        }

        /// <summary>
        /// PDF を byte[] から読み込む
        /// </summary>
        public void SetPdfBytes(byte[] bytes) {
            if (bytes is null || bytes.Length == 0)
                return;
            Unload();                                                       // 既存の PDF を破棄

            _stream = new MemoryStream(bytes);
            _document = PdfDocument.Load(_stream);

            this.Document = _document;
        }

        /// <summary>
        /// 表示中の PDF を破棄する
        /// </summary>
        public void Unload() {
            this.Document = null;                                           // PdfViewer の Document を解除
            /*
             * PdfDocument を破棄
             */
            if (_document is not null) {
                _document.Dispose();
                _document = null;
            }
            /*
             * MemoryStream を破棄
             */
            if (_stream is not null) {
                _stream.Dispose();
                _stream = null;
            }
        }
    }
}
