using NutrientAuto.Community.Domain.Commands.GoalAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.GoalAggregate;
using System;

namespace NutrientAuto.Community.Domain.Commands.GoalAggregate
{
    public class SetCompletedGoalCommand : BaseGoalCommand
    {
        public DateTime DateCompleted { get; set; }
        public string AccomplishmentDetails { get; set; }

        public override bool Validate()
        {
            ValidationResult = new SetCompletedGoalCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
