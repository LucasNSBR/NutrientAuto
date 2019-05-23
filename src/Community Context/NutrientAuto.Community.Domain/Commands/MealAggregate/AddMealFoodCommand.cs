using NutrientAuto.Community.Domain.Commands.MealAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.MealAggregate;
using System;

namespace NutrientAuto.Community.Domain.Commands.MealAggregate
{
    public class AddMealFoodCommand : BaseMealCommand
    {
        public Guid FoodId { get; set; }
        public int Quantity { get; set; }

        public override bool Validate()
        {
            ValidationResult = new AddMealFoodCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
