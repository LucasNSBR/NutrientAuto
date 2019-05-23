using NutrientAuto.Community.Domain.Commands.FoodAggregate;
using NutrientAuto.Community.Domain.CommandValidators.FoodAggregate.BaseCommand;

namespace NutrientAuto.Community.Domain.CommandValidators.FoodAggregate
{
    public class RegisterFoodCommandValidator : BaseFoodCommandValidator<RegisterFoodCommand>
    {
        public RegisterFoodCommandValidator()
        {
            ValidateName();
            ValidateDescription();
            ValidateFoodTableId();
            ValidateMacronutrients();
            ValidateMicronutrients();
            ValidateUnitType();
            ValidateDefaultGramsQuantityMultiplier();
        }
    }
}
