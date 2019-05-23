using FluentValidation;
using NutrientAuto.Community.Domain.Commands.MeasureAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.SeedWork;

namespace NutrientAuto.Community.Domain.CommandValidators.MeasureAggregate.BaseCommandValidator
{
    public abstract class BaseMeasureCommandValidator<TCommand> : AbstractValidator<TCommand>
        where TCommand : BaseMeasureCommand
    {
        public void ValidateMeasureId()
        {
            RuleFor(command => command.MeasureId)
                .NotEmpty();
        }

        public void ValidateTitle()
        {
            RuleFor(command => command.Title)
                .Length(3, 100);
        }

        public void ValidateDetails()
        {
            RuleFor(command => command.Details)
                .MaximumLength(250);
        }

        public void ValidateHeight()
        {
            RuleFor(command => command.Height)
                .GreaterThan(0);
        }

        public void ValidateWeight()
        {
            RuleFor(command => command.Weight)
                .GreaterThan(0)
                .LessThan(500);
        }

        public void ValidateMeasureDate()
        {
            RuleFor(command => command.MeasureDate)
                .NotEmpty();
        }

        public void ValidateMeasureLines()
        {
            RuleForEach(command => command.MeasureLines)
                .SetValidator(new MeasureLineDtoValidator());
        }
    }
}
