using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Utility.AspNetCore.Consul.Extensions;
using Utility.AspNetCore.Consul.Model;
using Utility.AspNetCore.Zipkin.Extensions;

namespace Company.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ServiceEntity>(Configuration.GetSection("Service")); 
            services.AddRazorPages();
            services.AddControllers();//不添加 api consul调用失败 服务 注册失败
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
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            //暂时 放弃 直接放在iis上运行算了

            //路劲新 映射 url docker 又不支持 有些可以有些不可以 注意：html 文件的js css等文件路劲没映射成功
            //app.UseMiddleware<UrlMiddleware>();
            //app.UseFileServer();
            //wwwroot 有些 404 请求不了 
            //var options = new RewriteOptions()
            //                .AddRedirect("admin/(.*)", "src/admin/$1")
            //                .AddRedirect("company/(.*)", "src/ui/$1")
            //                .AddRedirect("js/index.js", "src/js/index.js");
            //app.UseRewriter(options);

            app.UseStaticFiles();
            //发布时文件找不到 只能放在 wwwroot 这里了 docker 也不支持 只能用中间重定向
            //可用参考：https://www.cnblogs.com/jidanfan/p/11670043.html
            app.UseStaticFiles(new StaticFileOptions()
            {

                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory() + "\\src\\ui")),
                RequestPath = "/company",
                ServeUnknownFileTypes = true
            });
            app.UseStaticFiles(new StaticFileOptions()
            {

                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory() + "\\src\\admin")),
                RequestPath = "/admin",
                ServeUnknownFileTypes = true
            });
            app.UseStaticFiles(new StaticFileOptions()
            {
                //Path.Combine(Environment.CurrentDirectory + "\\src\\js")
                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory() + "\\src\\js")),
                //FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory() + "\\src\\js")),
                RequestPath = "/js",
                ServeUnknownFileTypes = true
            });
            //直接使用模板

            app.UseRouting();

            app.UseAuthorization();
            if (bool.TryParse(Configuration["EnableService"], out bool enable))
            {
                if (enable)
                {
                    app.RegisterConsul(Configuration.GetSection("Service").Get<ServiceEntity>());
                }
            }
            if (bool.TryParse(Configuration["EnableZinkin"], out bool enableZinkin))
            {
                if (enableZinkin)
                {
                    app.RegisterZipkin(loggerFactory, lifetime, Configuration["Zinkin:Address"], Configuration["Zinkin:Name"]);
                }
            }
          
            lifetime.ApplicationStopped.Register(() => {
                app.StopConsul();
                app.StopZipkin();
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();//不添加 api consul调用失败 服务 注册失败
            });
        }
    }
}
