using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using System;

namespace NutrientAuto.Community.Domain.Aggregates.FoodAggregate
{
    public class CustomFood : Food
    {
        public Guid ProfileId { get; private set; }

        protected CustomFood()
        {
        }

        public CustomFood(Guid profileId, string name, string description, Guid foodTableId, MacronutrientTable macronutrients, MicronutrientTable micronutrients, FoodUnit foodUnit)
            : base(name, description, foodTableId, macronutrients, micronutrients, foodUnit)
        {
            FoodType = FoodType.Custom;
            ProfileId = profileId;
        }
    }
}
