using Masny.QRAnimal.Application.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Masny.QRAnimal.Infrastructure.Extensions
{
    /// <summary>
    /// Метод расширения для IdentityResult.
    /// </summary>
    public static class IdentityResultExtension
    {
        /// <summary>
        /// Перевод для приложения.
        /// </summary>
        /// <param name="result">Результат Identity.</param>
        /// <returns>Результат.</returns>
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description));
        }
    }
}
