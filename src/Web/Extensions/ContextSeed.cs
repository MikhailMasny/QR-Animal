using Masny.QRAnimal.Infrastructure.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

        /// <summary>
        /// Заполнить базу данных.
        /// </summary>
        /// <param name="host">Хост приложения.</param>
        public static void Initialize(IHost host)
        {
            host = host ?? throw new ArgumentNullException(nameof(host));

            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            try
            {
                ApplicationContextSeed.IdentitySeedAsync(services).Wait();
            }
            catch (Exception ex)
            {
                Log.Error(ex, logErrorMessage);
            }
        }
    }
}
