using NutrientAuto.Community.Domain.Commands.CommentAggregate;
using NutrientAuto.Community.Domain.CommandValidators.CommentAggregate.BaseCommand;

namespace NutrientAuto.Community.Domain.CommandValidators.CommentAggregate
{
    public class ReplyCommentCommandValidator : BaseCommentCommandValidator<ReplyCommentCommand>
    {
        public ReplyCommentCommandValidator()
        {
            ValidateCommentId();
            ValidatePostId();
            ValidateBody();
        }
    }
}
