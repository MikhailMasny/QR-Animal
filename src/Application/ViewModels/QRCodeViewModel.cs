using System;

namespace Masny.QRAnimal.Application.ViewModels
{
    /// <summary>
    /// ViewModel для QRCode.
    /// </summary>
    public class QRCodeViewModel
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
