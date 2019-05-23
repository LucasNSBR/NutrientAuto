using NutrientAuto.Community.Domain.Commands.MeasureAggregate;
using NutrientAuto.Community.Domain.CommandValidators.MeasureAggregate.BaseCommandValidator;

namespace NutrientAuto.Community.Domain.CommandValidators.MeasureAggregate
{
    public class RemoveMeasureCommandValidator : BaseMeasureCommandValidator<RemoveMeasureCommand>
    {
        public RemoveMeasureCommandValidator()
        {
            ValidateMeasureId();
        }
    }
}
