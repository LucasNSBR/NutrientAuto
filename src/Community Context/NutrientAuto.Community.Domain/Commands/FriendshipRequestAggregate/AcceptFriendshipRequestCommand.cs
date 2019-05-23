using NutrientAuto.Community.Domain.Commands.FriendshipRequestAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.FriendshipRequestAggregate;

namespace NutrientAuto.Community.Domain.Commands.FriendshipRequestAggregate
{
    public class AcceptFriendshipRequestCommand : BaseFriendshipRequestCommand
    {
        public override bool Validate()
        {
            ValidationResult = new AcceptFriendshipRequestCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
