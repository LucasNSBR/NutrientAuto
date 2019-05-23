using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using NutrientAuto.Shared.ValueObjects;
using System;

namespace NutrientAuto.Community.Domain.ReadModels.MealAggregate
{
    public class MealListReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Time TimeOfDay { get; set; }
        public MacronutrientTable MealMacronutrients { get; set; }
        public int FoodsCount { get; set; }
    }
}
