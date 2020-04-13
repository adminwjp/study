using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.AspNetCore.Extensions
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddJsonV1(this IMvcBuilder builder)
        {
            return builder.AddNewtonsoftJson(options =>
            {
                //.JsonSerializerOptions.MaxDepth = 10;
                // options.JsonSerializerOptions.PropertyNamingPolicy = JsonPropertyNamingPolicy.CamelCase;

                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //使用 AbC ab_c
                options.SerializerSettings.ContractResolver = JsonContractResolver.ObjectResolverJson;
                //设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            }
          )
          .AddXmlSerializerFormatters();
            //全局配置Json序列化处理
            //  .AddJsonOptions(options =>
            //  {
            //      options.JsonSerializerOptions.MaxDepth = 10;
            //       options.JsonSerializerOptions.PropertyNamingPolicy = JsonPropertyNamingPolicy.CamelCase;

            //  }
            //)

        }
        public static IMvcBuilder AddJsonV2(this IMvcBuilder builder)
        {
            return builder.AddNewtonsoftJson(options =>
            {
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();//json字符串大小写原样输出
                //设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            }
          )
          .AddXmlSerializerFormatters();

        }
    }
}
