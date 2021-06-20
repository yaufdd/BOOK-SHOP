using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BookShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        
        // public static IHostBuilder CreateHostBuilder(string[] args) =>
        //     Host.CreateDefaultBuilder(args)
        //         .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder
                        .UseKestrel(options =>
                        {
                            options.Limits.MaxConcurrentConnections = 100;
                            options.Limits.MaxConcurrentUpgradedConnections = 100;
                            options.Limits.MaxRequestBodySize = (long) 10 * 1024 * 1024 * 1024;
                        })
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .ConfigureLogging((host, builder) => { builder.SetMinimumLevel(LogLevel.Trace); })
                        .ConfigureAppConfiguration((hostContext, config) =>
                        {
                            var env = hostContext.HostingEnvironment;
                            config.Sources.Clear();
                            string dockerSettings = Environment.GetEnvironmentVariable("DOCKER_APPSETTINGS") ?? "dummy";
                            config
                                .SetBasePath(env.ContentRootPath)
                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                                .AddJsonFile(dockerSettings, optional: true)
                                .AddCommandLine(args)
                                .AddEnvironmentVariables();
                        })
                        .UseIISIntegration()
                        .UseDefaultServiceProvider((context, options) =>
                        {
                            options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
                        }).UseStartup<Startup>();
                });
    }
}