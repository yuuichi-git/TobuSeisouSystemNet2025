/*
 * 2026-04-07
 */
using CcControl;

using Common;

using Dao;

using Vo;

namespace VoluntaryAutomobileInsurance {
    public partial class VoluntaryAutomobileInsuranceDetail : Form {
        private int _staffCode;
        private PdfUtility _pdfUtility = new();
        private CcPdfView[] _ccPdfViews = new CcPdfView[4];             // 4つの PdfViewer（経路図 / 自賠責 / 任意保険 / 通勤許可証）
        private MemoryStream[] _memoryStream = new MemoryStream[4];     // PdfViewer ごとに MemoryStream を保持する
        /*
         * Dao
         */
        private VoluntaryAutomobileInsuranceDao _voluntaryAutomobileInsuranceDao;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="staffCode"></param>
        public VoluntaryAutomobileInsuranceDetail(ConnectionVo connectionVo, int staffCode) {
            /*
             * Dao
             */
            _voluntaryAutomobileInsuranceDao = new(connectionVo);
            _staffCode = staffCode;
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
                "ToolStripMenuItemHelp"
            };
            this.CcMenuStrip1.ChangeEnable(listString);

            // 対象車両種別
            this.InitializeCcComboBoxVehicleType();

            // 保険会社名
            this.InitializeCcComboBoxCompanyName();

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
            for (int i = 0; i < 4; i++) {
                _ccPdfViews[i] = new();
                tabPages[i].Controls.Add(_ccPdfViews[i]);
                _ccPdfViews[i].ContextMenuStrip = this.CcContextMenuStrip1;                                                     // 共通の ContextMenuStrip を設定
            }

            // 表示対象のデータを画面へ反映
            this.PutSheetViewList(staffCode);
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
            /*
             * 新規 or 更新用の VO を作成
             */
            VoluntaryAutomobileInsuranceVo vo = new();
            vo.Id = Guid.NewGuid().ToString();                                                                                  // 一意な ID を生成
            vo.StaffCode = _staffCode;
            vo.VehicleType = this.CcComboBoxVehicleType.Text;
            vo.CompanyName = this.CcComboBoxCompanyName.Text;
            vo.StartDate = this.CcDateTimePickerStartDate.Value.ToString("yyyy-MM-dd");
            vo.EndDate = this.CcDateTimePickerEndDate.Value.ToString("yyyy-MM-dd");

            /*
             * PDF（byte[]）をセット
             */
            vo.Image1 = _memoryStream[0]?.ToArray() ?? Array.Empty<byte>();
            vo.Image2 = _memoryStream[1]?.ToArray() ?? Array.Empty<byte>();
            vo.Image3 = _memoryStream[2]?.ToArray() ?? Array.Empty<byte>();
            vo.Image4 = _memoryStream[3]?.ToArray() ?? Array.Empty<byte>();

            /*
             * INSERT or UPDATE の判定
             */
            if (_voluntaryAutomobileInsuranceDao.ExistsByStaffCode(vo.StaffCode)) {
                _voluntaryAutomobileInsuranceDao.UpdateOneVoluntaryAutomobileInsuranceVo(vo);
                this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "更新が完了しました。";
            } else {
                _voluntaryAutomobileInsuranceDao.InsertOneVoluntaryAutomobileInsuranceVo(vo);
                this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "新規登録が完了しました。";
            }
            // 二度押し防止/更新後は編集不可にする
            ((CcButton)sender).Enabled = false;
            this.CcComboBoxVehicleType.Enabled = false;
            this.CcComboBoxCompanyName.Enabled = false;
            this.CcDateTimePickerStartDate.Enabled = false;
            this.CcDateTimePickerEndDate.Enabled = false;
        }

        /// <summary>
        /// 画面へ PDF 等を表示する
        /// </summary>
        private void PutSheetViewList(int staffCode) {
            if (_voluntaryAutomobileInsuranceDao.ExistsByStaffCode(staffCode)) {
                this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "指定のデータは存在します。";
                VoluntaryAutomobileInsuranceVo vo = _voluntaryAutomobileInsuranceDao.SelectOneByStaffCode(staffCode);

                if (vo is null)
                    return;

                /*
                 * 画面項目へ反映
                 */
                this.CcComboBoxVehicleType.Text = vo.VehicleType;
                this.CcComboBoxCompanyName.Text = vo.CompanyName;

                if (DateTime.TryParse(vo.StartDate, out DateTime start))
                    this.CcDateTimePickerStartDate.Value = start;

                if (DateTime.TryParse(vo.EndDate, out DateTime end))
                    this.CcDateTimePickerEndDate.Value = end;

                /*
                 * PDF 表示（Image1〜4）
                 */
                ShowPdfIfExists(_ccPdfViews[0], vo.Image1, 0);
                ShowPdfIfExists(_ccPdfViews[1], vo.Image2, 1);
                ShowPdfIfExists(_ccPdfViews[2], vo.Image3, 2);
                ShowPdfIfExists(_ccPdfViews[3], vo.Image4, 3);
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
                for (int i = 0; i < 4; i++) {
                    ClearPdfViewer(_ccPdfViews[i]);
                }
            }
        }

        /// <summary>
        /// PDF が存在すれば表示する
        /// </summary>
        private void ShowPdfIfExists(CcPdfView ccPdfView, byte[] bytes, int index) {
            if (bytes is null || bytes.Length == 0) {
                ClearPdfViewer(ccPdfView);
                return;
            }

            _memoryStream[index]?.Dispose();
            _memoryStream[index] = new MemoryStream(bytes);

            ccPdfView.Unload();
            ccPdfView.SetPdfStream(_memoryStream[index]);
        }

        /// <summary>
        /// PdfViewer がどの ImageNo に対応しているかを返す
        /// </summary>
        private int GetImageNoFromViewer(CcPdfView viewer) {
            for (int i = 0; i < _ccPdfViews.Length; i++) {
                if (_ccPdfViews[i] == viewer) {
                    return i + 1;
                }
            }
            return 0;
        }

        /// <summary>
        /// 指定された PdfViewer に PDF（byte[]）を表示する
        /// </summary>
        private void ShowPdfToViewer(CcPdfView ccPdfView, byte[] pdfBytes) {
            int imageNo = GetImageNoFromViewer(ccPdfView);
            if (imageNo == 0)
                return;

            int index = imageNo - 1;

            _memoryStream[index]?.Dispose();
            _memoryStream[index] = new MemoryStream(pdfBytes);

            ccPdfView.Unload();
            ccPdfView.SetPdfStream(_memoryStream[index]);
        }

        /// <summary>
        /// 指定された PdfViewer をクリアする
        /// </summary>
        private void ClearPdfViewer(CcPdfView ccPdfView) {
            int imageNo = GetImageNoFromViewer(ccPdfView);
            if (imageNo == 0)
                return;

            int index = imageNo - 1;

            _memoryStream[index]?.Dispose();
            _memoryStream[index] = null;

            ccPdfView.Unload();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeCcComboBoxVehicleType() {
            this.CcComboBoxVehicleType.Items.Clear();
            foreach (string data in _voluntaryAutomobileInsuranceDao.SelectGroupVehicleType())
                this.CcComboBoxVehicleType.Items.Add(data);
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeCcComboBoxCompanyName() {
            this.CcComboBoxCompanyName.Items.Clear();
            foreach (string data in _voluntaryAutomobileInsuranceDao.SelectGroupCompanyName())
                this.CcComboBoxCompanyName.Items.Add(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
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
        private async void ContextMenuStripEx_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            if (sender is not ContextMenuStrip menu)
                return;

            if (menu.SourceControl is not CcPdfView ccPdfView)
                return;

            int imageNo = GetImageNoFromViewer(ccPdfView);
            if (imageNo == 0)
                return;

            switch (e.ClickedItem.Name) {
                case "ToolStripMenuItemOpen":
                    byte[] bytes = _pdfUtility.ConvertPdfToByte(menu);
                    if (bytes is null)
                        return;

                    this.ShowPdfToViewer(ccPdfView, bytes);
                    this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "PDF を表示しました。";
                    break;

                case "ToolStripMenuItemPaste": {
                        IDataObject data = Clipboard.GetDataObject();
                        if (data == null) {
                            MessageBox.Show("クリップボードが空です。");
                            break;
                        }

                        // ★ クリップボードに画像があるか？
                        if (data.GetDataPresent(DataFormats.Bitmap)) {
                            Bitmap bmp = (Bitmap)data.GetData(DataFormats.Bitmap);
                            if (bmp == null) {
                                MessageBox.Show("画像の取得に失敗しました。");
                                break;
                            }

                            // ★ Bitmap → PDF(byte[]) に変換（PdfUtility 使用）
                            byte[] pdfBytes = _pdfUtility.ConvertImageToPdfBytes(bmp);
                            if (pdfBytes == null || pdfBytes.Length == 0) {
                                MessageBox.Show("画像を PDF に変換できませんでした。");
                                break;
                            }

                            // ★ PdfiumViewer に表示（CcPdfView）
                            this.ShowPdfToViewer(ccPdfView, pdfBytes);

                            // ★ DB 保存用に MemoryStream を保持
                            int imageNo1 = GetImageNoFromViewer(ccPdfView);
                            int index = imageNo1 - 1;

                            //_memoryStream[index]?.Dispose();
                            _memoryStream[index] = new MemoryStream(pdfBytes);

                            this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "画像を PDF として貼り付けました。";
                            break;
                        }

                        MessageBox.Show("クリップボードに画像がありません。");
                        break;
                    }


                case "ToolStripMenuItemDelete":
                    this.ClearPdfViewer(ccPdfView);
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

        }
    }
}
