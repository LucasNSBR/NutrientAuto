using NutrientAuto.Identity.Domain.CommandValidators.UserAggregate;
using NutrientAuto.Shared.Commands;
using System;

namespace NutrientAuto.Identity.Domain.Commands.UserAggregate
{
    public class ChangeUserPasswordCommand : Command
    {
        public Guid UserId { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }

        public override bool Validate()
        {
            ValidationResult = new ChangeUserPasswordCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
