using FluentValidation;
using NutrientAuto.Community.Domain.Commands.MealAggregate.BaseCommand;

namespace NutrientAuto.Community.Domain.CommandValidators.MealAggregate.BaseCommandValidator
{
    public abstract class BaseMealCommandValidator<TCommand> : AbstractValidator<TCommand>
        where TCommand : BaseMealCommand
    {
        public void ValidateMealId()
        {
            RuleFor(command => command.MealId)
                .NotEmpty();
        }
    }
}
