using FluentValidation;
using NutrientAuto.Community.Domain.Commands.PostAggregate.BaseCommand;

namespace NutrientAuto.Community.Domain.CommandValidators.PostAggregate.BaseCommandValidator
{
    public abstract class BasePostCommandValidator<TCommand> : AbstractValidator<TCommand>
        where TCommand : BasePostCommand
    {
        public void ValidatePostId()
        {
            RuleFor(command => command.PostId)
                .NotEmpty();
        }

        public void ValidateTitle()
        {
            RuleFor(command => command.Title)
                .NotEmpty()
                .Length(3, 100);
        }

        public void ValidateBody()
        {
            RuleFor(command => command.Body)
                .NotEmpty()
                .Length(3, 250);
        }
    }
}
