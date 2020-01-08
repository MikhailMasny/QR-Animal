using Masny.QRAnimal.Infrastructure.Identity;
using Masny.QRAnimal.Infrastructure.Logging;
using Masny.QRAnimal.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Web
{
    public class Program
    {
        private const string url = "http://*:85";
        private const string logErrorMessage = "Error.";

        public async static Task Main(string[] args)
        {
            Log.Logger = SerilogConfiguration.LoggerConfig();

            try
            {
                Log.Information("Starting web host");

                var host = CreateHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                    try
                    {
                        var context = services.GetRequiredService<ApplicationContext>();
                        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                        await ApplicationContextSeed.IdentitySeedAsync(context, userManager, roleManager);
                    }
                    catch (Exception ex)
                    {
                        var logger = loggerFactory.CreateLogger<Program>();
                        logger.LogError(ex, logErrorMessage);
                    }
                }

                host.Run();
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
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls(url);
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseSerilog();
                });
    }
}
