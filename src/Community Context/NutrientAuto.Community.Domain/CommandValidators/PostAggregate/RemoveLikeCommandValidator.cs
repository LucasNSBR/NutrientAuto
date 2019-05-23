using NutrientAuto.Community.Domain.Commands.PostAggregate;
using NutrientAuto.Community.Domain.CommandValidators.PostAggregate.BaseCommandValidator;

namespace NutrientAuto.Community.Domain.CommandValidators.PostAggregate
{
    public class RemoveLikeCommandValidator : BasePostCommandValidator<RemoveLikeCommand>
    {
        public RemoveLikeCommandValidator()
        {
            ValidatePostId();
        }
    }
}
