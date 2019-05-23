using FluentValidation;
using NutrientAuto.Identity.Domain.Commands.UserAggregate;
using NutrientAuto.Identity.Domain.CommandValidators.UserAggregate.BaseCommandValidator;
using NutrientAuto.Shared.ValueObjectValidators;

namespace NutrientAuto.Identity.Domain.CommandValidators.UserAggregate
{
    public class RegisterAsNutritionistWithCodeCommandValidator : RegisterUserBaseCommandValidator<RegisterAsNutritionistWithCodeCommand>
    {
        public RegisterAsNutritionistWithCodeCommandValidator()
        {
            ValidateName();
            ValidateEmail();
            ValidatePassword();
            ValidateBirthDate();

            RuleFor(command => command.CrnNumber)
                .SetValidator(new CrnNumberValidator());

            RuleFor(command => command.NutritionistCode)
                .NotEmpty();
        }
    }
}
