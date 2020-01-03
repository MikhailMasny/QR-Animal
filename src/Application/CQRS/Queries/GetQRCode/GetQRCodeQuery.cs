using AutoMapper;
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

namespace Masny.QRAnimal.Application.CQRS.Queries.GetQRCode
{
    /// <summary>
    /// Получить данные QRCode.
    /// </summary>
    public class GetQRCodeQuery : IRequest<QRCodeDTO>
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int AnimalId { get; set; }

        /// <summary>
        /// Запрос получить данные.
        /// </summary>
        public class GetQRCodeQueryHandler : IRequestHandler<GetQRCodeQuery, QRCodeDTO>
        {
            private readonly IApplicationContext _context;
            private readonly IMapper _mapper;

            /// <summary>
            /// Конструктор с параметрами.
            /// </summary>
            /// <param name="context">Контекст.</param>
            /// <param name="mapper">Маппер.</param>
            public GetQRCodeQueryHandler(IApplicationContext context,
                                         IMapper mapper)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            /// <summary>
            /// Получить данные о QR Code.
            /// </summary>
            /// <returns>DTO QR Code.</returns>
            public async Task<QRCodeDTO> Handle(GetQRCodeQuery request, CancellationToken cancellationToken)
            {
                request = request ?? throw new ArgumentNullException(nameof(request));

                var entity = await _context.QRCodes.Where(a => a.AnimalId == request.AnimalId)
                                                   .SingleOrDefaultAsync();

                if (entity == null)
                {
                    throw new NotFoundException(nameof(QRCode), request.AnimalId);
                }

                var qrcode = _mapper.Map<QRCodeDTO>(entity);

                return qrcode;
            }
        }
    }
}
