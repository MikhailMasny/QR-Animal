using AutoMapper;
using Masny.QRAnimal.Application.Exceptions;
using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Application.ViewModels;
using Masny.QRAnimal.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.GetAnimal
{
    /// <summary>
    /// Получить данные Animal.
    /// </summary>
    public class GetAnimalQuery : IRequest<AnimalViewModel>
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Запрос получить данные.
        /// </summary>
        public class GetAnimalQueryHandler : IRequestHandler<GetAnimalQuery, AnimalViewModel>
        {
            private readonly IApplicationContext _context;
            private readonly IMapper _mapper;

            /// <summary>
            /// Конструктор с параметрами.
            /// </summary>
            /// <param name="context">Контекст.</param>
            /// <param name="mapper">Маппер.</param>
            public GetAnimalQueryHandler(IApplicationContext context,
                                         IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            /// <summary>
            /// Получить данные о животном.
            /// </summary>
            /// <returns>ViewModel животного.</returns>
            public async Task<AnimalViewModel> Handle(GetAnimalQuery request, CancellationToken cancellationToken)
            {
                var entity = await _context.Animals.FindAsync(request.Id, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Animal), request.Id);
                }

                var animal = _mapper.Map<AnimalViewModel>(entity);

                return animal;
            }
        }
    }
}
