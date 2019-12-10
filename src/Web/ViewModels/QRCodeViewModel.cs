using System.ComponentModel.DataAnnotations;

namespace Masny.QRAnimal.Web.ViewModels
{
    /// <summary>
    /// ViewModel для QRCode.
    /// </summary>
    public class QRCodeViewModel
    {
        /// <summary>
        /// Данные для генерации.
        /// </summary>
        [Required(ErrorMessage = "Неверный QR code")]
        public string Code { get; set; }
    }
}
