using FluentValidation;
using NutrientAuto.Shared.ValueObjects;

namespace NutrientAuto.Shared.ValueObjectValidators
{
    public class EmailAddressValidator : AbstractValidator<EmailAddress>
    {
        public EmailAddressValidator()
        {
            RuleFor(email => email.Email)
                .EmailAddress()
                .Length(10, 150);
        }
    }
}
