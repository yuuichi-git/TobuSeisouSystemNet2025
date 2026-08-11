/*
 * 2025-05-17
 */
using CcControl;

using Common;

using Dao;

using Vo;

namespace LegalTwelveItem {
    public partial class LegalTwelveItemDetail : Form {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly Screen _screen;
        private readonly int _fiscalYear;
        private readonly int _staffCode;
        private PdfUtility _pdfUtility = new();
        private CcPdfView[] _ccPdfViews = new CcPdfView[3];             // 3つの PdfViewer（第一回目 / 第二回目 / 第三回目）
        /*
         * Dao
         */
        private readonly StaffMasterDao _staffMasterDao;
        private readonly LegalTwelveItemDao _legalTwelveItemDao;
        /// <summary>
        /// 0→1回目　1→2回目　2→3回目
        /// </summary>
        private string[] _signNumber = ["１回目", "２回目", "３回目"];
        /*
         * Control用の配列を確保
         */
        private CcCheckBox[] _arrayCcCheckBox = new CcCheckBox[12];
        private CcDateTime[] _arrayCcDateTimePicker = new CcDateTime[12];
        private CcComboBox[] _arrayCcComboBox = new CcComboBox[12];
        private CcTextBox[] _arrayCcTextBox = new CcTextBox[12];


        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        /// <param name="fiscalYear"></param>
        /// <param name="staffCode"></param>
        public LegalTwelveItemDetail(ConnectionVo connectionVo, Screen screen, int fiscalYear, int staffCode) {
            _screen = screen;
            _fiscalYear = fiscalYear;
            _staffCode = staffCode;
            /*
             * Dao
             */
            _staffMasterDao = new(connectionVo);
            _legalTwelveItemDao = new(connectionVo);
            /*
             * InitializeControl
             */
            InitializeComponent();
            /*
             * 配列にControlを割り当て
             * １２項目名
             */
            _arrayCcCheckBox[0] = this.CheckBoxEx1;
            _arrayCcCheckBox[1] = this.CheckBoxEx2;
            _arrayCcCheckBox[2] = this.CheckBoxEx3;
            _arrayCcCheckBox[3] = this.CheckBoxEx4;
            _arrayCcCheckBox[4] = this.CheckBoxEx5;
            _arrayCcCheckBox[5] = this.CheckBoxEx6;
            _arrayCcCheckBox[6] = this.CheckBoxEx7;
            _arrayCcCheckBox[7] = this.CheckBoxEx8;
            _arrayCcCheckBox[8] = this.CheckBoxEx9;
            _arrayCcCheckBox[9] = this.CheckBoxEx10;
            _arrayCcCheckBox[10] = this.CheckBoxEx11;
            _arrayCcCheckBox[11] = this.CheckBoxEx12;
            /*
             * 配列にControlを割り当て
             * 指導実施日
             */
            _arrayCcDateTimePicker[0] = this.DateTimePickerEx1;
            _arrayCcDateTimePicker[1] = this.DateTimePickerEx2;
            _arrayCcDateTimePicker[2] = this.DateTimePickerEx3;
            _arrayCcDateTimePicker[3] = this.DateTimePickerEx4;
            _arrayCcDateTimePicker[4] = this.DateTimePickerEx5;
            _arrayCcDateTimePicker[5] = this.DateTimePickerEx6;
            _arrayCcDateTimePicker[6] = this.DateTimePickerEx7;
            _arrayCcDateTimePicker[7] = this.DateTimePickerEx8;
            _arrayCcDateTimePicker[8] = this.DateTimePickerEx9;
            _arrayCcDateTimePicker[9] = this.DateTimePickerEx10;
            _arrayCcDateTimePicker[10] = this.DateTimePickerEx11;
            _arrayCcDateTimePicker[11] = this.DateTimePickerEx12;
            /*
             * 配列にControlを割り当て
             * サイン№
             */
            _arrayCcComboBox[0] = this.ComboBoxEx1;
            _arrayCcComboBox[1] = this.ComboBoxEx2;
            _arrayCcComboBox[2] = this.ComboBoxEx3;
            _arrayCcComboBox[3] = this.ComboBoxEx4;
            _arrayCcComboBox[4] = this.ComboBoxEx5;
            _arrayCcComboBox[5] = this.ComboBoxEx6;
            _arrayCcComboBox[6] = this.ComboBoxEx7;
            _arrayCcComboBox[7] = this.ComboBoxEx8;
            _arrayCcComboBox[8] = this.ComboBoxEx9;
            _arrayCcComboBox[9] = this.ComboBoxEx10;
            _arrayCcComboBox[10] = this.ComboBoxEx11;
            _arrayCcComboBox[11] = this.ComboBoxEx12;
            /*
             * 配列にControlを割り当て
             * メモ
             */
            _arrayCcTextBox[0] = this.TextBoxEx1;
            _arrayCcTextBox[1] = this.TextBoxEx2;
            _arrayCcTextBox[2] = this.TextBoxEx3;
            _arrayCcTextBox[3] = this.TextBoxEx4;
            _arrayCcTextBox[4] = this.TextBoxEx5;
            _arrayCcTextBox[5] = this.TextBoxEx6;
            _arrayCcTextBox[6] = this.TextBoxEx7;
            _arrayCcTextBox[7] = this.TextBoxEx8;
            _arrayCcTextBox[8] = this.TextBoxEx9;
            _arrayCcTextBox[9] = this.TextBoxEx10;
            _arrayCcTextBox[10] = this.TextBoxEx11;
            _arrayCcTextBox[11] = this.TextBoxEx12;
            /*
             * MenuStrip
             */
            List<string> listString = new() {"ToolStripMenuItemFile",
                                             "ToolStripMenuItemExit",
                                             "ToolStripMenuItemHelp"};
            this.MenuStripEx1.ChangeEnable(listString);
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;

            this.InitializeControl();

            this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = string.Empty;

            this.SetControls(_legalTwelveItemDao.SelectLegalTwelveItemVo(_fiscalYear, staffCode));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExUpdate_Click(object sender, EventArgs e) {
            DialogResult dialogResult = MessageBox.Show("登録します。よろしいですか？", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            switch(dialogResult) {
                case DialogResult.OK:
                    for(int i = 0; i < 12; i++) {
                        if(_arrayCcCheckBox[i].Checked) {
                            /*
                             * Controlの値をLegalTwelveItemVoに代入
                             */
                            LegalTwelveItemVo legalTwelveItemVo = new();
                            legalTwelveItemVo.StudentsDate = _arrayCcDateTimePicker[i].GetValue();
                            legalTwelveItemVo.StudentsCode = Convert.ToInt32(_arrayCcCheckBox[i].Tag);
                            legalTwelveItemVo.StudentsFlag = _arrayCcCheckBox[i].Checked;
                            legalTwelveItemVo.StaffCode = _staffCode;
                            legalTwelveItemVo.StaffSign = _ccPdfViews[_arrayCcComboBox[i].SelectedIndex].MemoryStream?.ToArray() ?? Array.Empty<byte>();               // StaffSign は MemoryStream から取得する
                            legalTwelveItemVo.SignNumber = _arrayCcComboBox[i].SelectedIndex;
                            legalTwelveItemVo.Memo = _arrayCcTextBox[i].Text;
                            legalTwelveItemVo.InsertPcName = Environment.MachineName;
                            legalTwelveItemVo.InsertYmdHms = DateTime.Now;
                            legalTwelveItemVo.UpdatePcName = string.Empty;
                            legalTwelveItemVo.UpdateYmdHms = _defaultDateTime;
                            legalTwelveItemVo.DeletePcName = string.Empty;
                            legalTwelveItemVo.DeleteYmdHms = _defaultDateTime;
                            legalTwelveItemVo.DeleteFlag = false;
                            /*
                             * レコードが存在すればUPDATEする。
                             * Tagに退避させてあるVoを渡す。変更前の値でSQLを発行しないとダメだよ！
                             */
                            if((LegalTwelveItemVo)_arrayCcTextBox[i].Tag is not null && _legalTwelveItemDao.ExistenceLegalTwelveItem((LegalTwelveItemVo)_arrayCcTextBox[i].Tag)) {
                                try {
                                    _legalTwelveItemDao.UpdateOneLegalTwelveItem((LegalTwelveItemVo)_arrayCcTextBox[i].Tag, legalTwelveItemVo);
                                } catch(Exception exception) {
                                    MessageBox.Show(exception.Message);
                                }
                            } else {
                                try {
                                    _legalTwelveItemDao.InsertOneLegalTwelveItem(legalTwelveItemVo);
                                } catch(Exception exception) {
                                    MessageBox.Show(exception.Message);
                                }
                            }
                        } else {
                            /*
                             * 最初にセットされた値(Vo)はTagに代入してある
                             * _arrayTextBox[i].Tag = legalTwelveItemVo;
                             */
                            if((LegalTwelveItemVo)_arrayCcTextBox[i].Tag is not null) {
                                try {
                                    _legalTwelveItemDao.DeleteOneLegalTwelveItemVo((LegalTwelveItemVo)_arrayCcTextBox[i].Tag);
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
        /// <param name="listLegalTwelveItemVo"></param>
        private void SetControls(List<LegalTwelveItemVo> listLegalTwelveItemVo) {
            this.LabelExStaffCode.Text = Convert.ToString(_staffCode);                                                          // StaffCode
            this.LabelExName.Text = _staffMasterDao.SelectOneStaffMaster(_staffCode).Name;                                      // Name
            /*
             * CheckBox等の処理
             */
            for(int i = 0; i < 12; i++) {
                LegalTwelveItemVo legalTwelveItemVo = listLegalTwelveItemVo.Find(x => x.StudentsCode == i);
                /*
                 * _arrayTextBoxのTagにLegalTwelveItemVoを格納
                 * Recordを削除するさいに必要な情報になる
                 */
                _arrayCcTextBox[i].Tag = legalTwelveItemVo;                                                                     // LegalTwelveItemVoをTagに格納する

                if(legalTwelveItemVo is not null) {                                                                             // LegalTwelveItemVoが存在する場合、Controlに値をセットする
                    _arrayCcCheckBox[i].Checked = true;
                    _arrayCcDateTimePicker[i].SetValue(legalTwelveItemVo.StudentsDate);
                    _arrayCcComboBox[i].Text = _signNumber[legalTwelveItemVo.SignNumber];
                    _arrayCcTextBox[i].Text = legalTwelveItemVo.Memo;
                } else {                                                                                                        // LegalTwelveItemVoが存在しない場合、Controlを初期化する
                    _arrayCcCheckBox[i].Checked = false;
                    _arrayCcDateTimePicker[i].SetEmpty();
                    _arrayCcComboBox[i].Text = string.Empty;
                    _arrayCcTextBox[i].Text = string.Empty;
                }
            }

            /*
             * 1回目〜3回目のPDF表示処理
             */
            listLegalTwelveItemVo = listLegalTwelveItemVo.DistinctBy(c => c.SignNumber).ToList();                               // SignNumberで重複するVoを除外する
            foreach(LegalTwelveItemVo legalTwelveItemVo in listLegalTwelveItemVo.OrderBy(x => x.SignNumber)) {                  // SignNumberで昇順にソートする
                int index = legalTwelveItemVo.SignNumber;
                /*
                 * SignNumber が 0〜3 の範囲外ならスキップ
                 */
                if(index < 0 || index >= _ccPdfViews.Length)
                    continue;
                /*
                 * PDF 表示（SignNumber が示すビューへ）
                 */
                _ccPdfViews[index].SetPdfBytes(legalTwelveItemVo.StaffSign);
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

                        ccPdfView.MemoryStream = new MemoryStream(pdfBytes);

                        ccPdfView.Clear();
                        ccPdfView.SetPdfStream(ccPdfView.MemoryStream);

                        this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "画像を PDF として貼り付けました。";
                        break;
                    }

                    MessageBox.Show("クリップボードに画像がありません。");
                    break;
                }

                case "ToolStripMenuItemDelete":
                    // PdfViewer の PDF を破棄
                    ccPdfView.Clear();
                    // ★ MemoryStream も破棄（Dispose は絶対にしない）
                    ccPdfView.MemoryStream = null;
                    this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "PDF を削除しました。";
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxEx_CheckedChanged(object sender, EventArgs e) {
            if(((CcCheckBox)sender).Checked) {
                _arrayCcDateTimePicker[Convert.ToInt32(((CcCheckBox)sender).Tag)].Enabled = true;
                /*
                 * 指導実施日が空白の場合、値を入力する
                 */
                if(_arrayCcDateTimePicker[Convert.ToInt32(((CcCheckBox)sender).Tag)].CustomFormat == " ")
                    _arrayCcDateTimePicker[Convert.ToInt32(((CcCheckBox)sender).Tag)].SetValue(DateTimePickerExBase.GetValue());

                _arrayCcComboBox[Convert.ToInt32(((CcCheckBox)sender).Tag)].Enabled = true;
                _arrayCcComboBox[Convert.ToInt32(((CcCheckBox)sender).Tag)].SelectedIndex = this.ComboBoxExBase.SelectedIndex;

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
        /// 指定された PdfViewer に PDF（byte[]）を表示する
        /// </summary>
        /// <param name="ccPdfView">PdfViewer のインスタンス</param>
        /// <param name="pdfBytes">PDF のバイト配列</param>
        private void ShowPdfToViewer(CcPdfView ccPdfView, byte[] pdfBytes) {
            ccPdfView.SetPdfStream(new MemoryStream(pdfBytes));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LegalTwelveItemDetail_FormClosing(object sender, FormClosingEventArgs e) {

        }

        /// <summary>
        /// コントロールを初期化
        /// </summary>
        private void InitializeControl() {
            this.LabelExStaffCode.Text = string.Empty;
            this.LabelExName.Text = string.Empty;

            this.DateTimePickerExBase.SetToday();
            this.ComboBoxExBase.SelectedIndex = 0;
            for(int i = 0; i < 12; i++) {
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
    }
}
