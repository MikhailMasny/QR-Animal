using Masny.QRAnimal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Application.Interfaces
{
    /// <summary>
    /// Интерфейс для контекста данных.
    /// </summary>
    public interface IApplicationContext
    {
        // QRCode entities.
        public DbSet<QRCode> QRCodes { get; set; }

        // Animal entities.
        public DbSet<Animal> Animals { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
