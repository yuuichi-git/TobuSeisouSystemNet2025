using CcControl;

using Common;

using Dao;

using Vo;

namespace PdfView {
    public partial class PdfControlView : Form {
        private string _returnValue;
        private MemoryStream _memoryStream;
        private readonly PdfUtility _pdfUtility = new();
        private string _id;
        /*
         * Dao
         */
        private PdfFileDao _pdfFileDao;
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;
        private PdfFileVo _pdfFileVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public PdfControlView(ConnectionVo connectionVo, Screen screen, string id = null) {
            _id = id;
            /*
             * Dao
             */
            _pdfFileDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            /*
             * InitializeControl
             */
            InitializeComponent();
            /*
             * MenuStrip
             */
            List<string> listString = new() {
                "ToolStripMenuItemFile",
                "ToolStripMenuItemExit",
                "ToolStripMenuItemPDF",
                "ToolStripMenuItemPDFOpen",
                "ToolStripMenuItemHelp"
            };
            this.CcMenuStrip1.ChangeEnable(listString);

            if(_id is not null && _pdfFileDao.ExistsById(_id)) {
                // 画面に表示するデータを取得する
                PdfFileVo pdfFileVo = _pdfFileDao.SelectOnePdfFile(_id);
                ShowPdfToViewer(this.CcPdfView1, pdfFileVo.PdfImage);

            } else {
                // 新規登録画面用に初期化
                ClearPdfViewer(this.CcPdfView1);

            }
            /*
             * Eventを登録する
             */
            this.CcMenuStrip1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CcButtonUpdate_Click(object sender, EventArgs e) {
            // PDF が読み込まれていない場合は何もしない
            if(_memoryStream is null || _memoryStream.Length == 0)
                return;

            if(_id is not null && _pdfFileDao.ExistsById(_id)) {
                // UPDATE
                PdfFileVo pdfFileVo = new();
                pdfFileVo.Id = _id;
                pdfFileVo.PdfImage = _memoryStream.ToArray();
                pdfFileVo.InsertPcName = Environment.MachineName;
                pdfFileVo.InsertYmdHms = DateTime.Now;
                pdfFileVo.UpdatePcName = Environment.MachineName;
                pdfFileVo.UpdateYmdHms = DateTime.Now;
                pdfFileVo.DeletePcName = Environment.MachineName;
                pdfFileVo.DeleteYmdHms = DateTime.Now;
                pdfFileVo.DeleteFlag = false;
                _pdfFileDao.UpdateOnePdfFile(pdfFileVo);
                // 画面を閉じる前に ReturnValue に ID を設定する
                ReturnValue = pdfFileVo.Id;
                this.DialogResult = DialogResult.OK;
                this.Close();

            } else {
                // INSERT
                try {
                    PdfFileVo pdfFileVo = new();
                    pdfFileVo.Id = Guid.NewGuid().ToString("N");
                    pdfFileVo.PdfImage = _memoryStream.ToArray();
                    pdfFileVo.InsertPcName = Environment.MachineName;
                    pdfFileVo.InsertYmdHms = DateTime.Now;
                    pdfFileVo.UpdatePcName = Environment.MachineName;
                    pdfFileVo.UpdateYmdHms = DateTime.Now;
                    pdfFileVo.DeletePcName = Environment.MachineName;
                    pdfFileVo.DeleteYmdHms = DateTime.Now;
                    pdfFileVo.DeleteFlag = false;
                    _pdfFileDao.InsertOnePdfFile(pdfFileVo);
                    // 画面を閉じる前に ReturnValue に ID を設定する
                    ReturnValue = pdfFileVo.Id;
                    this.DialogResult = DialogResult.OK;
                    this.Close();

                } catch(Exception exception) {
                    MessageBox.Show($"PDF ファイルの保存に失敗しました。\n{exception.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch(((ToolStripMenuItem)sender).Name) {
                /*
                 * PDF を開く処理
                 * ConvertPdfToByte() → byte[] を取得 → Viewer に表示
                 */
                case "ToolStripMenuItemPDFOpen":
                    byte[] bytes = _pdfUtility.ConvertPdfToByte(this);
                    /*
                     * ファイル選択がキャンセルされた場合は null が返るため、そのまま終了。
                     */
                    if(bytes is null)
                        return;
                    /*
                     * PDF を Viewer に表示する
                     */
                    ShowPdfToViewer(this.CcPdfView1, bytes);
                    break;
                /*
                 * アプリケーション終了
                 */
                case "ToolStripMenuItemExit":
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// 指定された CcPdfView に PDF（byte[]）を表示する
        /// </summary>
        /// <param name="ccPdfView"></param>
        /// <param name="pdfBytes"></param>
        private void ShowPdfToViewer(CcPdfView ccPdfView, byte[] pdfBytes) {
            /*
             * 既存の MemoryStream を破棄する
             * PdfiumViewer はネイティブメモリを使うため、Dispose を確実に呼ぶ必要がある。
             */
            if(_memoryStream is not null) {
                _memoryStream.Dispose();
                _memoryStream = null;
            }
            /*
             * 新しい MemoryStream を生成
             * SetPdfStream() は Stream を保持するため、フォーム側で生存期間を管理する。
             */
            _memoryStream = new MemoryStream(pdfBytes);
            /*
             * CcPdfView の内部状態をリセット
             * Unload() は内部の PdfDocument と Stream を Dispose する。
             */
            ccPdfView.Unload();
            /*
             * 新しい PDF を読み込ませる
             * SetPdfStream() 内で PdfDocument.Load(_memoryStream) が実行される。
             */
            ccPdfView.SetPdfStream(_memoryStream);
        }

        /// <summary>
        /// 指定された CcPdfView をクリアする
        /// PDF を閉じたい場合に使用する。
        /// </summary>
        /// <param name="ccPdfView"></param>
        private void ClearPdfViewer(CcPdfView ccPdfView) {
            /*
             * MemoryStream の破棄
             */
            if(_memoryStream is not null) {
                _memoryStream.Dispose();
                _memoryStream = null;
            }
            /*
             * Viewer の内部状態をリセット
             */
            ccPdfView.Unload();
        }

        /// <summary>
        /// Foem の戻り値を取得または設定するプロパティ
        /// </summary>
        public string ReturnValue {
            get {
                return _returnValue;
            }

            set {
                _returnValue = value;
            }
        }
    }
}
