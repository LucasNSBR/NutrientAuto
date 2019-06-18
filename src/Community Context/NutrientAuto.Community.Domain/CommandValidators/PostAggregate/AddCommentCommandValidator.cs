using FluentValidation;
using NutrientAuto.Community.Domain.Commands.PostAggregate;

namespace NutrientAuto.Community.Domain.CommandValidators.PostAggregate
{
    public class AddCommentCommandValidator : AbstractValidator<AddCommentCommand>
    {
        public AddCommentCommandValidator()
        {
            RuleFor(command => command.PostId)
                .NotEmpty();

            RuleFor(command => command.Body)
                .NotEmpty()
                .Length(1, 150);
        }
    }
}
