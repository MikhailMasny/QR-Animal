using Masny.QRAnimal.Infrastructure.Identity;
using Masny.QRAnimal.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace Masny.QRAnimal.Web.Extensions
{
    /// <summary>
    /// Класс для заполнения контекста.
    /// </summary>
    public static class ContextSeed
    {
        private const string logErrorMessage = "An error occurred seeding the DB.";
        private const string logInformationMessage = "The database is successfully seeded.";

        /// <summary>
        /// Заполнить базу данных.
        /// </summary>
        /// <param name="serviceProvider">Провайдер сервисов.</param>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            try
            {
                var contextOptions = serviceProvider.GetRequiredService<DbContextOptions<ApplicationContext>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                using var applicationContext = new ApplicationContext(contextOptions);

                ApplicationContextSeed.IdentitySeedAsync(applicationContext, userManager, roleManager).GetAwaiter().GetResult();

                Log.Information(logInformationMessage);
            }
            catch (Exception ex)
            {
                Log.Error(ex, logErrorMessage);
            }
        }
    }
}
