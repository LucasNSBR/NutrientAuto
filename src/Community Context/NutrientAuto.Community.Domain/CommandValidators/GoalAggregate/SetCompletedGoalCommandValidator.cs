using FluentValidation;
using NutrientAuto.Community.Domain.Commands.GoalAggregate;
using NutrientAuto.Community.Domain.CommandValidators.GoalAggregate.BaseCommandValidator;

namespace NutrientAuto.Community.Domain.CommandValidators.GoalAggregate
{
    public class SetCompletedGoalCommandValidator : BaseGoalCommandValidator<SetCompletedGoalCommand>
    {
        public SetCompletedGoalCommandValidator()
        {
            ValidateGoalId();

            RuleFor(command => command.DateCompleted)
                .NotEmpty();

            RuleFor(command => command.AccomplishmentDetails)
                .MaximumLength(500);
        }
    }
}
