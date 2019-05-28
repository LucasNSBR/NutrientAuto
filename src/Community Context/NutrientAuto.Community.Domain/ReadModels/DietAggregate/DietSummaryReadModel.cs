using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using NutrientAuto.Community.Domain.ReadModels.MealAggregate;
using System;
using System.Collections.Generic;

namespace NutrientAuto.Community.Domain.ReadModels.DietAggregate
{
    public class DietSummaryReadModel
    {
        public Guid Id { get; set; }
        public Guid ProfileId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }

        public MacronutrientTable TotalMacronutrients { get; set; }

        public List<MealListReadModel> Meals { get; set; }

        public int MealCount { get; set; }
    }
}
