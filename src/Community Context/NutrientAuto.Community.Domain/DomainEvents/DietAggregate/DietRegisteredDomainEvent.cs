using NutrientAuto.Shared.Events;
using System;

namespace NutrientAuto.Community.Domain.DomainEvents.DietAggregate
{
    public class DietRegisteredDomainEvent : DomainEvent
    {
        public Guid GoalId { get; }
        public Guid ProfileId { get; }
        public bool WritePost { get; }

        public DietRegisteredDomainEvent(Guid goalId, Guid profileId, bool writePost)
        {
            GoalId = goalId;
            ProfileId = profileId;
            WritePost = writePost;
        }
    }
}
