/*
 * 2025-05-10
 */
using Common;

using ControlEx;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace StatusOfResidence {
    public partial class StatusOfResidenceList : Form {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly DateUtility _dateUtility = new();
        private readonly ScreenForm _screenForm = new();
        /*
         * Dao
         */
        private readonly StatusOfResidenceMasterDao _statusOfResidenceMasterDao;
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// �]���Җ�
        /// </summary>
        private const int _colStaffName = 0;
        /// <summary>
        /// �]���Җ��J�i
        /// </summary>
        private const int _colStaffNameKana = 1;
        /// <summary>
        /// ���N����
        /// </summary>
        private const int _colBirthDate = 2;
        /// <summary>
        /// ����
        /// </summary>
        private const int _colGender = 3;
        /// <summary>
        /// ���ЁE�n��
        /// </summary>
        private const int _colNationality = 4;
        /// <summary>
        /// �Z���n
        /// </summary>
        private const int _colAddress = 5;
        /// <summary>
        /// �ݗ����i
        /// </summary>
        private const int _colStatusOfResidence = 6;
        /// <summary>
        /// �A�J�����̗L��
        /// </summary>
        private const int _colWorkLimit = 7;
        /// <summary>
        /// �ݗ�����
        /// </summary>
        private const int _colPeriodDate = 8;
        /// <summary>
        /// �L������
        /// </summary>
        private const int _colDeadlineDate = 9;

        /// <summary>
        /// �R���X�g���N�^�[
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public StatusOfResidenceList(ConnectionVo connectionVo, Screen screen) {
            /*
             * Dao
             */
            _statusOfResidenceMasterDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            /*
             * 
             */
            InitializeComponent();
            /*
             * MenuStrip
             */
            List<string> listString = new() {
                "ToolStripMenuItemFile",
                "ToolStripMenuItemExit",
                "ToolStripMenuItemEdit",
                "ToolStripMenuItemInsertNewRecord",
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);

            this.InitializeSheetView(this.SheetViewList);
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
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemInsertNewRecord":
                    StatusOfResidenceDetail statusOfResidenceDetail = new(_connectionVo);
                    _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), statusOfResidenceDetail);
                    statusOfResidenceDetail.Show(this);
                    break;
                case "ToolStripMenuItemExit": // �A�v���P�[�V�������I������
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
            switch (((ButtonEx)sender).Name) {
                case "ButtonExUpdate":
                    this.PutSheetViewList(_statusOfResidenceMasterDao.SelectAllStatusOfResidenceMaster());
                    break;
            }
        }

        int _spreadListTopRow = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listStatusOfResidenceMasterVo"></param>
        private void PutSheetViewList(List<StatusOfResidenceMasterVo> listStatusOfResidenceMasterVo) {
            int rowCount = 0;
            // Spread �񊈐���
            this.SpreadList.SuspendLayout();
            // �擪�s�i��j�C���f�b�N�X���擾
            _spreadListTopRow = this.SpreadList.GetViewportTopRow(0);
            /*
             * Row���폜����
             */
            if (this.SheetViewList.Rows.Count > 0)
                this.SheetViewList.RemoveRows(0, this.SheetViewList.Rows.Count);
            foreach (StatusOfResidenceMasterVo statusOfResidenceMasterVo in listStatusOfResidenceMasterVo.OrderBy(x => x.DeadlineDate)) {
                this.SheetViewList.Rows.Add(rowCount, 1);
                this.SheetViewList.RowHeader.Columns[0].Label = (rowCount + 1).ToString();                                      // Row�w�b�_
                this.SheetViewList.Rows[rowCount].ForeColor = statusOfResidenceMasterVo.DeleteFlag ? Color.Red : Color.Black;   // �ސE�ς̃��R�[�h��ForeColor���Z�b�g
                this.SheetViewList.Rows[rowCount].Tag = statusOfResidenceMasterVo;
                this.SheetViewList.Cells[rowCount, _colStaffName].Text = statusOfResidenceMasterVo.StaffName;                   // �]���Җ�
                this.SheetViewList.Cells[rowCount, _colStaffNameKana].Text = statusOfResidenceMasterVo.StaffNameKana;           // �]���Җ��J�i
                this.SheetViewList.Cells[rowCount, _colBirthDate].Value = statusOfResidenceMasterVo.BirthDate;                  // ���N����
                this.SheetViewList.Cells[rowCount, _colGender].Text = statusOfResidenceMasterVo.Gender;                         // ����
                this.SheetViewList.Cells[rowCount, _colNationality].Text = statusOfResidenceMasterVo.Nationality;               // ���ЁE�n��
                this.SheetViewList.Cells[rowCount, _colAddress].Text = statusOfResidenceMasterVo.Address;                       // �Z���n
                this.SheetViewList.Cells[rowCount, _colStatusOfResidence].Text = statusOfResidenceMasterVo.StatusOfResidence;   // �ݗ����i
                this.SheetViewList.Cells[rowCount, _colWorkLimit].Text = statusOfResidenceMasterVo.WorkLimit;                   // �A�J�����̗L��
                this.SheetViewList.Cells[rowCount, _colPeriodDate].Value = statusOfResidenceMasterVo.PeriodDate;                // �ݗ�����
                this.SheetViewList.Cells[rowCount, _colDeadlineDate].Value = statusOfResidenceMasterVo.DeadlineDate;            // �L������
                rowCount++;
            }

            // �擪�s�i��j�C���f�b�N�X���Z�b�g
            this.SpreadList.SetViewportTopRow(0, _spreadListTopRow);
            // Spread ������
            this.SpreadList.ResumeLayout();
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(" ", rowCount, " ��");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpreadList_CellDoubleClick(object sender, CellClickEventArgs e) {
            /*
             * �w�b�_�[��DoubleClick�����
             */
            if (e.ColumnHeader)
                return;
            /*
             * StatusOfResidenceDetail��\������
             */
            int staffCode = ((StatusOfResidenceMasterVo)SheetViewList.Rows[SheetViewList.ActiveRowIndex].Tag).StaffCode;
            StatusOfResidenceDetail statusOfResidenceDetail = new(_connectionVo, staffCode);
            _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), statusOfResidenceDetail);
            statusOfResidenceDetail.Show(this);
        }

        /// <summary>
        /// InitializeSheetView
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns></returns>
        private SheetView InitializeSheetView(SheetView sheetView) {
            SpreadList.AllowDragDrop = false; // DrugDrop���֎~����
            SpreadList.PaintSelectionHeader = false; // �w�b�_�̑I����Ԃ����Ȃ�
            SpreadList.TabStrip.DefaultSheetTab.Font = new Font("Yu Gothic UI", 9);
            SpreadList.TabStripPolicy = TabStripPolicy.Never; // �V�[�g�^�u���\��
            sheetView.AlternatingRows.Count = 2; // �s�X�^�C�����Q�s�P�ʂƂ��܂�
            sheetView.AlternatingRows[0].BackColor = Color.WhiteSmoke; // 1�s�ڂ̔w�i�F��ݒ肵�܂�
            sheetView.AlternatingRows[1].BackColor = Color.White; // 2�s�ڂ̔w�i�F��ݒ肵�܂�
            sheetView.ColumnHeader.Rows[0].Height = 22; // Column�w�b�_�̍���
            sheetView.GrayAreaBackColor = Color.White;
            sheetView.HorizontalGridLine = new GridLine(GridLineType.None);
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9); // �s�w�b�_��Font
            sheetView.RowHeader.Columns[0].Width = 48; // �s�w�b�_�̕���ύX���܂�
            sheetView.VerticalGridLine = new GridLine(GridLineType.Flat, Color.LightGray);
            sheetView.RemoveRows(0, sheetView.Rows.Count);
            return sheetView;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusOfResidenceList_FormClosing(object sender, FormClosingEventArgs e) {
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
