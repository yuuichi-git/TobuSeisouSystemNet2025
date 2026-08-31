/*
 * 2026-04-07
 */
using CcControl;

using Common;

using Dao;

using Vo;

namespace VoluntaryAutomobileInsurance {
    public partial class VoluntaryAutomobileInsuranceDetail : Form {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private VoluntaryAutomobileInsuranceVo _voluntaryAutomobileInsuranceVo;
        private PdfUtility _pdfUtility = new();
        private CcPdfView[] _ccPdfViews = new CcPdfView[4];             // 4つの PdfViewer（経路図 / 自賠責 / 任意保険 / 通勤許可証）
        /*
         * Dao
         */
        private VoluntaryAutomobileInsuranceDao _voluntaryAutomobileInsuranceDao;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="voluntaryAutomobileInsuranceVo"></param>
        public VoluntaryAutomobileInsuranceDetail(ConnectionVo connectionVo, VoluntaryAutomobileInsuranceVo voluntaryAutomobileInsuranceVo) {
            /*
             * Dao
             */
            _voluntaryAutomobileInsuranceDao = new(connectionVo);
            _voluntaryAutomobileInsuranceVo = voluntaryAutomobileInsuranceVo;
            /*
             * InitializeControl
             */
            InitializeComponent();
            /*
             * MenuStrip
             */
            List<string> listString = new() {"ToolStripMenuItemFile",
                                             "ToolStripMenuItemExit",
                                             "ToolStripMenuItemHelp"};
            this.CcMenuStrip1.ChangeEnable(listString);
            this.CcMenuStrip1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
            // コントロールの初期化
            this.InitializeControl();

            // 表示対象のデータを画面へ反映
            this.SetControls(_voluntaryAutomobileInsuranceVo.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CcButtonUpdate_Click(object sender, EventArgs e) {
            DialogResult dialogResult = MessageBox.Show("登録します。よろしいですか？", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            switch(dialogResult) {
                case DialogResult.OK:
                    /*
                     * 新規 or 更新用の VO を作成
                     */
                    VoluntaryAutomobileInsuranceVo voluntaryAutomobileInsuranceVo = new();
                    voluntaryAutomobileInsuranceVo.Id = _voluntaryAutomobileInsuranceVo.Id is not null ? _voluntaryAutomobileInsuranceVo.Id : Guid.NewGuid().ToString("N");
                    voluntaryAutomobileInsuranceVo.StaffCode = _voluntaryAutomobileInsuranceVo.StaffCode;
                    voluntaryAutomobileInsuranceVo.VehicleType = this.CcComboBoxVehicleType.Text;
                    voluntaryAutomobileInsuranceVo.CompanyName = this.CcComboBoxCompanyName.Text;
                    voluntaryAutomobileInsuranceVo.AutomaticRenewal = this.CcCheckBoxAutomaticRenewal.Checked;
                    voluntaryAutomobileInsuranceVo.StartDate = this.CcDateTimePickerStartDate.Value.ToString("yyyy-MM-dd");
                    voluntaryAutomobileInsuranceVo.EndDate = this.CcDateTimePickerEndDate.Value.ToString("yyyy-MM-dd");
                    /*
                     * PDF（byte[]）をセット
                     */
                    voluntaryAutomobileInsuranceVo.Image1 = _ccPdfViews[0].MemoryStream?.ToArray() ?? Array.Empty<byte>();
                    voluntaryAutomobileInsuranceVo.Image2 = _ccPdfViews[1].MemoryStream?.ToArray() ?? Array.Empty<byte>();
                    voluntaryAutomobileInsuranceVo.Image3 = _ccPdfViews[2].MemoryStream?.ToArray() ?? Array.Empty<byte>();
                    voluntaryAutomobileInsuranceVo.Image4 = _ccPdfViews[3].MemoryStream?.ToArray() ?? Array.Empty<byte>();
                    /*
                     * INSERT or UPDATE の判定
                     */
                    if(_voluntaryAutomobileInsuranceDao.ExistsById(voluntaryAutomobileInsuranceVo.Id)) {
                        _voluntaryAutomobileInsuranceDao.UpdateOneVoluntaryAutomobileInsuranceVo(voluntaryAutomobileInsuranceVo);
                        this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "更新が完了しました。";
                    } else {
                        _voluntaryAutomobileInsuranceDao.InsertOneVoluntaryAutomobileInsuranceVo(voluntaryAutomobileInsuranceVo);
                        this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "新規登録が完了しました。";
                    }
                    // 二度押し防止/更新後は編集不可にする
                    ((CcButton)sender).Enabled = false;
                    this.CcComboBoxVehicleType.Enabled = false;
                    this.CcComboBoxCompanyName.Enabled = false;
                    this.CcCheckBoxAutomaticRenewal.Enabled = false;
                    this.CcDateTimePickerStartDate.Enabled = false;
                    this.CcDateTimePickerEndDate.Enabled = false;
                    break;
                case DialogResult.Cancel:
                    break;
            }
        }

        /// <summary>
        /// 画面へ PDF 等を表示する
        /// </summary>
        private void SetControls(string id) {
            if(id is not null && _voluntaryAutomobileInsuranceDao.ExistsById(id)) {
                this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "指定のデータは存在します。";
                VoluntaryAutomobileInsuranceVo voluntaryAutomobileInsuranceVo = _voluntaryAutomobileInsuranceDao.SelectOneById(_voluntaryAutomobileInsuranceVo.Id);

                if(voluntaryAutomobileInsuranceVo is null)
                    return;

                /*
                 * 画面項目へ反映
                 */
                this.CcComboBoxVehicleType.Text = voluntaryAutomobileInsuranceVo.VehicleType;                               // 対象車両種別
                this.CcComboBoxCompanyName.Text = voluntaryAutomobileInsuranceVo.CompanyName;                               // 保険会社名
                this.CcCheckBoxAutomaticRenewal.Checked = voluntaryAutomobileInsuranceVo.AutomaticRenewal;                  // 自動更新
                if(DateTime.TryParse(voluntaryAutomobileInsuranceVo.StartDate, out DateTime start)) {                       // 開始日
                    if(start.Date == _defaultDateTime.Date) {
                        this.CcDateTimePickerStartDate.SetEmpty();
                    } else {
                        this.CcDateTimePickerStartDate.Value = start;
                    }
                }
                if(DateTime.TryParse(voluntaryAutomobileInsuranceVo.EndDate, out DateTime end)) {                           // 終了日
                    if(end.Date == _defaultDateTime.Date) {
                        this.CcDateTimePickerEndDate.SetEmpty();
                    } else {
                        this.CcDateTimePickerEndDate.Value = end;
                    }
                }
                /*
                 * PDF 表示（Image1〜4）
                 */
                _ccPdfViews[0].SetPdfBytes(voluntaryAutomobileInsuranceVo.Image1);
                _ccPdfViews[1].SetPdfBytes(voluntaryAutomobileInsuranceVo.Image2);
                _ccPdfViews[2].SetPdfBytes(voluntaryAutomobileInsuranceVo.Image3);
                _ccPdfViews[3].SetPdfBytes(voluntaryAutomobileInsuranceVo.Image4);

            } else {
                this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "指定のデータは存在しません。";

                /*
                 * 画面クリア
                 */
                this.CcComboBoxVehicleType.Text = string.Empty;
                this.CcComboBoxCompanyName.Text = string.Empty;

                /*
                 * PDF クリア
                 */
                for(int i = 0; i < 4; i++) {
                    _ccPdfViews[i].Clear();
                }
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
        /// コントロールを初期化
        /// </summary>
        private void InitializeControl() {
            // 対象車両種別
            this.InitializeCcComboBoxVehicleType();
            // 保険会社名
            this.InitializeCcComboBoxCompanyName();
            // 自動更新
            this.CcCheckBoxAutomaticRenewal.Checked = false;
            // 開始日・終了日
            this.CcDateTimePickerStartDate.Value = DateTime.Now.AddDays(1);
            this.CcDateTimePickerEndDate.Value = DateTime.Now.AddYears(1);

            // PDF 表示エリア
            TabPage[] tabPages = new TabPage[4];
            tabPages[0] = this.TabPage1;
            tabPages[1] = this.TabPage2;
            tabPages[2] = this.TabPage3;
            tabPages[3] = this.TabPage4;

            // 4つの CcPdfView を生成して TabPage に配置
            for(int i = 0; i < 4; i++) {
                _ccPdfViews[i] = new();
                _ccPdfViews[i].Tag = i;
                tabPages[i].Controls.Add(_ccPdfViews[i]);
                _ccPdfViews[i].ContextMenuStrip = this.CcContextMenuStrip1;                                                     // 共通の ContextMenuStrip を設定
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeCcComboBoxVehicleType() {
            this.CcComboBoxVehicleType.Items.Clear();
            foreach(string data in _voluntaryAutomobileInsuranceDao.SelectGroupVehicleType())
                this.CcComboBoxVehicleType.Items.Add(data);
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeCcComboBoxCompanyName() {
            this.CcComboBoxCompanyName.Items.Clear();
            foreach(string data in _voluntaryAutomobileInsuranceDao.SelectGroupCompanyName())
                this.CcComboBoxCompanyName.Items.Add(data);
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
        private void CcContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
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

                    // ★ クリップボードに画像があるか？
                    if(data.GetDataPresent(DataFormats.Bitmap)) {
                        Bitmap bmp = (Bitmap)data.GetData(DataFormats.Bitmap);
                        if(bmp == null) {
                            MessageBox.Show("画像の取得に失敗しました。");
                            break;
                        }

                        // ★ Bitmap → PDF(byte[]) に変換
                        byte[] pdfBytes = _pdfUtility.ConvertImageToPdfBytes(bmp);
                        if(pdfBytes == null || pdfBytes.Length == 0) {
                            MessageBox.Show("画像を PDF に変換できませんでした。");
                            break;
                        }

                        // ★ PdfiumViewer に表示
                        //ccPdfView.MemoryStream?.Dispose();
                        ccPdfView.MemoryStream = new MemoryStream(pdfBytes);

                        //ccPdfView.Clear();
                        ccPdfView.SetPdfStream(ccPdfView.MemoryStream);

                        this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "画像を PDF として貼り付けました。";
                        break;
                    }

                    MessageBox.Show("クリップボードに画像がありません。");
                    break;
                }

                case "ToolStripMenuItemDelete":
                    ccPdfView.Clear();
                    this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "PDF を削除しました。";
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VoluntaryAutomobileInsuranceDetail_FormClosing(object sender, FormClosingEventArgs e) {
            DialogResult dialogResult = MessageBox.Show("アプリケーションを終了します。よろしいですか？", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            switch(dialogResult) {
                case DialogResult.OK:
                    e.Cancel = false;
                    Dispose();
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }
    }
}
