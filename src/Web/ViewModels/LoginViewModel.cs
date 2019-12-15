using System.ComponentModel.DataAnnotations;

namespace Masny.QRAnimal.Web.ViewModels
{
    /// <summary>
    /// ViewModel для входа в систему.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [Required(ErrorMessage = "Неверное имя пользователя")]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        [Required(ErrorMessage = "Неверный пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        /// <summary>
        /// Запомнить.
        /// </summary>
        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        /// <summary>
        /// Вернуться по определенному адресу.
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}
