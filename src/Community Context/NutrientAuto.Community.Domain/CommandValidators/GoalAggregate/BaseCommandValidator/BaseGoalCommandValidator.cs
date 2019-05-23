using FluentValidation;
using NutrientAuto.Community.Domain.Commands.GoalAggregate.BaseCommand;

namespace NutrientAuto.Community.Domain.CommandValidators.GoalAggregate.BaseCommandValidator
{
    public abstract class BaseGoalCommandValidator<TCommand> : AbstractValidator<TCommand>
        where TCommand : BaseGoalCommand
    {
        protected void ValidateGoalId()
        {
            RuleFor(command => command.GoalId)
                .NotEmpty();
        }

        protected void ValidateTitle()
        {
            RuleFor(command => command.Title)
                .Length(3, 100);
        }

        protected void ValidateDetails()
        {
            RuleFor(command => command.Details)
                .Length(3, 500);
        }
    }
}
