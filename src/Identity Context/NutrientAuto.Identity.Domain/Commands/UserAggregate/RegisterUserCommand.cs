using NutrientAuto.Identity.Domain.Commands.UserAggregate.BaseCommand;
using NutrientAuto.Identity.Domain.CommandValidators.UserAggregate;

namespace NutrientAuto.Identity.Domain.Commands.UserAggregate
{
    public class RegisterUserCommand : RegisterUserBaseCommand
    {
        public override bool Validate()
        {
            ValidationResult = new RegisterUserCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
