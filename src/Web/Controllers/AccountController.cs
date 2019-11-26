using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Web.Controllers
{
    /// <summary>
    /// Контроллер управления личным кабинетом пользователя.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger _logger;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="identityService">Cервис работы с идентификацией пользователя.</param>
        public AccountController(IIdentityService identityService,
                                 ILogger<AccountController> logger)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Страница для входа в систему.
        /// </summary>
        [HttpGet]
        public IActionResult SignUp()
        {
            _logger.LogInformation("Test");
            return View();
        }

        /// <summary>
        /// Регистрация нового пользователя.
        /// </summary>
        /// <param name="model">Модель пользовательских данных.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUpAsync(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var (result, _) = await _identityService.CreateUserAsync(model.Email, model.UserName, model.Password);

                if (result.Succeeded)
                {
                    await _identityService.SignInUserAsync(model.Email, model.UserName);

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }

            return View(model);
        }

        /// <summary>
        /// Страница для входа в систему.
        /// </summary>
        /// <param name="returnUrl">Возврат по определенному адресу.</param>
        [HttpGet]
        public IActionResult SignIn(string returnUrl = null)
        {
            var viewModel = new SignInViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(viewModel);
        }

        /// <summary>
        /// Выполнение входа.
        /// </summary>
        /// <param name="model">Модель пользовательских данных.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignInAsync(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _identityService.LoginUserAsync(model.Email, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    // Проверка на принадлежность URL приложению.
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "InvalidData");
            }

            return View(model);
        }

        /// <summary>
        /// Выход из системы.
        /// </summary>
        /// <returns>Перенаправление на определенную страницу.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignOutAsync()
        {
            await _identityService.SignOutUserAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}