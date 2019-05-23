using NutrientAuto.Community.Domain.Aggregates.MeasureCategoryAggregate;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.Text;

namespace NutrientAuto.Community.Domain.Aggregates.MeasureAggregate
{
    public class MeasureLine : ValueObject<MeasureLine>
    {
        public MeasureCategory MeasureCategory { get; private set; }
        public Guid MeasureCategoryId { get; private set; }
        public decimal Value { get; private set; }

        protected MeasureLine()
        {
        }

        public MeasureLine(MeasureCategory measureCategory, decimal value)
        {
            MeasureCategory = measureCategory;
            MeasureCategoryId = measureCategory.Id;
            Value = value;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .AppendLine($"Categoria: {MeasureCategoryId}")
                .AppendLine($"Valor da medição: {Value}")
                .ToString();
        }
    }
}
