using NutrientAuto.Community.Domain.Commands.GoalAggregate;
using NutrientAuto.Community.Domain.CommandValidators.GoalAggregate.BaseCommandValidator;

namespace NutrientAuto.Community.Domain.CommandValidators.GoalAggregate
{
    public class UpdateGoalCommandValidator : BaseGoalCommandValidator<UpdateGoalCommand>
    {
        public UpdateGoalCommandValidator()
        {
            ValidateGoalId();
            ValidateTitle();
            ValidateDetails();
        }
    }
}
