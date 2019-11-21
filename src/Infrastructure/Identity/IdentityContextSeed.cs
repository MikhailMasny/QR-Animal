using Microsoft.AspNetCore.Identity;
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
        /// <param name="userManager">Менеджер.</param>
        public static async Task SeedAsync(UserManager<AppUser> userManager)
        {
            var defaultUser = new AppUser
            {
                UserName = "demouser@microsoft.com",
                Email = "demouser@microsoft.com"
            };

            await userManager.CreateAsync(defaultUser, "Pass@word1");
        }
    }
}
