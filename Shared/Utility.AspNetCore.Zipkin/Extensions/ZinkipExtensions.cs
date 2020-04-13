using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using zipkin4net;
using zipkin4net.Middleware;
using zipkin4net.Tracers;
using zipkin4net.Tracers.Zipkin;
using zipkin4net.Transport.Http;

namespace Utility.AspNetCore.Zipkin.Extensions
{
    public static class ZinkipExtensions
    {
#pragma warning disable CS0618 // 类型或成员已过时
        public static void RegisterZipkin(this IApplicationBuilder applicationBuilder,ILoggerFactory loggerFactory, 
            Microsoft.Extensions.Hosting.IApplicationLifetime lifetime,string url,string name)
#pragma warning restore CS0618 // 类型或成员已过时
        {
            lifetime.ApplicationStarted.Register(()=> {
                TraceManager.SamplingRate = 1.0f;//记录数据密度,1.0 代表所有记录
                var  tracingLogger = new TracingLogger(loggerFactory,"zipkin4net");//内存数据
                var  httpZipkinSender = new HttpZipkinSender(url,"application/json");
                var zipkinTracer = new ZipkinTracer(httpZipkinSender,new JSONSpanSerializer(),new Statistics());//注册zipkin
                var consoleTracer = new ConsoleTracer();//控制台输出
                TraceManager.RegisterTracer(zipkinTracer);//注册
                TraceManager.RegisterTracer(consoleTracer);//控制台输入日志
                TraceManager.Start(tracingLogger);//放到内存中的数据
                
            });
            ZinkipExtensions.Stop = () => {
                TraceManager.Stop();
            };
            applicationBuilder.UseTracing(name);
        }
        public static void StopZipkin(this IApplicationBuilder applicationBuilder)
        {
            ZinkipExtensions.Stop?.Invoke();
        }
        public static async Task<string> GetAsync(string url,string title,List<KeyValuePair<string,string>> header=null)
        {
            using HttpClient httpClient = new HttpClient(new TracingHandler(title));
            if (header != null)
            {
                foreach (KeyValuePair<string, string> item in header)
                {
                    httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }
            return await httpClient.GetStringAsync(url);
        }
        public static Action Stop { get; set; }
    }
}
