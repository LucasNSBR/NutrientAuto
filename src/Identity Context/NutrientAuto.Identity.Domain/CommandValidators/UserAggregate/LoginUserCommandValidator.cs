using FluentValidation;
using NutrientAuto.Identity.Domain.Commands.UserAggregate;

namespace NutrientAuto.Identity.Domain.CommandValidators.UserAggregate
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(command => command.Email)
                .EmailAddress()
                .Length(10, 150);

            RuleFor(command => command.Password)
                .NotEmpty()
                .Length(8, 24);
        }
    }
}
