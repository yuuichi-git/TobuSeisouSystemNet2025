/*
 * 2026-04-07
 * Essential Studio® 7-Day License Key(取得日:2026-04-10)
 * "Ngo9BigBOggjHTQxAR8/V1JHaF5cWWdCekx3Q3xbf1x2ZFRHal5XTnJZUj0eQnxTdENjXX9XcndXQGRaV0JyXEleYA=="
 */
using CcControl;   // ★ Syncfusion PDF Viewer

using Common;

using Dao;

using Syncfusion.Windows.Forms.PdfViewer;

using Vo;

namespace VoluntaryAutomobileInsurance {
    public partial class VoluntaryAutomobileInsuranceDetail : Form {
        private int _staffCode;

        /*
         * PDF 読み込み・変換ユーティリティ
         */
        private PdfUtility _pdfUtility = new();

        /*
         * 4つの PdfViewer（経路図 / 自賠責 / 任意保険 / 通勤許可証）
         * TabPage と 1:1 対応
         */
        private PdfViewerControl[] _pdfViewerControl = new PdfViewerControl[4];

        /*
         * DAO（PDF の Insert/Update/Delete を担当）
         */
        private VoluntaryAutomobileInsuranceDao _voluntaryAutomobileInsuranceDao;

        // PdfViewer ごとに MemoryStream を保持する
        private MemoryStream[] _memoryStream = new MemoryStream[4];

        /// <summary>
        /// コンストラクター
        /// </summary>
        public VoluntaryAutomobileInsuranceDetail(ConnectionVo connectionVo, int staffCode) {
            _staffCode = staffCode;

            /*
             * Dao
             */
            _voluntaryAutomobileInsuranceDao = new(connectionVo);

            /*
             * InitializeControl
             */
            InitializeComponent();
            this.CcDateTimePickerStartDate.Value = DateTime.Now.AddDays(1);
            this.CcDateTimePickerEndDate.Value = DateTime.Now.AddYears(1);

            /*
             * PdfViewer の初期化（4つ）
             */
            for (int i = 0; i < 4; i++) {
                _pdfViewerControl[i] = new PdfViewerControl();

                // ▼ Syncfusion の「フォルダ（Open）」アイコンを無効化
                _pdfViewerControl[i].ShowToolBar = true;
                _pdfViewerControl[i].ToolbarSettings.OpenButton.IsEnabled = false;

                // ▼ 共通の右クリックメニュー
                _pdfViewerControl[i].ContextMenuStrip = CcContextMenuStrip1;

                // ▼ レイアウト
                _pdfViewerControl[i].Dock = DockStyle.Fill;
            }

            /*
             * TabPage に PdfViewer を配置
             */
            this.TabPage1.Controls.Add(_pdfViewerControl[0]);
            this.TabPage2.Controls.Add(_pdfViewerControl[1]);
            this.TabPage3.Controls.Add(_pdfViewerControl[2]);
            this.TabPage4.Controls.Add(_pdfViewerControl[3]);

            /*
             * 画面表示
             */
            this.PutSheetViewList(_staffCode);
            this.InitializeCcComboBoxVehicleType();
            this.InitializeCcComboBoxCompanyName();
        }

        private void CcButtonUpdate_Click(object sender, EventArgs e) {
            /*
             * 新規 or 更新用の VO を作成
             */
            VoluntaryAutomobileInsuranceVo vo = new();
            vo.Id = Guid.NewGuid().ToString();
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

        private async void ContextMenuStripEx_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            if (sender is not ContextMenuStrip menu)
                return;

            if (menu.SourceControl is not PdfViewerControl viewer)
                return;

            int imageNo = GetImageNoFromViewer(viewer);
            if (imageNo == 0)
                return;

            switch (e.ClickedItem.Name) {
                case "ToolStripMenuItemOpen":
                    byte[]? bytes = _pdfUtility.ConvertPdfToByte(menu);
                    if (bytes is null)
                        return;

                    this.ShowPdfToViewer(viewer, bytes);
                    this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "PDF を表示しました。";
                    break;

                case "ToolStripMenuItemDelete":
                    this.ClearPdfViewer(viewer);
                    this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "PDF を削除しました。";
                    break;
            }
        }

        /// <summary>
        /// PdfViewer がどの ImageNo に対応しているかを返す
        /// </summary>
        private int GetImageNoFromViewer(PdfViewerControl viewer) {
            for (int i = 0; i < _pdfViewerControl.Length; i++) {
                if (_pdfViewerControl[i] == viewer) {
                    return i + 1;
                }
            }
            return 0;
        }

        /// <summary>
        /// 画面へPDF等を表示する
        /// </summary>
        private void PutSheetViewList(int staffCode) {
            if (_voluntaryAutomobileInsuranceDao.ExistsByStaffCode(staffCode)) {
                this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "指定のデータは存在します。";

                VoluntaryAutomobileInsuranceVo vo =
                    _voluntaryAutomobileInsuranceDao.SelectOneByStaffCode(staffCode);

                if (vo is null)
                    return;

                this.CcComboBoxVehicleType.Text = vo.VehicleType;
                this.CcComboBoxCompanyName.Text = vo.CompanyName;

                if (DateTime.TryParse(vo.StartDate, out DateTime start))
                    this.CcDateTimePickerStartDate.Value = start;

                if (DateTime.TryParse(vo.EndDate, out DateTime end))
                    this.CcDateTimePickerEndDate.Value = end;

                ShowPdfIfExists(_pdfViewerControl[0], vo.Image1, 0);
                ShowPdfIfExists(_pdfViewerControl[1], vo.Image2, 1);
                ShowPdfIfExists(_pdfViewerControl[2], vo.Image3, 2);
                ShowPdfIfExists(_pdfViewerControl[3], vo.Image4, 3);
            } else {
                this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "指定のデータは存在しません。";

                this.CcComboBoxVehicleType.Text = string.Empty;
                this.CcComboBoxCompanyName.Text = string.Empty;
            }
        }

        /// <summary>
        /// PDF が存在すれば表示する
        /// </summary>
        private void ShowPdfIfExists(PdfViewerControl viewer, byte[] bytes, int index) {
            if (bytes is null || bytes.Length == 0) {
                ClearPdfViewer(viewer);
                return;
            }

            _memoryStream[index]?.Dispose();
            _memoryStream[index] = new MemoryStream(bytes);

            viewer.Unload();
            viewer.Load(_memoryStream[index]);
        }

        /// <summary>
        /// 指定された PdfViewer に PDF（byte[]）を表示する
        /// </summary>
        private void ShowPdfToViewer(PdfViewerControl viewer, byte[] pdfBytes) {
            int imageNo = GetImageNoFromViewer(viewer);
            if (imageNo == 0)
                return;

            int index = imageNo - 1;

            _memoryStream[index]?.Dispose();
            _memoryStream[index] = null;

            viewer.Unload();

            _memoryStream[index] = new MemoryStream(pdfBytes);

            viewer.Load(_memoryStream[index]);
        }

        /// <summary>
        /// 指定された PdfViewer をクリアする
        /// </summary>
        private void ClearPdfViewer(PdfViewerControl viewer) {
            int imageNo = GetImageNoFromViewer(viewer);
            if (imageNo == 0)
                return;

            int index = imageNo - 1;

            _memoryStream[index]?.Dispose();
            _memoryStream[index] = null;

            viewer.Unload();
        }

        private void InitializeCcComboBoxVehicleType() {
            this.CcComboBoxVehicleType.Items.Clear();
            foreach (string data in _voluntaryAutomobileInsuranceDao.SelectGroupVehicleType())
                this.CcComboBoxVehicleType.Items.Add(data);
        }

        private void InitializeCcComboBoxCompanyName() {
            this.CcComboBoxCompanyName.Items.Clear();
            foreach (string data in _voluntaryAutomobileInsuranceDao.SelectGroupCompanyName())
                this.CcComboBoxCompanyName.Items.Add(data);
        }
    }
}
