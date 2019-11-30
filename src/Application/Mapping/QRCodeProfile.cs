using AutoMapper;
using Masny.QRAnimal.Application.ViewModels;
using Masny.QRAnimal.Domain.Entities;

namespace Masny.QRAnimal.Application.Mapping
{
    /// <summary>
    /// Профиль AutoMapper для QRCode.
    /// </summary>
    public class QRCodeProfile : Profile
    {
        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public QRCodeProfile()
        {
            CreateMap<QRCode, QRCodeViewModel>().ReverseMap();
        }
    }
}
