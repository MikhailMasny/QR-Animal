﻿using Masny.QRAnimal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Masny.QRAnimal.Web.Extensions
{
    /// <summary>
    /// Класс для применения миграции в режиме реального времени.
    /// </summary>
    public static class RuntimeMigration
    {
        private const string logErrorMessage = "An error occurred migrating the DB.";

        /// <summary>
        /// Применить миграцию.
        /// </summary>
        /// <param name="host">Хост приложения.</param>
        public static void Initialize(IHost host)
        {
            host = host ?? throw new ArgumentNullException(nameof(host));

            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            try
            {
                services.GetService<ApplicationContext>().Database.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error(ex, logErrorMessage);
            }
        }
    }
}
