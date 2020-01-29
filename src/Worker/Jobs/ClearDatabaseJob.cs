using Coravel.Invocable;
using Masny.QRAnimal.Application.CQRS.Commands.DeleteAnimal;
using Masny.QRAnimal.Application.CQRS.Commands.DeleteQRCode;
using Masny.QRAnimal.Application.CQRS.Queries.GetAnimal;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Worker.Jobs
{
    /// <summary>
    /// Задача для очистки базы данных.
    /// </summary>
    public class ClearDatabaseJob : IInvocable
    {
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
            var animals = await _mediator.Send(new GetDeletedAnimalsQuery());

            if (animals.Any())
            {
                foreach (var a in animals)
                {
                    var animalId = a.Id;

                    var qrDeleteCommand = new DeleteQRCodeCommand
                    {
                        AnimalId = animalId
                    };

                    await _mediator.Send(qrDeleteCommand);

                    var animalDeleteCommand = new DeleteAnimalCommand
                    {
                        Id = animalId
                    };

                    await _mediator.Send(animalDeleteCommand);
                }
            }

            _logger.LogInformation($"The clear database job completed successfully. {animals.Count()} marked data has been deleted.");
        }
    }
}
