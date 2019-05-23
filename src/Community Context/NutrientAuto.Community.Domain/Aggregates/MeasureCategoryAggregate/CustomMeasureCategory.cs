using System;

namespace NutrientAuto.Community.Domain.Aggregates.MeasureCategoryAggregate
{
    public class CustomMeasureCategory : MeasureCategory
    {
        public Guid ProfileId { get; private set; }

        protected CustomMeasureCategory()
        {
        }

        public CustomMeasureCategory(Guid profileId, string name, string description, bool isFavorite)
            : base(name, description, isFavorite)
        {
            MeasureCategoryType = MeasureCategoryType.Custom;
            ProfileId = profileId;
        }
    }
}
