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
        /// Получить имя пользователя.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <returns>Имя пользователя.</returns>
        Task<string> GetUserNameAsync(string userId);

        /// <summary>
        /// Создать пользователя.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="password">Пароль.</param>
        /// <returns>Результат операции и Id пользователя.</returns>
        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        /// <summary>
        /// Удалить пользователя.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <returns>Результат операции</returns>
        Task<Result> DeleteUserAsync(string userId);
    }
}
