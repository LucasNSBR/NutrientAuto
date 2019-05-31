using System;

namespace NutrientAuto.Community.Domain.ReadModels.MeasureAggregate
{
    public class MeasureLineReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid MeasureCategoryId { get; set; }
        public decimal Value { get; set; }
    }
}
