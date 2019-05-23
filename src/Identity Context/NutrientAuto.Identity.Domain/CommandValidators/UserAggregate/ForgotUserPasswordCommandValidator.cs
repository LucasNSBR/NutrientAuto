using FluentValidation;
using NutrientAuto.Identity.Domain.Commands.UserAggregate;

namespace NutrientAuto.Identity.Domain.CommandValidators.UserAggregate
{
    public class ForgotUserPasswordCommandValidator : AbstractValidator<ForgotUserPasswordCommand>
    {
        public ForgotUserPasswordCommandValidator()
        {
            RuleFor(command => command.Email)
                .EmailAddress()
                .Length(10, 150);
        }
    }
}
