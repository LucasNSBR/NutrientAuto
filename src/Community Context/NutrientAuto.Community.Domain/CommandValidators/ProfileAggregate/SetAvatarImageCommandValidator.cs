using NutrientAuto.Community.Domain.Commands.ProfileAggregate;
using NutrientAuto.Community.Domain.CommandValidators.ProfileAggregate.BaseCommandValidator;

namespace NutrientAuto.Community.Domain.CommandValidators.ProfileAggregate
{
    public class SetAvatarImageCommandValidator : BaseProfileCommandValidator<SetAvatarImageCommand>
    {
        public SetAvatarImageCommandValidator()
        {
            ValidateProfileId();
        }
    }
}
