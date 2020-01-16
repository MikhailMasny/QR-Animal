using Masny.QRAnimal.Application.Exceptions;
using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Application.CQRS.Commands.DeleteQRCode
{
    /// <summary>
    /// Удалить QRCode.
    /// </summary>
    public class DeleteQRCodeCommand : IRequest
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int AnimalId { get; set; }

        /// <summary>
        /// Команда удалить.
        /// </summary>
        public class DeleteQRCodeCommandHandler : IRequestHandler<DeleteQRCodeCommand>
        {
            private readonly IApplicationContext _context;

            /// <summary>
            /// Конструктор с параметрами.
            /// </summary>
            /// <param name="context">Контекст.</param>
            public DeleteQRCodeCommandHandler(IApplicationContext context)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
            }

            /// <summary>
            /// Удалить QR код.
            /// </summary>
            /// <returns>Значение.</returns>
            public async Task<Unit> Handle(DeleteQRCodeCommand request, CancellationToken cancellationToken)
            {
                request = request ?? throw new ArgumentNullException(nameof(request));


                var entity = await _context.QRCodes.Where(qr => qr.AnimalId == request.AnimalId)
                                                   .SingleOrDefaultAsync();

                if (entity == null)
                {
                    throw new NotFoundException(nameof(QRCode), request.AnimalId);
                }

                _context.QRCodes.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
