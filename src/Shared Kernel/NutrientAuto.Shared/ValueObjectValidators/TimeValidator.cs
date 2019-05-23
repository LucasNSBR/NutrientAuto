using FluentValidation;
using NutrientAuto.Shared.ValueObjects;

namespace NutrientAuto.Shared.ValueObjectValidators
{
    public class TimeValidator : AbstractValidator<Time>
    {
        public TimeValidator()
        {
            RuleFor(time => time.Hour)
                .InclusiveBetween(0, 23);

            RuleFor(time => time.Hour)
                .InclusiveBetween(0, 59);

            RuleFor(time => time.Hour)
                .InclusiveBetween(0, 59);
        }
    }
}
