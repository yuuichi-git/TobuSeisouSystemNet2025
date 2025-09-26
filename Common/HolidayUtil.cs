/*
 * 2024-03-20
 * 内閣府のデータから祝日一覧を取得する
 * https://www8.cao.go.jp/chosei/shukujitsu/syukujitsu.csv
 */
using System.Net;
using System.Text;

namespace Common {
    public class HolidayUtility {
        private Dictionary<DateTime, string> _dictionary = new Dictionary<DateTime, string>();

        public Dictionary<DateTime, string> GetHoliday() {
            // .Net5でSJISを使う場合に必要
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            // 祝日ファイルを取得
            string _path = @"https://www8.cao.go.jp/chosei/shukujitsu/syukujitsu.csv";
            byte[] buffer;
            using (HttpClient httpClient = new()) {
                buffer = httpClient.GetByteArrayAsync(_path).GetAwaiter().GetResult();
            }
            string str = Encoding.GetEncoding("shift_jis").GetString(buffer);
            // 行毎に配列に分割
            string[] rows = str.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            /*
             * CSVの１行目はヘッダなのでSkipする
             */
            foreach (var data in rows.Skip(1)) {
                string[] cols = data.Split(',');
                _dictionary.Add(DateTime.Parse(cols[0]), cols[1]);
            }
            return _dictionary;
        }
    }
}
