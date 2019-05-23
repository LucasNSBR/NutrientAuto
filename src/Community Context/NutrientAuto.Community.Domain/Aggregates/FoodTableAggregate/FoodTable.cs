using NutrientAuto.Shared.Entities;

namespace NutrientAuto.Community.Domain.Aggregates.FoodTableAggregate
{
    public class FoodTable : Entity<FoodTable>, IAggregateRoot
    {
        public FoodTableType FoodTableType { get; protected set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        protected FoodTable()
        {
        }

        public FoodTable(string name, string description)
        {
            FoodTableType = FoodTableType.Default;
            Name = name;
            Description = description;
        }

        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
