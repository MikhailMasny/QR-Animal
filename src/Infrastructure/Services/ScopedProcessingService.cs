using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Infrastructure.Services
{
    public interface IScopedProcessingService
    {
        Task DoWork(CancellationToken stoppingToken);
    }

    public class ScopedProcessingService : IScopedProcessingService
    {
        private int executionCount = 0;
        private readonly ILogger _logger;

        public ScopedProcessingService(ILogger<ScopedProcessingService> logger)
        {
            _logger = logger;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;

                _logger.LogInformation("Scoped Processing Service is working. Count: {Count}", executionCount);

                // Выполнение каждый час
                await Task.Delay(3600000, stoppingToken);
            }
        }
    }
}
