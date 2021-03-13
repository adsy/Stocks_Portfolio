using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using Serilog;
using Serilog.Events;

namespace StockAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //configure logger object to write to file
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(
                    path: "logs\\log-.txt",
                    outputTemplate: "{Timestamp:yyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    // How often should we create a new file - RollingInterval.Day
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: LogEventLevel.Information
                ).CreateLogger();

            try
            {
                Log.Information("Application is starting");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                Log.Fatal("Application exited with fatal error -- " + e.Message);
            }
            finally
            {
                Log.CloseAndFlush();
            }
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