using System;
using System.Collections.Generic;
using System.Linq;

namespace Masny.QRAnimal.Application.Models
{
    /// <summary>
    /// Класс результата.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Внутренний конструктор.
        /// </summary>
        /// <param name="succeeded">Результат успеха.</param>
        /// <param name="errors">Список ошибок.</param>
        internal Result(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        /// <summary>
        /// Результат успеха.
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// Массив ошибок.
        /// </summary>
        public IEnumerable<string> Errors { get; set; }

        /// <summary>
        /// Результат успешно.
        /// </summary>
        /// <returns>Результат.</returns>
        public static Result Success()
        {
            return new Result(true, Array.Empty<string>());
        }

        /// <summary>
        /// Результат с ошибкой.
        /// </summary>
        /// <param name="errors">Список ошибок.</param>
        /// <returns>Результат с ошибками.</returns>
        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }
    }
}
