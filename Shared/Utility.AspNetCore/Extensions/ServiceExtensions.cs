using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using SocialContact.Api.Filter;
using Swashbuckle.AspNetCore.SwaggerGen;
using Utility.AspNetCore.Data;
using Utility.AspNetCore.Filter;

namespace Utility.AspNetCore.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddHttpClientV1(this IServiceCollection services)
        {
            services.AddHttpClient();
        }
        public static IMvcBuilder AddMvcV1(this IServiceCollection services,Type[] filters=null)
        {
            return services.AddMvc(options =>
             {
                // options.ModelBinderProviders.Insert(0, new CustomModelBinderProvider());
                //options.InputFormatters.Insert(0, new XDocumentInputFormatter());
                 options.Conventions.Add(new ApiControllerVersionConvention());
                 options.Filters.Add<HttpGlobalExceptionFilter>();

                 if(filters!=null)
                 {
                     foreach (var item in filters)
                     {
                         options.Filters.Add(item);
                     }
                 }
                 options.Filters.Add<APIResultFilter>();
             });
        }
        public static void AddIISServerOptionsV1(this IServiceCollection services)
        {
            //https://www.cnblogs.com/cmt/p/11347507.html
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
        }
        /// <summary>
        /// 添加 api 模型 验证 自动取消 需要手动调用
        /// </summary>
        /// <param name="services"></param>
        public static void AddApiModelValidate(this IServiceCollection services)
        {
            //禁用默认行为
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }
        public static void AddSwaggerErrorV1(this IServiceCollection services)
        {
            services.AddSingleton<IActionDescriptorCollectionProvider, CustomActionDescriptorCollectionProvider>();
            services.AddSingleton<IApiDescriptionGroupCollectionProvider, CustomApiDescriptionGroupCollectionProvider>();
            foreach (var item in services)
            {
                if (item.ServiceType.FullName == "Microsoft.AspNetCore.Mvc.ApiExplorer.DefaultApiDescriptionProvider")
                {
                    services.Remove(item);
                    break;
                }

            }
            services.AddSingleton<IApiDescriptionProvider, CustomApiDescriptionProvider>();

        }
        /// <summary>
        /// services.AddSingleton &lt; Swashbuckle.AspNetCore.SwaggerGen.ISchemaGenerator, Swashbuckle.AspNetCore.SwaggerGen.JsonSchemaGenerator &gt; ();
        ///<para> Register the Swagger generator, defining 1 or more Swagger documents</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="verion"></param>
        /// <param name="title"></param>
        public static void AddSwaggerV1<T>(this IServiceCollection services,string verion,string title)where T: IOperationFilter, IDocumentFilter
        {
            services.AddSwaggerGen(c =>
            {
                //使用过滤器单独对某些API接口实施认证
                c.OperationFilter<T>();
                //参数结果循环引用，会导致电脑司机 无效 一直加载中
                c.SwaggerDoc(verion, new OpenApiInfo { Title = title, Version = verion });
                // Set the comments path for the Swagger JSON and UI.
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });

        }
        public static void AddApiVersioningV1(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;//return versions in a response header
                options.DefaultApiVersion = new ApiVersion(1, 0);//default version select 
                options.AssumeDefaultVersionWhenUnspecified = true;//if not specifying an api version,show the default version
            });
        }
        public static void AddCorsV1(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                         builder =>
                         {
                             //builder.AllowAnyOrigin().AllowAnyMethod(); ;
                             builder.AllowAnyHeader();
                             builder.AllowAnyMethod();
                             builder.AllowAnyOrigin();
                             //builder.AllowCredentials();
                         });

            });
        }
    }
}
