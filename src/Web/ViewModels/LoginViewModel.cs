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
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Запомнить.
        /// </summary>
        public bool RememberMe { get; set; }

        /// <summary>
        /// Вернуться по определенному адресу.
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}
