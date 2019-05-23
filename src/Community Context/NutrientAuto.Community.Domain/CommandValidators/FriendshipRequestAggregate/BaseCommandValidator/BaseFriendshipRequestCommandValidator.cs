using FluentValidation;
using NutrientAuto.Community.Domain.Commands.FriendshipRequestAggregate.BaseCommand;

namespace NutrientAuto.Community.Domain.CommandValidators.FriendshipRequestAggregate.BaseCommandValidator
{
    public abstract class BaseFriendshipRequestCommandValidator<TCommand> : AbstractValidator<TCommand>
        where TCommand : BaseFriendshipRequestCommand
    {
        public void ValidateFriendshipRequestId()
        {
            RuleFor(command => command.FriendshipRequestId)
                .NotEmpty();
        }

        public void ValidateRequesterId()
        {
            RuleFor(command => command.RequesterId)
                .NotEmpty();
        }

        public void ValidateRequestedId()
        {
            RuleFor(command => command.RequestedId)
                .NotEmpty();
        }
    }
}
