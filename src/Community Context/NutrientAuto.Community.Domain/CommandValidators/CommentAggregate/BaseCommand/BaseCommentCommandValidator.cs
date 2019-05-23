using FluentValidation;
using NutrientAuto.Community.Domain.Commands.CommentAggregate.BaseCommand;

namespace NutrientAuto.Community.Domain.CommandValidators.CommentAggregate.BaseCommand
{
    public abstract class BaseCommentCommandValidator<TCommand> : AbstractValidator<TCommand>
        where TCommand : BaseCommentCommand
    {
        public void ValidateCommentId()
        {
            RuleFor(command => command.CommentId)
                .NotEmpty();
        }

        public void ValidatePostId()
        {
            RuleFor(command => command.PostId)
                .NotEmpty();
        }

        public void ValidateBody()
        {
            RuleFor(command => command.Body)
                .Length(1, 150);
        }
    }
}
