using NutrientAuto.Shared.Events;
using System;

namespace NutrientAuto.Community.Domain.DomainEvents.ProfileAggregate
{
    public class ProfileUpdatedDomainEvent : DomainEvent
    {
        public Guid Id { get; }
        public bool WritePost { get; }

        public ProfileUpdatedDomainEvent(Guid id, bool writePost)
        {
            Id = id;
            WritePost = writePost;
        }
    }
}
