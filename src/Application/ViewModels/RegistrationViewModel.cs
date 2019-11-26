using System.ComponentModel.DataAnnotations;

namespace Masny.QRAnimal.Application.ViewModels
{
    /// <summary>
    /// ViewModel для формы регистрации.
    /// </summary>
    public class RegistrationViewModel
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [Required(ErrorMessage = "Неверное имя пользователя")]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        /// <summary>
        /// Электронный адрес.
        /// </summary>
        [Required(ErrorMessage = "Неверная электронная почта")]
        [Display(Name = "Электронная почта")]
        public string Email { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        [Required(ErrorMessage = "Неверный пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        /// <summary>
        /// Подтвердить пароль.
        /// </summary>
        [Required(ErrorMessage = "Неверный пароль")]
        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}
