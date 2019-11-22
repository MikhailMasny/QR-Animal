namespace Masny.QRAnimal.ApplicationCore.Interfaces
{
    /// <summary>
    /// Интерфейс для логгера.
    /// </summary>
    /// <typeparam name="T">Обобщенный тип.</typeparam>
    public interface IAppLogger<T>
    {
        /// <summary>
        /// Логгирование информации.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="args">Аргументы.</param>
        void LogInformation(string message, params object[] args);

        /// <summary>
        /// Логгирование предупреждения.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="args">Аргументы.</param>
        void LogWarning(string message, params object[] args);
    }
}
