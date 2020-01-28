using Masny.QRAnimal.Application.Constants;
using Masny.QRAnimal.Infrastructure.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Masny.QRAnimal.Worker
{
    public static class Program
    {
        private const string url = "http://*:84";

        public static void Main(string[] args)
        {
            Log.Logger = SerilogConfiguration.LoggerConfig();

            try
            {
                Log.Information(CommonConstants.HostStart);
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, CommonConstants.HostTerminate);
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
