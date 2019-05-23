using NutrientAuto.Community.Domain.Commands.ReminderAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.ReminderAggregate;

namespace NutrientAuto.Community.Domain.Commands.ReminderAggregate
{
    public class RegisterReminderCommand : BaseReminderCommand
    {
        public override bool Validate()
        {
            ValidationResult = new RegisterReminderCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
