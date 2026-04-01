using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace EGov {

    // ---------------------------------------------------------
    // Normalizer の基本インターフェース
    // 入力データを「揺れのない扱いやすい形式」に整える責務を持つ
    // ---------------------------------------------------------
    public interface INormalizer<TIn, TOut> {
        TOut Normalize(TIn input);
    }

    // ---------------------------------------------------------
    // Normalizer を順番に適用するパイプライン
    // UI → Parser に渡す前に、すべての揺れを吸収する
    // ---------------------------------------------------------
    public sealed class NormalizationPipeline {
        private readonly List<INormalizer<string, string>> _normalizers = new();

        /// <summary>
        /// Normalizer を追加する（チェーン可能）
        /// </summary>
        public NormalizationPipeline Add(INormalizer<string, string> normalizer) {
            _normalizers.Add(normalizer);
            return this;
        }

        /// <summary>
        /// 追加された Normalizer を順番に適用して正規化を完了させる
        /// </summary>
        public string Execute(string input) {
            string result = input ?? string.Empty;
            foreach (var n in _normalizers)
                result = n.Normalize(result);
            return result;
        }
    }

    // ---------------------------------------------------------
    // 文字列の基本正規化
    // ・改行コード統一
    // ・前後の空白除去
    // ---------------------------------------------------------
    public sealed class StringNormalizer : INormalizer<string, string> {
        public string Normalize(string input) {
            if (input == null)
                return string.Empty;

            return input
                .Replace("\r\n", "\n")
                .Replace("\r", "\n")
                .Trim();
        }
    }

    // ---------------------------------------------------------
    // XML の基本正規化
    // ・BOM 除去
    // ・空文字の場合は最低限の XML を返す
    // ---------------------------------------------------------
    public sealed class XmlNormalizer : INormalizer<string, string> {
        public string Normalize(string input) {
            if (string.IsNullOrWhiteSpace(input))
                return "<root></root>";

            // BOM除去
            input = input.Trim('\uFEFF');

            // 必要に応じて namespace 削除などもここに追加可能
            return input;
        }
    }

    // ---------------------------------------------------------
    // XML 属性の null → "" を吸収するユーティリティ
    // Parser から呼ばれる
    // ---------------------------------------------------------
    public static class AttributeNormalizer {
        /// <summary>
        /// XElement.Attribute(name) の null を "" に統一する
        /// </summary>
        public static string Get(XElement parent, string name)
            => (string?)parent.Attribute(name) ?? string.Empty;
    }

    // ---------------------------------------------------------
    // XML 要素の null → "" を吸収するユーティリティ
    // Parser から呼ばれる
    // ---------------------------------------------------------
    public static class ElementNormalizer {
        /// <summary>
        /// XElement.Element(name) の null を "" に統一する
        /// </summary>
        public static string Get(XElement parent, string name)
            => (string?)parent.Element(name) ?? string.Empty;
    }

    // ---------------------------------------------------------
    // Sentence 内の混在コンテンツ（Ruby など）をテキスト化する Normalizer
    // ・Ruby の Rt を括弧付きで付与
    // ・その他のタグは Value をそのまま連結
    // ---------------------------------------------------------
    public static class RubyTextNormalizer {
        /// <summary>
        /// Sentence ノード内の混在コンテンツを「表示用テキスト」に変換する
        /// </summary>
        public static string Extract(XElement sentenceElement) {
            if (sentenceElement == null)
                return string.Empty;

            StringBuilder stringBuilder = new();

            foreach (XNode xNode in sentenceElement.Nodes()) {

                // 素のテキスト
                if (xNode is XText txt) {
                    stringBuilder.Append(txt.Value);
                }

                // 要素（Ruby など）
                else if (xNode is XElement el) {

                    // Ruby タグの処理
                    if (string.Equals(el.Name.LocalName, "Ruby", StringComparison.OrdinalIgnoreCase)) {

                        // Rt（ルビ）取得
                        var rt = (string?)el.Element("Rt") ?? string.Empty;

                        // Ruby の直下のテキスト（ベース文字）
                        var baseText = string.Concat(
                            el.Nodes()
                              .OfType<XText>()
                              .Select(t => t.Value)
                        ).Trim();

                        if (!string.IsNullOrEmpty(baseText)) {
                            stringBuilder.Append(baseText);
                            if (!string.IsNullOrEmpty(rt))
                                stringBuilder.Append($"({rt})");
                        } else {
                            // Ruby 内にテキストがない場合は Value をそのまま
                            stringBuilder.Append(el.Value);
                        }
                    }

                    // その他のタグは Value をそのまま
                    else {
                        stringBuilder.Append(el.Value);
                    }
                }
            }

            return stringBuilder.ToString().Trim();
        }
    }

    /// <summary>
    /// ParagraphNum を正規化する Normalizer
    /// ・空 → Num + "項"
    /// ・数字だけ → 数字 + "項"
    /// ・全角数字 → 半角に変換して "項"
    /// ・漢数字だけ → 漢数字 + "項"
    /// ・完全形（例：第二項） → そのまま
    /// </summary>
    public sealed class ParagraphNumNormalizer : INormalizer<XElement, XElement> {
        public XElement Normalize(XElement paragraph) {
            if (paragraph == null)
                return new XElement("Paragraph");

            string raw = (string?)paragraph.Element("ParagraphNum") ?? "";
            string numAttr = (string?)paragraph.Attribute("Num") ?? "0";

            int num = int.TryParse(numAttr, out var n) ? n : 0;

            string normalized = NormalizeParagraphNum(raw, num);

            // ParagraphNum を上書き
            paragraph.SetElementValue("ParagraphNum", normalized);

            return paragraph;
        }

        private string NormalizeParagraphNum(string raw, int num) {
            // 空 → 数字項
            if (string.IsNullOrWhiteSpace(raw))
                return $"{num}項";

            // 全角数字 → 半角数字
            string half = Microsoft.VisualBasic.Strings.StrConv(raw, Microsoft.VisualBasic.VbStrConv.Narrow);

            // 半角数字だけ
            if (Regex.IsMatch(half, @"^[0-9]+$"))
                return $"{half}項";

            // 漢数字だけ
            if (Regex.IsMatch(raw, @"^[一二三四五六七八九十百千]+$"))
                return $"{raw}項";

            // 完全形（例：第二項）→ そのまま
            return raw;
        }
    }

    public sealed class ItemNumNormalizer : INormalizer<XElement, XElement> {
        public XElement Normalize(XElement item) {
            if (item == null)
                return new XElement("Item");

            string raw = (string?)item.Element("ItemTitle") ?? "";
            string numAttr = (string?)item.Attribute("Num") ?? "0";

            int num = int.TryParse(numAttr, out var n) ? n : 0;

            string normalized = NormalizeItemNum(raw, num);

            item.SetElementValue("ItemTitle", normalized);

            return item;
        }

        private string NormalizeItemNum(string raw, int num) {
            // 空 → 数字号
            if (string.IsNullOrWhiteSpace(raw))
                return $"{num}号";

            // 全角数字 → 半角数字
            string half = Microsoft.VisualBasic.Strings.StrConv(raw, Microsoft.VisualBasic.VbStrConv.Narrow);

            // 半角数字だけ
            if (Regex.IsMatch(half, @"^[0-9]+$"))
                return $"{half}号";

            // 漢数字だけ
            if (Regex.IsMatch(raw, @"^[一二三四五六七八九十百千]+$"))
                return $"{raw}号";

            // 完全形（例：第一号）→ そのまま
            return raw;
        }
    }

}