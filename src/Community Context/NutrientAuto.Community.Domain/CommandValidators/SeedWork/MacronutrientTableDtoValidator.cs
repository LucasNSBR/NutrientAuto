using FluentValidation;
using NutrientAuto.Community.Domain.Commands.SeedWork;

namespace NutrientAuto.Community.Domain.CommandValidators.SeedWork
{
    public class MacronutrientTableDtoValidator : AbstractValidator<MacronutrientTableDto>
    {
        public MacronutrientTableDtoValidator()
        {
            RuleFor(macronutrientTable => macronutrientTable.Kcal)
                .GreaterThan(0);

            RuleFor(macronutrientTable => macronutrientTable.Protein)
                .GreaterThan(0);

            RuleFor(macronutrientTable => macronutrientTable.Carbohydrate)
                .GreaterThan(0);

            RuleFor(macronutrientTable => macronutrientTable.Fat)
                .GreaterThan(0);
        }
    }
}
