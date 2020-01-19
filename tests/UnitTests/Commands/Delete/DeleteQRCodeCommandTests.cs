using Masny.QRAnimal.Application.CQRS.Commands.DeleteQRCode;
using Masny.QRAnimal.Application.Exceptions;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Masny.QRAnimal.UnitTests.Commands.Delete
{
    public class DeleteQRCodeCommandTests : BaseTestsFixture
    {
        [Fact]
        public async Task Handle_GivenValidId_ShouldRemovePersistedQRCode()
        {
            var command = new DeleteQRCodeCommand
            {
                AnimalId = 1
            };

            var handler = new DeleteQRCodeCommand.DeleteQRCodeCommandHandler(Context);

            await handler.Handle(command, CancellationToken.None);

            var entity = Context.QRCodes.Find(command.AnimalId);

            entity.ShouldBeNull();
        }

        [Fact]
        public void Handle_GivenInvalidId_ThrowsException()
        {
            var command = new DeleteQRCodeCommand
            {
                AnimalId = 99
            };

            var handler = new DeleteQRCodeCommand.DeleteQRCodeCommandHandler(Context);

            Should.ThrowAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
