using FluentValidation;
using NutrientAuto.Identity.Domain.Commands.UserAggregate;

namespace NutrientAuto.Identity.Domain.CommandValidators.UserAggregate
{
    public class ConfirmUserEmailCommandValidator : AbstractValidator<ConfirmUserEmailCommand>
    {
        public ConfirmUserEmailCommandValidator()
        {
            RuleFor(command => command.Email)
                .EmailAddress()
                .Length(10, 150);

            RuleFor(command => command.ConfirmationToken)
                .NotEmpty()
                .MaximumLength(2000);
        }
    }
}
