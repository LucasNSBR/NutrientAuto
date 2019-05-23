using NutrientAuto.Community.Domain.Commands.CommentAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.CommentAggregate;

namespace NutrientAuto.Community.Domain.Commands.CommentAggregate
{
    public class ReplyCommentCommand : BaseCommentCommand
    {
        public override bool Validate()
        {
            ValidationResult = new ReplyCommentCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
