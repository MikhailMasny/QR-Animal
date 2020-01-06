using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Web.Hubs
{
    /// <summary>
    /// Хаб для чата пользователей.
    /// </summary>
    public class ChatHub : Hub
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="logger">Логгер.</param>
        public ChatHub(ILogger<ChatHub> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Отправка сообщения.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="message">Сообщение.</param>
        /// <returns></returns>
        public async Task SendMessage(string user, string message)
        {
            _logger.LogInformation($"User: {user} send message: {message}.");

            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
