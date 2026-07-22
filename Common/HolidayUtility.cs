/*
 * 2024-03-20
 * 内閣府のデータから祝日一覧を取得する
 * https://www8.cao.go.jp/chosei/shukujitsu/syukujitsu.csv
 */
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

        /// <summary>
        /// 祝祭日と会社指定休日の日数合計を取得する
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="holidaySet"></param>
        /// <returns></returns>
        public int GetWorkingDays(DateTime startDate, DateTime endDate, Dictionary<DateTime, string> holidaySet) {
            int count = 0;

            DateTime date = startDate.Date;

            while(date <= endDate.Date) {
                bool isSunday  = date.DayOfWeek == DayOfWeek.Sunday;
                bool isHoliday = holidaySet.Keys.Contains(date);

                if(isSunday || isHoliday) {
                    count++;
                }

                date = date.AddDays(1);
            }

            return count;
        }
    }
}
