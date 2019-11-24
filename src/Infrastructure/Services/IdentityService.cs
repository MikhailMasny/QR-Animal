using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Application.Models;
using Masny.QRAnimal.Infrastructure.Extensions;
using Masny.QRAnimal.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Infrastructure.Services
{
    /// <summary>
    /// Cервис для работы с идентификацией пользователя.
    /// </summary>
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="userManager">Управление пользователем.</param>
        /// <param name="signInManager">Управление состоянием входа пользователя.</param>
        public IdentityService(UserManager<AppUser> userManager,
                               SignInManager<AppUser> signInManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        /// <inheritdoc />
        public async Task<string> GetUserNameByIdAsync(string userId)
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
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

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

        /// <inheritdoc />
        public async Task<Result> LoginUserAsync(string email, string password, bool isPersistent, bool lockoutOnFailure)
        {
            // UNDONE: Реализовать корректное поведение RememberMe
            var result = await _signInManager.PasswordSignInAsync(email, password, isPersistent, lockoutOnFailure);

            return result.ToApplicationResult();
        }

        /// <inheritdoc />
        public async Task SignInUserAsync(string email, string userName)
        {
            var user = new AppUser
            {
                Email = email,
                UserName = userName
            };

            await _signInManager.SignInAsync(user, false);
        }

        /// <inheritdoc />
        public async Task SignOutUserAsync()
        {
            // Удаление аутентификационных куков.
            await _signInManager.SignOutAsync();
        }

        private async Task<Result> DeleteUserAsync(AppUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }
    }
}
