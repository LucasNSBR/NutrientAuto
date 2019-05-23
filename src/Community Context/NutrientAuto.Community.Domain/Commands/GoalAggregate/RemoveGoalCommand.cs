using NutrientAuto.Community.Domain.Commands.GoalAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.GoalAggregate;

namespace NutrientAuto.Community.Domain.Commands.GoalAggregate
{
    public class RemoveGoalCommand : BaseGoalCommand
    {
        public override bool Validate()
        {
            ValidationResult = new RemoveGoalCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
