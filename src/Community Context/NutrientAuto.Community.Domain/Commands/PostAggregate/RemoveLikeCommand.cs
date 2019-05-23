using NutrientAuto.Community.Domain.Commands.PostAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.PostAggregate;

namespace NutrientAuto.Community.Domain.Commands.PostAggregate
{
    public class RemoveLikeCommand : BasePostCommand
    {
        public override bool Validate()
        {
            ValidationResult = new RemoveLikeCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
