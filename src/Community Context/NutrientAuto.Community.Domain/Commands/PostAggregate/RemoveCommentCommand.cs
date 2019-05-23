using MediatR;
using NutrientAuto.Community.Domain.CommandValidators.PostAggregate;
using NutrientAuto.Shared.Commands;
using System;

namespace NutrientAuto.Community.Domain.Commands.PostAggregate
{
    public class RemoveCommentCommand : Command, IRequest<CommandResult>
    {
        public Guid CommentId { get; set; }
        public Guid PostId { get; set; }
        
        public override bool Validate()
        {
            ValidationResult = new RemoveCommentCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
