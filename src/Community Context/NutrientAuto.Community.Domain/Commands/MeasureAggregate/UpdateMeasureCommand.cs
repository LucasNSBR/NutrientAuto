using NutrientAuto.Community.Domain.Commands.MeasureAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.MeasureAggregate;

namespace NutrientAuto.Community.Domain.Commands.MeasureAggregate
{
    public class UpdateMeasureCommand : BaseMeasureCommand
    {
        public override bool Validate()
        {
            ValidationResult = new UpdateMeasureCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
