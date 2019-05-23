using System;

namespace NutrientAuto.Community.Domain.Aggregates.PostAggregate.Subtypes
{
    public class GoalCompletedPost : Post
    {
        protected GoalCompletedPost()
        {
        }

        public GoalCompletedPost(Guid profileId, Guid measureId)
            : base(profileId, "Objetivo completado", $"Eu completei um objetivo. Veja mais detalhes no meu registro de objetivos.")
        {
            EntityReference = EntityReference.Measure(measureId);
        }
    }
}
