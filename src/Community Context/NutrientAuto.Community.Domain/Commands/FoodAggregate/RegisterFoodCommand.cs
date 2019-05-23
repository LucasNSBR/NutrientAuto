using NutrientAuto.Community.Domain.Commands.FoodAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.FoodAggregate;

namespace NutrientAuto.Community.Domain.Commands.FoodAggregate
{
    public class RegisterFoodCommand : BaseFoodCommand
    {
        public override bool Validate()
        {
            ValidationResult = new RegisterFoodCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
