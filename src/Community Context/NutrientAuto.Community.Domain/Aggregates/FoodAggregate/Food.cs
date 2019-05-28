using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using NutrientAuto.Shared.Entities;
using System;

namespace NutrientAuto.Community.Domain.Aggregates.FoodAggregate
{
    public class Food : Entity<Food>, IAggregateRoot
    {
        public FoodType FoodType { get; protected set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Guid FoodTableId { get; private set; }

        public MacronutrientTable Macronutrients { get; private set; }
        public MicronutrientTable Micronutrients { get; private set; }
        public FoodUnit FoodUnit { get; private set; }

        protected Food()
        {
        }

        public Food(string name, string description, Guid foodTableId, MacronutrientTable macronutrients, MicronutrientTable micronutrients, FoodUnit foodUnit)
        {
            FoodType = FoodType.Default;
            Name = name;
            Description = description;
            FoodTableId = foodTableId;
            Macronutrients = macronutrients;
            Micronutrients = micronutrients ?? new MicronutrientTable();
            FoodUnit = foodUnit;
        }

        public void Update(string name, string description, Guid foodTableId, MacronutrientTable macronutrients, MicronutrientTable micronutrients, FoodUnit foodUnit)
        {
            Name = name;
            Description = description;
            FoodTableId = foodTableId;
            Macronutrients = macronutrients;
            Micronutrients = micronutrients ?? new MicronutrientTable();
            FoodUnit = foodUnit;
        }
    }
}
