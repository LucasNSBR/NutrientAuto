using NutrientAuto.Community.Domain.Commands.FoodAggregate;
using NutrientAuto.Community.Domain.CommandValidators.FoodAggregate.BaseCommand;

namespace NutrientAuto.Community.Domain.CommandValidators.FoodAggregate
{
    public class UpdateFoodCommandValidator : BaseFoodCommandValidator<UpdateFoodCommand>
    {
        public UpdateFoodCommandValidator()
        {
            ValidateFoodId();
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
