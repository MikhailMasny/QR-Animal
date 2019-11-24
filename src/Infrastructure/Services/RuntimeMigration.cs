using Masny.QRAnimal.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Masny.QRAnimal.Infrastructure.Services
{
    /// <summary>
    /// Применение миграции в режиме реального времени.
    /// </summary>
    public static class RuntimeMigration
    {
        /// <summary>
        /// Применить миграцию.
        /// </summary>
        /// <param name="app">Строитель приложения.</param>
        public static void ApplyMigration(IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetService<IdentityContext>().Database.Migrate();
        }
    }
}
