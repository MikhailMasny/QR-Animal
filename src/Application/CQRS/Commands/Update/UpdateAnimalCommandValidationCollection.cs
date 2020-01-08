using FluentValidation;

namespace Masny.QRAnimal.Application.CQRS.Commands.UpdateAnimal
{
    /// <summary>
    /// Валидация данных для Animal.
    /// </summary>
    public class UpdateAnimalCommandValidationCollection : AbstractValidator<UpdateAnimalCommand>
    {
        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public UpdateAnimalCommandValidationCollection()
        {
            RuleFor(a => a.Model.Nickname).MaximumLength(100).NotEmpty();
            RuleFor(a => a.Model.Passport).MaximumLength(50);
            RuleFor(a => a.Model.Kind).MaximumLength(50);
            RuleFor(a => a.Model.Breed).MaximumLength(100);
            RuleFor(a => a.Model.IsPublic).Must(x => !x || x);
            RuleFor(a => a.Model.Features).MaximumLength(256);
        }
    }
}
