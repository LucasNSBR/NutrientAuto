using System;

namespace NutrientAuto.Community.Domain.Aggregates.PostAggregate.Subtypes
{
    public class GoalRegisteredPost : Post
    {
        protected GoalRegisteredPost()
        {
        }

        public GoalRegisteredPost(Guid profileId, Guid goalId)
            : base(profileId, "Novo objetivo definido", $"Você pode ver mais detalhes no meu registro de objetivos.")
        {
            EntityReference = EntityReference.Goal(goalId);
        }
    }
}
