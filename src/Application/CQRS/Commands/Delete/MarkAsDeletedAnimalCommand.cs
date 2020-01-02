using Masny.QRAnimal.Application.Exceptions;
using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Application.CQRS.Commands.DeleteAnimal
{
    /// <summary>
    /// Пометить на удаление Animal.
    /// </summary>
    public class MarkAsDeletedAnimalCommand : IRequest
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Команда пометить на удаление.
        /// </summary>
        public class MarkAsDeletedAnimalCommandHandler : IRequestHandler<MarkAsDeletedAnimalCommand>
        {
            private readonly IApplicationContext _context;

            /// <summary>
            /// Конструктор с параметрами.
            /// </summary>
            /// <param name="context">Контекст.</param>
            public MarkAsDeletedAnimalCommandHandler(IApplicationContext context)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
            }

            /// <summary>
            /// Пометить на удаление животное.
            /// </summary>
            /// <returns>Значение.</returns>
            public async Task<Unit> Handle(MarkAsDeletedAnimalCommand request, CancellationToken cancellationToken)
            {
                request = request ?? throw new ArgumentNullException(nameof(request));


                var entity = await _context.Animals.Where(a => a.Id == request.Id &&
                                                          a.UserId == request.UserId &&
                                                          !a.IsDeleted)
                                                   .SingleOrDefaultAsync();

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Animal), request.Id);
                }

                entity.IsDeleted = true;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
