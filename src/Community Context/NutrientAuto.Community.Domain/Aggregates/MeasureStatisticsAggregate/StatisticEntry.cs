using NutrientAuto.Shared.ValueObjects;
using System;
using System.Text;

namespace NutrientAuto.Community.Domain.Aggregates.MeasureStatisticsAggregate
{
    public class StatisticEntry : ValueObject<StatisticEntry>
    {
        public DateTime DateMeasure { get; private set; }
        public decimal Value { get; private set; }

        public StatisticEntry(DateTime dateMeasure, decimal value)
        {
            DateMeasure = dateMeasure;
            Value = value;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .AppendLine($"Data da Entrada: {DateMeasure}")
                .AppendLine($"Valor da medição: {Value}")
                .ToString();
        }
    }
}
