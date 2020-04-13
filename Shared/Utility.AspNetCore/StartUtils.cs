using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Utility.AspNetCore
{
    public class StartUtils
    {
        public static void Init<T>(string title,string[] args)where T:class
        {
            Console.Title = title;
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (string.IsNullOrEmpty(environment))
            {
                environment = "Development";
            }
            var builder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            var config = builder.Build();
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File(Path.Combine("logs", @"log.txt"), rollingInterval: RollingInterval.Hour)
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate)
                .ReadFrom.Configuration(config)
                .CreateLogger();
            CreateHostBuilder<T>(config, args).Run();
        }

        public static IWebHost CreateHostBuilder<T>(IConfiguration configuration, string[] args) where T : class =>
           WebHost.CreateDefaultBuilder(args)
             .CaptureStartupErrors(false)
            .UseDefaultServiceProvider(options => { options.ValidateScopes = false; })
             .UseStartup<T>()
             // .UseApplicationInsights()
             .UseContentRoot(Directory.GetCurrentDirectory())
             .UseConfiguration(configuration)
             .UseSerilog()
            //.UseUrls(configuration["applicationUrl"].Split(new char[] { ';' }))
             .Build();
    }
}
