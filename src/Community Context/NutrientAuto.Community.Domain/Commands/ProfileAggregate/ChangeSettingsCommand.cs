using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;
using NutrientAuto.Community.Domain.Commands.ProfileAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.ProfileAggregate;

namespace NutrientAuto.Community.Domain.Commands.ProfileAggregate
{
    public class ChangeSettingsCommand : BaseProfileCommand
    {
        public PrivacyType PrivacyType { get; set; }

        public override bool Validate()
        {
            ValidationResult = new ChangeSettingsCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
