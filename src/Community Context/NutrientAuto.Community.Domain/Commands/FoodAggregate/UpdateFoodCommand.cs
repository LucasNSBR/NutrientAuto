using NutrientAuto.Community.Domain.Commands.FoodAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.FoodAggregate;

namespace NutrientAuto.Community.Domain.Commands.FoodAggregate
{
    public class UpdateFoodCommand : BaseFoodCommand
    {
        public override bool Validate()
        {
            ValidationResult = new UpdateFoodCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
