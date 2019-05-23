using NutrientAuto.Identity.Domain.CommandValidators.UserAggregate;
using NutrientAuto.Shared.Commands;

namespace NutrientAuto.Identity.Domain.Commands.UserAggregate
{
    public class ForgotUserPasswordCommand : Command
    {
        public string Email { get; set; }

        public override bool Validate()
        {
            ValidationResult = new ForgotUserPasswordCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
