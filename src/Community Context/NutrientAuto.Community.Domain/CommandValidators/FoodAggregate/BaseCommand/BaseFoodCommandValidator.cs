using FluentValidation;
using NutrientAuto.Community.Domain.Commands.FoodAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.SeedWork;

namespace NutrientAuto.Community.Domain.CommandValidators.FoodAggregate.BaseCommand
{
    public abstract class BaseFoodCommandValidator<TCommand> : AbstractValidator<TCommand>
        where TCommand : BaseFoodCommand
    {
        public void ValidateFoodId()
        {
            RuleFor(command => command.FoodId)
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

        public void ValidateFoodTableId()
        {
            RuleFor(command => command.FoodTableId)
                .NotEmpty();
        }

        public void ValidateMacronutrients()
        {
            RuleFor(command => command.Macronutrients)
                .NotNull()
                .SetValidator(new MacronutrientTableDtoValidator());
        }

        public void ValidateMicronutrients()
        {
            RuleFor(command => command.Micronutrients)
                .SetValidator(new MicronutrientTableDtoValidator());
        }

        public void ValidateUnitType()
        {
            RuleFor(command => command.UnitType)
                .IsInEnum();
        }

        public void ValidateDefaultGramsQuantityMultiplier()
        {
            RuleFor(command => command.DefaultGramsQuantityMultiplier)
                .GreaterThanOrEqualTo(0);
        }
    }
}
