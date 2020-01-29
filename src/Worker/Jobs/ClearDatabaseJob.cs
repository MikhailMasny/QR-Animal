using Coravel.Invocable;
using Masny.QRAnimal.Application.CQRS.Queries.GetAnimal;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Worker.Jobs
{
    /// <summary>
    /// Задача для очистки базы данных.
    /// </summary>
    public class ClearDatabaseJob : IInvocable
    {
        private const string _doWorkMessage = "Marked data for deletion cleared.";

        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="logger">Логгер.</param>
        /// <param name="mediator">Медиатор.</param>
        public ClearDatabaseJob(ILogger<ClearDatabaseJob> logger,
                                IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Invoke()
        {
            // TODO: разработать специальный класс для очистки.
            await _mediator.Send(new GetAnimalsQuery());

            _logger.LogInformation(_doWorkMessage);
        }
    }
}
