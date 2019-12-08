using AutoMapper;
using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Application.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Application.CQRS.Queries.GetAnimal
{
    /// <summary>
    /// Получить Animal.
    /// </summary>
    public class GetAnimalQuery : IRequest<AnimalViewModel>
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
                var entity = await _context.Animals.Where(a => a.Id == request.Id && 
                                                          a.UserId == request.UserId && 
                                                          !a.IsDeleted)
                                                   .SingleOrDefaultAsync();

                var animal = _mapper.Map<AnimalViewModel>(entity);

                return animal;
            }
        }
    }
}
