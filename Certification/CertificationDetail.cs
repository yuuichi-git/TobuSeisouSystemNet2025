/*
 * 2025-05-21
 */
using ControlEx;

using Dao;

using Vo;

namespace Certification {
    public partial class CertificationDetail : Form {
        private readonly DateTime _defaultDatetime = new(1900, 01, 01);
        private int _staffCode;
        private int _certificationCode;
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
            List<string> listString = new() {
                "ToolStripMenuItemFile",
                "ToolStripMenuItemExit",
                "ToolStripMenuItemPrint",
                "ToolStripMenuItemPrintA4",
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);

            this.PutControl(_certificationFileDao.SelectOneCertificationFile(_staffCode, _certificationCode));
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;
            /*
             * Eventを登録する
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
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
            certificationFileVo.Picture1Flag = PictureBoxEx1.Image is not null ? true : false;
            certificationFileVo.Picture1 = (byte[])new ImageConverter().ConvertTo(PictureBoxEx1.Image, typeof(byte[]));
            certificationFileVo.Picture2Flag = PictureBoxEx2.Image is not null ? true : false;
            certificationFileVo.Picture2 = (byte[])new ImageConverter().ConvertTo(PictureBoxEx2.Image, typeof(byte[]));
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
            if (_certificationFileDao.ExistenceHCertificationFile(_staffCode, _certificationCode)) {
                try {
                    _certificationFileDao.UpdateOneLicenseLedger(certificationFileVo);
                    this.Close();
                } catch (Exception exception) {
                    MessageBox.Show(exception.Message);
                }
            } else {
                try {
                    _certificationFileDao.InsertOneCertificationFile(certificationFileVo);
                    this.Close();
                } catch (Exception exception) {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="certificationFileVo"></param>
        private void PutControl(CertificationFileVo certificationFileVo) {
            if (certificationFileVo.Picture1.Length > 0) {
                ImageConverter imageConv = new();
                PictureBoxEx1.Image = (Image)imageConv.ConvertFrom(certificationFileVo.Picture1); //写真１
            }
            if (certificationFileVo.Picture2.Length > 0) {
                ImageConverter imageConv = new();
                PictureBoxEx2.Image = (Image)imageConv.ConvertFrom(certificationFileVo.Picture2); //写真２
            }
        }

        /// <summary>
        /// ToolStripMenuItemがクリックされた時のSourceControlを保持
        /// </summary>
        Control _sourceControl = null;
        /// <summary>
        /// ContextMenuStrip1_Opened
        /// コンテキストが開かれた親コントロールを取得する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStrip1_Opened(object sender, EventArgs e) {
            //ContextMenuStripを表示しているコントロールを取得する
            _sourceControl = ((ContextMenuStrip)sender).SourceControl;
        }

        /// <summary>
        /// ToolStripMenuItem_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                /*
                 * Picture クリップボード
                 */
                case "ToolStripMenuItemClip":
                    ((CcPictureBox)_sourceControl).Image = (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap);
                    break;
                /*
                 * Picture 削除
                 */
                case "ToolStripMenuItemDelete":
                    ((CcPictureBox)_sourceControl).Image = null;
                    break;
                /*
                 * アプリケーションを終了する
                 */
                case "ToolStripMenuItemExit":
                    this.Close();
                    break;
            }
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
