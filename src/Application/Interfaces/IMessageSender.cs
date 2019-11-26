using System.Threading.Tasks;

namespace Masny.QRAnimal.Application.Interfaces
{
    /// <summary>
    /// Интерфейс для сервиса работы с отправкой сообщений.
    /// </summary>
    public interface IMessageSender
    {
        /// <summary>
        /// Отправить сообщение.
        /// </summary>
        /// <param name="recipient">Получатель.</param>
        /// <param name="subject">Тема.</param>
        /// <param name="message">Сообщение</param>

        Task SendMessageAsync(string recipient, string subject, string message);
    }
}
