using FluentValidation;
using NutrientAuto.Community.Domain.Commands.PostAggregate;

namespace NutrientAuto.Community.Domain.CommandValidators.PostAggregate
{
    public class RemoveCommentCommandValidator : AbstractValidator<RemoveCommentCommand>
    {
        public RemoveCommentCommandValidator()
        {
            RuleFor(command => command.CommentId)
                .NotEmpty();

            RuleFor(command => command.PostId)
                .NotEmpty();
        }
    }
}
