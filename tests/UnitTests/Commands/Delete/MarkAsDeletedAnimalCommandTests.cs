using Masny.QRAnimal.Application.CQRS.Commands.DeleteAnimal;
using Masny.QRAnimal.Application.Exceptions;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Masny.QRAnimal.UnitTests.Commands.Delete
{
    public class MarkAsDeletedAnimalCommandTests : BaseTestsFixture
    {
        [Fact]
        public async Task Handle_GivenValidId_ShouldMarkAsDeletedPersistedAnimal()
        {
            var command = new MarkAsDeletedAnimalCommand
            {
                AnimalId = 2,
                UserId = "QWERTY1234567890_Two"
            };

            var handler = new MarkAsDeletedAnimalCommand.MarkAsDeletedAnimalCommandHandler(Context);

            await handler.Handle(command, CancellationToken.None);

            var entity = Context.Animals.Find(command.AnimalId);

            entity.ShouldNotBeNull();

            entity.IsDeleted.ShouldBe(true);
        }

        [Fact]
        public void Handle_GivenInvalidId_ThrowsException()
        {
            var command = new MarkAsDeletedAnimalCommand
            {
                AnimalId = 99,
                UserId = "QWERTY1234567890_Test"
            };

            var handler = new MarkAsDeletedAnimalCommand.MarkAsDeletedAnimalCommandHandler(Context);

            Should.ThrowAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
