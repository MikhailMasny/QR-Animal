using Masny.QRAnimal.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Masny.QRAnimal.Domain.Entities
{
    /// <summary>
    /// Модель животного.
    /// </summary>
    public class Animal
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Вид (разновидность).
        /// </summary>
        public string Kind { get; set; }

        /// <summary>
        /// Порода.
        /// </summary>
        public string Breed { get; set; }

        /// <summary>
        /// Пол.
        /// </summary>
        public GenderTypes Gender { get; set; }

        /// <summary>
        /// Паспорт.
        /// </summary>
        public string Passport { get; set; }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Кличка.
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// Особенности.
        /// </summary>
        public string Features { get; set; }

        /// <summary>
        /// Удалено.
        /// </summary>
        public bool IsDeleted { get; set; }



        /// <summary>
        /// Навигация по QR кодам.
        /// </summary>
        public ICollection<QRCode> QRCodes { get; set; }
    }
}
