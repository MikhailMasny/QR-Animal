﻿using AutoMapper;
using Masny.QRAnimal.Application.DTO;
using Masny.QRAnimal.Domain.Entities;

namespace Masny.QRAnimal.Application.Mapping
{
    /// <summary>
    /// Профиль AutoMapper для Animal.
    /// </summary>
    public class AnimalProfile : Profile
    {
        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public AnimalProfile()
        {
            CreateMap<Animal, AnimalDTO>().ReverseMap();
        }
    }
}
