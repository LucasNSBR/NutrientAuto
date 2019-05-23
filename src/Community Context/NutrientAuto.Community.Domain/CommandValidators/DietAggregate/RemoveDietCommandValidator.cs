using NutrientAuto.Community.Domain.Commands.DietAggregate;
using NutrientAuto.Community.Domain.CommandValidators.DietAggregate.BaseCommandValidator;

namespace NutrientAuto.Community.Domain.CommandValidators.DietAggregate
{
    public class RemoveDietCommandValidator : BaseDietCommandValidator<RemoveDietCommand>
    {
        public RemoveDietCommandValidator()
        {
            ValidateDietId();
        }
    }
}
