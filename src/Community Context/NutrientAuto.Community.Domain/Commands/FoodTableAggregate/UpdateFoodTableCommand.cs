using NutrientAuto.Community.Domain.Commands.FoodTableAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.FoodTableAggregate;

namespace NutrientAuto.Community.Domain.Commands.FoodTableAggregate
{
    public class UpdateFoodTableCommand : BaseFoodTableCommand
    {
        public override bool Validate()
        {
            ValidationResult = new UpdateFoodTableCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
