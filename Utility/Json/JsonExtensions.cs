using Newtonsoft.Json;

namespace Utility
{
#if !NET20 && !NET30 
    /// <summary>json扩展类 </summary>
    public static class JsonExtensions
    {

        /// <summary>
        /// json 字符串 转对象
        /// </summary>
        /// <param name="json">json 字符串</param>
        /// <returns></returns>
        public static T ToObject<T>(this string json) => JsonUtils.Instance.ToObject<T>(json);

        /// <summary>
        ///  对象  转  json 字符串
        /// </summary>
        /// <param name="json">对象</param>
        public static object ToObject(this string json) => JsonUtils.Instance.ToObject(json);

        /// <summary>
        ///  对象  转  json 字符串
        /// </summary>
        /// <param name="obj">对象</param>
        public static string ToJson(this object obj) => JsonUtils.Instance.ToJson(obj);
        /// <summary>
        /// json 字符串 转对象
        /// </summary>
        /// <param name="json">json 字符串</param>
        /// <param name="jsonSerializer"></param>
        /// <returns></returns>
        public static object ToObject(this string json, JsonSerializerSettings jsonSerializer) => JsonUtils.Instance.ToObject(json,jsonSerializer);

        /// <summary>
        ///  对象  转  json 字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="jsonSerializer"></param>
        /// <returns></returns>
        public static string ToJson(this object obj, JsonSerializerSettings jsonSerializer) => JsonUtils.Instance.ToJson(obj, jsonSerializer);

        /// <summary>
        ///  对象  转  json 字符串
        /// </summary>
        /// <param name="json">字符串</param>
        /// <param name="jsonSerializer"></param>
        /// <returns></returns>
        public static T ToObject<T>(this string json, JsonSerializerSettings jsonSerializer) => JsonUtils.Instance.ToObject<T>(json, jsonSerializer);
    }
#endif
}
