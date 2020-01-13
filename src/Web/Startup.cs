using FluentValidation.AspNetCore;
using Masny.QRAnimal.Application;
using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Application.Models;
using Masny.QRAnimal.Infrastructure;
using Masny.QRAnimal.Infrastructure.Extensions;
using Masny.QRAnimal.Infrastructure.Persistence;
using Masny.QRAnimal.Web.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using System;
using System.Globalization;

namespace Masny.QRAnimal.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddFeatureManagement();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddControllersWithViews(options =>
            {
                options.CacheProfiles.Add("Caching", new CacheProfile() 
                { 
                    Duration = 300 
                });

                options.CacheProfiles.Add("NotCaching", new CacheProfile() 
                { 
                    Location = ResponseCacheLocation.None, NoStore = true 
                });
            })
                .AddDataAnnotationsLocalization()
                .AddViewLocalization()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IApplicationContext>());

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

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSignalR();

            services.AddInfrastructure();
            services.AddApplication();

            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);

            // Добавлен Identity контекст.
            var isDockerSupport = appSettingSection.Get<AppSettings>().IsDockerSupport;
            string connectionString = Configuration.GetConnectionString(isDockerSupport.ToDbConnectionString());
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IApplicationContext, ApplicationContext>();
            services.AddHealthChecks().AddDbContextCheck<ApplicationContext>();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app = app ?? throw new ArgumentNullException(nameof(app));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = crh =>
                {
                    crh.Context.Response.Headers.Add("Cache-Control", "public, max-age=600");
                }
            });

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHealthChecks("/health").RequireAuthorization();
                endpoints.MapHub<ChatHub>("/chatHub");
            });
        }
    }
}
