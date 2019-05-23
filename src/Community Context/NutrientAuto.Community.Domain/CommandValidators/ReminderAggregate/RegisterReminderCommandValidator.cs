using NutrientAuto.Community.Domain.Commands.ReminderAggregate;
using NutrientAuto.Community.Domain.CommandValidators.ReminderAggregate.BaseCommandValidator;

namespace NutrientAuto.Community.Domain.CommandValidators.ReminderAggregate
{
    public class RegisterReminderCommandValidator : BaseReminderCommandValidator<RegisterReminderCommand>
    {
        public RegisterReminderCommandValidator()
        {
            ValidateTitle();
            ValidateDetails();
            ValidateTimeOfDay();
        }
    }
}
