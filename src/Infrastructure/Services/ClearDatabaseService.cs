using Masny.QRAnimal.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Infrastructure.Services
{
    /// <summary>
    /// Сервис для очистки базы данных.
    /// </summary>
    public class ClearDatabaseService : IClearDatabaseService
    {
        private const string _doWorkMessage = "Marked data for deletion cleared.";

        private readonly ILogger _logger;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="logger">Логгер.</param>
        public ClearDatabaseService(ILogger<ClearDatabaseService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public async Task DoWork()
        {
            _logger.LogInformation(_doWorkMessage);

            await Task.CompletedTask;
        }
    }
}
