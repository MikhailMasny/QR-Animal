﻿using Masny.QRAnimal.Application.Exceptions;
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
    /// Удалить Animal.
    /// </summary>
    public class DeleteAnimalCommand : IRequest
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Команда удалить.
        /// </summary>
        public class DeleteAnimalCommandHandler : IRequestHandler<DeleteAnimalCommand>
        {
            private readonly IApplicationContext _context;

            /// <summary>
            /// Конструктор с параметрами.
            /// </summary>
            /// <param name="context">Контекст.</param>
            public DeleteAnimalCommandHandler(IApplicationContext context)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
            }

            /// <summary>
            /// Удалить животное.
            /// </summary>
            /// <returns>Значение.</returns>
            public async Task<Unit> Handle(DeleteAnimalCommand request, CancellationToken cancellationToken)
            {
                request = request ?? throw new ArgumentNullException(nameof(request));


                var entity = await _context.Animals.Where(a => a.Id == request.Id &&
                                                          a.IsDeleted)
                                                   .SingleOrDefaultAsync();

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Animal), request.Id);
                }

                _context.Animals.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
