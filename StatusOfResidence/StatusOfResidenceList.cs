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
        /// 従事者名
        /// </summary>
        private const int _colStaffName = 0;
        /// <summary>
        /// 従事者名カナ
        /// </summary>
        private const int _colStaffNameKana = 1;
        /// <summary>
        /// 生年月日
        /// </summary>
        private const int _colBirthDate = 2;
        /// <summary>
        /// 性別
        /// </summary>
        private const int _colGender = 3;
        /// <summary>
        /// 国籍・地域
        /// </summary>
        private const int _colNationality = 4;
        /// <summary>
        /// 住居地
        /// </summary>
        private const int _colAddress = 5;
        /// <summary>
        /// 在留資格
        /// </summary>
        private const int _colStatusOfResidence = 6;
        /// <summary>
        /// 就労制限の有無
        /// </summary>
        private const int _colWorkLimit = 7;
        /// <summary>
        /// 在留期間
        /// </summary>
        private const int _colPeriodDate = 8;
        /// <summary>
        /// 有効期限
        /// </summary>
        private const int _colDeadlineDate = 9;

        /// <summary>
        /// コンストラクター
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
             * InitializeControl
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
             * Eventを登録する
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
                case "ToolStripMenuItemExit": // アプリケーションを終了する
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
            // Spread 非活性化
            this.SpreadList.SuspendLayout();
            // 先頭行（列）インデックスを取得
            _spreadListTopRow = this.SpreadList.GetViewportTopRow(0);
            /*
             * Rowを削除する
             */
            if (this.SheetViewList.Rows.Count > 0)
                this.SheetViewList.RemoveRows(0, this.SheetViewList.Rows.Count);
            foreach (StatusOfResidenceMasterVo statusOfResidenceMasterVo in listStatusOfResidenceMasterVo.OrderBy(x => x.DeadlineDate)) {
                this.SheetViewList.Rows.Add(rowCount, 1);
                this.SheetViewList.RowHeader.Columns[0].Label = (rowCount + 1).ToString();                                          // Rowヘッダ
                this.SheetViewList.Rows[rowCount].ForeColor = statusOfResidenceMasterVo.RetirementFlag ? Color.Red : Color.Black;   // 退職済のレコードのForeColorをセット
                this.SheetViewList.Rows[rowCount].Tag = statusOfResidenceMasterVo;
                this.SheetViewList.Cells[rowCount, _colStaffName].Text = statusOfResidenceMasterVo.StaffName;                       // 従事者名
                this.SheetViewList.Cells[rowCount, _colStaffNameKana].Text = statusOfResidenceMasterVo.StaffNameKana;               // 従事者名カナ
                this.SheetViewList.Cells[rowCount, _colBirthDate].Value = statusOfResidenceMasterVo.BirthDate;                      // 生年月日
                this.SheetViewList.Cells[rowCount, _colGender].Text = statusOfResidenceMasterVo.Gender;                             // 性別
                this.SheetViewList.Cells[rowCount, _colNationality].Text = statusOfResidenceMasterVo.Nationality;                   // 国籍・地域
                this.SheetViewList.Cells[rowCount, _colAddress].Text = statusOfResidenceMasterVo.Address;                           // 住居地
                this.SheetViewList.Cells[rowCount, _colStatusOfResidence].Text = statusOfResidenceMasterVo.StatusOfResidence;       // 在留資格
                this.SheetViewList.Cells[rowCount, _colWorkLimit].Text = statusOfResidenceMasterVo.WorkLimit;                       // 就労制限の有無
                this.SheetViewList.Cells[rowCount, _colPeriodDate].Value = statusOfResidenceMasterVo.PeriodDate;                    // 在留期間
                this.SheetViewList.Cells[rowCount, _colDeadlineDate].Value = statusOfResidenceMasterVo.DeadlineDate;                // 有効期限
                rowCount++;
            }

            // 先頭行（列）インデックスをセット
            this.SpreadList.SetViewportTopRow(0, _spreadListTopRow);
            // Spread 活性化
            this.SpreadList.ResumeLayout();
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(" ", rowCount, " 件");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpreadList_CellDoubleClick(object sender, CellClickEventArgs e) {
            /*
             * ヘッダーのDoubleClickを回避
             */
            if (e.ColumnHeader)
                return;
            /*
             * StatusOfResidenceDetailを表示する
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
            SpreadList.AllowDragDrop = false; // DrugDropを禁止する
            SpreadList.PaintSelectionHeader = false; // ヘッダの選択状態をしない
            SpreadList.TabStrip.DefaultSheetTab.Font = new Font("Yu Gothic UI", 9);
            SpreadList.TabStripPolicy = TabStripPolicy.Never; // シートタブを非表示
            sheetView.AlternatingRows.Count = 2; // 行スタイルを２行単位とします
            sheetView.AlternatingRows[0].BackColor = Color.WhiteSmoke; // 1行目の背景色を設定します
            sheetView.AlternatingRows[1].BackColor = Color.White; // 2行目の背景色を設定します
            sheetView.ColumnHeader.Rows[0].Height = 22; // Columnヘッダの高さ
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
        private void StatusOfResidenceList_FormClosing(object sender, FormClosingEventArgs e) {
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
