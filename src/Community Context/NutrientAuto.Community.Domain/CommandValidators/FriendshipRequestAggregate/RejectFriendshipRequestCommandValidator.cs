using NutrientAuto.Community.Domain.Commands.FriendshipRequestAggregate;
using NutrientAuto.Community.Domain.CommandValidators.FriendshipRequestAggregate.BaseCommandValidator;

namespace NutrientAuto.Community.Domain.CommandValidators.FriendshipRequestAggregate
{
    public class RejectFriendshipRequestCommandValidator : BaseFriendshipRequestCommandValidator<RejectFriendshipRequestCommand>
    {
        public RejectFriendshipRequestCommandValidator()
        {
            ValidateFriendshipRequestId();
        }
    }
}
