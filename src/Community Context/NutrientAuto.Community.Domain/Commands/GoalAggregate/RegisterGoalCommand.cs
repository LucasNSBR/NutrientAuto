using NutrientAuto.Community.Domain.Commands.GoalAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.GoalAggregate;

namespace NutrientAuto.Community.Domain.Commands.GoalAggregate
{
    public class RegisterGoalCommand : BaseGoalCommand
    {
        public override bool Validate()
        {
            ValidationResult = new RegisterGoalCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
