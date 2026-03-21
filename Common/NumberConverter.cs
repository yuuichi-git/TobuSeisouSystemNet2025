/*
 * 2026-03-22
 */
using System.Text.RegularExpressions;

namespace Common {
    public static class LawNumberConverter {
        public static string ConvertLawNotation(string input) {
            // 例: 昭和23年法律第186号
            var match = Regex.Match(input, @"^(昭和)(\d+)年法律第(\d+)号$");
            if (!match.Success)
                return input;

            string era = match.Groups[1].Value;
            int year = int.Parse(match.Groups[2].Value);
            int number = int.Parse(match.Groups[3].Value);

            string yearKanji = ToKanjiNumber(year);
            string numberKanji = ToKanjiNumber(number);

            return $"{era}{yearKanji}年法律第{numberKanji}号";
        }

        // --- 百の位までの漢数字変換 ---
        public static string ToKanjiNumber(int n) {
            if (n == 0)
                return "零";

            string[] kan = { "", "一", "二", "三", "四", "五", "六", "七", "八", "九" };

            int hundreds = n / 100;
            int tens = (n / 10) % 10;
            int ones = n % 10;

            string result = "";

            if (hundreds > 0)
                result += (hundreds == 1 ? "百" : kan[hundreds] + "百");

            if (tens > 0)
                result += (tens == 1 ? "十" : kan[tens] + "十");

            if (ones > 0)
                result += kan[ones];

            return result;
        }
    }
}
