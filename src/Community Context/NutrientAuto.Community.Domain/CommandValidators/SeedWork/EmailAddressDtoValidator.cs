using FluentValidation;
using NutrientAuto.Community.Domain.Commands.SeedWork;

namespace NutrientAuto.Community.Domain.CommandValidators.SeedWork
{
    public class EmailAddressDtoValidator : AbstractValidator<EmailAddressDto>
    {
        public EmailAddressDtoValidator()
        {
            RuleFor(email => email.Email)
                .EmailAddress()
                .Length(10, 150);
        }
    }
}
