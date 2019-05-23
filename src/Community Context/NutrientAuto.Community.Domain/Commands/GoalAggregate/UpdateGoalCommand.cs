using NutrientAuto.Community.Domain.Commands.GoalAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.GoalAggregate;

namespace NutrientAuto.Community.Domain.Commands.GoalAggregate
{
    public class UpdateGoalCommand : BaseGoalCommand
    {
        public override bool Validate()
        {
            ValidationResult = new UpdateGoalCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
