using System.ComponentModel.DataAnnotations;

namespace Masny.QRAnimal.Web.ViewModels
{
    /// <summary>
    /// ViewModel для формы регистрации.
    /// </summary>
    public class RegistrationViewModel
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Электронный адрес.
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Подтвердить пароль.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string PasswordConfirm { get; set; }
    }
}
