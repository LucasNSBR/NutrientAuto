using FluentValidation;
using NutrientAuto.Identity.Domain.Commands.UserAggregate;

namespace NutrientAuto.Identity.Domain.CommandValidators.UserAggregate
{
    public class ResetUserPasswordCommandValidator : AbstractValidator<ResetUserPasswordCommand>
    {
        public ResetUserPasswordCommandValidator()
        {
            RuleFor(command => command.Email)
                .EmailAddress()
                .Length(10, 150);

            RuleFor(command => command.ResetToken)
                .NotEmpty()
                .MaximumLength(2000);

            RuleFor(command => command.NewPassword)
                .Length(8, 24);
        }
    }
}
