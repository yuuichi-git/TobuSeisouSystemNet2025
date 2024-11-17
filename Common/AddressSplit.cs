/*
 * 2024-06-19
 */
using System.Text.RegularExpressions;

namespace Common {
    public class AddressSplit {
        private Match _prefecturesAddress;//都道府県    
        private Match _cityAddress;//市町村       
        private Match _otherAddress;//他住所

        public AddressSplit(string address) {
            Regex stringSeikiWord;//正規表現を格納する変数
            var targetString = address;//正規表現によって切り出された文字を格納する変数
            if (targetString.IndexOf("／") != -1)
                targetString = targetString.Substring(0, targetString.IndexOf("／"));
            if (targetString.IndexOf("＊") != -1)
                targetString = targetString.Substring(0, targetString.IndexOf("＊"));
            if (targetString.IndexOf("　") != -1)
                targetString = targetString.Substring(0, targetString.IndexOf("　"));
            if (targetString.IndexOf("（") != -1)
                targetString = targetString.Substring(0, targetString.IndexOf("（"));
            //都道府県を抜く
            stringSeikiWord = new Regex("^([^市区町村]{2}[都道府県]|[^市区町村]{3}県)", RegexOptions.IgnoreCase);
            _prefecturesAddress = stringSeikiWord.Match(targetString);
            if (_prefecturesAddress.Success)
                targetString = targetString.Substring(_prefecturesAddress.Length);
            //市区町村を抜く①(特殊なものを検索)
            stringSeikiWord = new Regex("^(余市郡(仁木町|赤井川村|余市町)|余市町|柴田郡村田町|(武蔵|東)村山市|[東西北]村山郡...?町|田村(市|郡..町)芳賀郡市貝町|(佐波郡)?玉村町|[羽大]村市|(十日|大)町市|(中新川郡)?上市町|(野々|[四廿]日)市市|西八代郡市川三郷町|神崎郡市川町|高市郡(高取町|明日香村)|(吉野郡)?下市町|(杵島郡)?大町町)", RegexOptions.IgnoreCase);
            _cityAddress = stringSeikiWord.Match(targetString);
            if (_cityAddress.Success == false) {
                //市区町村を抜く②(①に掛からなかったら[市区町村]で抜ける
                stringSeikiWord = new Regex("(.*)([市区町村])", RegexOptions.IgnoreCase);
                _cityAddress = stringSeikiWord.Match(targetString);
            }
            targetString = targetString.Substring(_cityAddress.Length);
            //その他の住所を抜く
            //@"[０１２３４５６７８９一二三四五六七八九十]+(－|丁目|番)[０-９]+(－|番|号)[０-９]+"
            //@"^.*[０１２３４５６７８９一二三四五六七八九十]*(－|丁目|番|号)[０１２３４５６７８９]*"
            //
            stringSeikiWord = new Regex(@"^.*", RegexOptions.IgnoreCase);
            _otherAddress = stringSeikiWord.Match(targetString);
        }
        public Match PrefecturesAddress {
            get => _prefecturesAddress;
            set => _prefecturesAddress = value;
        }
        public Match CityAddress {
            get => _cityAddress;
            set => _cityAddress = value;
        }
        public Match OtherAddress {
            get => _otherAddress;
            set => _otherAddress = value;
        }
    }
}
