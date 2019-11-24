using Masny.QRAnimal.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Masny.QRAnimal.Infrastructure.Logging
{
    /// <summary>
    /// Реализация базового логгера.
    /// </summary>
    /// <typeparam name="T">Обобщенный тип.</typeparam>
    public class LoggerAdapter<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="loggerFactory">Фабрика.</param>
        public LoggerAdapter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }

        /// <inheritdoc />
        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }

        /// <inheritdoc />
        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }
    }
}
