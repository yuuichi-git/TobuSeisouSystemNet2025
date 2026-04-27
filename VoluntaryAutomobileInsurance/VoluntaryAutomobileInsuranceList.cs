/*
 * 2026-04-04
 * 任意保険加入状況の一覧画面
 */
using CcControl;

using Common;

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
        private readonly ScreenForm _screenForm = new();
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
            /*
             * MenuStrip
             */
            List<string> listString = new() {
                "ToolStripMenuItemFile",
                "ToolStripMenuItemExit",
                "ToolStripMenuItemHelp"
            };
            this.CcMenuStrip1.ChangeEnable(listString);

            this.InitializeSheetView(this.SheetViewList);
            /*
             * Eventを登録する
             */
            this.CcMenuStrip1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemExit":                                                                   // アプリケーションを終了する
                    this.Close();
                    break;
            }
        }

        private void ButtonExUpdate_Click(object sender, EventArgs e) {
            switch (((CcButton)sender).Name) {
                case "ButtonExUpdate":
                    try {
                        this.PutSheetViewList(_voluntaryAutomobileInsuranceDao.SelectStaffWithVoluntaryInsurance(CreateArray(GroupBoxExBelongs),
                                                                                                                 CreateArray(GroupBoxExJobForm),
                                                                                                                 CreateArray(GroupBoxExOccupation),
                                                                                                                 this.CheckBoxExRetirementFlag.Checked));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
            }
        }

        int _spreadListTopRow = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listData"></param>
        private void PutSheetViewList(List<(StaffMasterVo staff, VoluntaryAutomobileInsuranceVo insurance, string belongsName, string occupationName, string jobFormName)> listData) {
            SheetView sheetView = this.SheetViewList;
            this.SpreadList.SuspendLayout();// Spread 非活性化
            _spreadListTopRow = this.SpreadList.GetViewportTopRow(0);// 先頭行（列）インデックスを取得

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
                sheetView.Cells[row, _colDisplayName].Tag = staff.StaffCode;

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
                sheetView.Cells[row, _colRoutePdf].Value = insurance.HasImage1 ? "✓" : string.Empty;

                // --- 15: 自賠責PDF（HasImage2）---
                sheetView.Cells[row, _colCompulsoryPdf].Value = insurance.HasImage2 ? "✓" : string.Empty;

                // --- 16: 任意保険PDF（HasImage3）---
                sheetView.Cells[row, _colVoluntaryPdf].Value = insurance.HasImage3 ? "✓" : string.Empty;

                sheetView.Cells[row, _colAuthorizedVehiclePdf].Value = insurance.HasImage4 ? "✓" : string.Empty;

                // --- 期限切れチェック ---
                DateTime endDate;
                if (DateTime.TryParse(insurance.EndDate, out endDate)) {
                    if (endDate.Date < DateTime.Today) {
                        // 行全体を赤色にする
                        sheetView.Rows[row].BackColor = Color.LightCoral;   // 目に優しい赤
                                                                            // sheetView.Rows[row].ForeColor = Color.White;     // 必要なら文字色も変更
                    }
                }
            }
            // 先頭行（列）インデックスをセット
            this.SpreadList.SetViewportTopRow(0, _spreadListTopRow);
            // Spread 活性化
            this.SpreadList.ResumeLayout();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpreadList_CellDoubleClick(object sender, CellClickEventArgs e) {
            if (e.ColumnHeader)                                                             // ヘッダーのDoubleClickを回避
                return;
            object? tag = SheetViewList.Cells[e.Row, _colDisplayName].Tag;                  // 行の Tag を取得            
            if (tag is not int staffCode)
                return;
            VoluntaryAutomobileInsuranceDetail voluntaryAutomobileInsuranceDetail = new(_connectionVo, staffCode);
            _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), voluntaryAutomobileInsuranceDetail);
            voluntaryAutomobileInsuranceDetail.Show(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns></returns>
        private SheetView InitializeSheetView(SheetView sheetView) {
            SpreadList.AllowDragDrop = false;                                               // DrugDropを禁止する
            SpreadList.PaintSelectionHeader = false;                                        // ヘッダの選択状態をしない
            SpreadList.TabStrip.DefaultSheetTab.Font = new Font("Yu Gothic UI", 9);
            SpreadList.TabStripPolicy = TabStripPolicy.Never;                               // シートタブを非表示

            sheetView.ColumnHeader.Rows[0].Height = 26;                                     // Columnヘッダの高さ
            sheetView.GrayAreaBackColor = Color.White;
            sheetView.HorizontalGridLine = new GridLine(GridLineType.Flat);
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9);              // 行ヘッダのFont
            sheetView.RowHeader.Columns[0].Resizable = false;                               // 行ヘッダの幅を変更できないようにします
            sheetView.RowHeader.Columns[0].Width = 28;                                      // 行ヘッダの幅を変更します
            sheetView.VerticalGridLine = new GridLine(GridLineType.Flat, Color.LightGray);  // 縦のグリッド線を薄いグレーに設定
            sheetView.RemoveRows(0, sheetView.Rows.Count);

            return sheetView;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupBoxEx"></param>
        /// <returns></returns>
        private List<int> CreateArray(CcGroupBox groupBoxEx) {
            List<int> list = new();
            foreach (CcCheckBox checkBoxEx in groupBoxEx.Controls) {
                if (checkBoxEx.Checked)
                    list.Add(Convert.ToInt32(checkBoxEx.Tag));
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VoluntaryAutomobileInsuranceList_FormClosing(object sender, FormClosingEventArgs e) {
            DialogResult dialogResult = MessageBox.Show("アプリケーションを終了します。よろしいですか？", "メッセージ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
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
