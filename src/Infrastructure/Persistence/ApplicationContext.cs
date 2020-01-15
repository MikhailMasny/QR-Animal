using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Domain.Entities;
using Masny.QRAnimal.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Infrastructure.Persistence
{
    /// <summary>
    /// Контекст для взаимодействия с приложения.
    /// </summary>
    public class ApplicationContext : IdentityDbContext<ApplicationUser>, IApplicationContext
    {
        // QRCode entities.
        public DbSet<QRCode> QRCodes { get; set; }

        // Animal entities.
        public DbSet<Animal> Animals { get; set; }

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="options">Параметры.</param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            // Использовать, если не требуется выполнение миграции в режиме реального времени.
            // Windows: Server=(localdb)\\mssqllocaldb;Database=QRAnimalApp;Trusted_Connection=True;MultipleActiveResultSets=true
            // Docker: Server=db;Database=QRAnimalApp;User=sa;Password=Your_password123;Trusted_Connection=False;MultipleActiveResultSets=true
            Database.Migrate();
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options, bool isTest)
            : base(options) 
        {
            // TODO: Восстановить на RuntimeMigration (commit 8b76cefb)
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
