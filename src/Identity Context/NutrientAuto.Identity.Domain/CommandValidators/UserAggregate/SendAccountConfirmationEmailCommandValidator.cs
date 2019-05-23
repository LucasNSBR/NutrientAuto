using FluentValidation;
using NutrientAuto.Identity.Domain.Commands.UserAggregate;

namespace NutrientAuto.Identity.Domain.CommandValidators.UserAggregate
{
    public class SendAccountConfirmationEmailCommandValidator : AbstractValidator<SendAccountConfirmationEmailCommand>
    {
        public SendAccountConfirmationEmailCommandValidator()
        {
            RuleFor(command => command.Email)
                .EmailAddress()
                .Length(10, 150);
        }
    }
}
