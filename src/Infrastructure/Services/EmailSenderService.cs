using System.Threading.Tasks;
using Masny.QRAnimal.Application.Interfaces;

namespace Masny.QRAnimal.Infrastructure.Services
{
    /// <summary>
    /// Сервис для отправки сообщений по электронной почте.
    /// </summary>
    public class EmailSenderService : IMessageSender
    {
        /// <inheritdoc />
        public Task SendMessageAsync(string recipient, string subject, string message)
        {
            throw new System.NotImplementedException();
        }
    }
}
