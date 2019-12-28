using System.ComponentModel.DataAnnotations;

namespace Masny.QRAnimalWeb.ViewModels
{
    /// <summary>
    /// ViewModel для ForgotPassword.
    /// </summary>
    public class ForgotPasswordViewModel
    {
        /// <summary>
        /// Электронный адрес.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
