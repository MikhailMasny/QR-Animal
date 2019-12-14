using Masny.QRAnimal.Application.Models;
using Masny.QRAnimal.Infrastructure.Extensions;
using Masny.QRAnimal.Infrastructure.Persistence;
using Masny.QRAnimal.Infrastructure.Services;
using Masny.QRAnimal.Worker.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Masny.QRAnimal.Worker
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
            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);

            // UNDONE: Рефакторинг

            // Добавлен Identity контекст.
            var isDockerSupport = appSettingSection.Get<AppSettings>().IsDockerSupport;
            string connectionString = Configuration.GetConnectionString(isDockerSupport.ToDbConnectionString());
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

            services.AddHostedService<ConsumeScopedServiceHostedService>();
            services.AddScoped<IScopedProcessingService, ScopedProcessingService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
