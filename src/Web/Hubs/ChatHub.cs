using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Web.Hubs
{
    /// <summary>
    /// Хаб для чата пользователей.
    /// </summary>
    public class ChatHub : Hub
    {
        /// <summary>
        /// Отправка сообщения.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="message">Сообщение.</param>
        /// <returns></returns>
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
