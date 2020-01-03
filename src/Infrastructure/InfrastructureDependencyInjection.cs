using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Infrastructure.Identity;
using Masny.QRAnimal.Infrastructure.Persistence;
using Masny.QRAnimal.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Masny.QRAnimal.Infrastructure
{
    /// <summary>
    /// Метод расширения для добавления сервисов уровня Infrastructure.
    /// </summary>
    public static class InfrastructureDependencyInjection
    {
        /// <summary>
        /// Добавить определенные сервисы.
        /// </summary>
        /// <param name="services">DI контейнер.</param>
        /// <returns>Сервисы.</returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationContext>()
                    .AddDefaultTokenProviders();

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IMessageSender, EmailSenderService>();
            services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IQRCodeGeneratorService, QRCodeGeneratorService>();

            return services;
        }
    }
}
