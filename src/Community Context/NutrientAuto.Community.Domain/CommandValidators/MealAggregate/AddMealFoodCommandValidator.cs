using FluentValidation;
using NutrientAuto.Community.Domain.Commands.MealAggregate;
using NutrientAuto.Community.Domain.CommandValidators.MealAggregate.BaseCommandValidator;

namespace NutrientAuto.Community.Domain.CommandValidators.MealAggregate
{
    public class AddMealFoodCommandValidator : BaseMealCommandValidator<AddMealFoodCommand>
    {
        public AddMealFoodCommandValidator()
        {
            ValidateMealId();

            RuleFor(command => command.FoodId)
                .NotEmpty();

            RuleFor(command => command.Quantity)
                .GreaterThan(0);
        }
    }
}
