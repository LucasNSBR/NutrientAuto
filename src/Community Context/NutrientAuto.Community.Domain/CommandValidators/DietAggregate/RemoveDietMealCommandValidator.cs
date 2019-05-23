using FluentValidation;
using NutrientAuto.Community.Domain.Commands.DietAggregate;

namespace NutrientAuto.Community.Domain.CommandValidators.DietAggregate
{
    public class RemoveDietMealCommandValidator : AbstractValidator<RemoveDietMealCommand>
    {
        public RemoveDietMealCommandValidator()
        {
            RuleFor(command => command.DietId)
                .NotEmpty();

            RuleFor(command => command.DietMealId)
                .NotEmpty();
        }
    }
}
