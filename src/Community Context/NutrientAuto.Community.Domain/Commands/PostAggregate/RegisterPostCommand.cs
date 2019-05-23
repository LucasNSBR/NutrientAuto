using NutrientAuto.Community.Domain.Commands.PostAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.PostAggregate;

namespace NutrientAuto.Community.Domain.Commands.PostAggregate
{
    public class RegisterPostCommand : BasePostCommand
    {
        public override bool Validate()
        {
            ValidationResult = new RegisterPostCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
