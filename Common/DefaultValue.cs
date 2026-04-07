/*
 *2023-11-08
 */
namespace Common {
    public class DefaultValue {
        /// <summary>
        /// DBからのobjectを変換
        /// </summary>
        public T GetDefaultValue<T>(object obj) {

            // Null or DBNull → 特殊デフォルト or default(T)
            if (obj == null || obj == DBNull.Value) {
                if (_specialDefaults.TryGetValue(typeof(T), out var special))
                    return (T)special;

                return default!;
            }

            // そのままキャストできる場合
            if (obj is T value)
                return value;

            // ChangeType で変換
            try {
                return (T)Convert.ChangeType(obj, typeof(T));
            } catch {
                return default!;
            }
        }

        /// <summary>
        /// 型ごとの特殊デフォルト値を管理する辞書
        /// </summary>
        private static readonly Dictionary<Type, object> _specialDefaults = new()
        {
            { typeof(DateTime), new DateTime(1900, 1, 1) },
            { typeof(byte[]), Array.Empty<byte>() },

            // --- ここに好きなだけ追加できる ---
            // { typeof(Guid), Guid.Empty },
            // { typeof(decimal), 0m },
            // { typeof(bool), false },
            // { typeof(string), string.Empty }, // string を default("") にしたい場合
        };
    }
}
