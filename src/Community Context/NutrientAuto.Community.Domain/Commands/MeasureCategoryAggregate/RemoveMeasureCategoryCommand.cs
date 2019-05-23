using NutrientAuto.Community.Domain.Commands.MeasureCategoryAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.MeasureCategoryAggregate;

namespace NutrientAuto.Community.Domain.Commands.MeasureCategoryAggregate
{
    public class RemoveMeasureCategoryCommand : BaseMeasureCategoryCommand
    {
        public override bool Validate()
        {
            ValidationResult = new RemoveMeasureCategoryCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
