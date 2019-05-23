using NutrientAuto.Shared.ValueObjects;
using System.Text;

namespace NutrientAuto.Community.Domain.Aggregates.MeasureAggregate
{
    public class BasicMeasure : ValueObject<BasicMeasure>
    {
        public decimal Height { get; private set; }
        public decimal Weight { get; private set; }
        public decimal Bmi { get; private set; }

        protected BasicMeasure()
        {
        }

        public BasicMeasure(decimal height, decimal weight)
        {
            Height = height;
            Weight = weight;

            Bmi = weight / (height * height);
        }

        public override string ToString()
        {
            return new StringBuilder()
                .AppendLine($"Altura: {Height}")
                .AppendLine($"Peso: {Weight}")
                .AppendLine($"Imc: {Bmi}")
                .ToString();
        }
    }
}
