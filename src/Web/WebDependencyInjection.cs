using FluentValidation.AspNetCore;
using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using System.Globalization;

namespace Masny.QRAnimal.Web
{
    /// <summary>
    /// Метод расширения для добавления сервисов уровня Infrastructure.
    /// </summary>
    public static class WebDependencyInjection
    {
        /// <summary>
        /// Добавить определенные сервисы.
        /// </summary>
        /// <param name="services">DI контейнер.</param>
        /// <returns>Сервисы.</returns>
        public static IServiceCollection AddWeb(this IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
            {
                options.CacheProfiles.Add("Caching", new CacheProfile()
                {
                    Duration = 300
                });

                options.CacheProfiles.Add("NotCaching", new CacheProfile()
                {
                    Location = ResponseCacheLocation.None,
                    NoStore = true
                });
            })
            .AddDataAnnotationsLocalization()
            .AddViewLocalization()
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IApplicationContext>());

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("ru")
                };

                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.AddScoped<IApplicationContext, ApplicationContext>();

            services.AddHealthChecks()
                    .AddDbContextCheck<ApplicationContext>();

            services.AddSignalR();
            services.AddFeatureManagement();

            return services;
        }
    }
}
