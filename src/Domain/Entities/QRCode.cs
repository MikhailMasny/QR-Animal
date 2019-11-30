using System;

namespace Masny.QRAnimal.Domain.Entities
{
    /// <summary>
    /// Модель QR кода.
    /// </summary>
    public class QRCode
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Данные для генерации.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Дата создания.
        /// </summary>
        public DateTime Created { get; set; }




        /// <summary>
        /// Идентификатор животного.
        /// </summary>
        public int AnimalId { get; set; }

        /// <summary>
        /// Навигация для животного.
        /// </summary>
        public Animal Animal { get; set; }
    }
}
