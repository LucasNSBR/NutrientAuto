using NutrientAuto.Community.Domain.Commands.FoodTableAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.FoodTableAggregate;

namespace NutrientAuto.Community.Domain.Commands.FoodTableAggregate
{
    public class RegisterFoodTableCommand : BaseFoodTableCommand
    {
        public override bool Validate()
        {
            ValidationResult = new RegisterFoodTableCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
