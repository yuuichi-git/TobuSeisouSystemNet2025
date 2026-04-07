/*
 * 2026-04-04
 * 任意保険加入状況の一覧画面
 */
using Common;

using CcControl;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace VoluntaryAutomobileInsurance {
    public partial class VoluntaryAutomobileInsuranceList : Form {
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
        /// カナ
        /// </summary>
        private const int _colNameKana = 5;
        /// <summary>
        /// 生年月日
        /// </summary>
        private const int _colBirthDate = 6;
        /// <summary>
        /// 年齢
        /// </summary>
        private const int _colAge = 7;
        /// <summary>
        /// 入社年月日
        /// </summary>
        private const int _colEmplomentDate = 8;
        /// <summary>
        /// 契約期間
        /// </summary>
        private const int _colContractExpirationPeriod = 9;
        /// <summary>対象車両種別</summary>
        private const int _colVehicleType = 10;
        /// <summary>保険会社名</summary>
        private const int _colCompanyName = 11;
        /// <summary>保険開始日</summary>
        private const int _colStartDate = 12;
        /// <summary>保険終了日</summary>
        private const int _colEndDate = 13;
        /// <summary>経路図PDF（HasImage1）</summary>
        private const int _colRoutePdf = 14;
        /// <summary>自賠責PDF（HasImage2）</summary>
        private const int _colCompulsoryPdf = 15;
        /// <summary>任意保険PDF（HasImage3）</summary>
        private const int _colVoluntaryPdf = 16;
        /// <summary>通勤許可証PDF（HasImage4）</summary>
        private const int _colAuthorizedVehiclePdf = 17;

        /*
         * インスタンス作成
         */
        private readonly Screen _screen;
        private readonly DateUtility _dateUtility = new();
        /*
         * Dao
         */
        private VoluntaryAutomobileInsuranceDao _voluntaryAutomobileInsuranceDao;
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;

        public VoluntaryAutomobileInsuranceList(ConnectionVo connectionVo, Screen screen) {
            /*
             * インスタンス作成
             */
            _screen = screen;
            /*
             * Dao
             */
            _voluntaryAutomobileInsuranceDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            /*
             * InitializeControl
             */
            InitializeComponent();
            this.InitializeSheetView(this.SheetViewList);
        }

        private void ButtonExUpdate_Click(object sender, EventArgs e) {
            switch (((CcButton)sender).Name) {
                case "ButtonExUpdate":
                    try {
                        this.AddSheetViewList(_voluntaryAutomobileInsuranceDao.SelectStaffWithVoluntaryInsurance(new List<int> { 11, 12, 13, 14, 15, 22 },
                                                                                                                 new List<int> { 20, 22, 99 },                // 新運転長期・自運労長期・指定なし
                                                                                                                 new List<int> { 10, 11, 12, 13, 20, 99 },    // 運転手・作業員・自転車駐輪場・リサイクルセンター・事務員・指定なし
                                                                                                                 false));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
            }
        }

        private void AddSheetViewList(List<(StaffMasterVo staff, VoluntaryAutomobileInsuranceVo insurance, string belongsName, string occupationName, string jobFormName)> listData) {
            SheetView sheetView = this.SheetViewList;

            sheetView.RowCount = 0;

            foreach (var (staff, insurance, belongsName, occupationName, jobFormName) in listData) {
                int row = sheetView.RowCount++;

                sheetView.Cells[row, _colBelongs].Value = belongsName;
                sheetView.Cells[row, _colOccupation].Value = occupationName;
                sheetView.Cells[row, _colJobForm].Value = jobFormName;

                // --- 3: 組合№ ---
                sheetView.Cells[row, _colUnionCode].Value = staff.UnionCode;

                // --- 4: 氏名 ---
                sheetView.Cells[row, _colDisplayName].Value = staff.DisplayName;

                // --- 5: カナ ---
                sheetView.Cells[row, _colNameKana].Value = staff.NameKana;

                // --- 6: 生年月日 ---
                sheetView.Cells[row, _colBirthDate].Value = staff.BirthDate.ToString("yyyy/MM/dd");

                // --- 7: 年齢（補助メソッド使用）---
                sheetView.Cells[row, _colAge].Value = _dateUtility.GetAge(staff.BirthDate);

                // --- 8: 入社年月日 ---
                // --- 9: 契約期間（月数）---
                if (staff.EmploymentDate.Date == new DateTime(1900, 1, 1)) {
                    // 1900-01-01 の場合は表示しない
                    sheetView.Cells[row, _colEmplomentDate].Value = string.Empty;
                    sheetView.Cells[row, _colContractExpirationPeriod].Value = "入社日未入力";
                } else {
                    sheetView.Cells[row, _colEmplomentDate].Value = staff.EmploymentDate.ToString("yyyy/MM/dd");
                    sheetView.Cells[row, _colContractExpirationPeriod].Value = string.Concat(_dateUtility.GetEmploymenteYear(staff.EmploymentDate.Date).ToString("#0年"),
                                                                                             _dateUtility.GetEmploymenteMonth(staff.EmploymentDate.Date).ToString("00月"));
                }

                // --- 10: 対象車両種別 ---
                sheetView.Cells[row, _colVehicleType].Value = insurance.VehicleType;

                // --- 11: 保険会社名 ---
                sheetView.Cells[row, _colCompanyName].Value = insurance.CompanyName;

                // --- 12: 保険開始日 ---
                sheetView.Cells[row, _colStartDate].Value = insurance.StartDate;

                // --- 13: 保険終了日 ---
                sheetView.Cells[row, _colEndDate].Value = insurance.EndDate;

                // --- 14: 経路図PDF（HasImage1）---
                var button1 = new FarPoint.Win.Spread.CellType.ButtonCellType();
                button1.Text = insurance.HasImage1 ? "✓" : string.Empty;
                sheetView.Cells[row, _colRoutePdf].CellType = button1;
                sheetView.Cells[row, _colRoutePdf].Value = insurance.HasImage1;

                // --- 15: 自賠責PDF（HasImage2）---
                var button2 = new FarPoint.Win.Spread.CellType.ButtonCellType();
                button2.Text = insurance.HasImage1 ? "✓" : string.Empty;
                sheetView.Cells[row, _colCompulsoryPdf].CellType = button2;
                sheetView.Cells[row, _colCompulsoryPdf].Value = insurance.HasImage2;

                // --- 16: 任意保険PDF（HasImage3）---
                var button3 = new FarPoint.Win.Spread.CellType.ButtonCellType();
                button3.Text = insurance.HasImage1 ? "✓" : string.Empty;
                sheetView.Cells[row, _colVoluntaryPdf].CellType = button3;
                sheetView.Cells[row, _colVoluntaryPdf].Value = insurance.HasImage3;
            }
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
            SpreadList.TabStripPolicy = TabStripPolicy.Never; // シートタブを非表示

            sheetView.ColumnHeader.Rows[0].Height = 26; // Columnヘッダの高さ
            sheetView.GrayAreaBackColor = Color.White;
            sheetView.HorizontalGridLine = new GridLine(GridLineType.Flat);
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9); // 行ヘッダのFont
            sheetView.RowHeader.Columns[0].Width = 28; // 行ヘッダの幅を変更します
            sheetView.VerticalGridLine = new GridLine(GridLineType.Flat, Color.LightGray);
            sheetView.RemoveRows(0, sheetView.Rows.Count);

            return sheetView;
        }

        
    }
}
