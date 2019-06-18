using NutrientAuto.Shared.ValueObjects;
using System.Text;

namespace NutrientAuto.Community.Domain.Aggregates.FoodAggregate
{
    public class FoodUnit : ValueObject<FoodUnit>
    {
        public UnitType UnitType { get; private set; }

        //Default Unit quantity
        //it will be used to do the calculation in diet
        //Grams or miligrams will use the multiplier to divions across quantity
        //Other unit types will use default 1
        public decimal DefaultGramsQuantityMultiplier { get; private set; }

        protected FoodUnit()
        {
        }

        public FoodUnit(UnitType unitType, decimal? defaultGramsQuantityMultiplier = null)
        {
            UnitType = unitType;
            DefaultGramsQuantityMultiplier = defaultGramsQuantityMultiplier ?? 1;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .AppendLine($"Tipo de unidade: {UnitType}")
                .AppendLine($"Multiplicador de quantidade: {DefaultGramsQuantityMultiplier}")
                .ToString();
        }
    }
}
