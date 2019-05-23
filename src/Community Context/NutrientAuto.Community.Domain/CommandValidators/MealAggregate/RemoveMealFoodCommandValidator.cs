using FluentValidation;
using NutrientAuto.Community.Domain.Commands.MealAggregate;
using NutrientAuto.Community.Domain.CommandValidators.MealAggregate.BaseCommandValidator;

namespace NutrientAuto.Community.Domain.CommandValidators.MealAggregate
{
    public class RemoveMealFoodCommandValidator : BaseMealCommandValidator<RemoveMealFoodCommand>
    {
        public RemoveMealFoodCommandValidator()
        {
            ValidateMealId();

            RuleFor(command => command.MealFoodId)
                .NotEmpty();
        }
    }
}
