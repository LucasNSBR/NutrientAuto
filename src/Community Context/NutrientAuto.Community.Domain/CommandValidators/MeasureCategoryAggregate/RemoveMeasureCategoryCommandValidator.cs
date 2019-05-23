using NutrientAuto.Community.Domain.Commands.MeasureCategoryAggregate;
using NutrientAuto.Community.Domain.CommandValidators.MeasureCategoryAggregate.BaseCommandValidator;

namespace NutrientAuto.Community.Domain.CommandValidators.MeasureCategoryAggregate
{
    public class RemoveMeasureCategoryCommandValidator : BaseMeasureCategoryCommandValidator<RemoveMeasureCategoryCommand>
    {
        public RemoveMeasureCategoryCommandValidator()
        {
            ValidateMeasureCategoryId();
        }
    }
}
