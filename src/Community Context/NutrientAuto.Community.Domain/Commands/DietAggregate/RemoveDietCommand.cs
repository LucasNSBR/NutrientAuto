using NutrientAuto.Community.Domain.Commands.DietAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.DietAggregate;

namespace NutrientAuto.Community.Domain.Commands.DietAggregate
{
    public class RemoveDietCommand : BaseDietCommand
    {
        public override bool Validate()
        {
            ValidationResult = new RemoveDietCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
