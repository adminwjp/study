using System;
using System.IO;
#if !NET20 && !NET30 && !NET35  && !NETSTANDARD1_0 
using System.Net;
using System.Text;
using System.Threading.Tasks;
#endif
#if !(NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET472 || NET48 || NET481 || NET482 || NETSTANDARD1_0)
using System.Net.Http;
#endif 

namespace Utility
{
    /// <summary>
    /// http 公共类
    /// </summary>
    public static class HttpUtils
    {

#if !(NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET472 || NET48 || NET481 || NET482 || NETSTANDARD1_0)
        /// <summary>
        /// http get 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public static Task<string> GetAsync(string url)
        {
            ArgumentsUtils.CheckArgumentNull("url", url);
            using (HttpClient httpClient = new HttpClient())
            {
                return httpClient.GetStringAsync(url);
            }
        }
        /// <summary>
        /// 对象HttpClient
        /// </summary>
        public static readonly HttpClient HttpClient = new HttpClient();
#endif
        /// <summary>
        /// http get 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public static string Get(string url)
        {
            ArgumentsUtils.CheckArgumentNull("url", url);
#if !(NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET472 || NET48 || NET481 || NET482 || NETSTANDARD1_0)
            using (HttpClient httpClient = new HttpClient())
            {
                return httpClient.GetStringAsync(url).Result;
            }
#else
            return Utility.DefaultHttpCollect.Default.GetString(url);
#endif
        }
        /// <summary>
        /// http get 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public static string GetLocation(string url)
        {
            ArgumentsUtils.CheckArgumentNull("url", url);
#if !(NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET472 || NET48 || NET481 || NET482 || NETSTANDARD1_0)
            using (HttpClient httpClient = new HttpClient())
            {
                return httpClient.GetAsync(url).Result.Headers.Location.OriginalString;
            }
#else
            return Utility.DefaultHttpCollect.Default.Http(new HttpEntity() { Url=url,Method=HttpMethod.GET}).Location;
#endif
        }
#if !(NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET472 || NET48 || NET481 || NET482 || NETSTANDARD1_0)
        /// <summary>
        /// http get 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="param">请求参数</param>
        /// <returns></returns>
        public static Task<string> PostJsonAsync(string url, string param)
        {
            ArgumentsUtils.CheckArgumentNull("url", url);
            param = param ?? string.Empty;
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpContent httpContent = new StringContent(param, Encoding.UTF8))
                {
                    return httpClient.PostAsync(url, httpContent).Result.Content.ReadAsStringAsync();
                }
            }
        }
#endif
        /// <summary>
        /// http get 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="json">请求参数</param>
        /// <returns></returns>
        public static string PostJson(string url, string json)
        {
            ArgumentsUtils.CheckArgumentNull("url", url);
            json = json ?? string.Empty;
#if !(NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET472 || NET48 || NET481 || NET482 || NETSTANDARD1_0)
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpContent httpContent = new StringContent(json, Encoding.UTF8, HttpContentType.APPLICATION_JSON))
                {
                    return httpClient.PostAsync(url, httpContent).Result.Content.ReadAsStringAsync().Result;
                }
            }
#else
            return Utility.DefaultHttpCollect.Default.PostJson(url,json);
#endif
        }
        public static string PostForm(string url, string param)
        {
            ArgumentsUtils.CheckArgumentNull("url", url);
            param = param ?? string.Empty;
#if !(NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET472 || NET48 || NET481 || NET482 || NETSTANDARD1_0)
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpContent httpContent = new StringContent(param, Encoding.UTF8, HttpContentType.APPLICATION_X_WWW_FORM_URLENCODED))
                {
                    var res = httpClient.PostAsync(url, httpContent).Result;
                    using (var sr = new StreamReader(res.Content.ReadAsStreamAsync().Result, Encoding.GetEncoding("iso-8859-1")))
                    {
                        return sr.ReadToEnd();
                    }
                   // return httpClient.PostAsync(url, httpContent).Result.Content.ReadAsStringAsync().Result;
                }
                
            }
#else
            return Utility.DefaultHttpCollect.Default.PostString(url, param);
#endif
        }
#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2
        /// <summary>
        /// http 上传文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public static string Upload(string url, FileInfo fileInfo)
        {
            ArgumentsUtils.CheckArgumentNull("url", url);
            ArgumentsUtils.CheckArgumentObjectNull("fileInfo", fileInfo);
            return string.Empty;
        }
        /// <summary>
        /// http 上传文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public static string Upload(string url, Stream stream)
        {
            ArgumentsUtils.CheckArgumentNull("url", url);
            ArgumentsUtils.CheckArgumentObjectNull("stream", stream);
            return string.Empty;
        }
#endif
        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public static string WebClientDownload(string url)
        {
            ArgumentsUtils.CheckArgumentNull("url", url);
#if !NETCOREAPP1_0 && !NETCOREAPP1_1 && !NETCOREAPP1_2 && !NETSTANDARD1_0  && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6
            using (System.Net.WebClient webClient = new System.Net.WebClient())
            {
                return webClient.DownloadString(url);
            }
#else
            return string.Empty;
#endif
        }
    }
}
