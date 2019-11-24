using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Infrastructure.Identity;
using Masny.QRAnimal.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Masny.QRAnimal.Infrastructure
{
    /// <summary>
    /// Метод расширения для добавления сервисов уровня Infrastructure.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Добавить определенные сервисы.
        /// </summary>
        /// <param name="services">DI контейнер.</param>
        /// <returns>Сервисы.</returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<IdentityContext>()
                    .AddDefaultTokenProviders();

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddTransient<IDateTime, DateTimeService>();

            return services;
        }
    }
}
