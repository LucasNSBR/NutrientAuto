using NutrientAuto.Community.Domain.Commands.MeasureAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.MeasureAggregate;

namespace NutrientAuto.Community.Domain.Commands.MeasureAggregate
{
    public class RegisterMeasureCommand : BaseMeasureCommand
    {
        public override bool Validate()
        {
            ValidationResult = new RegisterMeasureCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
