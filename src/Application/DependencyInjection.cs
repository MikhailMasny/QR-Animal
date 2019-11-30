using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Masny.QRAnimal.Application
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
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
