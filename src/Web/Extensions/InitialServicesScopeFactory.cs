using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Masny.QRAnimal.Web.Extensions
{
    /// <summary>
    /// Класс для создания начальной фабрики сервисов в определенной области видимости.
    /// </summary>
    public static class InitialServicesScopeFactory
    {
        /// <summary>
        /// Построить фабрику для выполнения определенных начальных заданий.
        /// </summary>
        /// <param name="host">Хост приложения.</param>
        public static void Build(IHost host)
        {
            host = host ?? throw new ArgumentNullException(nameof(host));

            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            RuntimeMigration.Initialize(services);
            ContextSeed.Initialize(services);
        }
    }
}
