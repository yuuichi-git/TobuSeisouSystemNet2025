using System.Drawing.Printing;

using Common;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace Certification {

    public partial class CertificationList : Form {
        private PrintDocument _printDocument = new();
        private DateUtility _dateUtility = new();
        private readonly Screen _screen;
        private readonly ScreenForm _screenForm = new();
        /*
         * Dao
         */
        private readonly StaffMasterDao _staffMasterDao;
        private readonly BelongsMasterDao _belongsMasterDao;
        private readonly LicenseMasterDao _licenseMasterDao;
        private readonly CertificationMasterDao _certificationMasterDao;
        private readonly CertificationFileDao _certificationFileDao;
        private readonly ToukanpoTrainingCardDao _toukanpoTrainingCardDao;
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;
        /*
         * Dictionary
         */
        private readonly Dictionary<int, string> _dictionaryBelongs = new();                    // �R�[�h�

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public CertificationList(ConnectionVo connectionVo, Screen screen) {
            _screen = screen;
            /*
             * Dao
             */
            _staffMasterDao = new(connectionVo);
            _belongsMasterDao = new(connectionVo);
            _licenseMasterDao = new(connectionVo);
            _certificationMasterDao = new(connectionVo);
            _certificationFileDao = new(connectionVo);
            _toukanpoTrainingCardDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            /*
             * Dictionary
             */
            foreach (BelongsMasterVo belongsMasterVo in _belongsMasterDao.SelectAllBelongsMaster())
                _dictionaryBelongs.Add(belongsMasterVo.Code, belongsMasterVo.Name);
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
                "ToolStripMenuItemPrint",
                "ToolStripMenuItemPrintA4",
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);
            /*
             * �v�����^�[�̈ꗗ���擾��A�ʏ�g���v�����^�[�����Z�b�g����
             */
            foreach (string item in new PrintUtility().GetAllPrinterName()) {
                this.ComboBoxExPrinterName.Items.Add(item);
            }
            this.ComboBoxExPrinterName.Text = _printDocument.PrinterSettings.PrinterName;
            this.InitializeSheetView(SheetViewList);
            this.InitializeSheetViewList(SheetViewList);

            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;
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
            this.PutSheetViewList(SheetViewList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpreadList_CellDoubleClick(object sender, CellClickEventArgs e) {
            /*
             * CertificationCode��101/102/103/104/238�̏ꍇ�͕ʏ���������
             */
            if (e.Column > 6 && e.Row > 3 && ((CertificationMasterVo)SheetViewList.Cells[1, e.Column].Tag).CertificationCode != 238) {
                /*
                 * Tag����Vo���擾
                 */
                StaffMasterVo staffMasterVo = (StaffMasterVo)SheetViewList.Cells[e.Row, 1].Tag;
                CertificationMasterVo certificationMasterVo = (CertificationMasterVo)SheetViewList.Cells[1, e.Column].Tag;
                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;
                /*
                 * Form��\������
                 */
                CertificationDetail certificationDetail = new(_connectionVo, staffMasterVo.StaffCode, certificationMasterVo.CertificationCode);
                _screenForm.SetPosition(_screen, certificationDetail);
                certificationDetail.KeyPreview = true;
                certificationDetail.Show(this);

            } else {
                // �͈͊O�Ȃ̂ő���𒆒f����
                e.Cancel = true;
                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "�_�u���N���b�N�����Z���͔͈͊O�ł��B";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        private void InitializeSheetViewList(SheetView sheetView) {
            this.SpreadList.SuspendLayout(); // Spread �񊈐���
            /*
             * �����l
             */
            sheetView.ColumnCount = 3;
            sheetView.RowCount = 4;
            /*
             * Column��ǉ�����
             */
            int columnCount = sheetView.Columns.Count;
            foreach (CertificationMasterVo certificationMasterVo in _certificationMasterDao.SelectAllCertificationMaster()) {
                sheetView.Columns.Add(columnCount, 1);
                sheetView.Columns[columnCount].Width = 25;
                /*
                 * ���i�R�[�h
                 */
                sheetView.Cells[0, columnCount].Font = new Font("���S�V�b�N", 8);
                sheetView.Cells[0, columnCount].Text = certificationMasterVo.CertificationCode.ToString("###");
                /*
                 * ���i��
                 * �J�X�^���Z�����g�p���Ă���
                 */
                sheetView.Cells[1, columnCount].Tag = certificationMasterVo;
                //TextCellType textCellType = new();
                //textCellType.TextOrientation = TextOrientation.TextVertical;
                sheetView.Cells[1, columnCount].CellType = new VerticalTextCell();
                sheetView.Cells[1, columnCount].Font = new Font("���C���I", 9);
                //sheetView.Cells[1, columnCount].VerticalAlignment = CellVerticalAlignment.Top;
                sheetView.Cells[1, columnCount].Text = certificationMasterVo.CertificationDisplayName;
                /*
                 * �擾�v��l��
                 */
                sheetView.Cells[2, columnCount].Font = new Font("���S�V�b�N", 8);
                sheetView.Cells[2, columnCount].Text = certificationMasterVo.NumberOfAppointments.ToString("###");

                columnCount++;
            }

            /*
             * Row��ǉ�����
             */
            int rowCount = sheetView.RowCount;
            foreach (StaffMasterVo staffMasterVo in _staffMasterDao.SelectAllStaffMaster(new List<int> { 10, 11, 12, 14, 15, 22 },
                                                                                         new List<int> { 20, 22, 99 },
                                                                                         new List<int> { 10, 11, 20, 99 },
                                                                                         false).OrderBy(x => x.Belongs).ThenBy(x => x.NameKana)) {
                sheetView.Rows.Add(rowCount, 1);
                sheetView.Rows[rowCount].Height = 20;
                /*
                 * ����
                 */
                sheetView.Cells[rowCount, 0].Font = new Font("���S�V�b�N", 8);
                sheetView.Cells[rowCount, 0].Text = _dictionaryBelongs[staffMasterVo.Belongs];
                /*
                 * �]���Җ�
                 */
                sheetView.Cells[rowCount, 1].Tag = staffMasterVo;
                sheetView.Cells[rowCount, 1].Font = new Font("���S�V�b�N", 9);
                sheetView.Cells[rowCount, 1].Text = staffMasterVo.DisplayName;
                /*
                 * �N��
                 */
                sheetView.Cells[rowCount, 2].Text = string.Concat(_dateUtility.GetAge(staffMasterVo.BirthDate.Date), "��");

                rowCount++;
            }
            /*
             * �Œ�s���ݒ肷��
             */
            SheetViewList.FrozenRowCount = 4;
            SheetViewList.FrozenColumnCount = 3;

            this.SpreadList.ResumeLayout();                                         // Spread ������
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        private void PutSheetViewList(SheetView sheetView) {
            List<LicenseMasterVo> listLicenseMasterVo = _licenseMasterDao.SelectAllLicenseMaster();
            List<CertificationFileVo> listCertificationFileVo = _certificationFileDao.SelectAllCertificationFile();
            /*
             * Row��ǉ�����
             */
            for (int rowCount = 4; rowCount < sheetView.RowCount; rowCount++) {
                /*
                 * �Ƌ��敪
                 */
                LicenseMasterVo licenseMasterVo = listLicenseMasterVo.Find(x => x.StaffCode == ((StaffMasterVo)sheetView.Cells[rowCount, 1].Tag).StaffCode);
                if (licenseMasterVo is not null) {
                    // ��^
                    sheetView.Cells[rowCount, 3].ForeColor = Color.Red;
                    sheetView.Cells[rowCount, 3].Font = new Font("Yu Gothic UI", 12);
                    sheetView.Cells[rowCount, 3].HorizontalAlignment = CellHorizontalAlignment.Center;
                    sheetView.Cells[rowCount, 3].Text = licenseMasterVo.Large ? "�Z" : "";
                    sheetView.Cells[rowCount, 3].VerticalAlignment = CellVerticalAlignment.Center;
                    // ���^
                    sheetView.Cells[rowCount, 4].ForeColor = Color.Red;
                    sheetView.Cells[rowCount, 4].Font = new Font("Yu Gothic UI", 12);
                    sheetView.Cells[rowCount, 4].HorizontalAlignment = CellHorizontalAlignment.Center;
                    sheetView.Cells[rowCount, 4].Text = licenseMasterVo.Medium ? "�Z" : "";
                    sheetView.Cells[rowCount, 4].VerticalAlignment = CellVerticalAlignment.Center;
                    // �����^
                    sheetView.Cells[rowCount, 5].ForeColor = Color.Red;
                    sheetView.Cells[rowCount, 5].Font = new Font("Yu Gothic UI", 12);
                    sheetView.Cells[rowCount, 5].HorizontalAlignment = CellHorizontalAlignment.Center;
                    sheetView.Cells[rowCount, 5].Text = licenseMasterVo.QuasiMedium ? "�Z" : "";
                    sheetView.Cells[rowCount, 5].VerticalAlignment = CellVerticalAlignment.Center;
                    // ����
                    sheetView.Cells[rowCount, 6].ForeColor = Color.Red;
                    sheetView.Cells[rowCount, 6].Font = new Font("Yu Gothic UI", 12);
                    sheetView.Cells[rowCount, 6].HorizontalAlignment = CellHorizontalAlignment.Center;
                    sheetView.Cells[rowCount, 6].Text = licenseMasterVo.Ordinary ? "�Z" : "";
                    sheetView.Cells[rowCount, 6].VerticalAlignment = CellVerticalAlignment.Center;
                }
                /*
                 * ���i�敪
                 */
                for (int columnCount = 7; columnCount < sheetView.ColumnCount; columnCount++) {
                    /*
                     * 238:��ƈ��Ɩ��́A���ۃJ�[�h�̗L���Ŕ��f����
                     */
                    if (((CertificationMasterVo)sheetView.Cells[1, columnCount].Tag).CertificationCode == 238) {
                        /*
                         * ���ۏ���
                         */
                        if (_toukanpoTrainingCardDao.ExistenceToukanpoTrainingCardMaster(((StaffMasterVo)sheetView.Cells[rowCount, 1].Tag).StaffCode)) {
                            sheetView.Cells[rowCount, columnCount].Font = new Font("Yu Gothic UI", 12);
                            sheetView.Cells[rowCount, columnCount].ForeColor = Color.Blue;
                            sheetView.Cells[rowCount, columnCount].HorizontalAlignment = CellHorizontalAlignment.Center;
                            sheetView.Cells[rowCount, columnCount].Text = "�Z";
                            sheetView.Cells[rowCount, columnCount].VerticalAlignment = CellVerticalAlignment.Center;
                        } else {

                        }
                    } else {
                        /*
                         * ���̑�����
                         */
                        CertificationFileVo certificationFileVo = listCertificationFileVo.Find(x => x.StaffCode == ((StaffMasterVo)sheetView.Cells[rowCount, 1].Tag).StaffCode &&
                                                                                                    x.CertificationCode == ((CertificationMasterVo)sheetView.Cells[1, columnCount].Tag).CertificationCode);
                        if (certificationFileVo is not null) {
                            sheetView.Cells[rowCount, columnCount].Font = new Font("Yu Gothic UI", 12);
                            sheetView.Cells[rowCount, columnCount].ForeColor = Color.Blue;
                            sheetView.Cells[rowCount, columnCount].HorizontalAlignment = CellHorizontalAlignment.Center;
                            if (certificationFileVo.Picture1Flag || certificationFileVo.Picture2Flag) {
                                sheetView.Cells[rowCount, columnCount].Text = "�Z";
                            } else {
                                sheetView.Cells[rowCount, columnCount].Text = "�~";
                            }

                            sheetView.Cells[rowCount, columnCount].VerticalAlignment = CellVerticalAlignment.Center;
                        }
                    }
                }
                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(rowCount - 4, "��");
            }

            /*
             * ���i�擾�ϐl�����Z�o����
             */
            int checkCount;
            for (int col = 3; col < SheetViewList.ColumnCount; col++) {
                checkCount = 0;
                for (int row = 4; row < SheetViewList.RowCount; row++) {
                    if (sheetView.Cells[row, col].Text != string.Empty)
                        checkCount++;
                }
                sheetView.Cells[3, col].Font = new Font("Yu Gothic UI", 8);
                sheetView.Cells[3, col].Value = checkCount;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                /*
                 * B5�ň������
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
        /// InitializeSheetView
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns>SheetView</returns>
        private SheetView InitializeSheetView(SheetView sheetView) {
            this.SpreadList.AllowDragDrop = false;                                              // DrugDrop���֎~����
            this.SpreadList.TabStripPolicy = TabStripPolicy.Never;                              // �V�[�g�^�u���\���ɂ���
            SpreadList.PaintSelectionHeader = false;                                            // �w�b�_�̑I����Ԃ����Ȃ�
            SpreadList.TabStrip.DefaultSheetTab.Font = new Font("Yu Gothic UI", 9);
            sheetView.ColumnHeader.Rows[0].Height = 26;                                         // Column�w�b�_�̍���
            sheetView.GrayAreaBackColor = Color.White;
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9);                  // �s�w�b�_��Font
            sheetView.RowHeader.Columns[0].Width = 48;                                          // �s�w�b�_�̕���ύX���܂�
            return sheetView;
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
        private void CertificationList_FormClosing(object sender, FormClosingEventArgs e) {

        }

        /// <summary>
        /// �J�X�^���Z���N���X
        /// </summary>
        private class VerticalTextCell : FarPoint.Win.Spread.CellType.TextCellType {
            public override void PaintCell(Graphics g, Rectangle r, FarPoint.Win.Spread.Appearance appearance, object value, bool isSelected, bool isLocked, float zoomFactor) {
                Brush backColorBrush = new SolidBrush(appearance.BackColor);
                Brush foreColorBrush = new SolidBrush(appearance.ForeColor);
                StringFormat S = new StringFormat();
                S.FormatFlags = StringFormatFlags.DirectionVertical;

                g.FillRectangle(backColorBrush, r);
                g.DrawString((string)value, appearance.Font, foreColorBrush, r, S);

                backColorBrush.Dispose();
                foreColorBrush.Dispose();
            }
        }
    }
}
