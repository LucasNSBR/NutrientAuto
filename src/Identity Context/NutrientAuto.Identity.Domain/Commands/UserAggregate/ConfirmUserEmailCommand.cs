using NutrientAuto.Identity.Domain.CommandValidators.UserAggregate;
using NutrientAuto.Shared.Commands;

namespace NutrientAuto.Identity.Domain.Commands.UserAggregate
{
    public class ConfirmUserEmailCommand : Command
    {
        public string Email { get; set; }
        public string ConfirmationToken { get; set; }

        public override bool Validate()
        {
            ValidationResult = new ConfirmUserEmailCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
