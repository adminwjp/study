using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace SocialContact.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "SocialContact.Api";
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (string.IsNullOrEmpty(environment))
            {
                environment = "Development";
            }
            var builder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true)
                .AddXmlFile("config/address.xml",optional:false,reloadOnChange:true)
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
            CreateHostBuilder(config, args).Run();
        }

        public static IWebHost CreateHostBuilder(IConfiguration configuration, string[] args) =>
           WebHost.CreateDefaultBuilder(args)
             .CaptureStartupErrors(false)
             .UseStartup<Startup>()
             // .UseApplicationInsights()
             .UseContentRoot(Directory.GetCurrentDirectory())
             .UseConfiguration(configuration)
             .UseSerilog()
             .Build();
    }
}
