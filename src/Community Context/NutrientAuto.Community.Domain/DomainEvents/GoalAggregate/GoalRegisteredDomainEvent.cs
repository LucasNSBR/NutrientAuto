using NutrientAuto.Shared.Events;
using System;

namespace NutrientAuto.Community.Domain.DomainEvents.GoalAggregate
{
    public class GoalRegisteredDomainEvent : DomainEvent
    {
        public Guid GoalId { get; }
        public Guid ProfileId { get; }
        public bool WritePost { get; }

        public GoalRegisteredDomainEvent(Guid goalId, Guid profileId, bool writePost)
        {
            GoalId = goalId;
            ProfileId = profileId;
            WritePost = writePost;
        }
    }
}
