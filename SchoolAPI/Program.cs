using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using Winton.Extensions.Configuration.Consul;

namespace SchoolAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();

            var host = BuildWebHost(args, cancellationTokenSource);

            var loggingFactory = host.Services.GetService(typeof(ILoggerFactory)) as ILoggerFactory;
            var logger = loggingFactory.CreateLogger(nameof(Program));
            logger.LogInformation($"{Process.GetCurrentProcess().Id}");

            host.Run();

            cancellationTokenSource.Cancel();
        }

        public static IWebHost BuildWebHost(string[] args, CancellationTokenSource cancellationTokenSource) =>
            WebHost
                .CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(
                    (hostingContext, builder) =>
                    {
                        IHostingEnvironment env = hostingContext.HostingEnvironment;
                        var envs = Environment.GetEnvironmentVariables();
                        var consulSettingsKey = envs.Contains("consulConfig:settingsKey") ? 
                            envs["consulConfig:settingsKey"].ToString()
                            : "SchoolAPI-appsettings.json";
                        Console.WriteLine(consulSettingsKey);
                        builder
                            .AddConsul(
                                consulSettingsKey,//"SchoolAPI-appsettings.json",
                                cancellationTokenSource.Token,
                                options =>
                                {
                                    options.ConsulConfigurationOptions =
                                        cco => { cco.Address = new Uri("http://consul:8500"); };
                                    options.Optional = true;
                                    options.ReloadOnChange = true;
                                    options.OnLoadException = exceptionContext => { exceptionContext.Ignore = true; };
                                })
                            .AddEnvironmentVariables();
                    })
                .UseStartup<Startup>()
                .Build();
    }
}
