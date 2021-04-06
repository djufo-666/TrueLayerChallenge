using log4net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TrueLayerChallenge.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string rootDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            System.IO.Directory.SetCurrentDirectory(rootDirectory);

#if DEBUG
            SetupLogging(rootDirectory);
#endif

            var host = CreateHostBuilder(args).Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

#if DEBUG
        static ILog SetupLogging(string pathToContentRoot)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            log4net.Config.XmlConfigurator.Configure(logRepository, new FileInfo(Path.Combine(pathToContentRoot, "log4net.config")));

            ILog log = log4net.LogManager.GetLogger(typeof(Program));
            log.Info($"Program Started. Application location {pathToContentRoot}");
            log.Info($"Current Directory set to:{Directory.GetCurrentDirectory()}");

            return log;
        }
#endif

    }
}
