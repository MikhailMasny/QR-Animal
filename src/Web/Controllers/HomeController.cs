using Masny.QRAnimal.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;

namespace Masny.QRAnimal.Web.Controllers
{
    /// <summary>
    /// Контроллер управления основными страницами приложения.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Главная страница.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Страница отображения ошибки.
        /// </summary>
        /// <param name="code">Код ошибки.</param>
        /// <returns>Общее представление с кодом ошибки.</returns>
        [Route("Error/{code:int}")]
        public IActionResult Error(int code)
        {
            var errorViewModel = new ErrorViewModel
            {
                RequestId = code.ToString(CultureInfo.InvariantCulture)
            };

            return View(errorViewModel);
        }

        /// <summary>
        /// Установка языковых параметров.
        /// </summary>
        /// <param name="culture">Культура.</param>
        /// <param name="returnUrl">URL для возврата.</param>
        /// <returns>Языковое переключение.</returns>
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        /// <summary>
        /// Страница для чата пользователей.
        /// </summary>
        [Authorize]
        [ResponseCache(CacheProfileName = "NotCaching")]
        public IActionResult Chat()
        {
            return View();
        }
    }
}