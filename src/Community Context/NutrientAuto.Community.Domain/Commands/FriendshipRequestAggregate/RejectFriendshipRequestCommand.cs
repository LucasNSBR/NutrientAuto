using NutrientAuto.Community.Domain.Commands.FriendshipRequestAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.FriendshipRequestAggregate;

namespace NutrientAuto.Community.Domain.Commands.FriendshipRequestAggregate
{
    public class RejectFriendshipRequestCommand : BaseFriendshipRequestCommand
    {
        public override bool Validate()
        {
            ValidationResult = new RejectFriendshipRequestCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
