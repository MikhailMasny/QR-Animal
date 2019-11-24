namespace Masny.QRAnimal.Infrastructure.Extensions
{
    /// <summary>
    /// Метод расширения для конвертации окружения приложения в текстовый формат получения секции строки подключения к БД.
    /// </summary>
    public static class DatabaseConnection
    {
        /// <summary>
        /// Конвертация на основе окружения приложения.
        /// </summary>
        /// <param name="environment">окружение приложения.</param>
        /// <returns>Секция строки подключения в текстовом формате.</returns>
        public static string ToDbConnectionString(this bool environment)
        {
            string result;

            switch (environment)
            {
                case true: { result = "DockerConnection"; } break;

                default: { result = "WindowsConnection"; } break;
            }

            return result;
        }
    }
}
