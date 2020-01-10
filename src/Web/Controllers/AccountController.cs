using Masny.QRAnimal.Application.DTO;
using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Web.Controllers
{
    /// <summary>
    /// Контроллер управления личным кабинетом пользователя.
    /// </summary>
    public class AccountController : Controller
    {
        private const string _emailExisted = "Email is already existed.";
        private const string _incorrectData = "Incorrect username and / or password";
        private const string _accountConfirm = "Confirm your account";
        private const string _accountResetPassword = "Reset password";

        private readonly IIdentityService _identityService;
        private readonly IMessageSender _messageSender;
        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="identityService">Cервис работы с идентификацией пользователя.</param>
        /// <param name="messageSender">Cервис работы с почтой.</param>
        /// <param name="razorViewToStringRenderer">Cервис для генерации HTML документов.</param>
        public AccountController(IIdentityService identityService,
                                 IMessageSender messageSender,
                                 IRazorViewToStringRenderer razorViewToStringRenderer)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _messageSender = messageSender ?? throw new ArgumentNullException(nameof(messageSender));
            _razorViewToStringRenderer = razorViewToStringRenderer ?? throw new ArgumentNullException(nameof(razorViewToStringRenderer));
        }

        /// <summary>
        /// Страница для регистрации нового пользователя.
        /// </summary>
        /// <returns>Определенное представление.</returns>
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        /// <summary>
        /// Регистрация нового пользователя.
        /// </summary>
        /// <param name="model">Модель пользовательских данных.</param>
        /// <returns>Определенное представление.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrationAsync(RegistrationViewModel model)
        {
            model = model ?? throw new ArgumentNullException(nameof(model));

            if (ModelState.IsValid)
            {
                var (result, userId, code) = await _identityService.CreateUserAsync(model.Email, model.UserName, model.Password);

                if (result == null)
                {
                    ModelState.AddModelError(string.Empty, _emailExisted);

                    return View(model);
                }

                if (result.Succeeded)
                {
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId, code }, protocol: HttpContext.Request.Scheme);

                    var emailModel = new EmailDTO
                    {
                        UserName = model.UserName,
                        Code = callbackUrl
                    };

                    var body = await _razorViewToStringRenderer.RenderViewToStringAsync("Views/Email/EmailConfirm.cshtml", emailModel);

                    await _messageSender.SendMessageAsync(model.Email, _accountConfirm, body);

                    return View("RegistartionSucceeded");
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
        /// <returns>Определенное представление.</returns>
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            var viewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(viewModel);
        }

        /// <summary>
        /// Вход в систему.
        /// </summary>
        /// <param name="model">Модель пользовательских данных.</param>
        /// <returns>Определенное представление.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            model = model ?? throw new ArgumentNullException(nameof(model));

            if (ModelState.IsValid)
            {
                var (result, message) = await _identityService.EmailConfirmCheckerAsync(model.UserName);

                if (!result)
                {
                    ModelState.AddModelError(string.Empty, message);

                    return View(model);
                }

                var isSignIn = await _identityService.LoginUserAsync(model.UserName, model.Password, model.RememberMe, true);

                if (isSignIn.Succeeded)
                {
                    // Проверка на принадлежность URL приложению.
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, _incorrectData);

            return View(model);
        }

        /// <summary>
        /// Выход из системы.
        /// </summary>
        /// <returns>Перенаправление на определенную страницу.</returns>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutAsync()
        {
            await _identityService.LogoutUserAsync();

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Подтверждение почты.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <param name="code">Confirmation Token.</param>
        /// <returns>Определенное представление.</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var (result, _) = await _identityService.ConfirmEmail(userId, code);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("Error");
        }

        /// <summary>
        /// Страница для восстановления пароля.
        /// </summary>
        /// <returns>Определенное представление.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        /// <summary>
        /// Восстановление пароля.
        /// </summary>
        /// <param name="model">Модель для восстановления пароля.</param>
        /// <returns>Определенное представление.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            model = model ?? throw new ArgumentNullException(nameof(model));

            if (ModelState.IsValid)
            {
                var (result, _, userName, code) = await _identityService.ForgotPassword(model.Email);

                if (!result)
                {
                    return View("ForgotPasswordConfirmation");
                }

                var callbackUrl = Url.Action("ResetPassword", "Account", new { userName, code }, protocol: HttpContext.Request.Scheme);

                var emailModel = new EmailDTO
                {
                    UserName = userName,
                    Code = callbackUrl
                };

                var body = await _razorViewToStringRenderer.RenderViewToStringAsync("Views/Email/EmailForgotPassword.cshtml", emailModel);

                await _messageSender.SendMessageAsync(model.Email, _accountResetPassword, body);

                return View("ForgotPasswordConfirmation");
            }

            return View(model);
        }

        /// <summary>
        /// Страница для сброса пароля.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="code">Confirmation Token</param>
        /// <returns>Определенное представление.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string userName = null, string code = null)
        {
            if (userName == null || code == null)
            {
                return View("Error");
            }

            return View();
        }

        /// <summary>
        /// Сброс пароля.
        /// </summary>
        /// <param name="model">Модель сброса пароля.</param>
        /// <returns>Определенное представление.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            model = model ?? throw new ArgumentNullException(nameof(model));

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _identityService.ResetPassword(model.UserName, model.Password, model.Code);

            if (result == null)
            {
                return View("ResetPasswordConfirmation");
            }

            if (result.Succeeded)
            {
                return View("ResetPasswordConfirmation");
            }

            return View("ResetPasswordInvalid");
        }
    }
}
