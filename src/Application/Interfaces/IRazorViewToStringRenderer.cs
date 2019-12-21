using System.Threading.Tasks;

namespace Masny.QRAnimal.Application.Interfaces
{
    /// <summary>
    /// Интерфейс для формирования HTML документа на основе Razor View.
    /// </summary>
    public interface IRazorViewToStringRenderer
    {
        /// <summary>
        /// Сформировать HTML документ.
        /// </summary>
        /// <typeparam name="TModel">Обобщенная модель.</typeparam>
        /// <param name="viewName">View представление.</param>
        /// <param name="model">Модель.</param>
        /// <returns>Строковое представление HMTL документа.</returns>
        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
    }
}
