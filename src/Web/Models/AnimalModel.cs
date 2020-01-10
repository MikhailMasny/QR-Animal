using System;

namespace Masny.QRAnimal.Web.Models
{
    /// <summary>
    /// API model для Animal.
    /// </summary>
    public class AnimalModel
    {
        /// <summary>
        /// Идентификатор животного.
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
        public string Gender { get; set; }

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
    }
}
