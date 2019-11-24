using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Infrastructure.Identity
{
    /// <summary>
    /// Класс для заполнения контекста.
    /// </summary>
    public class IdentityContextSeed
    {
        /// <summary>
        /// Заполнение базы данных начальными данными.
        /// </summary>
        /// <param name="identityContext">Контекст Identity.</param>
        /// <param name="userManager">Управление пользователем.</param>
        /// <param name="roleManager">Управление ролями.</param>
        public static async Task SeedAsync(IdentityContext identityContext,
                                           UserManager<ApplicationUser> userManager,
                                           RoleManager<IdentityRole> roleManager)
        {
            identityContext = identityContext ?? throw new ArgumentNullException(nameof(identityContext));
            userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));

            // Проверка на пустоту данных в базе данных.
            if (identityContext.Users.Any() || identityContext.Roles.Any() || identityContext.UserRoles.Any())
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
