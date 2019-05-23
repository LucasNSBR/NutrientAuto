using NutrientAuto.Community.Domain.Commands.ProfileAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.ProfileAggregate;

namespace NutrientAuto.Community.Domain.Commands.ProfileAggregate
{
    public class UpdateProfileCommand : BaseProfileCommand
    {
        public override bool Validate()
        {
            ValidationResult = new UpdateProfileCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
