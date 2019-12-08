using Masny.QRAnimal.Application.CQRS.Commands.CreateAnimal;
using Masny.QRAnimal.Application.CQRS.Commands.CreateQRCode;
using Masny.QRAnimal.Application.CQRS.Commands.DeleteAnimal;
using Masny.QRAnimal.Application.CQRS.Commands.UpdateAnimal;
using Masny.QRAnimal.Application.CQRS.Queries.GetAnimal;
using Masny.QRAnimal.Application.Exceptions;
using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

// UNDONE: добавить DTO модели
// UNDONE: добавить задачу worker для удаления определенных данных

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

                // Создание команды для добавления нового животного
                var animalCommand = new CreateAnimalCommand
                {
                    Model = model
                };

                var id = await _mediator.Send(animalCommand);

                // Создание команды для добавления QR кода для животного
                var qrCode = new QRCodeViewModel
                {
                    Code = Guid.NewGuid().ToString(),
                    Created = DateTime.Now,
                    AnimalId = id
                };

                var qrCommand = new CreateQRCodeCommand
                {
                    Model = qrCode
                };

                await _mediator.Send(qrCommand);

                return RedirectToAction("Index", "Profile");
            }

            return View(model);
        }

        /// <summary>
        /// Страница для обновления данных животного.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var animalCommand = new GetAnimalQuery
            {
                Id = id
            };

            var model = await _mediator.Send(animalCommand);

            if (model == null)
            {
                return RedirectToAction("Index", "Profile");
            }

            return View(model);
        }

        /// <summary>
        /// Обновить животное.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Edit(AnimalViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Создание команды для добавления нового животного
                var animalCommand = new UpdateAnimalCommand
                {
                    Model = model
                };

                try
                {
                    await _mediator.Send(animalCommand);
                }
                catch (NotFoundException ex)
                {
                    // UNDONE: Logger
                }

                return RedirectToAction("Index", "Profile");
            }

            return View(model);
        }

        /// <summary>
        /// Удалить выбранное животное.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Представление главной страницы.</returns>
        public async Task<IActionResult> Delete(int id)
        {
            // Создание команды для добавления нового животного
            var animalCommand = new DeleteAnimalCommand
            {
                Id = id
            };

            try
            {
                await _mediator.Send(animalCommand);
            }
            catch (NotFoundException ex)
            {
                // UNDONE: Logger
            }

            return RedirectToAction("Index", "Profile");
        }

        /// <summary>
        /// Получить информацию о выбранном животном.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Представление страницы с информацией о животном.</returns>
        public async Task<IActionResult> Info(int id)
        {
            var animalQuery = new GetAnimalQuery
            {
                Id = id
            };

            var userAnimal = await _mediator.Send(animalQuery);

            _logger.LogInformation($"Animal {userAnimal.Nickname} showed for user {User.Identity.Name}.");

            return View(userAnimal);
        }
    }
}
