using AutoMapper;
using Masny.QRAnimal.Application.DTO;
using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Application.CQRS.Queries.GetAnimal
{
    /// <summary>
    /// Получить Animal.
    /// </summary>
    public class GetAnimalQuery : IRequest<AnimalDTO>
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
        /// Другой пользователь.
        /// </summary>
        public bool AnotherUser { get; set; }

        /// <summary>
        /// Запрос получить данные.
        /// </summary>
        public class GetAnimalQueryHandler : IRequestHandler<GetAnimalQuery, AnimalDTO>
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
                _context = context ?? throw new ArgumentNullException(nameof(context));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            /// <summary>
            /// Получить данные о животном.
            /// </summary>
            /// <returns>DTO животного.</returns>
            public async Task<AnimalDTO> Handle(GetAnimalQuery request, CancellationToken cancellationToken)
            {
                request = request ?? throw new ArgumentNullException(nameof(request));

                Animal entity;
                // UNDONE: Переработать механизм
                if (request.AnotherUser)
                {
                    entity = await _context.Animals.Where(a => a.Id == request.Id &&
                                                          !a.IsDeleted)
                                                   .SingleOrDefaultAsync();
                }
                else
                {
                    entity = await _context.Animals.Where(a => a.Id == request.Id &&
                                                          a.UserId == request.UserId &&
                                                          !a.IsDeleted)
                                                   .SingleOrDefaultAsync();
                }

                var animal = _mapper.Map<AnimalDTO>(entity);

                return animal;
            }
        }
    }
}
