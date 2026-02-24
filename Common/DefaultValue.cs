/*
 *2023-11-08
 */
namespace Common {
    public class DefaultValue {
        /// <summary>
        /// DBからのobjectを変換
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public T GetDefaultValue<T>(object obj) {
            /*
             * 旧型
             */
            //object objectValue = new();
            //if (obj != DBNull.Value && obj != null) {
            //    return (T)obj;
            //} else { // objがNullだった場合
            //    switch (typeof(T).Name) {
            //        case "Boolean":
            //            objectValue = false;
            //            break;
            //        case "DateTime":
            //            objectValue = new DateTime(1900, 01, 01);
            //            break;
            //        case "Int32":
            //            objectValue = 0;
            //            break;
            //        case "String":
            //            objectValue = string.Empty;
            //            break;
            //        case "Decimal":
            //            objectValue = 0.0;
            //            break;
            //        case "Byte[]":
            //            objectValue = Array.Empty<byte>();
            //            break;
            //    }
            //}
            //return (T)objectValue;

            if (obj == null || obj == DBNull.Value) {                                           // objがNullだった場合
                if (typeof(T) == typeof(DateTime))                                              // 特殊デフォルト値
                    return (T)(object)new DateTime(1900, 1, 1);

                if (typeof(T) == typeof(byte[]))                                                // 特殊デフォルト値
                    return (T)(object)Array.Empty<byte>();

                return default!;                                                                // 通常の default(T)
            }

            if (obj is T value)                                                                 // そのままキャストできる場合
                return value;

            try {                                                                               // 変換できる場合のみ ChangeType を使う
                return (T)Convert.ChangeType(obj, typeof(T));
            } catch {
                return default!;                                                                // 変換できない場合は default(T)
            }
        }
    }
}
