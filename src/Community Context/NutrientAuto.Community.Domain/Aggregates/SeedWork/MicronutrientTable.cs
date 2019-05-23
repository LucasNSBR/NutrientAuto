using NutrientAuto.Shared.ValueObjects;
using System.Text;

namespace NutrientAuto.Community.Domain.Aggregates.SeedWork
{
    public class MicronutrientTable : ValueObject<MicronutrientTable>
    {
        public decimal? Calcium { get; private set; }
        public decimal? Chromium { get; private set; }
        public decimal? Copper { get; private set; }
        public decimal? Magnesium { get; private set; }
        public decimal? Manganese { get; private set; }
        public decimal? Phosphorus { get; private set; }
        public decimal? Potassium { get; private set; }
        public decimal? Sodium { get; private set; }
        public decimal? Selenium { get; private set; }
        public decimal? Zinc { get; private set; }
        public decimal? VitaminB1 { get; private set; }
        public decimal? VitaminB2 { get; private set; }
        public decimal? VitaminB3 { get; private set; }
        public decimal? VitaminB6 { get; private set; }
        public decimal? VitaminB12 { get; private set; }
        public decimal? VitaminC { get; private set; }
        public decimal? VitaminD3 { get; private set; }
        public decimal? VitaminE { get; private set; }

        public static MicronutrientTable Default()
        {
            return new MicronutrientTable(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        }

        protected MicronutrientTable()
        {
        }

        public MicronutrientTable(decimal? calcium = null, decimal? chromium = null, decimal? copper = null, decimal? magnesium = null, decimal? manganese = null,
            decimal? phosphorus = null, decimal? potassium = null, decimal? sodium = null, decimal? selenium = null, decimal? zinc = null, decimal? vitaminB1 = null,
            decimal? vitaminB2 = null, decimal? vitaminB3 = null, decimal? vitaminB6 = null, decimal? vitaminB12 = null, decimal? vitaminC = null, decimal? vitaminD3 = null,
            decimal? vitaminE = null)
        {
            Calcium = calcium;
            Chromium = chromium;
            Copper = copper;
            Magnesium = magnesium;
            Manganese = manganese;
            Phosphorus = phosphorus;
            Potassium = potassium;
            Sodium = sodium;
            Selenium = selenium;
            Zinc = zinc;
            VitaminB1 = vitaminB1;
            VitaminB2 = vitaminB2;
            VitaminB3 = vitaminB3;
            VitaminB6 = vitaminB6;
            VitaminB12 = vitaminB12;
            VitaminC = vitaminC;
            VitaminD3 = vitaminD3;
            VitaminE = vitaminE;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .AppendLine($"Cálcio: {Calcium}")
                .AppendLine($"Cromo: {Chromium}")
                .AppendLine($"Cobre: {Copper}")
                .AppendLine($"Magnésio: {Magnesium}")
                .AppendLine($"Manganês : {Manganese}")
                .AppendLine($"Fósforo: {Phosphorus}")
                .AppendLine($"Potássio: {Potassium}")
                .AppendLine($"Sódio: {Sodium}")
                .AppendLine($"Selênio: {Selenium}")
                .AppendLine($"Zinco: {Zinc}")
                .AppendLine($"Vitamina B1: {VitaminB1}")
                .AppendLine($"Vitamina B2: {VitaminB2}")
                .AppendLine($"Vitamina B3: {VitaminB3}")
                .AppendLine($"Vitamina B6: {VitaminB6}")
                .AppendLine($"Vitamina B12: {VitaminB12}")
                .AppendLine($"Vitamina C: {VitaminC}")
                .AppendLine($"Vitamina D3: {VitaminD3}")
                .AppendLine($"Vitamina E: {VitaminE}")
                .ToString();
        }
    }
}
