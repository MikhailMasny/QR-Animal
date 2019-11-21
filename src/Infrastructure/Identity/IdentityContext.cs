using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Masny.QRAnimal.Infrastructure.Identity
{
    /// <summary>
    /// Контекст для взаимодействия с пользователем.
    /// </summary>
    public class IdentityContext : IdentityDbContext<AppUser>
    {
        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="options">Параметры.</param>
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
