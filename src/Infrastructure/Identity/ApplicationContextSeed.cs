using Masny.QRAnimal.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
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
        /// Заполнить базу данных начальными данными.
        /// </summary>
        /// <param name="applicationContext">Контекст приложения.</param>
        /// <param name="userManager">Управление пользователем.</param>
        /// <param name="roleManager">Управление ролями.</param>
        public static async Task IdentitySeedAsync(ApplicationContext applicationContext,
                                                   UserManager<ApplicationUser> userManager,
                                                   RoleManager<IdentityRole> roleManager)
        {
            applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
            userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));

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
