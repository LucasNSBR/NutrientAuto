using NutrientAuto.Shared.Events;
using System;

namespace NutrientAuto.Community.Domain.DomainEvents.GoalAggregate
{
    public class GoalCompletedDomainEvent : DomainEvent
    {
        public Guid GoalId { get; }
        public Guid ProfileId { get; }
        public bool WritePost { get; }

        public GoalCompletedDomainEvent(Guid goalId, Guid profileId, bool writePost)
        {
            GoalId = goalId;
            ProfileId = profileId;
            WritePost = writePost;
        }
    }
}
