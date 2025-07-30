/*
 * 2025-07-30
 */
using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace Collection {
    public partial class CollectionStaffsTaitou : Form {
        /*
         * Dao
         */
        private readonly CollectionTaitouDao _collectionTaitouDao;

        /// <summary>
        /// Key0 → ４月の位置を格納
        /// Key1 → ５月の位置を格納
        /// Key2 → ６月の位置を格納
        /// Key3 → ７月の位置を格納
        /// Key4 → ８月の位置を格納
        /// Key5 → ９月の位置を格納
        /// Key6 → １０月の位置を格納
        /// Key7 → １１月の位置を格納
        /// Key8 → １２月の位置を格納
        /// Key9 → １月の位置を格納(次年度)
        /// Key10 → ２月の位置を格納(次年度)
        /// Key11 → ３月の位置を格納(次年度)
        /// </summary>
        private Dictionary<int, CellPoint> _dictionaryPoint = new();

        /// <summary>
        /// SetCodeが対応するRowの増し分
        /// </summary>
        private Dictionary<int, int> _dictionaryGroup = new() { {1310602, 0},
                                                                {1310603, 1},
                                                                {1310604, 3},
                                                                {1310608, 5}};
        /// <summary>
        /// 月と_dictionaryPointの紐づけ
        /// </summary>
        private Dictionary<int, int> _dictionaryMonth = new() { {4, 0},
                                                                {5, 1},
                                                                {6, 2},
                                                                {7, 3},
                                                                {8, 4},
                                                                {9, 5},
                                                                {10, 6},
                                                                {11, 7},
                                                                {12, 8},
                                                                {1, 9},
                                                                {2, 10},
                                                                {3, 11} };

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="connectionVo"></param>
        public CollectionStaffsTaitou(ConnectionVo connectionVo, Screen screen) {
            /*
             * Dao
             */
            _collectionTaitouDao = new(connectionVo);
            /*
             * Dictionaryを作成
             * セルの左上の座標を指定
             */
            _dictionaryPoint.Add(0, new CellPoint(2, 1));
            _dictionaryPoint.Add(1, new CellPoint(11, 1));
            _dictionaryPoint.Add(2, new CellPoint(20, 1));
            _dictionaryPoint.Add(3, new CellPoint(29, 1));
            _dictionaryPoint.Add(4, new CellPoint(38, 1));
            _dictionaryPoint.Add(5, new CellPoint(47, 1));
            _dictionaryPoint.Add(6, new CellPoint(2, 11));
            _dictionaryPoint.Add(7, new CellPoint(11, 11));
            _dictionaryPoint.Add(8, new CellPoint(20, 11));
            _dictionaryPoint.Add(9, new CellPoint(29, 11));
            _dictionaryPoint.Add(10, new CellPoint(38, 11));
            _dictionaryPoint.Add(11, new CellPoint(47, 11));
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

            /*
             * Eventを登録する
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;

            this.InitializeSheetView(SheetViewList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExUpdate_Click(object sender, EventArgs e) {
            // SheetViewを初期化
            InitializeSheetView(SheetViewList);

            DateTime selectDateTime = new((int)NumericUpDownExYear.Value, 4, 1);
            /*
             * 対象配車先の割当て(2025年度)
             * 1310602　１組
             * 1310603　２組
             * 1310604　４組
             * 1310605　臨時
             */
            Dictionary<int, int> _dictionaryTargetSetCode = new() { { 0, 1310602 }, { 1, 1310603 }, { 2, 1310604 }, { 3, 1310608 } };
            /*
             * １年分のデータ処理
             */
            for (int i = 0; i < 12; i++) {
                /*
                 * １か月分のデータ処理
                 */
                for (int count = 0; count < 4; count++) {
                    PutSheetViewList(selectDateTime.Year, selectDateTime.Month, _dictionaryTargetSetCode[count]);
                }
                selectDateTime = selectDateTime.AddMonths(1);
            }
        }

        /// <summary>
        /// PutSheetViewList
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="setCode"></param>
        private void PutSheetViewList(int year, int month, int setCode) {
            /*
             * 稼働日数を取得
             */
            int operationDays = _collectionTaitouDao.GetCollectionOperationDays(year, month, setCode);
            this.SheetViewList.Cells[_dictionaryPoint[_dictionaryMonth[month]].Row + _dictionaryGroup[setCode], _dictionaryPoint[_dictionaryMonth[month]].Column].Value = operationDays;
            /*
             * 曜日別人工数(３人目)を取得
             */
            List<CollectionStaffsTaitouVo> listCollectionStaffsTaitouVo = _collectionTaitouDao.GetCollectionStaffs(year, month, setCode);
            foreach (CollectionStaffsTaitouVo collectionStaffsTaitouVo in listCollectionStaffsTaitouVo) {
                int oldNumber = (int)this.SheetViewList.Cells[_dictionaryPoint[_dictionaryMonth[month]].Row + _dictionaryGroup[setCode], _dictionaryPoint[_dictionaryMonth[month]].Column + collectionStaffsTaitouVo.OperationWeekDay].Value;
                this.SheetViewList.Cells[_dictionaryPoint[_dictionaryMonth[month]].Row + _dictionaryGroup[setCode], _dictionaryPoint[_dictionaryMonth[month]].Column + collectionStaffsTaitouVo.OperationWeekDay].Value = oldNumber + 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemPrintA4":
                    this.SpreadList.PrintSheet(this.SheetViewList);
                    break;
                case "ToolStripMenuItemExit":
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// InitializeSheetView
        /// </summary>
        private void InitializeSheetView(SheetView sheetView) {
            // Tabを非表示
            SpreadList.TabStripPolicy = TabStripPolicy.Never;
            // １２ケ月分繰り返す
            for (int monthCount = 0; monthCount < 12; monthCount++) {
                // ひと月中の行数分繰り返す
                for (int rowNumber = 0; rowNumber < 6; rowNumber++) {
                    sheetView.Cells[_dictionaryPoint[monthCount].Row + rowNumber, _dictionaryPoint[monthCount].Column].Value = 0;       // 配車日数
                    sheetView.Cells[_dictionaryPoint[monthCount].Row + rowNumber, _dictionaryPoint[monthCount].Column + 2].Value = 0;   // 月曜日
                    sheetView.Cells[_dictionaryPoint[monthCount].Row + rowNumber, _dictionaryPoint[monthCount].Column + 3].Value = 0;   // 火曜日
                    sheetView.Cells[_dictionaryPoint[monthCount].Row + rowNumber, _dictionaryPoint[monthCount].Column + 4].Value = 0;   // 水曜日
                    sheetView.Cells[_dictionaryPoint[monthCount].Row + rowNumber, _dictionaryPoint[monthCount].Column + 5].Value = 0;   // 木曜日
                    sheetView.Cells[_dictionaryPoint[monthCount].Row + rowNumber, _dictionaryPoint[monthCount].Column + 6].Value = 0;   // 金曜日
                    sheetView.Cells[_dictionaryPoint[monthCount].Row + rowNumber, _dictionaryPoint[monthCount].Column + 7].Value = 0;   // 土曜日
                }
            }
        }

        /// <summary>
        /// Dictionaryで使用する値を作成
        /// </summary>
        private class CellPoint {
            private int _row;
            private int _column;
            public CellPoint(int row, int column) {
                _row = row;
                _column = column;
            }
            public int Row {
                get => _row;
                set => _row = value;
            }
            public int Column {
                get => _column;
                set => _column = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CollectionStaffsTaitou_FormClosing(object sender, FormClosingEventArgs e) {
        }
    }
}
