using Masny.QRAnimal.Application.CQRS.Commands.CreateAnimal;
using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Web.Controllers
{
    /// <summary>
    /// Контроллер управления животными пользователя.
    /// </summary>
    [Authorize]
    public class AnimalController : Controller
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
        public AnimalController(ILogger<AnimalController> logger,
                                IMediator mediator,
                                IIdentityService identityService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        /// <summary>
        /// Страница для добавления нового животного.
        /// </summary>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Добавить нового животного.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(AnimalViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = await _identityService.GetUserIdByNameAsync(User.Identity.Name);
                model.UserId = userId;

                CreateAnimalCommand command = new CreateAnimalCommand
                {
                    Model = model
                };

                await _mediator.Send(command);

                return RedirectToAction("Index", "Profile");
            }

            return View(model);
        }
    }
}
