using NutrientAuto.Community.Domain.Commands.GoalAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.GoalAggregate;

namespace NutrientAuto.Community.Domain.Commands.GoalAggregate
{
    public class SetUncompletedGoalCommand : BaseGoalCommand
    {
        public override bool Validate()
        {
            ValidationResult = new SetUncompletedGoalCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
