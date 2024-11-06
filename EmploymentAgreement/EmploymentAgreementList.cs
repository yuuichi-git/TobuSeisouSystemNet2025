/*
 * 2024-11-03
 */
using System.Data;

using Common;

using ControlEx;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace EmploymentAgreement {
    public partial class EmploymentAgreementList : Form {
        private readonly Dictionary<int, string> _dictionaryBelongs = new() { { 10, "役員" }, { 11, "社員" }, { 12, "アルバイト" }, { 13, "派遣" }, { 20, "新運転" }, { 21, "自運労" }, { 99, "" } };
        private readonly Dictionary<int, string> _dictionaryOccupation = new() { { 10, "運転手" }, { 11, "作業員" }, { 20, "事務職" }, { 99, "" } };
        private readonly Dictionary<int, string> _dictionaryJobForm = new() { { 10, "長期雇用" }, { 11, "短期雇用" }, { 99, "" } };

        /// <summary>
        /// 所属
        /// </summary>
        private const int _colBelongs = 0;
        /// <summary>
        /// 職種
        /// </summary>
        private const int _colOccupation = 1;
        /// <summary>
        /// 雇用形態
        /// </summary>
        private const int _colJobForm = 2;
        /// <summary>
        /// 組合№
        /// </summary>
        private const int _colUnionCode = 3;
        /// <summary>
        /// 氏名
        /// </summary>
        private const int _colDisplayName = 4;
        /// <summary>
        /// 生年月日
        /// </summary>
        private const int _colBirthDate = 5;
        /// <summary>
        /// 年齢
        /// </summary>
        private const int _colAge = 6;
        /// <summary>
        /// 入社年月日
        /// </summary>
        private const int _colEmplomentDate = 7;
        /// <summary>
        /// 契約期間
        /// </summary>
        private const int _colContractExpirationPeriod = 8;
        /// <summary>
        /// 体験入社日
        /// </summary>
        private const int _colExperienceDate = 9;
        /// <summary>
        /// 長期契約日１
        /// </summary>
        private const int _colLongContractExpirationDate1 = 10;
        /// <summary>
        /// 長期契約日２
        /// </summary>
        private const int _colLongContractExpirationDate2 = 11;
        /// <summary>
        /// 長期契約日３
        /// </summary>
        private const int _colLongContractExpirationDate3 = 12;
        /// <summary>
        /// 長期契約日４
        /// </summary>
        private const int _colLongContractExpirationDate4 = 13;
        /// <summary>
        /// 短期契約日１
        /// </summary>
        private const int _colShortContractExpirationDate1 = 14;
        /// <summary>
        /// 短期契約日２
        /// </summary>
        private const int _colShortContractExpirationDate2 = 15;
        /// <summary>
        /// 短期契約日３
        /// </summary>
        private const int _colShortContractExpirationDate3 = 16;
        /// <summary>
        /// 短期契約日４
        /// </summary>
        private const int _colShortContractExpirationDate4 = 17;
        /// <summary>
        /// 誓約書
        /// </summary>
        private const int _colWrittenPledge = 18;
        /// <summary>
        /// 失墜行為
        /// </summary>
        private const int _colLossWrittenPledge = 19;
        /// <summary>
        /// 契約満了通知
        /// </summary>
        private const int _colContractExpirationNotice = 20;

        /*
         * インスタンス作成
         */
        private readonly Screen _screen;
        private readonly ScreenForm _screenForm = new();
        /*
         * Dao
         */
        private StaffMasterDao _staffMasterDao;
        private EmploymentAgreementDao _employmentAgreementDao;
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;
        private List<StaffMasterVo> _listStaffMasterVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public EmploymentAgreementList(ConnectionVo connectionVo, Screen screen) {
            /*
             * インスタンス作成
             */
            _screen = screen;
            /*
             * Dao
             */
            _staffMasterDao = new(connectionVo);
            _employmentAgreementDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            /*
             * InitializeControl
             */
            InitializeComponent();
            this.CheckBoxExRetirementFlag.Checked = false;
            this.InitializeSheetView(this.SheetViewList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEx_Click(object sender, EventArgs e) {
            switch (((ButtonEx)sender).Name) {
                case "ButtonExUpdate":
                    try {
                        this.AddSheetViewList(_staffMasterDao.SelectAllStaffMaster());
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
            }
        }

        int spreadListTopRow = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listStaffMasterVo"></param>
        private void AddSheetViewList(List<StaffMasterVo> listStaffMasterVo) {
            /*
             * Filter
             */
            listStaffMasterVo = listStaffMasterVo.FindAll(x => x.Belongs == 12 || x.Belongs == 20 || x.Belongs == 21);
            listStaffMasterVo = listStaffMasterVo.FindAll(x => x.JobForm == 10 || x.JobForm == 12 || x.JobForm == 99);
            listStaffMasterVo = listStaffMasterVo.FindAll(x => x.Occupation == 10 || x.Occupation == 11 || x.Occupation == 99);
            if (!CheckBoxExRetirementFlag.Checked)
                listStaffMasterVo = listStaffMasterVo.FindAll(x => x.RetirementFlag == false);
            /*
             * Sort
             */
            var orderListStaffMasterVo = listStaffMasterVo.OrderBy(x => x.Belongs).ThenBy(x => x.UnionCode);

            int rowCount = 0;
            // Spread 非活性化
            SpreadList.SuspendLayout();
            // 先頭行（列）インデックスを取得
            spreadListTopRow = SpreadList.GetViewportTopRow(0);
            // Rowを削除する
            if (SheetViewList.Rows.Count > 0)
                SheetViewList.RemoveRows(0, SheetViewList.Rows.Count);
            foreach (StaffMasterVo staffMasterVo in orderListStaffMasterVo) {
                SheetViewList.Rows.Add(rowCount, 1);
                SheetViewList.RowHeader.Columns[0].Label = (rowCount + 1).ToString(); // Rowヘッダ
                SheetViewList.Rows[rowCount].ForeColor = Color.Black;
                SheetViewList.Rows[rowCount].Height = 20; // Rowの高さ
                SheetViewList.Rows[rowCount].Resizable = false; // RowのResizableを禁止

                SheetViewList.Cells[rowCount, _colBelongs].Text = _dictionaryBelongs[staffMasterVo.Belongs];
                SheetViewList.Cells[rowCount, _colOccupation].Text = _dictionaryOccupation[staffMasterVo.Occupation];
                SheetViewList.Cells[rowCount, _colJobForm].Text = _dictionaryJobForm[staffMasterVo.JobForm];
                SheetViewList.Cells[rowCount, _colUnionCode].Text = staffMasterVo.UnionCode > 0 ? staffMasterVo.UnionCode.ToString() : "";
                SheetViewList.Cells[rowCount, _colDisplayName].Text = staffMasterVo.DisplayName;
                SheetViewList.Cells[rowCount, _colBirthDate].Value = staffMasterVo.BirthDate;
                SheetViewList.Cells[rowCount, _colEmplomentDate].Value = staffMasterVo.EmploymentDate;

                EmploymentAgreementVo employmentAgreementVo = _employmentAgreementDao.SelectOneEmploymentAgreement(staffMasterVo.StaffCode);
                if (employmentAgreementVo != null) {
                    SheetViewList.Cells[rowCount, _colContractExpirationPeriod].Value = employmentAgreementVo.ContractExpirationPeriod;
                    SheetViewList.Cells[rowCount, _colExperienceDate].Value = employmentAgreementVo.ExperienceStartDate;
                }



                SheetViewList.Rows[rowCount].Tag = staffMasterVo;
                rowCount++;
            }
            // 先頭行（列）インデックスをセット
            SpreadList.SetViewportTopRow(0, spreadListTopRow);
            // Spread 活性化
            SpreadList.ResumeLayout();
            StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(" ", rowCount, " 件");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns></returns>
        private SheetView InitializeSheetView(SheetView sheetView) {
            SpreadList.AllowDragDrop = false; // DrugDropを禁止する
            SpreadList.PaintSelectionHeader = false; // ヘッダの選択状態をしない
            SpreadList.TabStrip.DefaultSheetTab.Font = new Font("Yu Gothic UI", 9);
            sheetView.AlternatingRows.Count = 2; // 行スタイルを２行単位とします
            sheetView.AlternatingRows[0].BackColor = Color.WhiteSmoke; // 1行目の背景色を設定します
            sheetView.AlternatingRows[1].BackColor = Color.White; // 2行目の背景色を設定します
            sheetView.ColumnHeader.Rows[0].Height = 26; // Columnヘッダの高さ
            sheetView.GrayAreaBackColor = Color.White;
            sheetView.HorizontalGridLine = new GridLine(GridLineType.None);
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9); // 行ヘッダのFont
            sheetView.RowHeader.Columns[0].Width = 48; // 行ヘッダの幅を変更します
            sheetView.VerticalGridLine = new GridLine(GridLineType.Flat, Color.LightGray);
            sheetView.RemoveRows(0, sheetView.Rows.Count);
            return sheetView;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpreadList_CellDoubleClick(object sender, CellClickEventArgs e) {
            // ヘッダーのDoubleClickを回避
            if (e.ColumnHeader)
                return;
            EmploymentAgreementDetail employmentAgreementDetail = new(_connectionVo, (StaffMasterVo)SheetViewList.Rows[e.Row].Tag);
            _screenForm.SetPosition(_screen, employmentAgreementDetail);
            employmentAgreementDetail.ShowDialog(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmploymentAgreementList_FormClosing(object sender, FormClosingEventArgs e) {

        }


    }
}
