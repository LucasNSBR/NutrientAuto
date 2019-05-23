using NutrientAuto.Community.Domain.Aggregates.FoodAggregate;
using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using NutrientAuto.Shared.Entities;
using System;

namespace NutrientAuto.Community.Domain.Aggregates.MealAggregate
{
    public class MealFood : Entity<MealFood>
    {
        public Guid FoodId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public FoodUnit FoodUnit { get; private set; }
        public decimal Quantity { get; private set; }
        public MacronutrientTable Macronutrients { get; private set; }

        protected MealFood()
        {
        }

        public MealFood(Guid foodId, string name, string description, MacronutrientTable foodMacronutrients, FoodUnit foodUnit, decimal quantity)
        {
            FoodId = foodId;
            Name = name;
            Description = description;
            FoodUnit = foodUnit;
            Quantity = quantity;

            Macronutrients = CalculateQuantityMacros(foodMacronutrients, foodUnit, quantity);
        }

        private MacronutrientTable CalculateQuantityMacros(MacronutrientTable macronutrient, FoodUnit foodUnit, decimal quantity)
        {
            decimal carbohydrates = (macronutrient.Carbohydrate * quantity) / foodUnit.DefaultGramsQuantityMultiplier;
            decimal protein = (macronutrient.Protein * quantity) / foodUnit.DefaultGramsQuantityMultiplier;
            decimal fat = (macronutrient.Fat * quantity) / foodUnit.DefaultGramsQuantityMultiplier;

            return new MacronutrientTable(carbohydrates, protein, fat);
        }
    }
}