using NutrientAuto.Community.Domain.Commands.FoodAggregate;
using NutrientAuto.Community.Domain.CommandValidators.FoodAggregate.BaseCommand;

namespace NutrientAuto.Community.Domain.CommandValidators.FoodAggregate
{
    public class RemoveFoodCommandValidator : BaseFoodCommandValidator<RemoveFoodCommand>
    {
        public RemoveFoodCommandValidator()
        {
            ValidateFoodId();
        }
    }
}
