/*
 * 2025-05-17
 */
using ControlEx;

using Dao;

using Vo;

namespace LegalTwelveItem {
    public partial class LegalTwelveItemDetail : Form {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly Screen _screen;
        private readonly int _fiscalYear;
        private readonly int _staffCode;
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
        private CheckBoxEx[] _arrayCheckBoxEx = new CheckBoxEx[12];
        private DateTimePickerEx[] _arrayDateTimePickerEx = new DateTimePickerEx[12];
        private ComboBoxEx[] _arrayComboBoxEx = new ComboBoxEx[12];
        private TextBoxEx[] _arrayTextBoxEx = new TextBoxEx[12];
        private PictureBoxEx[] _arrayPictureBoxEx = new PictureBoxEx[3];

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        /// <param name="legalTwelveItemListVo"></param>
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
             */
            _arrayCheckBoxEx[0] = this.CheckBoxEx1;
            _arrayCheckBoxEx[1] = this.CheckBoxEx2;
            _arrayCheckBoxEx[2] = this.CheckBoxEx3;
            _arrayCheckBoxEx[3] = this.CheckBoxEx4;
            _arrayCheckBoxEx[4] = this.CheckBoxEx5;
            _arrayCheckBoxEx[5] = this.CheckBoxEx6;
            _arrayCheckBoxEx[6] = this.CheckBoxEx7;
            _arrayCheckBoxEx[7] = this.CheckBoxEx8;
            _arrayCheckBoxEx[8] = this.CheckBoxEx9;
            _arrayCheckBoxEx[9] = this.CheckBoxEx10;
            _arrayCheckBoxEx[10] = this.CheckBoxEx11;
            _arrayCheckBoxEx[11] = this.CheckBoxEx12;

            _arrayDateTimePickerEx[0] = this.DateTimePickerEx1;
            _arrayDateTimePickerEx[1] = this.DateTimePickerEx2;
            _arrayDateTimePickerEx[2] = this.DateTimePickerEx3;
            _arrayDateTimePickerEx[3] = this.DateTimePickerEx4;
            _arrayDateTimePickerEx[4] = this.DateTimePickerEx5;
            _arrayDateTimePickerEx[5] = this.DateTimePickerEx6;
            _arrayDateTimePickerEx[6] = this.DateTimePickerEx7;
            _arrayDateTimePickerEx[7] = this.DateTimePickerEx8;
            _arrayDateTimePickerEx[8] = this.DateTimePickerEx9;
            _arrayDateTimePickerEx[9] = this.DateTimePickerEx10;
            _arrayDateTimePickerEx[10] = this.DateTimePickerEx11;
            _arrayDateTimePickerEx[11] = this.DateTimePickerEx12;

            _arrayComboBoxEx[0] = this.ComboBoxEx1;
            _arrayComboBoxEx[1] = this.ComboBoxEx2;
            _arrayComboBoxEx[2] = this.ComboBoxEx3;
            _arrayComboBoxEx[3] = this.ComboBoxEx4;
            _arrayComboBoxEx[4] = this.ComboBoxEx5;
            _arrayComboBoxEx[5] = this.ComboBoxEx6;
            _arrayComboBoxEx[6] = this.ComboBoxEx7;
            _arrayComboBoxEx[7] = this.ComboBoxEx8;
            _arrayComboBoxEx[8] = this.ComboBoxEx9;
            _arrayComboBoxEx[9] = this.ComboBoxEx10;
            _arrayComboBoxEx[10] = this.ComboBoxEx11;
            _arrayComboBoxEx[11] = this.ComboBoxEx12;

            _arrayTextBoxEx[0] = this.TextBoxEx1;
            _arrayTextBoxEx[1] = this.TextBoxEx2;
            _arrayTextBoxEx[2] = this.TextBoxEx3;
            _arrayTextBoxEx[3] = this.TextBoxEx4;
            _arrayTextBoxEx[4] = this.TextBoxEx5;
            _arrayTextBoxEx[5] = this.TextBoxEx6;
            _arrayTextBoxEx[6] = this.TextBoxEx7;
            _arrayTextBoxEx[7] = this.TextBoxEx8;
            _arrayTextBoxEx[8] = this.TextBoxEx9;
            _arrayTextBoxEx[9] = this.TextBoxEx10;
            _arrayTextBoxEx[10] = this.TextBoxEx11;
            _arrayTextBoxEx[11] = this.TextBoxEx12;

            _arrayPictureBoxEx[0] = this.PictureBoxEx1;
            _arrayPictureBoxEx[1] = this.PictureBoxEx2;
            _arrayPictureBoxEx[2] = this.PictureBoxEx3;
            /*
             * MenuStrip
             */
            List<string> listString = new() {
                "ToolStripMenuItemFile",
                "ToolStripMenuItemExit",
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);
            this.InitializeControl();

            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;
            /*
             * Eventを登録する
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
            this.PutControl(_legalTwelveItemDao.SelectLegalTwelveItemVo(_fiscalYear, staffCode));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExUpdate_Click(object sender, EventArgs e) {
            for (int i = 0; i < 12; i++) {
                if (_arrayCheckBoxEx[i].Checked) {
                    /*
                     * Controlの値をLegalTwelveItemVoに代入
                     */
                    LegalTwelveItemVo legalTwelveItemVo = new();
                    legalTwelveItemVo.StudentsDate = _arrayDateTimePickerEx[i].GetValue();
                    legalTwelveItemVo.StudentsCode = Convert.ToInt32(_arrayCheckBoxEx[i].Tag);
                    legalTwelveItemVo.StudentsFlag = _arrayCheckBoxEx[i].Checked;
                    legalTwelveItemVo.StaffCode = _staffCode;
                    legalTwelveItemVo.StaffSign = (byte[])new ImageConverter().ConvertTo(_arrayPictureBoxEx[_arrayComboBoxEx[i].SelectedIndex].Image, typeof(byte[]));
                    legalTwelveItemVo.SignNumber = _arrayComboBoxEx[i].SelectedIndex;
                    legalTwelveItemVo.Memo = _arrayTextBoxEx[i].Text;
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
                    if ((LegalTwelveItemVo)_arrayTextBoxEx[i].Tag is not null && _legalTwelveItemDao.ExistenceLegalTwelveItem((LegalTwelveItemVo)_arrayTextBoxEx[i].Tag)) {
                        try {
                            _legalTwelveItemDao.UpdateOneLegalTwelveItem((LegalTwelveItemVo)_arrayTextBoxEx[i].Tag, legalTwelveItemVo);
                        } catch (Exception exception) {
                            MessageBox.Show(exception.Message);
                        }
                    } else {
                        try {
                            _legalTwelveItemDao.InsertOneLegalTwelveItem(legalTwelveItemVo);
                        } catch (Exception exception) {
                            MessageBox.Show(exception.Message);
                        }
                    }
                } else {
                    /*
                     * 最初にセットされた値(Vo)はTagに代入してある
                     * _arrayTextBox[i].Tag = legalTwelveItemVo;
                     */
                    if ((LegalTwelveItemVo)_arrayTextBoxEx[i].Tag is not null) {
                        try {
                            _legalTwelveItemDao.DeleteOneLegalTwelveItemVo((LegalTwelveItemVo)_arrayTextBoxEx[i].Tag);
                        } catch (Exception exception) {
                            MessageBox.Show(exception.Message);
                        }
                    }
                }
            }
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listLegalTwelveItemVo"></param>
        private void PutControl(List<LegalTwelveItemVo> listLegalTwelveItemVo) {
            this.LabelExStaffCode.Text = Convert.ToString(_staffCode);                                      // StaffCode
            this.LabelExName.Text = _staffMasterDao.SelectOneStaffMaster(_staffCode).Name;                  // Name
            /*
             * CheckBox等の処理
             */
            for (int i = 0; i < 12; i++) {
                LegalTwelveItemVo legalTwelveItemVo = listLegalTwelveItemVo.Find(x => x.StudentsCode == i);
                /*
                 * _arrayTextBoxのTagにLegalTwelveItemVoを格納
                 * Recordを削除するさいに必要な情報になる
                 */
                _arrayTextBoxEx[i].Tag = legalTwelveItemVo;

                if (legalTwelveItemVo is not null) {
                    _arrayCheckBoxEx[i].Checked = true;
                    _arrayDateTimePickerEx[i].SetValue(legalTwelveItemVo.StudentsDate);
                    _arrayComboBoxEx[i].Text = _signNumber[legalTwelveItemVo.SignNumber];
                    _arrayTextBoxEx[i].Text = legalTwelveItemVo.Memo;
                } else {
                    _arrayCheckBoxEx[i].Checked = false;
                    _arrayDateTimePickerEx[i].SetEmpty();
                    _arrayComboBoxEx[i].Text = string.Empty;
                    _arrayTextBoxEx[i].Text = string.Empty;
                }
            }
            /*
             * PictureBoxの処理
             */
            IEnumerable<LegalTwelveItemVo> iEnumerableHLegalTwelveItemVo = listLegalTwelveItemVo.DistinctBy(c => c.SignNumber);
            foreach (LegalTwelveItemVo hLegalTwelveItemVo in iEnumerableHLegalTwelveItemVo.OrderBy(x => x.SignNumber)) {
                if (hLegalTwelveItemVo.StaffSign is not null && hLegalTwelveItemVo.StaffSign.Length != 0) {
                    ImageConverter imageConv = new();
                    _arrayPictureBoxEx[hLegalTwelveItemVo.SignNumber].Image = (Image)imageConv.ConvertFrom(hLegalTwelveItemVo.StaffSign);
                } else {
                    _arrayPictureBoxEx[hLegalTwelveItemVo.SignNumber].Image = null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeControl() {
            this.LabelExStaffCode.Text = string.Empty;
            this.LabelExName.Text = string.Empty;

            this.DateTimePickerExBase.SetToday();
            this.ComboBoxExBase.SelectedIndex = 0;
            for (int i = 0; i < 12; i++) {
                _arrayCheckBoxEx[i].Checked = false;

                _arrayDateTimePickerEx[i].Enabled = false;
                _arrayDateTimePickerEx[i].SetEmpty();

                _arrayComboBoxEx[i].Enabled = false;
                _arrayComboBoxEx[i].SelectedIndex = -1;

                _arrayTextBoxEx[i].Enabled = false;
                _arrayTextBoxEx[i].Text = string.Empty;
            }
            _arrayPictureBoxEx[0].Image = null;
            _arrayPictureBoxEx[1].Image = null;
            _arrayPictureBoxEx[2].Image = null;
        }

        /// <summary>
        /// ContextMenuStripが開いた時のSourceControlを保持
        /// </summary>
        Control _sourceControl = null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStrip1_Opened(object sender, EventArgs e) {
            _sourceControl = ((ContextMenuStrip)sender).SourceControl;                                                  // ContextMenuStripを表示しているコントロールを取得する
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemClip":                                                                           // Picture Clip
                    ((PictureBoxEx)_sourceControl).Image = (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap);
                    break;
                case "ToolStripMenuItemDelete":                                                                         // Picture Delete
                    ((PictureBoxEx)_sourceControl).Image = null;
                    break;
                case "ToolStripMenuItemExit":                                                                           // アプリケーションを終了する
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxEx_CheckedChanged(object sender, EventArgs e) {
            if (((CheckBoxEx)sender).Checked) {
                _arrayDateTimePickerEx[Convert.ToInt32(((CheckBoxEx)sender).Tag)].Enabled = true;
                /*
                 * 指導実施日が空白の場合、値を入力する
                 */
                if (_arrayDateTimePickerEx[Convert.ToInt32(((CheckBoxEx)sender).Tag)].CustomFormat == " ")
                    _arrayDateTimePickerEx[Convert.ToInt32(((CheckBoxEx)sender).Tag)].SetValue(DateTimePickerExBase.GetValue());

                _arrayComboBoxEx[Convert.ToInt32(((CheckBoxEx)sender).Tag)].Enabled = true;
                _arrayComboBoxEx[Convert.ToInt32(((CheckBoxEx)sender).Tag)].SelectedIndex = this.ComboBoxExBase.SelectedIndex;

                _arrayTextBoxEx[Convert.ToInt32(((CheckBoxEx)sender).Tag)].Enabled = true;
            } else {
                if (_arrayDateTimePickerEx[Convert.ToInt32(((CheckBoxEx)sender).Tag)].CustomFormat != " " || _arrayComboBoxEx[Convert.ToInt32(((CheckBoxEx)sender).Tag)].Text != "" || _arrayTextBoxEx[Convert.ToInt32(((CheckBoxEx)sender).Tag)].Text != "") {
                    DialogResult dialogResult = MessageBox.Show("登録されているデータを削除してもよろしいですか？", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    switch (dialogResult) {
                        case DialogResult.OK:
                            _arrayDateTimePickerEx[Convert.ToInt32(((CheckBoxEx)sender).Tag)].Enabled = false;
                            _arrayDateTimePickerEx[Convert.ToInt32(((CheckBoxEx)sender).Tag)].SetClear();

                            _arrayComboBoxEx[Convert.ToInt32(((CheckBoxEx)sender).Tag)].Enabled = false;
                            _arrayComboBoxEx[Convert.ToInt32(((CheckBoxEx)sender).Tag)].SelectedIndex = -1;

                            _arrayTextBoxEx[Convert.ToInt32(((CheckBoxEx)sender).Tag)].Enabled = false;
                            _arrayTextBoxEx[Convert.ToInt32(((CheckBoxEx)sender).Tag)].SetEmpty();
                            break;
                        case DialogResult.Cancel:
                            // 処理を戻す意味で、フラグを反転させる
                            ((CheckBoxEx)sender).Checked = !((CheckBoxEx)sender).Checked;
                            break;
                    }
                } else {
                    _arrayDateTimePickerEx[Convert.ToInt32(((CheckBoxEx)sender).Tag)].Enabled = false;
                    _arrayDateTimePickerEx[Convert.ToInt32(((CheckBoxEx)sender).Tag)].SetClear();

                    _arrayComboBoxEx[Convert.ToInt32(((CheckBoxEx)sender).Tag)].Enabled = false;
                    _arrayComboBoxEx[Convert.ToInt32(((CheckBoxEx)sender).Tag)].SelectedIndex = -1;

                    _arrayTextBoxEx[Convert.ToInt32(((CheckBoxEx)sender).Tag)].Enabled = false;
                    _arrayTextBoxEx[Convert.ToInt32(((CheckBoxEx)sender).Tag)].SetEmpty();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LegalTwelveItemDetail_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
