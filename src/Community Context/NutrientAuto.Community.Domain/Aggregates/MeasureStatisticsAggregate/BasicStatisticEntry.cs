using NutrientAuto.Community.Domain.Aggregates.MeasureAggregate;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.Text;

namespace NutrientAuto.Community.Domain.Aggregates.MeasureStatisticsAggregate
{
    public class BasicStatisticEntry : ValueObject<BasicStatisticEntry>
    {
        public DateTime DateMeasure { get; private set; }
        public BasicMeasure BasicMeasure { get; private set; }

        public BasicStatisticEntry(DateTime dateMeasure, BasicMeasure basicMeasure)
        {
            DateMeasure = dateMeasure;
            BasicMeasure = basicMeasure;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .AppendLine($"Data da Entrada: {DateMeasure}")
                .AppendLine($"Altura: {BasicMeasure.Height}")
                .AppendLine($"Peso: {BasicMeasure.Weight}")
                .AppendLine($"Imc: {BasicMeasure.Bmi}")
                .ToString();
        }
    }
}
