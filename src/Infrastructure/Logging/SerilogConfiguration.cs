using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;

namespace Masny.QRAnimal.Infrastructure.Logging
{
    /// <summary>
    /// Serilog конфигурация.
    /// </summary>
    public static class SerilogConfiguration
    {
        /// <summary>
        /// Применение конфигурации из appsettings.
        /// </summary>
        /// <returns>Конфигурация Seriog.</returns>
        public static Logger LoggerConfig()
        {
            // UNDONE: Config для Docker
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var serilogConfig = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            return serilogConfig;
        }
    }
}
