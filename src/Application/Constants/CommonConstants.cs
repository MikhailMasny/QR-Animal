namespace Masny.QRAnimal.Application.Constants
{
    /// <summary>
    /// Класс для общих констант.
    /// </summary>
    public static class CommonConstants
    {
        /// <summary>
        /// Успешно.
        /// </summary>
        public const string Successfully = nameof(Successfully);

        /// <summary>
        /// Строка подключения для Docker.
        /// </summary>
        public const string DockerConnection = nameof(DockerConnection);

        /// <summary>
        /// Строка подключения для Windows.
        /// </summary>
        public const string WindowsConnection = nameof(WindowsConnection);

        /// <summary>
        /// Название таблицы для логгирования Serilog.
        /// </summary>
        public const string EventLog = nameof(EventLog);

        /// <summary>
        /// Приложение запустилось.
        /// </summary>
        public const string HostStart = "Starting web host.";

        /// <summary>
        /// В приложении возникла ошибка.
        /// </summary>
        public const string HostTerminate = "Host terminated unexpectedly.";
    }
}
