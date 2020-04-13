using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SocialContact.Api.Filter;
using Utility;
using Utility.AspNetCore.Consul.Extensions;
using Utility.AspNetCore.Consul.Model;
using Utility.AspNetCore.Extensions;
using Utility.AspNetCore.Filter;
using Utility.AspNetCore.Zipkin.Extensions;

namespace OA.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<ServiceEntity>(Configuration.GetSection("Service"));
            services.AddControllers().AddJsonV2();
            services.AddMvcV1(/*new Type[] { typeof(ValidateActionParamFilter) }*/);
            services.AddApiModelValidate();
            services.AddApiVersioningV1();
            services.AddSwaggerErrorV1();
            //模型绑定 特性验证，自定义返回格式
            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    //options.InvalidModelStateResponseFactory = actionContext =>
            //    //{
            //    //    //获取验证失败的模型字段 
            //    //    var errors = actionContext.ModelState
            //    //    .Where(e => e.Value.Errors.Count > 0)
            //    //    .Select(e => e.Value.Errors.First().ErrorMessage)
            //    //    .ToList();
            //    //    var str = string.Join("|", errors);
            //    //    //设置返回内容
            //    //    var result = new
            //    //    {
            //    //        code = 40000,
            //    //        success = false,
            //    //        data = str,
            //    //        message="fail"
            //    //    };
            //    //    return new BadRequestObjectResult(result);
            //    //};
            //});
            services.AddSwaggerV1<EmptySwaggerOperationFilter>("v1","OA Api");
         
            var container =AutofacUtils.Builder;
            container.Populate(services);
            OA.Domain.CoreManager.RegisterAutofac(true);
            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
#pragma warning disable CS0618 // 类型或成员已过时
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Microsoft.Extensions.Hosting.IApplicationLifetime lifetime,ILoggerFactory loggerFactory)
#pragma warning restore CS0618 // 类型或成员已过时
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthorization();
            app.RegisterConsul(Configuration);
            if (bool.TryParse(Configuration["EnableZinkin"], out bool enable))
            {
                if (enable)
                {
                    app.RegisterZipkin(loggerFactory,lifetime, Configuration["Zinkin:Address"],Configuration["Zinkin:Name"]);
                }
            }
            OA.Domain.NhibernateManger.Initial(loggerFactory);
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "imgs")),
                RequestPath = "/imgs",
              
            });
            app.UseV1(env,"OA Api V1");
        }
    }
}
