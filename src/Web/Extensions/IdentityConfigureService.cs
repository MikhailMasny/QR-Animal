using Masny.QRAnimal.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Masny.QRAnimal.Web.Extensions
{
    /// <summary>
    /// Метод расширения для добавления сервиса Identity.
    /// </summary>
    public static class IdentityConfigureServiceExtension
    {
        // UNDONE: Перенести в проект Infrastructure в класс DI

        /// <summary>
        /// Добавление сервиса Identity.
        /// </summary>
        /// <param name="services">DI контейнер.</param>
        public static void AddIdentityService(this IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var existingUserManager = scope.ServiceProvider.GetService<UserManager<AppUser>>();
            if (existingUserManager == null)
            {
                services.AddIdentity<AppUser, IdentityRole>()
                        .AddEntityFrameworkStores<IdentityContext>()
                        .AddDefaultTokenProviders();
            }
        }
    }
}
