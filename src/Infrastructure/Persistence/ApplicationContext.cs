using Masny.QRAnimal.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Masny.QRAnimal.Infrastructure.Persistence
{
    /// <summary>
    /// Контекст для взаимодействия с приложения.
    /// </summary>
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="options">Параметры.</param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            // Использовать, если не требуется выполнение миграции в режиме реального времени.
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
