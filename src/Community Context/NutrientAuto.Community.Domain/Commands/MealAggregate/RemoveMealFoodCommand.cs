using NutrientAuto.Community.Domain.Commands.MealAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.MealAggregate;
using System;

namespace NutrientAuto.Community.Domain.Commands.MealAggregate
{
    public class RemoveMealFoodCommand : BaseMealCommand
    {
        public Guid MealFoodId { get; set; }

        public override bool Validate()
        {
            ValidationResult = new RemoveMealFoodCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
