using Masny.QRAnimal.Web.ViewModels;
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

        [Route("Error/{code:int}")]
        public IActionResult Error(int code)
        {
            var errorCodeViewModel = new ErrorCodeViewModel
            {
                Number = code
            };

            switch (code)
            {
                case 404: { errorCodeViewModel.Message = "Page not found."; } break;
                case 500: { errorCodeViewModel.Message = "Intermal server error."; } break;

                default: { errorCodeViewModel.Message = "Sorry, something went wrong.."; } break;
            }

            return View(errorCodeViewModel);
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