using NutrientAuto.Community.Domain.Commands.MeasureCategoryAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.MeasureCategoryAggregate;

namespace NutrientAuto.Community.Domain.Commands.MeasureCategoryAggregate
{
    public class RegisterMeasureCategoryCommand : BaseMeasureCategoryCommand
    {
        public override bool Validate()
        {
            ValidationResult = new RegisterMeasureCategoryCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
