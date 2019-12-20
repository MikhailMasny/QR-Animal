﻿using Masny.QRAnimal.Application.DTO;
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
        private readonly IIdentityService _identityService;
        private readonly IMessageSender _messageSender;
        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="identityService">Cервис работы с идентификацией пользователя.</param>
        /// <param name="messageSender">Cервис работы с почтой.</param>
        public AccountController(IIdentityService identityService,
                                 IMessageSender messageSender,
                                 IRazorViewToStringRenderer razorViewToStringRenderer)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _messageSender = messageSender ?? throw new ArgumentNullException(nameof(messageSender));
            _razorViewToStringRenderer = razorViewToStringRenderer ?? throw new ArgumentNullException(nameof(razorViewToStringRenderer));
        }

        /// <summary>
        /// Страница для входа в систему.
        /// </summary>
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        /// <summary>
        /// Регистрация нового пользователя.
        /// </summary>
        /// <param name="model">Модель пользовательских данных.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrationAsync(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var (result, userId, code) = await _identityService.CreateUserAsync(model.Email, model.UserName, model.Password);

                if (result.Succeeded)
                {
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId, code }, protocol: HttpContext.Request.Scheme);

                    var emailModel = new EmailDTO
                    {
                        UserName = "User",
                        SenderName = "Sender",
                        UserData1 = 1,
                        UserData2 = 2
                    };

                    var test = await _razorViewToStringRenderer.RenderViewToStringAsync("Views/Email/EmailTemplate.cshtml", emailModel);

                    await _messageSender.SendMessageAsync(model.Email, "Confirm your account", $"{test} Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

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
        public IActionResult Login(string returnUrl = null)
        {
            var viewModel = new LoginViewModel
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
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
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

            ModelState.AddModelError("", "Неправильный логин и (или) пароль");

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
        /// Подтвердить email.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <param name="code">Confirmation Token.</param>
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
    }
}
