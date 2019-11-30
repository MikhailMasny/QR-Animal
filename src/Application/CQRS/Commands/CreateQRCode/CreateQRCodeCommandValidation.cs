using FluentValidation;

namespace Masny.QRAnimal.Application.CQRS.Commands.CreateQRCode
{
    /// <summary>
    /// Валидация данных для QR Code.
    /// </summary>
    public class CreateQRCodeCommandValidation : AbstractValidator<CreateQRCodeCommand>
    {
        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public CreateQRCodeCommandValidation()
        {
            RuleFor(a => a.Model.Code).MaximumLength(50);
            RuleFor(a => a.Model.Created).NotEmpty();
            RuleFor(a => a.Model.AnimalId).NotEmpty();
        }
    }
}
