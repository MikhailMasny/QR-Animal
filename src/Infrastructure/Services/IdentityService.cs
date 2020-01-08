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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="userManager">Управление пользователем.</param>
        /// <param name="signInManager">Управление состоянием входа пользователя.</param>
        public IdentityService(UserManager<ApplicationUser> userManager,
                               SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        /// <inheritdoc />
        public async Task<string> GetUserIdByNameAsync(string userName)
        {
            var user = await _userManager.Users.FirstAsync(u => u.UserName == userName);

            return user.Id;
        }

        /// <inheritdoc />
        public async Task<(Result result, string userId, string code)> CreateUserAsync(string email, string userName, string password)
        {
            var user = new ApplicationUser
            {
                Email = email,
                UserName = userName
            };

            var isExist = await _userManager.FindByEmailAsync(email);
            IdentityResult result;

            if (isExist == null)
            {
                result = await _userManager.CreateAsync(user, password);
                var code = string.Empty;

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                }

                return (result.ToApplicationResult(), user.Id, code);
            }

            return (null, null, null);
        }

        /// <inheritdoc />
        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return Result.Success();
            }

            return await DeleteUserAsync(user);
        }

        /// <inheritdoc />
        public async Task<Result> LoginUserAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);

            return result.ToApplicationResult();
        }

        /// <inheritdoc />
        public async Task LogoutUserAsync()
        {
            // Удаление аутентификационных куков.
            await _signInManager.SignOutAsync();
        }

        /// <inheritdoc />
        public async Task<(bool result, string message)> EmailConfirmCheckerAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return (false, "Пользователь не найден.");
            }

            var isConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            if (isConfirmed)
            {
                return (true, null);
            }

            return (false, "Вы не подтвердили свой email.");
        }

        /// <inheritdoc />
        public async Task<(Result result, string message)> ConfirmEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return (null, "Пользователь не существует");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                await SignInUserAsync(user.Email, user.UserName);

                return (result.ToApplicationResult(), "Успешно");
            }

            return (result.ToApplicationResult(), "Проблемы с токеном");
        }

        /// <inheritdoc />
        public async Task<(bool result, string userId, string userName, string code)> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return (false, null, null, null);
            }

            var result = await _userManager.IsEmailConfirmedAsync(user);

            if (!result)
            {
                return (false, null, null, null);
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            return (true, user.Id, user.UserName, code);
        }

        /// <inheritdoc />
        public async Task<Result> ResetPassword(string userName, string password, string code)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return null;
            }

            var result = await _userManager.ResetPasswordAsync(user, code, password);

            return result.ToApplicationResult();
        }

        /// <summary>
        /// Удалить пользователя.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <returns>Результат операции.</returns>
        private async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }

        /// <summary>
        /// Вход в систему.
        /// </summary>
        /// <param name="email">Электронная почта.</param>
        /// <param name="userName">Имя пользователя.</param>
        private async Task SignInUserAsync(string email, string userName)
        {
            var user = new ApplicationUser
            {
                Email = email,
                UserName = userName
            };

            await _signInManager.SignInAsync(user, false);
        }
    }
}
