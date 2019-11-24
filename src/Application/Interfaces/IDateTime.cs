using System;

namespace Masny.QRAnimal.Application.Interfaces
{
    /// <summary>
    /// Интерфейс для сервиса работы со временем.
    /// </summary>
    public interface IDateTime
    {
        /// <summary>
        /// Текущее время.
        /// </summary>
        DateTime Now { get; }
    }
}
