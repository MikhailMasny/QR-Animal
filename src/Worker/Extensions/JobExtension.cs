using Coravel;
using Masny.QRAnimal.Worker.Jobs;
using Microsoft.AspNetCore.Builder;
using System;

namespace Masny.QRAnimal.Worker.Extensions
{
    /// <summary>
    /// Метод расширения для создания задач в конвейере.
    /// </summary>
    public static class JobExtension
    {
        /// <summary>
        /// Создать задачи.
        /// </summary>
        /// <param name="app">Строитель приложения.</param>
        public static void UseCoravelScheduler(this IApplicationBuilder app)
        {
            app = app ?? throw new ArgumentNullException(nameof(app));

            var serviceProvider = app.ApplicationServices;

            serviceProvider.UseScheduler(scheduler =>
            {
                scheduler.Schedule<ClearDatabaseJob>()
                         .Weekly();
            });
        }
    }
}
