using Masny.QRAnimal.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMessageSender _messageSender;
        private readonly ILogger _logger;
        private IMediator _mediator;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="identityService">Cервис работы с идентификацией пользователя.</param>
        public HomeController(IMessageSender messageSender,
                              ILogger<AccountController> logger,
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
        public async Task<IActionResult> Test()
        {
            _logger.LogInformation("Test");

            //await _messageSender.SendMessageAsync("somemail@mail.ru", "Тема письма", "Тест письма: тест!");

            //var animalViewModel = new AnimalViewModel
            //{
            //    UserId = "test",
            //    Kind = "test",
            //    Breed = "test",
            //    Gender = GenderTypes.None,
            //    Passport = "test",
            //    BirthDate = DateTime.Now,
            //    Nickname = "test",
            //    Features = "test"
            //};

            //CreateAnimalCommand command = new CreateAnimalCommand
            //{
            //    Model = animalViewModel
            //};
            // var id = await _mediator.Send(command);

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