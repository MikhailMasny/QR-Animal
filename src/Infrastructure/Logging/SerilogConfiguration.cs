using Masny.QRAnimal.Application.Models;
using Masny.QRAnimal.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Events;

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
            // UNDONE: Перенести в сервисы DI.

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var appSettingSection = configuration.GetSection("AppSettings");
            var appSettings = appSettingSection.Get<AppSettings>();
            var isDockerSupport = appSettings.IsDockerSupport;

            var connectionString = configuration.GetConnectionString(isDockerSupport.ToDbConnectionString());
            var tableName = "EventLog";

            //var object1 = new SqlColumn { ColumnName = "OtherData", DataType = SqlDbType.NVarChar, DataLength = 64 };
            //var object2 = new SqlColumn { ColumnName = "AnotherData", DataType = SqlDbType.NVarChar, DataLength = 64 };

            //var columnOption = new ColumnOptions();
            //columnOption.Store.Remove(StandardColumn.MessageTemplate);
            //columnOption.Store.Remove(StandardColumn.Properties);

            //columnOption.AdditionalColumns = new Collection<SqlColumn>();
            //columnOption.AdditionalColumns.Add(object1);
            //columnOption.AdditionalColumns.Add(object2);

            var serilogConfig =
                new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.MSSqlServer(connectionString,
                                     tableName,
                                     //columnOptions: columnOption,
                                     //schemaName: "log",
                                     autoCreateSqlTable: true)
                .CreateLogger();


            return serilogConfig;
        }
    }
}
