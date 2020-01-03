using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        /// Страница для чата пользователей.
        /// </summary>
        [Authorize]
        public IActionResult Chat()
        {
            return View();
        }
    }
}