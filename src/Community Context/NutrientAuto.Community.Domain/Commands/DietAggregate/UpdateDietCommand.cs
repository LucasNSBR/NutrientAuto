using NutrientAuto.Community.Domain.Commands.DietAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.DietAggregate;

namespace NutrientAuto.Community.Domain.Commands.DietAggregate
{
    public class UpdateDietCommand : BaseDietCommand
    {
        public override bool Validate()
        {
            ValidationResult = new UpdateDietCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
