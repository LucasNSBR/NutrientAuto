using NutrientAuto.Community.Domain.Commands.FoodAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.FoodAggregate;

namespace NutrientAuto.Community.Domain.Commands.FoodAggregate
{
    public class RemoveFoodCommand : BaseFoodCommand
    {
        public override bool Validate()
        {
            ValidationResult = new RemoveFoodCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
