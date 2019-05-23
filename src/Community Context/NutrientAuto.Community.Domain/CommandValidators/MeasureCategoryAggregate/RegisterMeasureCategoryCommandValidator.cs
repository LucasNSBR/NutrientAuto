using NutrientAuto.Community.Domain.Commands.MeasureCategoryAggregate;
using NutrientAuto.Community.Domain.CommandValidators.MeasureCategoryAggregate.BaseCommandValidator;

namespace NutrientAuto.Community.Domain.CommandValidators.MeasureCategoryAggregate
{
    public class RegisterMeasureCategoryCommandValidator : BaseMeasureCategoryCommandValidator<RegisterMeasureCategoryCommand>
    {
        public RegisterMeasureCategoryCommandValidator()
        {
            ValidateName();
            ValidateDescription();
        }
    }
}
