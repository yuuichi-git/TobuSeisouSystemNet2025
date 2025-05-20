/*
 * 2025-05-14
 */
using System.Drawing.Printing;

using Common;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace LegalTwelveItem {
    public partial class LegalTwelveItemList : Form {
        private readonly DateTime _defaultDatetime = new(1900, 01, 01);
        private readonly DateUtility _dateUtility = new();
        private readonly Screen _screen;
        private readonly ScreenForm _screenForm = new();
        private PrintDocument _printDocument = new();
        /*
         * Dao
         */
        private readonly LegalTwelveItemDao _legalTwelveItemDao;
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;
        /*
         * Columns
         */
        private const int _colBelongsName = 0;
        private const int _colJobFormName = 1;
        private const int _colOccupation = 2;
        private const int _colName = 3;
        private const int _colEmploymentDate = 4;
        private const int _colStudentsFlag01 = 5;
        private const int _colStudentsFlag02 = 6;
        private const int _colStudentsFlag03 = 7;
        private const int _colStudentsFlag04 = 8;
        private const int _colStudentsFlag05 = 9;
        private const int _colStudentsFlag06 = 10;
        private const int _colStudentsFlag07 = 11;
        private const int _colStudentsFlag08 = 12;
        private const int _colStudentsFlag09 = 13;
        private const int _colStudentsFlag10 = 14;
        private const int _colStudentsFlag11 = 15;
        private const int _colStudentsFlag12 = 16;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public LegalTwelveItemList(ConnectionVo connectionVo, Screen screen) {
            _screen = screen;
            /*
             * Dao
             */
            _legalTwelveItemDao = new(connectionVo);
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
            List<string> listString = new() {
                "ToolStripMenuItemFile",
                "ToolStripMenuItemExit",
                "ToolStripMenuItemPrint",
                "ToolStripMenuItemPrintA4",
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);
            // �Ώ۔N�x
            this.NumericUpDownExFiscalYear.Value = _dateUtility.GetFiscalYear();
            /*
             * �v�����^�[�̈ꗗ���擾��A�ʏ�g���v�����^�[�����Z�b�g����
             */
            foreach (string item in new PrintUtility().GetAllPrinterName()) {
                this.ComboBoxExPrinterName.Items.Add(item);
            }
            this.ComboBoxExPrinterName.Text = _printDocument.PrinterSettings.PrinterName;
            /*
             * InitializeSpread
             */
            this.InitializeSheetViewList(this.SheetViewList);
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
            this.PutSheetViewList(_legalTwelveItemDao.SelectLegalTwelveItemListVo(_dateUtility.GetFiscalYearStartDate((int)NumericUpDownExFiscalYear.Value), _dateUtility.GetFiscalYearEndDate((int)NumericUpDownExFiscalYear.Value)));
        }

        int spreadListTopRow = 0;
        /// <summary>
        /// PutSheetViewList
        /// </summary>
        /// <param name="listLegalTwelveItemVo"></param>
        private void PutSheetViewList(List<LegalTwelveItemListVo> listLegalTwelveItemVo) {
            /*
             * SheetViewList�̏���
             */
            this.SpreadList.SuspendLayout();                                                                                    // Spread �񊈐���
            this.spreadListTopRow = SpreadList.GetViewportTopRow(0);                                                            // �擪�s�i��j�C���f�b�N�X���擾
            if (this.SheetViewList.Rows.Count > 0)                                                                              // Row���폜����
                this.SheetViewList.RemoveRows(0, this.SheetViewList.Rows.Count);
            /*
             * SheetViewList�֕\��
             */
            int i = 0;
            foreach (LegalTwelveItemListVo legalTwelveItemListVo in listLegalTwelveItemVo) {
                this.SheetViewList.Rows.Add(i, 1);
                this.SheetViewList.RowHeader.Columns[0].Label = (i + 1).ToString();                                             // Row�w�b�_
                this.SheetViewList.Rows[i].ForeColor = legalTwelveItemListVo.JobForm == 11 ? Color.Blue : Color.Black;              // �蒠�̃��R�[�h��ForeColor���Z�b�g
                this.SheetViewList.Rows[i].Tag = legalTwelveItemListVo;                                                             // H_LegalTwelveItemVo��ޔ�
                this.SheetViewList.Cells[i, _colBelongsName].Text = legalTwelveItemListVo.BelongsName;
                this.SheetViewList.Cells[i, _colJobFormName].Text = legalTwelveItemListVo.JobFormName;
                this.SheetViewList.Cells[i, _colOccupation].Text = legalTwelveItemListVo.OccupationName;
                this.SheetViewList.Cells[i, _colName].Text = legalTwelveItemListVo.StaffName;
                this.SheetViewList.Cells[i, _colEmploymentDate].Text = legalTwelveItemListVo.EmploymentDate != _defaultDatetime ? legalTwelveItemListVo.EmploymentDate.ToString("yyyy/MM/dd") : string.Empty;
                this.SheetViewList.Cells[i, _colStudentsFlag01].Text = legalTwelveItemListVo.Students01Flag ? "�Z" : string.Empty;
                this.SheetViewList.Cells[i, _colStudentsFlag02].Text = legalTwelveItemListVo.Students02Flag ? "�Z" : string.Empty;
                this.SheetViewList.Cells[i, _colStudentsFlag03].Text = legalTwelveItemListVo.Students03Flag ? "�Z" : string.Empty;
                this.SheetViewList.Cells[i, _colStudentsFlag04].Text = legalTwelveItemListVo.Students04Flag ? "�Z" : string.Empty;
                this.SheetViewList.Cells[i, _colStudentsFlag05].Text = legalTwelveItemListVo.Students05Flag ? "�Z" : string.Empty;
                this.SheetViewList.Cells[i, _colStudentsFlag06].Text = legalTwelveItemListVo.Students06Flag ? "�Z" : string.Empty;
                this.SheetViewList.Cells[i, _colStudentsFlag07].Text = legalTwelveItemListVo.Students07Flag ? "�Z" : string.Empty;
                this.SheetViewList.Cells[i, _colStudentsFlag08].Text = legalTwelveItemListVo.Students08Flag ? "�Z" : string.Empty;
                this.SheetViewList.Cells[i, _colStudentsFlag09].Text = legalTwelveItemListVo.Students09Flag ? "�Z" : string.Empty;
                this.SheetViewList.Cells[i, _colStudentsFlag10].Text = legalTwelveItemListVo.Students10Flag ? "�Z" : string.Empty;
                this.SheetViewList.Cells[i, _colStudentsFlag11].Text = legalTwelveItemListVo.Students11Flag ? "�Z" : string.Empty;
                this.SheetViewList.Cells[i, _colStudentsFlag12].Text = legalTwelveItemListVo.Students12Flag ? "�Z" : string.Empty;
                i++;
            }
            this.SpreadList.SetViewportTopRow(0, spreadListTopRow);                                                             // �擪�s�i��j�C���f�b�N�X���Z�b�g
            this.SpreadList.ResumeLayout();                                                                                     // Spread ������
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(" ", i, " ��");
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
             * Detail�E�C���h�E��\��
             */
            LegalTwelveItemDetail legalTwelveItemDetail = new(_connectionVo, _screen, (int)this.NumericUpDownExFiscalYear.Value, ((LegalTwelveItemListVo)SheetViewList.Rows[e.Row].Tag).StaffCode);
            _screenForm.SetPosition(_screen, legalTwelveItemDetail);
            legalTwelveItemDetail.KeyPreview = true;
            legalTwelveItemDetail.Show(this);
            return;
        }

        /// <summary>
        /// InitializeSheetViewList
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns></returns>
        private SheetView InitializeSheetViewList(SheetView sheetView) {
            this.SpreadList.AllowDragDrop = false; // DrugDrop���֎~����
            this.SpreadList.PaintSelectionHeader = false; // �w�b�_�̑I����Ԃ����Ȃ�
            this.SpreadList.TabStrip.DefaultSheetTab.Font = new Font("Yu Gothic UI", 9);
            this.SpreadList.TabStripPolicy = TabStripPolicy.Never; // �V�[�g�^�u���\��
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
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                /*
                 * A4�ň������
                 */
                case "ToolStripMenuItemPrintA4":
                    // Event��o�^
                    _printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);
                    // �o�͐�v�����^���w�肵�܂��B
                    _printDocument.PrinterSettings.PrinterName = this.ComboBoxExPrinterName.Text;
                    // �p���̌�����ݒ�(���Ftrue�A�c�Ffalse)
                    _printDocument.DefaultPageSettings.Landscape = false;
                    /*
                     * �v�����^���T�|�[�g���Ă���p���T�C�Y�𒲂ׂ�
                     */
                    foreach (PaperSize paperSize in _printDocument.PrinterSettings.PaperSizes) {
                        // B5�p���ɐݒ肷��
                        if (paperSize.Kind == PaperKind.A4) {
                            _printDocument.DefaultPageSettings.PaperSize = paperSize;
                            break;
                        }
                    }
                    // ����������w�肵�܂��B
                    _printDocument.PrinterSettings.Copies = 1;
                    // �Жʈ���ɐݒ肵�܂��B
                    _printDocument.PrinterSettings.Duplex = Duplex.Default;
                    // �J���[����ɐݒ肵�܂��B
                    _printDocument.PrinterSettings.DefaultPageSettings.Color = true;
                    // �������
                    _printDocument.Print();
                    break;
                /*
                 * �A�v���P�[�V�������I������
                 */
                case "ToolStripMenuItemExit":
                    Close();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e) {
            // ����y�[�W�i1�y�[�W�ځj�̕`����s��
            Rectangle rectangle = new(e.PageBounds.X, e.PageBounds.Y, e.PageBounds.Width, e.PageBounds.Height);
            // e.Graphics�֏o��(page �p�����[�^�́A�O����ł͂Ȃ��P����n�܂�܂�)
            this.SpreadList.OwnerPrintDraw(e.Graphics, rectangle, 0, 1);
            // ����I�����w��
            e.HasMorePages = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmploymentAgreementList_FormClosing(object sender, FormClosingEventArgs e) {
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
