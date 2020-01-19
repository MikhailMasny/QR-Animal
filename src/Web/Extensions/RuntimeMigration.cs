using Masny.QRAnimal.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Masny.QRAnimal.Web.Extensions
{
    /// <summary>
    /// Метод расширения для применения миграции в режиме реального времени.
    /// </summary>
    public static class RuntimeMigration
    {
        /// <summary>
        /// Применить миграцию.
        /// </summary>
        /// <param name="app">Строитель приложения.</param>
        public static void UseRuntimeMigration(this IApplicationBuilder app)
        {
            app = app ?? throw new ArgumentNullException(nameof(app));

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<ApplicationContext>().Database.Migrate();
            }
        }
    }
}
