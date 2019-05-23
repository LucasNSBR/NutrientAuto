using MediatR;
using NutrientAuto.Shared.Commands;
using System;

namespace NutrientAuto.Community.Domain.Commands.CommentAggregate.BaseCommand
{
    public abstract class BaseCommentCommand : Command, IRequest<CommandResult>
    {
        public Guid CommentId { get; set; }
        public Guid PostId { get; set; }
        public string Body { get; set; }
    }
}
