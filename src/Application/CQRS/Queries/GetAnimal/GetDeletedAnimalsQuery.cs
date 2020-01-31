using AutoMapper;
using Masny.QRAnimal.Application.DTO;
using Masny.QRAnimal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Application.CQRS.Queries.GetAnimal
{
    /// <summary>
    /// Получить всех Animal помеченных на удаление.
    /// </summary>
    public class GetDeletedAnimalsQuery : IRequest<IEnumerable<AnimalDTO>>
    {
        /// <summary>
        /// Запрос получить данные.
        /// </summary>
        public class GetDeletedAnimalsQueryHandler : IRequestHandler<GetDeletedAnimalsQuery, IEnumerable<AnimalDTO>>
        {
            private readonly IApplicationContext _context;
            private readonly IMapper _mapper;

            /// <summary>
            /// Конструктор с параметрами.
            /// </summary>
            /// <param name="context">Контекст.</param>
            /// <param name="mapper">Маппер.</param>
            public GetDeletedAnimalsQueryHandler(IApplicationContext context,
                                                 IMapper mapper)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            /// <summary>
            /// Получить данные о животных помеченных на удаление.
            /// </summary>
            /// <returns>DTO животного.</returns>
            public async Task<IEnumerable<AnimalDTO>> Handle(GetDeletedAnimalsQuery request, CancellationToken cancellationToken)
            {
                var entites = await _context.Animals.Where(a => a.IsDeleted)
                                                    .ToListAsync(cancellationToken);

                var animals = _mapper.Map<List<AnimalDTO>>(entites);

                return animals;
            }
        }
    }
}
