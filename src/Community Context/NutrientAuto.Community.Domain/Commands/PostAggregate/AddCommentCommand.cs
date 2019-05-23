using MediatR;
using NutrientAuto.Community.Domain.CommandValidators.PostAggregate;
using NutrientAuto.Shared.Commands;
using System;

namespace NutrientAuto.Community.Domain.Commands.PostAggregate
{
    public class AddCommentCommand : Command, IRequest<CommandResult>
    {
        public Guid PostId { get; set; }
        public string Body { get; set; }

        public override bool Validate()
        {
            ValidationResult = new AddCommentCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
