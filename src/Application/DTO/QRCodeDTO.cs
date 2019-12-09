using System;

namespace Masny.QRAnimal.Application.DTO
{
    /// <summary>
    /// DataTransferObject для QRCode.
    /// </summary>
    public class QRCodeDTO
    {
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
    }
}
