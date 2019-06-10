using FluentValidation;
using NutrientAuto.Community.Domain.Commands.SeedWork;

namespace NutrientAuto.Community.Domain.CommandValidators.SeedWork
{
    public class ImageDtoValidator : AbstractValidator<ImageDto>
    {
        public ImageDtoValidator()
        {
            RuleFor(command => command.ImageName)
                .NotEmpty();

            RuleFor(command => command.UrlPath)
                .NotEmpty();
        }
    }
}
