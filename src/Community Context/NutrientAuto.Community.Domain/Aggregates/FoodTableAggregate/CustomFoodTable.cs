using System;

namespace NutrientAuto.Community.Domain.Aggregates.FoodTableAggregate
{
    public class CustomFoodTable : FoodTable
    {
        public Guid ProfileId { get; private set; }

        protected CustomFoodTable()
        {
        }

        public CustomFoodTable(Guid profileId, string name, string description)
            : base(name, description)
        {
            FoodTableType = FoodTableType.Custom;
            ProfileId = profileId;
        }
    }
}
