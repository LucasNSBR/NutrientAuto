using FluentValidation;
using NutrientAuto.Community.Domain.Commands.DietAggregate.BaseCommand;

namespace NutrientAuto.Community.Domain.CommandValidators.DietAggregate.BaseCommandValidator
{
    public class BaseDietCommandValidator<TCommand> : AbstractValidator<TCommand>
        where TCommand : BaseDietCommand
    {
        public void ValidateDietId()
        {
            RuleFor(command => command.DietId)
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
                .MaximumLength(500);
        }
    }
}
