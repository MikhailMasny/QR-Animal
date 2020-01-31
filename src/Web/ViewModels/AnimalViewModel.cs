using Masny.QRAnimal.Domain.Enums;
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
        [Required]
        public string Kind { get; set; }

        /// <summary>
        /// Порода.
        /// </summary>
        [Required]
        public string Breed { get; set; }

        /// <summary>
        /// Пол.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Паспорт.
        /// </summary>
        [Required]
        public string Passport { get; set; }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateTime BirthDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Кличка.
        /// </summary>
        [Required]
        public string Nickname { get; set; }

        /// <summary>
        /// Особенности.
        /// </summary>
        [Required]
        public string Features { get; set; }

        /// <summary>
        /// Доступно всем.
        /// </summary>
        [Required]
        public bool IsPublic { get; set; }

        /// <summary>
        /// QR код в виде ссылки.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// QR код.
        /// </summary>
        public byte[] Code { get; set; }


        /// <summary>
        /// Список возможных Gender.
        /// </summary>
        public SelectList GenderSelectList { get; set; } = new SelectList(new List<string>
        {
            GenderTypes.None.ToString(),
            GenderTypes.Female.ToString(),
            GenderTypes.Male.ToString()
        },
        nameof(Gender));
    }
}
