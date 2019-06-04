using FluentValidation;
using NutrientAuto.Community.Domain.Commands.ReminderAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.SeedWork;

namespace NutrientAuto.Community.Domain.CommandValidators.ReminderAggregate.BaseCommandValidator
{
    public abstract class BaseReminderCommandValidator<TCommand> : AbstractValidator<TCommand>
        where TCommand : BaseReminderCommand
    {
        public void ValidateReminderId()
        {
            RuleFor(command => command.ReminderId)
                .NotEmpty();
        }

        public void ValidateTitle()
        {
            RuleFor(command => command.Title)
                .Length(3, 100);
        }

        public void ValidateDetails()
        {
            RuleFor(command => command.Details)
                .MaximumLength(250);
        }

        public void ValidateTimeOfDay()
        {
            RuleFor(command => command.TimeOfDay)
                .NotNull()
                .SetValidator(new TimeDtoValidator());
        }
    }
}
