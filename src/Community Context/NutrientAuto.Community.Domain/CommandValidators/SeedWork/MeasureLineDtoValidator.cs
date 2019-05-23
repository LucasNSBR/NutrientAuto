using FluentValidation;
using NutrientAuto.Community.Domain.Commands.SeedWork;

namespace NutrientAuto.Community.Domain.CommandValidators.SeedWork
{
    public class MeasureLineDtoValidator : AbstractValidator<MeasureLineDto>
    {
        public MeasureLineDtoValidator()
        {
            RuleFor(measureLine => measureLine.MeasureCategoryId)
                .NotEmpty();

            RuleFor(measureLine => measureLine.Value)
                .GreaterThan(0);
        }
    }
}
