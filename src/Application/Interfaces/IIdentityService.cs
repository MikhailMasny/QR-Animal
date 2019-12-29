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
        /// <param name="email">Почта.</param>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="password">Пароль.</param>
        /// <returns>Результат операции, Id пользователя и confirmation Token.</returns>
        Task<(Result result, string userId, string code)> CreateUserAsync(string email, string userName, string password);

        /// <summary>
        /// Удалить пользователя.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <returns>Результат операции.</returns>
        Task<Result> DeleteUserAsync(string userId);

        /// <summary>
        /// Авторизировать пользователя.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="password">Пароль.</param>
        /// <param name="isPersistent">Запомнить меня.</param>
        /// <param name="lockoutOnFailure">Блокировка.</param>
        /// <returns>Результат операции.</returns>
        Task<Result> LoginUserAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);

        /// <summary>
        /// Выход из системы.
        /// </summary>
        Task LogoutUserAsync();

        /// <summary>
        /// Проверить подвержденность email.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <returns>Результат и сообщение.</returns>
        Task<(bool result, string message)> EmailConfirmCheckerAsync(string userName);

        /// <summary>
        /// Подтвердить email.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <param name="code">Confirmation Token.</param>
        /// <returns>Результат операции и сообщение.</returns>
        Task<(Result result, string message)> ConfirmEmail(string userId, string code);

        /// <summary>
        /// Восстановить пароль.
        /// </summary>
        /// <param name="email">Почта.</param>
        /// <returns>Результат операции, Id пользователя, имя пользователя и confirmation Token.</returns>
        Task<(bool result, string userId, string userName, string code)> ForgotPassword(string email);

        /// <summary>
        /// Сбросить пароль.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="password">Пароль.</param>
        /// <param name="code">Confirmation Token.</param>
        /// <returns>Результат операции.</returns>
        Task<Result> ResetPassword(string userName, string password, string code);
    }
}
