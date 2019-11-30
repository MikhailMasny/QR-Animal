using System;

namespace Masny.QRAnimal.Application.Exceptions
{
    /// <summary>
    /// Ошибка об отсутсвие сущности.
    /// </summary>
    public class NotFoundException : Exception
    {
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
