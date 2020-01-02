using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Masny.QRAnimal.Application.Exceptions
{
    /// <summary>
    /// Ошибка валидации.
    /// </summary>
    public class RequestValidationException : Exception
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public RequestValidationException()
            : base("One or more validation failures have occurred.")
        {
            Failures = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="failures">Ошибки.</param>
        public RequestValidationException(List<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }

        /// <summary>
        /// Словарь ошибок.
        /// </summary>
        public IDictionary<string, string[]> Failures { get; }
    }
}
