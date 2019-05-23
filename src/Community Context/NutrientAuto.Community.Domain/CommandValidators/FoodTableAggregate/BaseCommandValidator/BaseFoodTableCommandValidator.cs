using FluentValidation;
using NutrientAuto.Community.Domain.Commands.FoodTableAggregate.BaseCommand;

namespace NutrientAuto.Community.Domain.CommandValidators.FoodTableAggregate.BaseCommandValidator
{
    public abstract class BaseFoodTableCommandValidator<TCommand> : AbstractValidator<TCommand>
        where TCommand : BaseFoodTableCommand
    {
        public void ValidateFoodTableId()
        {
            RuleFor(command => command.FoodTableId)
                .NotEmpty();
        }

        public void ValidateName()
        {
            RuleFor(command => command.Name)
                .NotEmpty()
                .Length(3, 100);
        }

        public void ValidateDescription()
        {
            RuleFor(command => command.Description)
                .NotEmpty()
                .MaximumLength(250);
        }
    }
}
