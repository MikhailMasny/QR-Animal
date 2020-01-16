using Masny.QRAnimal.Application.CQRS.Commands.DeleteAnimal;
using Masny.QRAnimal.Application.Exceptions;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Masny.QRAnimal.UnitTests.Commands.Delete
{
    public class DeleteAnimalCommandTests : BaseTestsFixture
    {
        [Fact]
        public async Task Handle_GivenValidId_ShouldRemovePersistedAnimal()
        {
            var command = new DeleteAnimalCommand
            {
                Id = 3
            };

            var handler = new DeleteAnimalCommand.DeleteAnimalCommandHandler(Context);

            await handler.Handle(command, CancellationToken.None);

            var entity = Context.Animals.Find(command.Id);

            entity.ShouldBeNull();
        }

        [Fact]
        public void Handle_GivenInvalidId_ThrowsException()
        {
            var command = new DeleteAnimalCommand
            {
                Id = 99
            };

            var handler = new DeleteAnimalCommand.DeleteAnimalCommandHandler(Context);

            Should.ThrowAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
