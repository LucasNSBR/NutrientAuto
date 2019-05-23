using NutrientAuto.Community.Domain.Commands.GoalAggregate;
using NutrientAuto.Community.Domain.CommandValidators.GoalAggregate.BaseCommandValidator;

namespace NutrientAuto.Community.Domain.CommandValidators.GoalAggregate
{
    public class RegisterGoalCommandValidator : BaseGoalCommandValidator<RegisterGoalCommand>
    {
        public RegisterGoalCommandValidator()
        {
            ValidateTitle();
            ValidateDetails();
        }
    }
}
