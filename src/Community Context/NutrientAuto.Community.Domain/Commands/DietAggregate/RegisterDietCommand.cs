using NutrientAuto.Community.Domain.Commands.DietAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.DietAggregate;

namespace NutrientAuto.Community.Domain.Commands.DietAggregate
{
    public class RegisterDietCommand : BaseDietCommand
    {
        public override bool Validate()
        {
            ValidationResult = new RegisterDietCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
