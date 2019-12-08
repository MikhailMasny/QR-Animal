﻿using AutoMapper;
using Masny.QRAnimal.Application.Exceptions;
using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Application.ViewModels;
using Masny.QRAnimal.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Application.CQRS.Commands.UpdateAnimal
{
    /// <summary>
    /// Обновить новый Animal.
    /// </summary>
    public class UpdateAnimalCommand : IRequest
    {
        /// <summary>
        /// ViewModel животного.
        /// </summary>
        public AnimalViewModel Model { get; set; }

        /// <summary>
        /// Команда обновить.
        /// </summary>
        public class UpdateAnimalCommandHandler : IRequestHandler<UpdateAnimalCommand>
        {
            private readonly IApplicationContext _context;
            private readonly IMapper _mapper;

            /// <summary>
            /// Конструктор с параметрами.
            /// </summary>
            /// <param name="context">Контекст.</param>
            /// <param name="mapper">Маппер.</param>
            public UpdateAnimalCommandHandler(IApplicationContext context,
                                              IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            /// <summary>
            /// Обновить животное.
            /// </summary>
            /// <returns>Id нового животного.</returns>
            public async Task<Unit> Handle(UpdateAnimalCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Animals.FindAsync(request.Model.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Animal), request.Model.Id);
                }

                entity.Kind = request.Model.Kind;
                entity.Breed = request.Model.Breed;
                entity.Passport = request.Model.Passport;
                entity.Nickname = request.Model.Nickname;
                entity.Features = request.Model.Features;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
