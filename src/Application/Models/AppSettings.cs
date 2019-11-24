namespace Masny.QRAnimal.Application.Models
{
    /// <summary>
    /// Настройки приложения.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Окружение приложения.
        /// false - Windows
        /// true - Docker
        /// </summary>
        public bool IsDockerSupport { get; set; }
    }
}
