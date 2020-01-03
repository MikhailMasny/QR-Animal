using System;

namespace Masny.QRAnimal.Application.Exceptions
{
    /// <summary>
    /// Ошибка об отсутсвие сущности.
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public NotFoundException() { }

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        public NotFoundException(string message) : base(message) { }

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="innerException">Ошибка.</param>
        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="name">Название.</param>
        /// <param name="key">Ключ.</param>
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}
