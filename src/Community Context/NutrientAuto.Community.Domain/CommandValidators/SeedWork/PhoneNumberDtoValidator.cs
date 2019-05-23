using FluentValidation;
using NutrientAuto.Community.Domain.Commands.SeedWork;

namespace NutrientAuto.Community.Domain.CommandValidators.SeedWork
{
    public class PhoneNumberDtoValidator : AbstractValidator<PhoneNumberDto>
    {
        public PhoneNumberDtoValidator()
        {
            RuleFor(pn => pn.Number)
                .Matches("^[0-9]*$")
                .Length(10, 12);
        }
    }
}
