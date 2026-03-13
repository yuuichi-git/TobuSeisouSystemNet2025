using Common;

using FarPoint.Win.Spread;

using Vo;

namespace EGov {
    public partial class LawList : Form {
        private const int _colNo = 0;
        private const int _colLawTitle = 1;
        private const int _colLawNum = 2;
        private const int _colLawArticle = 3;
        private const int _colLawlawId = 4;
        private const int _colJyou = 5;
        private const int _colKou = 6;
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
            string lawName = this.SheetViewList.Cells[e.Row, _colLawTitle].Text;
            string lawArticle = this.SheetViewList.Cells[e.Row, _colJyou].Text;
            string lawParagraph = this.SheetViewList.Cells[e.Row, _colKou].Text;

            if (string.IsNullOrWhiteSpace(lawName)) {
                MessageBox.Show("法令名が空です。");
                return;
            }

            // ④ LawView を開く（あなたの既存コードに合わせる）
            LawView lawView = new(lawName, lawArticle, lawParagraph);
            _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), lawView);
            lawView.Show(this);
        }

    }
}
