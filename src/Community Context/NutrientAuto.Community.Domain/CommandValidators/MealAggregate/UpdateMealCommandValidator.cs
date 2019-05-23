using FluentValidation;
using NutrientAuto.Community.Domain.Commands.MealAggregate;
using NutrientAuto.Community.Domain.CommandValidators.MealAggregate.BaseCommandValidator;
using NutrientAuto.Community.Domain.CommandValidators.SeedWork;

namespace NutrientAuto.Community.Domain.CommandValidators.MealAggregate
{
    public class UpdateMealCommandValidator : BaseMealCommandValidator<UpdateMealCommand>
    {
        public UpdateMealCommandValidator()
        {
            ValidateMealId();

            RuleFor(command => command.Name)
                .Length(5, 100);

            RuleFor(command => command.Description)
                .MaximumLength(250);

            RuleFor(command => command.TimeOfDay)
                .NotEmpty()
                .SetValidator(new TimeDtoValidator());
        }
    }
}
