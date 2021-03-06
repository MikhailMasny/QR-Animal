﻿using Masny.QRAnimal.Application.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace Masny.QRAnimal.Infrastructure.Extensions
{
    /// <summary>
    /// Метод расширения для IdentityResult.
    /// </summary>
    public static class IdentityResultExtension
    {
        /// <summary>
        /// Перевод для приложения (Identity).
        /// </summary>
        /// <param name="result">Результат Identity.</param>
        /// <returns>Результат.</returns>
        public static Result ToApplicationResult(this IdentityResult result)
        {
            result = result ?? throw new ArgumentNullException(nameof(result));

            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description));
        }

        /// <summary>
        /// Перевод для приложения (SignIn).
        /// </summary>
        /// <param name="result">Результат SignIn.</param>
        /// <returns>Результат.</returns>
        public static Result ToApplicationResult(this SignInResult result)
        {
            result = result ?? throw new ArgumentNullException(nameof(result));

            return result.Succeeded
                ? Result.Success()
                : Result.Failure(Array.Empty<string>());
        }
    }
}
