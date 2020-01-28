using Masny.QRAnimal.Application.CQRS.Queries.GetAnimal;
using Masny.QRAnimal.Application.DTO;
using Masny.QRAnimal.Web.Extensions;
using Masny.QRAnimal.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Web.Controllers.WebAPI
{
    /// <summary>
    /// Контроллер управления животными пользователя через API.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [FeatureGate("FeatureWebAPI")]
    public class AnimalsController : Controller
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="logger">Логгер.</param>
        /// <param name="mediator">Медиатор.</param>
        /// <param name="memoryCache">Кэш.</param>
        public AnimalsController(ILogger<AnimalController> logger,
                                IMediator mediator,
                                IMemoryCache memoryCache)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        /// <summary>
        /// Получить список всех животных (публичных).
        /// </summary>
        /// <returns>Список животных (json).</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllPublicAnimalsAsync()
        {
            var publicAnimals =
                (await GetAnimals())
                .Where(a => a.IsPublic)
                .ToList();

            if (!publicAnimals.Any())
            {
                return NoContent();
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
                    Gender = animal.Gender.ToString(),
                    Passport = animal.Passport,
                    BirthDate = animal.BirthDate,
                    Nickname = animal.Nickname,
                    Features = animal.Features
                });
            });

            _logger.LogInformation($"Successfully sent {publicAnimals.Count} public animals.");

            return Json(animalModels);
        }

        /// <summary>
        /// Получить животное по id (публичных).
        /// </summary>
        /// <returns>Животное (json).</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublicAnimalAsync([FromRoute] int id)
        {
            if (!_memoryCache.TryGetValue(id, out AnimalDTO publicAnimal))
            {
                publicAnimal =
                    (await GetAnimals())
                    .SingleOrDefault(a => a.IsPublic && a.Id == id);

                if (publicAnimal != null)
                {
                    _memoryCache.Set(publicAnimal.Id, publicAnimal, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }

            if (publicAnimal == null)
            {
                return NoContent();
            }

            var animalModel = new AnimalModel
            {
                Id = publicAnimal.Id,
                UserId = publicAnimal.UserId,
                Kind = publicAnimal.Kind,
                Breed = publicAnimal.Breed,
                Gender = publicAnimal.Gender.ToString(),
                Passport = publicAnimal.Passport,
                BirthDate = publicAnimal.BirthDate,
                Nickname = publicAnimal.Nickname,
                Features = publicAnimal.Features
            };

            _logger.LogInformation($"Successfully sent public animal with Id: {animalModel.Id}.");

            return Json(animalModel);
        }

        [NonAction]
        private async Task<IEnumerable<AnimalDTO>> GetAnimals()
        {
            return await _mediator.Send(new GetAnimalsQuery());
        }
    }
}
