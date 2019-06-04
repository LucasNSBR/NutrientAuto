using NutrientAuto.Community.Domain.Aggregates.MealAggregate;
using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.Collections.Generic;

namespace NutrientAuto.Community.Domain.ReadModels.MealAggregate
{
    public class MealSummaryReadModel
    {
        public Guid Id { get; set; }
        public Guid ProfileId { get; set; }
        public Guid DietId { get; set; }
        public string Name { get; set; }
        public Time TimeOfDay { get; set; }
        public MacronutrientTable MealMacronutrients { get; set; }
        public int FoodsCount { get; set; }
        public List<MealFood> Foods { get; set; }
    }
}
