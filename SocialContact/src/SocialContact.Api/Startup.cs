using System;
using System.IO;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using SocialContact.Domain.Core;
using SocialContact.Domain.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NHibernate.Tool.hbm2ddl;
using SocialContact.Api.Data;
using SocialContact.Api.Filter;
using Utility.AspNetCore.Extensions;
using Utility.Nhibernate;
using Utility.Nhibernate.Infrastructure;
using Utility.ObjectMapping;
using Utility.Domain.Uow;
using Utility.Nhibernate.Uow;
using Utility.Json;
using Utility.Redis;
using Utility.Es;
using Utility.Consul;

namespace SocialContact.Api
{
    public class Startup
    {
        private readonly ILoggerFactory _loggerFactory;
  
        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            this.Configuration = configuration;
            this._loggerFactory = loggerFactory;
        }
        public IConfiguration Configuration { get; }
        private void CheckSameSite(HttpContext httpContext, CookieOptions options)
        {
            if (options.SameSite == SameSiteMode.None)
            {
                var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
                if (MyUserAgentDetectionLib.DisallowsSameSiteNone(userAgent))
                {
                    options.SameSite = (SameSiteMode)(-1);
                }

            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ConsulEntity>(Configuration.GetSection("Service"));
            // services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            // .AddScheme<AuthenticationSchemeOptions, ApiAuthHandler>("Api", o => { })
            // .AddCookie(options =>
            // {
            // // Foward any requests that start with /api to that scheme
            // options.ForwardDefaultSelector = ctx =>
            // {
            // return ctx.Request.Path.StartsWithSegments("/api") ? "Api" : null;
            // };
            // options.AccessDeniedPath = "/account/denied";
            // options.LoginPath = "/account/login";
            // });
            //https://docs.microsoft.com/zh-cn/aspnet/core/security/cookie-sharing?view=aspnetcore-2.2
            //services.AddDataProtection()
            ////.PersistKeysToFileSystem("{PATH TO COMMON KEY RING FOLDER}")
            //.SetApplicationName("SharedCookieApp");

            //services.ConfigureApplicationCookie(options => {
            //    options.Cookie.Name = ".AspNet.SharedCookie";
            //});
            //注册Cookie认证服务
            // services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            // services.Configure<CookiePolicyOptions>(options =>
            // {
            // options.CheckConsentNeeded = context => true;
            // options.MinimumSameSitePolicy = SameSiteMode.None;

            // //options.MinimumSameSitePolicy = (SameSiteMode)(-1);
            // //options.OnAppendCookie = cookieContext =>
            // //    CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
            // //options.OnDeleteCookie = cookieContext =>
            // //    CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
            // });
            ServiceCollectionExtensions.AddHttpClient(services);
            //https://www.cnblogs.com/cmt/p/11347507.html
            ServiceCollectionExtensions.AddIISServerOptions(services);
            services.AddMemoryCache(options=> {
                options.ExpirationScanFrequency = TimeSpan.FromHours(2);
            });
            AutoMapperObjectMapper.Empty.Init(config=>
            {
                config.CreateMap<AdminInfo, QueryAdminInfoResultViewModel>()
                .ForMember(it => it.HeadPic, options => options.MapFrom(src => src.HeadPic.FileId));

                config.CreateMap<AdminRoleInfo, CategoryEntry>();

                config.CreateMap<AdminInfo, AdminEntry>()
                .ForMember(it => it.Role, options => options.MapFrom(src => src.Role.Category))
                .ForMember(it => it.HeadPic, options => options.MapFrom(src => src.HeadPic.FileId));

                config.CreateMap<AdminRoleInfo, RoleCategoryViewModel>()
                .ForMember(it => it.Label, options => options.MapFrom(src => src.Category))
                .ForMember(it => it.Value, options => options.MapFrom(src => src.Id))
                .ForMember(it => it.Children, options => options.MapFrom(src => src.Children));

                config.CreateMap<AdminRoleInfo, QueryRoleInfoResultViewModel>();
                
                config.CreateMap<UserInfo, UserEntry>()          
				.ForMember(it => it.HeadPic, options => options.MapFrom(src => src.HeadPic.FileId));
                config.CreateMap<SocialContact.Domain.Core.UserFileInfo, QueryUserFileInfoResultViewModel>();
                config.CreateMap<FileCategoryInfo, QueryFileCategoryInfoResultViewModel>();

                config.CreateMap<EdutionCategoryInfo, QueryEdutionCategoryInfoResultViewModel>();

                config.CreateMap<IconInfo, QueryIconInfoResultViewModel>()
                .ForMember(it => it.Admin, options => options.MapFrom(src => src.Admin));

                config.CreateMap<LikeCategoryInfo, QueryLikeCategoryInfoResultViewModel>();
                config.CreateMap<LikeCategoryInfo, CategoryViewModel>()
                .ForMember(it => it.Label, options => options.MapFrom(src => src.Category))
               .ForMember(it => it.Value, options => options.MapFrom(src => src.Id))
               .ForMember(it => it.Children, options => options.MapFrom(src => src.Children));

                config.CreateMap<LikeInfo, QueryLikeInfoResultViewModel>();
                config.CreateMap<MaritalStatusInfo, QueryMaritalStatusInfoResultViewModel>();


                config.CreateMap<SkillCategoryInfo, QuerySkillCategoryInfoResultViewModel>();
                config.CreateMap<SkillInfo, QuerySkillInfoResultViewModel>();
                config.CreateMap<UserLevelInfo, QueryUserLevelInfoResultViewModel>();

                config.CreateMap<UserStatusInfo, QueryUserStatusInfoResultViewModel>();

                config.CreateMap<UserTagCategoryInfo, QueryUserTagCategoryInfoResultViewModel>();
                config.CreateMap<UserTagInfo, QueryUserTagInfoResultViewModel>();
                config.CreateMap<JobCategoryInfo, QueryJobCategoryInfoResultViewModel>();

                config.CreateMap<JobCategoryInfo, CategoryViewModel>()
               .ForMember(it => it.Label, options => options.MapFrom(src => src.Category))
               .ForMember(it => it.Value, options => options.MapFrom(src => src.Id))
               .ForMember(it => it.Children, options => options.MapFrom(src => src.Children));



                config.CreateMap<WorkInfo, QueryWorkInfoResultViewModel>();

                config.CreateMap<IconInfo, IconViewModel>()
                .ForMember(it => it.Id, options => options.MapFrom(src => src.Id))
                .ForMember(it => it.Label, options => options.MapFrom(src => src.Name))
                .ForMember(it => it.Value, options => options.MapFrom(src => src.Style));

                config.CreateMap<MenuInfo, QueryMenuInfoResultViewModel>();

                config.CreateMap<MenuInfo, MenuViewModel>()
                .ForMember(it => it.Label, options => options.MapFrom(src => src.MenuName))
                .ForMember(it => it.Value, options => options.MapFrom(src => src.Id))
                .ForMember(it => it.Style, options => options.MapFrom(src => src.Icon.Style))
                .ForMember(it => it.Href, options => options.MapFrom(src => src.Href))
                .ForMember(it => it.Group, options => options.MapFrom(src => src.MenuGroup));

                config.CreateMap<UserInfo, QueryUserInfoResultViewModel>()
                .ForMember(it => it.HeadPic, options => options.MapFrom(src => src.HeadPic.FileId))
                .ForMember(it => it.CardPhoto1, options => options.MapFrom(src => src.CardPhoto1.FileId))
                .ForMember(it => it.CardPhoto2, options => options.MapFrom(src => src.CardPhoto2.FileId))
                .ForMember(it => it.HandCardPhoto1, options => options.MapFrom(src => src.HandCardPhoto1.FileId))
                .ForMember(it => it.HandCardPhoto2, options => options.MapFrom(src => src.HandCardPhoto2.FileId))
                .ForMember(it => it.Edution, options => options.MapFrom(src => src.Edution.Category))
                .ForMember(it => it.Status, options => options.MapFrom(src => src.Status.Name))
                .ForMember(it => it.Marital, options => options.MapFrom(src => src.Marital.Name))
                .ForMember(it => it.Level, options => options.MapFrom(src => src.Level.Name))
                ;config.CreateMap<UserMenuInfo, QueryUserMneuInfoResultViewModel>();

            });
            services.AddTransient<IStartupFilter,
               RequestSetOptionsStartupFilter>();
            services.AddSingleton<IObjectMapper>(it=> AutoMapperObjectMapper.Empty);

            services.AddSingleton<Models.AuthrizeValidator>();
            //services.AddCorsV1(); 

            services.AddSingleton<AppSessionFactory>(new AppSessionFactory(config => {
                config = config.Configure("Config/hibernate.cfg.xml");
                //config.AddXmlFile("Config/hbm/social_contact.hbm.xml");
                config.Interceptor = new SQLWatcher(_loggerFactory);
                foreach (var item in Directory.GetFiles("Config/hbm", "*.hbm.xml"))
                {
                    config.AddXmlFile(item);
                }
                SchemaExport export = new SchemaExport(config);
                export.SetOutputFile(Path.Combine(Directory.GetCurrentDirectory(), "sql.sql")); //设置输出目录
                // export.Drop(true, true);//设置生成表结构存在性判断,并删除
                export.Create(false, false);//设置是否生成脚本,是否导出来
            })) ;
            services.AddScoped(it => it.GetService<AppSessionFactory>().OpenSession());
            services.AddScoped<IUnitWork, NhibernateUnitWork>();
            services.AddScoped<IUserFileService,DefaultUserFileService>();
            services.AddSingleton<Data.Core>();
            // services.AddSingleton<Data.EsService>();
            var redisHost =  Configuration["redis:connectionString"];//Configuration["RedisConnectionString"];
            var mongoHost = Configuration["mongo:connectionString"];
            var elasticsearchHost = Configuration["elasticsearch:connectionString"];
            //services.AddDistributedRedisCache(options =>
            //{
            //    options.Configuration = redisHost;
            //    options.InstanceName = "Demo";
            //});
            string[] elasticsearchHosts =JsonHelper.ToObject<string[]>(elasticsearchHost);
            services.AddSingleton<IRedisCache>(it=>new StackExchangeCache(redisHost));
            services.AddSingleton<ElasticsearchHelper>(it => new ElasticsearchHelper(elasticsearchHosts));
            services.AddControllers();
            ServiceCollectionExtensions.AddApiVersioning(services);
            services.AddSingleton(new System.Text.Json.JsonSerializerOptions() { MaxDepth=0, PropertyNamingPolicy=JsonPropertyNamingPolicy.ObjectResolverJson });
            ServiceCollectionExtensions.AddFilter(services,new Type[] { typeof(ActionAuthFilter) }).AddJson()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            //全局配置Json序列化处理
            //  .AddJsonOptions(options =>
            //  {
            //      options.JsonSerializerOptions.MaxDepth = 10;
            //       options.JsonSerializerOptions.PropertyNamingPolicy = JsonPropertyNamingPolicy.CamelCase;

            //  }
            //)
            ServiceCollectionExtensions.AddSwagger<SwaggerOperationFilter>(services, "V1", "SocialContact API");
            services.AddApiModelValidate();
        }
        private static void HandleMapTest(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map Test ");
            });
        }
        private static void HandleBranch(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var branchVer = context.Request.Query["token"];
                await context.Response.WriteAsync($"Branch used = {branchVer}");
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var logger = _loggerFactory.CreateLogger<Startup>();
          
            //app.UseSession();
            var core = app.ApplicationServices.GetService<Data.Core>();
            core.Initial();//订阅信息初始化
            core.InitialMain();//发布信息初始化
            app.MapWhen(context =>  context.Request.Query.ContainsKey("token"),
                                 HandleBranch);
            app.Map("/map", HandleMapTest);
            app.Use(async (context, next) =>
            {
                // Do work that doesn't write to the Response.
                await next.Invoke();
                // Do logging or other work that doesn't write to the Response.
            });

            //app.UseCors(options =>
            //{
            //    options.AllowAnyHeader();
            //    options.AllowAnyMethod();
            //    options.AllowAnyOrigin();
            //    options.AllowCredentials();
            //});
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); 
                logger.LogInformation("Development environment");
            }
            else
            {
                // Non-development service configuration
                logger.LogInformation("Environment: {EnvironmentName}", env.EnvironmentName);
            }
            // Make work identity server redirections in Edge and lastest versions of browers. WARN: Not valid in a production environment.
            //app.Use(async (context, next) =>
            //{
            //    context.Response.Headers.Add("Content-Security-Policy", "script-src 'unsafe-inline'");
            //    await next();
            //});
          
            app.UseForwardedHeaders();
            //lifetime.ApplicationStarted.Register(() => {
            //    Utility.AspNetCore.Consul.ConsulExtensions.RegisterConsul(app, Configuration);
            //    Utility.AspNetCore.Zipkin.ZinkinExtensions.RegisterZipkin(app, loggerFactory, lifetime, Configuration["Zinkin:Address"], Configuration["Zinkin:Name"]);
            //});
            //lifetime.ApplicationStopped.Register(() => {
            //    Utility.AspNetCore.Consul.ConsulExtensions.StopConsul(app);
            //    Utility.AspNetCore.Zipkin.ZinkinExtensions.StopZipkin(app);
            //});
            // app.UseCors("AllowAllOrigins");

            // app.UseAuthorization();
            Utility.AspNetCore.Extensions.ApplicationBuilderExtensions.Use(app, env,"SocialContact API V1");
        }
    }
    public class ApiAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ClaimsPrincipal _id;

        public ApiAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            var id = new ClaimsIdentity("Api");
            id.AddClaim(new Claim(ClaimTypes.Name, "Hao", ClaimValueTypes.String, "Api"));
            _id = new ClaimsPrincipal(id);
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
            => Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(_id, "Api")));
    }
}
