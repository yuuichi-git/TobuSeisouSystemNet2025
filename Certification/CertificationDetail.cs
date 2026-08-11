/*
 * 2025-05-21
 */
using CcControl;

using Common;

using Dao;

using Vo;

namespace Certification {
    public partial class CertificationDetail : Form {
        private readonly DateTime _defaultDatetime = new(1900, 01, 01);
        private int _staffCode;
        private int _certificationCode;
        private PdfUtility _pdfUtility = new();
        /*
         * Dao
         */
        private readonly CertificationFileDao _certificationFileDao;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="staffCode"></param>
        /// <param name="certificationCode">資格コード</param>
        public CertificationDetail(ConnectionVo connectionVo, int staffCode, int certificationCode) {
            _staffCode = staffCode;
            _certificationCode = certificationCode;
            /*
             * Dao
             */
            _certificationFileDao = new(connectionVo);
            /*
             * InitializeControl
             */
            InitializeComponent();
            /*
             * MenuStrip
             */
            List<string> listString = new() {"ToolStripMenuItemFile",
                                             "ToolStripMenuItemExit",
                                             "ToolStripMenuItemPrint",
                                             "ToolStripMenuItemPrintA4",
                                             "ToolStripMenuItemHelp"};
            this.CcMenuStrip1.ChangeEnable(listString);
            this.CcMenuStrip1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;

            this.SetControls(_certificationFileDao.SelectOneCertificationFile(_staffCode, _certificationCode));
            this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExUpdate_Click(object sender, EventArgs e) {
            CertificationFileVo certificationFileVo = new();
            certificationFileVo.StaffCode = _staffCode;
            certificationFileVo.CertificationCode = _certificationCode;
            certificationFileVo.MarkCode = 0;
            certificationFileVo.Image1Flag = this.CcPdfView1.MemoryStream != null ? true : false;
            certificationFileVo.Image1 = this.CcPdfView1.MemoryStream?.ToArray() ?? Array.Empty<byte>();                        // PDF（byte[]）をセット
            certificationFileVo.Image2Flag = this.CcPdfView2.MemoryStream != null ? true : false;
            certificationFileVo.Image2 = this.CcPdfView2.MemoryStream?.ToArray() ?? Array.Empty<byte>();                        // PDF（byte[]）をセット
            certificationFileVo.InsertPcName = Environment.MachineName;
            certificationFileVo.InsertYmdHms = _defaultDatetime;
            certificationFileVo.UpdatePcName = string.Empty;
            certificationFileVo.UpdateYmdHms = _defaultDatetime;
            certificationFileVo.DeletePcName = string.Empty;
            certificationFileVo.DeleteYmdHms = _defaultDatetime;
            certificationFileVo.DeleteFlag = false;
            /*
             * DBを更新
             * 存在すればUPDATE、存在しなければINSERT
             */
            if(_certificationFileDao.ExistenceHCertificationFile(_staffCode, _certificationCode)) {
                try {
                    _certificationFileDao.UpdateOneLicenseLedger(certificationFileVo);
                    this.Close();
                } catch(Exception exception) {
                    MessageBox.Show(exception.Message);
                }
            } else {
                try {
                    _certificationFileDao.InsertOneCertificationFile(certificationFileVo);
                    this.Close();
                } catch(Exception exception) {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="certificationFileVo"></param>
        private void SetControls(CertificationFileVo certificationFileVo) {
            /*
             * PDF 表示
             */
            if(certificationFileVo.Image1Flag) {
                CcPdfView1.SetPdfBytes(certificationFileVo.Image1);
            }
            if(certificationFileVo.Image2Flag) {
                CcPdfView2.SetPdfBytes(certificationFileVo.Image2);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch(((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemExit":                                                                   // アプリケーションを終了する
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CcContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            if(sender is not ContextMenuStrip contextMenuStrip)
                return;

            if(contextMenuStrip.SourceControl is not CcPdfView ccPdfView)
                return;

            switch(e.ClickedItem.Name) {
                case "ToolStripMenuItemOpen":
                    byte[] bytes = _pdfUtility.ConvertPdfToBytes(contextMenuStrip);
                    if(bytes is null)
                        return;

                    this.ShowPdfToViewer(ccPdfView, bytes);
                    this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "PDF を表示しました。";
                    break;
                case "ToolStripMenuItemPaste": {
                    IDataObject data = Clipboard.GetDataObject();
                    if(data == null) {
                        MessageBox.Show("クリップボードが空です。");
                        break;
                    }

                    if(data.GetDataPresent(DataFormats.Bitmap)) {
                        Bitmap bmp = (Bitmap)data.GetData(DataFormats.Bitmap);
                        if(bmp == null) {
                            MessageBox.Show("画像の取得に失敗しました。");
                            break;
                        }

                        byte[] pdfBytes = _pdfUtility.ConvertImageToPdfBytes(bmp);
                        if(pdfBytes == null || pdfBytes.Length == 0) {
                            MessageBox.Show("画像を PDF に変換できませんでした。");
                            break;
                        }

                        ccPdfView.Clear();
                        ccPdfView.SetPdfStream(new MemoryStream(pdfBytes));

                        this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "画像を PDF として貼り付けました。";
                        break;
                    }

                    MessageBox.Show("クリップボードに画像がありません。");
                    break;
                }

                case "ToolStripMenuItemDelete":
                    ccPdfView.Clear();                                                                                          // PdfViewer の PDF を破棄
                    this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "PDF を削除しました。";
                    break;
            }
        }

        /// <summary>
        /// 指定された PdfViewer に PDF（byte[]）を表示する
        /// </summary>
        /// <param name="ccPdfView">PdfViewer のインスタンス</param>
        /// <param name="pdfBytes">PDF のバイト配列</param>
        private void ShowPdfToViewer(CcPdfView ccPdfView, byte[] pdfBytes) {
            ccPdfView.Clear();
            ccPdfView.SetPdfStream(new MemoryStream(pdfBytes));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CertificationDetail_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
