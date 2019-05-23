using NutrientAuto.Community.Domain.Commands.MeasureCategoryAggregate;
using NutrientAuto.Community.Domain.CommandValidators.MeasureCategoryAggregate.BaseCommandValidator;

namespace NutrientAuto.Community.Domain.CommandValidators.MeasureCategoryAggregate
{
    public class UpdateMeasureCategoryCommandValidator : BaseMeasureCategoryCommandValidator<UpdateMeasureCategoryCommand>
    {
        public UpdateMeasureCategoryCommandValidator()
        {
            ValidateMeasureCategoryId();
            ValidateName();
            ValidateDescription();
        }
    }
}
