using NutrientAuto.Community.Domain.Commands.PostAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.PostAggregate;

namespace NutrientAuto.Community.Domain.Commands.PostAggregate
{
    public class AddLikeCommand : BasePostCommand
    {
        public override bool Validate()
        {
            ValidationResult = new AddLikeCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
