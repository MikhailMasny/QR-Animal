using System.ComponentModel.DataAnnotations;

namespace Masny.QRAnimal.Web.ViewModels
{
    /// <summary>
    /// ViewModel для ResetPassword.
    /// </summary>
    public class ResetPasswordViewModel
    {
        /// <summary>
        /// Электронный адрес.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "Пароль должен содержать как минимум 6 символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Подтвердить пароль.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Верификационный код.
        /// </summary>
        public string Code { get; set; }
    }
}
