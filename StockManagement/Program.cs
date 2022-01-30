using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
              .Enrich.FromLogContext()
              .WriteTo.File(@"C:\inetpub\wwwroot\Logs\StockManagement\log.txt",
                  restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error,
                  outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss zzz}[{Level:u3}]{Message:lj}{NewLine}{Exception}",
                  rollingInterval: RollingInterval.Day)
              .CreateLogger();


            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
