using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Masny.QRAnimal.Application.ViewModels
{
    /// <summary>
    /// ViewModel для входа в систему.
    /// </summary>
    public class LoginViewModel
    {
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
