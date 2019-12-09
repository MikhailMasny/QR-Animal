using AutoMapper;
using Masny.QRAnimal.Application.Interfaces;
using Masny.QRAnimal.Application.DTO;
using Masny.QRAnimal.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Masny.QRAnimal.Application.CQRS.Commands.CreateAnimal
{
    /// <summary>
    /// Добавить новый Animal.
    /// </summary>
    public class CreateAnimalCommand : IRequest<int>
    {
        /// <summary>
        /// DTO животного.
        /// </summary>
        public AnimalDTO Model { get; set; }

        /// <summary>
        /// Команда добавить.
        /// </summary>
        public class CreateTodoItemCommandHandler : IRequestHandler<CreateAnimalCommand, int>
        {
            private readonly IApplicationContext _context;
            private readonly IMapper _mapper;

            /// <summary>
            /// Конструктор с параметрами.
            /// </summary>
            /// <param name="context">Контекст.</param>
            /// <param name="mapper">Маппер.</param>
            public CreateTodoItemCommandHandler(IApplicationContext context,
                                                IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            /// <summary>
            /// Добавить новое животное.
            /// </summary>
            /// <returns>Id нового животного.</returns>
            public async Task<int> Handle(CreateAnimalCommand request, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<Animal>(request.Model);

                _context.Animals.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
