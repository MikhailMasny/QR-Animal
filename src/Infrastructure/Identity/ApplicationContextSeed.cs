using Masny.QRAnimal.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Infrastructure.Identity
{
    /// <summary>
    /// Класс для заполнения контекста.
    /// </summary>
    public static class ApplicationContextSeed
    {
        /// <summary>
        /// Заполнение базы данных начальными данными.
        /// </summary>
        /// <param name="serviceProvider">Провайдер сервисов.</param>
        public static async Task IdentitySeedAsync(IServiceProvider serviceProvider)
        {
            var contextOptions = serviceProvider.GetRequiredService<DbContextOptions<ApplicationContext>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            using var applicationContext = new ApplicationContext(contextOptions);

            // Проверка на пустоту данных в базе данных.
            if (applicationContext.Users.Any() || applicationContext.Roles.Any() || applicationContext.UserRoles.Any())
            {
                return;
            }

            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (await roleManager.FindByNameAsync("User") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            var defaultAdmin = new ApplicationUser
            {
                UserName = "demouser",
                Email = "demouser@microsoft.com"
            };

            if (await userManager.FindByNameAsync(defaultAdmin.UserName) == null)
            {
                var result = await userManager.CreateAsync(defaultAdmin, "Pass@word1");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(defaultAdmin, "Admin");
                }
            }
        }
    }
}
