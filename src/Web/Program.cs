using Masny.QRAnimal.Infrastructure.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Masny.QRAnimal.Web
{
    public class Program
    {
        private const string url = "http://*:85";

        public static void Main(string[] args)
        {
            Log.Logger = SerilogConfiguration.LoggerConfig();

            try
            {
                Log.Information("Starting web host");

                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
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
                    webBuilder.UseUrls(url);
                    webBuilder.UseStartup<Startup>();
                });
    }
}
