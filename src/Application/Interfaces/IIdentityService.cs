using Masny.QRAnimal.Application.Models;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Application.Interfaces
{
    /// <summary>
    /// Интерфейс для сервиса работы с идентификацией пользователя.
    /// </summary>
    public interface IIdentityService
    {
        /// <summary>
        /// Получить Id пользователя.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <returns>Id пользователя.</returns>
        Task<string> GetUserIdByNameAsync(string userName);

        /// <summary>
        /// Создать пользователя.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="password">Пароль.</param>
        /// <returns>Результат операции и Id пользователя.</returns>
        Task<(Result Result, string UserId)> CreateUserAsync(string email, string userName, string password);

        /// <summary>
        /// Удалить пользователя.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <returns>Результат операции</returns>
        Task<Result> DeleteUserAsync(string userId);

        /// <summary>
        /// Авторизировать пользователя.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="password">Пароль.</param>
        /// <param name="isPersistent">Запомнить меня.</param>
        /// <param name="lockoutOnFailure">Блокировка.</param>
        Task<Result> LoginUserAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);

        /// <summary>
        /// Вход в систему.
        /// </summary>
        /// <param name="email">Электронная почта.</param>
        /// <param name="userName">Имя пользователя.</param>
        Task SignInUserAsync(string email, string userName);

        /// <summary>
        /// Выход из системы.
        /// </summary>
        Task LogoutUserAsync();
    }
}
