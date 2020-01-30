using AutoMapper;
using Masny.QRAnimal.Application.DTO;
using Masny.QRAnimal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Application.CQRS.Queries.GetAnimal
{
    /// <summary>
    /// Получить всех Animal.
    /// </summary>
    public class GetAnimalsQuery : IRequest<IEnumerable<AnimalDTO>>
    {
        /// <summary>
        /// Запрос получить данные.
        /// </summary>
        public class GetAnimalsQueryHandler : IRequestHandler<GetAnimalsQuery, IEnumerable<AnimalDTO>>
        {
            private readonly IApplicationContext _context;
            private readonly IMapper _mapper;

            /// <summary>
            /// Конструктор с параметрами.
            /// </summary>
            /// <param name="context">Контекст.</param>
            /// <param name="mapper">Маппер.</param>
            public GetAnimalsQueryHandler(IApplicationContext context,
                                          IMapper mapper)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            /// <summary>
            /// Получить данные о животных.
            /// </summary>
            /// <returns>DTO животного.</returns>
            public async Task<IEnumerable<AnimalDTO>> Handle(GetAnimalsQuery request, CancellationToken cancellationToken)
            {
                var entites = await _context.Animals.ToListAsync(cancellationToken);

                var animals = _mapper.Map<List<AnimalDTO>>(entites);

                return animals;
            }
        }
    }
}
