using FluentValidation;
using NutrientAuto.Community.Domain.Commands.ProfileAggregate;
using NutrientAuto.Community.Domain.CommandValidators.ProfileAggregate.BaseCommandValidator;

namespace NutrientAuto.Community.Domain.CommandValidators.ProfileAggregate
{
    public class ChangeSettingsCommandValidator : BaseProfileCommandValidator<ChangeSettingsCommand>
    {
        public ChangeSettingsCommandValidator()
        {
            RuleFor(command => command.PrivacyType)
                .IsInEnum();

            ValidateProfileId();
        }
    }
}
