/*
 * 2025-05-23
 */
using Common;

using ControlEx;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace Accident {
    public partial class AccidentList : Form {
        private readonly ScreenForm _screenForm = new();
        /*
         * Column�̔ԍ�
         */
        /// <summary>
        /// �����N����
        /// </summary>
        private const int _colOccurrenceDate = 0;
        /// <summary>
        /// �����ꏊ
        /// </summary>
        private const int _colOccurrenceAddress = 1;
        /// <summary>
        /// ���̏����敪
        /// </summary>
        private const int _colTotallingFlag = 2;
        /// <summary>
        /// ��t�̎��
        /// </summary>
        private const int _colAccident_Kind = 3;
        /// <summary>
        /// ����
        /// </summary>
        private const int _colDisplayName = 4;
        /// <summary>
        /// �E��
        /// </summary>
        private const int _colWorkKind = 5;
        /// <summary>
        /// �ԗ��o�^�ԍ�
        /// </summary>
        private const int _colCarRegistrationNumber = 6;
        /// <summary>
        /// �T�v
        /// </summary>
        private const int _colAccidentSummary = 7;
        /// <summary>
        /// �ڍ�
        /// </summary>
        private const int _colAccidentDetail = 8;
        /// <summary>
        /// �w��
        /// </summary>
        private const int _colGuide = 9;

        /*
         * Dao
         */
        private readonly CarAccidentMasterDao _carAccidentMasterDao;
        private readonly CarMasterDao _carMasterDao;
        private readonly StaffMasterDao _staffMasterDao;
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public AccidentList(ConnectionVo connectionVo, Screen screen) {
            /*
             * Dao 
             */
            _carAccidentMasterDao = new(connectionVo);
            _carMasterDao = new(connectionVo);
            _staffMasterDao = new(connectionVo);
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
            List<string> listStringSheetViewList = new() {
                        "ToolStripMenuItemFile",
                        "ToolStripMenuItemExit",
                        "ToolStripMenuItemEdit",
                        "ToolStripMenuItemInsertNewRecord",
                        "ToolStripMenuItemHelp"
                     };
            this.MenuStripEx1.ChangeEnable(listStringSheetViewList);
            // ���t��������
            this.DateTimePickerExOperationDate1.SetValueJp(DateTime.Now.AddMonths(-3));
            this.DateTimePickerExOperationDate2.SetValueJp(DateTime.Now.AddDays(1));
            this.InitializeSheetView(SheetViewList);
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat("�����R�[�h���F0��");
            /*
             * Event��o�^����
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExUpdate_Click(object sender, EventArgs e) {
            try {
                this.PutSheetViewList(_carAccidentMasterDao.SelectAllCarAccidentMaster(this.DateTimePickerExOperationDate1.GetValue(), this.DateTimePickerExOperationDate2.GetValue()));
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
        }

        int spreadListTopRow = 0;
        /// <summary>
        /// PutSheetViewList
        /// </summary>
        /// <param name="listCarAccidentMasterVo"></param>
        private void PutSheetViewList(List<CarAccidentMasterVo> listCarAccidentMasterVo) {
            // Spread �񊈐���
            this.SpreadList.SuspendLayout();
            // �擪�s�i��j�C���f�b�N�X���擾
            spreadListTopRow = SpreadList.GetViewportTopRow(0);
            // Row���폜����
            if (SheetViewList.Rows.Count > 0)
                SheetViewList.RemoveRows(0, SheetViewList.Rows.Count);

            int i = 0;
            foreach (CarAccidentMasterVo carAccidentMasterVo in listCarAccidentMasterVo.OrderBy(x => x.OccurrenceYmdHms)) {
                SheetViewList.Rows.Add(i, 1);
                SheetViewList.RowHeader.Columns[0].Label = (i + 1).ToString(); // Row�w�b�_
                SheetViewList.Rows[i].Height = 22; // Row�̍���
                SheetViewList.Rows[i].Resizable = false; // Row��Resizable���֎~

                SheetViewList.Rows[i].Tag = carAccidentMasterVo; //carAccidentLedgerVo��ޔ�����
                SheetViewList.Cells[i, _colOccurrenceDate].Value = carAccidentMasterVo.OccurrenceYmdHms;
                SheetViewList.Cells[i, _colOccurrenceAddress].Text = carAccidentMasterVo.OccurrenceAddress;
                SheetViewList.Cells[i, _colTotallingFlag].Text = carAccidentMasterVo.TotallingFlag ? "���̂Ƃ��Ĉ���" : "";
                SheetViewList.Cells[i, _colAccident_Kind].Text = carAccidentMasterVo.AccidentKind;
                SheetViewList.Cells[i, _colDisplayName].Text = carAccidentMasterVo.DisplayName;
                SheetViewList.Cells[i, _colWorkKind].Text = carAccidentMasterVo.WorkKind;
                SheetViewList.Cells[i, _colCarRegistrationNumber].Text = carAccidentMasterVo.CarRegistrationNumber;
                SheetViewList.Cells[i, _colAccidentSummary].Text = carAccidentMasterVo.AccidentSummary;
                SheetViewList.Cells[i, _colAccidentDetail].Text = carAccidentMasterVo.AccidentDetail;
                SheetViewList.Cells[i, _colGuide].Text = carAccidentMasterVo.Guide;
                i++;
            }
            // �擪�s�i��j�C���f�b�N�X���Z�b�g
            this.SpreadList.SetViewportTopRow(0, spreadListTopRow);
            // Spread ������
            this.SpreadList.ResumeLayout(true);
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat("�����R�[�h���F", listCarAccidentMasterVo.Count, "��");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemInsertNewRecord":                                        // �V�K���R�[�h
                    AccidentDetail accidentDetail = new(_connectionVo, null);
                    _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), accidentDetail);
                    accidentDetail.Show(this);
                    break;
                case "ToolStripMenuItemExit":                                                   // �A�v���P�[�V�������I������
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpreadList_CellDoubleClick(object sender, CellClickEventArgs e) {
            // �w�b�_�[��DoubleClick�����
            if (e.ColumnHeader)
                return;
            AccidentDetail accidentDetail = new(_connectionVo, (CarAccidentMasterVo)SheetViewList.Rows[e.Row].Tag);
            _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), accidentDetail);
            accidentDetail.Show(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns></returns>
        private SheetView InitializeSheetView(SheetView sheetView) {
            this.SpreadList.AllowDragDrop = false;                                              // DrugDrop���֎~����
            this.SpreadList.PaintSelectionHeader = false;                                       // �w�b�_�̑I����Ԃ����Ȃ�
            this.SpreadList.TabStrip.DefaultSheetTab.Font = new Font("Yu Gothic UI", 9);
            sheetView.AlternatingRows.Count = 2;                                                // �s�X�^�C�����Q�s�P�ʂƂ��܂�
            sheetView.AlternatingRows[0].BackColor = Color.WhiteSmoke;                          // 1�s�ڂ̔w�i�F��ݒ肵�܂�
            sheetView.AlternatingRows[1].BackColor = Color.White;                               // 2�s�ڂ̔w�i�F��ݒ肵�܂�
            sheetView.ColumnHeader.Rows[0].Height = 26;                                         // Column�w�b�_�̍���
            sheetView.GrayAreaBackColor = Color.White;
            sheetView.HorizontalGridLine = new GridLine(GridLineType.None);
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9);                  // �s�w�b�_��Font
            sheetView.RowHeader.Columns[0].Width = 48;                                          // �s�w�b�_�̕���ύX���܂�
            sheetView.VerticalGridLine = new GridLine(GridLineType.Flat, Color.LightGray);
            sheetView.RemoveRows(0, sheetView.Rows.Count);
            return sheetView;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimePickerExOperationDate1_ValueChanged(object sender, EventArgs e) {
            if (((DateTimePickerEx)sender).Value > DateTimePickerExOperationDate2.Value) {
                DateTimePickerExOperationDate2.Value = DateTimePickerExOperationDate1.Value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimePickerExOperationDate2_ValueChanged(object sender, EventArgs e) {
            if (((DateTimePickerEx)sender).Value < DateTimePickerExOperationDate1.Value) {
                DateTimePickerExOperationDate1.Value = DateTimePickerExOperationDate2.Value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccidentList_FormClosing(object sender, FormClosingEventArgs e) {
            DialogResult dialogResult = MessageBox.Show("�A�v���P�[�V�������I�����܂��B��낵���ł����H", "���b�Z�[�W", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            switch (dialogResult) {
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
