﻿using Masny.QRAnimal.Application.CQRS.Queries.GetAnimal;
using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
        /// Конструктор с параметрами.
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

            var animalQuery = new GetAnimalsByUserIdQuery
            {
                UserId = userId
            };

            var userAnimals = (await _mediator.Send(animalQuery)).ToList();

            _logger.LogInformation($"{userAnimals.Count} animals showed for user {User.Identity.Name}.");

            // Формирование ViewModels для представления
            var animalViewModels = new List<AnimalViewModel>();

            userAnimals.ForEach(a =>
            {
                var features = a.Features;

                if (features.Length > 200)
                {
                    features = features.Substring(0, 200) + "...";
                }

                var animal = new AnimalViewModel
                {
                    Id = a.Id,
                    UserId = a.UserId,
                    Kind = a.Kind,
                    Breed = a.Breed,
                    Gender = a.Gender.ToString(),
                    Passport = a.Passport,
                    BirthDate = a.BirthDate,
                    Nickname = a.Nickname,
                    Features = features
                };

                animalViewModels.Add(animal);
            });

            return View(animalViewModels);
        }
    }
}
