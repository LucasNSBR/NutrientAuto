using NutrientAuto.Identity.Domain.CommandValidators.UserAggregate;
using NutrientAuto.Shared.Commands;

namespace NutrientAuto.Identity.Domain.Commands.UserAggregate
{
    public class LoginUserCommand : Command
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public override bool Validate()
        {
            ValidationResult = new LoginUserCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
