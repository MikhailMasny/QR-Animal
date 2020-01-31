using Masny.QRAnimal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace Masny.QRAnimal.Web.Extensions
{
    /// <summary>
    /// Класс для применения миграции в режиме реального времени.
    /// </summary>
    public static class RuntimeMigration
    {
        private const string logErrorMessage = "An error occurred migrating the DB.";
        private const string logInformationMessage = "The database is successfully migrated.";

        /// <summary>
        /// Применить миграцию.
        /// </summary>
        /// <param name="serviceProvider">Провайдер сервисов.</param>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            try
            {
                var appContextService = serviceProvider.GetRequiredService<ApplicationContext>();
                appContextService.Database.Migrate();

                Log.Information(logInformationMessage);
            }
            catch (Exception ex)
            {
                Log.Error(ex, logErrorMessage);
            }
        }
    }
}
