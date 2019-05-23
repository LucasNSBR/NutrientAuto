using MediatR;
using NutrientAuto.Shared.Commands;
using System;

namespace NutrientAuto.Community.Domain.Commands.FriendshipRequestAggregate.BaseCommand
{
    public abstract class BaseFriendshipRequestCommand : Command, IRequest<CommandResult>
    {
        public Guid FriendshipRequestId { get; set; }
        public Guid RequestedId { get; set; }
        public Guid RequesterId { get; set; }
    }
}
