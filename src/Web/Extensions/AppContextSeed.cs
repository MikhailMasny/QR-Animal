using Masny.QRAnimal.Infrastructure.Identity;
using Masny.QRAnimal.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Masny.QRAnimal.Web.Extensions
{
    /// <summary>
    /// Метод расширения для заполнения базы данных.
    /// </summary>
    public static class AppContextSeed
    {
        private const string logErrorMessage = "Error.";

        /// <summary>
        /// Заполнить базу данных.
        /// </summary>
        /// <param name="app">Строитель приложения.</param>
        public static void UseDataSeed(this IApplicationBuilder app)
        {
            app = app ?? throw new ArgumentNullException(nameof(app));

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context = services.GetRequiredService<ApplicationContext>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    ApplicationContextSeed.IdentitySeedAsync(context, userManager, roleManager).Wait();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, logErrorMessage);
                }
            }
        }
    }
}
