using Common;

using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;

using Vo;

namespace EGov {
    public partial class LawList : Form {
        private const int _colNo = 0;                           // No
        private const int _colLawTitle = 1;                     // 法令名
        private const int _colLawSubTitle = 2;                  // 項目内容
        private const int _colLawNum = 3;                       // 法令番号
        private const int _colLawArticle = 4;                   // 条
        private const int _colLawParagraph = 5;                 // 項
        private const int _colLawItem = 6;                      // 号
        /*
         * Screen
         */
        private readonly ScreenForm _screenForm = new();
        private Screen _screen;
        /*
         * コンストラクタ
         */
        public LawList(ConnectionVo connectionVo, Screen screen) {
            /*
             * Screen
             */
            _screen = screen;
            /*
             * Initialize
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

            InitializeSheetView(this.SheetViewList);

            /*
             * StatusStrip
             */
            this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = string.Empty;
            /*
             * Event
             */
            this.CcMenuStrip1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        private async void SheetViewList_CellDoubleClick(object sender, CellClickEventArgs e) {
            ICellType cellType = this.SheetViewList.Cells[e.Row, _colLawTitle].CellType;
            // ハイパーリンクセルの場合は処理を中断
            if(cellType is HyperLinkCellType)
                return;

            // ① UI から法令名・条・項を取得
            string lawTitle = this.SheetViewList.Cells[e.Row, _colLawTitle].Text;
            string lawNum = this.SheetViewList.Cells[e.Row, _colLawNum].Text;
            string lawArticle = this.SheetViewList.Cells[e.Row, _colLawArticle].Text;
            string lawParagraph = this.SheetViewList.Cells[e.Row, _colLawParagraph].Text;
            string lawItem = this.SheetViewList.Cells[e.Row, _colLawItem].Text;

            if(string.IsNullOrWhiteSpace(lawTitle)) {
                MessageBox.Show("法令名が空です。");
                return;
            }

            // ④ LawView を開く（あなたの既存コードに合わせる）
            LawView lawView = new(lawTitle, lawNum, lawArticle, lawParagraph, lawItem);
            await lawView.InitializeAsync();
            _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), lawView);
            lawView.Show(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns></returns>
        private SheetView InitializeSheetView(SheetView sheetView) {
            SpreadList.AllowDragDrop = false; // DrugDropを禁止する
            SpreadList.PaintSelectionHeader = false; // ヘッダの選択状態をしない
            sheetView.ColumnHeader.Rows[0].Height = 26; // Columnヘッダの高さ
            sheetView.GrayAreaBackColor = Color.White;
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9); // 行ヘッダのFont
            sheetView.RowHeader.Columns[0].Width = 48; // 行ヘッダの幅を変更します
            sheetView.VerticalGridLine = new GridLine(GridLineType.Flat, Color.LightGray);
            return sheetView;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch(((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemExit":
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LawList_FormClosing(object sender, FormClosingEventArgs e) {
            DialogResult dialogResult = MessageBox.Show("アプリケーションを終了します。よろしいですか？", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
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
