using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public interface IResponseMiddleware
    {
        bool Exected(ResponseApi response);
    }
    public class DefaultResponseMiddleware : IResponseMiddleware
    {
        bool IResponseMiddleware.Exected(ResponseApi response)
        {
            if (response.Code.ToString().StartsWith("2"))
                response.Success = true;
            else
            {
                response.Success = false;
            }
            return false;
        }
    }
#if !(NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
    public class ResponseApiUtils
    {
        public static List<IResponseMiddleware> Middlewares = new List<IResponseMiddleware>() {
            new DefaultResponseMiddleware()
        };
        public static ResponseApi GetResponse(Language language=Language.Chinese,Code code=Code.Success,bool success=true,int statusCode=0)
        {
            DescAttribute desc = DescUtils.DescAttributes.ContainsKey(code.ToString()) ? DescUtils.DescAttributes[code.ToString()] : DescUtils.GetDescAttribute(code);
            ResponseApi response = new ResponseApi() { Code = statusCode == 0 ? (int)code : statusCode, Success = success };
            if (desc == null)
            {
                response.Message = code.ToString();
            }
            else
            {
                switch (language)
                {
                    case Language.Chinese: response.Message = desc.ChineseDesc; break;
                    case Language.English: response.Message = desc.EnglishDesc; break;
                    default: response.Message = code.ToString(); break;
                }
            }
            foreach (var item in Middlewares)
            {
                if (item.Exected(response)) break;
            }
            return response;
        }
        public static ResponseApi<T> GetResponse<T>(Language language = Language.Chinese, Code code = Code.Success, bool success = true, int statusCode = 0)
        {
            DescAttribute desc = DescUtils.DescAttributes.ContainsKey(code.ToString()) ? DescUtils.DescAttributes[code.ToString()] : DescUtils.GetDescAttribute(code);
            ResponseApi<T> response = new ResponseApi<T>() { Code = statusCode == 0 ? (int)code : statusCode, Success = success };
            if (desc == null)
            {
                response.Message = code.ToString();
            }
            else
            {
                switch (language)
                {
                    case Language.Chinese: response.Message = desc.ChineseDesc; break;
                    case Language.English: response.Message = desc.EnglishDesc; break;
                    default: response.Message = code.ToString(); break;
                }
            }
            foreach (var item in Middlewares)
            {
                if (item.Exected(response)) break;
            }
            return response;
        }
        public static ResponseApi Success(Language language = Language.Chinese)
        {
            return GetResponse(language, Code.Success);
        }
        public static ResponseApi Fail(Language language = Language.Chinese)
        {
            return GetResponse(language, Code.Fail, false);
        }
        public static ResponseApi Error(Language language = Language.Chinese)
        {
            return GetResponse(language, Code.Error,false);
        }
        public static ResponseApi<T> Success<T>(Language language = Language.Chinese)
        {
            return GetResponse<T>(language, Code.Success);
        }
        public static ResponseApi<T> Fail<T>(Language language = Language.Chinese)
        {
            return GetResponse<T>(language, Code.Fail, false);
        }
        public static ResponseApi Error<T>(Language language = Language.Chinese)
        {
            return GetResponse<T>(language, Code.Error, false);
        }
    }
#endif
}
