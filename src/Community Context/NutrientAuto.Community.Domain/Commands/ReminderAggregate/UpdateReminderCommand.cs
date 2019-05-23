using NutrientAuto.Community.Domain.Commands.ReminderAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.ReminderAggregate;

namespace NutrientAuto.Community.Domain.Commands.ReminderAggregate
{
    public class UpdateReminderCommand : BaseReminderCommand
    {
        public override bool Validate()
        {
            ValidationResult = new UpdateReminderCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
