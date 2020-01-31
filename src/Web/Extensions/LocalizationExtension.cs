using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Masny.QRAnimal.Web.Extensions
{
    /// <summary>
    /// Метод расширения для локализации в конвейере.
    /// </summary>
    public static class LocalizationExtension
    {
        /// <summary>
        /// Применить локализацию.
        /// </summary>
        /// <param name="app">Строитель приложения.</param>
        public static void UseLocalization(this IApplicationBuilder app)
        {
            app = app ?? throw new ArgumentNullException(nameof(app));

            var locOptions = app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);
        }
    }
}
