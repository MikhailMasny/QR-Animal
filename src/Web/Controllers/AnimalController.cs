using Masny.QRAnimal.Application.CQRS.Commands.CreateAnimal;
using Masny.QRAnimal.Application.CQRS.Commands.CreateQRCode;
using Masny.QRAnimal.Application.CQRS.Commands.DeleteAnimal;
using Masny.QRAnimal.Application.CQRS.Commands.UpdateAnimal;
using Masny.QRAnimal.Application.CQRS.Queries.GetAnimal;
using Masny.QRAnimal.Application.CQRS.Queries.GetQRCode;
using Masny.QRAnimal.Application.DTO;
using Masny.QRAnimal.Application.Exceptions;
using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Application.Models;
using Masny.QRAnimal.Web.Extensions;
using Masny.QRAnimal.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
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
        private readonly IQRCodeGeneratorService _QRCodeGeneratorService;
        private readonly IOptions<AppSettings> _options;

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="logger">Логгер.</param>
        /// <param name="mediator">Медиатор.</param>
        /// <param name="identityService">Cервис работы с идентификацией пользователя.</param>
        /// <param name="QRCodeGeneratorService">Сервис для формирования QR кода.</param>
        /// <param name="options">Настройки приложения.</param>
        public AnimalController(ILogger<AnimalController> logger,
                                IMediator mediator,
                                IIdentityService identityService,
                                IQRCodeGeneratorService QRCodeGeneratorService,
                                IOptions<AppSettings> options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _QRCodeGeneratorService = QRCodeGeneratorService ?? throw new ArgumentNullException(nameof(QRCodeGeneratorService));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>
        /// Страница для добавления нового животного.
        /// </summary>
        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new AnimalViewModel()
            {
                BirthDate = DateTime.Now,
                GenderSelectList = new SelectList(new List<string>
                {
                    "None",
                    "Male",
                    "Female"
                },
                "Gender")
            };

            return View(viewModel);
        }

        /// <summary>
        /// Добавление нового животного.
        /// </summary>
        /// <returns>Определенное представление.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(AnimalViewModel model)
        {
            model = model ?? throw new ArgumentNullException(nameof(model));

            if (ModelState.IsValid)
            {
                var userId = await _identityService.GetUserIdByNameAsync(User.Identity.Name);
                model.UserId = userId;

                // Создание команды для добавления нового животного
                var animalDTO = new AnimalDTO
                {
                    UserId = userId,
                    Kind = model.Kind,
                    Breed = model.Breed,
                    Gender = model.Gender.ToLocalType(),
                    Passport = model.Passport,
                    BirthDate = model.BirthDate,
                    Nickname = model.Nickname,
                    Features = model.Features,
                    IsPublic = model.IsPublic
                };

                var animalCommand = new CreateAnimalCommand
                {
                    Model = animalDTO
                };

                int id;

                try
                {
                    id = await _mediator.Send(animalCommand);
                }
                catch (RequestValidationException failures)
                {
                    foreach (var error in failures.Failures)
                    {
                        ModelState.AddModelError(string.Empty, error.Value[0]);
                    }

                    return View(model);
                }

                // Создание команды для добавления QR кода для животного
                var qrCodeDTO = new QRCodeDTO
                {
                    Code = _options.Value.QRGeneratorCode + id,
                    Created = DateTime.Now,
                    AnimalId = id
                };

                var qrCommand = new CreateQRCodeCommand
                {
                    Model = qrCodeDTO
                };

                await _mediator.Send(qrCommand);

                _logger.LogInformation($"{User.Identity.Name} successfully created animal with id: {id}.");

                return RedirectToAction("Index", "Profile");
            }

            return View(model);
        }

        /// <summary>
        /// Страница для обновления данных животного.
        /// </summary>
        /// <returns>Определенное представление.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = await _identityService.GetUserIdByNameAsync(User.Identity.Name);

            var animalCommand = new GetAnimalQuery
            {
                Id = id,
                UserId = userId,
                AnotherUser = false
            };

            var model = await _mediator.Send(animalCommand);

            if (model == null)
            {
                return RedirectToAction("Index", "Profile");
            }

            var animalViewModel = new AnimalViewModel
            {
                Id = model.Id,
                UserId = model.UserId,
                Nickname = model.Nickname,
                Passport = model.Passport,
                Kind = model.Kind,
                Breed = model.Breed,
                Features = model.Features,
                IsPublic = model.IsPublic
            };

            return View(animalViewModel);
        }

        /// <summary>
        /// Обновление животного.
        /// </summary>
        /// <returns>Определенное представление.</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(AnimalViewModel model)
        {
            model = model ?? throw new ArgumentNullException(nameof(model));

            if (ModelState.IsValid)
            {
                var userId = await _identityService.GetUserIdByNameAsync(User.Identity.Name);

                // Создание команды для обновления животного
                var animalDTO = new AnimalDTO
                {
                    Id = model.Id,
                    UserId = userId,
                    Nickname = model.Nickname,
                    Passport = model.Passport,
                    Kind = model.Kind,
                    Breed = model.Breed,
                    Features = model.Features,
                    IsPublic = model.IsPublic
                };

                var animalCommand = new UpdateAnimalCommand
                {
                    Model = animalDTO
                };

                try
                {
                    await _mediator.Send(animalCommand);
                }
                catch (RequestValidationException failures)
                {
                    foreach (var error in failures.Failures)
                    {
                        ModelState.AddModelError(string.Empty, error.Value[0]);
                    }

                    return View(model);
                }

                _logger.LogInformation($"{User.Identity.Name} successfully edited animal with id: {model.Id}.");

                return RedirectToAction("Index", "Profile");
            }

            return View(model);
        }

        /// <summary>
        /// Удаление выбранного животного (пометить для удаления).
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Определенное представление.</returns>
        public async Task<IActionResult> Delete(int id)
        {
            var userId = await _identityService.GetUserIdByNameAsync(User.Identity.Name);

            var animalCommand = new MarkAsDeletedAnimalCommand
            {
                Id = id,
                UserId = userId
            };

            try
            {
                await _mediator.Send(animalCommand);
            }
            catch
            {
                _logger.LogInformation($"{User.Identity.Name} can't delete animal with id: {id}.");
            }

            _logger.LogInformation($"{User.Identity.Name} successfully deleted animal with id: {id}.");

            return RedirectToAction("Index", "Profile");
        }

        /// <summary>
        /// Страница для получения информации о выбранном животном.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Определенное представление.</returns>
        public async Task<IActionResult> Info(int id)
        {
            var userId = await _identityService.GetUserIdByNameAsync(User.Identity.Name);

            var animalQuery = new GetAnimalQuery
            {
                Id = id,
                UserId = userId,
                AnotherUser = false
            };

            var userAnimal = await _mediator.Send(animalQuery);

            if (userAnimal == null)
            {
                return RedirectToAction("Index", "Profile");
            }

            var qrQuery = new GetQRCodeQuery
            {
                AnimalId = userAnimal.Id
            };

            QRCodeDTO qrCodeText = await _mediator.Send(qrQuery);

            var code = _QRCodeGeneratorService.CreateQRCode(qrCodeText.Code);

            var animalViewModel = new AnimalViewModel
            {
                Id = userAnimal.Id,
                UserId = userAnimal.UserId,
                Kind = userAnimal.Kind,
                Breed = userAnimal.Breed,
                Gender = userAnimal.Gender.ToLocalString(),
                Passport = userAnimal.Passport,
                BirthDate = userAnimal.BirthDate,
                Nickname = userAnimal.Nickname,
                Features = userAnimal.Features,
                IsPublic = userAnimal.IsPublic,
                Code = code
            };

            _logger.LogInformation($"For {User.Identity.Name} successfully showed animal with id: {userAnimal.Id}.");

            return View(animalViewModel);
        }

        /// <summary>
        /// Страница для получения информации о выбранном животном (для другого пользователя, Public).
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Представление публичной страницы с информацией о животном.</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Public(int id)
        {
            var animalQuery = new GetAnimalQuery
            {
                Id = id,
                AnotherUser = true
            };

            var userAnimal = await _mediator.Send(animalQuery);

            if (userAnimal == null || !userAnimal.IsPublic)
            {
                return View("PermissionDenied");
            }

            var qrQuery = new GetQRCodeQuery
            {
                AnimalId = userAnimal.Id
            };

            QRCodeDTO qrCodeText = await _mediator.Send(qrQuery);

            var code = _QRCodeGeneratorService.CreateQRCode(qrCodeText.Code);

            var animalViewModel = new AnimalViewModel
            {
                Id = userAnimal.Id,
                UserId = userAnimal.UserId,
                Kind = userAnimal.Kind,
                Breed = userAnimal.Breed,
                Gender = userAnimal.Gender.ToLocalString(),
                Passport = userAnimal.Passport,
                BirthDate = userAnimal.BirthDate,
                Nickname = userAnimal.Nickname,
                Features = userAnimal.Features,
                IsPublic = userAnimal.IsPublic,
                Code = code
            };

            _logger.LogInformation($"Animal with id: {userAnimal.Id}, successfully showed on public request.");

            return View(animalViewModel);
        }
    }
}
