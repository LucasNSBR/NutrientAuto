using System;

namespace NutrientAuto.Community.Domain.ReadModels.DietAggregate
{
    public class DietListReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public decimal KcalsCount { get; set; }
    }
}
