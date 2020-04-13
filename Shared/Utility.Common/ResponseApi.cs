using Utility;

namespace Utility
{
    /// <summary>
    /// 通用返回结果
    /// </summary>
    public class ResponseApi
    {
        /// <summary>
        /// 是否操作成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 操作信息
        /// </summary>
        public string Message { get; set; } 
        public int Code { get; set; }
        public object Data { get; set; }
        /// <summary>
        /// 返回json
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonUtils.Instance.ToJson(this);
        }
        public virtual ResponseApi SetData(object data)
        {
            this.Data = data;
            return this;
        }

    }
    /// <summary>
    /// 通用返回结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseApi<T> : ResponseApi
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public new  T Data { get; set; } = default(T);
        /// <summary>
        /// 返回json
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonUtils.Instance.ToJson(this);
        }
        public ResponseApi Response()
        {
            return new ResponseApi() { Success = Success, Message = Message, Code = Code, Data = Data };
        }
        public override ResponseApi SetData(object data)
        {
            this.Data = (T)data;
            return this;
        }
        public  ResponseApi<T> SetData(T data)
        {
            this.Data = data;
            return this;
        }
    }
}
