using AutoMapper;
using Masny.QRAnimal.Application.Exceptions;
using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Application.ViewModels;
using Masny.QRAnimal.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Application.CQRS.Queries.GetQRCode
{
    /// <summary>
    /// Получить данные QRCode.
    /// </summary>
    public class GetQRCodeQuery : IRequest<QRCodeViewModel>
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Запрос получить данные.
        /// </summary>
        public class GetQRCodeQueryHandler : IRequestHandler<GetQRCodeQuery, QRCodeViewModel>
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
                _context = context;
                _mapper = mapper;
            }

            /// <summary>
            /// Получить данные о QR Code.
            /// </summary>
            /// <returns>ViewModel QR Code.</returns>
            public async Task<QRCodeViewModel> Handle(GetQRCodeQuery request, CancellationToken cancellationToken)
            {
                var entity = await _context.QRCodes.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(QRCode), request.Id);
                }

                var qrcode = _mapper.Map<QRCodeViewModel>(entity);

                return qrcode;
            }
        }
    }
}
