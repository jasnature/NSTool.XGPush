using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

    ///  author:jasnature from http://www.cnblogs.com/NatureSex/
namespace NSTool.XGPush.Base
{
    /// <summary>
    /// 处理JSON 序列化与反序列化
    /// </summary>
    public sealed class JsonHelper
    {
        private static JsonSerializerSettings jss = new JsonSerializerSettings();


        static JsonHelper()
        {
            jss.NullValueHandling = NullValueHandling.Ignore;
            jss.Converters.Add(new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
        }

        /// <summary>
        /// 转换到JOSN格式， 默认忽略为NULL的字段
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(object obj)
        {
            if (obj == null) return "";
            return JsonConvert.SerializeObject(obj, jss);
        }

        /// <summary>
        /// json反序列化到对象，时间是yyyy-MM-dd HH:mm:ss格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static T FromJson<T>(string jsonStr)
        {
            if (string.IsNullOrWhiteSpace(jsonStr)) return default(T);
            return JsonConvert.DeserializeObject<T>(jsonStr, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
        }
    }
}
