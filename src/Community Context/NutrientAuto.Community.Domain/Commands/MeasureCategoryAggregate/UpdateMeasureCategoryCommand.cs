using NutrientAuto.Community.Domain.Commands.MeasureCategoryAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.MeasureCategoryAggregate;

namespace NutrientAuto.Community.Domain.Commands.MeasureCategoryAggregate
{
    public class UpdateMeasureCategoryCommand : BaseMeasureCategoryCommand
    {
        public override bool Validate()
        {
            ValidationResult = new UpdateMeasureCategoryCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
