using NutrientAuto.Identity.Domain.Commands.UserAggregate.BaseCommand;
using NutrientAuto.Identity.Domain.CommandValidators.UserAggregate;
using NutrientAuto.Shared.ValueObjects;

namespace NutrientAuto.Identity.Domain.Commands.UserAggregate
{
    public class RegisterAsNutritionistCommand : RegisterUserBaseCommand
    {
        public CrnNumber CrnNumber { get; set; }

        public override bool Validate()
        {
            ValidationResult = new RegisterAsNutritionistCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
