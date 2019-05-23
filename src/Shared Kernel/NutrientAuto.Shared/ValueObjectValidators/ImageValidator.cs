using FluentValidation;
using NutrientAuto.Shared.ValueObjects;

namespace NutrientAuto.Shared.ValueObjectValidators
{
    public class ImageValidator : AbstractValidator<Image>
    {
        public ImageValidator()
        {
            RuleFor(i => i.UrlPath)
                .NotEmpty();
        }
    }
}
