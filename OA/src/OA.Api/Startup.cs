using System;
using System.IO;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Utility.AspNetCore.Extensions;
using Utility.AspNetCore.Filter;
using Utility.Consul;
using Utility.Ioc;

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
            services.Configure<ConsulEntity>(Configuration.GetSection("Service"));
            services.AddControllers().AddJson();
            Utility.AspNetCore.Extensions.ServiceCollectionExtensions.AddFilter(services/*,new Type[] { typeof(ValidateActionParamFilter) }*/);
            services.AddApiModelValidate();
            Utility.AspNetCore.Extensions.ServiceCollectionExtensions.AddApiVersioning(services);
            Utility.AspNetCore.Extensions.ServiceCollectionExtensions.AddSwagger<EmptySwaggerOperationFilter>(services, "v1", "OA Api");
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
         
            var container =AutofacIocManager.Instance.Builder;
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
            lifetime.ApplicationStarted.Register(()=> {
                Utility.AspNetCore.Consul.ConsulExtensions.RegisterConsul(app, Configuration);
                Utility.AspNetCore.Zipkin.ZinkinExtensions.RegisterZipkin(app, loggerFactory, lifetime, Configuration["Zinkin:Address"], Configuration["Zinkin:Name"]);
            });
            lifetime.ApplicationStopped.Register(() => {
                Utility.AspNetCore.Consul.ConsulExtensions.StopConsul(app);
                Utility.AspNetCore.Zipkin.ZinkinExtensions.StopZipkin(app);
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "imgs")),
                RequestPath = "/imgs",
              
            }); 
            Utility.AspNetCore.Extensions.ApplicationBuilderExtensions.Use(app,env, "OA Api V1");
        }
    }
}
