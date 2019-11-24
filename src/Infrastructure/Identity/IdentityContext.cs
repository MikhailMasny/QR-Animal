using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Masny.QRAnimal.Infrastructure.Identity
{
    /// <summary>
    /// Контекст для взаимодействия с пользователем.
    /// </summary>
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="options">Параметры.</param>
        public IdentityContext(DbContextOptions<IdentityContext> options)
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
