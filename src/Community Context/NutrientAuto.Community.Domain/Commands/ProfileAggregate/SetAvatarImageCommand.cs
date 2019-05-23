using NutrientAuto.Community.Domain.Commands.ProfileAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.ProfileAggregate;

namespace NutrientAuto.Community.Domain.Commands.ProfileAggregate
{
    public class SetAvatarImageCommand : BaseProfileCommand
    {
        public override bool Validate()
        {
            ValidationResult = new SetAvatarImageCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
