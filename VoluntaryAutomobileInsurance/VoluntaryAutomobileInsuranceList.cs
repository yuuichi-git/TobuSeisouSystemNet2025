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
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
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
            List<string> listString = new() {"ToolStripMenuItemFile",
                                             "ToolStripMenuItemExit",
                                             "ToolStripMenuItemHelp"};
            this.CcMenuStrip1.ChangeEnable(listString);
            this.CcMenuStrip1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;

            this.InitializeSheetView(this.SheetViewList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CcContextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
            if(this.SheetViewList.ActiveRowIndex < 0) {                                                             // ActiveRowIndexが0未満の場合、ContextMenuStripExを表示しない
                e.Cancel = true;
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch(((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemDelete":                                                                     // DeleteItem
                    DialogResult dialogResult = MessageBox.Show("選択した項目を削除します。よろしいですか？", "メッセージ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    switch(dialogResult) {
                        case DialogResult.OK:
                            try {
                                // 選択した行のTagに格納されているVoluntaryAutomobileInsuranceVoのIdを取得して削除
                                _voluntaryAutomobileInsuranceDao.DeleteOneVoluntaryAutomobileInsuranceVo(((VoluntaryAutomobileInsuranceVo)SheetViewList.Rows[this.SheetViewList.ActiveRowIndex].Tag).Id);
                            } catch(Exception exception) {
                                MessageBox.Show(exception.Message);
                            }
                            break;
                        case DialogResult.Cancel:
                            break;
                    }
                    break;
                case "ToolStripMenuItemExit":                                                                       // アプリケーションを終了する
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExUpdate_Click(object sender, EventArgs e) {
            switch(((CcButton)sender).Name) {
                case "ButtonExUpdate":
                    try {
                        this.PutSheetViewList(_voluntaryAutomobileInsuranceDao.SelectStaffWithVoluntaryInsurance(this.GroupBoxExBelongs.CreateArray(GroupBoxExBelongs),
                                                                                                                 this.GroupBoxExJobForm.CreateArray(GroupBoxExJobForm),
                                                                                                                 this.GroupBoxExOccupation.CreateArray(GroupBoxExOccupation),
                                                                                                                 this.CheckBoxExRetirementFlag.Checked));
                    } catch(Exception exception) {
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
        private void PutSheetViewList(List<(StaffMasterVo staffMasterVo, VoluntaryAutomobileInsuranceVo voluntaryAutomobileInsuranceVo, string belongsName, string occupationName, string jobFormName)> listData) {
            SheetView sheetView = this.SheetViewList;
            this.SpreadList.SuspendLayout();                                                                                            // Spread 非活性化
            _spreadListTopRow = this.SpreadList.GetViewportTopRow(0);                                                                   // 先頭行（列）インデックスを取得

            if(sheetView.Rows.Count > 0)                                                                                                // Rowを削除する
                sheetView.RemoveRows(0, sheetView.Rows.Count);

            int row = 0;
            foreach(var (staffMasterVo, voluntaryAutomobileInsuranceVo, belongsName, occupationName, jobFormName) in listData) {
                sheetView.Rows.Add(row, 1);
                sheetView.Rows[row].Tag = voluntaryAutomobileInsuranceVo;                                                              // TagにvoluntaryAutomobileInsuranceVo をセット

                sheetView.Cells[row, _colBelongs].Value = belongsName;
                sheetView.Cells[row, _colOccupation].Value = occupationName;
                sheetView.Cells[row, _colJobForm].Value = jobFormName;

                // --- 3: 組合№ ---
                sheetView.Cells[row, _colUnionCode].Value = staffMasterVo.UnionCode;

                // --- 4: 氏名 ---
                sheetView.Cells[row, _colDisplayName].Value = staffMasterVo.DisplayName;
                sheetView.Cells[row, _colDisplayName].Tag = staffMasterVo.StaffCode;

                // --- 5: カナ ---
                sheetView.Cells[row, _colNameKana].Value = staffMasterVo.NameKana;

                // --- 6: 生年月日 ---
                sheetView.Cells[row, _colBirthDate].Value = staffMasterVo.BirthDate.ToString("yyyy/MM/dd");

                // --- 7: 年齢（補助メソッド使用）---
                sheetView.Cells[row, _colAge].Value = _dateUtility.GetAge(staffMasterVo.BirthDate);

                // --- 8: 入社年月日 ---
                // --- 9: 契約期間（月数）---
                if(staffMasterVo.EmploymentDate.Date == new DateTime(1900, 1, 1)) {
                    // 1900-01-01 の場合は表示しない
                    sheetView.Cells[row, _colEmplomentDate].Value = string.Empty;
                    sheetView.Cells[row, _colContractExpirationPeriod].Value = "入社日未入力";
                } else {
                    sheetView.Cells[row, _colEmplomentDate].Value = staffMasterVo.EmploymentDate.ToString("yyyy/MM/dd");
                    sheetView.Cells[row, _colContractExpirationPeriod].Value = string.Concat(_dateUtility.GetEmploymenteYear(staffMasterVo.EmploymentDate.Date).ToString("#0年"),
                                                                                             _dateUtility.GetEmploymenteMonth(staffMasterVo.EmploymentDate.Date).ToString("00月"));
                }

                // --- 10: 対象車両種別 ---
                sheetView.Cells[row, _colVehicleType].Value = voluntaryAutomobileInsuranceVo.VehicleType;

                // --- 11: 保険会社名 ---
                sheetView.Cells[row, _colCompanyName].Value = voluntaryAutomobileInsuranceVo.CompanyName;

                // --- 12: 保険開始日 ---
                if(DateTime.TryParse(voluntaryAutomobileInsuranceVo.StartDate, out DateTime start)) {                       // 開始日
                    if(start.Date == _defaultDateTime.Date) {
                        sheetView.Cells[row, _colStartDate].Value = string.Empty;
                    } else {
                        sheetView.Cells[row, _colStartDate].Value = voluntaryAutomobileInsuranceVo.StartDate;
                    }
                }

                // --- 13: 保険終了日 ---
                if(DateTime.TryParse(voluntaryAutomobileInsuranceVo.EndDate, out DateTime end)) {                           // 終了日
                    if(end.Date == _defaultDateTime.Date) {
                        sheetView.Cells[row, _colEndDate].Value = string.Empty;
                    } else {
                        sheetView.Cells[row, _colEndDate].Value = voluntaryAutomobileInsuranceVo.EndDate;
                    }
                }

                // --- 14: 経路図PDF（HasImage1）---
                sheetView.Cells[row, _colRoutePdf].Value = voluntaryAutomobileInsuranceVo.HasImage1 ? "✓" : string.Empty;

                // --- 15: 自賠責PDF（HasImage2）---
                sheetView.Cells[row, _colCompulsoryPdf].Value = voluntaryAutomobileInsuranceVo.HasImage2 ? "✓" : string.Empty;

                // --- 16: 任意保険PDF（HasImage3）---
                sheetView.Cells[row, _colVoluntaryPdf].Value = voluntaryAutomobileInsuranceVo.HasImage3 ? "✓" : string.Empty;

                sheetView.Cells[row, _colAuthorizedVehiclePdf].Value = voluntaryAutomobileInsuranceVo.HasImage4 ? "✓" : string.Empty;

                // --- 期限切れチェック ---
                DateTime endDate;
                if(DateTime.TryParse(voluntaryAutomobileInsuranceVo.EndDate, out endDate)) {
                    if(endDate.Date != _defaultDateTime.Date && endDate.Date < DateTime.Today) {
                        // 行全体を赤色にする
                        sheetView.Rows[row].BackColor = Color.LightCoral;                                                               // 目に優しい赤
                                                                                                                                        // sheetView.Rows[row].ForeColor = Color.White;     // 必要なら文字色も変更
                    }
                }
                row++;
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
            if(e.ColumnHeader)                                                              // ヘッダーのDoubleClickを回避
                return;
            object? tag = this.SheetViewList.Rows[e.Row].Tag;                  // 行の Tag を取得            
            if(tag is not VoluntaryAutomobileInsuranceVo voluntaryAutomobileInsuranceVo)
                return;
            VoluntaryAutomobileInsuranceDetail voluntaryAutomobileInsuranceDetail = new(_connectionVo, voluntaryAutomobileInsuranceVo);
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VoluntaryAutomobileInsuranceList_FormClosing(object sender, FormClosingEventArgs e) {
            DialogResult dialogResult = MessageBox.Show("アプリケーションを終了します。よろしいですか？", "メッセージ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            switch(dialogResult) {
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
