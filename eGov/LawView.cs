/*
 * 2026-03-05
 */

using Vo;

namespace EGov {
    public partial class LawView : Form {
        private EGobApi _egobApi = new();

        private readonly string _lawName;
        private readonly string _lawArticle;
        private readonly string _lawParagraph;

        private readonly Dictionary<string, string> _dictionaryLawTypeMap = new(){
            { "Constitution",        "憲法" },
            { "Act",                 "法律" },
            { "CabinetOrder",        "政令" },
            { "ImperialOrder",       "勅令" },
            { "MinisterialOrdinance","府省令" },
            { "Rule",                "規則" },
            { "Misc",                "その他" }};

        public LawView(string lawName, string lawArticle, string lawParagraph) {
            _lawName = lawName;
            _lawArticle = lawArticle;
            _lawParagraph = lawParagraph;

            InitializeComponent();

            // フォームロード後に API を叩く
            this.Load += async (_, __) => await LoadLawAsync();
        }

        private async Task LoadLawAsync() {
            // ① KeyWord検索 API で KeywordItem を取得
            KeywordResponse keywordResponse = await _egobApi.GetLawInfoAsync(_lawName);

            if (keywordResponse == null) {
                MessageBox.Show("法令が見つかりませんでした。");
                return;
            }

            // ② law_id を取得
            string? lawId = keywordResponse.Items.FirstOrDefault()?.LawInfo.LawId;

            if (string.IsNullOrWhiteSpace(lawId)) {
                MessageBox.Show("法令IDが取得できませんでした。");
                return;
            }

            // ③ Controlに出力
            this.CcLabelLawId.Text = string.Concat("LawId : ", keywordResponse.Items.FirstOrDefault()?.LawInfo.LawId);
            this.CcLabelLawNum.Text = string.Concat("LawNum : ", keywordResponse.Items.FirstOrDefault()?.LawInfo.LawNum);
            this.CcLabelLawType.Text = string.Concat("LawType : ", _dictionaryLawTypeMap[keywordResponse.Items.FirstOrDefault()?.RevisionInfo.LawType]);
            this.CcLabelLawTitle.Text = string.Concat("LawTitle : ", keywordResponse.Items.FirstOrDefault()?.RevisionInfo.LawTitle);
            this.CcLabelLawArticle.Text = string.Concat("Article : ", _lawArticle);

            // ④ 法令APIで法令の内容を取得
            LawDataResponse lawDataResponse = await _egobApi.GetLawDataAsync(lawId);



        }



    }
}