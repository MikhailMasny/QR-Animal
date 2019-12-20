﻿using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = Masny.QRAnimal.Application.Exceptions.ValidationException;

namespace Masny.QRAnimal.Application.Application.Behaviours
{
    /// <summary>
    /// Класс для обработки запроса FluentValidation'ом.
    /// </summary>
    /// <typeparam name="TRequest">Общий запрос.</typeparam>
    /// <typeparam name="TResponse">Общий ответ.</typeparam>
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="validators">Обработчики от FluentValidation.</param>
        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <summary>
        /// Обработчик.
        /// </summary>
        /// <param name="request">Общий запрос.</param>
        /// <param name="cancellationToken">Токен для асинхронности.</param>
        /// <param name="next">Следующее действие.</param>
        /// <returns></returns>
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext(request);

            var failures = _validators.Select(v => v.Validate(context))
                                      .SelectMany(result => result.Errors)
                                      .Where(f => f != null)
                                      .ToList();

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }

            return next();
        }
    }
}