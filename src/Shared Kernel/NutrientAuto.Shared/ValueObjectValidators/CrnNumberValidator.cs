using FluentValidation;
using NutrientAuto.Shared.ValueObjects;

namespace NutrientAuto.Shared.ValueObjectValidators
{
    public class CrnNumberValidator : AbstractValidator<CrnNumber>
    {
        public CrnNumberValidator()
        {
            RuleFor(crnNumber => crnNumber.Number)
                .Length(2, 12);

            RuleFor(crnNumber => crnNumber.Region)
                .IsInEnum();
        }
    }
}
