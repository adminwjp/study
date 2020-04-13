using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Company.Domain;
using Microsoft.EntityFrameworkCore;
using Company.Api.Data;
using Utility;
using Utility.EF;
using System.IO;
using Company.Domain.Core;
using Newtonsoft.Json;
using Utility.AspNetCore.Extensions;
using SocialContact.Api.Filter;
using Utility.AspNetCore.Consul.Model;
using Utility.AspNetCore.Consul.Extensions;
using Microsoft.Extensions.Options;
using Utility.AspNetCore.Zipkin.Extensions;

namespace Company.Api
{
    public class Startup
    {
        public static bool Test = true;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ServiceEntity>(Configuration.GetSection("Service"));
            var connectionString = Configuration["ConnectionString"];
            services.AddDbContext<CompanyDbContext>(
                b => b
               .UseMySql(connectionString, providerOptions =>
               {
                   providerOptions.EnableRetryOnFailure();
                   providerOptions.MigrationsAssembly("Company.Domain");
               })
               .AddInterceptors(new HintCommandInterceptor())) ;
            services.AddApiVersioningV1();
            services.AddScoped<ImgFactory, ImgFactory>();
            services.AddScoped<DbContext, CompanyDbContext>();
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddMvcV1().AddJsonV1()
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0) ;
            services.AddControllers();
            //    .ConfigureApiBehaviorOptions(options =>
            //{
            //    options.SuppressConsumesConstraintForFormFileParameters = true;
            //    options.SuppressInferBindingSourcesForParameters = true;
            //    options.SuppressModelStateInvalidFilter = true;
            //    options.SuppressMapClientErrors = true;
            //    options.ClientErrorMapping[404].Link =
            //        "https://httpstatuses.com/404";
            //});
            services.AddSwaggerErrorV1();
            services.AddSwaggerV1<EmptySwaggerOperationFilter>("v1","Company.Api");
            services.AddApiModelValidate();
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
            CompanyDbContext  companyDbContext = app.ApplicationServices.GetService<CompanyDbContext>();
           // companyDbContext.Database.EnsureDeleted();
            if (companyDbContext.Database.EnsureCreated())
            {
              
                IUnitWork unit = new UnitEfWork(companyDbContext);
                unit.Save();
            }
            app.UseStaticFiles();
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
            app.UseV1(env, "Company API V1");
        }
    }
}
