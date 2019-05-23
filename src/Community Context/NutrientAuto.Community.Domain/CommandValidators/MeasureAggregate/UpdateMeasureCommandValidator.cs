using NutrientAuto.Community.Domain.Commands.MeasureAggregate;
using NutrientAuto.Community.Domain.CommandValidators.MeasureAggregate.BaseCommandValidator;

namespace NutrientAuto.Community.Domain.CommandValidators.MeasureAggregate
{
    public class UpdateMeasureCommandValidator : BaseMeasureCommandValidator<UpdateMeasureCommand>
    {
        public UpdateMeasureCommandValidator()
        {
            ValidateMeasureId();
            ValidateTitle();
            ValidateDetails();
            ValidateHeight();
            ValidateWeight();
            ValidateMeasureDate();
            ValidateMeasureLines();
        }
    }
}
