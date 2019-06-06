using FluentValidation;
using NutrientAuto.Community.Domain.Commands.FriendshipRequestAggregate;
using NutrientAuto.Community.Domain.CommandValidators.FriendshipRequestAggregate.BaseCommandValidator;

namespace NutrientAuto.Community.Domain.CommandValidators.FriendshipRequestAggregate
{
    public class RegisterFriendshipRequestCommandValidator : BaseFriendshipRequestCommandValidator<RegisterFriendshipRequestCommand>
    {
        public RegisterFriendshipRequestCommandValidator()
        {
            RuleFor(command => command.RequestBody)
                .MaximumLength(100);
            
            ValidateRequestedId();
        }
    }
}
