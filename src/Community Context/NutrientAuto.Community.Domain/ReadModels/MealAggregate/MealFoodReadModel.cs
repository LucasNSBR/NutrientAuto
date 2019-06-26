using NutrientAuto.Community.Domain.Aggregates.FoodAggregate;
using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using System;

namespace NutrientAuto.Community.Domain.ReadModels.MealAggregate
{
    public class MealFoodReadModel
    {
        public Guid FoodId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public FoodUnit FoodUnit { get; set; }
        public string Quantity { get; set; }
        public MacronutrientTable Macronutrients { get; set; }
    }
}
