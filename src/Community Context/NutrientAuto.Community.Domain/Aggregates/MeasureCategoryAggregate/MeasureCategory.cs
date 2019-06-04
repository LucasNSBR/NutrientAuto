using NutrientAuto.Shared.Entities;

namespace NutrientAuto.Community.Domain.Aggregates.MeasureCategoryAggregate
{
    public class MeasureCategory : Entity<MeasureCategory>, IAggregateRoot
    {
        public MeasureCategoryType MeasureCategoryType { get; protected set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsFavorite { get; private set; }

        protected MeasureCategory()
        {
        }

        public MeasureCategory(string name, string description, bool isFavorite)
        {
            MeasureCategoryType = MeasureCategoryType.Default;
            Name = name;
            Description = description;
            IsFavorite = isFavorite;
        }

        public void Update(string name, string description, bool isFavorite)
        {
            Name = name;
            Description = description;
            IsFavorite = isFavorite;
        }
    }
}
