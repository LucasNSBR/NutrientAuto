using NutrientAuto.Community.Domain.Commands.FriendshipRequestAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.FriendshipRequestAggregate;

namespace NutrientAuto.Community.Domain.Commands.FriendshipRequestAggregate
{
    public class CancelFriendshipRequestCommand : BaseFriendshipRequestCommand
    {
        public override bool Validate()
        {
            ValidationResult = new CancelFriendshipRequestCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
