using NutrientAuto.Identity.Domain.CommandValidators.UserAggregate;
using NutrientAuto.Shared.Commands;

namespace NutrientAuto.Identity.Domain.Commands.UserAggregate
{
    public class ResetUserPasswordCommand : Command
    {
        public string Email { get; set; }
        public string ResetToken { get; set; }
        public string NewPassword { get; set; }

        public override bool Validate()
        {
            ValidationResult = new ResetUserPasswordCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
