using System;

namespace Domain.Entities
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
        /// Идентификатор животного.
        /// </summary>
        public int AnimalId { get; set; }

        /// <summary>
        /// Данные для генерации.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Дата создания.
        /// </summary>
        public DateTime Created { get; set; }
    }
}
