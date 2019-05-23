using NutrientAuto.Community.Domain.Commands.FriendshipRequestAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.FriendshipRequestAggregate;

namespace NutrientAuto.Community.Domain.Commands.FriendshipRequestAggregate
{
    public class RegisterFriendshipRequestCommand : BaseFriendshipRequestCommand 
    {
        public string RequestBody { get; set; }

        public override bool Validate()
        {
            ValidationResult = new RegisterFriendshipRequestCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
