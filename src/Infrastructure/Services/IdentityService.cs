using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Application.Models;
using Masny.QRAnimal.Infrastructure.Extensions;
using Masny.QRAnimal.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Infrastructure.Services
{
    /// <summary>
    /// Cервис работы с идентификацией пользователя.
    /// </summary>
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="userManager">Управление пользователем.</param>
        public IdentityService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        /// <inheritdoc />
        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        /// <inheritdoc />
        public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
        {
            var user = new AppUser
            {
                UserName = userName,
                Email = userName,
            };

            var result = await _userManager.CreateAsync(user, password);

            return (result.ToApplicationResult(), user.Id);
        }

        /// <inheritdoc />
        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return await DeleteUserAsync(user);
            }

            return Result.Success();
        }

        private async Task<Result> DeleteUserAsync(AppUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }
    }
}
