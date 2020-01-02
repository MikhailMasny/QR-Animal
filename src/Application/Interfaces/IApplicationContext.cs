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
        /// <summary>
        /// DbSet QRCodes.
        /// </summary>
        public DbSet<QRCode> QRCodes { get; set; }

        /// <summary>
        /// DbSet Animals.
        /// </summary>
        public DbSet<Animal> Animals { get; set; }

        /// <summary>
        /// Сохранить данные (переопределенный метод).
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Результат сохранения.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
