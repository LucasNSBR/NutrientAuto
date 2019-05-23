using NutrientAuto.Community.Domain.Commands.MeasureAggregate;
using NutrientAuto.Community.Domain.CommandValidators.MeasureAggregate.BaseCommandValidator;

namespace NutrientAuto.Community.Domain.CommandValidators.MeasureAggregate
{
    public class RegisterMeasureCommandValidator : BaseMeasureCommandValidator<RegisterMeasureCommand>
    {
        public RegisterMeasureCommandValidator()
        {
            ValidateTitle();
            ValidateDetails();
            ValidateHeight();
            ValidateWeight();
            ValidateMeasureDate();
            ValidateMeasureLines();
        }
    }
}
