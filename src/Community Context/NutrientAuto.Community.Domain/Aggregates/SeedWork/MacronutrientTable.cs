using NutrientAuto.Shared.ValueObjects;
using System.Text;

namespace NutrientAuto.Community.Domain.Aggregates.SeedWork
{
    public class MacronutrientTable : ValueObject<MacronutrientTable>
    {
        public decimal Kcal { get; private set; }
        public decimal Kj { get; private set; }
        public decimal Protein { get; private set; }
        public decimal Carbohydrate { get; private set; }
        public decimal Fat { get; private set; }

        public static MacronutrientTable Default()
        {
            return new MacronutrientTable(carbohydrate: 0, 
                protein: 0, 
                fat: 0);
        }

        protected MacronutrientTable()
        {
        }

        public MacronutrientTable(decimal carbohydrate, decimal protein, decimal fat)
        {
            Carbohydrate = carbohydrate;
            Protein = protein;
            Fat = fat;

            Kcal = CalculateKcal();
            Kj = CalculateKj();
        }

        private decimal CalculateKcal()
        {
            decimal kcal = (Carbohydrate * 4) +
                           (Protein * 4) +
                           (Fat * 9);

            return kcal;
        }

        private decimal CalculateKj()
        {
            return Kcal * 4.184m;
        }

        public MacronutrientTable Sum(MacronutrientTable other)
        {
            return new MacronutrientTable(
                Carbohydrate + other.Carbohydrate,
                Protein + other.Protein,
                Fat + other.Fat);
        }

        public override string ToString()
        {
            return new StringBuilder()
                .AppendLine($"Kcals: {Kcal}")
                .AppendLine($"Kjs: {Kj}")
                .AppendLine($"Carboidratos: {Carbohydrate}")
                .AppendLine($"Proteínas: {Protein}")
                .AppendLine($"Gorduras: {Fat}")
                .ToString();
        }
    }
}
