using NutrientAuto.Community.Domain.Commands.FriendshipRequestAggregate;
using NutrientAuto.Community.Domain.CommandValidators.FriendshipRequestAggregate.BaseCommandValidator;

namespace NutrientAuto.Community.Domain.CommandValidators.FriendshipRequestAggregate
{
    public class CancelFriendshipRequestCommandValidator : BaseFriendshipRequestCommandValidator<CancelFriendshipRequestCommand>
    {
        public CancelFriendshipRequestCommandValidator()
        {
            ValidateFriendshipRequestId();
        }
    }
}
