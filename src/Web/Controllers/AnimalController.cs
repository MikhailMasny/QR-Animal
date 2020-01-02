﻿using Masny.QRAnimal.Application.CQRS.Commands.CreateAnimal;
using Masny.QRAnimal.Application.CQRS.Commands.CreateQRCode;
using Masny.QRAnimal.Application.CQRS.Commands.DeleteAnimal;
using Masny.QRAnimal.Application.CQRS.Commands.UpdateAnimal;
using Masny.QRAnimal.Application.CQRS.Queries.GetAnimal;
using Masny.QRAnimal.Application.CQRS.Queries.GetQRCode;
using Masny.QRAnimal.Application.DTO;
using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Web.Extensions;
using Masny.QRAnimal.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

// UNDONE: добавить DTO модели
// UNDONE: добавить задачу worker для удаления определенных данных
// UNDONE: добавить возможность при создании животного делать его исключительно приватным

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
                catch
                {
                    // UNDONE: Добавить обработчик.
                    return View(model);
                }

                // Создание команды для добавления QR кода для животного
                var qrCodeDTO = new QRCodeDTO
                {
                    Code = $"http://localhost:85/Animal/Public/{id}",
                    Created = DateTime.Now,
                    AnimalId = id
                };

                var qrCommand = new CreateQRCodeCommand
                {
                    Model = qrCodeDTO
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
        /// Обновить животное.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Edit(AnimalViewModel model)
        {
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
                catch
                {
                    // UNDONE: Добавить обработчик.
                    return View(model);
                }

                return RedirectToAction("Index", "Profile");
            }

            return View(model);
        }

        /// <summary>
        /// Удалить выбранное животное (пометить для удаления).
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Представление главной страницы.</returns>
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
            //(NotFoundException ex)
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
            var userId = await _identityService.GetUserIdByNameAsync(User.Identity.Name);

            var animalQuery = new GetAnimalQuery
            {
                Id = id,
                UserId = userId,
                AnotherUser = false
            };

            var userAnimal = await _mediator.Send(animalQuery);

            //_logger.LogInformation($"Animal {userAnimal.Nickname} showed for user {User.Identity.Name}.");

            if (userAnimal == null)
            {
                return RedirectToAction("Index", "Profile");
            }

            var qrQuery = new GetQRCodeQuery
            {
                AnimalId = userAnimal.Id
            };

            QRCodeDTO qrCodeText = await _mediator.Send(qrQuery);

            var code = CreateQRCode(qrCodeText.Code);

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

            return View(animalViewModel);
        }

        /// <summary>
        /// Получить информацию о выбранном животном (для другого пользователя, Public).
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Представление публичной страницы с информацией о животном.</returns>
        public async Task<IActionResult> Public(int id)
        {
            var animalQuery = new GetAnimalQuery
            {
                Id = id,
                AnotherUser = true
            };

            var userAnimal = await _mediator.Send(animalQuery);

            //_logger.LogInformation($"Animal {userAnimal.Nickname} showed for user {User.Identity.Name}.");

            if (userAnimal == null || !userAnimal.IsPublic)
            {
                return RedirectToAction("Index", "Home");
            }

            var qrQuery = new GetQRCodeQuery
            {
                AnimalId = userAnimal.Id
            };

            QRCodeDTO qrCodeText = await _mediator.Send(qrQuery);

            var code = CreateQRCode(qrCodeText.Code);

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

            return View(animalViewModel);
        }

        // UNDONE: Перенести в сервисы или отдельный метод
        private static byte[] CreateQRCode(string text)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            var code = BitmapToBytes(qrCodeImage);

            return code;
        }

        private static byte[] BitmapToBytes(Bitmap img)
        {
            using MemoryStream stream = new MemoryStream();
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }
    }
}
