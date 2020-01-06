using Masny.QRAnimal.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Worker.Services
{
    /// <summary>
    /// Background сервис для очистки базы данных.
    /// </summary>
    public class ClearDatabaseHostedService : BackgroundService
    {
        private const string _executeMessage = "Clear database service running.";
        private const string _doWorkMessage = "Clear database service is working.";
        private const string _stopMessage = "Clear database service is stopping.";

        private readonly ILogger<ClearDatabaseHostedService> _logger;

        /// <summary>
        /// Список сервисов в DI.
        /// </summary>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="services">Список сервисов в DI.</param>
        /// <param name="logger">Логгер.</param>
        public ClearDatabaseHostedService(IServiceProvider services,
                                          ILogger<ClearDatabaseHostedService> logger)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Выполнить работу сервиса.
        /// </summary>
        /// <param name="stoppingToken">Токен для прерывания работы.</param>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(_executeMessage);

            await DoWork(stoppingToken);
        }

        /// <summary>
        /// Запустить работу сервису.
        /// </summary>
        /// <param name="stoppingToken">Токен для прерывания работы.</param>
        private async Task DoWork(CancellationToken stoppingToken)
        {
            _logger.LogInformation(_doWorkMessage);

            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                         .GetRequiredService<IClearDatabaseService>();

                await scopedProcessingService.DoWork(stoppingToken);
            }
        }

        /// <summary>
        /// Остановить работу сервиса.
        /// </summary>
        /// <param name="stoppingToken">Токен для прерывания работы.</param>
        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(_stopMessage);

            await Task.CompletedTask;
        }
    }
}
