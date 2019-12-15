using System.Threading;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Application.Interfaces
{
    /// <summary>
    /// Интерфейс для очистки базы данных.
    /// </summary>
    public interface IClearDatabaseService
    {
        /// <summary>
        /// Запустить работу сервиса.
        /// </summary>
        /// <param name="stoppingToken">Токен для прерывания работы.</param>
        Task DoWork(CancellationToken stoppingToken);
    }
}
