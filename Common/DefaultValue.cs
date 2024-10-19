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
            object objectValue = new();
            if (obj != DBNull.Value && obj != null) {
                return (T)obj;
            } else { // objがNullだった場合
                switch (typeof(T).Name) {
                    case "Boolean":
                        objectValue = false;
                        break;
                    case "DateTime":
                        objectValue = new DateTime(1900, 01, 01);
                        break;
                    case "Int32":
                        objectValue = 0;
                        break;
                    case "String":
                        objectValue = "";
                        break;
                    case "Decimal":
                        objectValue = 0.0;
                        break;
                    case "Byte[]":
                        objectValue = Array.Empty<byte>();
                        break;
                }
            }
            return (T)objectValue;
        }
    }
}
