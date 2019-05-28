using FluentValidation;
using NutrientAuto.Community.Domain.Commands.DietAggregate;
using NutrientAuto.Community.Domain.CommandValidators.SeedWork;

namespace NutrientAuto.Community.Domain.CommandValidators.DietAggregate
{
    public class AddDietMealCommandValidator : AbstractValidator<AddDietMealCommand>
    {
        public AddDietMealCommandValidator()
        {
            RuleFor(command => command.DietId)
                .NotEmpty();
            
            RuleFor(command => command.Name)
                .Length(3, 100);

            RuleFor(command => command.Description)
                .MaximumLength(250);

            RuleFor(command => command.TimeOfDay)
                .NotNull()
                .SetValidator(new TimeDtoValidator());
        }
    }
}
