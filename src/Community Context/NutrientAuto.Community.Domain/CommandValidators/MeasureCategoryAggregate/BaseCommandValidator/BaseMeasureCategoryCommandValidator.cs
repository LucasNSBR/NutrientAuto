using FluentValidation;
using NutrientAuto.Community.Domain.Commands.MeasureCategoryAggregate.BaseCommand;

namespace NutrientAuto.Community.Domain.CommandValidators.MeasureCategoryAggregate.BaseCommandValidator
{
    public abstract class BaseMeasureCategoryCommandValidator<TCommand> : AbstractValidator<TCommand>
        where TCommand : BaseMeasureCategoryCommand
    {
        public void ValidateMeasureCategoryId()
        {
            RuleFor(command => command.MeasureCategoryId)
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
