using AutoMapper;
using Masny.QRAnimal.Application.Exceptions;
using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Application.ViewModels;
using Masny.QRAnimal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Application.CQRS.Queries.GetAnimal
{
    /// <summary>
    /// Получить данные Animal.
    /// </summary>
    public class GetAnimalsQuery : IRequest<IEnumerable<AnimalViewModel>>
    {
        /// <summary>
        /// Запрос получить данные.
        /// </summary>
        public class GetAnimalsQueryHandler : IRequestHandler<GetAnimalsQuery, IEnumerable<AnimalViewModel>>
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
                _context = context;
                _mapper = mapper;
            }

            /// <summary>
            /// Получить данные о животных.
            /// </summary>
            /// <returns>ViewModel животного.</returns>
            public async Task<IEnumerable<AnimalViewModel>> Handle(GetAnimalsQuery request, CancellationToken cancellationToken)
            {
                var entites = await _context.Animals.ToListAsync(cancellationToken);

                if (!entites.Any())
                {
                    throw new NotFoundException(nameof(Animal), "list");
                }

                var animals = _mapper.Map<List<AnimalViewModel>>(entites);

                return animals;
            }
        }
    }
}
