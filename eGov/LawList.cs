using Common;

using FarPoint.Win.Spread;

using Vo;

namespace EGov {
    public partial class LawList : Form {
        private const int _colNo = 0;
        private const int _colLawTitle = 1;
        private const int _colLawSubTitle = 2;
        private const int _colLawNum = 3;
        private const int _colLawArticle = 4;
        private const int _colLawParagraph = 5;
        private const int _colLawItem = 6;
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
            /*
             * StatusStrip
             */
            this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = string.Empty;
        }

        private async void CcButtonUpdate_Click(object sender, EventArgs e) {

        }


        private async void SheetViewList_CellDoubleClick(object sender, CellClickEventArgs e) {
            // ① UI から法令名・条・項を取得
            string lawTitle = this.SheetViewList.Cells[e.Row, _colLawTitle].Text;
            string lawNum = this.SheetViewList.Cells[e.Row, _colLawNum].Text;
            string lawArticle = this.SheetViewList.Cells[e.Row, _colLawArticle].Text;
            string lawParagraph = this.SheetViewList.Cells[e.Row, _colLawParagraph].Text;
            string lawItem = this.SheetViewList.Cells[e.Row, _colLawItem].Text;

            if (string.IsNullOrWhiteSpace(lawTitle)) {
                MessageBox.Show("法令名が空です。");
                return;
            }

            // ④ LawView を開く（あなたの既存コードに合わせる）
            LawView lawView = new(lawTitle, lawNum, lawArticle, lawParagraph, lawItem);
            await lawView.InitializeAsync();

            _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), lawView);
            lawView.Show(this);
        }

    }
}
