using Masny.QRAnimal.Application.Interfaces;
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

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="identityService">Cервис работы с идентификацией пользователя.</param>
        public HomeController(IMessageSender messageSender,
                              ILogger<AccountController> logger)
        {
            _messageSender = messageSender ?? throw new ArgumentNullException(nameof(messageSender));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Test()
        {
            _logger.LogInformation("Test");

            await _messageSender.SendMessageAsync("somemail@mail.ru", "Тема письма", "Тест письма: тест!");

            return View();
        }
    }
}