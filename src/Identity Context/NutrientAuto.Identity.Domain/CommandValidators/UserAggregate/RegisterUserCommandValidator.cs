using NutrientAuto.Identity.Domain.Commands.UserAggregate;
using NutrientAuto.Identity.Domain.CommandValidators.UserAggregate.BaseCommandValidator;

namespace NutrientAuto.Identity.Domain.CommandValidators.UserAggregate
{
    public class RegisterUserCommandValidator : RegisterUserBaseCommandValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            ValidateName();
            ValidateUsername();
            ValidateEmail();
            ValidatePassword();
            ValidateBirthDate();
        }
    }
}
