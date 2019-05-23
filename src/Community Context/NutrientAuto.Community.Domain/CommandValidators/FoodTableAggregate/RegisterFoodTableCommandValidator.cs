using NutrientAuto.Community.Domain.Commands.FoodTableAggregate;
using NutrientAuto.Community.Domain.CommandValidators.FoodTableAggregate.BaseCommandValidator;

namespace NutrientAuto.Community.Domain.CommandValidators.FoodTableAggregate
{
    public class RegisterFoodTableCommandValidator : BaseFoodTableCommandValidator<RegisterFoodTableCommand>
    {
        public RegisterFoodTableCommandValidator()
        {
            ValidateName();
            ValidateDescription();
        }
    }
}
