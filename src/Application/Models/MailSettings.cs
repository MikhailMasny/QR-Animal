namespace Masny.QRAnimal.Application.Models
{
    /// <summary>
    /// Настройки для почтового сервиса.
    /// </summary>
    public class MailSettings
    {
        /// <summary>
        /// Сервер.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Порт.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Электронная почта.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get; set; }
    }
}
