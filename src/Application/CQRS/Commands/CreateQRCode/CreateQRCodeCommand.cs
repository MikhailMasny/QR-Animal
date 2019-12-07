﻿using AutoMapper;
using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Application.ViewModels;
using Masny.QRAnimal.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Application.CQRS.Commands.CreateQRCode
{
    /// <summary>
    /// Добавить новый QRCode.
    /// </summary>
    public class CreateQRCodeCommand : IRequest<string>
    {
        /// <summary>
        /// ViewModel QRCode.
        /// </summary>
        public QRCodeViewModel Model { get; set; }

        /// <summary>
        /// Команда добавить.
        /// </summary>
        public class CreateQRCodeCommandHandler : IRequestHandler<CreateQRCodeCommand, string>
        {
            private readonly IApplicationContext _context;
            private readonly IMapper _mapper;

            /// <summary>
            /// Конструктор с параметрами.
            /// </summary>
            /// <param name="context">Контекст.</param>
            /// <param name="mapper">Маппер.</param>
            public CreateQRCodeCommandHandler(IApplicationContext context,
                                              IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            /// <summary>
            /// Добавить новое QR код.
            /// </summary>
            /// <returns>QR код.</returns>
            public async Task<string> Handle(CreateQRCodeCommand request, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<QRCode>(request.Model);

                _context.QRCodes.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return entity.Code;
            }
        }
    }
}