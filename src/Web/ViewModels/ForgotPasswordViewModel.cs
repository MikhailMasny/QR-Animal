using System.ComponentModel.DataAnnotations;

namespace Masny.QRAnimal.Web.ViewModels
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
