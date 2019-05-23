using NutrientAuto.Community.Domain.Commands.FoodTableAggregate;
using NutrientAuto.Community.Domain.CommandValidators.FoodTableAggregate.BaseCommandValidator;

namespace NutrientAuto.Community.Domain.CommandValidators.FoodTableAggregate
{
    public class UpdateFoodTableCommandValidator : BaseFoodTableCommandValidator<UpdateFoodTableCommand>
    {
        public UpdateFoodTableCommandValidator()
        {
            ValidateFoodTableId();
            ValidateName();
            ValidateDescription();
        }
    }
}
