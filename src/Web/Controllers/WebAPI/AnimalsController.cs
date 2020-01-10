using Masny.QRAnimal.Application.CQRS.Queries.GetAnimal;
using Masny.QRAnimal.Web.Extensions;
using Masny.QRAnimal.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Web.Controllers.WebAPI
{
    /// <summary>
    /// Контроллер управления животными пользователя через API.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : Controller
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="logger">Логгер.</param>
        /// <param name="mediator">Медиатор.</param>
        public AnimalsController(ILogger<AnimalController> logger,
                                IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Получить список всех животных (публичных).
        /// </summary>
        /// <returns>Список животных (json).</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllPublicAnimalsAsync()
        {
            var animalsQuery = new GetAnimalsQuery();

            var publicAnimals =
                (await _mediator.Send(animalsQuery))
                .Where(a => a.IsPublic)
                .ToList();

            if (!publicAnimals.Any())
            {
                return Ok();
            }

            var animalModels = new List<AnimalModel>();

            publicAnimals.ForEach(animal =>
            {
                animalModels.Add(new AnimalModel
                {
                    Id = animal.Id,
                    UserId = animal.UserId,
                    Kind = animal.Kind,
                    Breed = animal.Breed,
                    Gender = animal.Gender.ToLocalString(),
                    Passport = animal.Passport,
                    BirthDate = animal.BirthDate,
                    Nickname = animal.Nickname,
                    Features = animal.Features
                });
            });

            _logger.LogInformation($"Successfully sent {publicAnimals.Count} public animals.");

            return Json(animalModels);
        }
    }
}
