using NutrientAuto.Community.Domain.Commands.FoodTableAggregate;
using NutrientAuto.Community.Domain.CommandValidators.FoodTableAggregate.BaseCommandValidator;

namespace NutrientAuto.Community.Domain.CommandValidators.FoodTableAggregate
{
    public class RemoveFoodTableCommandValidator : BaseFoodTableCommandValidator<RemoveFoodTableCommand>
    {
        public RemoveFoodTableCommandValidator()
        {
            ValidateFoodTableId();
        }
    }
}
