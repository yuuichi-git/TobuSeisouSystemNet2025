/*
 * 2025-05-10
 */
using System.Diagnostics;

using CcControl;

using Common;

using Dao;

using Vo;

namespace StatusOfResidence {
    public partial class StatusOfResidenceDetail : Form {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private PdfUtility _pdfUtility = new();
        private CcPdfView[] _ccPdfViews = new CcPdfView[3];                                                                                 // 4つの PdfViewer（在留カード表 / 在留カード裏 / 在留カード等番号失効情報照会）
        /*
         * Dao
         */
        private readonly StaffMasterDao _staffMasterDao;
        private readonly StatusOfResidenceMasterDao _statusOfResidenceMasterDao;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        public StatusOfResidenceDetail(ConnectionVo connectionVo) {
            /*
             * Dao
             */
            _staffMasterDao = new(connectionVo);
            _statusOfResidenceMasterDao = new(connectionVo);
            /*
             * InitializeControl
             */
            InitializeComponent();
            this.InitializeControl();
            /*
             * MenuStrip
             */
            List<string> listString = new() {"ToolStripMenuItemFile",
                                             "ToolStripMenuItemExit",
                                             "ToolStripMenuItemHelp"};
            this.CcMenuStrip1.ChangeEnable(listString);
            this.CcMenuStrip1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;

            this.CcComboBoxSelectName.Enabled = true;

            this.InitializeComboBoxExSelectName();

            this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = string.Empty;
        }

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="staffCode"></param>
        public StatusOfResidenceDetail(ConnectionVo connectionVo, int staffCode) {
            /*
             * Dao
             */
            _staffMasterDao = new(connectionVo);
            _statusOfResidenceMasterDao = new(connectionVo);
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
            this.CcMenuStrip1.ChangeEnable(listString);
            this.CcMenuStrip1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;

            this.CcComboBoxSelectName.Enabled = false;
            this.InitializeControl();
            this.SetControls(staffCode);

            this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch(((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemExit":                                                                                               // アプリケーションを終了する
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEx_Click(object sender, EventArgs e) {
            switch(((CcButton)sender).Name) {
                case "ButtonExUpdate":
                    DialogResult dialogResult = MessageBox.Show("データを更新します。よろしいですか？", "メッセージ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    switch(dialogResult) {
                        case DialogResult.OK:
                            try {
                                int.TryParse(this.CcLabelStaffCode.Text, out int staffCode);
                                if(_statusOfResidenceMasterDao.ExistenceHStatusOfResidenceMaster(staffCode)) {
                                    try {
                                        _statusOfResidenceMasterDao.UpdateOneStatusOfResidenceMaster(this.SetVo());
                                    } catch(Exception exception) {
                                        MessageBox.Show(exception.Message);
                                    }
                                    this.Close();
                                } else {
                                    try {
                                        _statusOfResidenceMasterDao.InsertOneStatusOfResidenceMaster(this.SetVo());
                                    } catch(Exception exception) {
                                        MessageBox.Show(exception.Message);
                                    }
                                    this.Close();
                                }
                            } catch(Exception exception) {
                                MessageBox.Show(exception.Message);
                            }
                            break;
                        case DialogResult.Cancel:
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// InitializeControl
        /// </summary>
        private void InitializeControl() {
            this.CcComboBoxSelectName.Enabled = false;
            this.CcComboBoxSelectName.Text = string.Empty;
            this.CcLabelStaffCode.Text = string.Empty;
            this.CcLabelNameKana.Text = string.Empty;
            this.CcTextBoxNameKana.Text = string.Empty;
            this.CcTextBoxName.Text = string.Empty;
            this.CcDateTimePickerBirthDate.SetEmpty();
            this.CcComboBoxGender.SelectedIndex = -1;
            this.CcComboBoxCompany.SelectedIndex = -1;
            this.CcTextBoxAddress.Text = string.Empty;
            this.ComboBoxExStatusOfResidence.Text = string.Empty;
            this.ComboBoxExWorkLimit.Text = string.Empty;
            this.DateTimePickerExPeriodDate.SetEmpty();
            this.DateTimePickerExDeadlineDate.SetEmpty();
            // PDF 表示エリア
            TabPage[] tabPages = new TabPage[3];
            tabPages[0] = this.TabPage1;
            tabPages[1] = this.TabPage2;
            tabPages[2] = this.TabPage3;

            // 4つの CcPdfView を生成して TabPage に配置
            for(int i = 0; i < 3; i++) {
                _ccPdfViews[i] = new();
                _ccPdfViews[i].Tag = i;
                _ccPdfViews[i].ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitWidth;                                                          // 幅に合わせて表示
                tabPages[i].Controls.Add(_ccPdfViews[i]);
                _ccPdfViews[i].ContextMenuStrip = this.CcContextMenuStrip1;                                                                 // 共通の ContextMenuStrip を設定
            }
            this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = string.Empty;
        }

        /// <summary>
        /// SetControls
        /// </summary>
        /// <param name="staffCode"></param>
        private void SetControls(int staffCode) {
            StaffMasterVo staffMasterVo = _staffMasterDao.SelectOneStaffMaster(staffCode);
            if(_statusOfResidenceMasterDao.ExistenceHStatusOfResidenceMaster(staffCode)) {
                StatusOfResidenceMasterVo statusOfResidenceMasterVo = _statusOfResidenceMasterDao.SelectOneStatusOfResidenceMasterP(staffCode);
                CcComboBoxSelectName.Text = string.Empty;
                CcLabelStaffCode.Text = staffMasterVo.StaffCode.ToString("#####");
                CcLabelNameKana.Text = staffMasterVo.DisplayName;
                CcTextBoxNameKana.Text = statusOfResidenceMasterVo.StaffNameKana;
                CcTextBoxName.Text = statusOfResidenceMasterVo.StaffName;
                CcDateTimePickerBirthDate.SetValue(statusOfResidenceMasterVo.BirthDate);
                CcComboBoxGender.Text = statusOfResidenceMasterVo.Gender;
                CcComboBoxCompany.Text = statusOfResidenceMasterVo.Nationality;
                CcTextBoxAddress.Text = statusOfResidenceMasterVo.Address;
                ComboBoxExStatusOfResidence.Text = statusOfResidenceMasterVo.StatusOfResidence;
                ComboBoxExWorkLimit.Text = statusOfResidenceMasterVo.WorkLimit;
                DateTimePickerExPeriodDate.SetValue(statusOfResidenceMasterVo.PeriodDate);
                DateTimePickerExDeadlineDate.SetValue(statusOfResidenceMasterVo.DeadlineDate);
                /*
                 * PDF 表示（Image1〜4）
                 */
                _ccPdfViews[0].SetPdfBytes(statusOfResidenceMasterVo.PictureHead);
                _ccPdfViews[1].SetPdfBytes(statusOfResidenceMasterVo.PictureTail);
                _ccPdfViews[2].SetPdfBytes(statusOfResidenceMasterVo.PictureVerify);
            } else {
                /*
                 * PDF クリア
                 */
                for(int i = 0; i < 3; i++) {
                    _ccPdfViews[i].Clear();
                }
            }

        }

        /// <summary>
        /// SetVo
        /// </summary>
        /// <returns></returns>
        private StatusOfResidenceMasterVo SetVo() {
            StatusOfResidenceMasterVo statusOfResidenceMasterVo = new();
            statusOfResidenceMasterVo.StaffCode = int.Parse(CcLabelStaffCode.Text);
            statusOfResidenceMasterVo.StaffNameKana = CcTextBoxNameKana.Text;
            statusOfResidenceMasterVo.StaffName = CcTextBoxName.Text;
            statusOfResidenceMasterVo.BirthDate = CcDateTimePickerBirthDate.GetValue();
            statusOfResidenceMasterVo.Gender = CcComboBoxGender.Text;
            statusOfResidenceMasterVo.Nationality = CcComboBoxCompany.Text;
            statusOfResidenceMasterVo.Address = CcTextBoxAddress.Text;
            statusOfResidenceMasterVo.StatusOfResidence = ComboBoxExStatusOfResidence.Text;
            statusOfResidenceMasterVo.WorkLimit = ComboBoxExWorkLimit.Text;
            statusOfResidenceMasterVo.PeriodDate = DateTimePickerExPeriodDate.GetValue();
            statusOfResidenceMasterVo.DeadlineDate = DateTimePickerExDeadlineDate.GetValue();
            statusOfResidenceMasterVo.PictureHead = _ccPdfViews[0].MemoryStream?.ToArray() ?? Array.Empty<byte>();                         // 在留カード表
            statusOfResidenceMasterVo.PictureTail = _ccPdfViews[1].MemoryStream?.ToArray() ?? Array.Empty<byte>();                         // 在留カード裏
            statusOfResidenceMasterVo.PictureVerify = _ccPdfViews[2].MemoryStream?.ToArray() ?? Array.Empty<byte>();                       // 在留カード等番号失効情報照会
            statusOfResidenceMasterVo.InsertPcName = string.Empty;
            statusOfResidenceMasterVo.InsertYmdHms = _defaultDateTime;
            statusOfResidenceMasterVo.UpdatePcName = string.Empty;
            statusOfResidenceMasterVo.UpdateYmdHms = _defaultDateTime;
            statusOfResidenceMasterVo.DeletePcName = string.Empty;
            statusOfResidenceMasterVo.DeleteYmdHms = _defaultDateTime;
            statusOfResidenceMasterVo.DeleteFlag = false;
            return statusOfResidenceMasterVo;
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeComboBoxExSelectName() {
            this.CcComboBoxSelectName.Items.Clear();
            List<ComboBoxExSelectNameVo> listComboBoxSelectNameVo = new();
            foreach(StaffMasterVo staffMasterVo in _staffMasterDao.SelectAllStaffMaster(new List<int> { 10, 11, 12, 14, 15, 22, 99 },      // 役員・社員・アルバイト・嘱託雇用契約社員・パートタイマー・労供・指定なし
                                                                                        new List<int> { 20, 21, 22, 23, 99 },              // 労供長期・労供短期・指定なし
                                                                                        new List<int> { 10, 11, 12, 13, 20, 99 },          // 運転手・作業員・自転車駐輪場・リサイクルセンター・事務員・指定なし
                                                                                        false).FindAll(x => x.RetirementFlag == false).OrderBy(x => x.NameKana)) {
                this.CcComboBoxSelectName.Items.Add(new ComboBoxExSelectNameVo(staffMasterVo.Name, staffMasterVo));
            }
            this.CcComboBoxSelectName.DisplayMember = "Name";
            // ここでイベント追加しないと初期化で発火しちゃうよ
            this.CcComboBoxSelectName.SelectedIndexChanged += new EventHandler(ComboBoxExSelectName_SelectedIndexChanged);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxExSelectName_SelectedIndexChanged(object sender, EventArgs e) {
            StaffMasterVo staffMasterVo = ((ComboBoxExSelectNameVo)((CcComboBox)sender).SelectedItem).StaffMasterVo;
            /*
             * StaffLedgerVoの値をControlにセットする
             */
            this.CcLabelStaffCode.Text = staffMasterVo.StaffCode.ToString();
            this.CcLabelNameKana.Text = staffMasterVo.NameKana;
            this.CcTextBoxNameKana.Text = staffMasterVo.NameKana;
            this.CcTextBoxName.Text = staffMasterVo.Name;
            this.CcDateTimePickerBirthDate.SetValueJp(staffMasterVo.BirthDate);
            this.CcComboBoxGender.Text = staffMasterVo.Gender;
            this.CcTextBoxAddress.Text = staffMasterVo.CurrentAddress;
        }

        /// <summary>
        /// インナークラス
        /// </summary>
        private class ComboBoxExSelectNameVo {
            private string _name;
            private StaffMasterVo _staffMasterVo;

            // プロパティをコンストラクタでセット
            public ComboBoxExSelectNameVo(string name, StaffMasterVo staffMasterVo) {
                _name = name;
                _staffMasterVo = staffMasterVo;
            }

            public string Name {
                get => _name;
                set => _name = value;
            }
            public StaffMasterVo StaffMasterVo {
                get => _staffMasterVo;
                set => _staffMasterVo = value;
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
        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            var url = "https://lapse-immi.moj.go.jp/html/top.html";
            System.Diagnostics.Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusOfResidenceDetail_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
