﻿using Masny.QRAnimal.Application.CQRS.Commands.CreateAnimal;
using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Application.DTO;
using Masny.QRAnimal.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Web.Controllers
{
    /// <summary>
    /// Контроллер управления основными страницами приложения.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IMessageSender _messageSender;
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="identityService">Cервис работы с идентификацией пользователя.</param>
        public HomeController(IMessageSender messageSender,
                              ILogger<HomeController> logger,
                              IMediator mediator)
        {
            _messageSender = messageSender ?? throw new ArgumentNullException(nameof(messageSender));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Chat()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Test()
        {
            _logger.LogInformation("Test");

            //await _messageSender.SendMessageAsync("somemail@mail.ru", "Тема письма", "Тест письма: тест!");

            var animalDTO = new AnimalDTO
            {
                UserId = "34f27b3c-aa0d-481d-8750-2c0942a9099a",
                Kind = "test1",
                Breed = "test1",
                Gender = GenderTypes.None,
                Passport = "test1",
                BirthDate = DateTime.Now,
                Nickname = "test1",
                Features = "test1"
            };

            CreateAnimalCommand command = new CreateAnimalCommand
            {
                Model = animalDTO
            };
            var id = await _mediator.Send(command);

            //var animals = await _mediator.Send(new GetAnimalsQuery());

            //var qr = new QRCodeViewModel
            //{
            //    Code = "test",
            //    Created = DateTime.Now,
            //    AnimalId = 1
            //};

            //CreateQRCodeCommand command = new CreateQRCodeCommand
            //{
            //    Model = qr
            //};
            //var id = await _mediator.Send(command);

            //var qrq = new GetQRCodeQuery
            //{
            //    Id = 1
            //};
            //var qr = await _mediator.Send(qrq);

            return View();
        }
    }
}