using NutrientAuto.Community.Domain.Commands.ReminderAggregate;
using NutrientAuto.Community.Domain.CommandValidators.ReminderAggregate.BaseCommandValidator;

namespace NutrientAuto.Community.Domain.CommandValidators.ReminderAggregate
{
    public class RemoveReminderCommandValidator : BaseReminderCommandValidator<RemoveReminderCommand>
    {
        public RemoveReminderCommandValidator()
        {
            ValidateReminderId();
        }
    }
}
