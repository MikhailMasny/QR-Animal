using Masny.QRAnimal.Application.DTO;
using Masny.QRAnimal.Application.Exceptions;
using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Application.CQRS.Commands.UpdateAnimal
{
    /// <summary>
    /// Обновить Animal.
    /// </summary>
    public class UpdateAnimalCommand : IRequest
    {
        /// <summary>
        /// DTO животного.
        /// </summary>
        public AnimalDTO Model { get; set; }

        /// <summary>
        /// Команда обновить.
        /// </summary>
        public class UpdateAnimalCommandHandler : IRequestHandler<UpdateAnimalCommand>
        {
            private readonly IApplicationContext _context;

            /// <summary>
            /// Конструктор с параметрами.
            /// </summary>
            /// <param name="context">Контекст.</param>
            /// <param name="mapper">Маппер.</param>
            public UpdateAnimalCommandHandler(IApplicationContext context)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
            }

            /// <summary>
            /// Обновить животное.
            /// </summary>
            /// <returns>Id нового животного.</returns>
            public async Task<Unit> Handle(UpdateAnimalCommand request, CancellationToken cancellationToken)
            {
                request = request ?? throw new ArgumentNullException(nameof(request));

                var entity = await _context.Animals.Where(a => a.Id == request.Model.Id &&
                                                          a.UserId == request.Model.UserId &&
                                                          !a.IsDeleted)
                                                   .SingleOrDefaultAsync();

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Animal), request.Model.Id);
                }

                entity.Kind = request.Model.Kind;
                entity.Breed = request.Model.Breed;
                entity.Passport = request.Model.Passport;
                entity.Nickname = request.Model.Nickname;
                entity.Features = request.Model.Features;
                entity.IsPublic = request.Model.IsPublic;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
