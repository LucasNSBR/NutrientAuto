using System;

namespace NutrientAuto.Community.Domain.Commands.SeedWork
{
    public class MeasureLineDto
    {
        public Guid MeasureCategoryId { get; set; }
        public decimal Value { get; set; }
    }
}
