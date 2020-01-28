using Masny.QRAnimal.Application.Constants;
using Masny.QRAnimal.Infrastructure.Logging;
using Masny.QRAnimal.Web.Extensions;
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
                Log.Information(CommonConstants.HostStart);

                IHost host = CreateHostBuilder(args).Build();

                InitialServicesScopeFactory.Build(host);

                host.Run();
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
