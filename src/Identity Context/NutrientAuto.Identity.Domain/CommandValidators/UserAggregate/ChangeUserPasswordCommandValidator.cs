using FluentValidation;
using NutrientAuto.Identity.Domain.Commands.UserAggregate;

namespace NutrientAuto.Identity.Domain.CommandValidators.UserAggregate
{
    public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangeUserPasswordCommandValidator()
        {
            RuleFor(command => command.UserId)
                .NotEmpty();

            RuleFor(command => command.Password)
                .Length(8, 24);

            RuleFor(command => command.NewPassword)
                .Length(8, 24);
        }
    }
}
