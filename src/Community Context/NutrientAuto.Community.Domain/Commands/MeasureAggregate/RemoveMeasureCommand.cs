using NutrientAuto.Community.Domain.Commands.MeasureAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.MeasureAggregate;

namespace NutrientAuto.Community.Domain.Commands.MeasureAggregate
{
    public class RemoveMeasureCommand : BaseMeasureCommand
    {
        public override bool Validate()
        {
            ValidationResult = new RemoveMeasureCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
