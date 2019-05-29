using NutrientAuto.Shared.ValueObjects;
using System;

namespace NutrientAuto.Community.Domain.ReadModels.DietAggregate
{
    public class DietMealReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Time Time { get; set; }
    }
}
