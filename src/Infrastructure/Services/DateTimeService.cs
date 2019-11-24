using Masny.QRAnimal.Application.Interfaces;
using System;

namespace Masny.QRAnimal.Infrastructure.Services
{
    /// <summary>
    /// Cервис для работы со временем.
    /// </summary>
    public class DateTimeService : IDateTime
    {
        /// <inheritdoc />
        public DateTime Now => DateTime.Now;
    }
}
