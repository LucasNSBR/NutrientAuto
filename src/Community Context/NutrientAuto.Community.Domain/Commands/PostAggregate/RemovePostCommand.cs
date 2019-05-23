using NutrientAuto.Community.Domain.Commands.PostAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.PostAggregate;

namespace NutrientAuto.Community.Domain.Commands.PostAggregate
{
    public class RemovePostCommand : BasePostCommand
    {
        public override bool Validate()
        {
            ValidationResult = new RemovePostCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
