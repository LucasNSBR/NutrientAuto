using NutrientAuto.Community.Domain.Commands.ProfileAggregate;
using NutrientAuto.Community.Domain.CommandValidators.ProfileAggregate.BaseCommandValidator;

namespace NutrientAuto.Community.Domain.CommandValidators.ProfileAggregate
{
    public class UpdateProfileCommandValidator : BaseProfileCommandValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {
            ValidateProfileId();
            ValidateGenre();
            ValidateName();
            ValidateUsername();
            ValidateBirthDate();
            ValidateBio();
        }
    }
}
