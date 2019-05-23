using NutrientAuto.Identity.Domain.Commands.UserAggregate;
using NutrientAuto.Identity.Domain.CommandValidators.UserAggregate.BaseCommandValidator;
using NutrientAuto.Shared.ValueObjectValidators;

namespace NutrientAuto.Identity.Domain.CommandValidators.UserAggregate
{
    public class RegisterAsNutritionistCommandValidator : RegisterUserBaseCommandValidator<RegisterAsNutritionistCommand>
    {
        public RegisterAsNutritionistCommandValidator()
        {
            ValidateName();
            ValidateEmail();
            ValidatePassword();
            ValidateBirthDate();
            
            RuleFor(command => command.CrnNumber)
                .SetValidator(new CrnNumberValidator());
        }
    }
}
