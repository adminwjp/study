using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Company.Domain;
using Microsoft.EntityFrameworkCore;
using Company.Api.Data;
using Utility.AspNetCore.Extensions;
using Utility.AspNetCore.Consul;
using Utility.AspNetCore.Zipkin;
using Utility.Consul;
using Utility.AspNetCore.Filter;
using Utility.Domain.Repositories;
using Utility.Ef.Repositories;

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
            services.Configure<ConsulEntity>(Configuration.GetSection("Service"));
            var connectionString = Configuration["ConnectionString"];
            services.AddDbContext<CompanyDbContext>(
                b => b
               .UseMySql(connectionString, providerOptions =>
               {
                   providerOptions.EnableRetryOnFailure();
                   providerOptions.MigrationsAssembly("Company.Domain");
               })
               .AddInterceptors(new HintCommandInterceptor())) ;
            Utility.AspNetCore.Extensions.ServiceCollectionExtensions.AddApiVersioning(services);
            services.AddScoped<ImgFactory, ImgFactory>();
            services.AddScoped<DbContext, CompanyDbContext>();
            services.AddScoped(typeof(IRepository<>), typeof(BaseEfRepository<>));
            services.AddScoped(typeof(I<>), typeof(Impl<>));
            services.AddFilter().AddJson()
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
            services.AddSwagger<EmptySwaggerOperationFilter>("V1","Company.Api");
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
            }
            app.UseStaticFiles();
            app.UseAuthorization();
            
            lifetime.ApplicationStarted.Register(() => {
                app.RegisterConsul(Configuration);
                app.RegisterZipkin(loggerFactory, lifetime, Configuration);
            });
            lifetime.ApplicationStopped.Register(() => {
                app.StopConsul();
                app.StopZipkin();
            });
            app.Use(env, "Company API V1");
        }
    }
}
