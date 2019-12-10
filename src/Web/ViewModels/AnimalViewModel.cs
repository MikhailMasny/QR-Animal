using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Masny.QRAnimal.Web.ViewModels
{
    /// <summary>
    /// ViewModel для Animal.
    /// </summary>
    public class AnimalViewModel
    {
        /// <summary>
        /// Идентификатор животного.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Вид (разновидность).
        /// </summary>
        [Required(ErrorMessage = "Неверный Kind")]
        public string Kind { get; set; }

        /// <summary>
        /// Порода.
        /// </summary>
        [Required(ErrorMessage = "Неверный Breed")]
        public string Breed { get; set; }

        /// <summary>
        /// Пол.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Паспорт.
        /// </summary>
        [Required(ErrorMessage = "Неверный Passport")]
        public string Passport { get; set; }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateTime BirthDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Кличка.
        /// </summary>
        [Required(ErrorMessage = "Неверный Nickname")]
        public string Nickname { get; set; }

        /// <summary>
        /// Особенности.
        /// </summary>
        [Required(ErrorMessage = "Неверный Features")]
        public string Features { get; set; }



        /// <summary>
        /// Список возможных Gender.
        /// </summary>
        public SelectList GenderSelectList { get; set; } = new SelectList(new List<string>
        {
            "None",
            "Male",
            "Female"
        },
        "Gender");
    }
}
