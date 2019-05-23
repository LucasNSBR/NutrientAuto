using FluentValidation;
using NutrientAuto.Community.Domain.Commands.PostAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.SeedWork;
using NutrientAuto.Shared.Constants;
using System.Text.RegularExpressions;

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
                .Length(3, 100);
        }

        public void ValidateBody()
        {
            RuleFor(command => command.Body)
                .Length(3, 250);
        }
    }
}
