using NutrientAuto.Community.Domain.Commands.DietAggregate;
using NutrientAuto.Community.Domain.CommandValidators.DietAggregate.BaseCommandValidator;

namespace NutrientAuto.Community.Domain.CommandValidators.DietAggregate
{
    public class UpdateDietCommandValidator : BaseDietCommandValidator<UpdateDietCommand>
    {
        public UpdateDietCommandValidator()
        {
            ValidateDietId();
            ValidateName();
            ValidateDescription();
        }
    }
}
