using Masny.QRAnimal.Application.CQRS.Queries.GetAnimal;
using Masny.QRAnimal.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Web.Controllers
{
    /// <summary>
    /// Контроллер управления профилем пользователя.
    /// </summary>
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="logger">Логгер.</param>
        /// <param name="mediator">Медиатор.</param>
        /// <param name="identityService">Cервис работы с идентификацией пользователя.</param>
        public ProfileController(ILogger<ProfileController> logger,
                                 IMediator mediator,
                                 IIdentityService identityService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        /// <summary>
        /// Страница для отображения всех животных пользователя.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var userId = await _identityService.GetUserIdByNameAsync(User.Identity.Name);
            var animals = await _mediator.Send(new GetAnimalsQuery());

            var userAnimals = animals.Where(a => a.UserId == userId);

            _logger.LogInformation($"{userAnimals.Count()} animals showed for user {User.Identity.Name}.");

            return View(userAnimals);
        }
    }
}
