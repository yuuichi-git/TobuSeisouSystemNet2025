/*
 * 2026-08-31
 */
using CcControl;

using Common;

using Dao;

using Vo;

namespace RiskAssessment {
    public partial class RiskAssessmentDetail : Form {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly int _fiscalYear;
        private readonly int _staffCode;
        private PdfUtility _pdfUtility = new();
        /// <summary>
        /// 0→1回目　1→2回目　2→3回目
        /// </summary>
        private string[] _signNumber = ["１回目", "２回目", "３回目"];
        /*
         * Control用の配列を確保
         */
        private CcCheckBox[] _arrayCcCheckBox = new CcCheckBox[3];
        private CcDateTime[] _arrayCcDateTimePicker = new CcDateTime[3];
        private CcComboBox[] _arrayCcComboBox = new CcComboBox[3];
        private CcTextBox[] _arrayCcTextBox = new CcTextBox[3];
        private CcPdfView[] _ccPdfViews = new CcPdfView[3];             // 3つの PdfViewer（第一回目 / 第二回目 / 第三回目）
        /*
         * Dao
         */
        private StaffMasterDao _staffMasterDao;
        private RiskAssessmentSeminarDao _riskAssessmentSeminarDao;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        /// <param name="id"></param>
        public RiskAssessmentDetail(ConnectionVo connectionVo, Screen screen, int fiscalYear, int staffCode) {
            _fiscalYear = fiscalYear;
            _staffCode = staffCode;
            /*
             * Dao
             */
            _staffMasterDao = new(connectionVo);
            _riskAssessmentSeminarDao = new(connectionVo);
            /*
             * InitializeControls
             */
            InitializeComponent();
            /*
             * 配列にControlを割り当て
             */
            _arrayCcCheckBox[0] = this.CcCheckBox1;
            _arrayCcCheckBox[1] = this.CcCheckBox2;
            _arrayCcCheckBox[2] = this.CcCheckBox3;
            /*
             * 配列にControlを割り当て
             * 指導実施日
             */
            _arrayCcDateTimePicker[0] = this.CcDateTimePicker1;
            _arrayCcDateTimePicker[1] = this.CcDateTimePicker2;
            _arrayCcDateTimePicker[2] = this.CcDateTimePicker3;
            /*
             * 配列にControlを割り当て
             * サイン№
             */
            _arrayCcComboBox[0] = this.CcComboBox1;
            _arrayCcComboBox[1] = this.CcComboBox2;
            _arrayCcComboBox[2] = this.CcComboBox3;
            /*
             * 配列にControlを割り当て
             * メモ
             */
            _arrayCcTextBox[0] = this.CcTextBox1;
            _arrayCcTextBox[1] = this.CcTextBox2;
            _arrayCcTextBox[2] = this.CcTextBox3;
            /*
             * MenuStrip
             */
            List<string> listString = new() {"ToolStripMenuItemFile",
                                             "ToolStripMenuItemExit",
                                             "ToolStripMenuItemHelp"};
            this.CcMenuStrip1.ChangeEnable(listString);
            this.CcMenuStrip1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;

            this.InitializeControl();
            this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = string.Empty;

            this.SetControls(_riskAssessmentSeminarDao.SelectRiskAssessmentSeminar(_fiscalYear, _staffCode));
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
                    for(int i = 0; i < 3; i++) {
                        /*
                         * 変更前のVoを保持
                         */
                        RiskAssessmentSeminarVo beforeRiskAssessmentSeminarVo = (RiskAssessmentSeminarVo)_arrayCcTextBox[i].Tag;
                        if(_arrayCcCheckBox[i].Checked) {
                            /*
                             * Controlの値をRiskAssessmentSeminarVoに代入
                             */
                            RiskAssessmentSeminarVo afterRiskAssessmentSeminarVo = new();
                            afterRiskAssessmentSeminarVo.Id = Guid.NewGuid().ToString();                                                                                        // 変更前のVoが存在すればそのIdを使用、存在しなければ新しいIdを生成
                            afterRiskAssessmentSeminarVo.StudentsDate = _arrayCcDateTimePicker[i].GetValue();
                            afterRiskAssessmentSeminarVo.StudentsCode = Convert.ToInt32(_arrayCcCheckBox[i].Tag);
                            afterRiskAssessmentSeminarVo.StudentsFlag = _arrayCcCheckBox[i].Checked;
                            afterRiskAssessmentSeminarVo.StaffCode = _staffCode;
                            afterRiskAssessmentSeminarVo.StaffSign = _ccPdfViews[_arrayCcComboBox[i].SelectedIndex].MemoryStream?.ToArray() ?? Array.Empty<byte>();             // StaffSign は MemoryStream から取得する
                            afterRiskAssessmentSeminarVo.SignNumber = _arrayCcComboBox[i].SelectedIndex;
                            afterRiskAssessmentSeminarVo.Memo = _arrayCcTextBox[i].Text;
                            afterRiskAssessmentSeminarVo.InsertPcName = Environment.MachineName;
                            afterRiskAssessmentSeminarVo.InsertYmdHms = DateTime.Now;
                            afterRiskAssessmentSeminarVo.UpdatePcName = string.Empty;
                            afterRiskAssessmentSeminarVo.UpdateYmdHms = _defaultDateTime;
                            afterRiskAssessmentSeminarVo.DeletePcName = string.Empty;
                            afterRiskAssessmentSeminarVo.DeleteYmdHms = _defaultDateTime;
                            afterRiskAssessmentSeminarVo.DeleteFlag = false;
                            /*
                             * レコードが存在すればUPDATEする。
                             * Tagに退避させてあるVoを渡す。変更前の値でSQLを発行しないとダメだよ！
                             */
                            if(beforeRiskAssessmentSeminarVo is not null && _riskAssessmentSeminarDao.ExistenceRiskAssessmentSeminar(beforeRiskAssessmentSeminarVo.Id)) {
                                try {
                                    _riskAssessmentSeminarDao.UpdateOneRiskAssessmentSeminar(beforeRiskAssessmentSeminarVo, afterRiskAssessmentSeminarVo);
                                } catch(Exception exception) {
                                    MessageBox.Show(exception.Message);
                                }
                            } else {
                                try {
                                    _riskAssessmentSeminarDao.InsertOneRiskAssessmentSeminar(afterRiskAssessmentSeminarVo);
                                } catch(Exception exception) {
                                    MessageBox.Show(exception.Message);
                                }
                            }
                        } else {
                            /*
                             * 最初にセットされた値(Vo)はTagに代入してある
                             * _arrayTextBox[i].Tag = riskAssessmentSeminarVo;
                             */
                            if(beforeRiskAssessmentSeminarVo is not null) {
                                try {
                                    _riskAssessmentSeminarDao.DeleteOneRiskAssessmentSeminar(beforeRiskAssessmentSeminarVo.Id);
                                } catch(Exception exception) {
                                    MessageBox.Show(exception.Message);
                                }
                            }
                        }
                    }
                    this.Close();
                    break;
                case DialogResult.Cancel:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch(((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemExit":                                                                                                   // アプリケーションを終了する
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
        /// コントロールに値を設定
        /// </summary>
        /// <param name="listRiskAssessmentSeminarVo"></param>
        private void SetControls(List<RiskAssessmentSeminarVo> listRiskAssessmentSeminarVo) {
            this.CcLabelStaffCode.Text = Convert.ToString(_staffCode);                                                                          // StaffCode
            this.CcLabelName.Text = _staffMasterDao.SelectOneStaffMaster(_staffCode).Name;                                                      // Name
            this.CcDateTimePickerBase.SetToday();                                                                                               // 基準日
            this.CcComboBoxBase.DisplayClear();                                                                                                 // 基準日ComboBoxを初期化

            if(listRiskAssessmentSeminarVo is null || listRiskAssessmentSeminarVo.Count == 0) {
                this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "対象のレコードが存在しません。";
                return;
            }

            /*
             * CheckBox等の処理
             */
            for(int i = 0; i < 3; i++) {
                RiskAssessmentSeminarVo riskAssessmentSeminarVo = listRiskAssessmentSeminarVo.Find(x => x.StudentsCode == i);
                /*
                 * _arrayTextBoxのTagにRiskAssessmentSeminarVoを格納
                 * Recordを削除するさいに必要な情報になる
                 */
                _arrayCcTextBox[i].Tag = riskAssessmentSeminarVo;                                                                               // RiskAssessmentSeminarVoをTagに格納する

                if(riskAssessmentSeminarVo is not null) {                                                                                       // RiskAssessmentSeminarVoが存在する場合、Controlに値をセットする
                    _arrayCcCheckBox[i].Checked = true;
                    _arrayCcDateTimePicker[i].SetValue(riskAssessmentSeminarVo.StudentsDate);
                    _arrayCcComboBox[i].Text = _signNumber[riskAssessmentSeminarVo.SignNumber];
                    _arrayCcTextBox[i].Text = riskAssessmentSeminarVo.Memo;

                } else {                                                                                                                        // RiskAssessmentSeminarVoが存在しない場合、Controlを初期化する
                    _arrayCcCheckBox[i].Checked = false;
                    _arrayCcDateTimePicker[i].SetEmpty();
                    _arrayCcComboBox[i].Text = string.Empty;
                    _arrayCcTextBox[i].Text = string.Empty;
                }
            }

            /*
             * 1回目〜3回目のPDF表示処理
             */
            listRiskAssessmentSeminarVo = listRiskAssessmentSeminarVo.DistinctBy(c => c.SignNumber).ToList();                                   // SignNumberで重複するVoを除外する
            foreach(RiskAssessmentSeminarVo riskAssessmentSeminarVo in listRiskAssessmentSeminarVo.OrderBy(x => x.SignNumber)) {                // SignNumberで昇順にソートする
                int index = riskAssessmentSeminarVo.SignNumber;
                /*
                 * SignNumber が 0〜3 の範囲外ならスキップ
                 */
                if(index < 0 || index >= _ccPdfViews.Length)
                    continue;
                /*
                 * PDF 表示（SignNumber が示すビューへ）
                 */
                _ccPdfViews[index].SetPdfBytes(riskAssessmentSeminarVo.StaffSign);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CcCheckBox_CheckedChanged(object sender, EventArgs e) {
            if(((CcCheckBox)sender).Checked) {
                _arrayCcDateTimePicker[Convert.ToInt32(((CcCheckBox)sender).Tag)].Enabled = true;
                /*
                 * 指導実施日が空白の場合、値を入力する
                 */
                if(_arrayCcDateTimePicker[Convert.ToInt32(((CcCheckBox)sender).Tag)].CustomFormat == " ")
                    _arrayCcDateTimePicker[Convert.ToInt32(((CcCheckBox)sender).Tag)].SetValue(CcDateTimePickerBase.GetValue());

                _arrayCcComboBox[Convert.ToInt32(((CcCheckBox)sender).Tag)].Enabled = true;
                _arrayCcComboBox[Convert.ToInt32(((CcCheckBox)sender).Tag)].SelectedIndex = this.CcComboBoxBase.SelectedIndex;

                _arrayCcTextBox[Convert.ToInt32(((CcCheckBox)sender).Tag)].Enabled = true;
            } else {
                if(_arrayCcDateTimePicker[Convert.ToInt32(((CcCheckBox)sender).Tag)].CustomFormat != " " || _arrayCcComboBox[Convert.ToInt32(((CcCheckBox)sender).Tag)].Text != "" || _arrayCcTextBox[Convert.ToInt32(((CcCheckBox)sender).Tag)].Text != "") {
                    DialogResult dialogResult = MessageBox.Show("登録されているデータを削除してもよろしいですか？", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    switch(dialogResult) {
                        case DialogResult.OK:
                            _arrayCcDateTimePicker[Convert.ToInt32(((CcCheckBox)sender).Tag)].Enabled = false;
                            _arrayCcDateTimePicker[Convert.ToInt32(((CcCheckBox)sender).Tag)].SetClear();

                            _arrayCcComboBox[Convert.ToInt32(((CcCheckBox)sender).Tag)].Enabled = false;
                            _arrayCcComboBox[Convert.ToInt32(((CcCheckBox)sender).Tag)].SelectedIndex = -1;

                            _arrayCcTextBox[Convert.ToInt32(((CcCheckBox)sender).Tag)].Enabled = false;
                            _arrayCcTextBox[Convert.ToInt32(((CcCheckBox)sender).Tag)].SetEmpty();
                            break;
                        case DialogResult.Cancel:
                            // 処理を戻す意味で、フラグを反転させる
                            ((CcCheckBox)sender).Checked = !((CcCheckBox)sender).Checked;
                            break;
                    }
                } else {
                    _arrayCcDateTimePicker[Convert.ToInt32(((CcCheckBox)sender).Tag)].Enabled = false;
                    _arrayCcDateTimePicker[Convert.ToInt32(((CcCheckBox)sender).Tag)].SetClear();

                    _arrayCcComboBox[Convert.ToInt32(((CcCheckBox)sender).Tag)].Enabled = false;
                    _arrayCcComboBox[Convert.ToInt32(((CcCheckBox)sender).Tag)].SelectedIndex = -1;

                    _arrayCcTextBox[Convert.ToInt32(((CcCheckBox)sender).Tag)].Enabled = false;
                    _arrayCcTextBox[Convert.ToInt32(((CcCheckBox)sender).Tag)].SetEmpty();
                }
            }
        }

        /// <summary>
        /// コントロールを初期化
        /// </summary>
        private void InitializeControl() {
            this.CcLabelStaffCode.Text = string.Empty;
            this.CcLabelName.Text = string.Empty;

            this.CcDateTimePickerBase.SetToday();
            this.CcComboBoxBase.SelectedIndex = 0;
            for(int i = 0; i < 3; i++) {
                _arrayCcCheckBox[i].Checked = false;

                _arrayCcDateTimePicker[i].Enabled = false;
                _arrayCcDateTimePicker[i].SetEmpty();

                _arrayCcComboBox[i].Enabled = false;
                _arrayCcComboBox[i].SelectedIndex = -1;

                _arrayCcTextBox[i].Enabled = false;
                _arrayCcTextBox[i].Text = string.Empty;
            }
            // PDF 表示エリア
            TabPage[] tabPages = new TabPage[4];
            tabPages[0] = this.TabPage1;
            tabPages[1] = this.TabPage2;
            tabPages[2] = this.TabPage3;

            // 3つの CcPdfView を生成して TabPage に配置
            for(int i = 0; i < 3; i++) {
                _ccPdfViews[i] = new();
                _ccPdfViews[i].Tag = i;
                tabPages[i].Controls.Add(_ccPdfViews[i]);
                _ccPdfViews[i].ContextMenuStrip = this.CcContextMenuStrip1;                                                     // 共通の ContextMenuStrip を設定
            }
        }

        /// <summary>
        /// 指定された PdfViewer に PDF（byte[]）を表示する
        /// </summary>
        /// <param name="ccPdfView">PdfViewer のインスタンス</param>
        /// <param name="pdfBytes">PDF のバイト配列</param>
        private void ShowPdfToViewer(CcPdfView ccPdfView, byte[] pdfBytes) {
            ccPdfView.SetPdfStream(new MemoryStream(pdfBytes));
        }
    }
}
