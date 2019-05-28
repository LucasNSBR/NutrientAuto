using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using System;

namespace NutrientAuto.Community.Domain.ReadModels.DietAggregate
{
    public class DietListReadModel
    {
        public Guid Id { get; set; }
        public Guid ProfileId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public MacronutrientTable TotalMacronutrients { get; set; }
    }
}
