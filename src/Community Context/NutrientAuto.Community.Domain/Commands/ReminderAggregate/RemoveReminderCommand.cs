using NutrientAuto.Community.Domain.Commands.ReminderAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.ReminderAggregate;

namespace NutrientAuto.Community.Domain.Commands.ReminderAggregate
{
    public class RemoveReminderCommand : BaseReminderCommand
    {
        public override bool Validate()
        {
            ValidationResult = new RemoveReminderCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
