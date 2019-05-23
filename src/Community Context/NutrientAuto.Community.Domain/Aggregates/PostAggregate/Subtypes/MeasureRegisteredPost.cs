using System;

namespace NutrientAuto.Community.Domain.Aggregates.PostAggregate.Subtypes
{
    public class MeasureRegisteredPost : Post
    {
        protected MeasureRegisteredPost()
        {
        }

        public MeasureRegisteredPost(Guid profileId, Guid measureId)
            : base(profileId, "Nova medição realizada", $"Você pode ver mais detalhes no meu registro de medições.")
        {
            EntityReference = EntityReference.Measure(measureId);
        }
    }
}
