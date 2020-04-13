using Newtonsoft.Json;

namespace Utility
{
    #region json 公共类
    /// <summary>json 公共类 </summary>
    public class JsonUtils
    {

        /// <summary>
        /// 受保护构造函数
        /// </summary>
        protected JsonUtils()
        {

        }
        /// <summary>
        ///  a_b_c ABC
        /// </summary>
       public static  readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings()
        {
            //忽略循环引用
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            //使用 ab_c AbC 
            ContractResolver = JsonContractResolver.ObjectResolverJson,//JsonContractResolver.JsonResolverObject,
                                                                       //设置时间格式
            DateFormatString = "yyyy-MM-dd HH:mm:ss"
        };
        /// <summary>
        /// 内部类
        /// </summary>
        class InnerJson
        {
            /// <summary>
            /// 声明JsonUtils对象 初始化
            /// </summary>
            public readonly static JsonUtils JsonObject=new JsonUtils();
        }
        /// <summary>
        ///  饿汉式 单例模式 
        /// </summary>
        public static JsonUtils Instance
        {
            get
            {
                return InnerJson.JsonObject;
            }
        }
        /// <summary>
        /// json 字符串 转对象
        /// </summary>
        /// <param name="json">json 字符串</param>
        /// <returns></returns>
        public virtual object ToObject(string json) => string.IsNullOrEmpty(json) ? null : JsonConvert.DeserializeObject(json, new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });

        /// <summary>
        ///  对象  转  json 字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public virtual string ToJson(object obj) => obj == null ? "{}" : JsonConvert.SerializeObject(obj,
            Formatting.None,
           new JsonSerializerSettings()
           {
               ReferenceLoopHandling = ReferenceLoopHandling.Ignore
           });

        /// <summary>
        ///  对象  转  json 字符串
        /// </summary>
        /// <param name="json">字符串</param>
        public virtual T ToObject<T>(string json) => string.IsNullOrEmpty(json) ? default(T) : JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });
        /// <summary>
        /// json 字符串 转对象
        /// </summary>
        /// <param name="json">json 字符串</param>
        /// <param name="jsonSerializer"></param>
        /// <returns></returns>
        public virtual object ToObject(string json,JsonSerializerSettings jsonSerializer) => string.IsNullOrEmpty(json) ? null : JsonConvert.DeserializeObject(json,jsonSerializer);

        /// <summary>
        ///  对象  转  json 字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="jsonSerializer"></param>
        /// <returns></returns>
        public virtual string ToJson(object obj, JsonSerializerSettings jsonSerializer) => obj == null ? "{}" : JsonConvert.SerializeObject(obj, jsonSerializer);

        /// <summary>
        ///  对象  转  json 字符串
        /// </summary>
        /// <param name="json">字符串</param>
        /// <param name="jsonSerializer"></param>
        /// <returns></returns>
        public virtual T ToObject<T>(string json, JsonSerializerSettings jsonSerializer) => string.IsNullOrEmpty(json) ? default(T) : JsonConvert.DeserializeObject<T>(json,jsonSerializer);
    }
    #endregion json 公共类
}
