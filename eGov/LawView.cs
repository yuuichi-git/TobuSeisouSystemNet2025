/*
 * 2026-03-05
 */
namespace EGov {
    public partial class LawView : Form {
        /*
         * インスタンス
         */
        private EGovApi eGobApi = new();

        public LawView(string lawTitle = null, string article = null, string paragraph = null) {
            /*
             * Initialize
             */
            InitializeComponent();

            this.CcTextBoxDetail.SetEmpty();

            this.PutLaw(lawTitle, article, paragraph);
        }

        private async void PutLaw(string lawTitle = null, string article = null, string paragraph = null) {
            var result = await eGobApi.FindBestMatchAsync("消防法");


            this.CcTextBoxDetail.Text = result?.RevisionInfo?.LawTitle;
        }


    }
}
